using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Entity;
using PayCompute.Services;
using PayCompute2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmployeeController(IEmployeeService employeeService, IHostingEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                ImageUrl = employee.ImageUrl,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                City = employee.City,
                DateJoined = employee.DateJoined


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
        [ValidateAntiForgeryToken] //Prevents  cross-site  Request Forgery attacks
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = model.Id,
                    FullName = model.FullName,
                    LastName = model.LastName,
                    DateJoined = model.DateJoined,
                    MiddleName = model.MiddleName,
                    Address = model.Address,
                    StudentLoan = model.StudentLoan,
                    Email = model.Email,
                    Designation = model.Designation,
                    EmployeeNo = model.EmployeeNo,
                    DOB = model.DOB,
                    City = model.City,
                    FirstName = model.FirstName,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    Postcode = model.Postcode,
                    UnionMember = model.UnionMember
                    

                };
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRoothPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yyyymmssff") + fileName + extension;
                    var path = Path.Combine(webRoothPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
                 
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();

            }
            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                
                LastName = employee.LastName,
                DateJoined = employee.DateJoined,
                MiddleName = employee.MiddleName,
                Address = employee.Address,
                StudentLoan = employee.StudentLoan,
                Email = employee.Email,
                Designation = employee.Designation,
                EmployeeNo = employee.EmployeeNo,
                DOB = employee.DOB,
                City = employee.City,
                FirstName = employee.FirstName,
                Gender =employee.Gender,
                PhoneNumber = employee.PhoneNumber,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                Postcode = employee.Postcode,
                UnionMember = employee.UnionMember


            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null)
                {
                    return NotFound();

                }
                employee.FullName = model.FirstName;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.PaymentMethod = model.PaymentMethod;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Postcode = model.Postcode;
                employee.UnionMember = model.UnionMember;
                employee.Email = model.Email;
                employee.EmployeeNo = model.EmployeeNo;
                employee.Gender = model.Gender;
                employee.DOB = model.DOB;
                employee.Designation = model.Designation;
                employee.Address = model.Address;
                employee.Address = model.City;
                employee.UnionMember = model.UnionMember;
                employee.DateJoined = model.DateJoined;

                if(model.ImageUrl !=null  && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRoothPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yyyymmssff") + fileName + extension;
                    var path = Path.Combine(webRoothPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;

                }


               await  _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));


            }

            return View();
        }

       
        
     
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            EmployeeDetailViewModel model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmployeeNo = employee.EmployeeNo,
                Email = employee.Email,
                Gender = employee.Gender,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                UnionMember = employee.UnionMember,
                StudentLoan = employee.StudentLoan,
                Address = employee.Address,
                City = employee.City,
                Designation = employee.Designation,
                PhoneNumber = employee.PhoneNumber,
                ImageUrl = employee.ImageUrl,
                Postcode = employee.Postcode

            };


            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id)
;
            if (employee ==null)
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
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            await _employeeService.Delete(model.Id);
            

            return RedirectToAction(nameof(Index));
        }
    }
}
