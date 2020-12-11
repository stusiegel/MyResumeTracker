using EmployeeManagement.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLJobRespository : IJobRepository
    {
        private readonly AppDbContext context;

        public SQLJobRespository(AppDbContext context)
        {
            this.context = context;
        }

        public Job Add(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();
            return job;
        }

        public Job Delete(int id)
        {
            Job job = context.Jobs.Find(id);
            if (job != null)
            {
                context.Jobs.Remove(job);
                context.SaveChanges();

            }
            return job;
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return context.Jobs;
        }

        public Job GetJob(int Id)
        {
            return context.Jobs.Find(Id);
        }

        public Job Update(Job jobChanges)
        {
            var job = context.Jobs.Attach(jobChanges);
            job.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return jobChanges;
        }
    }
}
