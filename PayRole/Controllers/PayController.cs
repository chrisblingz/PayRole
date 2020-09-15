using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayRole.Entity;
using PayRole.Models;
using PayRole.Services;

namespace PayRole.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayRoleService _payRoleService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaxService _taxService;
        private readonly INationalInsuranceContributionService _nationalInsuranceContributionService;
        private decimal overTimeHrs;
        private decimal contractualEarnungs;
        private decimal overTimeEarning;
        private decimal totalEarning;
        private decimal tax;
        private decimal unionFee;
        private decimal studentLoad;
        private decimal nationalInsurance;
        private decimal totalDeduction;

        public PayController(IPayRoleService payRoleService,
            IEmployeeService employeeService,
            ITaxService taxService,
            INationalInsuranceContributionService nationalInsuranceContributionService)
        {
            _payRoleService = payRoleService;
            _employeeService = employeeService;
            _taxService = taxService;
            _nationalInsuranceContributionService = nationalInsuranceContributionService;
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

        public IActionResult Create()
        {
            ViewBag.emplyees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payRoleService.GetAllTaxYear();
            var model = new PaymentRecordCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payRecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeService.GetById(model.EmployeeId).FullName,
                    Nino = _employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OvertimeHours = overTimeHrs = _payRoleService.OvertimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contractualEarnungs = _payRoleService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    Overtimearnings = overTimeEarning = _payRoleService.OvertimeEarnings(_payRoleService.OvertimeRate(model.HourlyRate), overTimeHrs),
                    TotalEarnings = totalEarning = _payRoleService.TotalEarnings(overTimeEarning, contractualEarnungs),
                    Tax = tax = _taxService.TaxAmount(totalEarning),
                    UnionFee = unionFee = _employeeService.UnionFees(model.EmployeeId),
                    SLC = studentLoad = _employeeService.StudentLoadRepaymentAmount(model.EmployeeId, totalEarning),
                    NIC = nationalInsurance = _nationalInsuranceContributionService.NIContribution(totalEarning),
                    TotalDeductions = totalDeduction = _payRoleService.TotalDeduction(tax, nationalInsurance, studentLoad, unionFee),
                    NetPayment = _payRoleService.NetPay(totalEarning, totalDeduction)
                };

                await _payRoleService.CreatAsync(payRecord);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.emplyees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payRoleService.GetAllTaxYear();
            return View();
        }
    }
}