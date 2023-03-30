using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    /// <summary>
    /// ScheduleGroup хранит в себе rows
    /// </summary>
    public class SGroup
    {
        public string Title;
        public List<ScheduleRow> Rows { get; set; } // ScheduleAcademicClass

        public SGroup(string title)
        {
            Rows = new List<ScheduleRow>();
            Title = title;
        }

        [JsonConstructor]
        public SGroup(string title, List<ScheduleRow> rows)
        {
            Rows = rows;
            Title = title;
        }

        public void Add(ScheduleRow row)
        {
            Rows.Add(row);
        }

        public ScheduleRow this[int index]
        {
            get { return Rows[index]; }
            set { Rows[index] = value; }
        }

        public ScheduleRow this[DayOfWeek dayOfWeek, int classNumber]
        {
            get { return Rows.Where(row => row.WeekDay == dayOfWeek && row.ClassNumber == classNumber).Single(); }
        }

        public int Count()
        {
            return Rows.Count();
        }
    }
}
