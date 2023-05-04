using KanS.Models;

namespace KanS.Entities;

public class Board {
    public int Id { get; set; }
    public ICollection<UserBoard> UserBoards { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public int OwnerId { get; set; }
    public ICollection<Section> Sections { get; set; }
}
