using System;
using Personal_Development_Planner.Displays;
using Personal_Development_Planner.Enums;
using Personal_Development_Planner.Models;
using Personal_Development_Planner.Services;
using Spectre.Console;

class Program
{
    public static void Main(string[] args)
    {
        MainMenu menu = new MainMenu();
        menu.Run();
    }
}
