﻿using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScheduleEditorClassLibrary;
using System.Runtime.CompilerServices;
using System.Drawing;

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
        static void ToTwoGrops(this DataGridView dataGrid, int ind)
        {
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 1;
            for (int i = 0; i < 4; i++)
                (dataGrid[i, ind] as DataGridViewTextBoxCellEx).RowSpan = 2;
        }
        static void ToTwoWeeks(this DataGridView dataGrid, int ind)
        {
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
            (dataGrid[3, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
            (dataGrid[0, ind + 1] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
        }
        static void ToTwoGroupsAndTwoWeeks(this DataGridView dataGrid, int ind)
        {
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 1;
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
            (dataGrid[3, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
        }


        /// <summary>
        /// приводит строку расписания к нужному виду в зависимости от типа
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind">номер строки</param>
        public static void VisualizeRow(this DataGridView dataGrid, int ind, RowTypes RowType)
        {
            ind *= 2;
            switch (RowType)
            {
                case RowTypes.Simple:
                    ToSimpleView(dataGrid, ind);
                    break;
                case RowTypes.TwoGroups:
                    ToSimpleView(dataGrid, ind);
                    ToTwoGrops(dataGrid, ind);
                    break;
                case RowTypes.TwoWeeks:
                    ToSimpleView(dataGrid, ind);
                    ToTwoWeeks(dataGrid, ind);
                    break;
                case RowTypes.TwoGroupsAndTwoWeeks:
                    ToSimpleView(dataGrid, ind);
                    ToTwoGroupsAndTwoWeeks(dataGrid, ind);
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
            ind *= 2;
            dataGrid[1, ind].Value = scheduleRow?.Group1week1.GetTitleAndTeacher() ?? "";
            dataGrid[2, ind].Value = scheduleRow?.Group2week1.GetTitleAndTeacher() ?? "";
            dataGrid[1, ind + 1].Value = scheduleRow?.Group1week1.GetTitleAndTeacher() ?? "";
            dataGrid[2, ind + 1].Value = scheduleRow?.Group2week1.GetTitleAndTeacher() ?? "";
            dataGrid[0, ind].Value = scheduleRow?.Group1week1.GetAudience() ?? "";
            dataGrid[3, ind].Value = scheduleRow?.Group2week1.GetAudience() ?? "";
            dataGrid[0, ind + 1].Value = scheduleRow?.Group1week1.GetAudience() ?? "";
            dataGrid[3, ind + 1].Value = scheduleRow?.Group2week1.GetAudience() ?? "";
        }

        public static void UpdateDataGrid(this DataGridView dataGrid,  SGroup group)
        {
            for (int i = 0; i < group.Count(); i++)
            {
                dataGrid.FillRow(i, group[i]);
                dataGrid.VisualizeRow(i, group[i]?.RowType ?? RowTypes.Simple);
            }
        }
        static void ColorRow(this DataGridView dataGrid, int ind, Color color)
        {
            dataGrid[0, ind].Style.BackColor = color;
            dataGrid[0, ind + 1].Style.BackColor = color;
            dataGrid[1, ind].Style.BackColor = color;
            dataGrid[1, ind + 1].Style.BackColor = color;
            dataGrid[2, ind].Style.BackColor = color;
            dataGrid[2, ind + 1].Style.BackColor = color;
            dataGrid[3, ind].Style.BackColor = color;
            dataGrid[3, ind + 1].Style.BackColor = color;
        }
        static void Discolor(this DataGridView dataGrid)
        {
            for (int i = 0; i < dataGrid.RowCount; i++)
            {
                for (int r = 0; r < dataGrid.ColumnCount; r++)
                {
                    dataGrid[r, i].Style.BackColor = Color.White;
                }
            }
        }
        /// <summary>
        /// подсветка(подсказка) для заполнения расписания
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="row"> номер строки в расписании</param>
        /// <param name="academicClass">предполагаемая пара для вставки</param>
        public static void HighlightRow(this DataGridView dataGrid, int row, int col, AcademicClass academicClass)
        {
            dataGrid.Discolor();
            Color colorLight = Color.FromArgb(135, 206, 250);
            Color colorDark = Color.FromArgb(37, 165, 245);
            ToSimpleView(dataGrid, row - row % 2);
            dataGrid.ColorRow(row - row % 2, colorLight);
            if (academicClass.Type == ClassTypes.Lecture)
            {
                if (academicClass.Hours <= 36) // раз в 2 недели
                {
                    ToTwoWeeks(dataGrid, row - row % 2);
                    dataGrid[0, row].Style.BackColor = colorDark;
                    dataGrid[3, row].Style.BackColor = colorDark;

                }
                else
                {
                    dataGrid[0, row - row % 2].Style.BackColor = colorDark;
                    dataGrid[3, row - row % 2].Style.BackColor = colorDark;
                }
            }
            else
            {
                if(academicClass.SubGroup == SubGroups.First)
                {
                    if (academicClass.Hours <= 36) // раз в 2 недели
                    {
                        ToTwoGroupsAndTwoWeeks(dataGrid, row - row % 2);
                        dataGrid[0, row].Style.BackColor = colorDark;
                        dataGrid[3, row].Style.BackColor = colorDark;
                        dataGrid[0, row].Style.BackColor = colorDark;
                        dataGrid[3, row].Style.BackColor = colorDark;
                    }
                    else
                    {
                        ToTwoGrops(dataGrid, row - row % 2);
                        int anotherCol;
                        if(col > 1)
                        {
                            anotherCol = 1 + 2 - col % 2;
                        }
                        else
                        {
                            anotherCol = 1 - col;
                        }
                        dataGrid[col, row - row % 2].Style.BackColor = colorDark;
                        dataGrid[anotherCol, row - row % 2].Style.BackColor = colorDark;
                    }
                }
                else if(academicClass.SubGroup == SubGroups.Second)
                {

                }
            }
        }
    }

}