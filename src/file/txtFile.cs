﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Files;
using src.processing;
using src.XML;
using src.segment;
using src.TM; 

namespace src.Files
{
    public class txtFile : file 
    {
        public override void readContent(string path)
        {            
            txtProc txtProc = new txtProc();
            this.content = txtProc.readTxtFile(path);

            contentpage contentpage = new contentpage();
            contentpage.content = this.content;
            contentpage.page = 1;
            listcontentpage.Add(contentpage); 

            //this.listSegments = txtProc.splitTxtContentToSegment(this.content,delimiters,this.fileName);
            this.listSegments = txtProc.splitTxtContentToSegment2(this.content, delimiters, this.fileName);
        }
        public override void loadFileSave(string path)
        {
            pathSaveFolder = path;
            if (File.Exists(path))
            {
                readwriteXML txt = new readwriteXML();
                this.listSegmentsFromSave = txt.readSegmentFromFileSave(path);

                //            //Set lai cac tm voi file save
            }
            if (listSegmentsFromSave.Count == 0)
            {
                progress = 0;
            }
            else
            {
                int count = 0;
                foreach (Segment segment in listSegmentsFromSave)
                {
                    if (!string.IsNullOrEmpty(segment.getTMTarget()))
                    {
                        count++;
                    }
                }

                progress = (double)count / listSegmentsFromSave.Count;
            }
        }

        public override void createFileTranslateDocument(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, replaceContent());
        }
        public string replaceContent()
        {
            txtProc txtProc = new txtProc();
            string contentReplace = this.content; 
            if (this.content.Length > 0)
            {
                foreach (Segment a in listSegments)
                {
                    tm tmp = a.getTM();
                    if (tmp.Target != null)
                    {
                        if (tmp.Target.Trim() != "")
                        {
                            contentReplace = txtProc.ReplaceFirst(contentReplace, tmp.Source, tmp.Target);
                        }
                    }
                }
            }
            return contentReplace; 
        }
        public override void convertToWord(string path,string tempFolder)
        {
            throw new NotImplementedException();
        }

    }
}
