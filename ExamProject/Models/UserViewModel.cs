using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamProject.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public UserViewModel(int id, string name)
        {
            this.UserId = id;
            this.UserName = name;
        }
    }
}