﻿namespace KanS.Entities;
public class User {

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? PasswordHash { get; set; }
    public virtual ICollection<UserBoard> UserBoards { get; set; }
    public int BoardsCreated { get; set; }

}