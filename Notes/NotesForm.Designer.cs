namespace Notes
{
    partial class NotesForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotesForm));
            this.NotesListBox = new System.Windows.Forms.ListBox();
            this.AddNoteButton = new System.Windows.Forms.Button();
            this.RemoveNoteButton = new System.Windows.Forms.Button();
            this.EditTitleButton = new System.Windows.Forms.Button();
            this.OpenNoteButton = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.ThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayContextMenu.SuspendLayout();
            this.ContextMenu.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotesListBox
            // 
            this.NotesListBox.FormattingEnabled = true;
            this.NotesListBox.Location = new System.Drawing.Point(12, 28);
            this.NotesListBox.Name = "NotesListBox";
            this.NotesListBox.Size = new System.Drawing.Size(318, 199);
            this.NotesListBox.TabIndex = 0;
            this.NotesListBox.SelectedIndexChanged += new System.EventHandler(this.NotesListBox_SelectedIndexChanged);
            this.NotesListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotesListBox_MouseDoubleClick);
            // 
            // AddNoteButton
            // 
            this.AddNoteButton.Location = new System.Drawing.Point(12, 233);
            this.AddNoteButton.Name = "AddNoteButton";
            this.AddNoteButton.Size = new System.Drawing.Size(75, 23);
            this.AddNoteButton.TabIndex = 1;
            this.AddNoteButton.Text = "Створити";
            this.AddNoteButton.UseVisualStyleBackColor = true;
            this.AddNoteButton.Click += new System.EventHandler(this.AddNoteButton_Click);
            // 
            // RemoveNoteButton
            // 
            this.RemoveNoteButton.Enabled = false;
            this.RemoveNoteButton.Location = new System.Drawing.Point(255, 233);
            this.RemoveNoteButton.Name = "RemoveNoteButton";
            this.RemoveNoteButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveNoteButton.TabIndex = 2;
            this.RemoveNoteButton.Text = "Видалити";
            this.RemoveNoteButton.UseVisualStyleBackColor = true;
            this.RemoveNoteButton.Click += new System.EventHandler(this.RemoveNoteButton_Click);
            // 
            // EditTitleButton
            // 
            this.EditTitleButton.Enabled = false;
            this.EditTitleButton.Location = new System.Drawing.Point(174, 233);
            this.EditTitleButton.Name = "EditTitleButton";
            this.EditTitleButton.Size = new System.Drawing.Size(75, 23);
            this.EditTitleButton.TabIndex = 3;
            this.EditTitleButton.Text = "Редагувати";
            this.EditTitleButton.UseVisualStyleBackColor = true;
            this.EditTitleButton.Click += new System.EventHandler(this.EditTitleButton_Click);
            // 
            // OpenNoteButton
            // 
            this.OpenNoteButton.Enabled = false;
            this.OpenNoteButton.Location = new System.Drawing.Point(93, 233);
            this.OpenNoteButton.Name = "OpenNoteButton";
            this.OpenNoteButton.Size = new System.Drawing.Size(75, 23);
            this.OpenNoteButton.TabIndex = 4;
            this.OpenNoteButton.Text = "Відкрити";
            this.OpenNoteButton.UseVisualStyleBackColor = true;
            this.OpenNoteButton.Click += new System.EventHandler(this.OpenNoteButton_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipText = "Програму згорнуто у трей";
            this.NotifyIcon.BalloonTipTitle = "Notes";
            this.NotifyIcon.ContextMenuStrip = this.TrayContextMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "Notes";
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.CloseToolStripMenuItem});
            this.TrayContextMenu.Name = "ContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(129, 48);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Font = new System.Drawing.Font("Google Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.OpenToolStripMenuItem.Text = "Відкрити";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.CloseToolStripMenuItem.Text = "Закрити";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenNoteToolStripMenuItem,
            this.EditTitleToolStripMenuItem,
            this.RemoveNoteToolStripMenuItem});
            this.ContextMenu.Name = "contextMenuStrip1";
            this.ContextMenu.Size = new System.Drawing.Size(140, 70);
            // 
            // OpenNoteToolStripMenuItem
            // 
            this.OpenNoteToolStripMenuItem.Font = new System.Drawing.Font("Google Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OpenNoteToolStripMenuItem.Name = "OpenNoteToolStripMenuItem";
            this.OpenNoteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.OpenNoteToolStripMenuItem.Text = "Відкрити";
            this.OpenNoteToolStripMenuItem.Click += new System.EventHandler(this.OpenNoteButton_Click);
            // 
            // EditTitleToolStripMenuItem
            // 
            this.EditTitleToolStripMenuItem.Name = "EditTitleToolStripMenuItem";
            this.EditTitleToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.EditTitleToolStripMenuItem.Text = "Редагувати";
            this.EditTitleToolStripMenuItem.Click += new System.EventHandler(this.EditTitleButton_Click);
            // 
            // RemoveNoteToolStripMenuItem
            // 
            this.RemoveNoteToolStripMenuItem.Name = "RemoveNoteToolStripMenuItem";
            this.RemoveNoteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.RemoveNoteToolStripMenuItem.Text = "Видалити";
            this.RemoveNoteToolStripMenuItem.Click += new System.EventHandler(this.RemoveNoteButton_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ThemeToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(342, 25);
            this.MenuStrip.TabIndex = 5;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // ThemeToolStripMenuItem
            // 
            this.ThemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoToolStripMenuItem,
            this.LightToolStripMenuItem,
            this.DarkToolStripMenuItem});
            this.ThemeToolStripMenuItem.Name = "ThemeToolStripMenuItem";
            this.ThemeToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.ThemeToolStripMenuItem.Text = "Тема";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(98, 21);
            this.AboutToolStripMenuItem.Text = "Про програму";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // AutoToolStripMenuItem
            // 
            this.AutoToolStripMenuItem.Name = "AutoToolStripMenuItem";
            this.AutoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.AutoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AutoToolStripMenuItem.Text = "Авто";
            this.AutoToolStripMenuItem.Click += new System.EventHandler(this.AutoToolStripMenuItem_Click);
            // 
            // LightToolStripMenuItem
            // 
            this.LightToolStripMenuItem.Name = "LightToolStripMenuItem";
            this.LightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.LightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.LightToolStripMenuItem.Text = "Світла";
            this.LightToolStripMenuItem.Click += new System.EventHandler(this.LightToolStripMenuItem_Click);
            // 
            // DarkToolStripMenuItem
            // 
            this.DarkToolStripMenuItem.Name = "DarkToolStripMenuItem";
            this.DarkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.DarkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DarkToolStripMenuItem.Text = "Темна";
            this.DarkToolStripMenuItem.Click += new System.EventHandler(this.DarkToolStripMenuItem_Click);
            // 
            // NotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 268);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.OpenNoteButton);
            this.Controls.Add(this.EditTitleButton);
            this.Controls.Add(this.RemoveNoteButton);
            this.Controls.Add(this.AddNoteButton);
            this.Controls.Add(this.NotesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "NotesForm";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.NotesForm_Load);
            this.Resize += new System.EventHandler(this.NotesForm_Resize);
            this.TrayContextMenu.ResumeLayout(false);
            this.ContextMenu.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox NotesListBox;
        private System.Windows.Forms.Button AddNoteButton;
        private System.Windows.Forms.Button RemoveNoteButton;
        private System.Windows.Forms.Button EditTitleButton;
        private System.Windows.Forms.Button OpenNoteButton;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveNoteToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
    }
}

