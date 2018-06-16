using System;

namespace EventsSolution
{
    public delegate void BalanceEventHandler(decimal theValue);

    class PiggyBank
    {
        private decimal m_bankBalance;
        public event BalanceEventHandler BalanceChanged;

        public decimal TheBalance {
            set {
                m_bankBalance = value;
                BalanceChanged(value);
            }
            get {
                return m_bankBalance;
            }
        }
    }

    class BalanceLogger
    {
        public void BalanceLog(decimal amount) {
            Console.WriteLine("The balance amount is {0}", amount);
        }
    }

    class BalanceWatcher
    {
        public void BalanceWatch(decimal amount) {
            if (amount > 500.0m)
                Console.WriteLine("You reached your savings goal! You have {0}", amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PiggyBank pb = new PiggyBank();
            BalanceLogger bl = new BalanceLogger();
            BalanceWatcher bw = new BalanceWatcher();

            pb.BalanceChanged += bl.BalanceLog;
            pb.BalanceChanged += bw.BalanceWatch;

            string theStr;
            do {
                Console.WriteLine("How much to deposit?");

                theStr = Console.ReadLine();
                if (!theStr.Equals("exit")) {
                    decimal newVal = decimal.Parse(theStr);

                    pb.TheBalance += newVal;
                }
            } while (!theStr.Equals("exit"));
        }
    }
}
