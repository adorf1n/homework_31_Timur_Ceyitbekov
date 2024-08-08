using System;

class Program
{
    static void Main(string[] args)
    {
        var filePath = "tasks.json";
        ITaskRepository repository = new JsonTaskRepository(filePath);
        var taskManager = new TaskManager(repository);

        taskManager.TaskDone += (sender, e) =>
        {
            Console.Beep();
            Console.WriteLine("Congratulations! A task is done!");
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Task Manager Menu:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Change Task Status");
            Console.WriteLine("3. Change Task Description");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Display Tasks");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddTask(taskManager);
                    break;
                case "2":
                    ChangeTaskStatus(taskManager);
                    break;
                case "3":
                    ChangeTaskDescription(taskManager);
                    break;
                case "4":
                    DeleteTask(taskManager);
                    break;
                case "5":
                    DisplayTasks(taskManager);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private static void AddTask(TaskManager taskManager)
    {
        try
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();
            Console.Write("Completion Date (dd.mm.yyyy): ");
            var completionDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);
            var creationDate = DateTime.Now;
            Console.Write("Priority (Low, Medium, High): ");
            var priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);
            var task = new Task(title, description, completionDate, creationDate, priority, Status.New);
            taskManager.AddTask(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ChangeTaskStatus(TaskManager taskManager)
    {
        try
        {
            Console.Write("Enter task index: ");
            var index = int.Parse(Console.ReadLine());
            Console.Write("New Status (New, InProgress, Done): ");
            var newStatus = (Status)Enum.Parse(typeof(Status), Console.ReadLine(), true);
            taskManager.ChangeTaskStatus(index, newStatus);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ChangeTaskDescription(TaskManager taskManager)
    {
        try
        {
            Console.Write("Enter task index: ");
            var index = int.Parse(Console.ReadLine());
            Console.Write("New Description: ");
            var newDescription = Console.ReadLine();
            taskManager.ChangeTaskDescription(index, newDescription);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void DeleteTask(TaskManager taskManager)
    {
        try
        {
            Console.Write("Enter task index: ");
            var index = int.Parse(Console.ReadLine());
            taskManager.DeleteTask(index);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void DisplayTasks(TaskManager taskManager)
    {
        Console.WriteLine("Sort By (title, date, priority): ");
        var sortBy = Console.ReadLine();
        taskManager.DisplayTasks(sortBy);
    }
}
