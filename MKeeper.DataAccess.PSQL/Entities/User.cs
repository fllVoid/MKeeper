﻿namespace MKeeper.DataAccess.PSQL.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}
