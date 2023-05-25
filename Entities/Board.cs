namespace KanS.Entities;

public class Board {
    public int Id { get; set; }
    public virtual ICollection<UserBoard> UserBoards { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Favourite { get; set; }
    public int OwnerId { get; set; }
    public virtual ICollection<Section> Sections { get; set; }
}
