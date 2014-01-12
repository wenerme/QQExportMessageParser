using System.Data;
using Omu.ValueInjecter;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace QQMessageManager
{
	internal class SQLiteExporter : MSGExporter
	{
		public SQLiteExporter(MSGContainer msg) : base(msg)
		{
			Filter = "SQLite database|*.sqlite";
			FileExtension = "sqlite";
		}

		public override void ExportMessage()
		{
			var dbFactory = new OrmLiteConnectionFactory(ExportFile, SqliteDialect.Provider);

			using (IDbConnection db = dbFactory.OpenDbConnection())
			using (IDbTransaction trans = db.OpenTransaction(IsolationLevel.ReadCommitted))
			{
				db.DropTable<MessageDB>();
				db.CreateTable<MessageDB>();

				foreach (MSGContent msg in MSG.Messages)
					db.Insert(new MessageDB(msg));

				trans.Commit();
			}
		}
	}

	internal class MessageDB : MSGContent
	{
		public MessageDB(MSGContent msg)
		{
			this.InjectFrom(msg);
		}

		[AutoIncrement]
		public int Id { get; set; }
	}
}