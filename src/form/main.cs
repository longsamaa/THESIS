﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using src.form;
using src.project; 
using System.IO;
using System.Xml;
using src.messagebox;
using src.Files;
using src.segment;
using src.TM;
using src.semanticsimilarity;
using src.machinetranslator;
using src.processing;
using src.TB;
using System.Diagnostics;

namespace src.form
{
    public partial class main : Form
    {
        public Project project; 
        public fuzzymatches fuzzymatchesForm; 
        public editor editorForm;
        public createNewProject creatNewProjectForm;
        public projectfiles projectFilesForm;
        public machineTranslation machineTranslationForm;
        public dictionary dictionaryForm;
        public importTB importTBForm;
        public createTM createTMForm;
        public TranslationAll TranslationAllForm;
        public raw rawForm;
        public webrowser webrowserForm;
        public preview previewForm; 
        TMDATA tmDataAccess = new TMDATA();
        List<tm> tmData = new List<tm>();
        public HashSet<tb> dictionary = new HashSet<tb>(); 
        public string filter = "cat|*.cat";
        public string catname = "CAT";
        tbOffline tbOffline = new tbOffline();
        public System.Windows.Forms.Form ParentForm { get; }

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            //openChildForm();
            readTbOff();
            loadTB(); 
            setBackColor();
            //Init Control
            initControl();
            //------------

            //Editor form
            openIntroduction();
            //--------------

            //Fuzzy matched form
            openChildForm();
            //------------------

            //Machine Translation Form
            openMachineTranslationForm();
            //------------------------
            //initProject(); 
            //openProjectFilesForm();
        }

        public void setProject(Project Project)
        {
            this.project = new Project(); 
            this.project = Project; 
        }
        public void initProject()
        {
            
        }
        public void readTbOff()
        {
            tbOffline.readTBOff(); 
        } 

        private void initControl()
        {
            if(project == null)
            { 
                reloadToolStripMenuItem.Enabled = false;  
                SaveToolStripMenuItem.Enabled = false; 
                createTranslatedDocumentToolStripMenuItem.Enabled = false;
                btnStripCreateTranslationFile.Enabled = false;
                btnStripReloadFolder.Enabled = false;
                btnStripSaveProject.Enabled = false;
                btnStripSaveSegment.Enabled = false;
                toolStripButton1.Enabled = false; 
            }
        }

        private void reloadControl()
        {
            if(project != null)
            { 
                reloadToolStripMenuItem.Enabled = true; 
                SaveToolStripMenuItem.Enabled = true; 
                createTranslatedDocumentToolStripMenuItem.Enabled = true;
                btnStripCreateTranslationFile.Enabled = true;
                btnStripReloadFolder.Enabled = true;
                btnStripSaveProject.Enabled = true;
                btnStripSaveSegment.Enabled = true;
                toolStripButton1.Enabled = true; 
            }
        }

        private void reloadNameCAT()
        {
            if(project != null)
            {
                this.Text = catname + "::" + project.getNameProject(); 
            }
        }

        public void createNewProject()
        {
            //Tạo folder làm việc
            if (this.project != null)
            {
                project.createProject(); 
                string path = Path.Combine(this.project.getPathProject(),this.project.getNameFileProject());
                loadProject(path); 
            }
        }

        public void loadProject(string path)
        {
            this.project = new Project();
            if (path != null)
            {
                if (this.project.readProject(path))
                {
                    if (project != null)
                    {
                        if (project.isEmptySourceFolder())
                        {
                            editorForm.openTutorial();
                        }
                        else
                        {

                        }
                        openProjectFilesForm();
                        setSourceLangandTargetLangtoMachineTrans();
                        loadTMDATA();
                        //loadTB();
                        reloadControl();
                        reloadNameCAT(); 
                    }
                }
                else
                {
                    TextOfMessageBox a = new TextOfMessageBox();
                    MessageBox.Show(a.FILE_NOT_FOUND, "Cảnh báo", MessageBoxButtons.YesNo);
                }
            }
        }

