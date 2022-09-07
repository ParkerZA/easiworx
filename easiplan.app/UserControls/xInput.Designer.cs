namespace Finx.App.UserControls
{
    partial class xInput
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_Cntrl = new System.Windows.Forms.Panel();
            this.dageTextBox = new DevAge.Windows.Forms.DevAgeTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel_Cntrl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_Cntrl);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(373, 28);
            this.panel2.TabIndex = 4;
            // 
            // panel_Cntrl
            // 
            this.panel_Cntrl.BackColor = System.Drawing.Color.Transparent;
            this.panel_Cntrl.Controls.Add(this.dageTextBox);
            this.panel_Cntrl.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Cntrl.Location = new System.Drawing.Point(173, 0);
            this.panel_Cntrl.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Cntrl.Name = "panel_Cntrl";
            this.panel_Cntrl.Padding = new System.Windows.Forms.Padding(1, 2, 1, 1);
            this.panel_Cntrl.Size = new System.Drawing.Size(200, 28);
            this.panel_Cntrl.TabIndex = 5;
            // 
            // dageTextBox
            // 
            this.dageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dageTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dageTextBox.Location = new System.Drawing.Point(1, 2);
            this.dageTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.dageTextBox.Name = "dageTextBox";
            this.dageTextBox.Size = new System.Drawing.Size(198, 24);
            this.dageTextBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(170, 28);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1.........";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(50, 30);
            this.Name = "xInput";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(375, 30);
            this.panel2.ResumeLayout(false);
            this.panel_Cntrl.ResumeLayout(false);
            this.panel_Cntrl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_Cntrl;
        private DevAge.Windows.Forms.DevAgeTextBox dageTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}
