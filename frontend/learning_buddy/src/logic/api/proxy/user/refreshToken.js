import axiosInstance from "../../axios";

export default async function refreshToken() {
    try {
      const resp = await axiosInstance.post('refresh-token', {});
      return resp?.data?.accessToken;
    } catch (err) {
      console.error(err);
      throw err;
    }
}