using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace QQMessageManager
{
	public class MHTContainer
	{
		private MHTContainer()
		{
			Parts = new List<MIMEPart>();
			Locations = new Dictionary<string, MIMEPart>();
		}

		public MIMEPart HeaderPart { get; private set; }
		public IList<MIMEPart> Parts { get; private set; }
		public Dictionary<string, MIMEPart> Locations { get; private set; }

		public static MHTContainer ParseFile(string filepath)
		{
			var mht = new MHTContainer();

			var reader = new StreamReader(filepath);


			return mht;
		}

		public static MHTContainer ParseContent(string content)
		{
			var mht = new MHTContainer();
			// 解析 Header 部分
			mht.HeaderPart = MIMEPart.ParseHeader(content);
			content = content.Substring(mht.HeaderPart.HeaderLength + 4);
			mht.Parts.Add(mht.HeaderPart);
			// 获取 boundary
			string boundary = mht.HeaderPart.ContentType.Boundary;
			//if (boundary[0] == '"')
			//	boundary = boundary.Trim('"');

			// 分割

			//var contentParts = content.Split(new[] { boundary }, StringSplitOptions.RemoveEmptyEntries);
			string[] contentParts = Regex.Split(content, "^.*?" + Regex.Escape(boundary) + ".*"
				, RegexOptions.Compiled
				  | RegexOptions.Multiline);
			contentParts = contentParts.Skip(0).ToArray();
			// 解析每一部分
			foreach (string strPart in contentParts)
			{
				string str = strPart.Trim();
				if (string.IsNullOrWhiteSpace(str)) // 忽略开头和结尾的两部分
					continue;

				MIMEPart part = MIMEPart.ParseContent(str);

				mht.Parts.Add(part);
				string location;
				if (part.Header.TryGetValue("Content-Location", out location))
					mht.Locations[location] = part;
			}

			return mht;
		}
	}
}