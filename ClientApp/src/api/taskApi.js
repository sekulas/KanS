import axiosClient from "./axiosClient";

const taskApi = {
  create: (boardId, params) =>
    axiosClient.post(`board/${boardId}/task`, params),
  update: (boardId, taskId, params) =>
    axiosClient.put(`board/${boardId}/task/${taskId}`, params),
  remove: (boardId, taskId) =>
    axiosClient.delete(`board/${boardId}/task/${taskId}`),
};

export default taskApi;
