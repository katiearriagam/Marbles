﻿using Marbles.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    /// View with graphical input blocks for the user to drag and drop into instructions.
    /// </summary>
    public sealed partial class CodeView : Page
    {
		public CodeView()
        {
            this.InitializeComponent();
			this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            Utilities.BlueCompile();
		}



		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AssetListViewContainer.UpdateAssets();
            CompileButton.Background = Utilities.CompileButtonColor;
            CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
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
            
            WriteCodeToFile(out string filePath);
            /*
            string directoryPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "MarblesOutput");
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "testMarblesCode.txt");
			*/
            AnalyzeCode(filePath);
            
            MemoryManager.PrintMemory();
            QuadrupleManager.PrintQuadruples();

            Debug.WriteLine(ErrorPrinter.errorCount + " error(s) and " + ErrorPrinter.warningCount + " warning(s) found.");
            ErrorPrinter.PrintWarnings();
            ErrorPrinter.PrintErrors();

            if (ErrorPrinter.errorCount == 0)
            {
                Utilities.GreenCompile();
                Utilities.EnableRunButton();
                CompileButton.Background = Utilities.CompileButtonColor;
                CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
            }
            else
            {
                Utilities.RedCompile();
                Utilities.DisableRunButton();
                
                // TODO: Pass errors here
                CompileButton.Background = Utilities.CompileButtonColor;
                CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
            }
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
            try { parser.Parse(); }
            catch (Exception e) { ErrorPrinter.AddError(e.Message); }
		}

        private void CodeViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFunction.SomethingChanged += new EventHandler(SomethingChanged);
            CreateVariable.SomethingChanged += new EventHandler(SomethingChanged);
            DeleteBlockButton.SomethingChanged += new EventHandler(SomethingChanged);
            FunctionInstructionList.SomethingChanged += new EventHandler(SomethingChanged);
            InstructionListView.SomethingChanged += new EventHandler(SomethingChanged);
            VariableList.SomethingChanged += new EventHandler(SomethingChanged);
            AssetAttribute.SomethingChanged += new EventHandler(SomethingChanged);
            AssetFunction.SomethingChanged += new EventHandler(SomethingChanged);
            BooleanExpression.SomethingChanged += new EventHandler(SomethingChanged);
            ConstantNumber.SomethingChanged += new EventHandler(SomethingChanged);
            ConstantText.SomethingChanged += new EventHandler(SomethingChanged);
            FunctionCall.SomethingChanged += new EventHandler(SomethingChanged);
            MathExpression.SomethingChanged += new EventHandler(SomethingChanged);
            Values.SomethingChanged += new EventHandler(SomethingChanged);
            VariableCall.SomethingChanged += new EventHandler(SomethingChanged);
        }

        private void SomethingChanged(object sender, EventArgs e)
        {
            Utilities.BlueCompile();
            CompileButton.Background = Utilities.CompileButtonColor;
            CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
        }
    }
}
