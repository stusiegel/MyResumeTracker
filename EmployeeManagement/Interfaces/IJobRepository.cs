using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IJobRepository
    {
        Job GetJob(int Id);

        IEnumerable<Job> GetAllJobs();

        Job Add(Job job);

       Job Update(Job jobChanges);

        Job Delete(int id);
       

        //Job Edit(int id);
    }
}
