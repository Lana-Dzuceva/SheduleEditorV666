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
        FormMain formMain;
        public ListViewItem selectedLVIAudience;
        public int num;
        public FormChooseAudience(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }

        private void FormChooseAudience_Load(object sender, EventArgs e)
        {
            foreach (var item in new string[] { "Номер", "Количество мест", "Меловая доска", "Маркерная доска", "Количество компьютеров", "Проектор" })
            {
                listViewAudienceDescription.Columns.Add(item, 120);
            }
            foreach (var item in formMain.audiences)
            {
                //listViewAudienceDescription.Items.Clear();
                ListViewItem listViewItem = new ListViewItem(item.Number.ToString());
                listViewItem.SubItems.Add(item.CountOfSeats.ToString());
                listViewItem.SubItems.Add(item.ChalkBoard ? "Есть" : "Нет");
                listViewItem.SubItems.Add(item.MarkerBoard ? "Есть" : "Нет");
                listViewItem.SubItems.Add(item.NumberOfComputers.ToString());
                listViewItem.SubItems.Add(item.Projector ? "Есть" : "Нет");
                listViewAudienceDescription.Items.Add(listViewItem);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //selectedLVIAudience = listViewAudienceDescription.SelectedItems[0].Text;
            num = int.Parse(listViewAudienceDescription.SelectedItems[0].Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewAudienceDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLVIAudience = listViewAudienceDescription.SelectedItems[0];
        }
    }
}
