﻿using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
        _ = AddAsync(new Post("Cat discussion", "Cats are pretty neat, sometimes.", 1)).Result;
        _ = AddAsync(new Post("Cat discussion 2", "Cat dropped a dead bird in my bed. No longer neat.", 1)).Result;
        _ = AddAsync(new Post("Dog discussion", "Dogs are just far superior to cats. EOD.", 3)).Result;
        _ = AddAsync(new Post("Weather?", "So, does anyone else like weather?", 2)).Result;
        _ = AddAsync(new Post("DNP QA", "This post is for DNP discussions, or if you need help with stuff.", 4)).Result;
        _ = AddAsync(new Post("Best lawn mower?", "What's the bet lawn mower robot to mow my living room carpet?", 3)).Result;
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException($"Post with id {post.Id} does not exist");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove == null)
        {
            throw new InvalidOperationException($"Post with id {id} not found");
        }
        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingle(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException($"Post with id {id} not found");
        }
        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}