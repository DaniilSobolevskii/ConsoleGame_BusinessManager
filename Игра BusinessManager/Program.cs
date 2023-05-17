using System;
using System.Text;
using Timer = System.Timers.Timer;


public class Programm
{
    public static void Main(string[] args)
    {
      
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var IsRunning = true;
      
        

        Console.WriteLine("Введите имя игрока:");
        Player player = new Player(Console.ReadLine());
        List<Business> businesses = new List<Business>();
        AddBusinessToGame();
        StartMenu();
        
        void StartMenu()
        {
            Timer timer = new Timer();
            timer.Interval = 20000;
            timer.Elapsed += (sender, e) => GetEarnings();
            timer.Start();
            while (IsRunning)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine($"{player.Name} имеет {player.wallet.Money}$.");
                Console.WriteLine();
                Console.WriteLine($"{player.Name} имеет следующие бизнесы:");
                GetBusinessName(player.Businesses);
                
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Выберите бизнес для улучшения\n" +
                                  "2 - Купить новый бизнес\n" +
                                  "3 - Выйти из игры");
                Console.WriteLine();
                var userChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                ProcessSelectedMenuItem((GameMenuAsk)userChoice);
            }

            void ProcessSelectedMenuItem(GameMenuAsk userChoice)
            {
                switch (userChoice)
                {
                    case GameMenuAsk.ChoiseBusiness:
                        if (player.Businesses.Count != 0)
                        {
                            ImproveBusiness();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("У вас нет бизнеса");
                            break;
                        }
                        
                    case GameMenuAsk.BuyNewBusiness:
                        
                            BuyBusiness();
                            break;
                      
                    case GameMenuAsk.EndProgram:
                        IsRunning = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверный запрос");
                        break;
                }
            }
        }

        void AddBusinessToGame()
        {
            var GasStation = new Business("Gazstation", 5000, 500, new List<Improvement>());
            GasStation.AddPosibleImprovement("New column", 1500, 350);
            GasStation.AddPosibleImprovement("Cafe", 1000, 250);

            var CarWash = new Business("Carwash", 1000, 200, new List<Improvement>());
            CarWash.AddPosibleImprovement("New equipment", 400, 50);

            var Pizza = new Business("Pizza", 3000, 400, new List<Improvement>());
            Pizza.AddPosibleImprovement("Delivery car", 1000, 450);
            
            var Taxi = new Business("Taxi", 1000, 100, new List<Improvement>());
            Taxi.AddPosibleImprovement("Car", 1000, 100);
            
            businesses.Add(Taxi);
            businesses.Add(Pizza);
            businesses.Add(GasStation);
            businesses.Add(CarWash);
        }
        
        void BuyBusiness()
        {
            GetInformationAboutBusiness(businesses);
            Console.WriteLine("Введите название бизнеса для покупки или 0 если передумали");
            Console.WriteLine();
            var name = Console.ReadLine();
            Console.WriteLine();
            if (name == "0")
            {
                return;
            }
            else
            {
                Business businessToBuy = businesses.Find(business => business.GetName().ToLower() == name.ToLower());
                if (businessToBuy==null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Бизнес не найден");
                    return;
                }
                
                if (player.wallet.Buy(businessToBuy.Cost()))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Бизнес удачно куплен");
                    player.Businesses.Add(businessToBuy);
                    businesses.Remove(businessToBuy);
                    Console.WriteLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не достаточно денег");
                    
                }
            }
        }

        void ImproveBusiness()
        {
            Console.WriteLine($"{player.Name} имеет следующие бизнесы:");
            GetBusinessName(player.Businesses);
            
            Console.WriteLine("Введите название бизнеса для улучшения или 0 если передумали");
            var name = Console.ReadLine();
            if (name == "0")
            {
                return;
            }
            else
            { 
                Business businessToImprove = player.Businesses.Find(business => business.GetName().ToLower() == name.ToLower());
                if (businessToImprove == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Бизнес не найден");
                    return;
                }
                else
                {
                    businessToImprove.GetImprovements();
                    Console.WriteLine("Введите название улучшения");
                    string improv_name = Console.ReadLine();
                    var improvement = businessToImprove.SearchImprovementByName(improv_name.ToLower());
                    if (improvement != null && player.wallet.Buy(improvement.Cost()))
                    { 
                       businessToImprove.AddActiveImprovement(improvement);
                    }
                    if(improvement==null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Улучшение не найдено.");
                    }
                    
                }
                
            }
        }

        void GetBusinessName( List<Business>business)
        {
            foreach (var buisnes in business)
            {
                Console.WriteLine(buisnes.GetName()+" приносит: "+ buisnes.Profit()+"$") ;
            }
        }
        void GetInformationAboutBusiness( List<Business>business)
        {
            foreach (var buisnes in business)
            {
                buisnes.GetInformation();
            }
        }
       

        void GetEarnings()
        {
            if (player.Businesses.Count != 0)
            {
                var profit = 0;
            

            foreach (var busines in player.Businesses)
            {
                profit += busines.Profit();
            }

            player.wallet.CalculateEarnings(profit);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("У вас прибыль. На вашем счету:" + player.wallet.Money);
            Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}