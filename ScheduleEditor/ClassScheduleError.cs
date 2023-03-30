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
        public string GroupTitle { get; set; }
        //test
        public SAcademicClass ScheduleAcademicClass { get; set; }
        public string Message { get; set; }
    }
}
