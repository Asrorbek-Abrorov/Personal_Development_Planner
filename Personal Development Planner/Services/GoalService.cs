using Personal_Development_Planner.Interfaces;
using Personal_Development_Planner.Models;

namespace Personal_Development_Planner.Services;

public class GoalService : IGoalService
{
    private List<Goal> goals;

    public GoalService(List<Goal> goals)
    {
        this.goals = goals;
    }
    
    public Goal GetGoalById(int id) => goals.FirstOrDefault(g => g.Id == id);
    
    public List<Goal> GetGoals() => goals;

    public bool Create(Goal goal)
    {
        goals.Add(goal);
        return true;
    }

    public bool Update(int id, Goal goal)
    {
        Goal existingGoal = GetGoalById(id);
        if (existingGoal != null)
        {
            existingGoal.Name = goal.Name;
            existingGoal.Description = goal.Description;
            existingGoal.Deadline = goal.Deadline;
            existingGoal.Priority = goal.Priority;
            existingGoal.Status = goal.Status;
            return true;
        }

        return false;
    }

    public bool Delete(int id)
    {
        Goal goal = GetGoalById(id);
        if (goal != null)
        {
            goals.Remove(goal);
            return true;
        }

        return false;
    }

    public bool Abandon(int id)
    {
        var existingGoal = GetGoalById(id);
        if (existingGoal != null)
        {
            existingGoal.Status = Status.Abandoned;
            return true;
        }
        return false;
    }

    public bool Complete(int id)
    {
        var existingGoal = GetGoalById(id);
        if (existingGoal != null)
        {
            existingGoal.Status = Status.Completed;
            return true;
        }
        return false;
    }
}