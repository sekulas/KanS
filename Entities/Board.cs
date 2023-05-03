using KanS.Models;

namespace KanS.Entities;

public class Board {
    public int Id { get; set; }
    public string Name { get; set; } = "Board";
    public string Description { get; set; } = "";
    public string Icon { get; set; }
    public int OwnerId { get; set; }
    public List<Section> Sections { get; set; }
    public List<UserDto> UsersWithAccess { get; set; }
}
