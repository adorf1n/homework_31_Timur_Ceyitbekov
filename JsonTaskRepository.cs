using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<Task> LoadTasks()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No tasks found. Please create tasks.");
            return new List<Task>();
        }

        var jsonString = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Task>>(jsonString) ?? new List<Task>();
    }

    public void SaveTasks(List<Task> tasks)
    {
        var jsonString = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, jsonString);
    }
}
