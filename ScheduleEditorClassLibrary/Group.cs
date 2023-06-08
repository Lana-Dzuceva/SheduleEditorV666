using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
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

        internal void FillAcademicClasses(MySqlConnection connection, string databaseName)
        {
            throw new NotImplementedException();
        }
    }
}
