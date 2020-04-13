using PayRole.Entity;
using PayRole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRole.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private decimal studentLoanAmout;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreatAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }
        public Employee GetById(int employeeId)
        {
            return _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
        }

        public async Task Delete(int employeeId)
        {
            var employee = GetById(employeeId);
            _context.Remove(employee);
           await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int Id)
        {
            var employee = GetById(Id);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }


        public decimal StudentLoadRepaymentAmount(int id, decimal totalAmount)
        {
            var employee = GetById(id);
            if(employee.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanAmout = 15m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount > 2000 && totalAmount < 2250)
            {
                studentLoanAmout = 38m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                studentLoanAmout = 60m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
            {
                studentLoanAmout = 83m;
            }
            else
            {
                studentLoanAmout = 0.0m;
            }
            return studentLoanAmout;

        }

        public decimal UnionFees(int Id)
        {
            throw new NotImplementedException();
        }


    }
}
