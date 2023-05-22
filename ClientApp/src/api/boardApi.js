import axiosClient from "./axiosClient";

const boardApi = {
  create: () => axiosClient.post("board"),
  getAllForUser: () => axiosClient.get("board"),
  getAllFavouritesForUser: () => axiosClient.get("board/favourites"),
  getOne: (boardId) => axiosClient.get(`board/${boardId}`),
  update: (boardId, params) => axiosClient.put(`board/${boardId}`, params),
  remove: (boardId) => axiosClient.delete(`board/${boardId}`),
};

export default boardApi;
