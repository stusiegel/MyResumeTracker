using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeManagement.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //calls the extentions method in Models.ModelBuilderExtensions.Seed()
            modelBuilder.Seed();

            //Data moved to Models.ModelBuilderExtensions.Seed()
            // modelBuilder.Entity<Employee>().HasData(
            //     new Employee
            //     {
            //         Id = 1,
            //         Name = "Mark",
            //         Department = DeptEnum.IT,
            //         Email = "mark@pragimtech.com"
            //     },
            //     new Employee {
            //         Id = 2,
            //         Name = "Stu",
            //         Department = DeptEnum.Payroll,
            //         Email = "stu.siegel@yahoo.com"
            //     },
            //     new Employee
            //     {
            //         Id = 3,
            //         Name = "Mary Duckworth",
            //         Department = DeptEnum.HR,
            //         Email = "MaryD@gmail.com"
            //     }
            // );

            // modelBuilder.Entity<Job>().HasData(
            //    new Job
            //    {
            //        Id = 1,
            //        Title = "Sr Software Developer",
            //        Description = "Master of all",
            //        Comments = "Awesome Opportunity",
            //        CompanyName = "CVS",
            //        Rating = JobRatingEnum.One 
            //        //Salary = 104000M
            //    },
            //    new Job 
            //    { Id = 2, 
            //        Title = "Sr Engineer", 
            //        Description = "Use Java", 
            //        Comments = "Java's a job ", 
            //        CompanyName = "USAA", 
            //        Rating = JobRatingEnum.Three 
            //    },
            //    new Job 
            //    { Id = 3, 
            //        Title = "Golf Pro", 
            //        Description = "Leaving the dream", 
            //        Comments = "Who doesn't love golf", 
            //        CompanyName = "PGA", 
            //        Rating = JobRatingEnum.Five 
            //    }


            //);
        }

        public DbSet<EmployeeManagement.ViewModels.JobEditViewModel> JobEditViewModel { get; set; }
    }
}

