using System;
using System.Collections.Generic;
using System.Linq;

public class TaskManager
{
    private List<Task> _tasks;
    private readonly ITaskRepository _repository;

    public delegate void TaskDoneEventHandler(object sender, EventArgs e);
    public event TaskDoneEventHandler TaskDone;

    public TaskManager(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = _repository.LoadTasks();
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
        SaveTasks();
        Console.WriteLine("Task added.");
        DisplayTasks();
    }

    public void ChangeTaskStatus(int index, Status newStatus)
    {
        if (index < 0 || index >= _tasks.Count)
        {
            Console.WriteLine("Error: Task index out of range.");
            return;
        }

        var task = _tasks[index];
        var oldStatus = task.Status;

        if (oldStatus == Status.Done)
        {
            Console.WriteLine("Error: Cannot change the status of a completed task.");
            return;
        }

        if (oldStatus == Status.InProgress && newStatus == Status.New)
        {
            Console.WriteLine("Error: Cannot change status to New from InProgress.");
            return;
        }

        if (oldStatus == Status.New && newStatus == Status.Done)
        {
            Console.WriteLine("Error: Cannot change status to Done from New.");
            return;
        }

        if (oldStatus == Status.InProgress && newStatus == Status.Done)
        {
            task.Status = newStatus;
            OnTaskDone();
        }
        else
        {
            task.Status = newStatus;
        }

        SaveTasks();
        Console.WriteLine("Task status changed.");
        DisplayTasks();
    }

    public void ChangeTaskDescription(int index, string newDescription)
    {
        if (index < 0 || index >= _tasks.Count)
        {
            Console.WriteLine("Error: Task index out of range.");
            return;
        }

        var task = _tasks[index];

        if (task.Status == Status.Done)
        {
            Console.WriteLine("Error: Cannot change the description of a completed task.");
            return;
        }

        task.Description = newDescription;
        SaveTasks();
        Console.WriteLine("Task description changed.");
        DisplayTasks();
    }

    public void DeleteTask(int index)
    {
        if (index < 0 || index >= _tasks.Count)
        {
            Console.WriteLine("Error: Task index out of range.");
            return;
        }

        var task = _tasks[index];

        if (task.Status != Status.New)
        {
            Console.WriteLine("Error: Only new tasks can be deleted.");
            return;
        }

        _tasks.RemoveAt(index);
        SaveTasks();
        Console.WriteLine("Task deleted.");
        DisplayTasks();
    }

    public void DisplayTasks(string sortBy = "priority")
    {
        IEnumerable<Task> sortedTasks;

        switch (sortBy.ToLower())
        {
            case "title":
                sortedTasks = _tasks.OrderBy(t => t.Title);
                break;
            case "date":
                sortedTasks = _tasks.OrderBy(t => t.CreationDate);
                break;
            case "priority":
                sortedTasks = _tasks.OrderBy(t => t.Priority);
                break;
            default:
                sortedTasks = _tasks;
                break;
        }

        foreach (var task in sortedTasks)
        {
            if (task.CompletionDate < DateTime.Now && task.Status != Status.Done)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{task} - Overdue");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(task);
            }
        }
    }

    private void SaveTasks()
    {
        _repository.SaveTasks(_tasks);
    }

    protected virtual void OnTaskDone()
    {
        TaskDone?.Invoke(this, EventArgs.Empty);
        Console.Beep();
    }
}
