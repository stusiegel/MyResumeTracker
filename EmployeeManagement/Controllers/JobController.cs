using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    //[Route("Job")]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;

        //injecting employeeRepostory into private field
        public JobController( IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        //[Route("")]
        //[Route("~/")]
        //[Route("Index")]
        public ViewResult Index()
        {
            var model = _jobRepository.GetAllJobs();
            return View(model);
        }

        [Route("JobDetails/{id?}")]
        public ViewResult JobDetails(int? id)
        {

            JobDetailsViewModel jobDetailsViewModel = new JobDetailsViewModel()
            {
                Job = _jobRepository.GetJob(id??2),
                PageTitle = "Job Details"
            };

            return View(jobDetailsViewModel);

        }

        [HttpGet]
        //[Route("Create")]
        //[HttpGet("[controller]/[action]")]
        //[Route("Create")]
        public ViewResult Create()
        {
            return View();
        }

       

        [HttpPost]
        public IActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            { 
            //add the employee
            Job newJob = _jobRepository.Add(job);
            return RedirectToAction("jobdetails","job", new { id = newJob.Id });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(JobEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Job job = _jobRepository.GetJob(model.Id);
                job.CompanyName = model.CompanyName;
                job.Description = model.Description;
                job.Title = model.Title;
                job.Rating = model.Rating;

                _jobRepository.Update(job);
                //add the employee
                //Job newJob = _jobRepository.Update(job);
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Job job = _jobRepository.GetJob(id);  
            JobEditViewModel jobEditViewModel = new JobEditViewModel
            
            {
                Id = job.Id,  
                Description = job.Description, 
                CompanyName = job.CompanyName,
                Comments = job.Comments, 
                Rating = job.Rating,
                Title = job.Title
            };
            return View(jobEditViewModel);
        }


    }
}
