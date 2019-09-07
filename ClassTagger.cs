using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace 匈牙利回归
{
	/// <summary>
	/// 产生 <see cref="ClassTag"/>
	/// </summary>
	class ClassTagger : Microsoft.VisualStudio.Text.Tagging.ITagger<ClassTag>
	{
		private Document document;

		public IEnumerable<ITagSpan<ClassTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			if (spans.Count == 0) //there is no content in the buffer
				yield break;

			GetIntersectingLines(spans).Count();
			foreach (var span in spans)
			{
				SnapshotPoint snapshotPoint = new SnapshotPoint();

				document = snapshotPoint.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
				var semanticModel = document.GetSemanticModelAsync().Result;

				var syntaxRoot = document.GetSyntaxRootAsync().Result;

				var token = syntaxRoot.FindToken(snapshotPoint);
				var classDeclarationToken = token.Parent.AncestorsAndSelf().FirstOrDefault() as Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax;

				if (classDeclarationToken != null)
				{
					if (classDeclarationToken.Identifier.Text.StartsWith("C"))
					{

					}
				}
			}



			throw new NotImplementedException();
		}

		private static IEnumerable<ITextSnapshotLine> GetIntersectingLines(NormalizedSnapshotSpanCollection spans)
		{
			if (spans.Count != 0)
			{
				int val = -1;
				SnapshotSpan val2 = spans[0];
				ITextSnapshot snapshot = val2.Snapshot;
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
