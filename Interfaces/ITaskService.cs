using KanS.Models;

namespace KanS.Interfaces;

public interface ITaskService {
    Task<int> CreateTask(int boardId, int sectionId);
    Task UpdateTask(int boardId, int taskId, TaskUpdateDto taskDto);
    Task RemoveTask(int boardId, int taskId);
    Task<TaskDto> GetTaskById(int boardId, int taskId);
}
