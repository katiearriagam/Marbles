﻿using System;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Marbles
{
	public sealed partial class CreateFunction : UserControl
	{
		public CreateFunction()
		{
			this.InitializeComponent();
		}

		private void AddParameter(object sender, RoutedEventArgs e)
		{
            
            Parameters.Items.Add(Parameters.Template);
		}

        public void PrintCode()
        {
            Debug.Write("function " + ((ComboBoxItem)(functionType.SelectedItem)).Content.ToString() + " " + functionID.Text + "(");

            foreach (var item in Parameters.Items)
            {
                // imprimir parametros
            }
            Debug.WriteLine(") {");
            // --> print las instrucciones dentro de la funcion aqui <-- //
            Debug.WriteLine("\t}");
        }
    }
}