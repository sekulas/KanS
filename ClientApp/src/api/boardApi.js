import axiosClient from "./axiosClient";

const boardApi = {
  create: () => axiosClient.post("board"),
  getAllForUser: () => axiosClient.get("board"),
  getOne: (boardId) => axiosClient.get(`board/${boardId}`),
  update: (boardId, params) => axiosClient.put(`board/${boardId}`, params),
  remove: (boardId) => axiosClient.delete(`board/${boardId}`),
  getAllFavouritesForUser: () => axiosClient.get("board/favourites"),
  getAllRequestedForUser: () => axiosClient.get(`board/request`),
  requestForParticipation: (boardId, params) =>
    axiosClient.post(`board/${boardId}/request`, params),
  respondToRequest: (boardId, params) =>
    axiosClient.put(`board/${boardId}/request`, params),
};

export default boardApi;
