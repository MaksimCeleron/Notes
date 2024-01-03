using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace Notes
{
    public partial class NoteForm : Form
    {
        List<Note> notes;
        int selectednote;
        bool opening = false;
        DateTime start;
        bool timerworking = false;

        public NoteForm(List<Note> notes, int selectednote)
        {
            InitializeComponent();

            this.notes = notes;
            this.selectednote = selectednote;

            if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1) == null)
            {
                switch (Properties.Settings.Default.Theme)
                {
                    case 1:
                    case 2:
                        this.BackColor = Color.Empty;
                        NoteTextBox.BackColor = Color.Empty;
                        NoteTextBox.ForeColor = Color.Empty;
                        SaveToolStripMenuItem.BackColor = Color.Empty;
                        SaveToolStripMenuItem.ForeColor = Color.Empty;
                        AutoSaveToolStripMenuItem.BackColor = Color.Empty;
                        AutoSaveToolStripMenuItem.ForeColor = Color.Empty;
                        break;
                    case 3:
                        this.BackColor = Color.Black;
                        NoteTextBox.BackColor = Color.Black;
                        NoteTextBox.ForeColor = Color.White;
                        SaveToolStripMenuItem.BackColor = Color.Black;
                        SaveToolStripMenuItem.ForeColor = Color.White;
                        AutoSaveToolStripMenuItem.BackColor = Color.Black;
                        AutoSaveToolStripMenuItem.ForeColor = Color.White;
                        break;
                }
            }
            else
            {
                switch (Properties.Settings.Default.Theme)
                {
                    case 1:
                        switch ((int)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1))
                        {
                            case 0:
                                this.BackColor = Color.Black;
                                NoteTextBox.BackColor = Color.Black;
                                NoteTextBox.ForeColor = Color.White;
                                SaveToolStripMenuItem.BackColor = Color.Black;
                                SaveToolStripMenuItem.ForeColor = Color.White;
                                AutoSaveToolStripMenuItem.BackColor = Color.Black;
                                AutoSaveToolStripMenuItem.ForeColor = Color.White;
                                break;
                            case 1:
                                this.BackColor = Color.Empty;
                                NoteTextBox.BackColor = Color.Empty;
                                NoteTextBox.ForeColor = Color.Empty;
                                SaveToolStripMenuItem.BackColor = Color.Empty;
                                SaveToolStripMenuItem.ForeColor = Color.Empty;
                                AutoSaveToolStripMenuItem.BackColor = Color.Empty;
                                AutoSaveToolStripMenuItem.ForeColor = Color.Empty;
                                break;
                        }
                        break;
                    case 2:
                        this.BackColor = Color.Empty;
                        NoteTextBox.BackColor = Color.Empty;
                        NoteTextBox.ForeColor = Color.Empty;
                        SaveToolStripMenuItem.BackColor = Color.Empty;
                        SaveToolStripMenuItem.ForeColor = Color.Empty;
                        AutoSaveToolStripMenuItem.BackColor = Color.Empty;
                        AutoSaveToolStripMenuItem.ForeColor = Color.Empty;
                        break;
                    case 3:
                        this.BackColor = Color.Black;
                        NoteTextBox.BackColor = Color.Black;
                        NoteTextBox.ForeColor = Color.White;
                        SaveToolStripMenuItem.BackColor = Color.Black;
                        SaveToolStripMenuItem.ForeColor = Color.White;
                        AutoSaveToolStripMenuItem.BackColor = Color.Black;
                        AutoSaveToolStripMenuItem.ForeColor = Color.White;
                        break;
                }
            }
        }

        private async void NoteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!opening)
            {
                if (Properties.Settings.Default.AutoSave)
                {
                    start = DateTime.UtcNow;
                }

                Text = '*' + notes[selectednote].title;
                SaveToolStripMenuItem.Enabled = true;

                if (Properties.Settings.Default.AutoSave)
                {
                    if (timerworking == false)
                    {
                        TimeSpan diff = TimeSpan.FromSeconds(3);
                        timerworking = true;

                        while((DateTime.UtcNow - start) < diff && timerworking == true)
                        {
                            await Task.Delay(1000);
                        }

                        if (timerworking == true)
                        {
                            notes[selectednote].text = NoteTextBox.Text;
                            string data = JsonConvert.SerializeObject(notes);
                            File.WriteAllText("Notes.json", data);
                            timerworking = false;

                            Text = notes[selectednote].title;
                            SaveToolStripMenuItem.Enabled = false;
                        }
                    }
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notes[selectednote].text = NoteTextBox.Text;
            string data = JsonConvert.SerializeObject(notes);
            File.WriteAllText("Notes.json", data);
            timerworking = false;

            Text = notes[selectednote].title;
            SaveToolStripMenuItem.Enabled = false;
        }

        private void AutoSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AutoSave)
            {
                Properties.Settings.Default.AutoSave = false;
            } else
            {
                Properties.Settings.Default.AutoSave = true;
            }
            Properties.Settings.Default.Save();
        }

        private void NoteForm_Load(object sender, EventArgs e)
        {
            Text = notes[selectednote].title;
            opening = true;
            NoteTextBox.Text = notes[selectednote].text;
            opening = false;
            AutoSaveToolStripMenuItem.Checked = Properties.Settings.Default.AutoSave;
        }

        private void NoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerworking = false;

            if (SaveToolStripMenuItem.Enabled)
            {
                DialogResult dialogresult = MessageBox.Show("Бажаєте зберегти нотатку \"" + notes[selectednote].title + "\"?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dialogresult == DialogResult.Yes)
                {
                    notes[selectednote].text = NoteTextBox.Text;
                    string data = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("Notes.json", data);
                }
                else if (dialogresult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
