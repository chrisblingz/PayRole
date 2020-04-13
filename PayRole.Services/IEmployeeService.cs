using PayRole.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayRole.Services
{
    public interface IEmployeeService
    {
        Task CreatAsync(Employee newEmployee);

        Employee GetById(int employeeId);

        Task UpdateAsync(Employee employee);

        Task UpdateAsync(int Id);

        Task Delete(int employeeId);

        decimal UnionFees(int Id);

        decimal StudentLoadRepaymentAmount(int id, decimal totalAmount);

        IEnumerable<Employee> GetAll();
    }
}
