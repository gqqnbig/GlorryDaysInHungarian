using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace 匈牙利回归
{
	/// <summary>
	/// 产生 <see cref="ClassTag"/>
	/// </summary>
	class ClassTagger : Microsoft.VisualStudio.Text.Tagging.ITagger<ClassTag>
	{

		public IEnumerable<ITagSpan<ClassTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			if (spans.Count == 0) //there is no content in the buffer
				yield break;

			SyntaxNode syntaxRoot = null;
			foreach (var span in spans)
			{
				SnapshotPoint snapshotPoint = span.Start;

				if (syntaxRoot == null)
				{
					var document = snapshotPoint.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
					var semanticModel = document.GetSemanticModelAsync().Result;

					syntaxRoot = document.GetSyntaxRootAsync().Result;
				}

				Queue<SyntaxNode> queue = new Queue<SyntaxNode>();
				queue.Enqueue(syntaxRoot);



				while (queue.Count > 0)
				{
					var token = queue.Dequeue();

					if (token is ClassDeclarationSyntax classDeclarationToken)
					{
						if (classDeclarationToken.Identifier.Text.StartsWith("C"))
						{
							var s = new SnapshotSpan(snapshotPoint + token.SpanStart, token.Span.Length);
							yield return new TagSpan<ClassTag>(s, new ClassTag(new TextBlock() { Text = "C"},null));
						}
					}
					else
					{
						foreach (var t in token.ChildNodes())
							queue.Enqueue(t);
					}
				}
			}
		}

		private static IEnumerable<ITextSnapshotLine> GetIntersectingLines(NormalizedSnapshotSpanCollection spans)
		{
			if (spans.Count != 0)
			{
				int val = -1;
				ITextSnapshot snapshot = spans[0].Snapshot;
				foreach (SnapshotSpan span in spans)
				{
					SnapshotSpan current = span;
					int lineNumberFromPosition = snapshot.GetLineNumberFromPosition(current.Start);
					int lastLine = snapshot.GetLineNumberFromPosition(current.End);
					for (int i = Math.Max(val, lineNumberFromPosition); i <= lastLine; i++)
					{
						yield return snapshot.GetLineFromLineNumber(i);
					}
					val = lastLine;
				}
			}
		}

		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
	}
}
