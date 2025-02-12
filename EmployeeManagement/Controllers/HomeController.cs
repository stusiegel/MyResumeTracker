﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
     
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger logger;

        //injecting employeeRepostory into private field

        public HomeController(IEmployeeRepository employeeRepository,
                               IWebHostEnvironment webHostEnvironment,
                               ILogger<HomeController> logger) 
        {
           
            _employeeRepository = employeeRepository;
            this.employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        //[Route("")]
        //[Route("~/")]
        //[Route("Index")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //logger.LogTrace("Trace Log");
            //logger.LogDebug("Debug Log");
            //logger.LogInformation("Information Log");
            //logger.LogWarning("Warning Log");
            //logger.LogError("Error Log");
            //logger.LogCritical("Critical Log");
            //throw new Exception("Error in Details View");

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNot Found ", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //refactor
                //Employee = _employeeRepository.GetEmployee(id??1),
                Employee = employee,
                PageTitle = "Employee Details"
            };
           
          
            return View(homeDetailsViewModel);

        }

        [Authorize]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [Authorize]
        [HttpPost]
        //public RedirectToActionResult Create(Employee employee) //using ModelState we must change from RedirectionActionResult to IActionResult
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            //Check to see if onscreen validation has passed.
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        string uploadsFolder = Path.Combine(webRootPath, "images");
                        string filePath = Path.Combine(uploadsFolder, model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);

                    }
                    //string uniqueFileName = ProcessUploadedFile(model);
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
               

                _employeeRepository.Update(employee);
                return RedirectToAction("index");

            }

            return View();
        }

        [Authorize]
        private static string ProcessUploadedFile(EmployeeEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {

                foreach (IFormFile photo in model.Photos)
                {

                    string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string uploadsFolder = Path.Combine(webRootPath, "images");


                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //Video 56 has logic how to use fileStream to delete existing photo and add new one in it's place at 15:39
                    //using (var fileStream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    model.Photo.CopyTo(fileStream);
                    //}
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

            }

            return uniqueFileName;
        }

        [Authorize]
        [HttpPost]
        //public RedirectToActionResult Create(Employee employee) //using ModelState we must change from RedirectionActionResult to IActionResult
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            //Check to see if onscreen validation has passed.
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {

                    foreach (IFormFile photo in model.Photos)
                    {

                        string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        string uploadsFolder = Path.Combine(webRootPath, "images");


                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                }
                //add the employee
                // origial before file upload added Employee newEmployee = _employeeRepository.Add(employee);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };


                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });

            }

            return View();
        }


    }
}
