using System.Text.Json;
using Entities;

namespace FileRepositories;

public static class TestForNow
{
    private static readonly Dictionary<Type, string> FilePaths = new Dictionary<Type, string>
    {
        { typeof(Comment), "comments.json" },
        { typeof(User), "users.json" },
        { typeof(Post), "posts.json" }
    };


    private static string CreateEmptyFileIfNotExists<T>()
    {
        string filePath = FilePaths[typeof(T)];
        
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }

        return filePath;
    }

    public static async Task<List<T>> ReadFromFileAsync<T>() where T : class
    {
        string json = await File.ReadAllTextAsync(CreateEmptyFileIfNotExists<T>());
        List<T> items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();

        return items;
    }

    public static async Task AddOneItemAsync<T>(T item) where T : class
    {
        string filePath = CreateEmptyFileIfNotExists<T>();
        
        List<T> items = JsonSerializer.Deserialize<List<T>>(File.ReadAllTextAsync(filePath).Result) ?? new List<T>();

        int maxId = items.Count > 0 ? items.Max(i => (int)i.GetType().GetProperty("Id")!.GetValue(i)!) : 1;
        item.GetType().GetProperty("Id")!.SetValue(item, maxId + 1);
        items.Add(item);
        
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(items));
    }

    public static async Task RemoveOneItemAsync<T>(T item) where T : class
    {
        string filePath = CreateEmptyFileIfNotExists<T>();
        
        List<T> items = JsonSerializer.Deserialize<List<T>>(File.ReadAllTextAsync(filePath).Result) ?? new List<T>();
        items.Remove(item);
        
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(items));
    }
}