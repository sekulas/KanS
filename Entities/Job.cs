namespace KanS.Entities;

public class Job {
    public int Id { get; set; }
    public int SectionId { get; set; }
    public string Name { get; set; } = "Task";
    public string Description { get; set; } = "";
    public int Postion { get; set; }
}
