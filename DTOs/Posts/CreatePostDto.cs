﻿namespace DTOs.Posts;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
}