using System.Collections.Generic;

public interface ITaskRepository
{
    List<Task> LoadTasks();
    void SaveTasks(List<Task> tasks);
}
