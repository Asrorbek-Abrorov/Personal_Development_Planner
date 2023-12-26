using Personal_Development_Planner.Models;

namespace Personal_Development_Planner.Interfaces;

public interface IGoalService
{
    bool Create(Goal goal);
    bool Update(int id, Goal goal);
    bool Delete(int goalId);
    bool Abandon(int goalId);
    bool Complete(int goalId);
}