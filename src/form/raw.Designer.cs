namespace src.form
{
    partial class raw
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
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.wbPreview = new System.Windows.Forms.WebBrowser();
            this.rtbDocument = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.btnMinize = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.wbPreview);
            this.kryptonGroupBox1.Panel.Controls.Add(this.rtbDocument);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(324, 487);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonGroupBox1_Paint);
            // 
            // wbPreview
            // 
            this.wbPreview.Location = new System.Drawing.Point(3, 3);
            this.wbPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPreview.Name = "wbPreview";
            this.wbPreview.Size = new System.Drawing.Size(314, 462);
            this.wbPreview.TabIndex = 1;
            // 
            // rtbDocument
            // 
            this.rtbDocument.Location = new System.Drawing.Point(170, 3);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(147, 462);
            this.rtbDocument.TabIndex = 0;
            this.rtbDocument.Text = "";
            // 
            // btnMinize
            // 
            this.btnMinize.Location = new System.Drawing.Point(286, 3);
            this.btnMinize.Name = "btnMinize";
            this.btnMinize.Size = new System.Drawing.Size(32, 10);
            this.btnMinize.TabIndex = 1;
            this.btnMinize.Values.Text = "Ẩn";
            this.btnMinize.Click += new System.EventHandler(this.btnMinize_Click);
            // 
            // raw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(329, 493);
            this.Controls.Add(this.btnMinize);
            this.Controls.Add(this.kryptonGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "raw";
            this.Text = "Bản xem trước";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.raw_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox rtbDocument;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnMinize;
        private System.Windows.Forms.WebBrowser wbPreview;
    }
}