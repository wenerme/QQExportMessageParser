using System.IO;

namespace QQMessageManager
{
	public class Test
	{
		public static void Main(string[] args)
		{
			var file = new FileStream("msg.mht", FileMode.Open, FileAccess.Read);
			var reader = new StreamReader(file);
			string content = reader.ReadToEnd();
			//var part = MIMEPart.ParseContent(content);
			MHTContainer mht = MHTContainer.ParseContent(content);
			MSGContainer msg = MSGContainer.FromMHT(mht);
		}
	}
}