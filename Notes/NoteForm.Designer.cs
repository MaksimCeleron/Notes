namespace Notes
{
    partial class NoteForm
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
            this.components = new System.ComponentModel.Container();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Location = new System.Drawing.Point(12, 12);
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.Size = new System.Drawing.Size(144, 129);
            this.NoteTextBox.TabIndex = 0;
            this.NoteTextBox.TextChanged += new System.EventHandler(this.NoteTextBox_TextChanged);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.AutoSaveToolStripMenuItem});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(174, 48);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Enabled = false;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SaveToolStripMenuItem.Text = "Зберегти";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // AutoSaveToolStripMenuItem
            // 
            this.AutoSaveToolStripMenuItem.CheckOnClick = true;
            this.AutoSaveToolStripMenuItem.Name = "AutoSaveToolStripMenuItem";
            this.AutoSaveToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.AutoSaveToolStripMenuItem.Text = "Автозбереження";
            this.AutoSaveToolStripMenuItem.Click += new System.EventHandler(this.AutoSaveToolStripMenuItem_Click);
            // 
            // NoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 153);
            this.ContextMenuStrip = this.ContextMenu;
            this.Controls.Add(this.NoteTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NoteForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NoteForm_FormClosing);
            this.Load += new System.EventHandler(this.NoteForm_Load);
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NoteTextBox;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoSaveToolStripMenuItem;
    }
}