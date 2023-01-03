using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScheduleEditorClassLibrary;
using System.Runtime.CompilerServices;

namespace SheduleEditorV6
{
    public static class DataGridViewExtesions
    {

        /// <summary>
        /// приводит к стандартному виду где 1 подгруппа и 1 неделя
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind"></param>
        public static void ToSimpleView(this DataGridView dataGrid, int ind)
        {
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 3;

            for (int i = 0; i < 4; i++)
            {
                (dataGrid[i, ind] as DataGridViewTextBoxCellEx).RowSpan = 2;
            }
        }
        /// <summary>
        /// приводит строку расписания к нужному виду в зависимости от типа
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind">номер строки</param>
        public static void VisualizeRow(this DataGridView dataGrid, int ind, RowTypes RowType)
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
        /// <summary>
        /// переносит текстовую информацию из ScheduleRow
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind"></param>
        /// <param name="scheduleRow"></param>
        public static void FillRow(this DataGridView dataGrid, int ind, ScheduleRow scheduleRow)
        {
            dataGrid[1, ind].Value = scheduleRow.Group1week1.GetTitleAndTeacher();
            dataGrid[2, ind].Value = scheduleRow.Group2week1.GetTitleAndTeacher();
            dataGrid[1, ind + 1].Value = scheduleRow.Group1week1.GetTitleAndTeacher();
            dataGrid[2, ind + 1].Value = scheduleRow.Group2week1.GetTitleAndTeacher();
            dataGrid[0, ind].Value = scheduleRow.Group1week1.GetAudience();
            dataGrid[3, ind].Value = scheduleRow.Group2week1.GetAudience();
            dataGrid[0, ind + 1].Value = scheduleRow.Group1week1.GetAudience();
            dataGrid[3, ind + 1].Value = scheduleRow.Group2week1.GetAudience();
        }

        public static void UpdateDataGrid(this DataGridView dataGrid, ScheduleData data)
        {
            for (int i = 0; i < data.Count(); i++)
            {
                dataGrid.FillRow(i * 2, data[i]);
                dataGrid.VisualizeRow(i * 2, data[i].RowType);
            }
        }
    }

}
