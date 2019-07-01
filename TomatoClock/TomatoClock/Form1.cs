using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;


namespace TomatoClock
{
    public partial class TomatoClock : Form
    {
        #region 调用外部dll
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        //设置窗体句柄的窗体为活动窗体，因为如果此程序并不在焦点中，可以使此程序回归活动窗体
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion
        private int timer;//用户指定的倒计时
        private bool is_run = false;//控制番茄钟是否在运行
        int min;
        int sec;
        public TomatoClock()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer = Convert.ToInt32(textBox1.Text) * 60;
            timer1.Enabled = true;
            is_run = true;
            Pause.Text = "暂停计时";
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            if (Pause.Text == "暂停计时")
            {
                timer1.Stop();
                is_run = false;
                Pause.Text = "恢复计时";
            }
            else
            {
                timer1.Start();
                is_run = true;
                Pause.Text = "暂停计时";
            }
        }

        private void init_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Pause.Text = "暂停计时";
            label1.Text = "00m00s";
            timer = 0;
            is_run = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer > 0)
            {
                timer--;
                min = timer / 60;
                sec = timer % 60;
                label1.Text = min.ToString() + 'm' + sec.ToString() + 's';
            }
            else
            {
                timer1.Enabled = false;
                SetForegroundWindow(this.Handle);
                MessageBox.Show("TIME TO TAKE A REST!");
            }
        }
    }
}
