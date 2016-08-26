using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ExamProject.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("AppDb")
        {
        }
        public DbSet<Status> Status { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Task> Task { get; set; }
        


    }
}