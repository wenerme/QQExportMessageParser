using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QQMessageManager
{
	public abstract class MSGExporter
	{
		private string _exportFile;

		static MSGExporter()
		{
			LastExportDirectory = Directory.GetCurrentDirectory();
			Exporters = new Dictionary<string, Type>();

			Exporters.Add("导出为 JSON 格式", typeof (JSONExporter));
			Exporters.Add("导出为 SQLite 数据库", typeof (SQLiteExporter));
		}

		public MSGExporter(MSGContainer msg)
		{
			MSG = msg;
			Filter = "All Files|*.*";
		}

		public static Dictionary<string, Type> Exporters { get; private set; }

		public static string LastExportDirectory { get; internal set; }

		public string FileExtension { get; protected set; }
		protected string Filter { get; set; }
		public MSGContainer MSG { get; protected set; }

		public string ExportFile
		{
			get { return _exportFile; }
			protected set
			{
				_exportFile = value;
				ExportDirectory = Path.GetDirectoryName(_exportFile);
				LastExportDirectory = ExportDirectory;
			}
		}

		public string ExportDirectory { get; private set; }

		public static void ExportBy(MSGContainer container, string name, string filename = null)
		{
			if (! Exporters.ContainsKey(name))
				throw new Exception(name + " 导出器未找到");
			Type type = Exporters[name];

			var exporter = (MSGExporter) Activator.CreateInstance(type, container);
			;

			exporter.ExportTo(filename);
		}

		public virtual void Export()
		{
			ExportMessage();
			ExportAssets();
		}

		public abstract void ExportMessage();

		public virtual void ExportTo(string filepath = null)
		{
			if (filepath == null)
			{
				var dialog = new SaveFileDialog
				{
					InitialDirectory = LastExportDirectory,
					AddExtension = true,
					DefaultExt = FileExtension,
					FileName = ExportFile,
					Filter = Filter
				};
				if (dialog.ShowDialog() != DialogResult.OK)
					return;
				filepath = dialog.FileName;
			}

			ExportFile = filepath;
			Export();
		}

		protected virtual void ExportAssets()
		{
			foreach (var asset in MSG.Assets)
			{
				string dest = Path.Combine(ExportDirectory, MSG.AssetsNameConvision(asset.Value));

				REDO:
				try
				{
					MHTCacher.Default.CopyTo(asset.Value, dest);
				} catch (DirectoryNotFoundException)
				{
					string path = Path.GetDirectoryName(dest);
					Directory.CreateDirectory(path);
					goto REDO;
				}
			}
		}
	}
}