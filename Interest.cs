namespace TestProject
{
  class Interest(string dateString, string ruleId, double rate)
  {
    public string dateString = dateString;
    public string id = ruleId;
    public double rate = rate;

    public static List<Interest> interestsList = [];

    static public bool VerifyInput(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "20230615 RULE03 2.20"
      
      if (inputSplitted.Length != 3) return false;
      
      if (inputSplitted[0].Length != 8) return false;
      
      double rate = double.Parse(inputSplitted[2]);
      if (rate <= 0 || rate >= 100) return false;
      
      return true;
    }

    static public void CreateInterest(string input)
    {
      // sample input: "20230615 RULE03 2.20"
      string[] inputSplitted = input.Split(' ');
      double rate = double.Parse(inputSplitted[2]);
      Interest newInterest = new Interest(inputSplitted[0], inputSplitted[1], rate);
      interestsList.Add(newInterest);
      Console.WriteLine("");
      Console.WriteLine("Interest rules:");
      Console.WriteLine(" | Date    | Rule Id     | Amount     |");
      interestsList.ForEach(Interest =>
      {
        Console.WriteLine($" | {Interest.dateString,7} | {Interest.id,10} | {Interest.rate, 10:C} |");
      });
    }
  }
}