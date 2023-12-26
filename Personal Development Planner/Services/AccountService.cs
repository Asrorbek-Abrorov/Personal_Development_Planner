using Personal_Development_Planner.Models;

namespace Personal_Development_Planner.Services;

public class AccountService
{
    public bool Create(string firstName, string lastName, string password, string gmail)
    {
        var account = new Account(firstName, lastName, password, gmail);
        return true;
    }

    public bool Update(Account account,string firstName, string lastName, string password, string gmail)
    {
        account.FirstName = firstName;
        account.LastName = lastName;
        account.Password = password;
        account.Gmail = gmail;
        return true;
    }

    public bool Delete(List<Account> accounts, int id)
    {
        accounts.RemoveAll(account => account.Id == id);
        return true;
    }
    
    public bool LevelUpgrade(int experience, Account account)
    {
        account.Exp += experience;
        for (int i = 10; i > 0; i--)
        {
            if (account.Exp > account.Levels[i])
            {
                foreach (var level in account.Levels)
                {
                    if (level.Value == account.Levels[i])
                    {
                        account.Level = level.Key;
                        break;
                    }
                }
                return true;
            }
        }
        
        return false;
    }
}