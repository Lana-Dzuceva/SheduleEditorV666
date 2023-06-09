using ScheduleEditorClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheduleEditorV6
{
    public partial class FormEditTPCell : Form
    {
        public List<string> cellData;
        List<string> teachers;
        DataGridView dataGrid;
        int row;
        int col;
        FormTeacherPreferences form;
        public FormEditTPCell(List<string> teachers_, DataGridView dataGrid_, int row_, int col_, FormTeacherPreferences form)
        {
            InitializeComponent();
            cellData = dataGrid_[col_, row_].Tag as List<string>;
            teachers = teachers_;
            dataGrid = dataGrid_;
            row = row_;
            col = col_;
            this.form = form;
        }

        private void FormEditTPCell_Load(object sender, EventArgs e)
        {
            listViewIn.Items.AddRange(cellData.Select(name => new ListViewItem(name)).ToArray());
            listViewOut.Items.AddRange(teachers.Where(teacher => !cellData.Contains(teacher)).Select(teacher => new ListViewItem(teacher)).ToArray());
        }


        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var lv = sender as ListView;
                var lvi = lv.GetItemAt(e.X, e.Y);
                var a = lv.DoDragDrop(lvi.Text, DragDropEffects.Move);
                if (a != DragDropEffects.None)
                    lv.Items.Remove(lvi);
            }
            catch (Exception)
            { }
        }

        private void listView_DragDrop(object sender, DragEventArgs e)
        {
            var item = e.Data.GetData(typeof(string)) as string;
            (sender as ListView).Items.Add(new ListViewItem(item));
        }

        private void listView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void FormEditTPCell_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            dataGrid[col, row].Tag = listViewIn.Items
                                        .Cast<ListViewItem>()
                                        .Select(item => (item as ListViewItem).Text)
                                        .ToList();
            form.UpdateDGV();
            form.Save();
            //шатала я это все
            //в зад винформы
            // чтоб еще раз в своей жизни я делала фронт...
            this.Close();
        }
    }
}
