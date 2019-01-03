using System.Collections.Generic;

namespace Visitor
{
    public class VisitorMainApp
    {
        public static void Main()
        {
            var employees = new Employees();
            var employee1 = new Clerk
            {
                Name = "Hank",
                Income = 25000.0,
                VacationDays = 14
            };
            var employee2 = new Director
            {
                Name = "Elly",
                Income = 35000.0,
                VacationDays = 16
            };
            var employee3 = new President
            {
                Name = "Dick",
                Income = 45000.0,
                VacationDays = 21
            };

            employees.Hire(employee1);
            employees.Hire(employee2);
            employees.Hire(employee3);

            employees.AddPerk(new IncomePerk());
            employees.AddPerk(new VacationPerk());
        }
    }

    public interface IPerk
    {
        void Allocate(IEmployee employee);
    }

    public class IncomePerk : IPerk
    {
        public void Allocate(IEmployee employee) => employee.Income *= 1.10; // Provide 10% pay raise
    }

    public class VacationPerk : IPerk
    {
        public void Allocate(IEmployee employee) => employee.VacationDays += 3; // Provide 3 extra vacation days.
    }

    public interface IEmployee
    {
        string Name { get; set; }
        double Income { get; set; }
        int VacationDays { get; set; }
        void Accept(IPerk perk);
    }

    public class Employee : IEmployee
    {
        public string Name { get; set; }
        public double Income { get; set; }
        public int VacationDays { get; set; }

        public void Accept(IPerk perk) => perk.Allocate(this);
    }    

    public class Employees
    {
        private List<Employee> _Employees = new List<Employee>();

        public void Hire(Employee employee) => _Employees.Add(employee);
        public void Terminate(Employee employee) => _Employees.Remove(employee);

        public void AddPerk(IPerk perk)
        {
            foreach (var employee in _Employees)
                employee.Accept(perk);
        }
    }

    public class Clerk : Employee { }

    public class Director : Employee { }

    public class President : Employee { }
}