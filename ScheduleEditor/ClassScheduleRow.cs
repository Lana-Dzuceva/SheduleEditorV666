using ScheduleEditorClassLibrary;
using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheduleEditorV6
{
    public enum RowTypes
    {
        Simple,
        TwoGroups,
        TwoWeeks,
        TwoGroupsAndTwoWeeks
    }

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

    //class ScheduleRow
    //{
    //    RowTypes rowType;
    //    Dictionary<string, SAcademicClass> items;
    //    //List<List<ScheduleAcademicClass>> items;
    //    public int CountOfWeeks { get; private set; }
    //    public int CountOfGroups { get; private set; }
    //    public RowTypes RowType
    //    {
    //        get { return rowType; }
    //        set
    //        {
    //            CountOfWeeks = (value == RowTypes.Simple || value == RowTypes.TwoGroups ? 1 : 2);
    //            CountOfGroups = (value == RowTypes.Simple || value == RowTypes.TwoWeeks ? 1 : 2);
    //            rowType = value;
    //        }
    //    }

    //    public Dictionary<string, SAcademicClass> Items
    //    {
    //        get
    //        {
    //            return items;
    //        }
    //    }

    //    public ScheduleRow(RowTypes rowType, SAcademicClass group1week1 =null, SAcademicClass group1week2 = null, SAcademicClass group2week1 = null, SAcademicClass group2week2 = null)
    //    {
    //        RowType = rowType;
    //        items["group1week1"] = group1week1;
    //        items["group1week2"] = group1week2;
    //        items["group2week1"] = group2week1;
    //        items["group2week2"] = group2week2;
    //    }

        
    //}
}

