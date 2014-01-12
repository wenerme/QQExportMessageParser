using System.Dynamic;
using Newtonsoft.Json;

namespace QQMessageManager
{
	internal class JSONExporter : MSGExporter
	{
		public JSONExporter(MSGContainer msg) : base(msg)
		{
			FileExtension = "json";
			Filter = "JSON|*.json";
		}

		public override void ExportMessage()
		{
			dynamic obj = new ExpandoObject();
			obj.Target = MSG.To;

			obj.Messages = MSG.Messages;

			U5.FilePutContents(JsonConvert.SerializeObject(obj), ExportFile);
		}
	}
}