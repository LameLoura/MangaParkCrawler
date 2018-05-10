using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Models
{
    public class MangaPark
    {
        public string Key { get; set; }
        public string Link { get;  set; }
        public int LatestChapter { get;  set; }
        public int ReadChapter { get; set; }
        public string PicLink { get;  set; }

        public int UnreadChapter { get { return LatestChapter - ReadChapter; } }

        //http://mangapark.me/manga/shishunki-na-adam  -- format example
        string _name;
        public string Name { get { return _name; } set { _name = value; } }

        //for XML serialization
        public MangaPark()
        {
        }

        public MangaPark(string Link) : this( Link, 0 )
        {
            
        }

        public MangaPark(string Link, int LatestChapter)
        {
            this.Link = Link;
            this.LatestChapter = LatestChapter;

            _name = getCoolMangaName(Link);
        }

        public void GetLatestChapter()
        {

            int latestChap = 0;
            string htmlCode = string.Empty;
            using (WebClient client = new WebClient())
            {
                //client.QueryString.Add("p", "1500"); //add parameters
                //string htmlCode = client.DownloadString( "http://mangapark.me/manga/ganbare-nakamura-kun" );// this.Link );
                htmlCode = client.DownloadString(this.Link);


            }

            //find all the chapter available
            Regex pattern = new Regex(@"<a .*>ch.(.*?)<\/a>", RegexOptions.Multiline);
            MatchCollection matchList = pattern.Matches(htmlCode);
            foreach (Match match in matchList)
            {

                XElement linkNode = XDocument.Parse(match.Value).Root;

                String chapterString = String.Concat(linkNode.Nodes()).Substring(("ch.").Length);

                //sometimes it has 12v2 or something ** ignore it LOL
                int chapter = 0;
                int.TryParse(chapterString, out chapter);
                if (chapter > latestChap)
                    latestChap = chapter;
            }
            LatestChapter = latestChap;
           // return latestChap;
        }

        public static string StripForKey( string link )
        {
            string target = "manga/";
            string answer = string.Empty;
            try
            {
                answer = link.Substring(target.Length + link.IndexOf(target));
            }
            catch (Exception ex)
            {
                //LOL be confused
            }
            return answer;
        }

        #region serealization 
        public void SaveData()  // unused
        {

            // Insert code to set properties and fields of the object.
            XmlSerializer mySerializer = new XmlSerializer(typeof(MangaPark));

            // To write to a file, create a StreamWriter object.
            //StreamWriter myWriter = new StreamWriter(@"D:\test\myFileName.xml");
            string fileName = System.IO.Directory.GetCurrentDirectory() + "\\" + ( "userData.xml");
            using (StreamWriter myWriter = new StreamWriter(fileName))
            {
                mySerializer.Serialize(myWriter, this );
                myWriter.Close();
            }
        }
        #endregion

        #region very private stuffs
        public void UpdateDisplayName()
        {
            _name = getCoolMangaName(Link);
        }

        private string getCoolMangaName(string link)
        {

            Key = link.Substring(link.LastIndexOf('/')).TrimStart('/');
            StringBuilder answer = new StringBuilder();

            Boolean isNextCapital = true;
            foreach (char letter in Key)
            {
                if (letter == '-')
                {
                    answer.Append(" ");
                    isNextCapital = true;
                }
                else
                {
                    String newLetter = letter.ToString();
                    if (isNextCapital)
                    {
                        isNextCapital = false;
                        newLetter = letter.ToString().ToUpper();
                    }
                    answer.Append(newLetter);
                }
            }


            return answer.ToString();
            // _name = Link.Substring(Link.LastIndexOf('/'));
        }

        //private string getCoolMangaName(string link)
        //{
        //    StringBuilder answer = new StringBuilder();
        //    string[] words = link.Split(' ');
        //    foreach (string word in words)
        //    {
        //        string newWord = 
        //    }
        //}
        #endregion very private stuffs
    }
}
