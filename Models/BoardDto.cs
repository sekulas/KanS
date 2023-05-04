using KanS.Models;

namespace KanS.Entities;
public class BoardDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public ICollection<Section> Sections { get; set; }
}
