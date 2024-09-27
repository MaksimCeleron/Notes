namespace Notes
{
    partial class NotesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotesForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manageNote = new System.Windows.Forms.ToolStripMenuItem();
            this.createNote = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNote = new System.Windows.Forms.ToolStripMenuItem();
            this.save = new System.Windows.Forms.ToolStripMenuItem();
            this.settings = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSaveNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.language = new System.Windows.Forms.ToolStripMenuItem();
            this.automatic = new System.Windows.Forms.ToolStripMenuItem();
            this.ukrainian = new System.Windows.Forms.ToolStripMenuItem();
            this.english = new System.Windows.Forms.ToolStripMenuItem();
            this.theme = new System.Windows.Forms.ToolStripMenuItem();
            this.type = new System.Windows.Forms.ToolStripMenuItem();
            this.system = new System.Windows.Forms.ToolStripMenuItem();
            this.light = new System.Windows.Forms.ToolStripMenuItem();
            this.dark = new System.Windows.Forms.ToolStripMenuItem();
            this.style = new System.Windows.Forms.ToolStripMenuItem();
            this.modern = new System.Windows.Forms.ToolStripMenuItem();
            this.modernWindowsColors = new System.Windows.Forms.ToolStripMenuItem();
            this.oldModern = new System.Windows.Forms.ToolStripMenuItem();
            this.secret = new System.Windows.Forms.ToolStripMenuItem();
            this.classic = new System.Windows.Forms.ToolStripMenuItem();
            this.about = new System.Windows.Forms.ToolStripMenuItem();
            this.firstRegionTitle = new System.Windows.Forms.TextBox();
            this.firstRegionText = new System.Windows.Forms.TextBox();
            this.secondRegionTitle = new System.Windows.Forms.TextBox();
            this.secondRegionText = new System.Windows.Forms.TextBox();
            this.firstRegionNote = new System.Windows.Forms.ComboBox();
            this.secondRegionNote = new System.Windows.Forms.ComboBox();
            this.chooseFirstRegion = new System.Windows.Forms.Button();
            this.chooseSecondRegion = new System.Windows.Forms.Button();
            this.classicWindowsColors = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageNote,
            this.settings,
            this.about});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(803, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // manageNote
            // 
            this.manageNote.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNote,
            this.removeNote,
            this.save});
            this.manageNote.ForeColor = System.Drawing.Color.White;
            this.manageNote.Name = "manageNote";
            this.manageNote.Size = new System.Drawing.Size(133, 20);
            this.manageNote.Text = "Керування нотаткою";
            this.manageNote.DropDownClosed += new System.EventHandler(this.manageNote_DropDownClosed);
            this.manageNote.DropDownOpened += new System.EventHandler(this.manageNote_DropDownOpened);
            // 
            // createNote
            // 
            this.createNote.Name = "createNote";
            this.createNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createNote.Size = new System.Drawing.Size(214, 22);
            this.createNote.Text = "Створити нотатку";
            this.createNote.Click += new System.EventHandler(this.createNote_Click);
            // 
            // removeNote
            // 
            this.removeNote.Name = "removeNote";
            this.removeNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.removeNote.Size = new System.Drawing.Size(214, 22);
            this.removeNote.Text = "Видалити нотатку";
            this.removeNote.Click += new System.EventHandler(this.removeNote_Click);
            // 
            // save
            // 
            this.save.Enabled = false;
            this.save.Name = "save";
            this.save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.save.Size = new System.Drawing.Size(214, 22);
            this.save.Text = "Зберегти";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // settings
            // 
            this.settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoSaveNotes,
            this.language,
            this.theme});
            this.settings.ForeColor = System.Drawing.Color.White;
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(101, 20);
            this.settings.Text = "Налаштування";
            this.settings.DropDownClosed += new System.EventHandler(this.settings_DropDownClosed);
            this.settings.DropDownOpened += new System.EventHandler(this.settings_DropDownOpened);
            // 
            // autoSaveNotes
            // 
            this.autoSaveNotes.Name = "autoSaveNotes";
            this.autoSaveNotes.Size = new System.Drawing.Size(212, 22);
            this.autoSaveNotes.Text = "Автозбереження нотаток";
            this.autoSaveNotes.Click += new System.EventHandler(this.autoSaveNotes_Click);
            // 
            // language
            // 
            this.language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automatic,
            this.ukrainian,
            this.english});
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(212, 22);
            this.language.Text = "Мова";
            // 
            // automatic
            // 
            this.automatic.Name = "automatic";
            this.automatic.Size = new System.Drawing.Size(148, 22);
            this.automatic.Text = "Автоматично";
            this.automatic.Click += new System.EventHandler(this.automatic_Click);
            // 
            // ukrainian
            // 
            this.ukrainian.Name = "ukrainian";
            this.ukrainian.Size = new System.Drawing.Size(148, 22);
            this.ukrainian.Text = "Українська";
            this.ukrainian.Click += new System.EventHandler(this.ukrainian_Click);
            // 
            // english
            // 
            this.english.Name = "english";
            this.english.Size = new System.Drawing.Size(148, 22);
            this.english.Text = "Англійська";
            this.english.Click += new System.EventHandler(this.english_Click);
            // 
            // theme
            // 
            this.theme.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.type,
            this.style});
            this.theme.Name = "theme";
            this.theme.Size = new System.Drawing.Size(212, 22);
            this.theme.Text = "Тема";
            // 
            // type
            // 
            this.type.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.system,
            this.light,
            this.dark});
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(180, 22);
            this.type.Text = "Тип";
            // 
            // system
            // 
            this.system.Name = "system";
            this.system.Size = new System.Drawing.Size(128, 22);
            this.system.Text = "Системна";
            this.system.Click += new System.EventHandler(this.system_Click);
            // 
            // light
            // 
            this.light.Name = "light";
            this.light.Size = new System.Drawing.Size(128, 22);
            this.light.Text = "Світла";
            this.light.Click += new System.EventHandler(this.light_Click);
            // 
            // dark
            // 
            this.dark.Name = "dark";
            this.dark.Size = new System.Drawing.Size(128, 22);
            this.dark.Text = "Темна";
            this.dark.Click += new System.EventHandler(this.dark_Click);
            // 
            // style
            // 
            this.style.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modern,
            this.modernWindowsColors,
            this.oldModern,
            this.classic,
            this.classicWindowsColors});
            this.style.Name = "style";
            this.style.Size = new System.Drawing.Size(180, 22);
            this.style.Text = "Стиль";
            // 
            // modern
            // 
            this.modern.Name = "modern";
            this.modern.Size = new System.Drawing.Size(243, 22);
            this.modern.Text = "Сучасний";
            this.modern.Click += new System.EventHandler(this.modern_Click);
            // 
            // modernWindowsColors
            // 
            this.modernWindowsColors.Name = "modernWindowsColors";
            this.modernWindowsColors.Size = new System.Drawing.Size(243, 22);
            this.modernWindowsColors.Text = "Сучасний(Кольори Windows)";
            this.modernWindowsColors.Click += new System.EventHandler(this.modernWindowsColors_Click);
            // 
            // oldModern
            // 
            this.oldModern.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secret});
            this.oldModern.Name = "oldModern";
            this.oldModern.Size = new System.Drawing.Size(243, 22);
            this.oldModern.Text = "Старий сучасний";
            this.oldModern.Click += new System.EventHandler(this.oldModern_Click);
            // 
            // secret
            // 
            this.secret.Name = "secret";
            this.secret.Size = new System.Drawing.Size(133, 22);
            this.secret.Text = "Секретний";
            this.secret.Click += new System.EventHandler(this.secret_Click);
            // 
            // classic
            // 
            this.classic.Name = "classic";
            this.classic.Size = new System.Drawing.Size(243, 22);
            this.classic.Text = "Класичний(Win11)";
            this.classic.Click += new System.EventHandler(this.classic_Click);
            // 
            // about
            // 
            this.about.ForeColor = System.Drawing.Color.White;
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(88, 20);
            this.about.Text = "Про додаток";
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // firstRegionTitle
            // 
            this.firstRegionTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.firstRegionTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstRegionTitle.ForeColor = System.Drawing.Color.White;
            this.firstRegionTitle.Location = new System.Drawing.Point(11, 105);
            this.firstRegionTitle.Name = "firstRegionTitle";
            this.firstRegionTitle.Size = new System.Drawing.Size(385, 13);
            this.firstRegionTitle.TabIndex = 1;
            this.firstRegionTitle.TextChanged += new System.EventHandler(this.firstRegionTitle_TextChanged);
            // 
            // firstRegionText
            // 
            this.firstRegionText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.firstRegionText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstRegionText.ForeColor = System.Drawing.Color.White;
            this.firstRegionText.Location = new System.Drawing.Point(11, 131);
            this.firstRegionText.Multiline = true;
            this.firstRegionText.Name = "firstRegionText";
            this.firstRegionText.Size = new System.Drawing.Size(385, 376);
            this.firstRegionText.TabIndex = 2;
            this.firstRegionText.TextChanged += new System.EventHandler(this.firstRegionText_TextChanged);
            // 
            // secondRegionTitle
            // 
            this.secondRegionTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.secondRegionTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondRegionTitle.ForeColor = System.Drawing.Color.White;
            this.secondRegionTitle.Location = new System.Drawing.Point(406, 105);
            this.secondRegionTitle.Name = "secondRegionTitle";
            this.secondRegionTitle.Size = new System.Drawing.Size(385, 13);
            this.secondRegionTitle.TabIndex = 3;
            this.secondRegionTitle.TextChanged += new System.EventHandler(this.secondRegionTitle_TextChanged);
            // 
            // secondRegionText
            // 
            this.secondRegionText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.secondRegionText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondRegionText.ForeColor = System.Drawing.Color.White;
            this.secondRegionText.Location = new System.Drawing.Point(406, 131);
            this.secondRegionText.Multiline = true;
            this.secondRegionText.Name = "secondRegionText";
            this.secondRegionText.Size = new System.Drawing.Size(385, 376);
            this.secondRegionText.TabIndex = 4;
            this.secondRegionText.TextChanged += new System.EventHandler(this.secondRegionText_TextChanged);
            // 
            // firstRegionNote
            // 
            this.firstRegionNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.firstRegionNote.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.firstRegionNote.ForeColor = System.Drawing.Color.White;
            this.firstRegionNote.FormattingEnabled = true;
            this.firstRegionNote.Location = new System.Drawing.Point(11, 67);
            this.firstRegionNote.Name = "firstRegionNote";
            this.firstRegionNote.Size = new System.Drawing.Size(385, 21);
            this.firstRegionNote.TabIndex = 5;
            this.firstRegionNote.SelectedIndexChanged += new System.EventHandler(this.firstRegionNote_SelectedIndexChanged);
            // 
            // secondRegionNote
            // 
            this.secondRegionNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.secondRegionNote.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.secondRegionNote.ForeColor = System.Drawing.Color.White;
            this.secondRegionNote.FormattingEnabled = true;
            this.secondRegionNote.Location = new System.Drawing.Point(406, 67);
            this.secondRegionNote.Name = "secondRegionNote";
            this.secondRegionNote.Size = new System.Drawing.Size(385, 21);
            this.secondRegionNote.TabIndex = 6;
            this.secondRegionNote.SelectedIndexChanged += new System.EventHandler(this.secondRegionNote_SelectedIndexChanged);
            // 
            // chooseFirstRegion
            // 
            this.chooseFirstRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(100)))), ((int)(((byte)(237)))));
            this.chooseFirstRegion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chooseFirstRegion.ForeColor = System.Drawing.Color.White;
            this.chooseFirstRegion.Location = new System.Drawing.Point(11, 38);
            this.chooseFirstRegion.Name = "chooseFirstRegion";
            this.chooseFirstRegion.Size = new System.Drawing.Size(385, 23);
            this.chooseFirstRegion.TabIndex = 7;
            this.chooseFirstRegion.Text = "Область 1";
            this.chooseFirstRegion.UseVisualStyleBackColor = false;
            this.chooseFirstRegion.Click += new System.EventHandler(this.chooseFirstRegion_Click);
            // 
            // chooseSecondRegion
            // 
            this.chooseSecondRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.chooseSecondRegion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chooseSecondRegion.ForeColor = System.Drawing.Color.White;
            this.chooseSecondRegion.Location = new System.Drawing.Point(406, 38);
            this.chooseSecondRegion.Name = "chooseSecondRegion";
            this.chooseSecondRegion.Size = new System.Drawing.Size(385, 23);
            this.chooseSecondRegion.TabIndex = 8;
            this.chooseSecondRegion.Text = "Область 2";
            this.chooseSecondRegion.UseVisualStyleBackColor = false;
            this.chooseSecondRegion.Click += new System.EventHandler(this.chooseSecondRegion_Click);
            // 
            // classicWindowsColors
            // 
            this.classicWindowsColors.Name = "classicWindowsColors";
            this.classicWindowsColors.Size = new System.Drawing.Size(243, 22);
            this.classicWindowsColors.Text = "Класичний(Кольори Windows)";
            this.classicWindowsColors.Click += new System.EventHandler(this.classicWindowsColors_Click);
            // 
            // NotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(803, 519);
            this.Controls.Add(this.chooseSecondRegion);
            this.Controls.Add(this.chooseFirstRegion);
            this.Controls.Add(this.secondRegionNote);
            this.Controls.Add(this.firstRegionNote);
            this.Controls.Add(this.secondRegionText);
            this.Controls.Add(this.secondRegionTitle);
            this.Controls.Add(this.firstRegionText);
            this.Controls.Add(this.firstRegionTitle);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "NotesForm";
            this.Text = "Notes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotesForm_FormClosing);
            this.Load += new System.EventHandler(this.NotesForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manageNote;
        private System.Windows.Forms.ToolStripMenuItem settings;
        private System.Windows.Forms.ToolStripMenuItem language;
        private System.Windows.Forms.ToolStripMenuItem ukrainian;
        private System.Windows.Forms.ToolStripMenuItem english;
        private System.Windows.Forms.ToolStripMenuItem theme;
        private System.Windows.Forms.ToolStripMenuItem about;
        private System.Windows.Forms.TextBox firstRegionTitle;
        private System.Windows.Forms.TextBox firstRegionText;
        private System.Windows.Forms.ToolStripMenuItem createNote;
        private System.Windows.Forms.ToolStripMenuItem removeNote;
        private System.Windows.Forms.ToolStripMenuItem style;
        private System.Windows.Forms.ToolStripMenuItem modern;
        private System.Windows.Forms.ToolStripMenuItem classic;
        private System.Windows.Forms.ToolStripMenuItem type;
        private System.Windows.Forms.ToolStripMenuItem system;
        private System.Windows.Forms.ToolStripMenuItem light;
        private System.Windows.Forms.ToolStripMenuItem dark;
        private System.Windows.Forms.TextBox secondRegionTitle;
        private System.Windows.Forms.TextBox secondRegionText;
        private System.Windows.Forms.ComboBox firstRegionNote;
        private System.Windows.Forms.ComboBox secondRegionNote;
        private System.Windows.Forms.Button chooseFirstRegion;
        private System.Windows.Forms.ToolStripMenuItem oldModern;
        private System.Windows.Forms.Button chooseSecondRegion;
        private System.Windows.Forms.ToolStripMenuItem autoSaveNotes;
        private System.Windows.Forms.ToolStripMenuItem modernWindowsColors;
        private System.Windows.Forms.ToolStripMenuItem secret;
        private System.Windows.Forms.ToolStripMenuItem save;
        private System.Windows.Forms.ToolStripMenuItem automatic;
        private System.Windows.Forms.ToolStripMenuItem classicWindowsColors;
    }
}