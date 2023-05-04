namespace KanS.Entities;

public class Section {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; }
    public string Name { get; set; }
    public ICollection<Job> Tasks { get; set; }
}
