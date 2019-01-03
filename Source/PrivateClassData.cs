using System;

namespace PrivateClassData
{
    public class PrivateClassDataApp
    {
        public static void Main()
        {
           
        }
    }

    public class EmployeeData
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public EmployeeData(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }        
    }

    public class Employee
    {
        private EmployeeData _EmployeeData;
        public Employee(EmployeeData employeeData)
        {
            _EmployeeData = employeeData;
        }
        public string SimpleId => _EmployeeData.Id.ToString().Replace("-", string.Empty);
        public string FullName => string.IsNullOrEmpty(_EmployeeData.FirstName) 
            ? _EmployeeData.LastName 
            : $"{_EmployeeData.FirstName} {_EmployeeData.LastName}";
    }
}