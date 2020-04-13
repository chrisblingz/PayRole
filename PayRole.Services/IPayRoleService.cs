using Microsoft.AspNetCore.Mvc.Rendering;
using PayRole.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayRole.Services
{
    public interface IPayRoleService
    {
        Task CreatAsync(PaymentRecord paymentRecord);
        PaymentRecord GetById(int id);
        IEnumerable<PaymentRecord> GetAll();
        IEnumerable<SelectListItem> GetAllTaxYear();
        decimal OvertimeHours(decimal hoursWorked, decimal contratualHours);
        decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate);
        decimal OvertimeRate(decimal hourlyRate);
        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);
        decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings);
        decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal UnionFees);
        decimal NetPay(decimal totalEarnings, decimal totalDeduction);
    }
}
