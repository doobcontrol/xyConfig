namespace com.xiyuansoft.xyConfigSample
{
    partial class FrmOnePar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.txtScanLines = new System.Windows.Forms.TextBox();
            this.txtRoSpeed = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(177, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtScanLines
            // 
            this.txtScanLines.Location = new System.Drawing.Point(151, 31);
            this.txtScanLines.Name = "txtScanLines";
            this.txtScanLines.Size = new System.Drawing.Size(101, 25);
            this.txtScanLines.TabIndex = 59;
            this.txtScanLines.TextChanged += new System.EventHandler(this.txtScanLines_TextChanged);
            // 
            // txtRoSpeed
            // 
            this.txtRoSpeed.Location = new System.Drawing.Point(151, 6);
            this.txtRoSpeed.Name = "txtRoSpeed";
            this.txtRoSpeed.Size = new System.Drawing.Size(101, 25);
            this.txtRoSpeed.TabIndex = 58;
            this.txtRoSpeed.TextChanged += new System.EventHandler(this.txtRoSpeed_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 15);
            this.label12.TabIndex = 57;
            this.label12.Text = "扫描线数：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 15);
            this.label13.TabIndex = 56;
            this.label13.Text = "转速（秒/转）：";
            // 
            // FrmOnePar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 95);
            this.Controls.Add(this.txtScanLines);
            this.Controls.Add(this.txtRoSpeed);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmOnePar";
            this.Text = "FrmOnePar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtScanLines;
        private System.Windows.Forms.TextBox txtRoSpeed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}