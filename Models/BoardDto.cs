﻿using KanS.Models;

namespace KanS.Entities;
public class BoardDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Favourite { get; set; }
}
