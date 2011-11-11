using System.IO;
using System.Configuration;
using System;
using System.Diagnostics;

namespace TvTzRenameTool
{
    public class Logger
    {
        /*
         * Loglevels:
         * 1 info
         * 2 tbd -> user tips / hints ?
         * 3 warning
         * 4 error
         * 5 critical
         * 6 not used yet
         */
        private static System.IO.StreamWriter _Output = null;
        private static Logger _Logger = null;
        private static Object _classLock = typeof(Logger);
        public static string _LogFile = "TvTzRenameToolLogger.log";
        public static int _LogLevel = Properties.Settings.Default._LogLevel;

        private Logger()
        {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            Debug.Indent();
        }

        public static Logger getInstance()
        {

            //lock object to make it thread safe
            lock (_classLock)
            {
                if (_Logger == null)
                {
                    _Logger = new Logger();

                }
            }
            return _Logger;
        }

        public static void logError(string s, int severity)
        {

            try
            {
                if (severity >= _LogLevel)
                {
                    if (_Output == null)
                    {
                        _Output = new System.IO.StreamWriter(_LogFile, true, System.Text.UnicodeEncoding.Default);
                    }

                    _Output.WriteLine(System.DateTime.Now + " | " + severity + " | " + s, new object[0]);

                    if (_Output != null)
                    {
                        _Output.Close();
                        _Output = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void closeLog()
        {
            try
            {
                if (_Output != null)
                {
                    _Output.Close();
                    _Output = null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

}
