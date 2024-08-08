using System;

public enum Priority
{
    Low,
    Medium,
    High
}

public enum Status
{
    New,
    InProgress,
    Done
}

public class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CompletionDate { get; set; }
    public DateTime CreationDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }

    public Task(string title, string description, DateTime completionDate, DateTime creationDate, Priority priority, Status status)
    {
        Title = title;
        Description = description;
        CompletionDate = completionDate;
        CreationDate = creationDate;
        Priority = priority;
        Status = status;
    }

    public override string ToString()
    {
        return $"{Title} - {Description} (Due: {CompletionDate.ToShortDateString()}, Created: {CreationDate.ToShortDateString()}, Priority: {Priority}, Status: {Status})";
    }
}
