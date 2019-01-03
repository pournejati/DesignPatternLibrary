using System;
using System.Collections.Generic;

namespace Rule
{
    public class Customer
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public bool HasBeenLoyalForYears(int yearsAsCustomer, DateTime date) => true;
        public bool IsBirthday() => BirthDay.Date == DateTime.Now.Date;
    }

    public interface IDiscountRule
    {
        decimal CalculateCustomerDiscount(Customer customer);
    }

    public class BirthdayDiscountRule : IDiscountRule
    {
        public decimal CalculateCustomerDiscount(Customer customer)
        {
            return customer.IsBirthday() ? 0.10m : 0;
        }
    }

    public class LoyalCustomerRule : IDiscountRule
    {
        private readonly int _yearsAsCustomer;
        private readonly decimal _discount;
        private readonly DateTime _date;

        public LoyalCustomerRule(int yearsAsCustomer, decimal discount, DateTime? date = null)
        {
            _yearsAsCustomer = yearsAsCustomer;
            _discount = discount;
            _date = date ?? DateTime.Now;
        }

        public decimal CalculateCustomerDiscount(Customer customer)
        {
            if (customer.HasBeenLoyalForYears(_yearsAsCustomer, _date))
            {
                var birthdayRule = new BirthdayDiscountRule();
                return _discount + birthdayRule.CalculateCustomerDiscount(customer);
            }
            return 0;
        }
    }

    public class RulesDiscountCalculator
    {
        List<IDiscountRule> _rules = new List<IDiscountRule>();

        public RulesDiscountCalculator()
        {
            _rules.Add(new BirthdayDiscountRule());
            _rules.Add(new LoyalCustomerRule(1, 0.10m));
            _rules.Add(new LoyalCustomerRule(5, 0.12m));
            _rules.Add(new LoyalCustomerRule(10, 0.20m));
        }

        public decimal CalculateDiscountPercentage(Customer customer)
        {
            decimal discount = 0;
            foreach (var rule in _rules)
                discount = Math.Max(rule.CalculateCustomerDiscount(customer), discount);
            return discount;
        }
    }
}