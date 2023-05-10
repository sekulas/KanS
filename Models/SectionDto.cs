﻿namespace KanS.Entities;

public class SectionDto {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public string Name { get; set; }
    public ICollection<Job> Tasks { get; set; }
}