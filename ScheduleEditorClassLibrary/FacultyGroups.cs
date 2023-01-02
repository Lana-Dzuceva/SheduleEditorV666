﻿using System;
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

    public enum ClassTypes
    {
        Lecture,
        Practice
    }

    public enum SubGroups
    {
        first,
        second
    }
    public enum WeekDays
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
        public ClassTypes Type { get; set; }
        public SubGroups SubGroup { get; set; }

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
        public AcademicClass()
        {

        }
    }


    /// <summary>
    /// Пара которая меет в расписании конкретное место
    /// </summary>
    public class ScheduleAcademicClass : AcademicClass
    {
        public int Audience { get; set; }
        WeekDays WeekDay { get; set; }
        int ClassNumber { get; set; } // номер пары

        //public ScheduleAcademicClass(int audience, WeekDays weekDay, int classNumber, AcademicClass academicClass)
        //    : base(academicClass.ClassTitle, academicClass.Teacher, academicClass.Hours, academicClass.Type, academicClass.SubGroups)
        //{
        //    Audience = audience;
        //    WeekDays = weekDay;
        //    ClassNumber = classNumber;
        //}
        public ScheduleAcademicClass(int audience, WeekDays weekDay, int classNumber, AcademicClass academicClass) : base(academicClass)
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