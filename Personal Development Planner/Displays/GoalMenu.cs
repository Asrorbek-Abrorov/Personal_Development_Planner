using Personal_Development_Planner.Enums;
using Spectre.Console;
using Personal_Development_Planner.Models;
using Personal_Development_Planner.Services;

namespace Personal_Development_Planner.Displays;

public class GoalMenu
{
    private readonly List<Goal> goals;
    private readonly GoalService goalService;
    private readonly AccountService accountService;
    private readonly Account account;
    
    public GoalMenu(GoalService goalService, AccountService accountService, List<Goal> goals, Account account)
    {
        this.goalService = goalService;
        this.goals = goals;
        this.account = account;
        this.accountService = accountService;
    }

    public void Run()
    {
        AnsiConsole.Clear();
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("* Goals Page *")
                    .LeftJustified()
                    .Color(Color.Red1));
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Goals ***[/]?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Create", "Update", "Delete",
                        "Abandon a goal", "Complete the goal",
                        "View goal", "View all goals",
                        "Back"
                    }));
            switch (choice)
            {
                case "Create":
                    var goal = CreateGoal();
                    var check = goalService.Create(goal);
                    AnsiConsole.WriteLine(check ? "Goal created successfully" : "Something went wrong!");
                    break;

                case "Update":
                    int id = AnsiConsole.Ask<int>("ID : ");
                    goal = goalService.GetGoalById(id);

                    if (goal != null)
                    {
                        choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]*** Groups ***[/]?")
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                                "All", "1 detail"
                            }));
                        switch (choice)
                        {
                            case "All":
                                goal = UpdateGoal();
                                check = goalService.Update(id, goal);
                                if (check)
                                {
                                    AnsiConsole.WriteLine("Goal updated successfully!");
                                }
                                else
                                {
                                    AnsiConsole.WriteLine("Goal not found!");
                                }
                                break;
                            case "1 detail":
                                var updateDetailChoice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("[green]*** Update Goal Details ***[/]?")
                                        .PageSize(10)
                                        .AddChoices(new[]
                                        {
                                            "Name", "Description", "Deadline", "Priority"
                                        }));

                                switch (updateDetailChoice)
                                {
                                    case "Name":
                                        goal.Name = AnsiConsole.Ask<string>("New Name: ").Trim();
                                        break;

                                    case "Description":
                                        goal.Description = AnsiConsole.Ask<string>("New Description: ").Trim();
                                        break;

                                    case "Deadline":
                                        var updatedYear = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                                .Title("[green]*** Update Deadline YEAR of the goal ***[/]?")
                                                .PageSize(10)
                                                .AddChoices(new[]
                                                {
                                                    "2023", "2024", "2025",
                                                    "2026", "2027",
                                                    "2028", "2029",
                                                    "2030"
                                                }));

                                        var updatedMonth = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                                .Title("[green]*** Update Deadline MONTH of the goal ***[/]?")
                                                .PageSize(15)
                                                .AddChoices(new[]
                                                {
                                                    "1", "2", "3",
                                                    "4", "5",
                                                    "6", "7",
                                                    "8", "9", "10",
                                                    "11", "12"
                                                }));
                                        AnsiConsole.Clear();

                                        var updatedCalendar = new Calendar(int.Parse(updatedYear), int.Parse(updatedMonth));
                                        
                                        AnsiConsole.Write(updatedCalendar);

                                        goal.Deadline = AnsiConsole.Ask<DateTime>($"Updated Deadline of the goal (yyyy-MM-dd) :");
                                        break;

                                    case "Priority":
                                        var updatedPriorityStr = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                                .Title("[green]*** Updated Priority of the goal ***[/]?")
                                                .PageSize(10)
                                                .AddChoices(new[]
                                                {
                                                    $"{Priority.Low}", $"{Priority.Medium}", $"{Priority.High}"
                                                }));
                                        switch (updatedPriorityStr)
                                        {
                                            case "Low":
                                                goal.Priority = Priority.Low;
                                                break;

                                            case "Medium":
                                                goal.Priority = Priority.Medium;
                                                break;

                                            case "High":
                                                goal.Priority = Priority.High;
                                                break;
                                        }
                                        break;

                                    default:
                                        AnsiConsole.WriteLine("Invalid choice. Please try again.");
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Goal not found");
                    }
                    break;
                
                case "Delete":
                    id = AnsiConsole.Ask<int>("ID : ");
                    check = goalService.Delete(id);
                    if (check)
                    {
                        AnsiConsole.WriteLine("Goal deleted successfully!");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Goal not found!");
                    }
                    break;
                
                case "Abandon a goal":
                    AnsiConsole.WriteLine("There is a punishment of Abondoning a goal.");
                    var ask = AnsiConsole.Ask<string>("Are you sure you want to Abondon a goal? (yes/no)");
                    if (ask == "yes")
                    {
                        id = AnsiConsole.Ask<int>("ID : ");
                        check = goalService.Abandon(id);
                        if (check)
                        {
                            AnsiConsole.WriteLine("Goal abandoned successfully!");
                            Console.WriteLine("And you will be punished by 25 exps!");
                            check = accountService.LevelUpgrade(-25, account);
                            Console.WriteLine("-25 exps!");
                            
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Goal not found!");
                        }
                        
                    }

                    break;                
                
                case "Complete the goal":
                    id = AnsiConsole.Ask<int>("ID : ");
                    check = goalService.Complete(id);
                    if (check)
                    {
                        AnsiConsole.WriteLine("Goal completed successfully!");
                        Console.WriteLine();
                        Console.WriteLine("Congratulations you will be given 15 exps!");
                        check = accountService.LevelUpgrade(15, account);
                        Console.WriteLine("+15 exps!");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Goal not found!");
                    }
                    break;
                
                case "View goal":
                    id = AnsiConsole.Ask<int>("ID : ");
                    goal = goalService.GetGoalById(id);

                    if (goal is not null)
                    {
                        AnsiConsole.WriteLine("******************************");
                        AnsiConsole.WriteLine($"Id : {goal.Id}");
                        AnsiConsole.WriteLine($"Name : {goal.Name}");
                        AnsiConsole.WriteLine($"Description : {goal.Description}");
                        AnsiConsole.WriteLine($"Deadline : {goal.Deadline}");
                        AnsiConsole.WriteLine($"Priority : {goal.Priority}");
                        AnsiConsole.WriteLine($"Status : {goal.Status}");
                        AnsiConsole.WriteLine("******************************");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Goal not found");
                    }

                    break;
                
                case "View all goals":
                    foreach (Goal goall in goals)
                    {
                        AnsiConsole.WriteLine("******************************");
                        AnsiConsole.WriteLine($"Id : {goall.Id}");
                        AnsiConsole.WriteLine($"Name : {goall.Name}");
                        AnsiConsole.WriteLine($"Description : {goall.Description}");
                        AnsiConsole.WriteLine($"Deadline : {goall.Deadline}");
                        AnsiConsole.WriteLine($"Priority : {goall.Priority}");
                        AnsiConsole.WriteLine($"Status : {goall.Status}");
                    }
                    break;
                
                case "Back":
                    keepRunning = false;
                    break;
            }
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Enter to continue...");
            Console.ReadKey();
        }
    }

    public Goal CreateGoal()
    {
        AnsiConsole.Clear();
        AnsiConsole.WriteLine("*** Creating ***");
        AnsiConsole.WriteLine();

        var name = AnsiConsole.Ask<string>("Name : ").Trim();

        var description = AnsiConsole.Ask<string>("Description : ").Trim();

        var year = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]*** Deadline YEAR of the goal ***[/]?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "2023", "2024", "2025",
                    "2026", "2027",
                    "2028", "2029",
                    "2030"
                }));

        var month = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]*** Deadline MONTH of the goal ***[/]?")
                .PageSize(15)
                .AddChoices(new[]
                {
                    "1", "2", "3",
                    "4", "5",
                    "6", "7",
                    "8", "9", "10",
                    "11", "12"
                }));
        AnsiConsole.Clear();

        var calendar = new Calendar(int.Parse(year), int.Parse(month));

        AnsiConsole.Write(calendar);

        var deadline = AnsiConsole.Ask<DateTime>($"Deadline of the goal (yyyy-MM-dd) :");

        var str = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]*** Choose the status of the goal ***[/]?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    $"{Priority.Low}", $"{Priority.Medium}", $"{Priority.High}"
                }));
        Priority priority = Priority.Medium;
        switch (str)
        {
            case "Low":
                priority = Priority.Low;
                break;

            case "Medium":
                priority = Priority.Medium;
                break;

            case "High":
                priority = Priority.High;
                break;
        }

        var goal = new Goal(name, description, deadline, priority);

        return goal;
    }
    
    
    public Goal UpdateGoal()
    {
        AnsiConsole.Clear();
        AnsiConsole.WriteLine("*** Updating ***");
        AnsiConsole.WriteLine();

        var name = AnsiConsole.Ask<string>("New Name : ").Trim();

        var description = AnsiConsole.Ask<string>("New Description : ").Trim();

        var year = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]*** Deadline YEAR of the goal ***[/]?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "2023", "2024", "2025",
                    "2026", "2027",
                    "2028", "2029",
                    "2030"
                }));

        var month = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]*** Deadline MONTH of the goal ***[/]?")
                .PageSize(15)
                .AddChoices(new[]
                {
                    "1", "2", "3",
                    "4", "5",
                    "6", "7",
                    "8", "9", "10",
                    "11", "12"
                }));
        AnsiConsole.Clear();

        var calendar = new Calendar(int.Parse(year), int.Parse(month));

        AnsiConsole.Write(calendar);

        var deadline = AnsiConsole.Ask<DateTime>($"Deadline of the goal (yyyy-MM-dd) :");

        var str = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]*** Deadline MONTH of the goal ***[/]?")
                .PageSize(10)
                .AddChoices(new[]
                {
                    $"{Priority.Low}", $"{Priority.Medium}", $"{Priority.High}"
                }));
        Priority priority = Priority.Medium;
        switch (str)
        {
            case "Low":
                priority = Priority.Low;
                break;

            case "Medium":
                priority = Priority.Medium;
                break;

            case "High":
                priority = Priority.High;
                break;
        }

        var goal = new Goal(name, description, deadline, priority);

        return goal;
    }
}
