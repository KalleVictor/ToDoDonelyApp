﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDoDonelyApp
{
    public class Project
    {
        private static int nextID = 1; // Static field to keep track of the next ID

        public int ProjectIDnumber { get; private set; } // Property to store the project ID
        public string ProjectName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime ProjectDate { get; set; }
        public DateTime ProjectDueDate { get; set; }
        public string ProjectStatus { get; set; }

        public Project(string projectname, string taskdescription, DateTime projectdate, DateTime projectduedate, string projectstatus)
        {
            ProjectName = projectname;
            TaskDescription = taskdescription;
            ProjectDate = projectdate;
            ProjectDueDate = projectduedate;
            ProjectStatus = projectstatus;
            AssignID();
        }
        private void AssignID()
        {
            ProjectIDnumber = nextID++; // Assign the next available ID and increment the counter
        }
    }
}