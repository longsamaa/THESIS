using src.Files;
using src.segment;
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
    public partial class raw : Form
    {
        public main mainForm;
        public string Content;
        public string htmlStringStrong = "<strong>";
        public string htmlStringStrongEnd = "</strong>";
        public raw(main main)
        {
            InitializeComponent();
            mainForm = main; 
        }

        private void kryptonGroupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void raw_Load(object sender, EventArgs e)
        {
            initSize();
            initcontrol(); 
        }

        public void initSize()
        {
            this.Left = mainForm.editorForm.Width - 5;
            this.Top = 0;
        }

        public void initcontrol()
        {
            kryptonGroupBox1.Text = "Văn bản";
        }

        private void btnMinize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; 
        }

        public void writeContentToRichTextBox(string content)
        {
            //rtbDocument.Text = content;
            wbPreview.DocumentText = convertString(content);
           // wbPreview.Navigate(@"https://en.wikipedia.org/wiki/Dog");
            Content = content; 
        }

        public string convertString(string content)
        {
            var htmlText = new StringBuilder(content);
            htmlText.Replace("\r\n", "\n")
           .Replace("\r", "\n")
           .Replace("\n", "<br/>");
            return htmlText.ToString(); 
        }

        public void colorSegment(Segment segment)
        {
            List<contentpage> listcontentpage = mainForm.project.getCurrentFile().listcontentpage;
            int page = segment.getPage();
            int index = segment.index;  
            for(int i = 0; i < page - 1; i++)
            {
                index = listcontentpage[i].content.Length + index; 
            }
            string contenttemp = Content; 
            contenttemp = contenttemp.Insert(index, htmlStringStrong);
            contenttemp = contenttemp.Insert(htmlStringStrong.Length + index + segment.getTMSource().Length, htmlStringStrongEnd);
            wbPreview.DocumentText = convertString(contenttemp);
            //Application.DoEvents();
            //wbPreview.Document.ExecCommand("SelectAll", false, null);
            //wbPreview.Document.ExecCommand("Copy", false, null);
            //rtbDocument.Paste();
            //rtbDocument.ReadOnly = true;
        }
    }
}
