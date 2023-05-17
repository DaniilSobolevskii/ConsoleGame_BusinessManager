public class Improvement:IBuyble, IProfitable
{
    private string _name;
    private int _cost;
    private int _profit;

    public Improvement(string name, int cost, int profit)
    {
        _name = name;
        _cost = cost;
        _profit = profit;
    }

    public int Profit()
    {
        return _profit;
    }

    public string GetName()
    {
        return _name;
    }
    public void GetInformation()
    {
        Console.WriteLine($"{_name} - cтоимость улчучшения {_cost}, увеличивает прибыль на {_profit}.");
    }

    public int Cost()
    {
        return _cost;
    }
}