using System.Diagnostics;
using System.Text.RegularExpressions;
using Personal_Development_Planner.Models;
using Personal_Development_Planner.Services;
using Spectre.Console;

namespace Personal_Development_Planner.Displays;

public class AccountMenu
{
    private readonly Account account;
    private readonly AccountService accountService;

    public AccountMenu(AccountService accountService, Account account)
    {
        this.accountService = accountService;
        this.account = account;
    }

    public void Run()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("* Account Page *")
                    .LeftJustified()
                    .Color(Color.Red1));
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Account Management ***[/]?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Update account", "Account levels", "Account Details", "Back"
                    }));

            string? password;
            switch (choice)
            {
                case "Update account":
                    choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]*** Account Updating ***[/]?")
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                                "All", "1 Detail", "Back"
                            }));
                    switch (choice)
                    {
                        case "All":
                            AnsiConsole.Clear();
                            var firstName = AnsiConsole.Ask<string>("New First Name : ");
                            var lastName = AnsiConsole.Ask<string>("New Last Name : ");
                            do
                            {
                                password = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Enter new[green]password[/]?")
                                        .PromptStyle("red")
                                        .Secret()).Trim();

                                if (password.Length < 4)
                                {
                                    AnsiConsole.MarkupLine("[red1]Password must have at least 4 characters.[/]");
                                }
                            } while (password.Length < 4);
                            var gmail = AskEmailAddress("New Gmail : ");
                            accountService.Update(account, firstName, lastName, password, gmail);
                            AnsiConsole.WriteLine("Account updated successfully");
                            break;
                        case "1 Detail":
                            AnsiConsole.Clear();
                            choice = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[green]*** Choose 1 Detail ***[/]?")
                                    .PageSize(10)
                                    .AddChoices(new[]
                                    {
                                        "First Name", "Last Name", "Password", "Gmail"
                                    }));
                            switch (choice)
                            {
                                case "First Name":
                                    firstName = AnsiConsole.Ask<string>("New First Name : ");
                                    account.FirstName = firstName;
                                    AnsiConsole.WriteLine("Account updated successfully");
                                    break;
                                
                                case "Last Name":
                                    lastName = AnsiConsole.Ask<string>("New Last Name : ");
                                    account.LastName = lastName;
                                    AnsiConsole.WriteLine("Account updated successfully");
                                    break;
                                
                                case "Password":
                                    do
                                    {
                                        password = AnsiConsole.Prompt(
                                            new TextPrompt<string>("Enter new [green]password[/]?")
                                                .PromptStyle("red")
                                                .Secret()).Trim();
                                        if (password.Length < 4)
                                        {
                                            AnsiConsole.MarkupLine("[red1]Password must have at least 4 characters.[/]");
                                        }
                                    } while (password.Length < 4);
                                    var sPassword = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Enter new [green]password[/] again?")
                                            .PromptStyle("red")
                                            .Secret()).Trim();
                                    if (password == sPassword)
                                    {
                                        account.Password = password;
                                        AnsiConsole.WriteLine("Account updated successfully");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Passwords didn't match");
                                    }
                                    break;
                                
                                case "Gmail":
                                    gmail = AskEmailAddress("New Gmail : ");
                                    account.Gmail = gmail;
                                    AnsiConsole.WriteLine("Account updated successfully");
                                    break;
                            }
                            break;
                        case "Back":
                            break;
                    }
                    break;
                case "Account levels":
                    AnsiConsole.Write(
                        new FigletText("* Levels *")
                            .LeftJustified()
                            .Color(Color.Red1));
                    AnsiConsole.Write(new BreakdownChart()
                        .Width(60)
                        .AddItem("Exp gained", account.Exp, Color.Red)
                        .AddItem("Left exp", account.Levels[account.Level + 1] - account.Exp, Color.Blue));
                    AnsiConsole.WriteLine($"Account level : {account.Level}");
                    break;
                
                case "Account Details":
                    AnsiConsole.Write(
                        new FigletText("* Security *")
                            .LeftJustified()
                            .Color(Color.Red1));
                    password = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter [green]password[/] to [red1]Enter[/]?")
                            .PromptStyle("red")
                            .Secret());
                    if (password == account.Password)
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.Write(
                            new FigletText("* Don't show this to anyone *")
                                .LeftJustified()
                                .Color(Color.Red1));
                        Console.WriteLine();
                        AnsiConsole.WriteLine("*** Account details ***");
                        Console.WriteLine($"First name: {account.FirstName}");
                        Console.WriteLine($"Last name: {account.LastName}");
                        Console.WriteLine($"Password : {account.Password}");
                        Console.WriteLine($"Gmail : {account.Gmail}");
                    }
                    else
                    {
                        Console.WriteLine("You have no rights to see these informations!");
                    }
                    
                    break;
                
                case "Back":
                    keepRunning = false;
                    break;
            }

            Console.WriteLine();
            AnsiConsole.WriteLine("Enter to continue...");
            Console.ReadKey();

        }
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