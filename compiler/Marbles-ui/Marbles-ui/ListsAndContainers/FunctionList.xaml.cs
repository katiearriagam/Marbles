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

namespace Marbles
{
    public sealed partial class FunctionList : UserControl
    {
        public FunctionList()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Calls the PrintCode function for the contained <see cref="FunctionInstructionList"/>.
        /// </summary>
        public void PrintCode()
        {
            FunctionInstructionListTarget.PrintCode();
        }
    }
}
