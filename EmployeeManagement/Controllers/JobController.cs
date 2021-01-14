using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    //[Route("Job")]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<JobController> logger;

        //injecting employeeRepostory into private field
        public JobController( IJobRepository jobRepository, ILogger<JobController> logger)
        {
            _jobRepository = jobRepository;
            this.logger = logger;
        }

        //[Route("")]
        //[Route("~/")]
        //[Route("Index")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _jobRepository.GetAllJobs();
            return View(model);
        }

        [Route("JobDetails/{id?}")]
        [AllowAnonymous]
        public ViewResult JobDetails(int id)
        {
            Job job = _jobRepository.GetJob(id);
            if (job == null)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", id);
            }

            JobDetailsViewModel jobDetailsViewModel = new JobDetailsViewModel()
            {
                //Job = _jobRepository.GetJob(id??2),
                Job = _jobRepository.GetJob(id),
                PageTitle = "Job Details"
            };

            return View(jobDetailsViewModel);

        }

        [HttpGet]
        [Authorize]
        //[Route("Create")]
        //[HttpGet("[controller]/[action]")]
        //[Route("Create")]
        public ViewResult Create()
        {
            return View();
        }

       

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
