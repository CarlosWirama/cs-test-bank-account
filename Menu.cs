namespace TestProject
{
  class Menu()
  {
    static void TransactionMenu()
    {
      Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format");
      Console.WriteLine("(or enter blank to go back to main menu):");
      string input = Console.ReadLine() ?? "";
      if (input == "") {
        MainMenu();
      } else if (Transaction.VerifyInput(input))
      {
        Transaction.CreateTransaction(input);
      } else {
        TransactionMenu();
      }
    }

    static void InterestMenu()
    {
      Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format");
      Console.WriteLine("(or enter blank to go back to main menu):");
      string input = Console.ReadLine() ?? "";
      if (input == "") {
        MainMenu();
      } else if (Interest.VerifyInput(input))
      {
        Interest.CreateInterest(input);
      } else {
        InterestMenu();
      }
    }


    static public void MainMenu()
    {
      Console.WriteLine("[T] Input transactions");
      Console.WriteLine("[I] Define interest rules");
      Console.WriteLine("[P] Print statement");
      Console.WriteLine("[Q] Quit");
      string choice = Console.ReadLine() ?? "";
      string choiceLowerCase = choice.ToLower();

      switch (choiceLowerCase)
      {
        case "t":
          TransactionMenu();
          break;
        case "i":
          break;
        case "p":
          break;
        case "q":
          // quit
          break;
        default:
          MainMenu();
          break;
      }
    }
  }
}