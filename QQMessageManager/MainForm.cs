using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace QQMessageManager
{
	public partial class MainForm : Form
	{
		public MSGContainer MSG;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// 初始化 导出选项
			foreach (var exporter in MSGExporter.Exporters)
			{
				var item = new ToolStripMenuItem();
				item.Text = exporter.Key;
				item.Tag = "Export";
				item.Click += RunCommand;
				ExportMenu.DropDownItems.Add(item);
			}
		}

		public void BindDataList(IList<MSGContent> list)
		{
			MSGDataGridView.AutoGenerateColumns = false;
			MSGDataGridView.DataSource = new BindingList<MSGContent>(list);
			labelListCount.Text = string.Format("共有 {0} 条消息", list.Count);
		}

		public void Open(string filepath = null)
		{
			if (filepath == null)
			{
				var dialog = new OpenFileDialog
				{
					CheckFileExists = true,
					Filter = "MHT 文件格式(*.mht)|*.mht",
					InitialDirectory = Directory.GetCurrentDirectory()
				};
				if (dialog.ShowDialog() != DialogResult.OK)
					return;
				filepath = dialog.FileName;
			}

			var file = new FileStream(filepath, FileMode.Open, FileAccess.Read);
			var reader = new StreamReader(file);
			string content = reader.ReadToEnd();
			MHTContainer mht = MHTContainer.ParseContent(content);
			MSGContainer msg = MSGContainer.FromMHT(mht);
			MSG = msg;

			new MSGFormater().Format(msg);
			labelMessageTarget.Text = "消息对象: " + msg.To;

			BindDataList(msg.Messages);
		}

		private void RunCommand(object sender, EventArgs e)
		{
			string tag = null;
			if (sender is Control)
			{
				tag = (string) (sender as Control).Tag;
			} else if (sender is ToolStripItem)
			{
				tag = (string) (sender as ToolStripItem).Tag;
			}

			try
			{
				RunCommand(tag, sender, e);
			} catch (Exception ex)
			{
				MessageBox.Show("执行命令时发生错误\r\n" + ex.Message);
#if DEBUG
				throw;
#endif
			}
		}

		private void RunCommand(string cmd, params object[] extra)
		{
			if (cmd == null)
				return;
			switch (cmd)
			{
				case "Open":
					Open();
					break;
				case "Export":
					if (MSG == null || MSG.Messages.Count == 0)
					{
						MessageBox.Show("没有用于导出的消息");
						break;
					}

					var sender = extra[0] as ToolStripItem;
					if (sender == null)
						throw new Exception("未预期的导出操作");
					string title = Text;
					Text += "<正在导出,请稍候>";
					MSGExporter.ExportBy(MSG, sender.Text);
					Text = title;
					break;
				default:
					MessageBox.Show("该命令不存在 : " + cmd, "运行命令错误"
						, MessageBoxButtons.OK
						, MessageBoxIcon.Error);
					break;
			}
		}

		private void labelAbout_Click(object sender, EventArgs e)
		{
			Process.Start("http://blog.wener.me");
		}
	}
}