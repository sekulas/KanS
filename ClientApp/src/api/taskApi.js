import axiosClient from "./axiosClient";

const taskApi = {
  create: (boardId, sectionId) =>
    axiosClient.post(`board/${boardId}/section/${sectionId}/task`),
  updatePosition: (boardId, sectionId, taskId, params) =>
    axiosClient.put(
      `board/${boardId}/section/${sectionId}/task/${taskId}`,
      params
    ),
  remove: (boardId, sectionId, taskId) =>
    axiosClient.delete(`board/${boardId}/section/${sectionId}/task/${taskId}`),
};
