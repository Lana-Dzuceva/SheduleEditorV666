using System;
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
        Schedule scheduleData;
        FacultyGroups facultyGroups;
        public List<TeacherPreference> teacherPreferences;
        string activGroupeTitle;
        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            scheduleData = new Schedule();
            facultyGroups = JsonConvert.DeserializeObject<FacultyGroups>(File.ReadAllText(Environment.CurrentDirectory + @"\..\..\..\qqq.json"));
            teacherPreferences = JsonConvert.DeserializeObject<List<TeacherPreference>>(File.ReadAllText(Environment.CurrentDirectory + @"\..\..\..\teachers1.json"));

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
            listViewErrors.MouseDoubleClick += new MouseEventHandler(listViewErrors_MouseDoubleClick);
            tabControlGroups.SelectedIndexChanged += new EventHandler(tabControlGroups_SelectedIndexChanged);
            BuildSchedule();
            dataGridViewSchedule.UpdateDataGrid(scheduleData[activGroupeTitle]);
            BuildAndFillLessons();
            FillErrors();
        }

        public void BuildSchedule()
        {
            //dataGridViewSchedule.Height = this.Height - 40;
            //dataGridViewSchedule.Width  = this.Width;
            //dataGridViewSchedule.RowTemplate.Height = 23;
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
            dataGridViewSchedule.DefaultCellStyle.SelectionBackColor = dataGridViewSchedule.DefaultCellStyle.BackColor;
            dataGridViewSchedule.DefaultCellStyle.SelectionForeColor = dataGridViewSchedule.DefaultCellStyle.ForeColor;

        }
        #region
        //public void BuildLessonsTabPages()
        //{
        //    TabPage tabPage;
        //    for (int r = 0; r < 5; r++)
        //    {
        //        tabPage = new TabPage("hmm");
        //        tabControlGroups.Controls.Add(tabPage);
        //    }

        //    for (int r = 0; r < tabControlGroups.Controls.Count; r++)
        //    {
        //        ListView listViewSubjects = new ListView();
        //        listViewSubjects.View = View.Details;
        //        //listViewSubjects.BackColor = Color.Red;
        //        tabControlGroups.Controls[r].Controls.Add(listViewSubjects);
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
            listViewSubjects.Dock = DockStyle.Fill;
            listViewSubjects.Columns.Add("Дисциплина");
            listViewSubjects.Columns.Add("Преподователь");
            listViewSubjects.Columns.Add("Тип занятия");
            listViewSubjects.Columns.Add("Кол-во часов");
            listViewSubjects.Columns.Add("Подгруппа");
            listViewSubjects.Columns[0].Width = 220;
            listViewSubjects.Columns[1].Width = 150;
            listViewSubjects.Columns[2].Width = 150;
            listViewSubjects.Columns[3].Width = 150;
            listViewSubjects.Columns[4].Width = 150;
            listViewSubjects.Font = new Font(FontFamily.GenericSansSerif, 12);
            tabPage.Controls.Add(listViewSubjects);
            return tabPage;
        }
        public void BuildAndFillLessons()
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
                    lvi.SubItems.Add(acadClass.SubGroup.ToString());
                    (tabPage.Controls[0] as ListView).Items.Add(lvi);
                    lvi.Tag = acadClass;
                }
                (tabPage.Controls[0] as ListView).MouseDown += new System.Windows.Forms.MouseEventHandler(ListViewItem_MouseDown);
                (tabPage.Controls[0] as ListView).MultiSelect = false;
                tabControlGroups.Controls.Add(tabPage);
            }
        }

        public void FillErrors()
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
        //public void GenerateTeachers()
        //{
        //    Random random = new Random();
        //    var prefs = new List<TeacherPreference>();
        //    for (int i = 0; i < 30; i++)
        //    {
        //        var pref = new TeacherPreference($"Teacher {i}");
        //        for (int r = 0; r < 3; r++)
        //        {
        //            pref.Preferences.Add(new Preference((WeekDays)random.Next(7), random.Next(1, 5)));
        //        }
        //        prefs.Add(pref);
        //    }
        //    File.WriteAllText("teachers2.json", JsonConvert.SerializeObject(prefs));
        //}

        private void TeacherPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormTeacherPreferences(this);
            f.Show();
        }

        private void ListViewItem_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //DataGridView.HitTestInfo info = .HitTest(e.X, e.Y);
                //string aud = dataGridViewAudience[info.ColumnIndex, info.RowIndex].Value.ToString();
                var lv = sender as ListView;
                var lvi = lv.GetItemAt(e.X, e.Y);
                lv.DoDragDrop(lvi.Tag, DragDropEffects.Move);
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
            //var li = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;
            var acadClass = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;
            int a = 0;
            
        }

        private void listViewErrors_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dataGridViewSchedule_DragOver(object sender, DragEventArgs e)
        {
            var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            if (info.RowIndex == -1) return;
            //get_row_col(e.X, e.Y);
            //dataGridViewSchedule[info.ColumnIndex, info.RowIndex].Value = "hmm";
            listViewErrors.Items[0].SubItems[0].Text = $"row {info.RowIndex} col {info.ColumnIndex}";

            dataGridViewSchedule.HighlightRow(info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass);
        }

        private void dataGridViewSchedule_DragLeave(object sender, EventArgs e)
        {
            dataGridViewSchedule.UpdateDataGrid(scheduleData[activGroupeTitle]);
        }

        //void get_row_col(int x, int y)
        //{
        //    var info = dataGridViewSchedule.HitTest(x, y);
        //    Point locationOnForm = this.PointToClient(    dataGridViewSchedule.Parent.PointToScreen(dataGridViewSchedule.Location));
        //    y -= locationOnForm.Y;
        //    int row = (int)Math.Ceiling((double)y / dataGridViewSchedule.RowTemplate.Height);
        //    listViewErrors.Items[0].SubItems[0].Text = $"row {info.RowIndex} col {info.ColumnIndex}";
        //    listViewErrors.Items[1].SubItems[0].Text = $"row {row} col -6-{locationOnForm.Y}";

        //}
        private void listViewErrors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var a = listViewErrors.FocusedItem;
        }
        private void tabControlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            activGroupeTitle = tabControlGroups.SelectedTab.Text;
            MessageBox.Show(activGroupeTitle);
        }
    }
}
