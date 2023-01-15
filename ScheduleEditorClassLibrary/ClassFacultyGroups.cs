using Newtonsoft.Json;
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
        [JsonConstructor]
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

    public class AcademicClass
    {
        /// <summary>
        /// название пары
        /// </summary>
        public string ClassTitle { get; set; }
        public Teacher Teacher { get; set; }
        public int Hours { get; set; }
        public ClassTypes Type { get; set; }
        public SubGroups SubGroup { get; set; }
        [JsonConstructor]
        public AcademicClass(string classTitle, Teacher teacher, int hours, ClassTypes type, SubGroups subGroup)
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
    }


    /// <summary>
    /// Пара которая меет в расписании конкретное место
    /// </summary>
    public class ScheduleAcademicClass : AcademicClass
    {
        public int Audience { get; set; }
        public WeekDays WeekDay { get; set; }
        public int ClassNumber { get; set; } // номер пары

        public ScheduleAcademicClass(int audience, WeekDays weekDay, int classNumber, AcademicClass academicClass) : base(academicClass)
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
    }

    public class Teacher
    {
        public string Name { get; set; }

        public Teacher(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name; 
        }
    }
}