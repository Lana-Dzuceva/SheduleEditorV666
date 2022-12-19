using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheduleEditorV2
{
    public enum RowTypes
    {
        Simple,
        TwoGroups,
        TwoWeeks,
        TwoGroupsAndTwoWeeks
    }

    public class RowItem
    {
        public string Subject;
        public string Audience;

        public RowItem(string subject, string audience)
        {
            if (!int.TryParse(audience, out _)) throw new Exception("Аудитория должна быть числом");
            Subject = subject;
            Audience = audience;
        }
    }

    class ClassScheduleRow
    {
        RowTypes rowType;
        List<List<RowItem>> items;
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
        
        //public List<List<RowItem>> Items {
        //    get
        //    {
        //        return items;
        //    }
        //    set
        //    {

        //    }
        //}
        
        


        public ClassScheduleRow(RowTypes rowType, RowItem group1week1, RowItem group1week2 = null, RowItem group2week1 = null, RowItem group2week2 = null)
        {
            RowType = rowType;
            Items = new List<List<RowItem>>() {new List<RowItem>(){ null, null}, new List<RowItem> (){ null, null } };
            Items = new List<List<RowItem>>();

            rows["group1week1"] = group1week1;
            rows["group1week2"] = group1week2;
            rows["group2week1"] = group2week1;
            rows["group2week2"] = group2week2;
        }

        /// <summary>
        /// приводит к стандартному виду где 1 подгруппа и 1 неделя
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind"></param>
        public void ToSimpleView(DataGridView dataGrid, int ind)
        {
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 3;

            for (int i = 0; i < 4; i++)
            {
                (dataGrid[i, ind] as DataGridViewTextBoxCellEx).RowSpan = 2;
            }
        }
        /// <summary>
        /// Делает внешне датагрид таким каким надо
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind">номер строки</param>
        public void VisualizeRow(DataGridView dataGrid, int ind)
        {
            ind -= ind % 2;
            switch (RowType)
            {
                case RowTypes.Simple:
                    ToSimpleView(dataGrid, ind);
                    break;
                case RowTypes.TwoGroups:
                    ToSimpleView(dataGrid, ind);
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 1;
                    for (int i = 0; i < 4; i++)
                        (dataGrid[i, ind] as DataGridViewTextBoxCellEx).RowSpan = 2;

                    break;
                case RowTypes.TwoWeeks:
                    ToSimpleView(dataGrid, ind);
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (dataGrid[3, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
                    (dataGrid[0, ind + 1] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
                    break;
                case RowTypes.TwoGroupsAndTwoWeeks:
                    ToSimpleView(dataGrid, ind);
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 1;
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (dataGrid[3, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;

                    break;
                default:
                    break;
            }
        }
    }
}

