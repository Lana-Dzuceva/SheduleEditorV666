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

        public List<Teacher> teachersIn, teachersAll;
        FormTeacherPreferences form;
        public FormEditTPCell(List<Teacher> teachersIn_, List<Teacher> teachersAll_, FormTeacherPreferences form)
        {
            InitializeComponent();
            
            this.form = form;
        }

        private void FormEditTPCell_Load(object sender, EventArgs e)
        {
            listViewIn.Items.AddRange(teachersIn.Select(teacher => new ListViewItem(teacher.Name)).ToArray());
            listViewOut.Items.AddRange(teachersAll.Where(teacher => !teachersIn.Contains(teacher)).Select(teacher => new ListViewItem(teacher.Name)).ToArray());
        }


        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var lv = sender as ListView;
                var lvi = lv.GetItemAt(e.X, e.Y);
                var a = lv.DoDragDrop(lvi.Text, DragDropEffects.Move);
                if (a != DragDropEffects.None)
                {
                    lv.Items.Remove(lvi);
                    teachersIn
                }

                    
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
            form.UpdateDGV();
            form.Save();
            //шатала я это все
            //в зад винформы
            // чтоб еще раз в своей жизни я делала фронт...
            this.Close();
        }
    }
}
