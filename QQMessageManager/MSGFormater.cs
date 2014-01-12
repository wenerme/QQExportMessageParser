using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace QQMessageManager
{
	public class MSGFormater
	{
		private static readonly Regex FontTagRegex = new Regex("</?font[^>]*>"
			, RegexOptions.Compiled);

		private static readonly Regex IMGTagRegex = new Regex("(?<=<img[^>]*?src=\")(?<src>[^\"]*)"
			, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		private static readonly Regex ExtTagRegex = new Regex("^(?:[^/]*/)?(?<ext>.+)"
			, RegexOptions.Compiled);

		private MSGContainer currentContainer;

		public MSGFormater()
		{
			ImageBaseDirectory = "images";
		}

		public string ImageBaseDirectory { get; set; }

		private void FormatContent(MSGContent msg)
		{
			//msg.Content = FontTagRegex.Replace(msg.Content, "")
			//	.Replace("&nbsp;", "")
			//	
			msg.Content = FontTagRegex.Replace(msg.Content, "");
			msg.Content = WebUtility.HtmlDecode(msg.Content)
				.Trim()
				.Replace("<br>", "");
			msg.Content = IMGTagRegex.Replace(msg.Content, ImgTagEvel);
		}

		private string ImgTagEvel(Match m)
		{
			string src = m.Groups["src"].Value;
			//var fn
			MHTAsset asset = currentContainer.Assets[src];
			return currentContainer.AssetsNameConvision(asset);
		}

		public void Format(MSGContainer container)
		{
			currentContainer = container;
			container.AssetsNameConvision = a =>
			{
				string fn = a.Hash;
				string ext = ExtTagRegex.Match(a.Type).Groups["ext"].Value;
				fn += "." + ext;
				return Path.Combine(ImageBaseDirectory, fn);
			};

			foreach (MSGContent msg in container.Messages)
			{
				FormatContent(msg);
			}
		}
	}
}