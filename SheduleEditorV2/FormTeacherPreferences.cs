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
        List<TeacherPreference> prefs;
        public FormTeacherPreferences()
        {
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
            prefs = JsonConvert.DeserializeObject<List<TeacherPreference>>(File.ReadAllText(Environment.CurrentDirectory + @"\..\..\..\teachers2.json"));
            foreach (var teacher in prefs)
            {
                listView1.Items.Add(new ListViewItem(teacher.Name));
                foreach (var pref in teacher.Preferences)
                {
                    (dataGridViewTable[(int)pref.WeekDay, pref.LessonNumber - 1].Tag as List<string>).Add(teacher.Name);
                }
            }
            Update();
        }


        public void Update()
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
        }
        public void save()
        {
            foreach (var teacher in prefs)
            {
                teacher.Preferences.Clear();
            }
            for (int i = 0; i < dataGridViewTable.ColumnCount; i++)
            {
                for (int r = 0; r < dataGridViewTable.RowCount; r++)
                {
                    
                    foreach (var name in dataGridViewTable[i, r].Tag as List<string>)
                    {
                        //prefs[prefs.FindIndex(pref => pref.Name == name)].Preferences.Add()
                        

                        
                    }
                }
            }

        }
        private void FormTeacherPreferences_Load(object sender, EventArgs e)
        {
            //hmm();
            listView1.Items.Cast<List<ListViewItem>>();
        }

        private void dataGridViewTable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var info = dataGridViewTable.HitTest(e.X, e.Y);
            var form = new FormEditTPCell(dataGridViewTable[info.ColumnIndex, info.RowIndex], prefs);
            form.Show();
            Update();
        }
    }
}
