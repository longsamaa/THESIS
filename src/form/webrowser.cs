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
    public partial class webrowser : Form
    {
        public main mainForm;
        public string url = @"http://tratu.coviet.vn/hoc-tieng-anh/tu-dien/lac-viet/{0}/{1}.html";
        public string sourceLang;
        public string targetLang; 
        public webrowser(main main)
        {
            InitializeComponent();
            mainForm = main;
            //wbBrowser.Dock = DockStyle.Fill; 
        }

        private void wbBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (IsHorizontalScrollbarPresent)
            //{
            //    wbBrowser.Size = new Size(wbBrowser.Document.Body.ScrollRectangle.Width, wbBrowser.Document.Body.ScrollRectangle.Height);
            //    webrowser.ActiveForm.Size = new Size(wbBrowser.Document.Body.ScrollRectangle.Width, wbBrowser.Document.Body.ScrollRectangle.Height);
            //}
        }

        public void init()
        {
            this.Left = mainForm.editorForm.Width - 5;
            this.Top = 0;
        }
        public bool IsHorizontalScrollbarPresent
        {
            get
            {
                var widthofScrollableArea = wbBrowser.Document.Body.ScrollRectangle.Width;
                var widthofControl = wbBrowser.Document.Window.Size.Width;

                return widthofScrollableArea > widthofControl;
            }
        }
        public void setSourceLang()
        {
            string SourceLang = mainForm.project.getSourceLang(); 
            if(SourceLang == "vi")
            {
                sourceLang = "V";
            }
            if(SourceLang == "en")
            {
                sourceLang = "A";
            }
        }
        public void setTargetLang()
        {
            string TargetLang = mainForm.project.getTargetLang(); 
            if(TargetLang == "vi")
            {
                targetLang = "V";
            }
            if(TargetLang == "en")
            {
                targetLang = "A";
            }
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public void search(string source)
        {
            string lang = sourceLang + "-" + targetLang;
            string Url = String.Format(url, lang, source);
            wbBrowser.Navigate(Url);
            this.WindowState = FormWindowState.Normal; 
        }

        private void webrowser_Load(object sender, EventArgs e)
        {
            init();
        }
    }
}
