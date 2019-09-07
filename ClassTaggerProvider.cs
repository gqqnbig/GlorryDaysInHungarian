using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;

namespace 匈牙利回归
{

	[Export(typeof(ITaggerProvider))]
	[ContentType("CSharp")]
	[TagType(typeof(ClassTag))]
	internal sealed class ClassTaggerProvider: IViewTaggerProvider
	{
		[Import]
		internal ITextSearchService TextSearchService
		{
			get;
			set;
		}

		[Import]
		internal IBufferTagAggregatorFactoryService BufferTagAggregatorFactoryService
		{
			get;
			set;
		}

		//public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
		//{
		//	if ((int)buffer == 0)
		//	{
		//		throw new ArgumentNullException("buffer");
		//	}
		//	return buffer.get_Properties().GetOrCreateSingletonProperty<VarTagger>((Func<VarTagger>)(() => new VarTagger(buffer, this.TextSearchService))) as ITagger;
		//}

		public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
		{
			// Only provide highlighting on the top-level buffer
			if (textView.TextBuffer != buffer)
			{
				throw  new Exception("Not top level buffer");
				return null;
			}

			return buffer.Properties.GetOrCreateSingletonProperty(nameof(ClassTagger),()=>new ClassTagger()) as ITagger<T>;
		}
	}
}
