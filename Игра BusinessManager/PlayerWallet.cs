public class PlayerWallet
{
    private int _money = 5000;

    public int Money
    {
        get => _money;
        private set => _money = value;
    }

    public bool Buy(int x)
    {
        if (x <= _money)
        {
            _money -= x;
            return true;
        }

        return false;
    }

    public void CalculateEarnings(int earnings)
    {
        _money += earnings;
    }

}