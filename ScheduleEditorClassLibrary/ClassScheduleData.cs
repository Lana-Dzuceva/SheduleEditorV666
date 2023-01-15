using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public class ScheduleData
    {
        public List<ScheduleRow> Data { get; set; }

        public ScheduleData()
        {
            Data = new List<ScheduleRow>(20); // 5 дней в неделю по 4 пары
            for (int i = 0; i < 20; i++)
            {
                Data.Add(new ScheduleRow());    
            }
        }
        public ScheduleRow this [int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }
        public int Count()
        {
            return Data.Count;
        }
    }
}
