using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamProject.Models
{
    public class Task
    {
       // private static int noAssignments = 0;

        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }


        //public Task()
        //{
        //    noAssignments++;
        //    this.TaskId = noAssignments;
        //}

        //public static void decrementNoAssignments()
        //{
        //    Assignment.noAssignments--;
        //}
    }
}