using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public Results IsTeacherAvaible(string activeGroup, int row, int col, AcademicClass academicClass)
        {
            if ((academicClass.SubGroup == SubGroups.First && col >= 2 ||
                academicClass.SubGroup == SubGroups.Second && col < 2) && academicClass.Type != ClassTypes.Lecture)
                return Results.TypeMismatch;
            for (int i = 0; i < Groups.Count; i++)
            {
                var sRow = Groups[i][(DayOfWeek)(row / 8 + 1), (row - row / 8 * 8) / 2 + 1];
                if (sRow == null) continue;
                var teacher11 = sRow.Group1week1?.Teacher ?? (new Teacher("qqq"));
                var t12 = sRow.Group1week2?.Teacher ?? (new Teacher("qqq"));
                var t21 = sRow.Group2week1?.Teacher ?? (new Teacher("qqq"));
                var t22 = sRow.Group2week2?.Teacher ?? (new Teacher("qqq"));
                if (Groups[i].Title == activeGroup)
                {
                    if (academicClass.Type == ClassTypes.Practice)
                    {
                        if (academicClass.Hours > 36)
                        {
                            if (t21 == academicClass.Teacher || t22 == academicClass.Teacher)
                                return Results.TeacherIsBusy;
                        }
                        else // раз в 2 недели
                        {
                            if (row % 2 == 0 && (teacher11 == academicClass.Teacher || t21 == academicClass.Teacher) ||
                                row % 2 != 0 && t12 == academicClass.Teacher || t22 == academicClass.Teacher)
                                return Results.TeacherIsBusy;
                        }
                    }
                    continue;
                }

                if (academicClass.Type == ClassTypes.Lecture)
                {
                    if (academicClass.Hours > 36)
                    {
                        if (teacher11 == academicClass.Teacher || t12 == academicClass.Teacher ||
                            t21 == academicClass.Teacher || t22 == academicClass.Teacher)
                            return Results.TeacherIsBusy;
                    }
                    else // раз в 2 недели
                    {
                        if (row % 2 == 0 && (teacher11 == academicClass.Teacher || t21 == academicClass.Teacher) ||
                            row % 2 != 0 && (t12 == academicClass.Teacher || t22 == academicClass.Teacher))
                            return Results.TeacherIsBusy;
                    }
                }
                else if (academicClass.Type == ClassTypes.Practice)
                {
                    if (academicClass.Hours > 36)
                    {
                        if (col < 2 && (teacher11 == academicClass.Teacher || t12 == academicClass.Teacher) ||
                            col >= 2 && (t21 == academicClass.Teacher || sRow.Group2week2.Teacher == academicClass.Teacher))
                            return Results.TeacherIsBusy;
                    }
                    else // раз в 2 недели
                    {
                        if (row % 2 == 0)
                        {
                            if (col < 2 && teacher11 == academicClass.Teacher ||
                                col >= 2 && t21 == academicClass.Teacher)
                                return Results.TeacherIsBusy;
                        }
                        else
                        {
                            if (col < 2 && t12 == academicClass.Teacher ||
                                col >= 2 && t22 == academicClass.Teacher)
                                return Results.TeacherIsBusy;
                        }
                    }
                }
            }
            return Results.Available;
        }
        public Results IsAudienceAvaible(string activeGroup, int row, int col, AcademicClass academicClass, int aud)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                var sRow = Groups[i][(DayOfWeek)(row / 8 + 1), (row - row / 8 * 8) / 2 + 1];
                if (sRow == null) continue;
                var aud11 = sRow.Group1week1?.Audience ?? -1;
                var a12 = sRow.Group1week2?.Audience ?? -1;
                var a21 = sRow.Group2week1?.Audience ?? -1;
                var t22 = sRow.Group2week2?.Audience ?? -1;
                if (Groups[i].Title == activeGroup)
                {
                    if (academicClass.Type == ClassTypes.Practice)
                    {
                        if (academicClass.Hours > 36)
                        {
                            if (a21 == aud || t22 == aud)
                                return Results.TeacherIsBusy;
                        }
                        else // раз в 2 недели
                        {
                            if (row % 2 == 0 && (aud11 == aud || a21 == aud) ||
                                row % 2 != 0 && a12 == aud || t22 == aud)
                                return Results.TeacherIsBusy;
                        }
                    }
                    continue;
                }

                if (academicClass.Type == ClassTypes.Lecture)
                {
                    if (academicClass.Hours > 36)
                    {
                        if (aud11 == aud || a12 == aud ||
                            a21 == aud || t22 == aud)
                            return Results.TeacherIsBusy;
                    }
                    else // раз в 2 недели
                    {
                        if (row % 2 == 0 && (aud11 == aud || a21 == aud) ||
                            row % 2 != 0 && (a12 == aud || t22 == aud))
                            return Results.TeacherIsBusy;
                    }
                }
                else if (academicClass.Type == ClassTypes.Practice)
                {
                    if (academicClass.Hours > 36)
                    {
                        if (col < 2 && (aud11 == aud || a12 == aud) ||
                            col >= 2 && (a21 == aud || sRow.Group2week2.Teacher == academicClass.Teacher))
                            return Results.TeacherIsBusy;
                    }
                    else // раз в 2 недели
                    {
                        if (row % 2 == 0)
                        {
                            if (col < 2 && aud11 == aud ||
                                col >= 2 && a21 == aud)
                                return Results.TeacherIsBusy;
                        }
                        else
                        {
                            if (col < 2 && a12 == aud ||
                                col >= 2 && t22 == aud)
                                return Results.TeacherIsBusy;
                        }
                    }
                }
            }
            return Results.Available;
        }
        public void PutData(string activeGroup, int row, int col, AcademicClass academicClass, int audience)
        {
            var weekDay = (DayOfWeek)(row / 8 + 1);
            var сlassNumber = (row - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
            var sRow = this[activeGroup][weekDay, сlassNumber];
            if (sRow == null)
            {
                this[activeGroup].Add(new ScheduleRow(weekDay, сlassNumber));
                sRow = this[activeGroup][weekDay, сlassNumber];
            }
            var sAcademicClass = new SAcademicClass(audience, weekDay, сlassNumber, academicClass);
            if (academicClass.Type == ClassTypes.Lecture)
            {
                if (academicClass.Hours > 36)
                {
                    sRow.Group1week1 = sAcademicClass;
                    sRow.Group1week2 = sAcademicClass;
                    sRow.Group2week1 = sAcademicClass;
                    sRow.Group2week2 = sAcademicClass;
                }
                else
                {
                    if (row % 2 == 0)
                    {
                        sRow.Group1week1 = sAcademicClass;
                        sRow.Group2week1 = sAcademicClass;
                    }
                    else
                    {
                        sRow.Group1week2 = sAcademicClass;
                        sRow.Group2week2 = sAcademicClass;
                    }
                }
            }
            else
            {
                if (academicClass.Hours > 36)
                {
                    if (col < 2)
                    {
                        sRow.Group1week1 = sAcademicClass;
                        sRow.Group1week2 = sAcademicClass;
                    }
                    else
                    {
                        sRow.Group2week1 = sAcademicClass;
                        sRow.Group2week2 = sAcademicClass;
                    }
                }
                else
                {
                    if (row % 2 == 0 && col < 2)
                    {
                        sRow.Group1week1 = sAcademicClass;
                    }
                    else if (row % 2 == 0)
                    {
                        sRow.Group2week1 = sAcademicClass;
                    }
                    else if (col < 2)
                    {
                        sRow.Group1week2 = sAcademicClass;
                    }
                    else
                    {
                        sRow.Group2week2 = sAcademicClass;
                    }
                }
            }

            //if (academicClass.Type == ClassTypes.Lecture && (academicClass.Hours <= 36 && row % 2 == 0 || academicClass.Hours > 36) ||
            //academicClass.Type == ClassTypes.Practice && academicClass.Hours > 36 && col < 2)
            //{
            //    sRow.Group1week1 = sAcademicClass;
            //}
            //else if (academicClass.Type == ClassTypes.Lecture)
            //{
            //    if (academicClass.Hours <= 36 && row % 2 != 0) // раз в 2 недели
            //    {
            //        sRow.Group1week2 = sAcademicClass;
            //    }
            //}
            //else
            //{
            //    if (academicClass.Hours <= 36) // раз в 2 недели
            //    {
            //        if (row % 2 == 0 && col < 2)
            //        {
            //            sRow.Group1week1 = sAcademicClass;
            //        }
            //        else if (row % 2 == 0)
            //        {
            //            sRow.Group2week1 = sAcademicClass;
            //        }
            //        else if (col < 2)
            //        {
            //            sRow.Group1week2 = sAcademicClass;
            //        }
            //        else
            //        {
            //            sRow.Group2week2 = sAcademicClass;
            //        }
            //    }
            //    else if (col >= 2)
            //    {
            //        sRow.Group2week1 = sAcademicClass;
            //    }
            //}
        }
    }
}
