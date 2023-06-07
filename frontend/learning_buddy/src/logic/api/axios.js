import axios from "axios"
import refreshToken from "./proxy/user/refreshToken";

const axiosInstance = axios.create({
    baseURL: "https://localhost:7276/api/v1",
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true
})

axiosInstance.interceptors.request.use(function (config) {
    const token = document.cookie
        .split("; ")
        .find((row) => row.startsWith("accessToken="))
        ?.split("=")[1];
    config.headers.Authorization =  `Bearer ${token}`;
    return config;
});

/*axiosInstance.interceptors.response.use(
    (response) => response,
    (error) => {
      const originalRequest = error.config;
      if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;
        return refreshToken()
            .then((accessToken) => {
                document.cookie = `accessToken=${accessToken}; SameSite=Lax; Secure; max-age=300;`
                originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                return axios(originalRequest);
            });
      }
      return Promise.reject(error);
    }
  );*/

export default axiosInstance;