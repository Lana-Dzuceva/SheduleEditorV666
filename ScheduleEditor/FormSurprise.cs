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
    public partial class FormSurprise : Form
    {
        private Timer moveTimer;
        private Random random;
        public FormSurprise()
        {
            InitializeComponent();
            moveTimer = new Timer();
            moveTimer.Interval = 100;
            moveTimer.Tick += MoveTimer_Tick;
            random = new Random();
        }

        private void FormSurprise_Load(object sender, EventArgs e)
        {
            Screen screen = Screen.PrimaryScreen;
            Location = new Point(screen.Bounds.Width / 2 - this.Width / 2, screen.Bounds.Height / 2 - this.Height / 2);
            moveTimer.Start();
        }
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            int offsetX = random.Next(-30, 31);
            int offsetY = random.Next(-30, 31);
            int newX = Location.X + offsetX;
            int newY = Location.Y + offsetY;
            Location = new Point(newX, newY);
        }

    }
}
