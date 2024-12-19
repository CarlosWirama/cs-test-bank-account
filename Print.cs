namespace TestProject
{
  class Print()
  {
    static public bool VerifyInput(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "AC001 202306"
      
      if (inputSplitted.Length != 2) return false;
      
      if (inputSplitted[1].Length != 6) return false;
      
      return true;
    }

    static public void PrintStatement(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "AC001 202306"
      
      string account = inputSplitted[0];
      string yearDate = inputSplitted[1];
      
      // since we don't have cronjob for auto-calculation every end of month,
      // assume that we won't calculate at the end of the month until the next transaction
      // is recorded on a different month.

      Console.WriteLine(" | Date     | Txn Id      | Type | Amount     | Balance      |");

      Transaction.transactionsList
        .Where(transaction =>
          transaction.accountCode == account &&
          transaction.dateString.StartsWith(yearDate)
        )
        .ToList()
        .ForEach(transaction => {
          Console.WriteLine($" | {transaction.dateString,7} | {transaction.id,10} | {transaction.transactionType,1}    | {transaction.amount, 10:C} | {transaction.balance, 10:C} |");
        });
    }
  }
}