namespace KanS.Entities;

public class UserBoard {
    public int AssignmentId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int BoardId { get; set; } 
    public Board Board { get; set; } 
    public int Postion { get; set; }
    public bool Favorite { get; set; }
    public int FavouritePostion { get; set; }
    public DateTime AssignmentDate { get; set; }
    public bool Deleted { get; set; }
}
