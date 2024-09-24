namespace ATM
{
     class Program
    {
        static void Main(string[] args)
        {
            Atms atms = new Atms(1000);
            List<UserBank> userBank = new List<UserBank>()
            {
                new UserBank {Name = "Artem"},
                new UserBank {Name = "Grigoriy"},
                new UserBank {Name = "Stepa"}

            };

            Thread[] threads = new Thread[]
                {
                new Thread(() =>  atms.Withdraw(400, userBank[0].Name)) { Name = "ATM1"  },
                new Thread(() => atms.Withdraw(500, userBank[1].Name)){ Name = "ATM2" },
                new Thread(() => atms.Withdraw(500, userBank[2].Name)) { Name = "ATM3" }
                };


            foreach (Thread t in threads)
            {
                t.Start();
                t.Join();
            }

            atms.DisplayBalance();
        }
    }
    public class Atms
    {
        private decimal _money;
        private object locker = new object();
        public UserBank UserBank { get; set; }
        public Atms(decimal initialAmount) 
        {
            _money = initialAmount;
        }

        public void Withdraw(decimal amount, string userName)
        {
            lock(locker);
            
                if (_money > amount)
                {
                    Console.WriteLine($"{userName} пытаеться вывести {amount} грн {Thread.CurrentThread.Name}");
                    _money -= amount;
                    Console.WriteLine($"{userName}, Withdraw succssesful {amount} грн, остаток {_money} грн");
                }
                else
                {
                    //throw new Exception($"{userName} Попытлася вывести {amount}, отказ, баланс Банкомата: {_money}");
                    Console.WriteLine($"{userName} Попытлася вывести {amount}, отказ, баланс Банкомата: {_money}");
                }
            
            
            

        }
        public void DisplayBalance()
        {
            Console.WriteLine($"Balance: {_money}");
        }


    }
    public class UserBank
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
    }
}
