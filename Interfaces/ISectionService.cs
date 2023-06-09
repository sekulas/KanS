﻿using KanS.Entities;
using KanS.Models;

namespace KanS.Interfaces;

public interface ISectionService {
    Task<int> CreateSection(int boardId);
    Task UpdateSection(int sectionId, SectionUpdateDto sectionDto);
    Task RemoveSection(int boardId, int sectionId);
    Task<SectionDto> GetSectionById(int boardId, int sectionId);
}
