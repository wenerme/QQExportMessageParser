using System.IO;

namespace QQMessageManager
{
	public class MHTCacher
	{
		public const string DefaultDir = "cache";
		public static readonly MHTCacher Default;

		static MHTCacher()
		{
			Default = new MHTCacher(DefaultDir);
		}

		public MHTCacher(string dir)
		{
			CacheDir = dir;
			if (! Directory.Exists(CacheDir))
				Directory.CreateDirectory(CacheDir);
		}

		public string CacheDir { get; private set; }

		public void Clear()
		{
			Directory.Delete(CacheDir, true);
			Directory.CreateDirectory(CacheDir);
		}

		public string GetCacheFile(MHTAsset asset, bool force = false)
		{
			string fn = Path.Combine(CacheDir, asset.Hash);
			if (false == File.Exists(fn) || force)
			{
				using (FileStream file = File.Open(fn, FileMode.Create, FileAccess.Write))
					file.Write(asset.Bytes, 0, asset.Bytes.Length);
			}

			return fn;
		}

		public void CopyTo(MHTAsset asset, string dest, bool overwrite = false)
		{
			string src = GetCacheFile(asset);
			if (overwrite == false && File.Exists(dest))
				return;
			File.Copy(src, dest, overwrite);
		}
	}
}