using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace QQMessageManager
{
	public abstract class MSGParser : IEnumerator<MSGContent>
	{
		public MSGParser(TextReader reader)
		{
		}

		public virtual void Dispose()
		{
		}

		public abstract bool MoveNext();
		public abstract void Reset();
		public abstract MSGContent Current { get; }

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}
}