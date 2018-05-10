using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Xml.Serialization;
using System.IO;

namespace UserData
{
    public class UserDataProvider
    {
        private static string USER_DATA_PATH = System.IO.Directory.GetCurrentDirectory() + "\\" + ("userData.xml");

       
        private static List<MangaPark> _userData = null;
        public static List<MangaPark> USER_DATA
        {
            get 
            {
                if (_userData == null)
                {

                    string text = File.ReadAllText(USER_DATA_PATH, Encoding.UTF8);
                    XmlSerializer x = new XmlSerializer(typeof(List<MangaPark>));
                    _userData = (List<MangaPark>)x.Deserialize(new StringReader(text));
                }
                return _userData;
            }
        }
    }
}
