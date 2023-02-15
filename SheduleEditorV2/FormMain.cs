﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpannedDataGridView;
using ScheduleEditorClassLibrary;
using Newtonsoft.Json;
using System.IO;

namespace SheduleEditorV6
{
    public partial class FormMain : Form
    {
        ScheduleOne scheduleData;
        FacultyGroups facultyGroups;
        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            scheduleData = new ScheduleOne();
            facultyGroups = JsonConvert.DeserializeObject<FacultyGroups>(File.ReadAllText("qqq.json"));
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            listViewErrors.Columns.Add("Тип ошибки");
            listViewErrors.Columns.Add("Сведения");
            listViewErrors.Columns.Add("Еще сведелния");
            listViewErrors.Columns.Add("Какая-то цифра");
            listViewErrors.Font = new Font(FontFamily.GenericSansSerif, 12);
            for (int i = 0; i < 4; i++)
            {
                listViewErrors.Columns[i].Width = 140;
            }


            BuildSchedule();
            dataGridViewSchedule.UpdateDataGrid(scheduleData);
            //BuildLessonsTabPages();
            DrawLessons();
            DrawErrors();
            //GenerateData();
        }

        public void BuildSchedule()
        {
            //dataGridViewSchedule.Height = this.Height - 40;
            //dataGridViewSchedule.Width  = this.Width;
            dataGridViewSchedule.RowTemplate.Height = 23;
            for (int i = 0; i < 4; i++)
            {
                dataGridViewSchedule.Columns.Add(new SpannedDataGridView.DataGridViewTextBoxColumnEx());
            }
            dataGridViewSchedule.RowCount = 40;
            dataGridViewSchedule.ColumnHeadersHeight = 40;
            dataGridViewSchedule.RowHeadersWidth = 60;

            int w = dataGridViewSchedule.Width - dataGridViewSchedule.RowHeadersWidth;
            dataGridViewSchedule.Columns[0].Width = (int)(0.1 * w);
            dataGridViewSchedule.Columns[1].Width = (int)(0.4 * w);
            dataGridViewSchedule.Columns[2].Width = (int)(0.4 * w);
            dataGridViewSchedule.Columns[3].Width = (int)(0.1 * w);
            string[] weekDays = { "Пн", "Вт", "Ср", "Чт", "Пт" };
            for (int i = 0; i < 5; i++)
            {
                dataGridViewSchedule.Rows[i * 8].HeaderCell.Value = weekDays[i];
            }
            for (int i = 0; i < dataGridViewSchedule.Columns.Count; i++)
            {
                dataGridViewSchedule.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #region
        //public void BuildLessonsTabPages()
        //{
        //    TabPage tabPage;
        //    for (int i = 0; i < 5; i++)
        //    {
        //        tabPage = new TabPage("hmm");
        //        tabControlGroups.Controls.Add(tabPage);
        //    }

        //    for (int i = 0; i < tabControlGroups.Controls.Count; i++)
        //    {
        //        ListView listViewSubjects = new ListView();
        //        listViewSubjects.View = View.Details;
        //        //listViewSubjects.BackColor = Color.Red;
        //        tabControlGroups.Controls[i].Controls.Add(listViewSubjects);
        //        listViewSubjects.Dock = DockStyle.Fill;
        //        listViewSubjects.Columns.Add("Дисциплина");
        //        listViewSubjects.Columns.Add("Преподователь");
        //        listViewSubjects.Columns.Add("Тип занятия");
        //        listViewSubjects.Columns.Add("Кол-во часов");
        //        listViewSubjects.Columns[0].Width = 220;
        //        listViewSubjects.Columns[1].Width = 150;
        //        listViewSubjects.Columns[2].Width = 150;
        //        listViewSubjects.Columns[3].Width = 150;
        //        listViewSubjects.Font = new Font(FontFamily.GenericSansSerif, 12);

        //    }
        //}
        #endregion
        public TabPage MakeClassesTabPage(string title = "")
        {
            TabPage tabPage = new TabPage(title);
            ListView listViewSubjects = new ListView();
            listViewSubjects.View = View.Details;
            tabPage.Controls.Add(listViewSubjects);
            listViewSubjects.Dock = DockStyle.Fill;
            listViewSubjects.Columns.Add("Дисциплина");
            listViewSubjects.Columns.Add("Преподователь");
            listViewSubjects.Columns.Add("Тип занятия");
            listViewSubjects.Columns.Add("Кол-во часов");
            listViewSubjects.Columns[0].Width = 220;
            listViewSubjects.Columns[1].Width = 150;
            listViewSubjects.Columns[2].Width = 150;
            listViewSubjects.Columns[3].Width = 150;
            listViewSubjects.Font = new Font(FontFamily.GenericSansSerif, 12);
            return tabPage;
        }
        public void DrawLessons()
        {
            TabPage tabPage;
            foreach (var group in facultyGroups.Groups)
            {
                tabPage = MakeClassesTabPage(group.Title);
                foreach (var acadClass in group.Classes)
                {
                    ListViewItem lvi = new ListViewItem(acadClass.ClassTitle);
                    lvi.SubItems.Add(acadClass.Teacher.ToString());
                    lvi.SubItems.Add(acadClass.Type.ToString());
                    lvi.SubItems.Add(acadClass.Hours.ToString());
                    (tabPage.Controls[0] as ListView).Items.Add(lvi);
                }
                tabControlGroups.Controls.Add(tabPage);
            }
        }

        public void DrawErrors()
        {
            ListViewItem lvi;
            for (int i = 0; i < 10; i++)
            {
                lvi = new ListViewItem("Error");
                lvi.SubItems.Add("very");
                lvi.SubItems.Add("bad");
                lvi.SubItems.Add(i.ToString());
                listViewErrors.Items.Add(lvi);
            }
        }
        public void DrawSchedule()
        {

        }
        public void GenerateData()
        {
            var facultyGroups = new FacultyGroups();
            Teacher teacher;
            AcademicClass academClass; ;
            for (int i = 0; i < 20; i++)
            {
                var group = new Group($"Группа {i + 1}");

                for (int r = 0; r < 5; r++)
                {
                    teacher = new Teacher($"Учитель{i}{r}");
                    academClass = new AcademicClass($"Пара{r}", teacher, 72, ClassTypes.Practice, SubGroups.First);
                    group.Add(academClass);
                }
                for (int r = 0; r < 5; r++)
                {
                    teacher = new Teacher($"Учитель{i}{r + 5}");
                    academClass = new AcademicClass($"Пара{r + 5}", teacher, 72, ClassTypes.Lecture, SubGroups.Second);
                    group.Add(academClass);
                }
                teacher = new Teacher($"Учитель{i}{11}");
                academClass = new AcademicClass($"Пара{12}", teacher, 36, ClassTypes.Lecture, SubGroups.Second);
                group.Add(academClass);
                facultyGroups.Add(group);
            }

            File.WriteAllText("qqq.json", JsonConvert.SerializeObject(facultyGroups));
        }

        private void TeacherPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormTeacherPreferences();
            f.Show();
        }

        private void ListViewItem_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //DataGridView.HitTestInfo info = .HitTest(e.X, e.Y);
                //string aud = dataGridViewAudience[info.ColumnIndex, info.RowIndex].Value.ToString();
                //ShowAudienceDescription(aud);
                //dataGridViewShedule.DoDragDrop(aud, DragDropEffects.Copy);
                var lv = (tabControlGroups.SelectedTab.Controls[0] as ListView);
                var lvi = lv.CheckedItems[0];
                lv.DoDragDrop(lvi, DragDropEffects.Move);
                //int indexSource = listView.Items.IndexOf(listViewSubjects.GetItemAt(e.X, e.Y));
                //string s = ListViewItemToString(listViewSubjects.Items[indexSource]);
                //listViewSubjects.DoDragDrop(s, DragDropEffects.Copy);
            }
            catch (Exception)
            { }
        }

        private void dataGridViewSchedule_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.Text))
            //    e.Effect = DragDropEffects.Copy;
            //else
            //    e.Effect = DragDropEffects.None;
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridViewSchedule_DragDrop(object sender, DragEventArgs e)
        {
            var li = e.Data.GetData(DataFormats.FileDrop) as ListViewItem;

            //dataGridViewSchedule
        }
    }
}
