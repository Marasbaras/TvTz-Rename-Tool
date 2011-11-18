using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;
using System.Diagnostics;

namespace TvTzRenameTool
{
    public partial class MainForm : Form
    {
        #region nastystatics
        public bool DebugMode = true;
        public int SettingsType { get; set; }
        public List<string> fileNames = new List<string>();
        public List<string> newFileNames = new List<string>();
        public string[] episode;
        //public string[] SeasonEpisode;
        public List<string> filePaths = new List<string>();
        public string dir;
        public Stopwatch sw = new Stopwatch();
        #endregion


        #region Initialization

        private void outputListBox_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)

                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);

        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outputListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a file before pressing copy");
            }
            else
            {
                Clipboard.SetText(outputListBox.SelectedItem.ToString());
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            //if newFileNAmes is empty throw messagebox / error to hit test first.
            if (outputListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a file before pressing edit");
            }
            else
            {
                newFileNames[outputListBox.SelectedIndex] = MainForm.GetFromUser(outputListBox.SelectedItem.ToString());
                outputListBox.Items.Clear();
                UpdateOutputListBox(newFileNames);
            }

        }

        private void fileListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None; 
        }

        public bool IsFolder(string path)
        {
            return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
        }

        private void fileListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] DropContents = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            List<string> Droplist = new List<string>(DropContents);
            if (Droplist.Count == 1 && Droplist[0] != "" && IsFolder(Droplist[0]))
            {
                FolderTextBox.Text = Droplist[0].ToString();
                FolderTextBox.Text.Replace(@"\\", @"\");
                LoadFolder.PerformClick();
            }
            else
            {
                MessageBox.Show("Im sorry, only drag and drop of folders is allowed, not single files");
            }
        }


        public static string GetFromUser(string originalValue)
        {
            using (EditForm form = new EditForm())
            {
                form.TheValue = originalValue;
                if (form.ShowDialog() == DialogResult.OK)
                    return form.TheValue;
                else
                    return originalValue;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            Debug.Indent();
            Debug.WriteLine("initialize main");


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //allow drag&drop of folder
            this.fileListBox.AllowDrop = true;
            this.fileListBox.DragEnter += new DragEventHandler(fileListBox_DragEnter);
            this.fileListBox.DragDrop += new DragEventHandler(fileListBox_DragDrop);

                Logger.logError("Debugmode on, you wuss", 1);

            //loading the filebox the neat way to allow later adding of fileTypes through config file or dataset
                Logger.logError("Loading Containers and SceneNames", 1);
            List<string> fileTypes = new List<string>(GetSettings("Container"));
                Debug.WriteLine("Loaded: " + fileTypes.Count + " fileTypes");
                Logger.logError("Loaded: " + fileTypes.Count + " fileTypes", 1);


            for (int i = 0; i != fileTypes.Count; i++)
            {
                fileTypeBox.Items.Add(fileTypes[i]);
                Debug.WriteLine("Container loaded: " + fileTypes[i]);
            }
            fileTypeBox.SelectedIndex = 0;
            TestOutput.Enabled = false;
            TestSR.Enabled = false;
            DoRename.Enabled = false;

            //temp debugmode enable's for development
            if (DebugMode == true)
            {
                checkBoxTVrageLookup.Enabled = true;
            }
        }
        #endregion


        #region mainprocessing(click)

        private void folderPath_Click(object sender, EventArgs e)
        {
            //Get folder data into textbox
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.FolderTextBox.Text = folderBrowserDialog1.SelectedPath;
            }

        }


        public void LoadFolder_Click(object sender, EventArgs e)
        {
            //make sure there's at least text in the folder box, everything else is user error :D
            if (fileListBox.Items != null)
            {
                filePaths.Clear();
                fileNames.Clear();
                fileListBox.Items.Clear();
                outputListBox.Items.Clear();
            }
            if (FolderTextBox.Text != "")
            {
                //putting all files in an array, including dir, based on selected filetype
                if (fileTypeBox.Text == "all") filePaths.AddRange(Directory.GetFiles(FolderTextBox.Text, "*.*"));
                filePaths.AddRange(Directory.GetFiles(FolderTextBox.Text, "*." + fileTypeBox.Text));

                //updating the status label to show the number of loaded files
                toolStripStatusLabel1.Text = "Loaded " + (filePaths.Count) + " files";

                //clearing current listboxes
                fileListBox.Items.Clear();
                outputListBox.Items.Clear();

                //showing all files in the inputlistbox
                fileNames = getFileNames(filePaths);
                fileNames.Sort();
                for (int i = 0; i != (fileNames.Count); i++)
                {
                    fileListBox.Items.Add(fileNames[i]);
                }
                TestOutput.Enabled = true;
                TestSR.Enabled = true;
                DoRename.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                progressBar.Value = 0;
            }
            else
            {
                MessageBox.Show("No folder selected !");
            }

        }

        private void TestOutput_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Starting renaming / lookup ...";
            outputListBox.Items.Clear();
            BackgroundOutput.RunWorkerAsync();
            progressBar.Value = 0;
        }
        private void TestSR_Click(object sender, EventArgs e)
        {
            //getting search and replace texts
            string sSearch = textBoxSearch.Text.ToString();
            string sReplace = textBoxReplace.Text.ToString();
            toolStripStatusLabel1.Text = "Starting Search / Replace ...";
            //getting new filenames if they dont exist yet and populate the output filebox.
            if (outputListBox.Items.Count == 0)
            {
                newFileNames.Clear();
                newFileNames.AddRange(fileNames);
                UpdateOutputListBox(newFileNames);
            }
            //funtimes with loops and redraw

            for (int i = 0; i != (newFileNames.Count); i++)
            {
                newFileNames[i] = newFileNames[i].Replace(sSearch, sReplace);
            }
            editToolStripMenuItem.Enabled = true;
            copyToolStripMenuItem.Enabled = true;
            UpdateOutputListBox(newFileNames);
        }

        #endregion


        #region stringmanipulation
        private void UpdateOutputListBox(List<string> s)
        {
            outputListBox.Items.Clear();
            for (int i = 0; i != (s.Count); i++)
            {
                outputListBox.Items.Add(s[i]);
            }
        }

        private string removeSceneFromEpName(string fullName, string epName, List<string> sceneNames, List<string> sceneQuality, List<string> sceneCodec)
        {
            fullName = Regex.Replace(fullName, @"[^a-zA-Z0-9\.\-\s]", "");
            fullName = ReplaceChars(fullName, ' ', '.');
            //fullName = fullName.Replace("'", "");
            //fullName = fullName.Replace(",", "");
            fullName = fullName.Replace(".-.", ".");
            fullName = fullName.TrimStart('.', ',', '-');
            if (epName != "none")
            {
                epName = ReplaceChars(epName, ' ', '.');
                epName = epName.TrimStart('.', ',', '-');
            }
            bool lowestFindFirst;
            bool sceneFound = true;
            int lowestFind = 0;
            List<int> quality = checkForSceneStuff(fullName, sceneQuality);
            List<int> name = checkForSceneStuff(fullName, sceneNames);
            List<int> codec = checkForSceneStuff(fullName, sceneCodec);

            //again, this goes wrong when quality is not in front of the other scene info, but then its not scene in the first place ^^,
            if (quality.Count == 6)
            {
                lowestFind = quality[4];
                lowestFindFirst = false;
            }
            else if (quality.Count == 3)
            {
                lowestFind = quality[1];
                lowestFindFirst = true;
            }
            else if (quality.Count > 6)
            {
                Logger.logError("found more then 2 quality option for this filename " + fullName + " currently not supported, create an issue", 3);
                lowestFindFirst = false;
            }
            if (quality.Count == 0 && name.Count == 0 && codec.Count == 0)
            {
                Logger.logError("No scene info found on file: " + fullName, 3);
                sceneFound = false;
            }
            /*
             * Ok, now we have the lowest find of the scene info position
             * Also we know the complete filename and what it should be according to TVrage
             * Next is to determine if the TVrage name matches the fullName on any part.
             */
            if (removeEpInfo.Checked == false && epName != "none")
            {
                if (fullName.StartsWith(epName, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    fullName = fullName.Remove(fullName.IndexOf(epName, System.StringComparison.CurrentCultureIgnoreCase), epName.Length);
                }

                if (fullName.IndexOf(epName, System.StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    fullName = fullName.Remove(fullName.IndexOf(epName), epName.Length);
                }
                //doing nothing, since when lowestFind = 0 the Scene info is behind the ep, eg, no episode information so removing everything but container.
                else if (lowestFind == 0 && sceneFound == false)
                {
                    fullName = fullName.Remove(0, fullName.Length - 3);
                }
                //Handle the fullname when there is no scene fount at all
                else if (lowestFind == 0 && sceneFound == false && epName != "" | epName != "Empty")
                {

                }
                //this is where it gets tricky, the epName currently reported from TVrage is larger then the first start of the scene
                //this could mean 2 things 1) scene finds something like ts1 in the name, or the name is off
                else if (epName.Length > lowestFind)
                {
                    //for now, just check the difference, if its lower then 3 its save to assume that it has found more scene info then needed
                    if (epName.Length - lowestFind <= 3)
                    {
                        //    fullName = fullName.Remove(0, epName.Length);
                    }
                    else
                    {
                        fullName = fullName.Remove(0, lowestFind);
                    }
                }
                else if (epName.Length < lowestFind)
                {
                    fullName = fullName.Remove(0, lowestFind);
                }
            }
            //remove all info except scene when removeEpInfo is checked
            else if (removeEpInfo.Checked == true | removeEpInfo.Checked == true && epName == "none")
            {
                fullName = "." + fullName.Remove(0, lowestFind);

            }
            else
            {
                fullName = "." + fullName;
            }

            quality = checkForSceneStuff(fullName, sceneQuality);
            name = checkForSceneStuff(fullName, sceneNames);
            codec = checkForSceneStuff(fullName, sceneCodec);

            fullName = replaceSceneNames(fullName, quality, sceneQuality);
            fullName = replaceSceneNames(fullName, name, sceneNames);
            fullName = replaceSceneNames(fullName, codec, sceneCodec);

            return fullName;
        }

        private List<int> checkForSceneStuff(string s, List<string> scene)
        {
            List<int> sceneNamePos = new List<int>();
            for (int i = 0; i != (scene.Count); i++)
            {
                if (0 <= s.IndexOf(scene[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    //checking if it already "hit", if its closer to the start (in case of 720p.HDTV for example) it can add it (so it can handle more then 1 quality)
                    if (sceneNamePos.Count > 1 && sceneNamePos[1] != s.IndexOf(scene[i]))
                    {
                        sceneNamePos.Add(i);
                        sceneNamePos.Add(s.IndexOf(scene[i], StringComparison.CurrentCultureIgnoreCase));
                        sceneNamePos.Add(scene[i].Length);
                    }
                    else
                    {
                        //int array, pos 0 = scene pos in list, 1 = start of scene pos in string, 2 = lenght of scene name.
                        sceneNamePos.Add(i);
                        sceneNamePos.Add(s.IndexOf(scene[i], StringComparison.CurrentCultureIgnoreCase));
                        sceneNamePos.Add(scene[i].Length);
                    }
                }   
            }
            return sceneNamePos;
        }

        private string replaceSceneNames(string s, List<int> i, List<string> scene)
        {
            if (i.Count == 9)
            {
                s = s.Remove(i[1], i[2]);
                s = s.Insert(i[1], scene[i[0]]);
                s = s.Remove(i[4], i[5]);
                s = s.Insert(i[4], scene[i[3]]);
                s = s.Remove(i[7], i[8]);
                s = s.Insert(i[7], scene[i[6]]);
            }
            if (i.Count == 6)
            {
                s = s.Remove(i[1], i[2]);
                s = s.Insert(i[1], scene[i[0]]);
                s = s.Remove(i[4], i[5]);
                s = s.Insert(i[4], scene[i[3]]);
            }
            else if (i.Count == 3)
            {
                s = s.Remove(i[1], i[2]);
                s = s.Insert(i[1], scene[i[0]]);
            }
            return s;
        }

        private string EpisodeAndScene(string s, List<string> sceneNames, List<string> sceneQuality, List<string> codec)
        {
            //currently used as a nasty work-around, at the end of the method this determines if we need to add additional seperators
            bool foundScene = false;
            for (int i = 0; i != (sceneQuality.Count); i++)
            {
                //first check if it even has anything that looks like scene info. (only check atm is on Quality since thats a fairly consistant part).
                if (0 <= s.IndexOf(sceneQuality[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    foundScene = true;
                    int sceneStart = s.IndexOf(sceneQuality[i], StringComparison.CurrentCultureIgnoreCase);
                    //if the start of the scene part (quality) is higher then 1 (thus further away in the string) do this (has episode information in front of scene)
                    if (sceneStart >= 1)
                    {
                        //splitting into ep and sc strings (everything before the quality)
                        //this goes very wrong when anything when scene info is placed in front of the quality in the filename !
                        string ep = s.Substring(0, sceneStart);
                        string sc = s.Substring(sceneStart);

                        //replacing Quality found (ignore uppercase) with quality from the .config file (with uppercase)
                        sc = sc.Remove(sceneStart - ep.Length, sceneQuality[i].Length);
                        sc = sc.Insert(sceneStart - ep.Length, sceneQuality[i]);

                        ep = ep.TrimEnd('.');
                        ep = UppercaseFirst(ep);

                        //replacing scene names with the scene names from the .config file (to make sure uppercases are correct)
                        for (int ii = 0; ii != (sceneNames.Count); ii++)
                        {
                            if (0 <= sc.IndexOf(sceneNames[ii], StringComparison.InvariantCultureIgnoreCase))
                            {
                                int nameStart = sc.IndexOf(sceneNames[ii], StringComparison.CurrentCultureIgnoreCase);
                                sc = sc.Remove(nameStart, sceneNames[ii].Length);
                                sc = sc.Insert(nameStart, sceneNames[ii]);
                            }
                        }
                        //replacing codec names with the names from the .config file
                        for (int iii = 0; iii != (codec.Count); iii++)
                        {
                            if (0 <= sc.IndexOf(codec[iii], StringComparison.InvariantCultureIgnoreCase))
                            {
                                int codecStart = sc.IndexOf(codec[iii], StringComparison.CurrentCultureIgnoreCase);
                                sc = sc.Remove(codecStart, codec[iii].Length);
                                sc = sc.Insert(codecStart, codec[iii]);
                            }
                        }

                        s = ep + sc;
                    }

                    //If the start of the scene info is at 0 it should go here (at this point it doesnt have episode information).
                    else if (sceneStart == 0)
                    {
                        //replacing Quality found (ignore uppercase) with quality from the .config file (with uppercase)
                        Debug.WriteLine("Found sceneQuality at pos: " + sceneStart);
                        s = s.Remove(sceneStart, sceneQuality[i].Length);
                        s = s.Insert(sceneStart, sceneQuality[i]);
                        foundScene = true;
                        for (int ii = 0; ii != (sceneNames.Count); ii++)
                        {
                            //replacing scene names with the scene names from the .config file (to make sure uppercases are correct
                            if (0 <= s.IndexOf(sceneNames[ii], StringComparison.InvariantCultureIgnoreCase))
                            {
                                int nameStart = s.IndexOf(sceneNames[ii], StringComparison.CurrentCultureIgnoreCase);
                                Debug.WriteLine("Found sceneName at pos: " + nameStart);
                                s = s.Remove(nameStart, sceneNames[ii].Length);
                                s = s.Insert(nameStart, sceneNames[ii]);

                            }
                        }
                        //replacing codec names with the names from the .config file
                        for (int iii = 0; iii != (codec.Count); iii++)
                        {
                            if (0 <= s.IndexOf(codec[iii], StringComparison.InvariantCultureIgnoreCase))
                            {
                                int codecStart = s.IndexOf(codec[iii], StringComparison.CurrentCultureIgnoreCase);
                                s = s.Remove(codecStart, codec[iii].Length);
                                s = s.Insert(codecStart, codec[iii]);
                            }
                        }

                    }

                }
                else
                {
                    Logger.logError("Could not find any Scene or Quality info, going with defaults", 2);
                }
            }
            if (foundScene == false)
            {
                s = UppercaseFirst(s);
                s = s.TrimStart('-');
                s = s.TrimEnd('.');
            }
            else
            {
                s = "." + s;
                s = ReplaceChars(s, '_', '.');
            }
            if (s.Substring(0, 1) != ".")
            {
                s = "." + s;
            }
            return s;
        }

        private string ReplaceChars(string s, char find, char replace)
        {
            StringBuilder b = new StringBuilder(s);
            b.Replace(find, replace);
            return b.ToString();
        }

        private string UppercaseFirst(string s)
        {
            List<string> tempContainer = new List<string>(GetSettings("Container"));
            bool bIsContainer = false;
            int iIsContainer = 0;
            s = s.TrimEnd('-',' ','.');
            for (int iContainer = 0; iContainer < tempContainer.Count; iContainer++)
            {
                if (s.Contains(tempContainer[iContainer]))
                {
                    bIsContainer = true;
                    iIsContainer = tempContainer[iContainer].Length;
                }
            }
            if (bIsContainer == true && s.Length == 3)
            {
            }
            else
            {
                if  (!s.EndsWith(" ") | !s.EndsWith("."))
                {
                    s = s + ".";
                }
            }
            if (s.LastIndexOf('-') >= s.Length - 3 && s.Length > 3)
            {
                s = s.Remove(s.LastIndexOf('-'));
            }
            StringBuilder b = new StringBuilder(s);
            b.Replace(".", " ");
            b.Replace(",", " ");
            b.Replace("_", " ");
            b.Replace("  ", " ");
            b.Replace("'", "");
            string n = b.ToString();
            char[] array = n.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length - iIsContainer; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            string o = new string(array);
            StringBuilder c = new StringBuilder(o);
            c.Replace(" ", ".");
            o = c.ToString();
            o = o.TrimEnd('-');
            return o;
        }

        private string CorrectSeasonNumbering (string s)
        {
            StringBuilder b = new StringBuilder(s);
            if (IsNumeric(s) == true)
            {
                if (s.Length == 1)
                {
                    s = "S0" + s;
                }
                else if (s.Length == 2)
                {
                    s = "S" + s;
                }
                else
                {
                    Debug.WriteLine("Season is number only, but not reconised as season number" + s);
                    s = "Sxx";
                }
            }
            else
            {
                if (s.Length != 3)
                {
                    Debug.WriteLine("Unknown season format, with txt and numbers, but longer then 3" + s);
                    s = "Sxx";
                }

            }

            return s.ToUpper();
            
        }

        private string CorrectEpisodeNumbering(string s)
        {
            StringBuilder b = new StringBuilder(s);
            if (IsNumeric(s) == true)
            {
                if (s.Length == 1)
                {
                    s = "E0" + s;
                }
                else if (s.Length == 2)
                {
                    s = "E" + s;
                }
                else
                {
                    Debug.WriteLine("Episode is number only, but not reconised as Episode number" + s);
                    s = "Exx";
                }
            }
            else
            {
                if (s.Length != 3)
                {
                    Debug.WriteLine("Unknown Episode format, with txt and numbers, but longer then 3" + s);
                    s = "Exx";
                }

            }

            return s.ToUpper();
        }

        private bool IsNumeric(string chkNumeric)
        {
            int intOutVal;
            bool isValidNumeric = false;
            isValidNumeric = int.TryParse(chkNumeric, out intOutVal);
            return isValidNumeric;
        }

        private string CapsShowName(string inputShowname, List<string> capsShows) //###
        {
            string outputShowname = "";
            List<string> lShow = new List<string>(inputShowname.Split('.'));
            if (lShow[lShow.Count-1] == "") 
            {
                lShow.RemoveAt(lShow.Count-1);
            }
            for (int i = 0; i != (lShow.Count); i++)
            {
                for (int j = 0; j != (capsShows.Count); j++)
                {
                    if (lShow[i].IndexOf(capsShows[j], StringComparison.InvariantCultureIgnoreCase) != -1 && lShow[i].Length == capsShows[j].Length)
                    {
                        lShow[i] = lShow[i].ToUpper();
                    }
                }
                outputShowname = outputShowname + lShow[i].ToString() + ".";
            }
            
            return outputShowname;
        }

        #endregion


        #region fileprocessing

        public string GetTempPath()
        {
            string path = System.Environment.CurrentDirectory;
            if (!path.EndsWith("\\")) path += "\\";
            return path;

        }

        private string[] GetSettings(string SettingType) //reading .config input is type in .config
        {
            string[] setting = null;
            try
            {
                Logger.logError("Trying to load " + SettingType, 2);
                if (SettingType == "Container") setting = Properties.Settings.Default.Container.Split(',');
                if (SettingType == "SceneName") setting = Properties.Settings.Default.SceneName.Split(',');
                if (SettingType == "SceneQuality") setting = Properties.Settings.Default.SceneQuality.Split(',');
                if (SettingType == "Codec") setting = Properties.Settings.Default.Codec.Split(',');
                if (SettingType == "ShowName") setting = Properties.Settings.Default.ShowName.Split(',');
            }
            catch (InvalidCastException e)
            {
                Logger.logError(e.Message, 5);
            }
            //output is always an array (have to make this pritty someday), will always be split on ,
            return setting;
        }
        
        public List<string> getFileNames(List<string> filePaths)
        {
            //getting the directory path seperated from file list
            //first checking wether the dir path contains an ending '\'
            string charslash = "\\";
            dir = FolderTextBox.Text;
            char last = dir[dir.Length - 1];
            if (last.Equals('\\'))
            {
                charslash = "";
            }

            //and if not adding it
            else
            {
                dir = dir + charslash.ToString();
            }

            //then putting everything in a new array with only file names
            fileNames.AddRange(filePaths);
            for (int i = 0; i != (fileNames.Count); i++)
            {
                fileNames[i] = filePaths[i].Replace(dir, "");
            }
            return fileNames;
        }
        private void DoRename_Click(object sender, EventArgs e)
        {
            backgroundProcessFiles.RunWorkerAsync();
            progressBar.Value = 0;
        }
        #endregion


        #region formcontrols
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks to: \nEveryone in IRC for keeping up with my spam \n\nSpecial thnx to: \nbitgeek (for listening to my crap) \nNJL61 (for supporting me to finish this tool) \nal7air (for writing the Uploading rule O guide) \nBondoca (for testing the crap out of it) \nTvTz (for being such an awesome TV-Show tracker)");
        }
        
        private void checkBoxTVrageLookup_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTVrageLookup.Checked)
            {
                string result = "ok";
                try
                {
                    toolStripStatusLabel1.Text = "Checking TVrage connection and speed, hold on, this might take a while ...";
                    this.Update();
                    sw.Reset();
                    sw.Start();
                    WebGet tvrageApiInterface = new WebGet("The Walking Dead", "01", "01");
                    tvrageApiInterface.ConsultTVrage("The Walking Dead", "01", "01");
                    result = tvrageApiInterface.webName;
                    sw.Stop();
                    decimal dSeconds = sw.ElapsedMilliseconds / 1000M;
                    if (result != "")
                    {
                        toolStripStatusLabel1.Text = "Managed to get a sane result from TVrage, time for 1 filename is " + dSeconds.ToString() +" seconds.";
                    }
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    if (ex.Message.Contains("could not be resolved"))
                    {
                        MessageBox.Show("No connection to TVrage API, do you have internet connection ? else, TVrage might be down");
                        Logger.logError("Problem opening connection to TVrage: "+ex.Message.ToString(), 4);
                        toolStripStatusLabel1.Text = "Internet Test: Internet Borked";
                    }
                    else if (result == "")
                    {
                        MessageBox.Show("TVrage doesnt give the expected result, might be down");
                        Logger.logError("Bigass problems on the TVrage side", 4);
                        toolStripStatusLabel1.Text = "Internet Test: TVrage Borked";
                    }
                    else
                    {
                        MessageBox.Show("Some unknown problem with internet / starting up the WebGet class see log for details");
                        Logger.logError("Bigass problems, not internet down i guess, anyway, error result: " + ex.Message.ToString(), 4);
                        toolStripStatusLabel1.Text = "Internet Test: Unkown, tool borked ? nah, cant be, must be you !";
                    }
                    checkBoxTVrageLookup.Checked = false;
                }

            }
            if (checkBoxTVrageLookup.Checked == true)
            {
                if (removeEpInfo.Checked == true)
                {
                    removeEpInfo.Checked = false;
                }
            }
            else if (checkBoxTVrageLookup.Checked == false)
            {
                toolStripStatusLabel1.Text = "Not using TVrage lookup then, your call boss";
                if (checkBoxKeepScene.Checked == false)
                {
                    checkBoxKeepScene.Checked = true;
                }
            }
        }
        #endregion


        #region BackgroundOutput_Threads

        private void BackgroundOutput_DoWork(object sender, DoWorkEventArgs e)
        {
            newFileNames.Clear();
            newFileNames.AddRange(fileNames);
            int progress = 0;

            //Mandatory loading of sceneNames and Qualities, for now here, later in some loading method for everything.
            List<string> sceneNames = new List<string>(GetSettings("SceneName"));
            Debug.WriteLine("Loaded: " + sceneNames.Count + " SceneNames");
            Logger.logError("Loaded: " + sceneNames.Count + " SceneNames", 1);
            List<string> sceneQuality = new List<string>(GetSettings("SceneQuality"));
            Debug.WriteLine("Loaded: " + sceneQuality.Count + " SceneQualities");
            Logger.logError("Loaded: " + sceneQuality.Count + " SceneQualities", 1);
            List<string> codecs = new List<string>(GetSettings("Codec"));
            Debug.WriteLine("Loaded: " + sceneQuality.Count + " Codecs");
            Logger.logError("Loaded: " + sceneQuality.Count + " Codecs", 1);
            List<string> showNames = new List<string>(GetSettings("ShowName"));
            Debug.WriteLine("Loaded: " + showNames.Count + " Codecs");
            Logger.logError("Loaded: " + showNames.Count + " CAPS only Show Names", 1);

            SplitEngine fileInfo = new SplitEngine(newFileNames[0]);
            string tempName, tempSeason, tempEpisode, tempEpScene;
            sw.Reset();
          
            for (int i = 0; i != (newFileNames.Count); i++)
            {
                Logger.logError("Got " + newFileNames[i] + " splitting the filename(s) now", 1);

                //doing the actual splitting
                fileInfo.Split(newFileNames[i]);

                if (fileInfo.Result.Contains("ok") && fileInfo.Result.Length < 3)
                {

                    tempName = (UppercaseFirst(fileInfo.Name));
                    if (checkBoxTVrageLookup.Checked == false)
                    {
                        tempName = CapsShowName(tempName, showNames);
                    }
                    Logger.logError("After split and cleanup, ShowName " + tempName, 1);
                    tempSeason = CorrectSeasonNumbering(fileInfo.Season);
                    Logger.logError("After split and cleanup, Season " + tempSeason, 1);
                    tempEpisode = (CorrectEpisodeNumbering(fileInfo.Ep));
                    Logger.logError("After split and cleanup, Episode " + tempEpisode, 1);

                    //escaping into API lookup before the filename lookup/check
                    if (checkBoxTVrageLookup.Checked == true)
                    {
                        sw.Start();                        
                        WebGet tvrageApiInterface = new WebGet("tmp", "tmp", "tmp");
                        tvrageApiInterface.ConsultTVrage(tempName, tempSeason, tempEpisode);
                        sw.Stop();
                        if (tvrageApiInterface.webName == "error")
                        {
                            MessageBox.Show("hmmm, timeout on TVrage for " + newFileNames[i] + ", or some other error, doing it manually, no episode lookup");
                            tempEpScene = removeSceneFromEpName(fileInfo.Show, "none", sceneNames, sceneQuality, codecs);
                        }
                        else
                        {
                        tempEpScene = UppercaseFirst(tvrageApiInterface.webName);
                        tempName = UppercaseFirst(tvrageApiInterface.webShow);
                        }
                        
                        if (tempEpScene.Length <= 0)
                        {
                            MessageBox.Show("It seems TVrage doesnt know the episode for " + tempName + "check the log for more information");
                        }
                        else if (tempName.Length <= 0)
                        {
                            MessageBox.Show("It seems TVrage doesnt know the show for " + tempName + "check the log for more information");
                        }

                        if (checkBoxKeepScene.Checked == true)
                        {
                            tempEpScene = tempEpScene + removeSceneFromEpName(fileInfo.Show, tempEpScene, sceneNames, sceneQuality, codecs);
                        }
                        else
                        {
                            tempEpScene = tempEpScene + fileNames[i].Substring(fileNames[i].Length - 3);
                        }

                        ;
                    }
                    else
                    {
                        //tempEpScene = (EpisodeAndScene(fileInfo.Show, sceneNames, sceneQuality, codecs));
                        tempEpScene = removeSceneFromEpName(fileInfo.Show, "none", sceneNames, sceneQuality, codecs);
                    }
                    Logger.logError("After split and cleanup, Epname+Sceneinfo " + tempEpScene, 1);

                    newFileNames[i] = tempName + tempSeason + tempEpisode + tempEpScene;
                }
                else
                {
                    newFileNames[i] = fileInfo.Result;
                    MessageBox.Show("Unable to split " + newFileNames[i] + " keeping origional filename, rename manually");
                }
                //Moving the progress bar
                progress++;
                double dIndex = (double)progress;
                double dTotal = (double)newFileNames.Count;
                double dProgressPercentage = (dIndex / dTotal);
                int iProgressPercentage = (int)(dProgressPercentage * 100);
                BackgroundOutput.ReportProgress(iProgressPercentage, newFileNames[i]);
                
            }
        }

        private void BackgroundOutput_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            this.outputListBox.Items.Add(e.UserState);
        }

        private void BackgroundOutput_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (checkBoxTVrageLookup.Checked == true)
            {
                decimal dSeconds = sw.ElapsedMilliseconds / 1000M;
                decimal dSecPerEp = dSeconds / newFileNames.Count;
                toolStripStatusLabel1.Text = "Finished preparing the rename ! it took " + dSeconds.ToString() + " seconds, " + dSecPerEp.ToString() + " Seconds average per file (to lookup at TVrage)";
            }
            else
            {
                toolStripStatusLabel1.Text = "Finished preparing the rename !";
            }
            DoRename.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            copyToolStripMenuItem.Enabled = true;
        }



        private void backgroundProcessFiles_DoWork(object sender, DoWorkEventArgs e)
        {
            int progress = 0;
            if (fileNames.Count == newFileNames.Count)
            {
                bool fileError = false;
                toolStripStatusLabel1.Text = "Starting moving / copying of " + newFileNames.Count + " files";
                for (int i = 0; i != fileNames.Count; i++)
                {
                    
                    try
                    {
                        if (checkBoxCopy.Checked == false)
                        {
                            File.Move(dir + fileNames[i], dir + newFileNames[i]);
                            Logger.logError("Renamed " + fileNames[i] + " to " + newFileNames[i], 1);
                            int movedcount = i + 1;
                            toolStripStatusLabel1.Text = "moved " + movedcount + "/" + fileNames.Count + " files ";

                            progress++;
                            double dIndex = (double)progress;
                            double dTotal = (double)newFileNames.Count;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            backgroundProcessFiles.ReportProgress(iProgressPercentage, "false");
                        }
                        if (checkBoxCopy.Checked == true)
                        {
                            Directory.CreateDirectory(dir + "RenamedFiles"); 
                            File.Copy(dir + fileNames[i], dir + "RenamedFiles\\" + newFileNames[i],true);
                            Logger.logError("Copied " + fileNames[i] + " to " + newFileNames[i], 1);
                            int movedcount = i + 1;
                            toolStripStatusLabel1.Text = "Copied " + movedcount + "/" + fileNames.Count + " files ";

                            progress++;
                            double dIndex = (double)progress;
                            double dTotal = (double)newFileNames.Count;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            backgroundProcessFiles.ReportProgress(iProgressPercentage, "false");
                        }

                    }
                    catch (IOException ex)
                    {
                        //check if message is for a File IO
                        Logger.logError(ex.Message.ToString(), 4);
                        fileError = true;
                        if (ex.Message.Contains("The process cannot access the file"))
                        {
                            MessageBox.Show("File ----->" + fileNames[i] + "<----- is currently in use, cannot rename, skipping...");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    //Allowing the user to click Rename again if its only 1 file that needs to be processed.

                }
                if (fileError == true && fileNames.Count == 1)
                {
                    backgroundProcessFiles.ReportProgress(100, "true");

                }
                else
                {
                    backgroundProcessFiles.ReportProgress(100, "false");
                }
            }
            else
            {
                Logger.logError("Mayor fuckup somewhere, i've got " + fileNames.Count + " old filenames and " + newFileNames.Count + " new filename, they should be the same !", 4);
            }

        }

        private void backgroundProcessFiles_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == "true")
            {
                DoRename.Enabled = true;
                TestOutput.Enabled = true;
                TestSR.Enabled = true;
            }
            else
            {
                DoRename.Enabled = false;
                TestSR.Enabled = false;
                TestOutput.Enabled = false;
            }
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundProcessFiles_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Finished, processed all files !";
        }

        #endregion

        private void removeEpInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (removeEpInfo.Checked == true)
            {
                if (checkBoxTVrageLookup.Checked == true)
                {
                    checkBoxTVrageLookup.Checked = false;
                }
                if (checkBoxKeepScene.Checked == false)
                {
                    checkBoxKeepScene.Checked = true;
                }
            }
        }

        private void checkBoxKeepScene_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeepScene.Checked == false)
            {
                if (checkBoxTVrageLookup.Checked == false)
                {
                    checkBoxTVrageLookup.Checked = true;
                }
            }
        }
    }

}