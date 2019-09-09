using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;

namespace 匈牙利回归
{
	/// <summary>
	/// 标记一个Class声明语句
	/// </summary>
	class ClassTag: ITag
	{
		//public ClassTag(UIElement adornment, AdornmentRemovedCallback removalCallback, double? topSpace, double? baseline, double? textHeight, double? bottomSpace, PositionAffinity? affinity) : base(adornment, removalCallback, topSpace, baseline, textHeight, bottomSpace, affinity) { }
		//public ClassTag(UIElement adornment, AdornmentRemovedCallback removalCallback, PositionAffinity? affinity) : base(adornment, removalCallback, affinity) { }
		//public ClassTag(UIElement adornment, AdornmentRemovedCallback removalCallback) : base(adornment, removalCallback) { }
	}
}
