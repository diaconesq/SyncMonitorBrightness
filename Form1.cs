using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncMonitorBrightness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated) CreateHandle();
            
            base.SetVisibleCore(false);
            Task.Run(() =>
            {
                var bs = new BackgroundSync();
                bs.BrightnessUpdated += Bs_BrightnessUpdated;
                bs.SyncBrightness();
            });
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Bs_BrightnessUpdated(byte arg1, int arg2)
        {
            notifyIcon.Text = $"Built-in monitor: {arg1}%, additional monitor: {arg2}%";
        }
    }
}
