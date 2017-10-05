using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SemanticCube
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

			List<Utilities.DataTypes> DataTypesList = Enum.GetValues(typeof(Utilities.DataTypes)).Cast<Utilities.DataTypes>().ToList();
			List<Utilities.Operators> OperatorsList = Enum.GetValues(typeof(Utilities.Operators)).Cast<Utilities.Operators>().ToList();
			TypeTypeOperator tto;
			try
			{
				foreach (Utilities.DataTypes typeOne in DataTypesList)
				{
					foreach (Utilities.DataTypes typeTwo in DataTypesList)
					{
						foreach (Utilities.Operators op in OperatorsList)
						{
							tto = new TypeTypeOperator(typeOne, typeTwo, op);

							try
							{
								Debug.WriteLine(typeOne + ", " +
											  typeTwo + ", " +
											  Utilities.GetOperatorVisualRepresentation(op) + "   \t-->\t" +
											  SemanticCube.AnalyzeSemantics(tto));
							}
							catch (System.ArgumentException)
							{
								Debug.WriteLine(typeOne + ", " +
											  typeTwo + ", " +
											  Utilities.GetOperatorVisualRepresentation(op) + "   \t-->\t" +
											  Utilities.DataTypes.empty);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
    }
}
