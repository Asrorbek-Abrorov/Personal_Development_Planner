using System.Diagnostics;
using Personal_Development_Planner.Enums;
using Personal_Development_Planner.Models;
using Spectre.Console;

namespace Personal_Development_Planner.Displays;

public class ProgressTrackerMenu
{
    List<Goal> goals;

    public ProgressTrackerMenu(List<Goal> goals)
    {
        this.goals = goals;
    }

    public void BarChart()
    {
        bool keepGoing = true;
        while (keepGoing)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("* Progress Page *")
                    .LeftJustified()
                    .Color(Color.Red1));
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Personal Development Planner ***[/]?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Barchart", "Abandoned Goals", "Pending Goals", "Completed Goals"
                        , "Sorting By Priority","Exit"
                    }));
            switch (choice)
            {
                case "Barchart":
                    int sum1 = 0;
                    int sum2 = 0;
                    int sum3 = 0;
                    foreach (Goal goal in goals)
                    {
                        if (goal.Status == Status.Abandoned)
                        {
                            sum1 += 1;
                        }
                        else if (goal.Status == Status.Pending)
                        {
                            sum2 += 1;
                        }
                        else if (goal.Status == Status.Completed)
                        {
                            sum3 += 1;
                        }
                    }

                    AnsiConsole.Write(new BarChart()
                        .Width(60)
                        .Label("[green bold underline]Progress[/]")
                        .CenterLabel()
                        .AddItem($"Abandoned goals", sum1, Color.Red1)
                        .AddItem("Pending goals", sum2, Color.Yellow3)
                        .AddItem("Completed", sum3, Color.Green3)
                        .AddItem("All goals", goals.Count(), Color.Blue1));
                    
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                    sum1 = 0;
                    sum2 = 0;
                    sum3 = 0;
                    foreach (Goal goal in goals)
                    {
                        if (goal.Status == Status.Completed && goal.Priority == Priority.High)
                        {
                            sum1 += 1;
                        }
                        else if (goal.Status == Status.Completed && goal.Priority == Priority.Medium)
                        {
                            sum2 += 1;
                        }
                        else if (goal.Status == Status.Completed && goal.Priority == Priority.Low)
                        {
                            sum3 += 1;
                        }
                    }
                    
                    AnsiConsole.Write(new BarChart()
                        .Width(60)
                        .Label("[green3 bold underline]Completed Goals Priorities[/]")
                        .CenterLabel()
                        .AddItem($"High", sum1, Color.Red1)
                        .AddItem("Medium", sum2, Color.Yellow3)
                        .AddItem("Low", sum3, Color.Green3)
                        .AddItem("Overall", sum1 + sum2 + sum3, Color.Blue1));
                    
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                    sum1 = 0;
                    sum2 = 0;
                    sum3 = 0;
                    foreach (Goal goal in goals)
                    {
                        if (goal.Status == Status.Pending && goal.Priority == Priority.High)
                        {
                            sum1 += 1;
                        }
                        else if (goal.Status == Status.Pending && goal.Priority == Priority.Medium)
                        {
                            sum2 += 1;
                        }
                        else if (goal.Status == Status.Pending && goal.Priority == Priority.Low)
                        {
                            sum3 += 1;
                        }
                    }
                    
                    AnsiConsole.Write(new BarChart()
                        .Width(60)
                        .Label("[yellow1 bold underline]Pending Goals Priorities[/]")
                        .CenterLabel()
                        .AddItem($"High", sum1, Color.Red1)
                        .AddItem("Medium", sum2, Color.Yellow3)
                        .AddItem("Low", sum3, Color.Green3)
                        .AddItem("Overall", sum1 + sum2 + sum3, Color.Blue1));
                    
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                    sum1 = 0;
                    sum2 = 0;
                    sum3 = 0;
                    foreach (Goal goal in goals)
                    {
                        if (goal.Status == Status.Abandoned && goal.Priority == Priority.High)
                        {
                            sum1 += 1;
                        }
                        else if (goal.Status == Status.Abandoned && goal.Priority == Priority.Medium)
                        {
                            sum2 += 1;
                        }
                        else if (goal.Status == Status.Abandoned && goal.Priority == Priority.Low)
                        {
                            sum3 += 1;
                        }
                    }
                    
                    AnsiConsole.Write(new BarChart()
                        .Width(60)
                        .Label("[red1 bold underline]Abondened Goals Priorities[/]")
                        .CenterLabel()
                        .AddItem($"High", sum1, Color.Red1)
                        .AddItem("Medium", sum2, Color.Yellow3)
                        .AddItem("Low", sum3, Color.Green3)
                        .AddItem("Overall", sum1 + sum2 + sum3, Color.Blue1));
                    break;
                
                case "Abandoned Goals":
                    Console.Clear();
                    Console.WriteLine("*** Abandoned Goals ***");
                    Console.WriteLine();
                    foreach (var goal in goals)
                    {
                        if (goal.Status == Status.Abandoned)
                        {
                            Console.WriteLine($"Id : {goal.Id}");
                            Console.WriteLine($"Name : {goal.Name}");
                            Console.WriteLine($"Description : {goal.Description}");
                            Console.WriteLine($"Deadline : {goal.Deadline}");
                            Console.WriteLine($"Priority : {goal.Priority}");
                            Console.WriteLine($"Status : {goal.Status}");
                        }
                    }
                    break;
                
                case "Completed Goals":
                    Console.Clear();
                    Console.WriteLine("*** Completed Goals ***");
                    Console.WriteLine();
                    foreach (var goal in goals)
                    {
                        if (goal.Status == Status.Completed)
                        {
                            Console.WriteLine($"Id : {goal.Id}");
                            Console.WriteLine($"Name : {goal.Name}");
                            Console.WriteLine($"Description : {goal.Description}");
                            Console.WriteLine($"Deadline : {goal.Deadline}");
                            Console.WriteLine($"Priority : {goal.Priority}");
                            Console.WriteLine($"Status : {goal.Status}");
                        }
                    }
                    break;
                
                case "Pending Goals":
                    Console.Clear();
                    Console.WriteLine("*** Pending Goals ***");
                    Console.WriteLine();
                    foreach (var goal in goals)
                    {
                        if (goal.Status == Status.Pending)
                        {
                            Console.WriteLine($"Id : {goal.Id}");
                            Console.WriteLine($"Name : {goal.Name}");
                            Console.WriteLine($"Description : {goal.Description}");
                            Console.WriteLine($"Deadline : {goal.Deadline}");
                            Console.WriteLine($"Priority : {goal.Priority}");
                            Console.WriteLine($"Status : {goal.Status}");
                        }
                    }
                    break;
                
                case "Sorting By Priority":
                    Console.Clear();
                    bool goOn = true;
                    while (goOn)
                    {
                        Console.Clear();
                        choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[green]*** Personal Development Planner ***[/]?")
                                .PageSize(10)
                                .AddChoices(new[]
                                {
                                    "Low", "Medium", "High", "Back"
                                }));
                        switch (choice)
                        {
                            case "Low":
                                choice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("[green]*** Personal Development Planner ***[/]?")
                                        .PageSize(10)
                                        .AddChoices(new[]
                                        {
                                            "Abandoned", "Pending", "Completed", "Back"
                                        }));
                                switch (choice)
                                {
                                    case "Abandoned":
                                        Console.Clear();
                                        Console.WriteLine("*** Low Priority Abandoned Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Abandoned && goal.Priority == Priority.Low)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                     
                                    case "Pending":
                                        Console.Clear();
                                        Console.WriteLine("*** Low Priority Abandoned Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Pending && goal.Priority == Priority.Low)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                     
                                    case "Completed":
                                        Console.Clear();
                                        Console.WriteLine("*** Low Priority Abandoned Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Completed && goal.Priority == Priority.Low)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                    case "Back":
                                        goOn = false;
                                        break;
                                }
                                break;
                             
                            case "Medium":
                                choice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("[green]*** Personal Development Planner ***[/]?")
                                        .PageSize(10)
                                        .AddChoices(new[]
                                        {
                                            "Abandoned", "Pending", "Completed", "Back"
                                        }));
                                switch (choice)
                                {
                                    case "Abandoned":
                                        Console.Clear();
                                        Console.WriteLine("*** Medium Priority Abandoned Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Abandoned && goal.Priority == Priority.Medium)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                     
                                    case "Pending":
                                        Console.Clear();
                                        AnsiConsole.WriteLine("*** Medium Priority Pending Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Pending && goal.Priority == Priority.Medium)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                     
                                    case "Completed":
                                        Console.Clear();
                                        Console.WriteLine("*** Medium Priority Completed Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Completed && goal.Priority == Priority.Medium)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                    case "Back":
                                        goOn = false;
                                        break;
                                }
                                break;
                             
                            case "High":
                                choice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("[green]*** Personal Development Planner ***[/]?")
                                        .PageSize(10)
                                        .AddChoices(new[]
                                        {
                                            "Abandoned", "Pending", "Completed", "Back"
                                        }));
                                switch (choice)
                                {
                                    case "Abandoned":
                                        Console.Clear();
                                        Console.WriteLine("*** High Priority Abandoned Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Abandoned && goal.Priority == Priority.High)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                     
                                    case "Pending":
                                        Console.Clear();
                                        Console.WriteLine("*** High Priority Pending Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Pending && goal.Priority == Priority.High)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                     
                                    case "Completed":
                                        Console.Clear();
                                        Console.WriteLine("*** High Priority Completed Goals ***");
                                        Console.WriteLine();
                                        foreach (var goal in goals)
                                        {
                                            if (goal.Status == Status.Completed && goal.Priority == Priority.High)
                                            {
                                                Console.WriteLine("**********************************************");
                                                Console.WriteLine($"Id : {goal.Id}");
                                                Console.WriteLine($"Name : {goal.Name}");
                                                Console.WriteLine($"Description : {goal.Description}");
                                                Console.WriteLine($"Deadline : {goal.Deadline}");
                                                Console.WriteLine($"Priority : {goal.Priority}");
                                                Console.WriteLine($"Status : {goal.Status}");
                                            }
                                        }
                                        break;
                                    case "Back":
                                        goOn = false;
                                        break;
                                }
                                break;
                            case "Back":
                                goOn = false;
                                break;
                        }
                        Console.WriteLine();
                        Console.WriteLine("Enter to continue...");
                        Console.ReadKey();
                    }
                    
                    break;
                    
                
                case "Exit":
                    keepGoing = false;
                    break;
            }
            Console.WriteLine();
            AnsiConsole.Write("Enter to continue...");
            Console.ReadKey();
        }
    }
    
}