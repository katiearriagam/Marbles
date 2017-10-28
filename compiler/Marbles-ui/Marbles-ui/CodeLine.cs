using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Marbles
{
	class CodeLine
	{
		public string content;
		public UserControl owner;

		public CodeLine(string content, UserControl owner)
		{
			this.content = content;
			this.owner = owner;
		}
	}
}
