using KanS.Entities;
using KanS.Models;

namespace KanS.Interfaces;

public interface IJobService {
    Task<int> CreateJob(int boardId, int sectionId);
    Task UpdateJob(int boardId, int sectionId, int jobId, JobUpdateDto jobDto);
    Task RemoveJob(int boardId, int sectionId, int jobId);
    Task<JobDto> GetJobById(int boardId, int jobId);
}
