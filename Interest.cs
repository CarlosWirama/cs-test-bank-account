namespace TestProject
{
  class Interest(string dateString, string ruleId, double rate)
  {
    public string dateString = dateString;
    public string id = ruleId;
    public double rate = rate;

    public static List<Interest> InterestsList = [];

    static public bool VerifyInput(string input)
    {
      string[] inputSplitted = input.Split(' ');
      // sample input: "20230615 RULE03 2.20"
      
      if (inputSplitted.Length != 3) return false;
      
      if (inputSplitted[0].Length != 7) return false;
      
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
      InterestsList.Add(newInterest);
      Console.WriteLine("");
      Console.WriteLine("Interest rules:");
      Console.WriteLine(" | Date    | Rule Id     | Amount     |");
      InterestsList.ForEach(Interest =>
      {
        Console.WriteLine($" | {Interest.dateString,7} | {Interest.id,10} | {Interest.rate, 10:C} |");
      });

      Console.WriteLine("Is there anything else you'd like to do?");
      Menu.MainMenu();
    }
  }
}