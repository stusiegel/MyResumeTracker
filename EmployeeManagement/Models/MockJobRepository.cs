using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockJobRepository : IJobRepository
    {


        private List<Job> _jobList;

        //Call the Constructor of the class
        public MockJobRepository()
        {
            _jobList = new List<Job>()

            {
                new Job() { Id = 1, Title = "Sr Software Developer", Description = "Master of all", Comments = "Awesome Opportunity", CompanyName = "CVS", Rating=JobRatingEnum.One, Salary = 104000M},
                new Job() { Id = 2,Title = "Sr Engineer", Description = "Use Java", Comments = "Java's a job ", CompanyName = "USAA",  Rating=JobRatingEnum.Three, Salary = 90000M},
                new Job() { Id = 3, Title = "Golf Pro", Description = "Leaving the dream", Comments = "Who doesn't love golf", CompanyName = "PGA", Rating=JobRatingEnum.Five, Salary = 50000M },
                 //new Job() { Id = 4, Title = "Golf Club Sales", Description = "Sales job", Comments = "Work at Dicks", CompanyName = "Dick's sporting goods", Salary = 4000M, SubmittedApplication = true },
                 // new Job() { Id = 5, Title = "Golf Club Fitter", Description = "Help fit people for clubs", Comments = "Helping people", CompanyName = "PGA Superstore", Salary = 50000M, SubmittedApplication = true }
            };

        }

        public Job Add(Job job)
        {
            job.Id = _jobList.Max(e => e.Id) + 1;
            _jobList.Add(job);
            return job;
        }

        public Job Delete(int id)
        {
            Job job = _jobList.FirstOrDefault(e => e.Id == id);
            if (job != null)
            {
                _jobList.Remove(job);
            }

            return job;
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return _jobList;
        }

        public Job GetJob(int Id)
        {
            return _jobList.FirstOrDefault(d => d.Id == Id);
        }

        public Job Update(Job jobChanges)
        {
            Job job = _jobList.FirstOrDefault(e => e.Id == jobChanges.Id);
            if (job != null)
            {
                job.Title = jobChanges.Title;
                job.Description = jobChanges.Description;
                job.Salary = jobChanges.Salary;
                job.Comments = jobChanges.Comments;
                job.CompanyName = jobChanges.CompanyName;
                job.Rating = jobChanges.Rating;
      
            }

            return job;
        }
    }
}

