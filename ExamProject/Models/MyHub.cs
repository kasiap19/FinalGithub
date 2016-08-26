using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ExamProject.Models
{
    public class MyHub : Hub
    {
        public void UpdateAssignmentState(int studentId, string studentName, int state, int previousState)
        {
            Clients.All.updateAssignmentState(studentId, studentName, state, previousState);
        }

        public void UpdateNewAssignment()
        {
            Clients.All.updateNewAssignment();
        }

        public void UpdateAfterDeletingAssignment()
        {
            Clients.All.updateAfterDeletingAssignment();
        }

        public void UpdateNewStudent(string studentName, int studentId)
        {
            Clients.All.updateNewStudent(studentName, studentId);
        }

        public void UpdateModifiedAssignment(int assignmentId)
        {
            Clients.All.updateModifiedAssignment(assignmentId);
        }
    }
}