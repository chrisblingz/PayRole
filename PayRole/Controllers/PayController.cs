using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayRole.Models;
using PayRole.Services;

namespace PayRole.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayRoleService _payRoleService;

        public PayController(IPayRoleService payRoleService)
        {
            _payRoleService = payRoleService;
        }
        public IActionResult Index()
        {
            var payRecords = _payRoleService.GetAll().Select(pay => new PaymentRecordIndexViewModel
            {
                Id = pay.Id,
                EmployeeId = pay.EmployeeId,
                FullName = pay.FullName,
                PayDate=pay.PayDate,
                PayMonth =pay.PayMonth,
                TaxYearId =pay.TaxYearId,
                Year = _payRoleService.GetTaxYearById(pay.TaxYearId).YearOfTax,
                TotalDeduction = pay.TotalDeductions,
                TotalEarnings =pay.TotalEarnings,
                NetPayment =pay.NetPayment,
                Employee = pay.Employee
            });
            return View(payRecords);
        }
    }
}