using Marbles.Analysis;
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
        /// <summary>
        /// CodeView class constructor.
        /// </summary>
        public CodeView()
        {
            this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Enabled;
            Utilities.BlueCompile();
		}

		public static List<Object> BlocksWithErrorsInOrder = new List<Object>();

        /// <summary>
        /// This function is an event called whenever the user changes from a different view
        /// to this view (CodeView). Reflects on code the assets that are currently in the
        /// canvas, and enables the Compile button.
        /// </summary>
        /// <param name="e"></param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AssetListViewContainer.UpdateAssets();
            CompileButton.Background = Utilities.CompileButtonColor;
            CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
        }

        /// <summary>
        /// This function is an event called whenever the user clicks the Compile button.
        /// The function prepares to compile by resetting all necessary values back to default and then
        /// begins compilation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            Utilities.linesOfCodeCount = 0;
            Utilities.linesOfCode = new List<CodeLine>();
			ErrorPrinter.errorCount = 0;
			ErrorPrinter.errorList = new Dictionary<int, List<string>>();
			ErrorPrinter.warningList = new Dictionary<int, List<string>>();
            FunctionDirectory.Reset();
            MemoryManager.Reset();
            QuadrupleManager.Reset();
			UserControl main = new UserControl();
            
			Utilities.BlockToLineErrors.Clear();
			BlocksWithErrorsInOrder.Clear();
			Utilities.errorsInLines.Clear();

			AssetListViewContainer.PrintCode();
            VariableListViewContainer.PrintCode();
            FunctionListViewContainer.PrintCode();
            
            Utilities.linesOfCode.Add(new CodeLine("instructions {", main, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;

            InstructionListViewContainer.PrintCode();

            Utilities.linesOfCode.Add(new CodeLine("}", main, Utilities.linesOfCodeCount + 1));
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

				FillErrorsDictionary();
				SetErrorsInUI();

				// TODO: Pass errors here
				CompileButton.Background = Utilities.CompileButtonColor;
                CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
            }
        }

        /// <summary>
        /// This function connects errors to the respective UI block where the error was found.
        /// This function is called by <see cref="CompileButton_Click(object, RoutedEventArgs)"/> when
        /// errors are found.
        /// </summary>
		private void FillErrorsDictionary()
		{
			Utilities.BlockToLineErrors.Clear();
			foreach (int element in ErrorPrinter.GetErrorLines())
			{
				if (Utilities.BlockToLineErrors.ContainsKey(Utilities.linesOfCode[element - 1].owner))
				{
					List<string> addedStrings = Utilities.BlockToLineErrors[Utilities.linesOfCode[element - 1].owner].Item1;
					addedStrings = (List<string>)addedStrings.Concat(ErrorPrinter.GetErrorsAtLine(element));

					Utilities.BlockToLineErrors[Utilities.linesOfCode[element - 1].owner] = new Tuple<List<string>, SolidColorBrush>(addedStrings, Utilities.GetRandomBrushForErrors());
				}
				else
				{
					Utilities.BlockToLineErrors.Add(Utilities.linesOfCode[element - 1].owner, new Tuple<List<string>, SolidColorBrush>(ErrorPrinter.GetErrorsAtLine(element), Utilities.GetRandomBrushForErrors()));
					BlocksWithErrorsInOrder.Add(Utilities.linesOfCode[element - 1].owner);
				}
			}
		}

        /// <summary>
        /// Shows the errors found during compilation and execution in the Error Page view.
        /// This function is called by <see cref="CompileButton_Click(object, RoutedEventArgs)"/> when
        /// errors are found.
        /// </summary>
		private void SetErrorsInUI()
		{
			Utilities.errorsInLines.Clear();
			foreach (UserControl element in BlocksWithErrorsInOrder)
			{
				Utilities.SetUserControlWithError(element, Utilities.BlockToLineErrors[element].Item2);
				var errorTemplate = new ErrorTemplate();
				errorTemplate.FillTemplate(element, Utilities.BlockToLineErrors[element].Item1, Utilities.BlockToLineErrors[element].Item2);
				Utilities.errorsInLines.Add(errorTemplate);
			}
		}

        /// <summary>
        /// This function is called by <see cref="CompileButton_Click(object, RoutedEventArgs)"/> after all
        /// the blocks in the CodeView have been translated to CodeLine objects.
        /// Reads all the CodeLine objects stored in Utility so far, and writes them to a new text file
        /// on a temporary folder. The file path is then returned to the caller.
        /// </summary>
        /// <param name="filePath"></param>
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

        /// <summary>
        /// Given a file path to a text file containing code in Marbles language, runs this code
        /// through the scanner and then through the parser.
        /// This function is called by <see cref="CompileButton_Click(object, RoutedEventArgs)"/> right after
        /// it has generated the source code.
        /// </summary>
        /// <param name="filePath"></param>
		private void AnalyzeCode(string filePath)
		{
			Scanner scanner = new Scanner(filePath);
			Parser parser = new Parser(scanner);
            try { parser.Parse(); }
            catch (Exception e) { }
		}

        /// <summary>
        /// This event is called after this Page is loaded. Subscribes different elements to an
        /// event that indicates that anything from that element has been modified. This is used
        /// to reset the Compile button when it is necessary to do so.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
			ConstantBoolean.SomethingChanged += new EventHandler(SomethingChanged);
			FunctionCall.SomethingChanged += new EventHandler(SomethingChanged);
            MathExpression.SomethingChanged += new EventHandler(SomethingChanged);
            Values.SomethingChanged += new EventHandler(SomethingChanged);
            VariableCall.SomethingChanged += new EventHandler(SomethingChanged);
        }

        /// <summary>
        /// Event called when an element on the screen has been modified. If this event is fired,
        /// the compile button goes to a state in which the user must compile again in order to run the program.
        /// This event is subscribed to in <see cref="CodeViewPage_Loaded(object, RoutedEventArgs)"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SomethingChanged(object sender, EventArgs e)
        {
            Utilities.BlueCompile();
            CompileButton.Background = Utilities.CompileButtonColor;
            CompileButton.IsEnabled = Utilities.CompileButtonEnabled;
        }
    }
}
