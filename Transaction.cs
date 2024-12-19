namespace TestProject
{
  class Transaction(string dateString, string accountCode, string transactionType, double amount)
  {
    public string id = generateId(dateString, accountCode);
    public string dateString = dateString;
    public string accountCode = accountCode;
    public string transactionType = transactionType;
    public double amount = amount;
    // TODO
    public double balance = amount;

    public static List<Transaction> transactionsList = [];

    static private string generateId(string dateString, string accountCode)
    {
      Transaction? lastTransactionOfTheDay = transactionsList
        .Where(transaction => transaction.accountCode == accountCode)
        .ToList()
        .FindLast(transaction => transaction.dateString == dateString) ?? null;
      if (lastTransactionOfTheDay == null)
      {
        return dateString + "-01";
      }
      string[] splittedId = lastTransactionOfTheDay.id.Split('-');
      int newTransactionNo = Convert.ToInt32(splittedId[1]) + 1;
      string newTransactionString = newTransactionNo.ToString("D2");
      return dateString + "-" + newTransactionString;

    }

    static public bool VerifyInput(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "20230626 AC001 W 100.00"

      if (inputSplitted.Length != 4) return false;

      if (inputSplitted[0].Length != 7) return false;
      
      string transactionType = inputSplitted[2];
      if (transactionType != "D" && transactionType != "W") return false;
      
      double amount = double.Parse(inputSplitted[3]);
      if (amount <= 0) return false;

      return true;
    }

    static public void CreateTransaction(string input)
    {
      // sample input: "20230626 AC001 W 100.00"
      string[] inputSplitted = input.Split(' ');
      string accountCode = inputSplitted[1];
      double amount = Convert.ToDouble(inputSplitted[3]);
      Transaction newTransaction = new Transaction(inputSplitted[0], accountCode, inputSplitted[2], amount);
      transactionsList.Add(newTransaction);
      Console.WriteLine("");
      Console.WriteLine("Account: " + accountCode);
      Console.WriteLine(" | Date    | Txn Id     | Type | Amount     |");
      transactionsList.Where(transaction => transaction.accountCode == accountCode).ToList().ForEach(transaction =>
      {
        Console.WriteLine($" | {transaction.dateString,7} | {transaction.id,10} | {transaction.transactionType,1}    | {transaction.amount, 10:C} |");
      });

      Console.WriteLine("Is there anything else you'd like to do?");
      Menu.MainMenu();
    }
  }
}