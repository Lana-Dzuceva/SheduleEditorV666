using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public class ScheduleData
    {
        public List<ClassScheduleRow> Data { get; set; }

        public ScheduleData()
        {
            Data = new List<ClassScheduleRow>(20); // 5 дней в неделю по 4 пары
        }
        public ClassScheduleRow this [int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }
    }
}
