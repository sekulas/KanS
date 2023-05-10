using KanS.Entities;
using KanS.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Interfaces;

public interface ISectionService {
    Task<int> CreateSection(int boardId);
    Task UpdateSection(int sectionId, SectionUpdateDto sectionDto);
    Task RemoveSection(int sectionId, int boardId);
    Task<SectionDto> GetSectionById(int sectionId);
}
