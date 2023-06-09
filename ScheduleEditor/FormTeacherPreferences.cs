using Newtonsoft.Json;
using ScheduleEditorClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheduleEditorV6
{
    public partial class FormTeacherPreferences : Form
    {
        List<Teacher> teachers;
        FormMain formMain;
        public FormTeacherPreferences(FormMain formMain)
        {
            this.formMain = formMain;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewTable.RowTemplate.Height = 100;
            for (int i = 0; i < 7; i++)
            {
                dataGridViewTable.Columns.Add(new SpannedDataGridView.DataGridViewTextBoxColumnEx());
                
            }
            dataGridViewTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewTable.RowCount = 4;
            dataGridViewTable.ColumnHeadersHeight = 40;
            dataGridViewTable.DefaultCellStyle.SelectionBackColor = dataGridViewTable.DefaultCellStyle.BackColor;
            dataGridViewTable.DefaultCellStyle.SelectionForeColor = dataGridViewTable.DefaultCellStyle.ForeColor;
            dataGridViewTable.MouseDoubleClick += new MouseEventHandler(dataGridViewTable_MouseDoubleClick);
            string[] weekDays = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вск" };
            for (int i = 0; i < 7; i++)
            {
                dataGridViewTable.Columns[i].HeaderCell.Value = weekDays[i];
                for (int r = 0; r < dataGridViewTable.RowCount; r++)
                {
                    dataGridViewTable[i, r].Tag = new List<string>();
                    dataGridViewTable[i, r].Value = "";
                }
            }
            for (int i = 0; i < dataGridViewTable.RowCount; i++)
            {
                dataGridViewTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            teachers = JsonConvert.DeserializeObject<List<Teacher>>(File.ReadAllText(Environment.CurrentDirectory + @"\..\..\..\teachers1.json"));
            foreach (var teacher in teachers)
            {
                listViewTeachers.Items.Add(new ListViewItem(teacher.Name));
                foreach (var pref in teacher.Preferences)
                {
                    (dataGridViewTable[(int)pref.WeekDay, pref.LessonNumber - 1].Tag as List<string>).Add(teacher.Name);
                }
            }
            UpdateDGV();
        }


        public void UpdateDGV()
        {
            for (int i = 0; i < dataGridViewTable.ColumnCount; i++)
            {
                for (int r = 0; r < dataGridViewTable.RowCount; r++)
                {
                    dataGridViewTable[i, r].Value = "";
                    foreach (var name in dataGridViewTable[i, r].Tag as List<string>)
                    {
                        dataGridViewTable[i, r].Value += name + '\n';
                    }
                }
            }
            dataGridViewTable.Update();
            dataGridViewTable.Refresh();
            //this.Refresh();
        }
        public void Save()
        {
            foreach (var teacher in teachers)
            {
                teacher.Preferences.Clear();
            }
            for (int i = 0; i < dataGridViewTable.ColumnCount; i++)
            {
                for (int r = 0; r < dataGridViewTable.RowCount; r++)
                {
                    foreach (var name in dataGridViewTable[i, r].Tag as List<string>)
                    {
                        teachers[teachers.FindIndex(pref => pref.Name == name)].Preferences.Add(new TeacherPreference((DayOfWeek)(i), r + 1));
                    }
                }
            }
            File.WriteAllText(Environment.CurrentDirectory + @"\..\..\..\teachers1.json", JsonConvert.SerializeObject(teachers));
        }
        private void FormTeacherPreferences_Load(object sender, EventArgs e)
        {
            //hmm();
            listViewTeachers.Items.Cast<List<ListViewItem>>();
        }

        private void dataGridViewTable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var info = dataGridViewTable.HitTest(e.X, e.Y);
            var form = new FormEditTPCell(teachers.Select(pref => pref.Name).ToList(), dataGridViewTable, info.RowIndex, info.ColumnIndex, this);
            form.Show();
        }

        private void FormTeacherPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            formMain.teachers = teachers;
        }
    }
}
