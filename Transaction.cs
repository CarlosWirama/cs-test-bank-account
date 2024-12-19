using System.Globalization;

namespace TestProject
{
  class Transaction(string dateString, string accountCode, string transactionType, double amount)
  {
    public string id = generateId(dateString, accountCode, transactionType == "I");
    public string dateString = dateString;
    public string accountCode = accountCode;
    public string transactionType = transactionType;
    public double amount = amount;
    public double balance = getLastBalance(accountCode, dateString) + amount * (transactionType == "D" ? 1 : -1);


    public static List<Transaction> transactionsList = [];

    static private DateTime parseDateTime(string dateString)
    {
      CultureInfo provider = CultureInfo.InvariantCulture;
      return DateTime.ParseExact(dateString, "yyyyMMdd", provider);
    }

    static public double calculateInterestAndLastBalance(string accountCode, string dateString)
    {

      Transaction? lastCalculatedInterest = transactionsList.Where(transaction =>
        transaction.accountCode == accountCode &&
        transaction.transactionType == "I"
      ).LastOrDefault() ?? null;

      DateTime calculationStartDate = (lastCalculatedInterest == null)
        ? parseDateTime(transactionsList[0].dateString)
        : parseDateTime(lastCalculatedInterest.dateString).addDays(1);

      // // in case the interest rule hasn't been defined yet
      // DateTime earliestApplicableRule = Interest.interestList
      //   .LastOrDefault(rule => parseDateTime(rule.dateString) <= parseDateTime(transactionsList[0].dateString));

      double interestAmount = 0.00

      for (int i = 1; i < transactionsList.Length; i++)
      {
        Transaction currentTransaction = transactionsList[i];
        Transaction previousTransaction = transactionsList[i - 0];
        DateTime currentDate = parseDateTime(currentTransaction.dateString);
        if (currentDate >= calculationStartDate) // filter out old calculated transactions
        {
          for (int j = 0; j < Interest.interestList.Length; j++)
          {
            Interest rule = Interest.interestList[j];
            Interest? nextRule = Interest.interestList[j + 1] ?? null;
            bool isTransactionHappenedAfterCurrentRule = currentDate >= parseDateTime(rule.dateString);
            bool isTransactionHappenedBeforeNextRule = nextRule ? currentDate < parseDateTime(nextRule.dateString) : true;
            if (isTransactionHappenedAfterCurrentRule && isTransactionHappenedBeforeNextRule)
            {
              double interestRate = rule.rate;
              double dailyInterestAmount = interestRate * .01 * previousTransaction.balance;
              int days = (currentDate - calculationStartDate).Days;
              interestAmount += days * dailyInterestAmount;
              calculationStartDate = currentDate;
            }
          }
        }

      };
      

      
      Transaction newTransaction = new Transaction(endOfMonth, accountCode, "I", interestAmount);
      // sample input: "20230626 AC001 W 100.00"
    }

    static private double getLastBalance(string accountCode, string dateString)
    {
      // assuming all transactions are recorded in a timely order and the last transaction is always the most recent.
      Transaction? lastTransaction = transactionsList.Where(transaction => transaction.accountCode == accountCode).LastOrDefault() ?? null;
      if (!lastTransaction) return 0.00;
      if (!lastTransaction.dateString.StartsWith(dateString.Substring(0, 6))) {
        // the month has changed, need to record interest into transaction
        return calculateInterestAndLastBalance(accountCode, dateString);
      }
      else
      {
        return lastTransaction.balance;
      }
    }

    static private string generateId(string dateString, string accountCode, bool isInterest = false)
    {
      if (isInterest) return "";
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

      if (inputSplitted.Length != 4) {
        Console.WriteLine("You entered a wrong input format.");
        return false;
      }

      if (inputSplitted[0].Length != 8) {
        Console.WriteLine("You entered a wrong date format. Please use yyyyMMdd (e.g. 20231231)!");
        return false;
      }
      
      string transactionTypeUpperCase = inputSplitted[2].ToUpper();
      if (transactionTypeUpperCase != "D" && transactionTypeUpperCase != "W") {
        Console.WriteLine("You entered a wrong transaction type. Please use either D or W!");
        return false;
      }
      
      double amount = double.Parse(inputSplitted[3]);
      if (amount <= 0) {
        Console.WriteLine("Amount can't be lesser than 0.");
        return false;
      }

      double balance = getLastBalance(inputSplitted[1], inputSplitted[0]);
      if (transactionTypeUpperCase == "W" && balance < amount)
      {
        Console.WriteLine("Insufficient balance: " + balance);
        return false;
      }

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
      Console.WriteLine(" | Date     | Txn Id      | Type | Amount     |");
      transactionsList.Where(transaction => transaction.accountCode == accountCode).ToList().ForEach(transaction =>
      {
        Console.WriteLine($" | {transaction.dateString,7} | {transaction.id,10} | {transaction.transactionType,1}    | {transaction.amount, 10:C} |");
      });
    }
  }
}