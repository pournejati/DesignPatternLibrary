namespace Facade
{
    public class FacadeMainApp
    {
        private static void Main()
        {
            var bank = new Bank();
            var loan = new Loan();
            var credit = new Credit();
            MortgageFacade mortgage = new MortgageFacade(bank, loan, credit);

            bool eligible = mortgage.IsEligible("Customer Name", 125000);
        }
    }
    public class Bank
    {
        public bool HasSufficientSavings(string customer, int amount) => true;
    }

    public class Credit
    {
        public bool HasGoodCredit(string customer) => true;
    }

    public class Loan
    {
        public bool HasNoBadLoans(string customer) => true;
    }

    public class MortgageFacade
    {
        private Bank _bank;
        private Loan _loan;
        private Credit _credit;

        public MortgageFacade(Bank bank, Loan loan, Credit credit)
        {
            _bank = bank;
            _loan = loan;
            _credit = credit;
        }

        public bool IsEligible(string customer, int amount)
        {
            bool eligible = true;

            if (!_bank.HasSufficientSavings(customer, amount))
                eligible = false;
            else if (!_loan.HasNoBadLoans(customer))
                eligible = false;
            else if (!_credit.HasGoodCredit(customer))
                eligible = false;

            return eligible;
        }
    }
}