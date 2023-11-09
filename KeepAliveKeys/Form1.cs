using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Timer = System.Threading.Timer;
using KeepAliveKeys.Properties;

namespace KeepAliveKeys
{
    public partial class FormStart : Form
    {
        private NotifyIcon _notifyIcon;
        private Icon appIcon;

        public FormStart()
        {
            InitializeComponent();
            InitializeNotifyIcon();
        }

        private void InitializeNotifyIcon()
        {
            appIcon = new Icon("cloud.ico");

            _notifyIcon = new NotifyIcon
            {
                Icon = appIcon,
                Text = "KeepAliveKeys",
                Visible = false
            };

            _notifyIcon.Click += (sender, e) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                _notifyIcon.Visible = false;
            };
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            if (buttonStart.Text == "Start")
            {
                buttonStart.Text = "Stop";
                Timer();
                BackColor = Color.FromArgb(0, 73, 126);
                buttonStart.BackColor = Color.FromArgb(0, 177, 235);
            }
            else if (buttonStart.Text == "Stop")
            {
                Start();
                buttonStart.Text = "Start";
                BackColor = Color.FromArgb(13, 90, 90);
                buttonStart.BackColor = Color.FromArgb(49, 174, 114);
            }
        }

        private void Timer()
        {
            Timer timer = new Timer(UsingKeyboard, null, 0, 3000);
        }

        private void UsingKeyboard(object state)
        {
            SendKeys.SendWait("{DOWN}");
            SendKeys.SendWait("{UP}");
        }

        protected override void OnResize(EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                _notifyIcon.Visible = true;
            }

            base.OnResize(e);
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            Start();
            MaximizeBox = false;
        }

        private void Start()
        {
            Timer();
        }
    }
}
