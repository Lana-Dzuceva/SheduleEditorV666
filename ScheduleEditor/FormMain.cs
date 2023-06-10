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
using System.Runtime.InteropServices.ComTypes;

namespace SheduleEditorV6
{
    public partial class FormMain : Form
    {
        FacultyGroups facultyGroups; // данные на входе
        Schedule schedule; // данные в процессе
        public List<Teacher> teachers; //учителя
        string activeGroupeTitle;
        public List<Audience> audiences;
        string curDir = Environment.CurrentDirectory;
        List<ScheduleError> errors;
        bool isFormLoaded;
        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            facultyGroups = JsonConvert.DeserializeObject<FacultyGroups>(File.ReadAllText(curDir + @"\..\..\..\schedule_in.json"));
            teachers = JsonConvert.DeserializeObject<List<Teacher>>(File.ReadAllText(curDir + @"\..\..\..\teachers_prefs.json"));
            schedule = new Schedule(facultyGroups.Groups.Select(group => group.Title).ToList());
            using (StreamReader file = new StreamReader(curDir + @"\..\..\..\audiences.json"))
            {
                audiences = JsonConvert.DeserializeObject<List<Audience>>(file.ReadToEnd());
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            errors = new List<ScheduleError>();
            listViewErrors.Columns.Add("№");
            listViewErrors.Columns.Add("Тип ошибки");
            listViewErrors.Columns.Add("Группа");
            listViewErrors.Columns.Add("Сообщение");
            listViewErrors.Font = new Font(FontFamily.GenericSansSerif, 12);
            for (int i = 0; i < 4; i++)
            {
                listViewErrors.Columns[i].Width = 140;
            }
            listViewErrors.FullRowSelect = true;
            listViewErrors.MouseDoubleClick += new MouseEventHandler(listViewErrors_MouseDoubleClick);
            tabControlGroups.SelectedIndexChanged += new EventHandler(tabControlGroups_SelectedIndexChanged);
            BuildSchedule();
            BuildAndFillLessons();
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);

            UpdateErrors();
            isFormLoaded = true;
        }

