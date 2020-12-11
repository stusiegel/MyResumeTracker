﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
     
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
       
        //injecting employeeRepostory into private field

        public HomeController(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        //[Route("")]
        //[Route("~/")]
        //[Route("Index")]
        public ViewResult Index()
        {
            
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }
         
        public ViewResult Details(int? id)
        {
            
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };
           
          
            return View(homeDetailsViewModel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        //public RedirectToActionResult Create(Employee employee) //using ModelState we must change from RedirectionActionResult to IActionResult
        public IActionResult Create(Employee employee)
        {
            //Check to see if onscreen validation has passed.
            if (ModelState.IsValid)
            { 
            //add the employee
            Employee newEmployee = _employeeRepository.Add(employee);
            return RedirectToAction("details", new { id = newEmployee.Id });
         
            }
            
            return View();
        }
    }
}