import axiosClient from "./axiosClient";

const authApi = {
  register: (params) => axiosClient.post("account/register", params),
  login: (params) => axiosClient.post("account/login", params),
};

export default authApi;
