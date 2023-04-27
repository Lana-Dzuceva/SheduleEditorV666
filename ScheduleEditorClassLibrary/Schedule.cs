using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScheduleEditorClassLibrary
{
    public class Schedule
    {
        public List<SGroup> Groups { get; set; }

        public Schedule(List<string> groupTitles)
        {
            Groups = groupTitles.Select(title => new SGroup(title)).ToList();
        }

        [JsonConstructor]
        public Schedule(List<SGroup> groups)
        {
            Groups = groups;
        }

        public SGroup this[string groupTitle]
        {
            get { return Groups.Where(group => group.Title == groupTitle).Single(); }
        }
        public Results isCellAvaible(string activeGroup, int row, int col, AcademicClass academicClass)
        {
            //    обновлять структуру для внешнего отображения и удалять лишние данные 
            if (academicClass.SubGroup == SubGroups.First && col >= 2 ||
                academicClass.SubGroup == SubGroups.Second && col < 2)
                return Results.TypeMismatch;
            for(int i = 0; i < Groups.Count; i++)
            {
                var group = Groups[i][(DayOfWeek)(row / 5), row % 5];
                bool simple = academicClass.Type == ClassTypes.Lecture && academicClass.Hours > 36 && group.Group1week1.Teacher == academicClass.Teacher;
                bool twoWeeks1 = academicClass.Type == ClassTypes.Lecture && academicClass.Hours <= 36 && row % 2 == 0 && group.Group1week1.Teacher == academicClass.Teacher;
                bool twoWeeks2 = academicClass.Type == ClassTypes.Lecture && academicClass.Hours <= 36 && row % 2 != 0 && group.Group1week2.Teacher == academicClass.Teacher;
                bool twoGroups1 = academicClass.Type == ClassTypes.Practice && academicClass.Hours > 36 && col < 2 && group.Group1week1.Teacher == academicClass.Teacher;
                bool twoGroups2 = academicClass.Type == ClassTypes.Practice && academicClass.Hours > 36 && col > 2 && group.Group2week1.Teacher == academicClass.Teacher;
                bool twoGroupsAndTwoWeeks1 = academicClass.Type == ClassTypes.Practice && academicClass.Hours <= 36 && col < 2 && row % 2 == 0 && group.Group1week1.Teacher == academicClass.Teacher;
                bool twoGroupsAndTwoWeeks2 = academicClass.Type == ClassTypes.Practice && academicClass.Hours <= 36 && col < 2 && row % 2 != 0 && group.Group1week2.Teacher == academicClass.Teacher;
                bool twoGroupsAndTwoWeeks3 = academicClass.Type == ClassTypes.Practice && academicClass.Hours <= 36 && col >= 2 && row % 2 == 0 && group.Group2week1.Teacher == academicClass.Teacher;
                bool twoGroupsAndTwoWeeks4 = academicClass.Type == ClassTypes.Practice && academicClass.Hours <= 36 && col >= 2 && row % 2 != 0 && group.Group2week2.Teacher == academicClass.Teacher;

                if (simple || twoWeeks1 || twoWeeks2 || twoGroups1 || twoGroups2 || twoGroupsAndTwoWeeks1 || twoGroupsAndTwoWeeks2 || twoGroupsAndTwoWeeks3 || twoGroupsAndTwoWeeks4) 
                {
                    return Results.TeacherIsBusy;
                }
            }
            return Results.Available;
        }
    }
}
