using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models
{
    public class User
    {
        //public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }
        public int UserType { get; set; }
    }

    public class Teacher : User
    {
        //private static int noTeachers = 0;
        public int TeacherId { get; set; }

        public Teacher()
        {
            
            this.UserType = 1; //teacher
            this.TeacherId = 1;
            //noTeachers++;
            //this.UserId = noTeachers;
        }
    }

    //public class Student : User
    //{
    //    private static int noStudents = 0;
    //    public int StudentId { get; set; }

    //    public Student()
    //    {
    //        this.UserType = 2; //student

    //        noStudents++;
    //        this.StudentId = noStudents;
    //    }
    //    public static int GetNoStudents()
    //    {
    //        return Student.noStudents;
    //    }
    //}
}