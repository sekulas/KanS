namespace KanS.Entities;

public class Section {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public string Name { get; set; } = "Section";
    public List<Job> Tasks { get; set; }

}
