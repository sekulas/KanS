namespace KanS.Entities;

public class UserBoard {
    public int AssignmentId { get; set; } //to
    public int UserId { get; set; }
    public int BoardId { get; set; } //to
    public string BoardName { get; set; } //to
    public int Postion { get; set; } //to
    public bool Favorite { get; set; } = false; //to
    public int FavouritePostion { get; set; } //to
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
    public bool Deleted { get; set; } = false;
}
