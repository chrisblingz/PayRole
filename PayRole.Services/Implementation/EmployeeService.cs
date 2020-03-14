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


        public decimal StudentLoadRepaymentAmoun5(int id, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

        public decimal UnionFees(int Id)
        {
            throw new NotImplementedException();
        }


    }
}
