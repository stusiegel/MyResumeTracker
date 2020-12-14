using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Mark",
                   Department = DeptEnum.IT,
                   Email = "mark@pragimtech.com"
               },
               new Employee
               {
                   Id = 2,
                   Name = "Stu",
                   Department = DeptEnum.Payroll,
                   Email = "stu.siegel@yahoo.com"
               },
               new Employee
               {
                   Id = 3,
                   Name = "Mary Duckworth",
                   Department = DeptEnum.HR,
                   Email = "MaryD@gmail.com"
               }
           );

            modelBuilder.Entity<Job>().HasData(
               new Job
               {
                   Id = 1,
                   Title = "Sr Software Developer",
                   Description = "Master of all",
                   Comments = "Awesome Opportunity",
                   CompanyName = "CVS",
                   Rating = JobRatingEnum.One
                   //Salary = 104000M
               },
               new Job
               {
                   Id = 2,
                   Title = "Sr Engineer",
                   Description = "Use Java",
                   Comments = "Java's a job ",
                   CompanyName = "USAA",
                   Rating = JobRatingEnum.Three
               },
               new Job
               {
                   Id = 3,
                   Title = "Golf Pro",
                   Description = "Leaving the dream",
                   Comments = "Who doesn't love golf",
                   CompanyName = "PGA",
                   Rating = JobRatingEnum.Five
               }


           );
        }
    }
}
