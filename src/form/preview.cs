using src.segment;
using src.TM;
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
    public partial class preview : Form
    {
        public main mainForm;
        public string content;
        public List<Segment> listSegment = new List<Segment>(); 
        public preview(main main)
        {
            InitializeComponent();
            mainForm = main; 
        }

        private void preview_Load(object sender, EventArgs e)
        {
            init();
            //writeContent(); 
        }

        public void init()
        {
            this.Left = mainForm.editorForm.Width - 5;
            this.Top = 0;
        }

        public void writeContent()
        {
            if(mainForm.project.getCurrentFile() != null)
            {
                content = mainForm.project.getCurrentFile().content;
                preDoc.DocumentText = convertString(content);
            }
        }

        public string convertString(string content)
        {
            var htmlText = new StringBuilder(content);
            htmlText.Replace("\r\n", "\n")
           .Replace("\r", "\n")
           .Replace("\n", "<br/>");
            return htmlText.ToString();
        }

        public void replace(List<Segment> segments)
        {
            copySegment(); 
            setTarget(segments);
            string Content = content;
            for (int i = 0; i < listSegment.Count; i++)
            {
                Content = replaceText(listSegment[i], i, Content);
            }
            preDoc.DocumentText = convertString(Content);
            //listSegment = null; 
        }

        public void copySegment()
        {
            listSegment = new List<Segment>(); 
            foreach(Segment seg in mainForm.project.getCurrentFile().getListSegments())
            {
                listSegment.Add(seg); 
            }
        }

        public void setTarget(List<Segment> segments)
        {
            for(int i = 0; i < segments.Count; i++)
            {
                tm temp = segments[i].getTM();
                listSegment[i].setTM(temp); 
            }
        }

        public string replaceText(Segment seg, int index, string Content)
        {
            string source = seg.getTMSource();
            string target = seg.getTMTarget();
            //var aStringBuilder = new StringBuilder(content);
            if (!String.IsNullOrEmpty(target))
            {
                Content = Content.Remove(seg.index, source.Length);
                Content = Content.Insert(seg.index, target);
                int dis = seg.getTMTarget().Length - seg.getTMSource().Length;
                updateListSegment(index, dis); 
            }
            return Content; 
        }

        public void updateListSegment(int index, int dis)
        {
            for(int i = index + 1; i < listSegment.Count; i++)
            {
                listSegment[i].index += dis; 
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; 
        }
    }
}
