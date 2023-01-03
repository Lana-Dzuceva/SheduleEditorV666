using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <summary>
        /// название пары
        /// </summary>
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
        public WeekDay WeekDay { get; set; }
        public int ClassNumber { get; set; } // номер пары

        public ScheduleAcademicClass(int audience, WeekDay weekDay, int classNumber, AcademicClass academicClass) : base(academicClass)
        {
            Audience = audience;
            WeekDay = weekDay;
            ClassNumber = classNumber;
        }

        public string GetTitleAndTeacher()
        {
            return $"{this.ClassTitle} {this.Teacher}";
        }
        public string GetAudience()
        {
            return Audience.ToString();
        }
        public override string ToString()
        {
            return $"{this.ClassTitle} {this.Teacher}";
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