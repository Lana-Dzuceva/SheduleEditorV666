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
using System.Reflection;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SheduleEditorV6
{
    public partial class FormMain : Form
    {
        string connectionString;
        FacultyGroups facultyGroups; // данные на входе
        Schedule schedule; // данные в процессе
        public List<Teacher> teachers; //учителя
        string activeGroupeTitle;
        public List<Audience> audiences;
        string curDir = Environment.CurrentDirectory;
        List<ScheduleError> errors;
        ToolTip toolTipeqqq, toolTip;
        string curToolTipText = "";
        string lastGroupTitle;
        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            if (File.Exists(Environment.CurrentDirectory + @"\..\..\..\curAudience.txt"))
            {
                lastGroupTitle = File.ReadAllText(Environment.CurrentDirectory + @"\..\..\..\curAudience.txt");
            }
            //facultyGroups = JsonConvert.DeserializeObject<FacultyGroups>(File.ReadAllText(curDir + @"\..\..\..\schedule_in.json"));
            connectionString = "server=localhost;database=db;user id=root;password=1234";
            facultyGroups = new FacultyGroups();
            facultyGroups.Fill(connectionString);
            //var q = facultyGroups.Groups.Select(group => group.Classes.Select(class_ => class_.Teacher).ToList()).Where(teachers => teachers.Count > 0).SelectMany(x => x).ToHashSet().ToList();
            //var teachers = FacultyGroups.GetTeachers(connectionString);
            //File.WriteAllText(Environment.CurrentDirectory + @"\..\..\..\teachers_prefs.json", JsonConvert.SerializeObject(teachers));
            teachers = JsonConvert.DeserializeObject<List<Teacher>>(File.ReadAllText(curDir + @"\..\..\..\teachers_prefs.json"));
            if (File.Exists(Environment.CurrentDirectory + @"\..\..\..\schedule_temp.json"))
            {
                schedule = JsonConvert.DeserializeObject<Schedule>(File.ReadAllText(Environment.CurrentDirectory + @"\..\..\..\schedule_temp.json"));
            }
            else
            {
                schedule = new Schedule(facultyGroups.Groups.Select(group => group.Title).ToList());
            }
            using (StreamReader file = new StreamReader(curDir + @"\..\..\..\audiences.json"))
            {
                audiences = JsonConvert.DeserializeObject<List<Audience>>(file.ReadToEnd());
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            toolTip = new ToolTip(this.components);
            toolTip.AutoPopDelay = 5000; // Задержка автоматического закрытия подсказки (5 секунд)
            toolTip.InitialDelay = 500; // Задержка перед отображением подсказки (0,5 секунды)
            toolTip.ReshowDelay = 500; // Задержка перед повторным отображением подсказки (0,5 секунды)
                                       //toolTip.ShowAlways = true; // Подсказка будет отображаться всегда, даже если элемент не перекрыт

            this.toolTipeqqq = new System.Windows.Forms.ToolTip(this.components);

            errors = new List<ScheduleError>();
            listViewErrors.Columns.Add("№");
            listViewErrors.Columns.Add("Тип ошибки");
            listViewErrors.Columns.Add("Группа");
            listViewErrors.Columns.Add("Сообщение");
            listViewErrors.Font = new Font(FontFamily.GenericSansSerif, 12);
            for (int i = 0; i < 4; i++)
            {
                listViewErrors.Columns[i].Width = 90;
            }
            listViewErrors.Columns[1].Width = 260;
            listViewErrors.Columns[3].Width += listViewErrors.Width - listViewErrors.Columns.Cast<ColumnHeader>().Sum(column => column.Width);
            listViewErrors.FullRowSelect = true;
            listViewErrors.MouseDoubleClick += new MouseEventHandler(listViewErrors_MouseDoubleClick);
            tabControlGroups.SelectedIndexChanged += new EventHandler(tabControlGroups_SelectedIndexChanged);

            BuildSchedule();
            BuildAndFillLessons();
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);

            UpdateErrors();
            toolTipeqqq.SetToolTip(tabControlGroups.SelectedTab.Controls[0] as ListView, "???");
            toolTipeqqq.ShowAlways = true;
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
            listViewSubjects.MouseMove += ListViewSubjects_MouseMove;
            tabPage.Controls.Add(listViewSubjects);
            return tabPage;
        }
        bool IsAcademicClassUsed(AcademicClass academicClass, SGroup group)
        {
            return false; // потому что так надо
            foreach (var row in group.Rows)
            {
                if (!(row[1, 1] is null) && row[1, 1] == academicClass ||
                    row.CountOfWeeks == 2 && row[1, 2] == academicClass ||
                    row.CountOfGroups == 2 && row[2, 1] == academicClass ||
                    row.CountOfWeeks == 2 && row.CountOfGroups == 2 && row[2, 2] == academicClass)
                    return true;
            }
            return false;
        }
        void FillTabPage(TabPage tabPage, Group group)
        {
            (tabPage.Controls[0] as ListView).Items.Clear();
            foreach (var acadClass in group.Classes)
            {
                if (lastGroupTitle == group.Title && IsAcademicClassUsed(acadClass, schedule[lastGroupTitle]))
                    continue;
                ListViewItem lvi = new ListViewItem(acadClass.ClassTitle);
                lvi.SubItems.Add(acadClass.Teacher.ToString());
                lvi.SubItems.Add(acadClass.Type.GetDescription());
                lvi.SubItems.Add(acadClass.Hours.ToString());
                lvi.SubItems.Add(acadClass.SubGroup.GetDescription());
                (tabPage.Controls[0] as ListView).Items.Add(lvi);
                lvi.Tag = acadClass;
            }
        }
        void BuildAndFillLessons()
        {
            TabPage tabPage;
            foreach (var group in facultyGroups.Groups)
            {
                if (activeGroupeTitle == null)
                {
                    activeGroupeTitle = group.Title;
                }
                tabPage = MakeClassesTabPage(group.Title);
                FillTabPage(tabPage, group);
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
            for (int i = 0; i < errors.Count; i++)
            {
                lvi = new ListViewItem((i + 1).ToString());
                lvi.SubItems.Add(errors[i].Type.GetDescription());
                lvi.SubItems.Add(errors[i].GroupTitle);
                lvi.SubItems.Add(errors[i].Message);
                lvi.Tag = errors[i];
                lvi.BackColor = Color.FromArgb(247, 193, 188);
                listViewErrors.Items.Add(lvi);
            }
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
                var res = lv.DoDragDrop(lvi.Tag, DragDropEffects.Move);
                if (res != DragDropEffects.None)
                {
                    //lv.Items.Remove(lvi);
                    facultyGroups[activeGroupeTitle].Classes.Remove(lvi.Tag as AcademicClass);
                    //facultyGroups[activeGroupeTitle].Classes.Find()
                    
                    FillTabPage(tabControlGroups.SelectedTab, facultyGroups[activeGroupeTitle]);
                }
            }
            catch (Exception)
            { }
            dataGridViewSchedule.Discolor();
            //dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
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

        public void checkErrors()
        {
            var newErrors = new List<ScheduleError>();
            for (int i = 0; i < errors.Count; i++)
            {
                try
                {
                    var res = schedule.IsTeacherAvaible(errors[i].GroupTitle,
                        ((int)errors[i].ScheduleRow.WeekDay - 1) * 8 + errors[i].row,
                        errors[i].col, errors[i].ScheduleRow[errors[i].col, errors[i].row],
                        teachers.Find(teacher => teacher.Name == errors[i].ScheduleRow[errors[i].col, errors[i].row].Teacher.Name));
                    if (res == errors[i].Type)
                    {
                        newErrors.Add(errors[i]);
                        dataGridViewSchedule.HighlightError(errors[i]);
                    }
                }
                catch (Exception)
                { }
            }
            errors = new List<ScheduleError>(newErrors);
        }
        private void dataGridViewSchedule_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.None)
            {
            }
            var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            if (info.RowIndex == -1) return;
            var resTeacher = schedule.IsTeacherAvaible(activeGroupeTitle, info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass,
                teachers.Where(teacher => teacher.Name == (e.Data.GetData(typeof(AcademicClass)) as AcademicClass).Teacher.Name).First());
            //var resAudience = MessageBox.Show("Препод занят. Все равно добавить?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resTeacher != Results.TypeMismatch)
            {
                var f = new FormChooseAudience(this);

                f.ShowDialog();
                int aud;
                Results resAudience = Results.Available;
                if (f.DialogResult == DialogResult.OK)
                {
                    aud = f.num;
                    resAudience = schedule.IsAudienceAvaible(activeGroupeTitle, info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass, aud);
                }
                else
                {
                    aud = 0;
                }

                schedule.PutData(activeGroupeTitle, info.RowIndex - 2, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass, aud);
                if (resTeacher != Results.Available)
                {
                    var weekDay = (DayOfWeek)(info.RowIndex / 8 + 1);
                    var сlassNumber = (info.RowIndex - 2 - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
                    errors.Add(new ScheduleError(resTeacher, activeGroupeTitle, schedule[activeGroupeTitle][weekDay, сlassNumber], info.ColumnIndex / 2 + 1, info.RowIndex % 2 + 1, $"Ошибка в {weekDay} на {сlassNumber} паре")); ;
                    UpdateErrors();
                }
                if (resAudience != Results.Available)
                {
                    var weekDay = (DayOfWeek)(info.RowIndex / 8 + 1);
                    var сlassNumber = (info.RowIndex - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
                    errors.Add(new ScheduleError(resAudience, activeGroupeTitle, schedule[activeGroupeTitle][weekDay, сlassNumber], info.ColumnIndex / 2 + 1, info.RowIndex % 2 + 1, $"Ошибка в {weekDay} на {сlassNumber} паре"));
                    UpdateErrors();
                }
                checkErrors();
                dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
                save();
            }
            else
            {
                MessageBox.Show("Выбранная пара не относится к этой подгруппе");
            }
            //listViewErrors.Items[0].SubItems[1].Text = $"r{info.RowIndex} c{info.ColumnIndex} resTeacher {resTeacher.ToString()}";
        }

        private void listViewErrors_MouseDown(object sender, MouseEventArgs e)
        {
            //activeGroupeTitle = listViewErrors.GetItemAt(e.X, e.Y).SubItems[2].Text;
            //dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            //tabControlGroups.SelectedTab = tabControlGroups.TabPages.Cast<TabPage>().FirstOrDefault(tab => tab.Text == activeGroupeTitle);
        }

        private void dataGridViewSchedule_DragOver(object sender, DragEventArgs e)
        {
            var info = dataGridViewSchedule.HitTest(e.X, e.Y);
            if (info.RowIndex == -1) return;
            //listViewErrors.Items[0].SubItems[0].Text = $"row {info.RowIndex} col {info.ColumnIndex}";
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
            //var cur_item = listViewErrors.FocusedItem;
            //activeGroupeTitle = (cur_item.Tag as ScheduleError).GroupTitle;
            //dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            activeGroupeTitle = listViewErrors.GetItemAt(e.X, e.Y).SubItems[2].Text;
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            tabControlGroups.SelectedTab = tabControlGroups.TabPages.Cast<TabPage>().FirstOrDefault(tab => tab.Text == activeGroupeTitle);

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
            //var resTeacher = schedule.IsTeacherAvaible(activeGroupeTitle, info.RowIndex, info.ColumnIndex, e.Data.GetData(typeof(AcademicClass)) as AcademicClass);
            //var resAudience = MessageBox.Show("Препод занят. Все равно добавить?", "Предупреждение", MessageBoxButtons.YesNo);
            //MessageBox.Show(resTeacher.ToString() + ' ' + resAudience.ToString());
        }


        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            facultyGroups.Fill(connectionString);
            MessageBox.Show("Вы успешно загрузили данные!");
        }

        private void newScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            schedule.Clear();
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            MessageBox.Show("Это конечно можно и прямо сейчас.");
        }

        private void dataGridViewSchedule_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var cell = (sender as DataGridView).CurrentCell;
            var row = cell.RowIndex;
            var col = cell.ColumnIndex;
            var weekDay = (DayOfWeek)(row / 8 + 1);
            var сlassNumber = (row - ((int)weekDay - 1) * 8) / 2 + 1; // [1 - 4]
            var acadClass = schedule[activeGroupeTitle][weekDay, сlassNumber][1 + (col < 2 ? 0 : 1), 1 + (row % 2)];
            facultyGroups[activeGroupeTitle].Add(acadClass);
            FillTabPage(tabControlGroups.SelectedTab, facultyGroups[activeGroupeTitle]);
            
            //ListViewItem lvi = new ListViewItem(acadClass.ClassTitle);
            //lvi.SubItems.Add(acadClass.Teacher.ToString());
            //lvi.SubItems.Add(acadClass.Type.GetDescription());
            //lvi.SubItems.Add(acadClass.Hours.ToString());
            //lvi.SubItems.Add(acadClass.SubGroup.GetDescription());
            //lvi.Tag = acadClass;

            //(tabControlGroups.SelectedTab.Controls[0] as ListView).Items.Add(lvi);
            schedule[activeGroupeTitle][weekDay, сlassNumber].ClearCell(1 + (col < 2 ? 0 : 1), 1 + (row % 2));
            dataGridViewSchedule.UpdateDataGrid(schedule[activeGroupeTitle]);
            checkErrors();
            save();
        }

        private void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Когда-нибудь Стас напишет нормальную функцию отправки в БД!
            MessageBox.Show("Расписание отправлено в БД!");
        }

        private void hmmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormSurprise();
            f.Show();
        }
        #region another GetDescription
        //public static string GetDescription<T>(this T enumerationValue)
        //    where T : struct
        //{
        //    Type type = enumerationValue.GetType();
        //    if (!type.IsEnum)
        //    {
        //        throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
        //    }

        //    //Tries to find row DescriptionAttribute for row potential friendly name
        //    //for the enum
        //    MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
        //    if (memberInfo != null && memberInfo.Length > 0)
        //    {
        //        object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        //        if (attrs != null && attrs.Length > 0)
        //        {
        //            //Pull out the description value
        //            return ((DescriptionAttribute)attrs[0]).Description;
        //        }
        //    }
        //    //If we have no description attribute, just return the ToString of the enum
        //    return enumerationValue.ToString();
        //}
        #endregion

        private void ListViewSubjects_MouseMove(object sender, MouseEventArgs e)
        {
            // Получаем элемент, на котором находится курсор мыши
            ListViewItem item = (tabControlGroups.SelectedTab.Controls[0] as ListView).GetItemAt(e.X, e.Y);
            if (item != null && curToolTipText != item.SubItems[0].Text)
            {
                // Получаем значение первого SubItem
                curToolTipText = item.SubItems[0].Text;
                // Устанавливаем текст подсказки для элемента списка
                //toolTipeqqq.SetToolTip((tabControlGroups.SelectedTab.Controls[0] as ListView), curToolTipText);
                toolTipeqqq.Show(curToolTipText, (tabControlGroups.SelectedTab.Controls[0] as ListView));
                toolTipeqqq.ShowAlways = true;
            }
            else
            {
                // Если курсор мыши не наведен на элемент списка, очищаем текст подсказки
                //toolTipeqqq.SetToolTip((tabControlGroups.SelectedTab.Controls[0] as ListView), string.Empty);
            }
        }

        #region не работающие события мыши
        void listView_MouseHover(object sender, EventArgs e)
        {
            var lv = tabControlGroups.SelectedTab.Controls[0] as ListView;/*(sender as ListView).Get*/
            ListViewItem item = lv.GetItemAt(Cursor.Position.X, Cursor.Position.Y);
            if (item != null)
            {
                toolTipeqqq.SetToolTip(lv, item.SubItems[0].Text); //(lv, );
            }
        }

        private void ListViewItem_MouseEnter(object sender, EventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                // Получаем значение первого SubItem
                string subItemText = item.SubItems[0].Text;

                // Устанавливаем текст подсказки для элемента списка
                toolTip.SetToolTip(tabControlGroups.SelectedTab.Controls[0] as ListView, subItemText);
            }
        }

        private void ListViewItem_MouseLeave(object sender, EventArgs e)
        {
            // Очищаем текст подсказки при покидании элемента списка
            toolTip.SetToolTip(tabControlGroups.SelectedTab.Controls[0] as ListView, string.Empty);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(Environment.CurrentDirectory + @"\..\..\..\curAudience.txt", activeGroupeTitle);
        }

        private void ListView1_MouseLeave(object sender, EventArgs e)
        {
            // При покидании ListView очищаем текст подсказки
            toolTip.SetToolTip((tabControlGroups.SelectedTab.Controls[0] as ListView), string.Empty);
        }
        #endregion
    }
}