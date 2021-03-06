﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using src.project;
using src.segment;
using src.TM;
using src.Text;
using src.Files;
using src.machinetranslator;
using src.messagebox;
using src.TB;

namespace src.form
{
    
    public partial class editor : Form
    {
        Project Project;
        List<Segment> listSegments = new List<Segment>();
        List<tm> lstTM = new List<tm>();
        fuzzymatches fuzzymatchesForm;
        string tutorialText = "Hướng dẫn"; 
        main mainForm;
        public editor(main MainForm)
        {
            InitializeComponent();
            mainForm = MainForm;  
        }

        private void editor_Load(object sender, EventArgs e)
        {
            initSize();
            editorGrid.ClearSelection();
            UpdateFont();
            openIntroduction(); 
        }

        public void initSize()
        {
            //init Size Form
        

            ////init Size panel Top 

            //panelTop.Width = ParentForm.ClientRectangle.Width / 2;

            ////init Size Grid view 
            //editorGrid.Left = 0;  
            //editorGrid.Width = recNew.Width;
            //editorGrid.Height = recNew.Height;

            //rtbTutorial.Left = 0;
            //rtbTutorial.Width = recNew.Width;
            //rtbTutorial.Height = recNew.Height; 
            //editorGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }
        public void setSentencesToGridview()
        {
            editorGrid.Rows.Clear(); 
            if (mainForm.project != null)
            {
                file a = mainForm.project.getCurrentFile();
                List<Segment> listSegs = new List<Segment>();
                listSegs = a.getListSegments();
                editorGrid.Rows.Add();
                editorGrid.RowCount = listSegs.Count;
                for (int i = 0;i < listSegs.Count; i++)
                {
                    editorGrid.Rows[i].Cells["htmlSourceColumn"].Value = listSegs[i].getTMSource(); 
                    editorGrid.Rows[i].Cells["sourceColumn"].Value = listSegs[i].getTMSource();
                    editorGrid.Rows[i].Cells["targetColumn"].Value = listSegs[i].getTMTarget();
                    editorGrid.Rows[i].Cells["confirmColumn"].Value = listSegs[i].confirm;
                }
            }
        }

        private void editorGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editorGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = editorGrid.CurrentRow.Index;
            if (Index >= 0)
            {
                if (editorGrid.Rows[Index].Cells["sourceColumn"].Value != null)
                {
                    lblStatus.Text = "Trạng thái phân đoạn";
                    string source = editorGrid.Rows[Index].Cells["sourceColumn"].Value.ToString();
                    handling(source);
                }
            }
        }

        public void openTutorial()
        {
            editorGrid.Visible = false;
            rtbTutorial.Visible = true;
            btnTranslationAll.Visible = false;
            txt txt = new txt(); 
            rtbTutorial.Text = txt.TUTORIAL_PROJECT;
            //lblEditor.Text = txt.EMPTY_PROJECT;
        }

        public void openIntroduction()
        {
            txt txt = new txt(); 
            editorGrid.Visible = false;
            lblStatus.Visible = false;
            btnTranslationAll.Visible = false;
            btnSaveAll.Visible = false; 
            rtbTutorial.Text = txt.TUTORIAL_PROJECT;
            //lblEditor.Text = txt.CAT_INTRODUCTION_LABEL; 
            rtbTutorial.Text = txt.CAT_INTRODUCTION; 
        }

        public void openEditor()
        {
            txt txt = new txt();
            rtbTutorial.Visible = false;
            editorGrid.Visible = true;
            lblStatus.Visible = true;
            btnTranslationAll.Visible = true;
            btnSaveAll.Visible = true; 
            //UpdateFont(); 
            if(mainForm != null)
            {
                file a = mainForm.project.getCurrentFile();
                groupBox1.Text = mainForm.project.getCurrentFile().getFileName(); 
            }
            setSentencesToGridview(); 
        }
        public void openEditorTutorial()
        {
            txt txt = new txt();
            rtbTutorial.Visible = true;
            editorGrid.Visible = false;
            lblStatus.Visible = false;
            groupBox1.Text = tutorialText;
            rtbTutorial.Text = txt.CAT_INTRODUCTION;
        }

        public List<Segment> getListSegment()
        {
            editorGrid.EndEdit(); 
            List<Segment> segments = new List<Segment>(); 
            foreach(DataGridViewRow row in editorGrid.Rows)
            {
                string target = null;
                string source = null; 
                if (row.Cells["targetColumn"].Value != null)
                {
                   target = row.Cells["targetColumn"].Value.ToString();
                }
                if (row.Cells["sourceColumn"].Value != null)
                {
                    source = row.Cells["sourceColumn"].Value.ToString();
                }

                Segment segment = new Segment();
                tm tm = new tm();
                tm.Source = source;
                tm.Target = target;
                if (row.Cells["confirmColumn"].Value != null)
                {
                    segment.confirm = Convert.ToBoolean(row.Cells["confirmColumn"].Value); 
                }
                segment.setTM(tm);
                segments.Add(segment); 
            }

            return segments; 
        }

