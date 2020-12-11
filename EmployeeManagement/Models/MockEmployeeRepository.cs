using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        private List<Employee> _employeeList;

        //Call the Constructor of the class
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
       
            {
                new Employee() { Id = 1, Name = "Mary", Department = DeptEnum.HR, Email = "mary@yahoo.com" },
                new Employee() { Id = 2, Name = "Tony", Department = DeptEnum.IT, Email = "Tony@yahoo.com" },
                new Employee() { Id = 3, Name = "Steven", Department = DeptEnum.Payroll, Email = "Steven@yahoo.com" }
            };

        }


        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee Add(Employee employee)
        {
            //Need to add 1 value to the max value of current list.
           employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;

        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;

            }

            return employee;
        }

        public Employee Delete(int id)
        {
          Employee employee =  _employeeList.FirstOrDefault(e => e.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }

            return employee;
        }
    }
}
