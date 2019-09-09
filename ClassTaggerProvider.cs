using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;

namespace 匈牙利回归
{
	[Export(typeof(IViewTaggerProvider))]
	[ContentType("CSharp")] //修改ContentType后必须清理
	[ContentType("projection")]
	[TagType(typeof(IntraTextAdornmentTag))] //修改TagType后必须清理
	internal sealed class ClassTaggerProvider : IViewTaggerProvider //修改类名后必须清理
	{
#pragma warning disable 649 // "field never assigned to" -- field is set by MEF.
		[Import]
		internal IBufferTagAggregatorFactoryService BufferTagAggregatorFactoryService;
#pragma warning restore 649

		public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
		{
			if (textView == null)
				throw new ArgumentNullException("textView");

			if (buffer == null)
				throw new ArgumentNullException("buffer");

			if (textView.TextBuffer != buffer)
			{
				//throw  new Exception("Not top level buffer");
				return null;
			}

			return textView.Properties.GetOrCreateSingletonProperty(() => new ClassTagger((IWpfTextView)textView) as ITagger<T>);
		}
	}
}
