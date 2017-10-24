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
    public sealed partial class MainPage : Page
    {
		public MainPage()
        {
            this.InitializeComponent();
		}

        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            Utilities.linesOfCodeCount = 0;
            Utilities.linesOfCode = new ArrayList();

            UserControl main = new UserControl();
            AssetListViewContainer.PrintCode();
            VariableListViewContainer.PrintCode();
            FunctionListViewContainer.PrintCode();

            Utilities.linesOfCode.Add(new CodeLine("instructions {", main));
            Utilities.linesOfCodeCount++;

            InstructionListViewContainer.PrintCode();

            Utilities.linesOfCode.Add(new CodeLine("}", main));
            Utilities.linesOfCodeCount++;

            List<string> linesToOutput = new List<string>();
            foreach(CodeLine line in Utilities.linesOfCode)
            {
                linesToOutput.Add(line.content);
                Debug.WriteLine(line.content);
            }
            string directoryName = "MarblesTemp";
            string directoryPath = Path.Combine(Path.GetTempPath(), directoryName);
            Directory.CreateDirectory(directoryPath);
            string filePath= Path.Combine(directoryPath, "OutputCode.txt");
            if (File.Exists(filePath))
            {
                // erase the contents if it already exists
                File.WriteAllText(filePath, string.Empty);
            }
            File.WriteAllLines(filePath, linesToOutput);
        }
    }
}
