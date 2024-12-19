namespace TestProject
{
  class Print()
  {
    static public bool VerifyInput(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "AC001 202306"
      
      if (inputSplitted.Length != 2) return false;
      
      if (inputSplitted[1].Length != 5) return false;
      
      return true;
    }

    static public void Print(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "AC001 202306"
      
      string account = inputSplitted[0];
      string yearDate = inputSplitted[1];
      
      Console.WriteLine(" | Date    | Txn Id     | Type | Amount     | Balance     ");

      Transaction.transactionsList
        .Where(transaction =>
          transaction.account == account &&
          transaction.dateString.StartsWith(yearDate)
        )
        .ToList()
        .ForEach(transaction => {
          Console.WriteLine($" | {transaction.dateString,7} | {transaction.id,10} | {transaction.transactionType,1}    | {transaction.amount, 10:C} | {transaction.balance, 10:C} |");
        });
    }
  }
}