        private void editorGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //int Index = e.RowIndex;
            //string SourceText = editorGrid.Rows[Index].Cells["sourceColumn"].Value.ToString();
            //string TargetText = null; 
            //tm tm = new tm();
            //Segment tmp = new Segment();
            //tm.Source = SourceText;
            //if (editorGrid.Rows[Index].Cells["targetColumn"].Value != null)
            //{
            //    TargetText = editorGrid.Rows[Index].Cells["targetColumn"].Value.ToString();
            //    if (mainForm.project != null)
            //    {
            //        tm.Target = TargetText;   
            //    }
            //}
            //tmp.setTM(tm);
            //mainForm.project.setTargetLangToCurrentFileListSegment(Index, TargetText);
            //mainForm.project.addSegmentToListSaveSegment(tmp);
        }
        public void addSegmentToTM()
        {
            //Console.WriteLine(editorGrid.CurrentRow.Index);
            editorGrid.EndEdit(); 
            int Index = editorGrid.CurrentRow.Index;
            if(editorGrid.Rows[Index].Cells["targetColumn"].Value != null && editorGrid.Rows[Index].Cells["sourceColumn"].Value != null) { 
            string targetText = editorGrid.Rows[Index].Cells["targetColumn"].Value.ToString();
            string sourceText = editorGrid.Rows[Index].Cells["sourceColumn"].Value.ToString();
            DAOTM daotm = new DAOTM();
                if (mainForm.project != null)
                {
                    string tmname = mainForm.project.getTMName();
                    tm tmp = new tm()
                    {
                        Source = sourceText,
                        Target = targetText
                    };
                    Segment segment = new Segment();
                    segment.setTM(tmp);
                    if (tmp.Source != string.Empty && tmp.Target != string.Empty && tmname != string.Empty)
                    {
                        int result = daotm.addSegmentToTM(segment, tmname);
                        if (result != 1)
                        {
                            TextOfMessageBox a = new TextOfMessageBox();
                            MessageBox.Show(a.ADD_SEGMENT_FAILED, "Cảnh báo", MessageBoxButtons.YesNo);
                        }
                        else
                        {
                            if(result == 1)
                            {
                                lblStatus.Text = "Đã lưu thành công";
                                editorGrid.Rows[Index].Cells["confirmColumn"].Value = true;
                            }
                        }
                    }
                    else
                    {
                        TextOfMessageBox a = new TextOfMessageBox();
                        MessageBox.Show(a.EMPTY_TARGET, "Cảnh báo", MessageBoxButtons.YesNo);
                    }
                }
            }
            else
            {
                TextOfMessageBox a = new TextOfMessageBox();
                MessageBox.Show(a.EMPTY_TARGET, "Cảnh báo", MessageBoxButtons.YesNo);
            }
        }
        public void addAllSegmentToTM()
        {
            List<Segment> segments = getListSegment();
            string tmname = mainForm.project.getTMName();
            DAOTM daotm = new DAOTM();
            int index = 0; 
            if (segments.Count > 0)
            {
                try
                {
                    foreach (Segment segment in segments)
                    {
                        string source = segment.getTMSource();
                        string target = segment.getTMTarget();
                        int result = 0;
                        if(!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(target))
                        {
                            result = daotm.addSegmentToTM(segment, tmname); 
                        }
                        if(result != 1)
                        {
                            break; 
                        }
                        else
                        {
                            editorGrid.Rows[index].Cells["confirmColumn"].Value = true;
                        }
                        index++; 
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex); 
                }
            }
        }
        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in editorGrid.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 16F, GraphicsUnit.Pixel);
            }
        }

        private void editorGrid_SelectionChanged(object sender, EventArgs e)
        {
            //int Index = editorGrid.CurrentRow.Index;
            //if (Index >= 0)
            //{
            //    if (editorGrid.Rows[Index].Cells["sourceColumn"].Value != null)
            //    {
            //        lblStatus.Text = "Trạng thái phân đoạn";
            //        string source = editorGrid.Rows[Index].Cells["sourceColumn"].Value.ToString();
            //        handling(source);
            //    }
            //}

        }

        private void handling(string source)
        {
            mainForm.getSourceToDictForm(source);
            setHTMLSource(source); 
            mainForm.resetTextMachineTranslationForm();
            mainForm.translationMachine(source);
            mainForm.predictSemantic(source);
            mainForm.hideRTBFuzzymatched();
            //mainForm.replacePreviewForm(getListSegment()); 
            colorSegment(); 
        }

        public void colorSegment()
        {
            int Index = editorGrid.CurrentRow.Index;
            Segment segment = mainForm.project.getCurrentFile().getSegmentByIndex(Index);
            mainForm.colorRTBRawForm(segment); 
        }

        private void setHTMLSource(string source)
        {
            int Index = editorGrid.CurrentRow.Index;
            HashSet<tb> dict = mainForm.dictionary;
            tbProc tbproc = new tbProc();
            string html = tbproc.checkTBInSegment(dict, source);
            editorGrid.Rows[Index].Cells["htmlSourceColumn"].Value = html;
        }

        public void setTargetToEditorGrid(string target)
        {
            int Index = editorGrid.CurrentRow.Index;
            if(Index >= 0)
            {
                editorGrid.Rows[Index].Cells["targetColumn"].Value = target;
            }
        }

        private void btnTranslationAll_Click(object sender, EventArgs e)
        {
            mainForm.openTranslationAllForm(); 
        }

        public void setAllTarget(List<machineTranslationResult> results)
        {
            for(int i = 0; i < results.Count; i++)
            {
                editorGrid.Rows[i].Cells["targetColumn"].Value = results[i].tag;
                editorGrid.Rows[i].Cells["confirmColumn"].Value = false; 
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            addAllSegmentToTM(); 
        }
    }

}
