﻿namespace EfcRepository;

public class EfcUser
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}