public class Player
{
    public PlayerWallet wallet = new PlayerWallet();
    public List<Business> Businesses= new List<Business>();
    private string _name;
    public string Name
    {
        get => _name; 
        private set => _name = value;
    }
    public Player(string name)
    {
        Name = name;
    }
    

    
}