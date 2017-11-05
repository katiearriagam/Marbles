using Marbles.Analysis;
using Marbles.MemoryManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Marbles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeView : Page
    {
		public CodeView()
        {
            this.InitializeComponent();
			this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AssetListViewContainer.UpdateAssets();
		}

        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            Utilities.linesOfCodeCount = 0;
            Utilities.linesOfCode = new ArrayList();
			ErrorPrinter.errorCount = 0;
			ErrorPrinter.errorList = new Dictionary<int, List<string>>();
			ErrorPrinter.warningList = new Dictionary<int, List<string>>();
            FunctionDirectory.Reset();
            MemoryManager.Reset();
            QuadrupleManager.Reset();
			UserControl main = new UserControl();

            AssetListViewContainer.PrintCode();
            VariableListViewContainer.PrintCode();
            FunctionListViewContainer.PrintCode();

            Utilities.linesOfCode.Add(new CodeLine("instructions {", main));
            Utilities.linesOfCodeCount++;

            InstructionListViewContainer.PrintCode();

            Utilities.linesOfCode.Add(new CodeLine("}", main));
            Utilities.linesOfCodeCount++;
			string filePath;
            WriteCodeToFile(out filePath);
			// AnalyzeCode(filePath);
        }

        private void WriteCodeToFile(out string filePath)
        {
            List<string> linesToOutput = new List<string>();
            foreach (CodeLine line in Utilities.linesOfCode)
            {
                linesToOutput.Add(line.content);
            }

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            
            string directoryPath = Path.Combine(localFolder.Path, "MarblesOutput");
            Directory.CreateDirectory(directoryPath);
            filePath = Path.Combine(directoryPath, "OutputCode.txt");

            // if a file already exists, erase its contents to start a new one
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
            }
            File.WriteAllLines(filePath, linesToOutput);
        }

		private void AnalyzeCode(string filePath)
		{
			Scanner scanner = new Scanner(filePath);
			Parser parser = new Parser(scanner);
			parser.Parse();

            

            Debug.WriteLine("---- QUADRUPLES START ----");
            QuadrupleManager.PrintQuadruples();
            Debug.WriteLine("---- QUADRUPLES END ----");

			Debug.WriteLine(ErrorPrinter.errorCount + " error(s) and " + ErrorPrinter.warningCount + " warning(s) found.");
			foreach (int warningLine in ErrorPrinter.GetWarningLines())
			{
				foreach (string warning in ErrorPrinter.GetWarningsAtLine(warningLine))
				{
					Debug.WriteLine("Warning in line " + warningLine + ": " + warning);
				}
			}
			foreach (int errorLine in ErrorPrinter.GetErrorLines())
			{
				foreach (string error in ErrorPrinter.GetErrorsAtLine(errorLine))
				{
					Debug.WriteLine("Error in line " + errorLine + ": " + error);
				}
			}
		}
    }
}
