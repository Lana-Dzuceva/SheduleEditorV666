using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheduleEditorV2
{
    public partial class FormTeacherPreferences : Form
    {
        public FormTeacherPreferences()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewTable.RowTemplate.Height = 23;
            for (int i = 0; i < 4; i++)
            {
                dataGridViewTable.Columns.Add(new SpannedDataGridView.DataGridViewTextBoxColumnEx());
            }
            dataGridViewTable.RowCount = 40;
            dataGridViewTable.ColumnHeadersHeight = 40;

        }

        private void FormTeacherPreferences_Load(object sender, EventArgs e)
        {
            //hmm();
        }
    }
}
