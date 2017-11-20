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
		public static int errorCount = 0;

        /// <summary>
        /// Add a new error to the error list given its line and message.
        /// Called by <see cref="Parser"/> when an error is found.
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
        /// Called by <see cref="Parser"/> when an error is found.
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
        /// Called by <see cref="CodeView.FillErrorsDictionary"/> to populate the list of errors.
        /// </summary>
        /// <returns>A list of line numbers that contain errors.</returns>
		public static List<int> GetErrorLines()
		{
			return errorList.Keys.ToList();
		}

        /// <summary>
        /// Given a line number, returns all the errors found at that line, if any exists.
        /// Called by <see cref="CodeView.FillErrorsDictionary"/> to populate the list of errors.
        /// </summary>
        /// <param name="line"></param>
        /// <returns>A list of errors at a specific line number.</returns>
		public static List<string> GetErrorsAtLine(int line)
		{
			if (!errorList.ContainsKey(line))
			{
				throw new ArgumentException();
			}
			return errorList[line];
		}

        /// <summary>
        /// Print the list of errors in console.
        /// Called in <see cref="CodeView"/> after compiling.
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
	}
}