        //public void createXMLFileProject(string fileName)
        //{
        //    string filename = project.getPathProject() + "\\" + fileName; 

        //}

        public void setAndReadCurrentFileProject(file file)
        {
            if(project != null)
            {
                project.setCurrentFile(file); 
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openCreateProjectForm(); 
        }

        public void openChildForm()
        {
            fuzzymatchesForm = new fuzzymatches(this);
            fuzzymatchesForm.MdiParent = this;
            fuzzymatchesForm.Show();

            dictionaryForm = new dictionary(this);
            dictionaryForm.MdiParent = this;
            dictionaryForm.Show();

        }

        public void openIntroduction()
        {
            if (editorForm == null)
            {
                editorForm = new editor(this);
                editorForm.MdiParent = this;
                editorForm.Show();
            }
            //editorForm.openEditor();
        }
        public void openEditorForm()
        {
            if(editorForm != null)
            {
                editorForm.openEditor(); 
            }
        }

        public void setTargetToGridFromFuzzyMatched(string target)
        {
            if(editorForm != null)
            {
                editorForm.setTargetToEditorGrid(target); 
            }
        }
        public void openMachineTranslationForm()
        {
            if(machineTranslationForm == null)
            {
                machineTranslationForm = new machineTranslation(this);
                machineTranslationForm.MdiParent = this;
                machineTranslationForm.Show();
                machineTranslationForm.Hide();
            }
        }

        public void openCreateProjectForm()
        {
            if (creatNewProjectForm == null)
            {
                creatNewProjectForm = new createNewProject(this);
                creatNewProjectForm.ShowDialog();
            }
        }

        public void closeCreateProjectForm()
        {
            if(creatNewProjectForm != null)
            {
                creatNewProjectForm = null; 
            }
        }

        public void openProjectFilesForm()
        {
            if (projectFilesForm == null)
            {
                projectFilesForm = new projectfiles(this);
                projectFilesForm.ShowDialog();
            }
            else
            {
                projectFilesForm.Close();

                projectFilesForm = new projectfiles(this);
                projectFilesForm.ShowDialog();
            }
        }

        public void closeProjectFilesForm()
        {
            if(projectFilesForm != null)
            {
                projectFilesForm = null; 
            }
        }

        public void readContentFile()
        {
            if(project.getCurrentFile() != null)
            {
                project.readContentCurrentFile(); 
            }
        }

       public Form GetForm()
        {
            return fuzzymatchesForm;
        }

        public void setBackColor()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = SystemColors.Window;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectFilesForm == null)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = filter;
                    ofd.Multiselect = false;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string path = ofd.FileName;
                        loadProject(path);
                    }
                }
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                string path = Path.Combine(this.project.getPathProject(), this.project.getNameFileProject());
                loadProject(path);
            }
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(project != null)
            {
                if(project.getCurrentFile() == null)
                {
                    TextOfMessageBox a = new TextOfMessageBox();
                    MessageBox.Show(a.NO_FILE_TO_SAVE, "Cảnh báo", MessageBoxButtons.YesNo);
                }
                else
                {
                    //project.saveProject(); 
                    saveProject(); 
                }
            }
        }

        private void createTranslatedDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(project != null)
            {
                createTranslationDocument(); 
            }
        }

        public void createTranslationDocument()
        {
            List<Segment> segments = new List<Segment>();
            segments = editorForm.getListSegment();
            //List<Segment> listSegs = new List<Segment>();
            //listSegs = editorForm.getListSegment();
            project.reWriteListSegment(segments);
            using (frmWaitForm frm = new frmWaitForm(project.createTranslatedDocument))
            {
                frm.ShowDialog(this);
            }
            //project.createTranslatedDocument();
            TextOfMessageBox a = new TextOfMessageBox();
            DialogResult dialogResult =  MessageBox.Show(a.CREATE_TRASLATED_DOCUMENT_SUCCESSFULLY, "Thông báo", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                string path = this.project.getPathTargetFolder();
                Process.Start(path);
            }

        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(project != null)
            {
                if (project.getCurrentFile() != null)
                {
                    saveProject();
                }
            }
        }

        private void myMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myMemoryToolStripMenuItem.Checked = !(myMemoryToolStripMenuItem.Checked); 
            if(machineTranslationForm != null)
            {
                machineTranslationForm.setActiveMymemoryMachine(myMemoryToolStripMenuItem.Checked); 
            }
        }
        private void openNMTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openNMTToolStripMenuItem.Checked = !(openNMTToolStripMenuItem.Checked); 
            if(machineTranslationForm != null)
            {
                machineTranslationForm.setActiveOpenNMTMachine(openNMTToolStripMenuItem.Checked); 
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if(project != null)
            //{
            //    string fileName = project.getTMName() + ".xlsx";
            //    string pathcsv = Path.Combine(project.getPathTempFolder(), fileName);
            //    List<tm> TMData = tmDataAccess.LoadTM(project.getTMName());
            //    exportCSV exportCSV = new exportCSV();
            //    if (File.Exists(pathcsv))
            //    {
            //        File.Delete(pathcsv); 
            //    }
            //    exportCSV.exportTM(pathcsv, TMData, project.getSourceLang(), project.getTargetLang());
            //    MessageBox.Show("Đã xuất cơ sở dữ liệu dịch thành công hãy kiểm tra trong thư mục tm của dự án", "Đã xuất tập tin thành công!", MessageBoxButtons.OK);
            //    string path = project.getPathTempFolder();
            //    Process.Start(path); 
            //    //exportCSV.exportTM
            //}
            if (createTMForm == null)
            {
                createTMForm = new createTM(this);
                createTMForm.ShowDialog();
            }
        }
        private void setSourceLangandTargetLangtoMachineTrans()
        {
            if(project != null)
            {
                if(machineTranslationForm != null)
                {
                    machineTranslationForm.setSourceLangandTargetLangmachine(project.getSourceLang(), project.getTargetLang()); 
                }
            }
        }
        public void translationMachine(string source)
        {
            if(project != null)
            {
                if(machineTranslationForm != null)
                {
                    machineTranslationForm.translate(source); 
                }
            }
        }

        public void resetTextMachineTranslationForm()
        {
            if(machineTranslationForm != null)
            {
                machineTranslationForm.resetText(); 
            }
        }

        public List<machineTranslationResult> getResultsMachineTranslatorion()
        {
            if(machineTranslationForm != null)
            {
                return machineTranslationForm.getResultMachineTranslator();
            }
            return null;
        }
     

   


        //Set from file 

        private void setMenufromFile()
        {

        }

        private void setMachineTranslationFormFile()
        {

        }

        private void CreateProjectButton_Click(object sender, EventArgs e)
        {
            openCreateProjectForm();
        }



        private void OpenProjectButton_Click(object sender, EventArgs e)
        {
            if (projectFilesForm == null)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = filter;
                    ofd.Multiselect = false;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string path = ofd.FileName;
                        initForm();
                        loadProject(path);
                    }
                }
            }
        }

        public void initForm()
        {
            editorForm.openEditorTutorial();
            dictionaryForm.initDict();
            fuzzymatchesForm.initFuzzzyMatch();
        }

        private void SaveProjectButton_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                if (project.getCurrentFile() == null)
                {
                    TextOfMessageBox a = new TextOfMessageBox();
                    MessageBox.Show(a.NO_FILE_TO_SAVE, "Cảnh báo", MessageBoxButtons.YesNo);
                }
                else
                {
                    saveProject(); 
                }
            }
        }

        public void saveProject()
        {
            List<Segment> listSegs = new List<Segment>();
            listSegs = editorForm.getListSegment();
            project.saveProject(listSegs);
        }

        private void ReloadFolder_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                initForm();
                if (project.getCurrentFile() == null)
                {
                    TextOfMessageBox a = new TextOfMessageBox();
                    MessageBox.Show(a.NO_FILE_TO_SAVE, "Cảnh báo", MessageBoxButtons.YesNo);
                }
                else
                {
                    saveProject();
                }
                string path = Path.Combine(this.project.getPathProject(), this.project.getNameFileProject());
                loadProject(path);
            }
        }

        private void CreateTranslationFile_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                if (project.getCurrentFile() != null)
                {
                    createTranslationDocument();
                }
                else
                {
                    MessageBox.Show("Cảnh báo", "Không có tập tin không thể tạo tập tin dịch", MessageBoxButtons.YesNo); 
                }
            }
        }

        private void loadTMDATA()
        {
            //tmData = tmDataAccess.LoadTM();
        }

        private void loadTB()
        {
            tbProc tbProc = new tbProc();
            dictionary = tbProc.readTB(); 
        }

        private void btnStripSaveSegment_Click(object sender, EventArgs e)
        {
            if(project != null && editorForm != null)
            {
                editorForm.addSegmentToTM(); 
            }
        }


        //-------------
        //Predict
        public void predictSemantic(string srcText)
        {
            if (project != null)
            {
                List<tm> TMData = new List<tm>();
                List<semanticSimilarity> resultSemantic = new List<semanticSimilarity>();
                List<resultTMOnline> resultTMOnline = new List<resultTMOnline>(); 
                if (TMLocalToolStripMenuItem.Checked)
                {
                    TMData = tmDataAccess.LoadTM(project.getTMName());
                    resultSemantic = fuzzymatchesForm.getResultSemantic(srcText, TMData); 
                }
                if (TMGlobalToolStripMenuItem.Checked)
                {
                    resultTMOnline = fuzzymatchesForm.GetResultTMOnlines(srcText); 
                }
                var resultMT = this.getResultsMachineTranslatorion();
                fuzzymatchesForm.setResultPredictSemantic(resultSemantic,resultMT,resultTMOnline);
            }
        }
        public void hideRTBFuzzymatched()
        {
            if(project != null)
            {
                if(fuzzymatchesForm != null)
                {
                    fuzzymatchesForm.hideRTBFuzzymatched(); 
                }
            }
        }

        //-----------

        //Dic
        public void getSourceToDictForm(string source)
        {
            if(dictionaryForm != null)
            {
                dictionaryForm.getgetSourceToDictForm(source);
            }
        }

        private void TMLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TMLocalToolStripMenuItem.Checked = !(TMLocalToolStripMenuItem.Checked);
        }

        private void TMGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TMGlobalToolStripMenuItem.Checked = !(TMGlobalToolStripMenuItem.Checked); 
        }

        private void findTembarseOnlineToolStripMenu_Click(object sender, EventArgs e)
        {
            findTembarseOnlineToolStripMenu.Checked = !(findTembarseOnlineToolStripMenu.Checked);
        }

        public void findTermbaseOnline(string src,string target)
        { 
            if (findTembarseOnlineToolStripMenu.Checked)
            {
                //DialogResult = MessageBox.Show("Lưu ý", "Bạn có muốn chuyển trang", MessageBoxButtons.YesNo);
                //if (DialogResult == DialogResult.Yes)
                //{
                //    wikimedia wiki = new wikimedia();
                //    wiki.lang = "en";
                //    string url = wiki.getResult(src);
                //    if (url != null)
                //    {
                //        Process.Start("explorer", url);
                //    }
                //    wiki.lang = "vi";
                //    url = wiki.getResult(target);
                //    if (url != null)
                //    {
                //        Process.Start("explorer", url);
                //    }
                //}
                if(webrowserForm != null)
                {
                    webrowserForm.search(src); 
                }
            }
        }

        public void findTermbaseOffline(string src)
        {
            if (tbOffline.listSeg.Count > 0)
            {
                if (dictionaryForm != null)
                {
                    dictionaryForm.setTBOffToGrid(tbOffline.findTermbaseOffline(src));
                }
            }
        }

        private void thêmCơSởThuậtNgữToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importTBForm = new importTB(this);
            importTBForm.ShowDialog(); 
        }

        public void createTBOffline(string path)
        {
           tbOffline.path = path;
            //tbOffline.pathFolder = tbOfflinePath; 
            using (frmWaitForm frm = new frmWaitForm(tbOffline.createTBOfflines))
            {
                frm.ShowDialog(this);
            }
            tbOffline.readTBOff();
        }
        //

        //Creat tm
        public void createTM(string password)
        {
            if (project != null)
            {
                string fileName = project.getTMName() + ".xlsx";
                string pathcsv = Path.Combine(project.getPathTempFolder(), fileName);
                List<tm> TMData = tmDataAccess.LoadTM(project.getTMName());
                exportCSV exportCSV = new exportCSV();
                if (File.Exists(pathcsv))
                {
                    File.Delete(pathcsv);
                }
                exportCSV.exportTM(pathcsv, TMData, project.getSourceLang(), project.getTargetLang(),password);
                MessageBox.Show("Đã xuất cơ sở dữ liệu dịch thành công hãy kiểm tra trong thư mục tm của dự án", "Đã xuất tập tin thành công!", MessageBoxButtons.OK);
                string path = project.getPathTempFolder();
                Process.Start(path);
                //exportCSV.exportTM
            }
        }

        //Translation All form
        public void openTranslationAllForm()
        {
            TranslationAllForm = new TranslationAll(this);
            TranslationAllForm.ShowDialog(); 
        }

        public void translationAllMymemory()
        {
            List<Segment> source = new List<Segment>();
            List<machineTranslationResult> results = new List<machineTranslationResult>(); 
            source = editorForm.getListSegment(); 
            if(source.Count > 0)
            {
                results = machineTranslationForm.getTranslationAllMymemory(source);
                editorForm.setAllTarget(results);
            }
        }

        public void translationAllOpenNMT()
        {
            List<Segment> source = new List<Segment>();
            List<machineTranslationResult> results = new List<machineTranslationResult>();
            source = editorForm.getListSegment(); 
            if(source.Count > 0)
            {
                results = machineTranslationForm.getTranslationAllOpenNMT(source);
                editorForm.setAllTarget(results); 
            }
        }
        //---------------------
        //Raw
        public void openRawForm()
        {
            if (rawForm == null)
            {
                rawForm = new raw(this);
                rawForm.MdiParent = this;
                rawForm.Show();
            }
        }
        public void writeContentToRichTextBoxRawForm()
        {
            if(project.getCurrentFile() != null)
            {
                string content = project.getCurrentFile().content;
                rawForm.writeContentToRichTextBox(content); 
            }
        }
        public void colorRTBRawForm(Segment segment)
        {
            rawForm.colorSegment(segment); 
        }
        //----
        //Web
        public void openWebBrowserForm()
        {
            if(webrowserForm == null)
            {
                webrowserForm = new webrowser(this);
                webrowserForm.MdiParent = this;
                webrowserForm.Show();
                webrowserForm.setSourceLang();
                webrowserForm.setTargetLang(); 
            }
        }
        //----
        //Preview
        public void openPreviewForm()
        {
            if(previewForm == null)
            {
                previewForm = new preview(this);
                previewForm.MdiParent = this;
                previewForm.Show();
            }
        }
        public void writeContentToRichTextBoxPreviewForm()
        {
            if (project.getCurrentFile() != null)
            {
                string content = project.getCurrentFile().content;
                previewForm.writeContent(); 
            }
        }

        public void replacePreviewForm(List<Segment> segments)
        {
            previewForm.replace(segments); 
        }

        //------
        public void nullCreateTMForm()
        {
            createTMForm = null;
        }
    }
}
