using KanS.Models;

namespace KanS.Entities;
public class BoardUpdateDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Favourite { get; set; }
}
