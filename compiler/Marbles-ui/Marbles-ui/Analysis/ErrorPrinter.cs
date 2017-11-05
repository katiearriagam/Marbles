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
	public static class ErrorPrinter
	{
		public static Dictionary<int, List<string> > errorList = new Dictionary<int, List<string>>();
		public static Dictionary<int, List<string>> warningList = new Dictionary<int, List<string>>();
		public static int errorCount = 0;
		public static int warningCount = 0;

		public static void AddError(int line, string error)
		{
			if (!errorList.ContainsKey(line))
			{
				errorList.Add(line, new List<string>());
			}
			errorList[line].Add(error);
			errorCount++;
		}

		public static void AddError(string error)
		{
			if (!errorList.ContainsKey(-1))
			{
				errorList.Add(-1, new List<string>());
			}
			errorList[-1].Add(error);
			errorCount++;
		}

		public static void ClearErrors()
		{
			errorList.Clear();
			errorCount = 0;
		}

		public static List<int> GetErrorLines()
		{
			return errorList.Keys.ToList();
		}

		public static List<string> GetErrorsAtLine(int line)
		{
			if (!errorList.ContainsKey(line))
			{
				throw new ArgumentException();
			}
			return errorList[line];
		}

		public static void AddWarning(int line, string warning)
		{
			if (!warningList.ContainsKey(line))
			{
				warningList.Add(line, new List<string>());
			}
			warningList[line].Add(warning);
			warningCount++;
		}

		public static void AddWarning(string warning)
		{
			if (!warningList.ContainsKey(-1))
			{
				warningList.Add(-1, new List<string>());
			}
			warningList[-1].Add(warning);
			warningCount++;
		}

		public static void ClearWarning()
		{
			warningList.Clear();
			warningCount = 0;
		}

		public static List<int> GetWarningLines()
		{
			return warningList.Keys.ToList();
		}

		public static List<string> GetWarningsAtLine(int line)
		{
			if (!warningList.ContainsKey(line))
			{
				throw new ArgumentException();
			}
			return warningList[line];
		}

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
