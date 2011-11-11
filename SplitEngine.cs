using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TvTzRenameTool
{
    public class SplitEngine
    {
        #region Member data
        List<string> ExcludeYears = new List<string>();
        int FoundYear = -1;
        #endregion

        #region Properties        
        
        public string Name
        {
            get;
            private set;
        }

        public string Season
        {
            get;
            private set;
        }

        public string Ep
        {
            get;
            private set;
        }

        public string Show
        {
            get;
            private set;
        }


        #endregion

        #region Constructor

        public SplitEngine(string fileName)
        {
            this.ExcludeYears.Add("2007");
            this.ExcludeYears.Add("2008");
            this.ExcludeYears.Add("2009");
            this.ExcludeYears.Add("2010");
            this.ExcludeYears.Add("2011");
        }

        #endregion

        #region Mainfunction

        public void Split(string fileName)
        {
            int regexMatch = 0;
            List<string> epName = new List<string>();
            //string[] epName = new string[5]; 
            //finds: 3x23 s3x23 3e23 s3e23 s04e01e02e03 *(and capital letters)
            Regex firstRegex = new Regex(@"(^|[^a-z])[sS]?(?<s>[0-9]+)[eEx](?<e>[0-9]{2,})(e[0-9]{2,})*[^a-z]", System.Text.RegularExpressions.RegexOptions.Compiled);
            //finds: 323 or s323 for season 3, episode 23. 2004 for season 20, episode 4.
            Regex secondRegex = new Regex(@"(^|[^a-z])s?(?<s>[0-9]+)(?<e>[0-9]{2,})[^a-z]", System.Text.RegularExpressions.RegexOptions.Compiled);
            //finds: se3e23 se323 se1ep1 se01xep01...
            Regex thirdRegex = new Regex(@"(^|[^a-z])[sSeE](?<s>[0-9]+)([ex]|ep|xep)?(?<e>[0-9]+)[^a-z]", System.Text.RegularExpressions.RegexOptions.Compiled);
            //Going into hail mary mode, gets everything that resembles a season and ep number
            Regex lastRegex = new Regex(@"(\d+)(x|e|-|\.)(\d+)", System.Text.RegularExpressions.RegexOptions.Compiled);



            /*Check if the regex matches a filename, if so splits it into a list on all hits
             this results in a list of 4 (if correct) with title, season, episode, rest
             NO CHECK IS DONE ON !=title in first list entry yet*/
            if (firstRegex.IsMatch(fileName))
            {
                regexMatch = 1;
                Logger.logError("Match on first regex performed, 3x23 s3x23 3e23 s3e23 s04e01e02e03", 1);
                epName = new List<string>(firstRegex.Split(fileName));
            }
            else if (secondRegex.IsMatch(fileName))
            {
                regexMatch = 2;
                Logger.logError("Match on second regex performed, 323 or s323 for season 3, episode 23. 2004 for season 20, episode 4.", 1);

                //quick and dirty fix for hawai-5-0, issue #28
                epName = repairYearInEp(fileName, secondRegex, epName);
            }
            else if (thirdRegex.IsMatch(fileName))
            {
                regexMatch = 3;
                Logger.logError("Match on first regex performed, 3x23 s3x23 3e23 s3e23 s04e01e02e03", 1);
                epName = new List<string>(thirdRegex.Split(fileName));
            }
            else if (lastRegex.IsMatch(fileName))
            {
                regexMatch = 99;
                Logger.logError("Match on last regex performed, triggers on basicly anything with numbers, trying to extract info ...", 1);
                epName = new List<string>(lastRegex.Split(fileName));
                repairResult(epName);
            }
            else
            {
                regexMatch = 0;
                epName.Add("empty");
                epName.Add("empty");
                epName.Add("empty");
                epName.Add("empty");
                Logger.logError("Could not split the filename into seasonName, Seaon, Episode and Episode name, maybe a weird layout ?", 4);
            }
            for (int i = 0; i != (epName.Count); i++)
            {
                if (epName[i].Equals(".")) epName.RemoveAt(i);
                if (epName[i].Equals(" ")) epName.RemoveAt(i);
                if (epName[i].Equals("_")) epName.RemoveAt(i);
                if (epName[i].Equals("e", StringComparison.InvariantCultureIgnoreCase)) epName.RemoveAt(i);
                if (epName[i].Equals("ep", StringComparison.InvariantCultureIgnoreCase)) epName.RemoveAt(i);
                if (epName[i].Equals("s", StringComparison.InvariantCultureIgnoreCase)) epName.RemoveAt(i);
                if (epName[i].Equals("se", StringComparison.InvariantCultureIgnoreCase)) epName.RemoveAt(i);
                if (epName[i].Equals("x", StringComparison.InvariantCultureIgnoreCase)) epName.RemoveAt(i);
            }

            if (epName.Count != 4)
            {
                Logger.logError("Error, got more results from the regex split then allowed (4), will continue, but results are probably wrong", 4);
                if (epName.Count == 3)
                {
                    epName[3] = "empty";
                }
            }
            //quick and dirty fix for example deads01E02rest, this will make the regex split to 5, containing, deads + 0 + 1 + E02 + rest.
            if (epName[1] == "0" | epName[1] == "00" && epName.Count == 5)
            {
                epName.RemoveAt(1);
            }

            Name = epName[0];
            Season = epName[1];
            Ep = epName[2];
            Show = epName[3];
        }

        #endregion

        #region resultfixes
        private List<string> repairResult(List<string> epName)
        {
            for (int i = 0; i != epName.Count; i++)
            {
                if (epName[i].Length == 1)
                {
                    char[] tmpChar = epName[i].ToCharArray();
                    if (char.IsLetter(tmpChar[0]))
                    {
                        epName.RemoveAt(i);
                        i--;
                    }
                }
            }
            epName[0] = epName[0].TrimEnd('s', 'S');

            return epName;
        }

        
        private List<string> repairYearInEp(string fileName, Regex secondRegex, List<string> epName)
        {
            for (int i = 0; i < ExcludeYears.Count; i++)
            {
                if (fileName.Contains(ExcludeYears[i]))
                {
                    fileName.Replace(ExcludeYears[i], "yyyy");
                    if (secondRegex.IsMatch(fileName))
                    {
                        FoundYear = i;
                    }
                    fileName.Replace("yyyy", ExcludeYears[i]);

                }
            }
            if (FoundYear != -1)
            {
                fileName = fileName.Replace(ExcludeYears[FoundYear], "yyyy");
                epName = new List<string>(secondRegex.Split(fileName));
                epName[0] = epName[0].Replace("yyyy", ExcludeYears[FoundYear]);
                FoundYear = -1;
            }
            else
            {
                epName = new List<string>(secondRegex.Split(fileName));
                if (epName[1] == "S" | epName[1] == "s")
                {
                    epName.RemoveAt(1);
                }
            }
            return epName;
        }
        #endregion
    }
}
