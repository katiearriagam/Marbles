using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Marbles.Analysis
{
    /// <summary>
    /// This class helps us keep track of each error found along with the line in the source
    /// code file generated where the error was found.
    /// </summary>
	public static class ErrorPrinter
	{
		public static Dictionary<int, List<string> > errorList = new Dictionary<int, List<string>>();
		public static Dictionary<int, List<string>> warningList = new Dictionary<int, List<string>>();
		public static int errorCount = 0;
		public static int warningCount = 0;

        /// <summary>
        /// Add a new error to the error list given its line and message.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="error"></param>
		public static void AddError(int line, string error)
		{
			if (!errorList.ContainsKey(line))
			{
				errorList.Add(line, new List<string>());
			}
			errorList[line].Add(error);
			errorCount++;
		}

        /// <summary>
        /// Add a new error to the error list given only its message.
        /// </summary>
        /// <param name="error"></param>
		public static void AddError(string error)
		{
			if (!errorList.ContainsKey(-1))
			{
				errorList.Add(-1, new List<string>());
			}
			errorList[-1].Add(error);
			errorCount++;
		}

        /// <summary>
        /// Reset the error list.
        /// </summary>
		public static void ClearErrors()
		{
			errorList.Clear();
			errorCount = 0;
		}

        /// <summary>
        /// Returns a list of the lines in which each of the errors was found.
        /// </summary>
		public static List<int> GetErrorLines()
		{
			return errorList.Keys.ToList();
		}

        /// <summary>
        /// Given a line number, returns all the errors found at that line, if any exists.
        /// </summary>
        /// <param name="line"></param>
		public static List<string> GetErrorsAtLine(int line)
		{
			if (!errorList.ContainsKey(line))
			{
				throw new ArgumentException();
			}
			return errorList[line];
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="warning"></param>
		public static void AddWarning(int line, string warning)
		{
			if (!warningList.ContainsKey(line))
			{
				warningList.Add(line, new List<string>());
			}
			warningList[line].Add(warning);
			warningCount++;
		}

        /// <summary>
        /// Add a new warning to the warning list given only its message.
        /// </summary>
        /// <param name="warning"></param>
		public static void AddWarning(string warning)
		{
			if (!warningList.ContainsKey(-1))
			{
				warningList.Add(-1, new List<string>());
			}
			warningList[-1].Add(warning);
			warningCount++;
		}

        /// <summary>
        /// Reset the warning list.
        /// </summary>
		public static void ClearWarning()
		{
			warningList.Clear();
			warningCount = 0;
		}

        /// <summary>
        /// Returns a list of the lines in which each of the warnings was found.
        /// </summary>
        /// <returns></returns>
		public static List<int> GetWarningLines()
		{
			return warningList.Keys.ToList();
		}

        /// <summary>
        /// Given a line number, returns all the warnings found at that line, if any exists.
        /// </summary>
        /// <param name="line"></param>
		public static List<string> GetWarningsAtLine(int line)
		{
			if (!warningList.ContainsKey(line))
			{
				throw new ArgumentException();
			}
			return warningList[line];
		}

        /// <summary>
        /// Print the list of errors in console.
        /// </summary>
        public static void PrintErrors()
        {
            foreach (int errorLine in GetErrorLines())
            {
                foreach (string error in GetErrorsAtLine(errorLine))
                {
                    Debug.WriteLine("Error in line " + errorLine + ": " + error);
                }
            }
        }

        /// <summary>
        /// Print the list of warnings in console.
        /// </summary>
        public static void PrintWarnings()
        {
            foreach (int warningLine in GetWarningLines())
            {
                foreach (string warning in GetWarningsAtLine(warningLine))
                {
                    Debug.WriteLine("Warning in line " + warningLine + ": " + warning);
                }
            }
        }
	}
}
