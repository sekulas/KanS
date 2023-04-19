import axiosClient from "./axiosClient";

const authApi = {
  register: (params) => axiosClient.post("account/register", params),
  login: async (params) => {
    try {
      const response = await axiosClient.post("account/login", params);
      const { data } = response;
      localStorage.setItem("token", data);
      return data;
    } catch (error) {
      throw error;
    }
  },
};

export default authApi;
