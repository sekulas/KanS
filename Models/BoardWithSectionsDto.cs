using KanS.Entities;

namespace KanS.Models;

public class BoardWithSectionsDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Favourite { get; set; }
    public virtual ICollection<SectionDto> Sections { get; set; }
}
