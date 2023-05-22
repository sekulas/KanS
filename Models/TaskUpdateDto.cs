namespace KanS.Models;

public class TaskUpdateDto {
    public int? SectionId { get; set; }
    public string? Name { get; set; }
    public string? AssignedTo { get; set; }
    public int? Position { get; set; }
}
