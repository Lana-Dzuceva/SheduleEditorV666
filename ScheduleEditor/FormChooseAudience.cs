using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheduleEditorV6
{
    public partial class FormChooseAudience : Form
    {
        public ListViewItem selectedLVIAudience;
        public FormChooseAudience()
        {
            InitializeComponent();
        }

        private void FormChooseAudience_Load(object sender, EventArgs e)
        {
            foreach (var item in new string[] { "Номер", "Количество мест", "Меловая доска", "Маркерная доска", "Количество компьютеров", "Проектор" })
            {
                listViewAudienceDescription.Columns.Add(item, 120);
            }

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewAudienceDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLVIAudience =  listViewAudienceDescription.SelectedItems[0];
        }
    }
}
