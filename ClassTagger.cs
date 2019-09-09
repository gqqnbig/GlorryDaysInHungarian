using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace 匈牙利回归
{
	/// <summary>
	/// 产生 <see cref="ClassTag"/>
	/// </summary>
	class ClassTagger : IntraTextAdornmentTagger<string, TextBlock>
	//Microsoft.VisualStudio.Text.Tagging.ITagger<IntraTextAdornmentTag>
	{
		protected override TextBlock CreateAdornment(string data, SnapshotSpan span)
		{
			var textBlock = new TextBlock() { Padding = new Thickness(0), FontFamily = new FontFamily("Consolas"), FontSize = 11, FontWeight = FontWeights.Regular };
			textBlock.Inlines.Add("C");

			return textBlock;
		}

		protected override bool UpdateAdornment(TextBlock adornment, string data)
		{
			return true;
		}

		protected override IEnumerable<Tuple<SnapshotSpan, PositionAffinity?, string>> GetAdornmentData(NormalizedSnapshotSpanCollection spans)
		{
			foreach (var span in spans)
			{
				SyntaxNode syntaxRoot = null;
				SnapshotPoint snapshotPoint = span.Start;

				var document = snapshotPoint.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
				//var semanticModel = document.GetSemanticModelAsync().Result;

				syntaxRoot = document.GetSyntaxRootAsync().Result;

				Queue<SyntaxNode> queue = new Queue<SyntaxNode>();
				queue.Enqueue(syntaxRoot);



				while (queue.Count > 0)
				{
					var token = queue.Dequeue();

					if (token is ClassDeclarationSyntax classDeclarationToken)
					{
						//if (classDeclarationToken.Identifier.Text.StartsWith("C"))
						//{
						var s = new SnapshotSpan(snapshotPoint.Snapshot, classDeclarationToken.Identifier.SpanStart + 1, 0);
						yield return Tuple.Create<SnapshotSpan, PositionAffinity?, string>(s, PositionAffinity.Successor, classDeclarationToken.Identifier.Text);
						//}
					}
					else
					{
						foreach (var t in token.ChildNodes())
							queue.Enqueue(t);
					}
				}
			}
		}

		//public IEnumerable<ITagSpan<IntraTextAdornmentTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		//{
		//	if (spans.Count == 0) //there is no content in the buffer
		//		yield break;

		//	SyntaxNode syntaxRoot = null;
		//	foreach (var span in spans)
		//	{
		//		SnapshotPoint snapshotPoint = span.Start;

		//		if (syntaxRoot == null)
		//		{
		//			var document = snapshotPoint.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
		//			//var semanticModel = document.GetSemanticModelAsync().Result;

		//			syntaxRoot = document.GetSyntaxRootAsync().Result;
		//		}

		//		Queue<SyntaxNode> queue = new Queue<SyntaxNode>();
		//		queue.Enqueue(syntaxRoot);



		//		while (queue.Count > 0)
		//		{
		//			var token = queue.Dequeue();

		//			if (token is ClassDeclarationSyntax classDeclarationToken)
		//			{
		//				//if (classDeclarationToken.Identifier.Text.StartsWith("C"))
		//				//{
		//					var s = new SnapshotSpan(snapshotPoint.Snapshot, token.SpanStart, 0);
		//				yield return new TagSpan<IntraTextAdornmentTag>(s, new IntraTextAdornmentTag(new TextBlock() { Text = "C" }, null, PositionAffinity.Predecessor));
		//				//}
		//			}
		//			else
		//			{
		//				foreach (var t in token.ChildNodes())
		//					queue.Enqueue(t);
		//			}
		//		}
		//	}
		//}

		//private static IEnumerable<ITextSnapshotLine> GetIntersectingLines(NormalizedSnapshotSpanCollection spans)
		//{
		//	if (spans.Count != 0)
		//	{
		//		int val = -1;
		//		ITextSnapshot snapshot = spans[0].Snapshot;
		//		foreach (SnapshotSpan span in spans)
		//		{
		//			SnapshotSpan current = span;
		//			int lineNumberFromPosition = snapshot.GetLineNumberFromPosition(current.Start);
		//			int lastLine = snapshot.GetLineNumberFromPosition(current.End);
		//			for (int i = Math.Max(val, lineNumberFromPosition); i <= lastLine; i++)
		//			{
		//				yield return snapshot.GetLineFromLineNumber(i);
		//			}
		//			val = lastLine;
		//		}
		//	}
		//}

		public ClassTagger(IWpfTextView view) : base(view)
		{

		}
	}
}
