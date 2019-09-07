using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.PlatformUI;
using System.Windows;

namespace 匈牙利回归
{
	public class BaseDialogWindow : DialogWindow
	{
		public BaseDialogWindow()
		{
			this.HasMaximizeButton = true;
			this.HasMinimizeButton = true;
		}
	}
}
