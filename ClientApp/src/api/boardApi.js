import axiosClient from "./axiosClient";

const boardApi = {
  create: () => axiosClient.post("board"),
  getAllForUser: () => axiosClient.get("board"),
};

export default boardApi;
