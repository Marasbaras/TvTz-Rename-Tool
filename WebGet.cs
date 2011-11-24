using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;

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

        public int showid
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
            string sApiKey = "pF7fdBwtutbXFwfr9g5K";

            string URLepinfo = "http://services.tvrage.com/myfeeds/episodeinfo.php?key=" + sApiKey + "&show=" + episode + "&exact=0&ep=" + season + "x" + epnr;
            string URLsearch = "http://services.tvrage.com/myfeeds/search.php?key=" + sApiKey + "&show=" + episode;
            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                //Grabbing SID
                //xmlDoc.Load(URLsearch);
                //XmlNode showidNode = xmlDoc.SelectSingleNode("//Results/show/showid");
                //if (showidNode != null) showid = Int32.Parse(showidNode.InnerText);

                //Grabbing episode title and showname
                xmlDoc.Load(URLepinfo);
                XmlNode titleNode = xmlDoc.SelectSingleNode("//show/episode/title");
                if (titleNode != null) webName = titleNode.InnerText;
                XmlNode showNode = xmlDoc.SelectSingleNode("//show/name");
                if (showNode != null) webShow = showNode.InnerText;

            }
            catch (Exception e)
            {

                if (e.Message.Contains("Unable to connect to the remote server"))
                {
                    Logger.logError("TVrage api is down, stacktrace: " + e.ToString() + " searched for " + URLepinfo, 3);
                }
                else
                {
                    Logger.logError("Holy hell, something went wrong with getting stuff from Tvrage, stacktrace " + e.ToString() + " searched for " + URLepinfo, 3);
                }
                webName = "error";
                webShow = "error";
            }
            /*
            if (PageContent.Contains("No Show Results Were Found") | PageContent.Contains("Error") | PageContent.Contains("Unavailable") | PageContent.Contains("Maintenance"))
            {
                Logger.logError("TVrage lookup didnt give the expected result, Pagecontent: " + PageContent + " searched for " + Url, 3);
                webName = "";
            }
            */
            webName = SanitizeName(webName, 1);
            webShow = SanitizeName(webShow, 2);
        }
        private string SanitizeName(string s, int type)
        {
            //type 1 for episodename, type 2 for showname (where showname does not need the initial . at the start of the name)
            s = Regex.Replace(s, @"[%?:\/*""<>|]", ".");
            s = setSeperator(s, '.', type);
            return s;
        }

        private string setSeperator(string s, char seperator, int type)
        {
            if (type == 1)
            {
                if (!s.StartsWith("."))
                {
                    s = "." + s;
                }
            }
            StringBuilder b = new StringBuilder(s);
            b.Replace(" ", seperator.ToString());
            return b.ToString();
        }
        #endregion
    }
}
