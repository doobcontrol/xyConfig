using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.xiyuansoft.xyConfigSample
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            Text = "参数管理测试程序";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmOnePar().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FrmTabledPars().ShowDialog();
        }
    }
}
