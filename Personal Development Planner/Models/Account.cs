namespace Personal_Development_Planner.Models;

public class Account
{
    public Dictionary<int, int> Levels { get; set; } = new Dictionary<int, int>()
    {
        {0, 0},
        {1, 10 },
        {2, 50 },
        {3, 100 },
        {4, 200 },
        {5, 400 },
        {6, 500 },
        {7, 600 },
        {8, 800 },
        {9, 900 },
        {10, 1500 },
    };

    private int id = 0;
    
    public int Id { get; set; }
    public int Level { get; set; } = 0;
    public int Exp;
    public string FirstName;
    public string LastName;
    public string Password;
    public string Gmail;
    

    public Account(string firstName, string lastName, string password, string gmail)
    {
        Id = ++id;
        FirstName = firstName;
        LastName = lastName;
        Gmail = gmail;
        Password = password;
    }
}