using ScheduleEditorClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleEditorV6
{
    // у препода пары в 2 местах сразу
    // занят один кабинет двумя группами
    // не учтены предпочтения препода
    // 
    public class ScheduleError
    {
        public Results Type { get; set; }
        public string GroupTitle { get; set; }
        public ScheduleRow ScheduleRow { get; set; }
        public int col;//1-2
        /// <summary>
        /// 1-2
        /// </summary>
        public int row;
        public string Message { get; set; }
        public ScheduleError(Results type, string groupTitle, ScheduleRow scheduleRow, int col, int row, string message="")
        {
            Type = type;
            GroupTitle = groupTitle;
            ScheduleRow = scheduleRow;
            this.col = col;
            this.row = row;
            Message = message;
        }
    }
}
