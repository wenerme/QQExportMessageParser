namespace QQMessageManager
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.打开MHTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExportMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.MSGDataGridView = new System.Windows.Forms.DataGridView();
			this.IsBySelf = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.labelListCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.labelMessageTarget = new System.Windows.Forms.ToolStripStatusLabel();
			this.labelAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MSGDataGridView)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.ExportMenu});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(449, 25);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "menuStrip1";
			// 
			// 打开ToolStripMenuItem
			// 
			this.打开ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开MHTToolStripMenuItem});
			this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
			this.打开ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.打开ToolStripMenuItem.Text = "菜单";
			// 
			// 打开MHTToolStripMenuItem
			// 
			this.打开MHTToolStripMenuItem.Name = "打开MHTToolStripMenuItem";
			this.打开MHTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.打开MHTToolStripMenuItem.Tag = "Open";
			this.打开MHTToolStripMenuItem.Text = "打开MHT";
			this.打开MHTToolStripMenuItem.Click += new System.EventHandler(this.RunCommand);
			// 
			// ExportMenu
			// 
			this.ExportMenu.Name = "ExportMenu";
			this.ExportMenu.Size = new System.Drawing.Size(44, 21);
			this.ExportMenu.Text = "导出";
			// 
			// MSGDataGridView
			// 
			this.MSGDataGridView.AllowUserToAddRows = false;
			this.MSGDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MSGDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsBySelf,
            this.Date,
            this.Content});
			this.MSGDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MSGDataGridView.Location = new System.Drawing.Point(0, 25);
			this.MSGDataGridView.MultiSelect = false;
			this.MSGDataGridView.Name = "MSGDataGridView";
			this.MSGDataGridView.ReadOnly = true;
			this.MSGDataGridView.RowTemplate.Height = 23;
			this.MSGDataGridView.Size = new System.Drawing.Size(449, 272);
			this.MSGDataGridView.TabIndex = 1;
			// 
			// IsBySelf
			// 
			this.IsBySelf.DataPropertyName = "IsBySelf";
			this.IsBySelf.HeaderText = "IsBySelf";
			this.IsBySelf.Name = "IsBySelf";
			this.IsBySelf.ReadOnly = true;
			this.IsBySelf.Width = 60;
			// 
			// Date
			// 
			this.Date.DataPropertyName = "Date";
			this.Date.HeaderText = "发送时间";
			this.Date.Name = "Date";
			this.Date.ReadOnly = true;
			this.Date.Width = 120;
			// 
			// Content
			// 
			this.Content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Content.DataPropertyName = "Content";
			this.Content.HeaderText = "消息内容";
			this.Content.Name = "Content";
			this.Content.ReadOnly = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelListCount,
            this.labelMessageTarget,
            this.labelAbout});
			this.statusStrip1.Location = new System.Drawing.Point(0, 275);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1.Size = new System.Drawing.Size(449, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// labelListCount
			// 
			this.labelListCount.Name = "labelListCount";
			this.labelListCount.Size = new System.Drawing.Size(91, 17);
			this.labelListCount.Text = "共有 {0} 条消息";
			// 
			// labelMessageTarget
			// 
			this.labelMessageTarget.Name = "labelMessageTarget";
			this.labelMessageTarget.Size = new System.Drawing.Size(56, 17);
			this.labelMessageTarget.Text = "消息对象";
			// 
			// labelAbout
			// 
			this.labelAbout.ForeColor = System.Drawing.Color.DeepSkyBlue;
			this.labelAbout.Name = "labelAbout";
			this.labelAbout.Size = new System.Drawing.Size(74, 17);
			this.labelAbout.Text = "作者: wener";
			this.labelAbout.Click += new System.EventHandler(this.labelAbout_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 297);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.MSGDataGridView);
			this.Controls.Add(this.MainMenu);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "MainForm";
			this.Text = "QQ导出信息解析器";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MSGDataGridView)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExportMenu;
		private System.Windows.Forms.DataGridView MSGDataGridView;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel labelListCount;
		private System.Windows.Forms.ToolStripStatusLabel labelAbout;
		private System.Windows.Forms.ToolStripMenuItem 打开MHTToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel labelMessageTarget;
		private System.Windows.Forms.DataGridViewTextBoxColumn IsBySelf;
		private System.Windows.Forms.DataGridViewTextBoxColumn Date;
		private System.Windows.Forms.DataGridViewTextBoxColumn Content;
	}
}

