using System;
using System.Collections.Generic;
using System.IO;

namespace QQMessageManager
{
	public class MSGContainer
	{
		private MSGContainer()
		{
			Messages = new List<MSGContent>();
		}

		/// <summary>
		///     消息对象
		/// </summary>
		public string To { get; private set; }

		public IList<MSGContent> Messages { get; private set; }
		public Dictionary<string, MHTAsset> Assets { get; private set; }

		//public Func<string, string> AssetsFileGetter { get; private set; }
		//public Func<string,MHTAsset> AssetsGetter { get; private set; }
		/// <summary>
		///     给定 Location, 返回文件路径
		/// </summary>
		/// <summary>
		///     给定 Location, 返回 Assets
		/// </summary>
		/// <summary>
		///     返回 Assets 的文件名
		/// </summary>
		public Func<MHTAsset, string> AssetsNameConvision { get; set; }

		public static MSGContainer FromMHT(MHTContainer mht)
		{
			var container = new MSGContainer();
			//container.AssetsFileGetter = mht.GetAssetFile;
			//container.AssetsGetter = loc => MHTAsset.FromPart(mht.Locations[loc]);
			// 设置Assets列表
			var assets = new Dictionary<string, MHTAsset>();
			foreach (var part in mht.Locations)
			{
				try
				{
					assets[part.Key] = MHTAsset.FromPart(part.Value);
				} catch
				{
				}
			}
			container.Assets = assets;

			string msgs = mht.Parts[1].Content; // 消息部分
			// 解析
			var reader = new StringReader(msgs);

			var parser = new MSGMHTParser(reader);

			while (parser.MoveNext())
			{
				container.Messages.Add(parser.Current);
			}
			container.To = parser.To;
			return container;
		}
	}
}