
namespace Notes
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.ProgramLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.programName = new System.Windows.Forms.Label();
            this.programVersion = new System.Windows.Forms.Label();
            this.programDeveloper = new System.Windows.Forms.Label();
            this.programDescription = new System.Windows.Forms.Label();
            this.youtubeChannel = new System.Windows.Forms.Button();
            this.telegramChannel = new System.Windows.Forms.Button();
            this.github = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ProgramLogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgramLogoPictureBox
            // 
            this.ProgramLogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ProgramLogoPictureBox.Image")));
            this.ProgramLogoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.ProgramLogoPictureBox.Name = "ProgramLogoPictureBox";
            this.ProgramLogoPictureBox.Size = new System.Drawing.Size(64, 64);
            this.ProgramLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProgramLogoPictureBox.TabIndex = 0;
            this.ProgramLogoPictureBox.TabStop = false;
            // 
            // programName
            // 
            this.programName.AutoSize = true;
            this.programName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.programName.ForeColor = System.Drawing.Color.White;
            this.programName.Location = new System.Drawing.Point(82, 12);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(86, 31);
            this.programName.TabIndex = 1;
            this.programName.Text = "Notes";
            // 
            // programVersion
            // 
            this.programVersion.AutoSize = true;
            this.programVersion.ForeColor = System.Drawing.Color.White;
            this.programVersion.Location = new System.Drawing.Point(85, 43);
            this.programVersion.Name = "programVersion";
            this.programVersion.Size = new System.Drawing.Size(58, 13);
            this.programVersion.TabIndex = 2;
            this.programVersion.Text = "Версія 2.0";
            // 
            // programDeveloper
            // 
            this.programDeveloper.AutoSize = true;
            this.programDeveloper.ForeColor = System.Drawing.Color.White;
            this.programDeveloper.Location = new System.Drawing.Point(85, 63);
            this.programDeveloper.Name = "programDeveloper";
            this.programDeveloper.Size = new System.Drawing.Size(140, 13);
            this.programDeveloper.TabIndex = 3;
            this.programDeveloper.Text = "Розробник: MaksimCeleron";
            // 
            // programDescription
            // 
            this.programDescription.AutoSize = true;
            this.programDescription.ForeColor = System.Drawing.Color.White;
            this.programDescription.Location = new System.Drawing.Point(9, 79);
            this.programDescription.Name = "programDescription";
            this.programDescription.Size = new System.Drawing.Size(131, 13);
            this.programDescription.TabIndex = 4;
            this.programDescription.Text = "Нотатки, написані на C#";
            // 
            // youtubeChannel
            // 
            this.youtubeChannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.youtubeChannel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.youtubeChannel.ForeColor = System.Drawing.Color.White;
            this.youtubeChannel.Location = new System.Drawing.Point(12, 95);
            this.youtubeChannel.Name = "youtubeChannel";
            this.youtubeChannel.Size = new System.Drawing.Size(213, 23);
            this.youtubeChannel.TabIndex = 5;
            this.youtubeChannel.Text = "YouTube канал";
            this.toolTip1.SetToolTip(this.youtubeChannel, "https://www.youtube.com/channel/UCR7RC5PCfs4-RZz6TUrBQDA");
            this.youtubeChannel.UseVisualStyleBackColor = false;
            this.youtubeChannel.Click += new System.EventHandler(this.button1_Click);
            // 
            // telegramChannel
            // 
            this.telegramChannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(17)))), ((int)(((byte)(47)))));
            this.telegramChannel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.telegramChannel.ForeColor = System.Drawing.Color.White;
            this.telegramChannel.Location = new System.Drawing.Point(12, 124);
            this.telegramChannel.Name = "telegramChannel";
            this.telegramChannel.Size = new System.Drawing.Size(213, 23);
            this.telegramChannel.TabIndex = 6;
            this.telegramChannel.Text = "Telegram канал";
            this.toolTip1.SetToolTip(this.telegramChannel, "https://t.me/MaksimCeleronCh");
            this.telegramChannel.UseVisualStyleBackColor = false;
            this.telegramChannel.Click += new System.EventHandler(this.button2_Click);
            // 
            // github
            // 
            this.github.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(100)))), ((int)(((byte)(237)))));
            this.github.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.github.ForeColor = System.Drawing.Color.White;
            this.github.Location = new System.Drawing.Point(12, 153);
            this.github.Name = "github";
            this.github.Size = new System.Drawing.Size(213, 23);
            this.github.TabIndex = 7;
            this.github.Text = "Інші додатки";
            this.toolTip1.SetToolTip(this.github, "https://github.com/MaksimCeleron");
            this.github.UseVisualStyleBackColor = false;
            this.github.Click += new System.EventHandler(this.button3_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(237, 188);
            this.Controls.Add(this.github);
            this.Controls.Add(this.telegramChannel);
            this.Controls.Add(this.youtubeChannel);
            this.Controls.Add(this.programDescription);
            this.Controls.Add(this.programDeveloper);
            this.Controls.Add(this.programVersion);
            this.Controls.Add(this.programName);
            this.Controls.Add(this.ProgramLogoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "Про додаток";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ProgramLogoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ProgramLogoPictureBox;
        private System.Windows.Forms.Label programName;
        private System.Windows.Forms.Label programVersion;
        private System.Windows.Forms.Label programDeveloper;
        private System.Windows.Forms.Label programDescription;
        private System.Windows.Forms.Button youtubeChannel;
        private System.Windows.Forms.Button telegramChannel;
        private System.Windows.Forms.Button github;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}