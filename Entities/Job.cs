namespace KanS.Entities;

public class Job {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public int SectionId { get; set; }
    public virtual Section Section { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string AssignedTo { get; set; }
    public int Postion { get; set; }
}
