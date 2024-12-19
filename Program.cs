// See https://aka.ms/new-console-template for more information

// using Account;
using TestProject;

/**
 * I came from javascript/typescript background, so my approach looks more functional oriented instead of OOP.
 * I chose this to do the assignment quickly. I do have some experience in OOP (C# and Java) long way back and
 * I'm willing to pick up again, so whatever language I'm going to use should not be a problem.
 *
 * Thank you for understanding!
 */


static void TransactionMenu()
{
  Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format");
  Console.WriteLine("(or enter blank to go back to main menu):");
  string input = Console.ReadLine() ?? "";
  Account.VerifyInput(input);
}


static void MainMenu()
{

  Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
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
      break;
    default:
      break;
  }
}



// int y = Convert.ToInt32(Console.ReadLine() ?? "");
// Console.WriteLine(x);

MainMenu();

Console.WriteLine("Thank you for banking with AwesomeGIC Bank.");
