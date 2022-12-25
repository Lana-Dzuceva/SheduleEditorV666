using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// Делает внешне датагрид таким каким надо
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind">номер строки</param>
        public static void VisualizeRow(this DataGridView dataGrid, int ind)
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
