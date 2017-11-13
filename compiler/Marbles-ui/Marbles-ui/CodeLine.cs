using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Marbles
{
	public class CodeLine
	{
		public string content;
		public UserControl owner;
		public int lineNumber; 

		public CodeLine(string content, UserControl owner, int lineNumber)
		{
			this.content = content;
			this.owner = owner;
			this.lineNumber = lineNumber;
		}
	}
}
