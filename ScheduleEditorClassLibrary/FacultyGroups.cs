using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public class FacultyGroups
    {
        public List<Group> Groups { get; set; }

        public FacultyGroups()
        {
            Groups = new List<Group>();
        }
        public FacultyGroups(List<Group> groups)
        {
            Groups = groups;
        }
        public void Add(Group group)
        {
            Groups.Add(group);
        }

        string GetAnsFromAPI()
        {
            HttpClient httpClient = new HttpClient();
            var answer = httpClient.GetStringAsync("http://math.nosu.ru/api/asdas4s5d1r45rd4h21hj45k/SELECT name, collation_name FROM sys.databases");
            return answer.ToString();
        }

        void ParseAPIAnswer()
        {
            string answer = GetAnsFromAPI();
            //...
        }
    }

    public class Group
    {
        public List<AcademicClass> Classes { get; set; } // ScheduleAcademicClass

        public Group()
        {
            Classes = new List<AcademicClass>();
        }

        public Group(List<AcademicClass> classes)
        {
            Classes = classes;
        }

        public void Add(AcademicClass academicClass)
        {
            Classes.Add(academicClass);
        }
    }

    public enum ClassType
    {
        Lecture,
        Practice
    }

    public enum SubGroup
    {
        first,
        second
    }
    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public class AcademicClass
    {
        public string ClassTitle { get; set; }
        public Teacher Teacher { get; set; }
        public int Hours { get; set; }
        public ClassType Type { get; set; }
        public SubGroup SubGroup { get; set; }

        public AcademicClass(string classTitle, Teacher teacher, int hours, ClassType type, SubGroup subGroup)
        {
            this.ClassTitle = classTitle;
            this.Teacher = teacher;
            this.Hours = hours;
            this.Type = type;
            this.SubGroup = subGroup;
        }
        public AcademicClass(AcademicClass @class)
        {
            ClassTitle = @class.ClassTitle;
            Teacher = @class.Teacher;
            Hours = @class.Hours;
            Type = @class.Type;
            SubGroup = @class.SubGroup;
        }
        public AcademicClass()
        {

        }
    }



    public class ScheduleAcademicClass : AcademicClass
    {
        public int Audience { get; set; }
        WeekDay WeekDay { get; set; }
        int ClassNumber { get; set; } // номер пары

        //public ScheduleAcademicClass(int audience, WeekDay weekDay, int classNumber, AcademicClass academicClass)
        //    : base(academicClass.ClassTitle, academicClass.Teacher, academicClass.Hours, academicClass.Type, academicClass.SubGroup)
        //{
        //    Audience = audience;
        //    WeekDay = weekDay;
        //    ClassNumber = classNumber;
        //}
        public ScheduleAcademicClass(int audience, WeekDay weekDay, int classNumber, AcademicClass academicClass) : base(academicClass)
        {
            Audience = audience;
            WeekDay = weekDay;
            ClassNumber = classNumber;
        }
    }


    
    public class Teacher
    {
        public string Name { get; set; }

        public Teacher(string name)
        {
            Name = name;
        }
    }
}