using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Marbles
{
    /// <summary>
    /// Class that links every line in the source code to a Block in the UI that generated that line.
    /// </summary>
	public class CodeLine
	{
        /// <summary>
        /// The string value at a specific line of code.
        /// </summary>
		public string content;

        /// <summary>
        /// The Block that generated <see cref="content"/>.
        /// </summary>
        public UserControl owner;

        /// <summary>
        /// The line number at which <see cref="content"/> is found in the source code.
        /// </summary>
		public int lineNumber; 

		public CodeLine(string content, UserControl owner, int lineNumber)
		{
			this.content = content;
			this.owner = owner;
			this.lineNumber = lineNumber;
		}
	}
}
