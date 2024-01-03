using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace Notes
{
    public partial class NotesForm : Form
    {
        List<Note> notes;

        public NotesForm()
        {
            InitializeComponent();
        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            string title = Interaction.InputBox("Введіть заголовок нотатки", "Створення нотатки");
            if (title != "")
            {
                notes.Add(new Note(title));
                string data = JsonConvert.SerializeObject(notes);
                File.WriteAllText("Notes.json", data);
                NotesListBox.Items.Add(notes.Last().title);
            }
        }

        private void OpenNoteButton_Click(object sender, EventArgs e)
        {
            NoteForm NoteForm = new NoteForm(notes, NotesListBox.SelectedIndex);
            NoteForm.Show();
        }

        private void EditTitleButton_Click(object sender, EventArgs e)
        {
            string title = Interaction.InputBox("Введіть новий заголовок нотатки", "Редагування заголовка нотатки");
            if (title != "")
            {
                notes[NotesListBox.SelectedIndex].title = title;
                string data = JsonConvert.SerializeObject(notes);
                File.WriteAllText("Notes.json", data);
                NotesListBox.Items[NotesListBox.SelectedIndex] = notes[NotesListBox.SelectedIndex].title;
            }
        }

        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = MessageBox.Show("Ви дійсно бажаєте видалити нотатку \"" + NotesListBox.Items[NotesListBox.SelectedIndex] + "\"?", "Видалення нотатки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogresult == DialogResult.Yes)
            {
                notes.RemoveAt(NotesListBox.SelectedIndex);
                string data = JsonConvert.SerializeObject(notes);
                File.WriteAllText("Notes.json", data);
                NotesListBox.Items.RemoveAt(NotesListBox.SelectedIndex);
                OpenNoteButton.Enabled = false;
                EditTitleButton.Enabled = false;
                RemoveNoteButton.Enabled = false;
                NotesListBox.ContextMenuStrip = null;
            }
        }

        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenNoteButton.Enabled = true;
            EditTitleButton.Enabled = true;
            RemoveNoteButton.Enabled = true;
            NotesListBox.ContextMenuStrip = ContextMenu;
        }

        private void NotesForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                NotifyIcon.Visible = true;
                NotifyIcon.ShowBalloonTip(1000);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            NotifyIcon.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenToolStripMenuItem_Click(OpenToolStripMenuItem, null);
        }

        private void NotesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OpenNoteButton.Enabled)
            {
                OpenNoteButton_Click(OpenNoteButton, null);
            }
        }

        private void AutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch ((int)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1))
            {
                case 0:
                    DarkToolStripMenuItem_Click(DarkToolStripMenuItem, null);
                    break;
                case 1:
                    LightToolStripMenuItem_Click(LightToolStripMenuItem, null);
                    break;
            }

            Properties.Settings.Default.Theme = 1;
            Properties.Settings.Default.Save();

            AutoToolStripMenuItem.Checked = true;
            LightToolStripMenuItem.Checked = false;
            DarkToolStripMenuItem.Checked = false;
        }

        private void LightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Empty;
            MenuStrip.BackColor = Color.FromArgb(255, 227, 227, 227);
            AutoToolStripMenuItem.BackColor = Color.Empty;
            AutoToolStripMenuItem.ForeColor = Color.Empty;
            LightToolStripMenuItem.BackColor = Color.Empty;
            LightToolStripMenuItem.ForeColor = Color.Empty;
            DarkToolStripMenuItem.BackColor = Color.Empty;
            DarkToolStripMenuItem.ForeColor = Color.Empty;
            NotesListBox.BackColor = Color.Empty;
            NotesListBox.ForeColor = Color.Empty;
            OpenNoteToolStripMenuItem.BackColor = Color.Empty;
            OpenNoteToolStripMenuItem.ForeColor = Color.Empty;
            EditTitleToolStripMenuItem.BackColor = Color.Empty;
            EditTitleToolStripMenuItem.ForeColor = Color.Empty;
            RemoveNoteToolStripMenuItem.BackColor = Color.Empty;
            RemoveNoteToolStripMenuItem.ForeColor = Color.Empty;
            OpenToolStripMenuItem.BackColor = Color.Empty;
            OpenToolStripMenuItem.ForeColor = Color.Empty;
            CloseToolStripMenuItem.BackColor = Color.Empty;
            CloseToolStripMenuItem.ForeColor = Color.Empty;

            Properties.Settings.Default.Theme = 2;
            Properties.Settings.Default.Save();

            AutoToolStripMenuItem.Checked = false;
            LightToolStripMenuItem.Checked = true;
            DarkToolStripMenuItem.Checked = false;
        }

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            MenuStrip.BackColor = Color.LightGray;
            AutoToolStripMenuItem.BackColor = Color.Black;
            AutoToolStripMenuItem.ForeColor = Color.White;
            LightToolStripMenuItem.BackColor = Color.Black;
            LightToolStripMenuItem.ForeColor = Color.White;
            DarkToolStripMenuItem.BackColor = Color.Black;
            DarkToolStripMenuItem.ForeColor = Color.White;
            NotesListBox.BackColor = Color.Black;
            NotesListBox.ForeColor = Color.White;
            OpenNoteToolStripMenuItem.BackColor = Color.Black;
            OpenNoteToolStripMenuItem.ForeColor = Color.White;
            EditTitleToolStripMenuItem.BackColor = Color.Black;
            EditTitleToolStripMenuItem.ForeColor = Color.White;
            RemoveNoteToolStripMenuItem.BackColor = Color.Black;
            RemoveNoteToolStripMenuItem.ForeColor = Color.White;
            OpenToolStripMenuItem.BackColor = Color.Black;
            OpenToolStripMenuItem.ForeColor = Color.White;
            CloseToolStripMenuItem.BackColor = Color.Black;
            CloseToolStripMenuItem.ForeColor = Color.White;

            Properties.Settings.Default.Theme = 3;
            Properties.Settings.Default.Save();

            AutoToolStripMenuItem.Checked = false;
            LightToolStripMenuItem.Checked = false;
            DarkToolStripMenuItem.Checked = true;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm AboutForm = new AboutForm();
            AboutForm.ShowDialog();
        }

        private void NotesForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists("Notes.json"))
            {
                File.Create("Notes.json").Close();
            }
            string data = File.ReadAllText("Notes.json");
            if (data != "")
            {
                notes = JsonConvert.DeserializeObject<List<Note>>(data);

                if (notes.Count != 0)
                {
                    for (int repeats = 0; repeats < notes.Count; repeats++)
                    {
                        NotesListBox.Items.Add(notes[repeats].title);
                    }
                }
            } else
            {
                notes = new List<Note>();
            }

            if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1) == null)
            {
                AutoToolStripMenuItem.Available = false;

                switch (Properties.Settings.Default.Theme)
                {
                    case 1:
                    case 2:
                        LightToolStripMenuItem_Click(LightToolStripMenuItem, null);
                        break;
                    case 3:
                        DarkToolStripMenuItem_Click(DarkToolStripMenuItem, null);
                        break;
                }
            }
            else
            {
                switch (Properties.Settings.Default.Theme)
                {
                    case 1:
                        AutoToolStripMenuItem_Click(AutoToolStripMenuItem, null);
                        break;
                    case 2:
                        LightToolStripMenuItem_Click(LightToolStripMenuItem, null);
                        break;
                    case 3:
                        DarkToolStripMenuItem_Click(DarkToolStripMenuItem, null);
                        break;
                }
            }
        }
    }

    public class Note
    {
        public string title { get; set; }
        public string text { get; set; }

        public Note(string title)
        {
            this.title = title;
        }
    }
}
