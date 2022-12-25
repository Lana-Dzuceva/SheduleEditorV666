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

namespace SheduleEditorV6
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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
            DrawSchedule();
            BuildLessonsTabPages();
            DrawLessons();
            DrawErrors();
            


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

            int w = dataGridViewSchedule.Width - dataGridViewSchedule.RowHeadersWidth;
            dataGridViewSchedule.Columns[0].Width = (int)(0.1 * w);
            dataGridViewSchedule.Columns[1].Width = (int)(0.4 * w);
            dataGridViewSchedule.Columns[2].Width = (int)(0.4 * w);
            dataGridViewSchedule.Columns[3].Width = (int)(0.1 * w);
            string[] weekDays = { "Пн", "Вт", "Ср", "Чт", "Пт" };
            for (int i = 0; i < 20; i += 4)
            {
                dataGridViewSchedule.Rows[i].HeaderCell.Value = weekDays[i / 4];
            }
            for (int i = 0; i < dataGridViewSchedule.Columns.Count; i++)
            {
                dataGridViewSchedule.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            
        }
        public void DrawSchedule()
        {

        }
        public void BuildLessonsTabPages()
        {
            TabPage tabPage;
            for (int i = 0; i < 5; i++)
            {
                tabPage = new TabPage("hmm");
                tabControlGroups.Controls.Add(tabPage);
            }

            for (int i = 0; i < tabControlGroups.Controls.Count; i++)
            {
                ListView listViewSubjects = new ListView();
                listViewSubjects.View = View.Details;
                //listViewSubjects.BackColor = Color.Red;
                tabControlGroups.Controls[i].Controls.Add(listViewSubjects);
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
                ListViewItem lvi = new ListViewItem("q");
                lvi.SubItems.Add("w");
                lvi.SubItems.Add("e");
                lvi.SubItems.Add(i.ToString());
                listViewSubjects.Items.Add(lvi);
            }
            
        }
        public void DrawLessons()
        {
            
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

       

        private void TeacherPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hmm");
            var f = new FormTeacherPreferences();
            f.Show();
        }
    }
}
