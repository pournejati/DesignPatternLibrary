using System;

namespace ChainOfResponsibility
{
    public class ChainOfResponsibilityApp
    {
        public static void Main()
        {
            var larry = new Director();
            var sam = new VicePresident();
            var tammy = new President();

            larry.Successor = sam;
            sam.Successor = tammy;

            var purchase = new Purchase(2034, 350.00, "Assets");
            larry.ProcessRequest(purchase);

            purchase = new Purchase(2035, 32590.10, "Project X");
            larry.ProcessRequest(purchase);

            purchase = new Purchase(2036, 122100.00, "Project Y");
            larry.ProcessRequest(purchase);
        }
    }

    public interface IApprover
    {
        IApprover Successor { get; set; }
        void ProcessRequest(Purchase purchase);
    }

    public class Director : IApprover
    {
        public IApprover Successor { get; set; }

        public void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000.0)
                Console.WriteLine("{0} approved request# {1}", GetType().Name, purchase.Number);
            else if (Successor != null)
                Successor.ProcessRequest(purchase);
        }        
    }

    public class VicePresident : IApprover
    {
        public IApprover Successor { get; set; }

        public void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
                Console.WriteLine("{0} approved request# {1}", GetType().Name, purchase.Number);
            else if (Successor != null)
                Successor.ProcessRequest(purchase);
        }
    }

    public class President : IApprover
    {
        public IApprover Successor { get; set; }

        public void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
                Console.WriteLine("{0} approved request# {1}", GetType().Name, purchase.Number);
            else Console.WriteLine("Request# {0} requires an executive meeting!", purchase.Number);
        }
    }

    public class Purchase
    {
        public int Number { get; set; }
        public double Amount { get; set; }
        public string Purpose { get; set; }

        public Purchase(int number, double amount, string purpose)
        {
            Number = number;
            Amount = amount;
            Purpose = purpose;
        }        
    }
}