using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src.form
{
    public partial class TranslationAll : Form
    {
        public main mainForm; 
        public TranslationAll(main main)
        {
            InitializeComponent();
            mainForm = main; 
        }

        private void TranslationAll_Load(object sender, EventArgs e)
        {
            grbTranslationAll.Text = "Hãy chọn dịch máy"; 
        }

        private void cbMyMemory_CheckedChanged(object sender, EventArgs e)
        {
            if(cbMyMemory.Checked)
            {
                cbOpenNMT.Enabled = false;
            }
            else
            {
                cbOpenNMT.Enabled = true; 
            }
        }

        private void cbOpenNMT_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOpenNMT.Checked)
            {
                cbMyMemory.Enabled = false;
            }
            else
            {
                cbMyMemory.Enabled = true; 
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (cbMyMemory.Checked)
            {
                mainForm.translationAllMymemory();
            }
            if (cbOpenNMT.Checked)
            {
                mainForm.translationAllOpenNMT(); 
            }
            this.Close(); 
        }
    }
}
