using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayRole.Models;
using PayRole.Services;

namespace PayRole.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetAll()
                .Select(e => new EmployeeIndexViewModel()
                {
                    Id = e.Id,
                    EmployeeNo = e.EmployeeNo,
                    ImageUrl = e.ImageUrl,
                    FullName = e.FullName,
                    Gender = e.Gender,
                    Designation = e.Designation,
                    City = e.City,
                    DateJoined = e.DateJoined
                }).ToList();

            return View(employees);
        }
    }
}