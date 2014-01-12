using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace QQMessageManager
{
	public class MIMEPart
	{
		private static readonly Regex HeaderRegex;

		static MIMEPart()
		{
			string headerParttern = @"
^
(?<key>[^:]+) # 匹配键
:
(?<value> # 值有两种
(?:
[^\n\r] # 单行值
|\r\n\t
)+) # 多行值
";
			HeaderRegex = new Regex(headerParttern,
				RegexOptions.IgnorePatternWhitespace
				| RegexOptions.Compiled
				| RegexOptions.Multiline);
		}

		private MIMEPart()
		{
			Header = new Dictionary<string, string>();
		}

		/// <summary>
		///     Content-Type of this part
		/// </summary>
		public string MediaType
		{
			get { return ContentType.MediaType; }
			//internal set { Header["Content-Type"] = value; }
		}

		public ContentType ContentType { get; private set; }

		public string Content { get; internal set; }
		public Dictionary<string, string> Header { get; private set; }
		public int HeaderLength { get; private set; }
		public string HeaderRaw { get; private set; }

		public static MIMEPart ParseContent(string content)
		{
			MIMEPart part = ParseHeader(content);
			if (content.Length > part.HeaderLength + 4)
				part.Content = content.Substring(part.HeaderLength + 4); // 4=\r\n\r\n

			return part;
		}

		public static MIMEPart ParseHeader(string content)
		{
			var part = new MIMEPart();

			// 获取 Header 长度
			part.HeaderLength = content.IndexOf("\r\n\r\n", StringComparison.Ordinal);
			if (part.HeaderLength < 0)
				part.HeaderLength = content.Length;
			// 获取 HeaderRaw
			part.HeaderRaw = content.Substring(0, part.HeaderLength);
			// 解析 Header
			{
				Match match = HeaderRegex.Match(part.HeaderRaw);
				while (match != Match.Empty)
				{
					string k = match.Groups["key"].Value.Trim();
					string v = match.Groups["value"].Value.Trim(); // remove white space

					part.Header.Add(k, v);
					match = match.NextMatch();
				}
			}
			// 解析 Type 参数
			{
				string ct = part.Header["Content-Type"];
				// 不知道为什么,这里不能解析 Type=text/html 这段参数
				ct = Regex.Replace(ct, "type=[^;]+", "");
				// 也不能包含有换行
				ct = ct.Replace("\r\n\t", "");
				part.ContentType = new ContentType(ct);
			}


			return part;
		}
	}
}