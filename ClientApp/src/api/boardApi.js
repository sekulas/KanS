import axiosClient from "./axiosClient";

const boardApi = {
  create: () => axiosClient.post("board"),
  getAllForUser: () => axiosClient.get("board"),
  getOne: (boardId) => axiosClient.get(`board/${boardId}`),
};

export default boardApi;
