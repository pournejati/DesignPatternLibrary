using System;
using System.Collections.Generic;

namespace Adapter
{
    public class AdapterMainApp
    {
        private static void Main()
        {
            var complexSystem = new ComplexSystem();
            IAcceptedContract employeeAdapter = new ComplexSystemAdapter(complexSystem);
            foreach (string item in employeeAdapter.GetList())
                Console.Write(item);
        }
    }

    public class ComplexSystem
    {
        public string[][] GetComplexItems()
        {
            string[][] employees = new string[4][];

            employees[0] = new string[] { "100", "Deepak", "Team Leader" };
            employees[1] = new string[] { "101", "Rohit", "Developer" };
            employees[2] = new string[] { "102", "Gautam", "Developer" };
            employees[3] = new string[] { "103", "Dev", "Tester" };

            return employees;
        }
    }

    public interface IAcceptedContract
    {
        List<string> GetList();
    }    

    public class ComplexSystemAdapter : IAcceptedContract
    {
        private ComplexSystem _ComplexSystem;

        public ComplexSystemAdapter(ComplexSystem complexSystem)
        {
            _ComplexSystem = complexSystem;
        }

        public List<string> GetList()
        {
            var employeeList = new List<string>();
            var employees = _ComplexSystem.GetComplexItems();
            foreach (string[] employee in employees)
            {
                employeeList.Add(employee[0]);
                employeeList.Add(employee[1]);
                employeeList.Add(employee[2]);
            }
            return employeeList;
        }
    }
}