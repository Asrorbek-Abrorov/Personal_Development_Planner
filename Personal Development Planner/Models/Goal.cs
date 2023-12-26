using Personal_Development_Planner.Enums;

namespace Personal_Development_Planner.Models;

public class Goal
{
    private static int id = 0;

    public Goal(string name, string description, DateTime deadline, Priority priority)
    {
        Id = ++id;
        Name = name;
        Description = description;
        Deadline = deadline;
        Priority = priority;
        Status = Status.Pending;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
}