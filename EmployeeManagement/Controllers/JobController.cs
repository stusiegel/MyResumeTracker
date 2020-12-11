using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
   [Route("Job")]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;

        //injecting employeeRepostory into private field
        public JobController( IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
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


        [Route("")]
        [Route("~/")]
        [Route("Index")]
        public ViewResult Index()
        {
            var model = _jobRepository.GetAllJobs();
            return View(model);
        }
    }
}
