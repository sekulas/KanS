namespace KanS.Models;

public class JobDto {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public int SectionId { get; set; }
    public string Name { get; set; }
    public string AssignedTo { get; set; }
    public int Position { get; set; }
}
