using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace TvTzRenameTool
{
    public class WebGet
    {
        #region Member data

        #endregion

        #region Properties

        public string webName
        {
            get;
            private set;
        }

        public string webSeason
        {
            get;
            private set;
        }

        public string webEpnr
        {
            get;
            private set;
        }

        public string webShow
        {
            get;
            private set;
        }
        #endregion

        #region Constructor

        public WebGet (string episode, string season, string epnr)
        {
            //ConsultTVrage(episode, season, epnr);
        }

        #endregion

        #region Initialization

        public void ConsultTVrage(string episode,string season,string epnr)
        {
            epnr = epnr.TrimStart('e');
            epnr = epnr.TrimStart('E');
            season = season.TrimStart('s');
            season = season.TrimStart('S');

            string URLString = "http://services.tvrage.com/myfeeds/episodeinfo.php?key=pF7fdBwtutbXFwfr9g5K&show=" + episode + "&exact=0&ep=" + season + "x" + epnr;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(URLString);
            XmlNode titleNode = xmlDoc.SelectSingleNode("//show/episode/title");
            if (titleNode != null) webName = titleNode.InnerText;

            /*
            if (PageContent.Contains("No Show Results Were Found") | PageContent.Contains("Error") | PageContent.Contains("Unavailable") | PageContent.Contains("Maintenance"))
            {
                Logger.logError("TVrage lookup didnt give the expected result, Pagecontent: " + PageContent + " searched for " + Url, 3);
                webName = "";
            }
            */
            webName = Regex.Replace(webName, @"[%?:\/*""<>|]", ".");
            webName = setSeperator(webName, '.');
        }

        private string setSeperator(string s, char seperator)
        {
            if (!s.StartsWith("."))
            {
                s = "." + s;
            }
            StringBuilder b = new StringBuilder(s);
            b.Replace(" ", ".");
            return b.ToString();
        }
        #endregion
    }
}
