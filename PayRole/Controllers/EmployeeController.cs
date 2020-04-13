using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using PayRole.Entity;
using PayRole.Models;
using PayRole.Services;

namespace PayRole.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMemeber = model.UnionMemeber,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    PostCode = model.PostCode,
                    Designation = model.Designation

                };

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.CreatAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var employee = _employeeService.GetById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditViewModel()
            {

                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMemeber = employee.UnionMemeber,
                Address = employee.Address,
                City = employee.City,
                Phone = employee.Phone,
                PostCode = employee.PostCode,
                Designation = employee.Designation
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null)
                {
                    return NotFound();
                }

                employee.Id = model.Id;
                employee.FirstName = model.FirstName;
                employee.EmployeeNo = model.EmployeeNo;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DOB = model.DOB;
                employee.DateJoined = model.DateJoined;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.PaymentMethod = model.PaymentMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMemeber = model.UnionMemeber;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.Phone = model.Phone;
                employee.PostCode = model.PostCode;
                employee.Designation = model.Designation;

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            var empolyee = _employeeService.GetById(Id);
            if(empolyee == null)
            {
                return NotFound() ;
            }
            var model = new EmployeeDetailViewModel()
            {
                Id = empolyee.Id,
                EmployeeNo = empolyee.EmployeeNo,
                FullName = empolyee.FullName,
                Gender = empolyee.Gender,
                Email = empolyee.Email,
                DOB = empolyee.DOB,
                DateJoined = empolyee.DateJoined,
                NationalInsuranceNo = empolyee.NationalInsuranceNo,
                PaymentMethod = empolyee.PaymentMethod,
                StudentLoan = empolyee.StudentLoan,
                UnionMemeber = empolyee.UnionMemeber,
                Address = empolyee.Address,
                City = empolyee.City,
                Phone = empolyee.Phone,
                PostCode = empolyee.PostCode,
                Designation = empolyee.Designation,
                ImageUrl = empolyee.ImageUrl
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var employee = _employeeService.GetById(Id);
            if(employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName
            }; 

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(EmployeeDeleteViewModel model)
        {
           await _employeeService.Delete(model.Id);

            return RedirectToAction(nameof(Index));
        } 

    }
}
