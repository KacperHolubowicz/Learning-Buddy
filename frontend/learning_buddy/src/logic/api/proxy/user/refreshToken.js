import axiosAuth from "../../authAxios";

export default function refreshToken() {
  return new Promise((resolve, reject) => {
    return axiosAuth.post('refresh-token', JSON.stringify({}))
    .then((response) => {
      resolve(response)
    }).catch((error) => {
      reject(error)
    })
  })
}