using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleEditorClassLibrary
{
    public class FacultyGroups
    {
        public List<Group> Groups { get; set; }

        public FacultyGroups()
        {
            Groups = new List<Group>();
        }
        [JsonConstructor]
        public FacultyGroups(List<Group> groups)
        {
            Groups = groups;
        }
        public void Add(Group group)
        {
            Groups.Add(group);
        }
    }

    public class Group
    {
        public string Title;
        public List<AcademicClass> Classes { get; set; } // ScheduleAcademicClass

        public Group(string title)
        {
            Classes = new List<AcademicClass>();
            Title = title;
        }
        [JsonConstructor]
        public Group(string title, List<AcademicClass> classes)
        {
            Classes = classes;
            Title = title;
        }
        public void Add(AcademicClass academicClass)
        {
            Classes.Add(academicClass);
        }
    }
}