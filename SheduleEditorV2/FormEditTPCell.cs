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
        DataGridViewCell cell;
        List<string> cellData;
        List<TeacherPreference> preferences;
        public FormEditTPCell(DataGridViewCell cell, List<TeacherPreference> preferences_)
        {
            InitializeComponent();
            this.cell = cell;
            cellData = (cell.Tag as List<string>);
            preferences = preferences_;

        }

        private void FormEditTPCell_Load(object sender, EventArgs e)
        {
            listViewIn.Items.AddRange(cellData.Select(name => new ListViewItem(name)).ToArray());
            listViewOut.Items.AddRange(preferences.Where(pref => !cellData.Contains(pref.Name)).Select(pref => new ListViewItem(pref.Name)).ToArray());

        }

        private void FormEditTPCell_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
