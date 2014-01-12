using System;

namespace QQMessageManager
{
	public class MHTAsset
	{
		private byte[] _bytes;
		private string _hash;
		public string Raw { get; private set; }
		public string Location { get; private set; }
		public string Encoding { get; private set; }
		public string Type { get; private set; }

		public string Hash
		{
			get { return _hash ?? (_hash = U5.GetMD5Hash(Raw)); }
		}

		public byte[] Bytes
		{
			get { return _bytes ?? GenerateBytes(); }
		}

		private byte[] GenerateBytes()
		{
			switch (Encoding.ToLower())
			{
				case "base64":
					_bytes = Convert.FromBase64String(Raw);
					break;
				case "7bit":
					_bytes = System.Text.Encoding.UTF8.GetBytes(Raw);
					break;
				default:
					throw new Exception("不能解码源数据,编码为 " + Encoding);
			}
			return _bytes;
		}

		public static MHTAsset FromPart(MIMEPart part)
		{
			var asset = new MHTAsset();
			try
			{
				asset.Raw = part.Content;
				asset.Type = part.MediaType;
				asset.Location = part.Header["Content-Location"];
				asset.Encoding = part.Header["Content-Transfer-Encoding"];
			} catch (Exception ex)
			{
				throw new Exception("不能将 MIMEPart 解析为 Asset", ex);
			}

			return asset;
		}

		public static bool IsAsset(MIMEPart part)
		{
			return true;
		}
	}
}