        public void BuildSchedule()
        {
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
            ListView listViewSubjects = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill
            };
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
                if (activeGroupeTitle == null)
                {
                    activeGroupeTitle = group.Title;
                }
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
                (tabPage.Controls[0] as ListView).FullRowSelect = true;
                tabControlGroups.Controls.Add(tabPage);
            }
        }

        public void UpdateErrors()
        {
            listViewErrors.Items.Clear();
            ListViewItem lvi;
            for (int i = 0; i < 1; i++)
            {
                lvi = new ListViewItem("Error");
                lvi.SubItems.Add("very");
                lvi.SubItems.Add("bad");
                lvi.SubItems.Add(i.ToString());
                //listViewErrors.ContextMenuStrip = new ContextMenuStrip();

                listViewErrors.Items.Add(lvi);
            }
            for (int i = 0; i < errors.Count; i++)
            {
                lvi = new ListViewItem((i + 1).ToString());
                lvi.SubItems.Add(errors[i].Type.ToString());
                lvi.SubItems.Add(errors[i].GroupTitle);
                lvi.SubItems.Add(errors[i].Message);
                //listViewErrors.ContextMenuStrip = new ContextMenuStrip();
                lvi.Tag = errors[i];
                lvi.BackColor = Color.FromArgb(247, 193, 188);
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

        void save()
        {
            File.WriteAllText(Environment.CurrentDirectory + @"\..\..\..\schedule_temp.json", JsonConvert.SerializeObject(schedule));
        }

        private void TeacherPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormTeacherPreferences(this);
            f.Show();
        }

        private void ListViewItem_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var lv = sender as ListView;
                var lvi = lv.GetItemAt(e.X, e.Y);
                lv.DoDragDrop(lvi.Tag, DragDropEffects.Move);
            }
            catch (Exception)
            { }
            dataGridViewSchedule.Discolor();
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            checkErrors();
        }

        private void dataGridViewSchedule_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.Text))
            //    e.Effect = DragDropEffects.Copy;
            //else
            //    e.Effect = DragDropEffects.None;
            e.Effect = DragDropEffects.Move;
            var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            if (info.RowIndex == -1) return;
        }

        void checkErrors()
        {
            var newErrors = new List<ScheduleError>();
            for (int i = 0; i < errors.Count; i++)
            {
                var res = schedule.IsTeacherAvaible(errors[i].GroupTitle,
                    ((int)errors[i].ScheduleRow.WeekDay - 1) * 8 + errors[i].row,
                    errors[i].col, errors[i].ScheduleRow[errors[i].col, errors[i].row],
                    teachers.Where(teacher => teacher.Name == errors[i].ScheduleRow[errors[i].col, errors[i].row].Teacher.Name).First());
                if (res == errors[i].Type)
                {
                    newErrors.Add(errors[i]);
                    dataGridViewSchedule.HighlightError(errors[i]);
                }
            }
            errors = newErrors;
        }
        private void dataGridViewSchedule_DragDrop(object sender, DragEventArgs e)
        {
            var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            if (info.RowIndex == -1) return;
            var res = schedule.IsTeacherAvaible(activeGroupeTitle, info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass,
                teachers.Where(teacher => teacher.Name == (e.Data.GetData(typeof(AcademicClass)) as AcademicClass).Teacher.Name).First());
            //var res2 = MessageBox.Show("Препод занят. Все равно добавить?", "Предупреждение", MessageBoxButtons.YesNo);
            if (res != Results.TypeMismatch)
            {
                var f = new FormChooseAudience(this);

                f.ShowDialog();
                int aud;
                Results res2 = Results.Available;
                if (f.DialogResult == DialogResult.OK)
                {
                    aud = f.num;
                    res2 = schedule.IsAudienceAvaible(activeGroupeTitle, info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass, aud);

                }
                else
                {
                    aud = 0;
                }

                schedule.PutData(activeGroupeTitle, info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass, aud);
                if (res != Results.Available)
                {
                    var weekDay = (DayOfWeek)(info.RowIndex / 8 + 1);
                    var сlassNumber = (info.RowIndex - 2 - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
                    errors.Add(new ScheduleError(res, activeGroupeTitle, schedule[activeGroupeTitle][weekDay, сlassNumber], info.ColumnIndex / 2 + 1, info.RowIndex % 2 + 1, "ААА матан захватит мир!"));
                    UpdateErrors();
                }
                if (res2 != Results.Available)
                {
                    var weekDay = (DayOfWeek)(info.RowIndex / 8 + 1);
                    var сlassNumber = (info.RowIndex - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
                    errors.Add(new ScheduleError(res2, activeGroupeTitle, schedule[activeGroupeTitle][weekDay, сlassNumber], info.ColumnIndex / 2 + 1, info.RowIndex % 2 + 1, "ААА матан захватит мир!"));
                    UpdateErrors();
                }
                checkErrors();
                dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
                save();
            }
            listViewErrors.Items[0].SubItems[1].Text = $"r{info.RowIndex} c{info.ColumnIndex} res{res.ToString()}";
        }

        private void listViewErrors_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dataGridViewSchedule_DragOver(object sender, DragEventArgs e)
        {
            var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            if (info.RowIndex == -1) return;
            listViewErrors.Items[0].SubItems[0].Text = $"row {info.RowIndex} col {info.ColumnIndex}";
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            // уберу весь текст для красивой подсветки
            var row = info.RowIndex - 2 - info.RowIndex % 2;
            dataGridViewSchedule[0, row].Value = "";
            dataGridViewSchedule[0, row + 1].Value = "";
            dataGridViewSchedule[1, row].Value = "";
            dataGridViewSchedule[1, row + 1].Value = "";
            dataGridViewSchedule[2, row].Value = "";
            dataGridViewSchedule[2, row + 1].Value = "";
            dataGridViewSchedule[3, row].Value = "";
            dataGridViewSchedule[3, row + 1].Value = "";
            dataGridViewSchedule.HighlightRow(info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass);
        }

        private void dataGridViewSchedule_DragLeave(object sender, EventArgs e)
        {
            dataGridViewSchedule.Discolor();
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            checkErrors();
        }
        #region моя функция get info
        //void get_row_col(int x, int y)
        //{
        //    var info = dataGridViewSchedule.HitTest(x, y);
        //    Point locationOnForm = this.PointToClient(    dataGridViewSchedule.Parent.PointToScreen(dataGridViewSchedule.Location));
        //    y -= locationOnForm.Y;
        //    int row = (int)Math.Ceiling((double)y / dataGridViewSchedule.RowTemplate.Height);
        //    listViewErrors.Items[0].SubItems[0].Text = $"row {info.RowIndex} col {info.ColumnIndex}";
        //    listViewErrors.Items[1].SubItems[0].Text = $"row {row} col -6-{locationOnForm.Y}";

        //}
        #endregion
        private void listViewErrors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var cur_item = listViewErrors.FocusedItem;
            activeGroupeTitle = (cur_item.Tag as ScheduleError).GroupTitle;
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);

        }
        private void tabControlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeGroupeTitle = tabControlGroups.SelectedTab.Text;
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
        }

        private void dataGridViewSchedule_MouseUp(object sender, MouseEventArgs e)
        {
            //var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            //if (info.RowIndex == -1) return;
            //var res = schedule.IsTeacherAvaible(activeGroupeTitle, info.RowIndex, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass);
            //var res2 = MessageBox.Show("Препод занят. Все равно добавить?", "Предупреждение", MessageBoxButtons.YesNo);
            //MessageBox.Show(res.ToString() + ' ' + res2.ToString());
        }

        private void dataGridViewSchedule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if(isFormLoaded)
            //save();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 1;
            //openFileDialog1.RestoreDirectory = true;

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    MessageBox.Show(openFileDialog1.FileName);
            //}
            MessageBox.Show("Вы успешно загрузили данные!");
        }

        private void новоеРасписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 1;
            //openFileDialog1.RestoreDirectory = true;
            //openFileDialog1.AddExtension = true;
            //openFileDialog1.Title = "Создайте файл";
            ////CreateFi
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    MessageBox.Show(openFileDialog1.FileName);
            //}
            #region диалог
            //SaveFileDialog saveFileDialog = new SaveFileDialog();

            //// Настройка параметров диалогового окна
            //saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            //saveFileDialog.Title = "Создать файл";
            //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //// Отображение диалогового окна и обработка результата
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string filePath = saveFileDialog.FileName;
            //    //В этом месте вы можете использовать filePath для создания файла или выполнения нужных действий
            //    // например, можно использовать StreamWriter для записи в файл
            //    using (StreamWriter writer = new StreamWriter(filePath))
            //    {
            //        writer.WriteLine("Пример текста");
            //    }
            //}
            #endregion
            MessageBox.Show("Это конечно можно, но не сейчас.");
        }

        private void dataGridViewSchedule_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var cell = (sender as DataGridView).CurrentCell;
            var a = cell.RowIndex;
            var b = cell.ColumnIndex;
            var weekDay = (DayOfWeek)(a / 8 + 1);
            var сlassNumber = (a - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
            schedule[activeGroupeTitle][weekDay, сlassNumber].ClearCell(1 + b < 2 ? 0 : 1, 1 + a % 2);
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
        }

        private void dataGridViewSchedule_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Расписание отправлено в БД!");
        }

        private void хммToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormSurprise();
            f.Show();
        }
    }
}
