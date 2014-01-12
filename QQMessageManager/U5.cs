using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QQMessageManager
{
	public static class U5
	{
		/// <summary>
		///     获取字符串的MD5值
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string GetMD5Hash(string input)
		{
			var x = new MD5CryptoServiceProvider();
			byte[] bs = Encoding.UTF8.GetBytes(input);
			bs = x.ComputeHash(bs);
			var s = new StringBuilder();
			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToLower());
			}
			return s.ToString();
		}

		#region FILE

		/// <summary>
		///     写入内容到文件
		/// </summary>
		/// <param name="content">文件内容</param>
		/// <param name="filename">文件名</param>
		public static void FilePutContents(string content, string filename)
		{
			var writer = new StreamWriter(filename);
			writer.Write(content);
			writer.Close();
		}

		/// <summary>
		///     从文件获取内容
		/// </summary>
		/// <param name="fileName">文件名</param>
		/// <returns>文件内容</returns>
		public static string FileGetContents(string filename)
		{
			string content = string.Empty;

			var sr = new StreamReader(filename);
			content = sr.ReadToEnd();
			sr.Close();

			return content;
		} //FileGetContents

		#endregion
	}
}