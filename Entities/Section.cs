namespace KanS.Entities;

public class Section {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public virtual Board Board { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TaskE> Tasks { get; set; }
    public bool Deleted { get; set; }
}
