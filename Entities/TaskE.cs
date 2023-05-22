namespace KanS.Entities;

public class TaskE {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public int SectionId { get; set; }
    public virtual Section Section { get; set; }
    public string Name { get; set; }
    public string AssignedTo { get; set; }
    public int Position { get; set; }
    public bool Deleted { get; set; }
}
