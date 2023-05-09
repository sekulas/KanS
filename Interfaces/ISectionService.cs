using KanS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KanS.Interfaces;

public interface ISectionService {
    Task<int> CreateSection(int boardId);
    Task<Section> GetSectionById(int sectionId);
}
