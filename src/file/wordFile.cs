﻿using src.segment;
using src.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.processing; 
namespace src.Files
{
    public class wordFile : file
    {
        public List<Segment> listSegmentHeaderAndFooter; 
        public override void readContent(string path)
        {
            List<string> listContentPage = new List<string>(); 
            wordProc wordproc = new wordProc();
            listContentPage = wordproc.readDocFile(path);

            if (listContentPage != null)
            {
                int page = 1;
                foreach (string contentPage in listContentPage)
                {
                    content = content + contentPage;
                    Console.WriteLine(contentPage.Length); 
                    contentpage contentpage = new contentpage();
                    contentpage.content = contentPage;
                    contentpage.page = page;
                    this.listcontentpage.Add(contentpage); 
                    List<Segment> tmp = new List<Segment>();
                    tmp = wordproc.splitTxtContentToSegment(contentPage, delimiters, page,this.fileName);
                    listSegments.AddRange(tmp);
                    page++;
                }
            }
        }
        public override void loadFileSave(string path)
        {
            pathSaveFolder = path; 
            if (File.Exists(path))
            {
                readwriteXML txt = new readwriteXML();
                this.listSegmentsFromSave = txt.readSegmentFromFileSave(path);
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
            //Copy file goc
            string sourceFile = this.filePath;
            string desFile = path;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Copy(sourceFile, desFile);
            //string destinationFile = this.file
            replaceContent(path); 
        }

        public void replaceContent(string path)
        {
            wordProc wordproc = new wordProc();
            wordproc.replaceContent(listSegments, path);
        }
        public override void convertToWord(string path,string tempFolder)
        {
            throw new NotImplementedException();
        }

    }
}
