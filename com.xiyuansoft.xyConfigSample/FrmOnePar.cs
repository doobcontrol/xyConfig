using com.xiyuansoft.xyConfig;
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
    public partial class FrmOnePar : Form
    {
        public FrmOnePar()
        {
            InitializeComponent();

            Text = "OneParTest";

            txtRoSpeed.Text = xConfig.getOnePar("txtRoSpeed", "30");
            txtScanLines.Text = xConfig.getOnePar("txtScanLines", "3500");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void txtRoSpeed_TextChanged(object sender, EventArgs e)
        {
            xConfig.setOnePar("txtRoSpeed", txtRoSpeed.Text);
        }

        private void txtScanLines_TextChanged(object sender, EventArgs e)
        {
            xConfig.setOnePar("txtScanLines", txtScanLines.Text);
        }
    }
}
