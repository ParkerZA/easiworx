namespace Finx.App.UserControls
{
    partial class xHtmlRenderer
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(150, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.AutoScrollMinSize = new System.Drawing.Size(148, 20);
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.BaseStylesheet = null;
            this.htmlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.htmlPanel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel1.Location = new System.Drawing.Point(0, 25);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(150, 125);
            this.htmlPanel1.TabIndex = 2;
            this.htmlPanel1.Text = "htmlPanel1";
            // 
            // xHtmlRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "xHtmlRenderer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
    }
}
