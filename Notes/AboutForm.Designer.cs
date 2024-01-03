
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.ProgramLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.ProgramNameLabel = new System.Windows.Forms.Label();
            this.ProgramVersionLabel = new System.Windows.Forms.Label();
            this.ProgramDeveloperLabel = new System.Windows.Forms.Label();
            this.ProgramDescriptionLabel = new System.Windows.Forms.Label();
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
            // ProgramNameLabel
            // 
            this.ProgramNameLabel.AutoSize = true;
            this.ProgramNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProgramNameLabel.Location = new System.Drawing.Point(82, 12);
            this.ProgramNameLabel.Name = "ProgramNameLabel";
            this.ProgramNameLabel.Size = new System.Drawing.Size(86, 31);
            this.ProgramNameLabel.TabIndex = 1;
            this.ProgramNameLabel.Text = "Notes";
            // 
            // ProgramVersionLabel
            // 
            this.ProgramVersionLabel.AutoSize = true;
            this.ProgramVersionLabel.Location = new System.Drawing.Point(85, 43);
            this.ProgramVersionLabel.Name = "ProgramVersionLabel";
            this.ProgramVersionLabel.Size = new System.Drawing.Size(58, 13);
            this.ProgramVersionLabel.TabIndex = 2;
            this.ProgramVersionLabel.Text = "Версія 1.0";
            // 
            // ProgramDeveloperLabel
            // 
            this.ProgramDeveloperLabel.AutoSize = true;
            this.ProgramDeveloperLabel.Location = new System.Drawing.Point(85, 63);
            this.ProgramDeveloperLabel.Name = "ProgramDeveloperLabel";
            this.ProgramDeveloperLabel.Size = new System.Drawing.Size(140, 13);
            this.ProgramDeveloperLabel.TabIndex = 3;
            this.ProgramDeveloperLabel.Text = "Розробник: MaksimCeleron";
            // 
            // ProgramDescriptionLabel
            // 
            this.ProgramDescriptionLabel.AutoSize = true;
            this.ProgramDescriptionLabel.Location = new System.Drawing.Point(9, 79);
            this.ProgramDescriptionLabel.Name = "ProgramDescriptionLabel";
            this.ProgramDescriptionLabel.Size = new System.Drawing.Size(131, 13);
            this.ProgramDescriptionLabel.TabIndex = 4;
            this.ProgramDescriptionLabel.Text = "Нотатки, написані на C#";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 101);
            this.Controls.Add(this.ProgramDescriptionLabel);
            this.Controls.Add(this.ProgramDeveloperLabel);
            this.Controls.Add(this.ProgramVersionLabel);
            this.Controls.Add(this.ProgramNameLabel);
            this.Controls.Add(this.ProgramLogoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "Про програму";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ProgramLogoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ProgramLogoPictureBox;
        private System.Windows.Forms.Label ProgramNameLabel;
        private System.Windows.Forms.Label ProgramVersionLabel;
        private System.Windows.Forms.Label ProgramDeveloperLabel;
        private System.Windows.Forms.Label ProgramDescriptionLabel;
    }
}