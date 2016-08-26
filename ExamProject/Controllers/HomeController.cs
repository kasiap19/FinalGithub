using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamProject.Models;

namespace ExamProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        public ActionResult LoginStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginStudent(string ActionName, Student newStudent)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    // create new student
                    switch (ActionName)
                    {
                        // create
                        case "Create":
                            db.Student.Add(newStudent);
                            db.SaveChanges();
                            ViewBag.Message = "new student successfully registered !";
                            break;
                        // login
                        case "Login":
                            var v = db.Student.Where(a => a.Username.Equals(newStudent.Username) && a.Password.Equals(newStudent.Password)).FirstOrDefault();
                            // admin
                            if ("admin".Equals(newStudent.Username) && "admin".Equals(newStudent.Password))
                            {
                                return RedirectToAction("Teacher", "Tasks", new { area = "" });
                            }
                            // student
                            else if (v != null)
                            {
                                Session["LoggedStudetId"] = v.StudentId.ToString();
                                //Session["LoggedStudentName"] = v.Name.ToString();
                                Session["LoggedStudentName"] = v.Name.ToString();
                                return RedirectToAction("StudentPage", "Tasks",new { studentId = v.StudentId });
                            }
                            break;
                    }

                }
            }
            return View(newStudent);
        }
        public ActionResult StudentPage(int studentId)
        {
            List<Status> stateOfAssignments = new List<Status>();

            //go through all stateOfAssignments and put all connected with logged student in a list for View
            foreach (Status item in db.Status)
            {
                if(item.StudentId == studentId)
                {
                    stateOfAssignments.Add(item);
                }
            }

            Student student = db.Student.Find(studentId);
            

            return View(stateOfAssignments);
        }

        public ActionResult CreateAssignment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssignment([Bind(Include = "AssignmentId,Title,Text")] Task task)
        {
            if (ModelState.IsValid)
            {
                //add new assignment to the list of all the assignments in the DB
                db.Task.Add(task);
                //create new StateOfAssignments with the new assignment and all students in the system
                foreach (Student item in db.Student)
                {
                    Status newState = new Status();
                    newState.Student = item;
                    newState.StudentId = item.StudentId;
                    newState.Task = task;
                    newState.TaskId = task.TaskId;
                    db.Status.Add(newState);
                }

                db.SaveChanges();
                return RedirectToAction("AfterLoginTeacher");
            }
            return View();
        }

        public ActionResult EditAssignment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAssignment([Bind(Include = "AssignmentId,Title,Text")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AfterLoginTeacher");
            }
            return View();
        }

        public ActionResult DeleteAssignment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task assignment = db.Task.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        [HttpPost, ActionName("DeleteAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAssignmentConfirmed(int id)
        {
            // loop through all StateOf Assignments and delete all connected to this assignment
            foreach (Status item in db.Status)
            {
                if(item.TaskId == id)
                {
                    db.Status.Remove(item);
                }
            }
            //remove this assignment from the list of assignments in DB
            Task task = db.Task.Find(id);
            db.Task.Remove(task);
            db.SaveChanges();
            //Assignment.decrementNoAssignments();// decreasing number of Assignments by 1
            return RedirectToAction("AfterLoginTeacher");
        }

        public ActionResult DetailsAssignment4Student(int stateOfAssignmentId)
        {
            Status stateOfAssignment = db.Status.Find(stateOfAssignmentId);
            return View(stateOfAssignment);
        }

        public ActionResult BetweenAfterLoginAndDetailsStudent(int stateOfAssignmentId, int state)
        {
            Status stateOfAssignment = db.Status.Find(stateOfAssignmentId);
            stateOfAssignment.State = state;
            db.Entry(stateOfAssignment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AfterLoginStudent", new { studentId = stateOfAssignment.StudentId });
        }

        public ActionResult DetailsAssignment4Teacher(int assignmentId)
        {
            List<Status> stateOfAssignments = new List<Status>();
            //go through all stateOfAssignments and put all connected with selected assignment in a list for View
            foreach (Status item in db.Status)
            {
                if (item.TaskId == assignmentId)
                {
                    stateOfAssignments.Add(item);
                }
            }
            return View(stateOfAssignments);
        }

        public ActionResult StatisticsAssignment(int assignmentId)
        {
            List<Status> stateOfAssignments = new List<Status>();
            //go through all stateOfAssignments and put all connected with selected assignment in a list for View
            foreach (Status item in db.Status)
            {
                if (item.TaskId == assignmentId)
                {
                    stateOfAssignments.Add(item);
                }
            }
            return View(stateOfAssignments);
        }
    }
}