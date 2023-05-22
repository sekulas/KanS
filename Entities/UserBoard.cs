namespace KanS.Entities;

public class UserBoard {
    public int AssignmentId { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int BoardId { get; set; } 
    public virtual Board Board { get; set; } 
    public DateTime AssignmentDate { get; set; }
    public bool Deleted { get; set; }
    public String ParticipatingAccepted { get; set; }
}
