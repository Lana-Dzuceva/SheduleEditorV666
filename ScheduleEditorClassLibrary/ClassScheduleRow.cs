using ScheduleEditorClassLibrary;
using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public enum RowTypes
    {
        Simple,
        TwoGroups,
        TwoWeeks,
        TwoGroupsAndTwoWeeks
    }
    #region 
    //public class RowItem
    //{
    //    public string Subject;
    //    public string Audience;

    //    public RowItem(string subject, string audience)
    //    {
    //        if (!int.TryParse(audience, out _)) throw new Exception("Аудитория должна быть числом");
    //        Subject = subject;
    //        Audience = audience;
    //    }
    //}
    #endregion
    public class ScheduleRow
    {
        RowTypes rowType;
        //Dictionary<string, ScheduleAcademicClass> items;
        public ScheduleAcademicClass Group1week1;
        public ScheduleAcademicClass Group1week2;
        public ScheduleAcademicClass Group2week1;
        public ScheduleAcademicClass Group2week2;
        public WeekDays WeekDay { get; set; }
        public int ClassNumber { get; set; } // номер пары
        //List<List<ScheduleAcademicClass>> items;
        public int CountOfWeeks { get; private set; }
        public int CountOfGroups { get; private set; }

        public RowTypes RowType
        {
            get { return rowType; }
            set
            {
                CountOfWeeks = (value == RowTypes.Simple || value == RowTypes.TwoGroups ? 1 : 2);
                CountOfGroups = (value == RowTypes.Simple || value == RowTypes.TwoWeeks ? 1 : 2);
                rowType = value;
            }
        }

        //public Dictionary<string, ScheduleAcademicClass> Items
        //{
        //    get
        //    {
        //        return items;
        //    }
        //}

        public ScheduleRow()
        {
        }
        public ScheduleRow(RowTypes rowType, ScheduleAcademicClass group1week1 = null, ScheduleAcademicClass group1week2 = null, ScheduleAcademicClass group2week1 = null, ScheduleAcademicClass group2week2 = null)
        {
            RowType = rowType;
            Group1week1 = group1week1;
            Group1week2 = group1week2;
            Group2week1 = group2week1;
            Group2week2 = group2week2;
            //items["group1week1"] = group1week1;
            //items["group1week2"] = group1week2;
            //items["group2week1"] = group2week1;
            //items["group2week2"] = group2week2;
        }
    }
}

