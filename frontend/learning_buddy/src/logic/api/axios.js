import axios from "axios"
import refreshToken from "./proxy/user/refreshToken";

const axiosApi = axios.create({
    baseURL: "https://localhost:7276/api/v1",
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true
})

axiosApi.interceptors.request.use(function (config) {
    const token = document.cookie
        .split("; ")
        .find((row) => row.startsWith("accessToken="))
        ?.split("=")[1];
    config.headers.Authorization =  `Bearer ${token}`;
    return config;
});

axiosApi.interceptors.response.use(
    (response) => response,
    (error) => {
      const originalRequest = error.config;
      if ((error.response.status === 401 || error.response.status === 403) && !originalRequest._retry) {
        originalRequest._retry = true;
        return refreshToken()
            .then((accessToken) => {
                document.cookie = `accessToken=${accessToken?.data?.accessToken}; SameSite=Lax; Secure; max-age=300; domain=localhost; path=/;`
                originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                return axiosApi(originalRequest);
            });
      }
      return Promise.reject(error);
    }
  );

export default axiosApi;