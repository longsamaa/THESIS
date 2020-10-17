namespace src.form
{
    partial class TranslationAll
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
            this.cbMyMemory = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.cbOpenNMT = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.grbTranslationAll = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.grbTranslationAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbTranslationAll.Panel)).BeginInit();
            this.grbTranslationAll.Panel.SuspendLayout();
            this.grbTranslationAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMyMemory
            // 
            this.cbMyMemory.Location = new System.Drawing.Point(3, 3);
            this.cbMyMemory.Name = "cbMyMemory";
            this.cbMyMemory.Size = new System.Drawing.Size(86, 20);
            this.cbMyMemory.TabIndex = 1;
            this.cbMyMemory.Values.Text = "MyMemory";
            this.cbMyMemory.CheckedChanged += new System.EventHandler(this.cbMyMemory_CheckedChanged);
            // 
            // cbOpenNMT
            // 
            this.cbOpenNMT.Location = new System.Drawing.Point(3, 29);
            this.cbOpenNMT.Name = "cbOpenNMT";
            this.cbOpenNMT.Size = new System.Drawing.Size(80, 20);
            this.cbOpenNMT.TabIndex = 2;
            this.cbOpenNMT.Values.Text = "OpenNMT";
            this.cbOpenNMT.CheckedChanged += new System.EventHandler(this.cbOpenNMT_CheckedChanged);
            // 
            // grbTranslationAll
            // 
            this.grbTranslationAll.Location = new System.Drawing.Point(2, -5);
            this.grbTranslationAll.Name = "grbTranslationAll";
            // 
            // grbTranslationAll.Panel
            // 
            this.grbTranslationAll.Panel.Controls.Add(this.kryptonButton2);
            this.grbTranslationAll.Panel.Controls.Add(this.btnExit);
            this.grbTranslationAll.Panel.Controls.Add(this.cbMyMemory);
            this.grbTranslationAll.Panel.Controls.Add(this.cbOpenNMT);
            this.grbTranslationAll.Size = new System.Drawing.Size(311, 104);
            this.grbTranslationAll.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(219, 52);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 25);
            this.btnExit.TabIndex = 3;
            this.btnExit.Values.Text = "Hủy";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(123, 52);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton2.TabIndex = 4;
            this.kryptonButton2.Values.Text = "Xác nhận";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // TranslationAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 100);
            this.Controls.Add(this.grbTranslationAll);
            this.Name = "TranslationAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dịch tất cả";
            this.Load += new System.EventHandler(this.TranslationAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grbTranslationAll.Panel)).EndInit();
            this.grbTranslationAll.Panel.ResumeLayout(false);
            this.grbTranslationAll.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbTranslationAll)).EndInit();
            this.grbTranslationAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbMyMemory;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbOpenNMT;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox grbTranslationAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
    }
}