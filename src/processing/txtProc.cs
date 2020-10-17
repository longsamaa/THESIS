using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Files;
using src.segment;
using src.TM;

namespace src.processing
{
    public class txtProc
    {
        //private char[] delimiters = { '.', '?', '\n', ':', '\r', '\t', '\a', '\f' };
        public string readTxtFile(string path)
        {
            try
            {
                string content = File.ReadAllText(path);
                return content; 
            }
            catch (Exception)
            {
                return null; 
            }
        }
        public List<Segment> splitTxtContentToSegment(string content, char[] delimiters,string filename)
        {
            var parts = new List<Segment>();
            //string tmp = null; 
            if (!string.IsNullOrEmpty(content))
            {
                int iFirst = 0;
                do
                {
                    int iLast = content.IndexOfAny(delimiters, iFirst);
                    if (iLast >= 0)
                    {
                        if (iLast > iFirst)
                            if (iLast != iFirst)
                            {
                                string str1 = content.Substring(iFirst, iLast - iFirst + 1).Trim();
                                if (!String.IsNullOrEmpty(str1))
                                {
                                    tm tm1 = new tm();
                                    tm1.Source = str1;
                                    Segment tmp1 = new Segment();
                                    tmp1.setTM(tm1);
                                    tmp1.file = filename;
                                    parts.Add(tmp1);
                                }
                            }
                        iFirst = iLast + 1;
                        continue;
                    }
                    string str = content.Substring(iFirst, content.Length - iFirst).Trim();
                    if (!String.IsNullOrEmpty(str))
                    {
                        tm tm = new tm();
                        tm.Source = str;
                        Segment tmp = new Segment();
                        tmp.setTM(tm);
                        tmp.file = filename;
                        parts.Add(tmp);
                    }
                    break;

                } while (iFirst < content.Length);
            }

            return parts;
        }


        public List<Segment> splitTxtContentToSegment2(string content, char[] delimiters, string filename)
        {
            var parts = new List<Segment>();
            string temp = null;
            if (!string.IsNullOrEmpty(content))
            {
                int iFirst = 0;
                do
                {
                    int iLast = content.IndexOfAny(delimiters, iFirst);
                    if (iLast >= 0)
                    {
                        if (iLast > iFirst)
                            if (iLast != iFirst)
                            {
                                string str1 = content.Substring(iFirst, iLast - iFirst + 1).Trim();
                                temp += str1;
                                if (iLast + 1 < content.Length)
                                {
                                    if (content[iLast + 1] == ' ' || delimiters.Contains(content[iLast + 1]))
                                    {
                                        if (!String.IsNullOrEmpty(temp))
                                        {
                                            tm tm1 = new tm();
                                            tm1.Source = temp;
                                            Segment tmp1 = new Segment();
                                            tmp1.setTM(tm1);
                                            tmp1.file = filename;
                                            tmp1.index = iLast - temp.Length + 1;
                                            tmp1.setPage(1); 
                                            parts.Add(tmp1);
                                            temp = null;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(temp))
                                    {
                                        tm tm1 = new tm();
                                        tm1.Source = temp;
                                        Segment tmp1 = new Segment();
                                        tmp1.setTM(tm1);
                                        tmp1.file = filename;
                                        tmp1.index = iLast - temp.Length + 1;
                                        tmp1.setPage(1);
                                        parts.Add(tmp1);
                                        temp = null;
                                    }
                                }
                            }
                        iFirst = iLast + 1;
                        continue;
                    }
                    string str = content.Substring(iFirst, content.Length - iFirst).Trim();
                    //temp += content.Substring(iFirst, content.Length - iFirst);
                    temp += str; 
                    if (!String.IsNullOrEmpty(temp))
                    {
                        tm tm = new tm();
                        tm.Source = temp;
                        Segment tmp = new Segment();
                        tmp.setTM(tm);
                        tmp.file = filename;
                        tmp.index = content.Length - temp.Length;
                        tmp.setPage(1);
                        parts.Add(tmp);
                    }
                    break;

                } while (iFirst < content.Length);
            }

            return parts;
        }




        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
