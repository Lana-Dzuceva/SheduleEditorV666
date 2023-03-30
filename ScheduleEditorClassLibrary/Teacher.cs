using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public class Teacher
    {
        public string Name { get; set; }
        public List<TeacherPreference> Preferences { get; set; }
        public Teacher(string name)
        {
            Name = name;
            Preferences = new List<TeacherPreference>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
