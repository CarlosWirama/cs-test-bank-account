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
        MainMenu();
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
        MainMenu();
      } else {
        InterestMenu();
      }
    }

    static void PrintMenu()
    {
      Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>");
      Console.WriteLine("(or enter blank to go back to main menu):");
      string input = Console.ReadLine() ?? "";
      if (input == "") {
        MainMenu();
      } else if (Print.VerifyInput(input))
      {
        Print.PrintStatement(input);
        MainMenu();
      } else {
        PrintMenu();
      }
    }


    static public void MainMenu(bool isFirstTime = false)
    {
      if (isFirstTime) {
        Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
      } else {
        Console.WriteLine("Is there anything else you'd like to do?");
      }
      Console.WriteLine("[T] Input transactions");
      Console.WriteLine("[I] Define interest rules");
      Console.WriteLine("[P] Print statement");
      Console.WriteLine("[Q] Quit");
      string choice = Console.ReadLine() ?? "";
      string choiceLowerCase = choice.ToUpper();

      switch (choiceLowerCase)
      {
        case "T": TransactionMenu(); break;
        case "I": InterestMenu(); break;
        case "P": PrintMenu(); break;
        // quit
        case "Q": break;
        default: MainMenu(); break;
      }
    }
  }
}