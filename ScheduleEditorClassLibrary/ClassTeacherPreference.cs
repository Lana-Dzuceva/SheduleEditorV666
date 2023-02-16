using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public class Preference
    {
        public WeekDays WeekDay { get; set; }
        public int LessonNumber { get; set; }
        public Preference(WeekDays weekDay, int lessonNumber)
        {
            WeekDay = weekDay;
            LessonNumber = lessonNumber;
        }
    }
    public class TeacherPreference
    {
        public string Name { get; set; }

        public List<Preference> Preferences { get; set; }
        public TeacherPreference(string name)
        {
            Name = name;
            Preferences = new List<Preference>();
        }
    }
}
