using System;
using System.Collections.Generic;

namespace Observer
{
    public class ObserverMainApp
    {
        public static void Main()
        {
            var microsoftStock = new MicrosoftStock();
            microsoftStock.Symbol = "MS";

            var investor1 = new Investor();
            investor1.Name = "Bill Gates";
            var investor2 = new Investor();
            investor2.Name = "Berkshire";

            microsoftStock.Attach(investor1);
            microsoftStock.Attach(investor2);

            microsoftStock.Price = 120.10;
            microsoftStock.Price = 121.00;
            microsoftStock.Price = 120.50;
            microsoftStock.Price = 120.75;
        }
    }

    public interface IStock
    {
        List<IInvestor> Investors { get; }
        string Symbol { get; set; }
        double Price { get; set; }

        void Attach(IInvestor investor);
        void Detach(IInvestor investor);
        void Notify();        
    }       

    public class MicrosoftStock : IStock
    {
        public List<IInvestor> Investors { get; private set; } = new List<IInvestor>();
        public string Symbol { get; set; }

        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        public void Attach(IInvestor investor) => Investors.Add(investor);
        public void Detach(IInvestor investor) => Investors.Remove(investor);
        public void Notify()
        {
            foreach (IInvestor investor in Investors)
                investor.Update(this);
        }
    }

    public interface IInvestor
    {
        string Name { get; set; }
        void Update(IStock stock);
    }

    public class Investor : IInvestor
    {
        public string Name { get; set; }
        public void Update(IStock stock) => Console.WriteLine("Notified {0} of {1}'s change to {2:C}", Name, stock.Symbol, stock.Price);        
    }
}