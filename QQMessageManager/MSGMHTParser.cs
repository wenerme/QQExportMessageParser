using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace QQMessageManager
{
	public class MSGMHTParser : MSGParser
	{
		private static readonly Regex TRTagRegex;
		public static Regex MessageContentRegex;
		private readonly TextReader reader;
		private MSGContent current;
		private DateTime currentDate;
		private string currentLine;
		private Match currentMatch = Match.Empty;
		private bool haveNext = true;

		static MSGMHTParser()
		{
			TRTagRegex = new Regex("<tr>.*?(?=</tr>)</tr>", RegexOptions.Compiled);
			MessageContentRegex = new Regex(
				"</div>(?<time>[^<]+)</div><div[^>]*>(?<content>.*?)(?=</div>)"
				, RegexOptions.Singleline
				  | RegexOptions.Compiled);
		}

		public MSGMHTParser(TextReader reader) : base(reader)
		{
			this.reader = reader;
			string line = reader.ReadLine();

			if (!(
				line.IndexOf("<html") >= 0
				&& line.IndexOf("QQ Message") > 0
				&& line.IndexOf("消息记录") > 0
				&& line.IndexOf("消息分组") > 0))
				throw new Exception("该解析器无法解析该内容");
			MatchCollection matches = TRTagRegex.Matches(line);
			// 只获取有用的TR
			currentLine = matches[2].Value + matches[4].Value + matches[5].Value;
		}

		public string To { get; private set; }

		public override MSGContent Current
		{
			get { return current; }
		}

		private string NextTR()
		{
			if (currentLine == null)
				currentLine = reader.ReadLine();

			currentMatch = currentMatch.NextMatch();
			if (currentMatch == Match.Empty)
			{
				currentMatch = TRTagRegex.Match(currentLine);
				currentLine = null;
			}

			if (currentMatch == Match.Empty)
			{
				haveNext = false;
				return null;
			}

			return currentMatch.Groups[0].Value;
		}

		/// <summary>
		///     解析 TR 标签,自动判断 TR类型
		///     如果 tr 为message,则返回true
		/// </summary>
		private bool ParseTR(string tr)
		{
			bool result = false;
			if (tr == null)
			{
				haveNext = false;
			} else if (tr.StartsWith("<tr><td><div style=color:"))
			{
				result = true;
				ParseMessageTR(tr);
			} else if (tr.StartsWith("<tr><td style="))
				ParseDateTR(tr);
			else if (tr.StartsWith("<tr><td><div style=padding-left"))
				ParseTargetTR(tr);
				//else if (tr.StartsWith("</table></body></html>"))
				//	end = true;
			else
				throw new Exception("无法解析该条数据 :" + tr);

			return result;
		}

		private void ParseDateTR(string tr)
		{
			string date = Regex.Match(tr, "日期:(?<date>[^<]+)").Groups["date"].Value;
			currentDate = DateTime.Parse(date);
		}

		private void ParseMessageTR(string tr)
		{
			Match match = MessageContentRegex.Match(tr);
			current = new MSGContent();

			current.Date = currentDate.Add(TimeSpan.Parse(match.Groups["time"].Value));
			current.Content = match.Groups["content"].Value;

			if (tr.StartsWith("<tr><td><div style=color:#42B475"))
			{
				current.IsBySelf = true;
			}
		}

		private void ParseTargetTR(string tr)
		{
			Match match = Regex.Match(tr, "消息对象:(?<target>[^<]+)");
			To = match.Groups["target"].Value;

			To = WebUtility.HtmlDecode(To);
		}

		public override bool MoveNext()
		{
			if (!haveNext)
				return false;

			while (false == ParseTR(NextTR()))
			{
				if (!haveNext)
					return false;
			}


			return haveNext;
		}

		public override void Reset()
		{
		}
	}
}