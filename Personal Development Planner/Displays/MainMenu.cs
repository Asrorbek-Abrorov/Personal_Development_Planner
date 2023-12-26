using System.Text.RegularExpressions;
using Personal_Development_Planner.Models;
using Personal_Development_Planner.Services;
using Spectre.Console;
namespace Personal_Development_Planner.Displays;

public class MainMenu
{
    private readonly ProgressTrackerMenu progressTracker;
    private readonly AccountMenu accountMenu;
    private readonly AccountService accountService;
    private readonly GoalMenu goalMenu;
    private readonly GoalService goalService;
    private readonly List<Goal> goals;
    private readonly Account account;
    public MainMenu()
    {
        goals = new List<Goal>();
        goalService = new GoalService(goals);
        progressTracker = new ProgressTrackerMenu(goals);
        account = CreateAccount();
        accountService = new AccountService();
        accountMenu = new AccountMenu(accountService, account);
        goalMenu = new GoalMenu(goalService, accountService, goals, account);
    }
    public void Run()
    {
        Console.Clear();
        
        AnsiConsole.Write(
            new FigletText("Welcome to project")
                .LeftJustified()
                .Color(Color.Aquamarine3));
        AnsiConsole.Write(
            new FigletText("* Personal  Development  Planner *")
                .LeftJustified()
                .Color(Color.Green1));
        
        AnsiConsole.WriteLine("Enter to continue...");
        Console.ReadKey();
        
        AnsiConsole.Clear();
        
        AnsiConsole.Write(
            new FigletText("GET   READY!!!")
                .LeftJustified()
                .Color(Color.Red1));
            
        AnsiConsole.WriteLine("Enter to continue...");
        Console.ReadKey();
        
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("* Main Page *")
                    .LeftJustified()
                    .Color(Color.Red1));
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Personal Development Planner ***[/]?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Goals", "Progress Tracker", "Account Management", "Exit"
                    }));
            
            switch (choice)
            {
                case "Goals":
                    goalMenu.Run();
                    break;

                case "Progress Tracker":
                    progressTracker.BarChart();
                    break;
                
                case "Account Management":
                    var password = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter [green]password[/] to [red1]Enter[/]?")
                            .PromptStyle("red")
                            .Secret());
                    if (password == account.Password)
                    {
                        accountMenu.Run();
                    }
                    else
                    {
                        Console.WriteLine("You have no rights to see this information!");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Enter to continue...");
                    Console.ReadKey();
                    break;
                
                case "Exit":
                    keepRunning = false;
                    break;
            }
        }
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Thanks for using my application, Goodbye!")
                .LeftJustified()
                .Color(Color.Gold3));
        AnsiConsole.Write(
            new FigletText("Designed and Engineered By Asrorbek Abrorov.")
                .LeftJustified()
                .Color(Color.Green1));
    }

    public Account CreateAccount()
    {
        AnsiConsole.Write(
            new FigletText("Creating Account ")
                .LeftJustified()
                .Color(Color.Green1));
        Console.WriteLine();
        var firstName = AnsiConsole.Ask<string>("First Name : ").Trim();
        var lastName = AnsiConsole.Ask<string>("Last Name : ").Trim();

        string password;
        do
        {
            password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]?")
                    .PromptStyle("red")
                    .Secret()).Trim();

            if (password.Length < 4)
            {
                AnsiConsole.MarkupLine("[red1]Password must have at least 4 characters.[/]");
            }
        } while (password.Length < 4);

        var gmail = AskEmailAddress("Gmail : ");
        var accountCreate = new Account(firstName, lastName, password, gmail);

        return accountCreate;
    }
    
    private string AskEmailAddress(string prompt)
    {
        while (true)
        {
            var gmailAddress = AnsiConsole.Ask<string>(prompt).Trim();
            if (ValidateEmailAddress(gmailAddress))
            {
                return gmailAddress;
            }
            else
            {
                AnsiConsole.WriteLine("Invalid gmail address format. Please enter a valid gmail address (***@gmail.com.");
                AnsiConsole.WriteLine();
            }
        }
    }
    
    private bool ValidateEmailAddress(string emailAddress)
    {
        string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        return Regex.IsMatch(emailAddress, pattern);
    }
}