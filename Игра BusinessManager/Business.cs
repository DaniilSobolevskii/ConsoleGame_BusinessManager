public class Business : IBuyble, IProfitable
{ 
    private string _name;
    private int _cost;
    private int _profit;
    private List<Improvement> ImprovementsCollection;
   
    public int Cost()
    {
        return _cost;
    }

    public Business( string name, int cost, int profit, List<Improvement> improvementsCollection)
    {
        _name = name;
        _cost = cost;
        _profit = profit;
        ImprovementsCollection = improvementsCollection;
    }

    public string GetName()
    {
        return _name;
    }

    public void AddPosibleImprovement(string name, int cost, int profit)
    {
        var x = new Improvement(name, cost, profit);
        ImprovementsCollection.Add(x);
    }

    public void AddActiveImprovement(Improvement improvement)
    {
        
        ImprovementsCollection.Remove(improvement); 
        _profit += improvement.Profit();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Улучшениe куплено");
        Console.WriteLine();
       
            
    }

    public Improvement SearchImprovementByName(string name)
    {
        Improvement improvement = ImprovementsCollection.Find(improv => improv.GetName().ToLower() == name);
        return improvement;
    }
    public void GetImprovements()
    {
        if (ImprovementsCollection.Count==0)
        {
            Console.WriteLine("Улучшений больше нет.");
        }
        foreach (var improvment in ImprovementsCollection)
        {
            improvment.GetInformation();
        }
            
    }

    public void GetInformation()
    {
        Console.WriteLine($"{_name} - приносит: {_profit}$, стоит {_cost}$");
    }

    public int Profit()
    {
        return _profit;
    }
}