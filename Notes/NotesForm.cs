using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Notes
{
    public partial class NotesForm : Form
    {
        private const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
        private ThemeWatcher _themeWatcher;

        Color settedaccentcolor = Color.FromArgb(255, 127, 100, 237);
        Color modernlightbackcolor = Color.FromArgb(255, 230, 223, 255);
        Color modernlightforecolor = Color.FromArgb(255, 210, 200, 247);
        Color moderndarkbackcolor = Color.FromArgb(255, 7, 0, 31);
        Color moderndarkforecolor = Color.FromArgb(255, 23, 17, 47);
        Color accentColor = Color.Empty;
        Color modernaccentlightbackcolor = Color.Empty;
        Color modernaccentlightforecolor = Color.Empty;
        Color modernaccentdarkbackcolor = Color.Empty;
        Color modernaccentdarkforecolor = Color.Empty;
        Color oldmodernbackcolor = Color.FromArgb(255, 32, 32, 33);
        Color oldmodernforecolor = Color.FromArgb(255, 48, 49, 49);

        [StructLayout(LayoutKind.Sequential)]
        struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int RtlGetVersion(ref OSVERSIONINFOEX versionInfo);

        bool win10orlater = false;

        private void CheckWindowsVersion()
        {
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();

            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            RtlGetVersion(ref osVersionInfo);

            if (osVersionInfo.dwMajorVersion >= 10)
            {
                win10orlater = true;
            }
        }

        List<Note> notes;
        bool refreshingNotes = false;
        int firstPreviousIndex = -1;
        int secondPreviousIndex = -1;
        bool firstNoteLoading = false;
        bool secondNoteLoading = false;
        DateTime fnstart;
        bool fntimerworking = false;
        DateTime snstart;
        bool sntimerworking = false;
        bool altRegionCorrection = false;

        public NotesForm()
        {
            InitializeComponent();
            _themeWatcher = new ThemeWatcher(this);
            CheckWindowsVersion();
        }

        public void HandleThemeChange()
        {
            var isLightTheme = IsLightTheme();

            if (isLightTheme)
            {
                light_Click(system, null);
            } else
            {
                dark_Click(system, null);
            }
        }

        private bool IsLightTheme()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
            {
                var value = key?.GetValue("AppsUseLightTheme");
                return value is int i && i > 0;
            }
        }

        public static uint GetAccentColor()
        {
            ThemeHelper.DwmGetColorizationColor(out uint color, out bool opaqueBlend);

            return color;
        }

        public static Color ConvertToColor(uint color)
        {
            byte a = (byte)((color >> 24) & 0xFF);
            byte r = (byte)((color >> 16) & 0xFF);
            byte g = (byte)((color >> 8) & 0xFF);
            byte b = (byte)(color & 0xFF);
            return Color.FromArgb(a, r, g, b);
        }

        private void refreshNotes(bool starting = false, bool creatingNote = false, bool removingNote = false)
        {
            refreshingNotes = true;

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
                    if (!starting)
                    {
                        firstRegionNote.Items.Clear();
                    }
                    for (int repeats = 0; repeats < notes.Count; repeats++)
                    {
                        firstRegionNote.Items.Add(notes[repeats].title);
                    }

                    if (!starting)
                    {
                        secondRegionNote.Items.Clear();
                    }
                    for (int repeats = 0; repeats < notes.Count; repeats++)
                    {
                        secondRegionNote.Items.Add(notes[repeats].title);
                    }
                }
            }
            else
            {
                notes = new List<Note>();
            }

            refreshingNotes = false;

            if (!starting)
            {
                if (notes.Count == 1)
                {
                    if (Properties.Settings.Default.SelectedRegion == false)
                    {
                        Properties.Settings.Default.FirstRegionNote = 0;
                        Properties.Settings.Default.SecondRegionNote = -1;
                    } else
                    {
                        Properties.Settings.Default.FirstRegionNote = -1;
                        Properties.Settings.Default.SecondRegionNote = 0;
                    }
                }

                if (notes.Count == 1)
                {
                    Properties.Settings.Default.Save();
                }
            }

            if (starting)
            {
                if (Properties.Settings.Default.FirstRegionNote == -1 && Properties.Settings.Default.SecondRegionNote == -1)
                {
                    if (notes.Count == 1)
                    {
                        Properties.Settings.Default.FirstRegionNote = 0;
                        Properties.Settings.Default.Save();
                    } else if (notes.Count > 1)
                    {
                        Properties.Settings.Default.FirstRegionNote = 0;
                        Properties.Settings.Default.SecondRegionNote = 1;
                        Properties.Settings.Default.Save();
                    }
                } else if (Properties.Settings.Default.FirstRegionNote > notes.Count - 1 || Properties.Settings.Default.SecondRegionNote > notes.Count - 1 || (Properties.Settings.Default.FirstRegionNote > notes.Count - 1 && Properties.Settings.Default.SecondRegionNote > notes.Count - 1))
                {
                    if (Properties.Settings.Default.FirstRegionNote > notes.Count - 1)
                    {
                        if (notes.Count == 0)
                        {
                            Properties.Settings.Default.FirstRegionNote = -1;
                        } else if (notes.Count == 1 || notes.Count > 1)
                        {
                            Properties.Settings.Default.FirstRegionNote = 0;
                        }
                    }
                    if (Properties.Settings.Default.SecondRegionNote > notes.Count - 1)
                    {
                        if (notes.Count == 0 || notes.Count == 1)
                        {
                            Properties.Settings.Default.SecondRegionNote = -1;
                        } else if (notes.Count > 1)
                        {
                            Properties.Settings.Default.SecondRegionNote = 1;
                        }
                    }
                }
            }

            if (creatingNote)
            {
                if (!Properties.Settings.Default.SelectedRegion)
                {
                    firstRegionNote.SelectedIndex = firstRegionNote.Items.Count - 1;
                    secondRegionNote.SelectedIndex = Properties.Settings.Default.SecondRegionNote;
                }
                else
                {
                    firstRegionNote.SelectedIndex = Properties.Settings.Default.FirstRegionNote;
                    secondRegionNote.SelectedIndex = secondRegionNote.Items.Count - 1;
                }
            } else if (removingNote)
            {
                if (!Properties.Settings.Default.SelectedRegion)
                {
                    firstRegionNote.SelectedIndex = Properties.Settings.Default.FirstRegionNote - 1;
                    secondRegionNote.SelectedIndex = altRegionCorrection ? Properties.Settings.Default.SecondRegionNote - 1 : Properties.Settings.Default.SecondRegionNote;
                } else
                {
                    firstRegionNote.SelectedIndex = altRegionCorrection ? Properties.Settings.Default.FirstRegionNote - 1 : Properties.Settings.Default.FirstRegionNote;
                    secondRegionNote.SelectedIndex = Properties.Settings.Default.SecondRegionNote - 1;
                }
            } else
            {
                firstRegionNote.SelectedIndex = Properties.Settings.Default.FirstRegionNote;
                secondRegionNote.SelectedIndex = Properties.Settings.Default.SecondRegionNote;
            }
        }

        private void switchToModernLightTheme()
        {
            modern.Checked = true;
            modernWindowsColors.Checked = false;
            oldModern.Checked = false;
            secret.Checked = false;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Popup;
            chooseSecondRegion.FlatStyle = FlatStyle.Popup;
            firstRegionNote.FlatStyle = FlatStyle.Popup;
            secondRegionNote.FlatStyle = FlatStyle.Popup;

            firstRegionTitle.BorderStyle = BorderStyle.None;
            firstRegionText.BorderStyle = BorderStyle.None;
            secondRegionTitle.BorderStyle = BorderStyle.None;
            secondRegionText.BorderStyle = BorderStyle.None;

            this.BackColor = modernlightbackcolor;

            menuStrip1.BackColor = modernlightforecolor;

            manageNote.ForeColor = Color.Empty;
            settings.ForeColor = Color.Empty;
            about.ForeColor = Color.Empty;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            save.BackColor = Color.Empty;
            save.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = settedaccentcolor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = modernlightforecolor;
                chooseSecondRegion.ForeColor = Color.Empty;
            }
            else
            {
                chooseFirstRegion.BackColor = modernlightforecolor;
                chooseFirstRegion.ForeColor = Color.Empty;

                chooseSecondRegion.BackColor = settedaccentcolor;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = modernlightforecolor;
            firstRegionNote.ForeColor = Color.Empty;

            secondRegionNote.BackColor = modernlightforecolor;
            secondRegionNote.ForeColor = Color.Empty;

            firstRegionTitle.BackColor = modernlightforecolor;
            firstRegionTitle.ForeColor = Color.Empty;

            firstRegionText.BackColor = modernlightforecolor;
            firstRegionText.ForeColor = Color.Empty;

            secondRegionTitle.BackColor = modernlightforecolor;
            secondRegionTitle.ForeColor = Color.Empty;

            secondRegionText.BackColor = modernlightforecolor;
            secondRegionText.ForeColor = Color.Empty;
        }

        private void switchToModernDarkTheme()
        {
            modern.Checked = true;
            modernWindowsColors.Checked = false;
            oldModern.Checked = false;
            secret.Checked = false;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Popup;
            chooseSecondRegion.FlatStyle = FlatStyle.Popup;
            firstRegionNote.FlatStyle = FlatStyle.Popup;
            secondRegionNote.FlatStyle = FlatStyle.Popup;

            firstRegionTitle.BorderStyle = BorderStyle.None;
            firstRegionText.BorderStyle = BorderStyle.None;
            secondRegionTitle.BorderStyle = BorderStyle.None;
            secondRegionText.BorderStyle = BorderStyle.None;

            this.BackColor = moderndarkbackcolor;

            menuStrip1.BackColor = moderndarkforecolor;

            manageNote.ForeColor = Color.White;
            settings.ForeColor = Color.White;
            about.ForeColor = Color.White;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            save.BackColor = Color.Empty;
            save.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = settedaccentcolor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = moderndarkforecolor;
                chooseSecondRegion.ForeColor = Color.White;
            } else
            {
                chooseFirstRegion.BackColor = moderndarkforecolor;
                chooseFirstRegion.ForeColor = Color.White;
                
                chooseSecondRegion.BackColor = settedaccentcolor;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = moderndarkforecolor;
            firstRegionNote.ForeColor = Color.White;

            secondRegionNote.BackColor = moderndarkforecolor;
            secondRegionNote.ForeColor = Color.White;

            firstRegionTitle.BackColor = moderndarkforecolor;
            firstRegionTitle.ForeColor = Color.White;

            firstRegionText.BackColor = moderndarkforecolor;
            firstRegionText.ForeColor = Color.White;

            secondRegionTitle.BackColor = moderndarkforecolor;
            secondRegionTitle.ForeColor = Color.White;

            secondRegionText.BackColor = moderndarkforecolor;
            secondRegionText.ForeColor = Color.White;
        }

        private void switchToModernAccentLightTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = true;
            oldModern.Checked = false;
            secret.Checked = false;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Popup;
            chooseSecondRegion.FlatStyle = FlatStyle.Popup;
            firstRegionNote.FlatStyle = FlatStyle.Popup;
            secondRegionNote.FlatStyle = FlatStyle.Popup;

            firstRegionTitle.BorderStyle = BorderStyle.None;
            firstRegionText.BorderStyle = BorderStyle.None;
            secondRegionTitle.BorderStyle = BorderStyle.None;
            secondRegionText.BorderStyle = BorderStyle.None;

            this.BackColor = modernaccentlightbackcolor;

            menuStrip1.BackColor = modernaccentlightforecolor;

            manageNote.ForeColor = Color.Empty;
            settings.ForeColor = Color.Empty;
            about.ForeColor = Color.Empty;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            save.BackColor = Color.Empty;
            save.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = accentColor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = modernaccentlightforecolor;
                chooseSecondRegion.ForeColor = Color.Empty;
            }
            else
            {
                chooseFirstRegion.BackColor = modernaccentlightforecolor;
                chooseFirstRegion.ForeColor = Color.Empty;

                chooseSecondRegion.BackColor = accentColor;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = modernaccentlightforecolor;
            firstRegionNote.ForeColor = Color.Empty;

            secondRegionNote.BackColor = modernaccentlightforecolor;
            secondRegionNote.ForeColor = Color.Empty;

            firstRegionTitle.BackColor = modernaccentlightforecolor;
            firstRegionTitle.ForeColor = Color.Empty;

            firstRegionText.BackColor = modernaccentlightforecolor;
            firstRegionText.ForeColor = Color.Empty;

            secondRegionTitle.BackColor = modernaccentlightforecolor;
            secondRegionTitle.ForeColor = Color.Empty;

            secondRegionText.BackColor = modernaccentlightforecolor;
            secondRegionText.ForeColor = Color.Empty;
        }

        private void switchToModernAccentDarkTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = true;
            oldModern.Checked = false;
            secret.Checked = false;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Popup;
            chooseSecondRegion.FlatStyle = FlatStyle.Popup;
            firstRegionNote.FlatStyle = FlatStyle.Popup;
            secondRegionNote.FlatStyle = FlatStyle.Popup;

            firstRegionTitle.BorderStyle = BorderStyle.None;
            firstRegionText.BorderStyle = BorderStyle.None;
            secondRegionTitle.BorderStyle = BorderStyle.None;
            secondRegionText.BorderStyle = BorderStyle.None;

            this.BackColor = modernaccentdarkbackcolor;

            menuStrip1.BackColor = modernaccentdarkforecolor;

            manageNote.ForeColor = Color.White;
            settings.ForeColor = Color.White;
            about.ForeColor = Color.White;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            save.BackColor = Color.Empty;
            save.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = accentColor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = modernaccentdarkforecolor;
                chooseSecondRegion.ForeColor = Color.White;
            }
            else
            {
                chooseFirstRegion.BackColor = modernaccentdarkforecolor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = accentColor;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = modernaccentdarkforecolor;
            firstRegionNote.ForeColor = Color.White;

            secondRegionNote.BackColor = modernaccentdarkforecolor;
            secondRegionNote.ForeColor = Color.White;

            firstRegionTitle.BackColor = modernaccentdarkforecolor;
            firstRegionTitle.ForeColor = Color.White;

            firstRegionText.BackColor = modernaccentdarkforecolor;
            firstRegionText.ForeColor = Color.White;

            secondRegionTitle.BackColor = modernaccentdarkforecolor;
            secondRegionTitle.ForeColor = Color.White;

            secondRegionText.BackColor = modernaccentdarkforecolor;
            secondRegionText.ForeColor = Color.White;
        }

        private void switchToOldModernLightTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = false;
            oldModern.Checked = true;
            secret.Checked = false;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Standard;
            chooseSecondRegion.FlatStyle = FlatStyle.Standard;
            firstRegionNote.FlatStyle = FlatStyle.Standard;
            secondRegionNote.FlatStyle = FlatStyle.Standard;

            firstRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            firstRegionText.BorderStyle = BorderStyle.Fixed3D;
            secondRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            secondRegionText.BorderStyle = BorderStyle.Fixed3D;
            
            this.BackColor = Color.White;

            menuStrip1.BackColor = Color.White;

            manageNote.ForeColor = Color.Empty;
            settings.ForeColor = Color.Empty;
            about.ForeColor = Color.Empty;

            createNote.BackColor = Color.White;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.White;
            removeNote.ForeColor = Color.Empty;

            save.BackColor = Color.White;
            save.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.White;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.White;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.White;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.White;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.White;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.White;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.White;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.White;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.White;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.White;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.White;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.White;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.White;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.White;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.White;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = Color.CornflowerBlue;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = Color.White;
                chooseSecondRegion.ForeColor = Color.Empty;
            }
            else
            {
                chooseFirstRegion.BackColor = Color.White;
                chooseFirstRegion.ForeColor = Color.Empty;

                chooseSecondRegion.BackColor = Color.CornflowerBlue;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = Color.White;
            firstRegionNote.ForeColor = Color.Empty;

            secondRegionNote.BackColor = Color.White;
            secondRegionNote.ForeColor = Color.Empty;

            firstRegionTitle.BackColor = Color.White;
            firstRegionTitle.ForeColor = Color.Empty;

            firstRegionText.BackColor = Color.White;
            firstRegionText.ForeColor = Color.Empty;

            secondRegionTitle.BackColor = Color.White;
            secondRegionTitle.ForeColor = Color.Empty;

            secondRegionText.BackColor = Color.White;
            secondRegionText.ForeColor = Color.Empty;
        }

        private void switchToOldModernDarkTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = false;
            oldModern.Checked = true;
            secret.Checked = false;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Standard;
            chooseSecondRegion.FlatStyle = FlatStyle.Standard;
            firstRegionNote.FlatStyle = FlatStyle.Standard;
            secondRegionNote.FlatStyle = FlatStyle.Standard;

            firstRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            firstRegionText.BorderStyle = BorderStyle.Fixed3D;
            secondRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            secondRegionText.BorderStyle = BorderStyle.Fixed3D;

            this.BackColor = oldmodernbackcolor;

            menuStrip1.BackColor = oldmodernforecolor;

            manageNote.ForeColor = Color.White;
            settings.ForeColor = Color.White;
            about.ForeColor = Color.White;

            createNote.BackColor = oldmodernforecolor;
            createNote.ForeColor = Color.White;

            removeNote.BackColor = oldmodernforecolor;
            removeNote.ForeColor = Color.White;

            save.BackColor = oldmodernforecolor;
            save.ForeColor = Color.White;

            autoSaveNotes.BackColor = oldmodernforecolor;
            autoSaveNotes.ForeColor = Color.White;

            language.BackColor = oldmodernforecolor;
            language.ForeColor = Color.White;

            theme.BackColor = oldmodernforecolor;
            theme.ForeColor = Color.White;

            ukrainian.BackColor = oldmodernforecolor;
            ukrainian.ForeColor = Color.White;

            english.BackColor = oldmodernforecolor;
            english.ForeColor = Color.White;

            type.BackColor = oldmodernforecolor;
            type.ForeColor = Color.White;

            style.BackColor = oldmodernforecolor;
            style.ForeColor = Color.White;

            system.BackColor = oldmodernforecolor;
            system.ForeColor = Color.White;

            light.BackColor = oldmodernforecolor;
            light.ForeColor = Color.White;

            dark.BackColor = oldmodernforecolor;
            dark.ForeColor = Color.White;

            modern.BackColor = oldmodernforecolor;
            modern.ForeColor = Color.White;

            modernWindowsColors.BackColor = oldmodernforecolor;
            modernWindowsColors.ForeColor = Color.White;

            oldModern.BackColor = oldmodernforecolor;
            oldModern.ForeColor = Color.White;

            secret.BackColor = oldmodernforecolor;
            secret.ForeColor = Color.White;

            classic.BackColor = oldmodernforecolor;
            classic.ForeColor = Color.White;


            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = Color.CornflowerBlue;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = oldmodernforecolor;
                chooseSecondRegion.ForeColor = Color.White;
            }
            else
            {
                chooseFirstRegion.BackColor = oldmodernforecolor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = Color.CornflowerBlue;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = oldmodernforecolor;
            firstRegionNote.ForeColor = Color.White;

            secondRegionNote.BackColor = oldmodernforecolor;
            secondRegionNote.ForeColor = Color.White;

            firstRegionTitle.BackColor = oldmodernforecolor;
            firstRegionTitle.ForeColor = Color.White;

            firstRegionText.BackColor = oldmodernforecolor;
            firstRegionText.ForeColor = Color.White;

            secondRegionTitle.BackColor = oldmodernforecolor;
            secondRegionTitle.ForeColor = Color.White;

            secondRegionText.BackColor = oldmodernforecolor;
            secondRegionText.ForeColor = Color.White;
        }

        private void switchToSecretLightTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = false;
            oldModern.Checked = false;
            secret.Checked = true;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Popup;
            chooseSecondRegion.FlatStyle = FlatStyle.Popup;
            firstRegionNote.FlatStyle = FlatStyle.Popup;
            secondRegionNote.FlatStyle = FlatStyle.Popup;

            firstRegionTitle.BorderStyle = BorderStyle.None;
            firstRegionText.BorderStyle = BorderStyle.None;
            secondRegionTitle.BorderStyle = BorderStyle.None;
            secondRegionText.BorderStyle = BorderStyle.None;

            this.BackColor = Color.White;

            menuStrip1.BackColor = Color.White;

            manageNote.ForeColor = Color.Empty;
            settings.ForeColor = Color.Empty;
            about.ForeColor = Color.Empty;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = Color.CornflowerBlue;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = Color.White;
                chooseSecondRegion.ForeColor = Color.Empty;
            }
            else
            {
                chooseFirstRegion.BackColor = Color.White;
                chooseFirstRegion.ForeColor = Color.Empty;

                chooseSecondRegion.BackColor = Color.CornflowerBlue;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = Color.White;
            firstRegionNote.ForeColor = Color.Empty;

            secondRegionNote.BackColor = Color.White;
            secondRegionNote.ForeColor = Color.Empty;

            firstRegionTitle.BackColor = Color.White;
            firstRegionTitle.ForeColor = Color.Empty;

            firstRegionText.BackColor = Color.White;
            firstRegionText.ForeColor = Color.Empty;

            secondRegionTitle.BackColor = Color.White;
            secondRegionTitle.ForeColor = Color.Empty;

            secondRegionText.BackColor = Color.White;
            secondRegionText.ForeColor = Color.Empty;
        }

        private void switchToSecretDarkTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = false;
            oldModern.Checked = false;
            secret.Checked = true;
            classic.Checked = false;
            classicWindowsColors.Checked = false;

            chooseFirstRegion.FlatStyle = FlatStyle.Popup;
            chooseSecondRegion.FlatStyle = FlatStyle.Popup;
            firstRegionNote.FlatStyle = FlatStyle.Popup;
            secondRegionNote.FlatStyle = FlatStyle.Popup;

            firstRegionTitle.BorderStyle = BorderStyle.None;
            firstRegionText.BorderStyle = BorderStyle.None;
            secondRegionTitle.BorderStyle = BorderStyle.None;
            secondRegionText.BorderStyle = BorderStyle.None;

            this.BackColor = oldmodernbackcolor;

            menuStrip1.BackColor = oldmodernforecolor;

            manageNote.ForeColor = Color.White;
            settings.ForeColor = Color.White;
            about.ForeColor = Color.White;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = Color.CornflowerBlue;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = oldmodernforecolor;
                chooseSecondRegion.ForeColor = Color.White;
            }
            else
            {
                chooseFirstRegion.BackColor = oldmodernforecolor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = Color.CornflowerBlue;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = oldmodernforecolor;
            firstRegionNote.ForeColor = Color.White;

            secondRegionNote.BackColor = oldmodernforecolor;
            secondRegionNote.ForeColor = Color.White;

            firstRegionTitle.BackColor = oldmodernforecolor;
            firstRegionTitle.ForeColor = Color.White;

            firstRegionText.BackColor = oldmodernforecolor;
            firstRegionText.ForeColor = Color.White;

            secondRegionTitle.BackColor = oldmodernforecolor;
            secondRegionTitle.ForeColor = Color.White;

            secondRegionText.BackColor = oldmodernforecolor;
            secondRegionText.ForeColor = Color.White;
        }

        private void switchToClassicLightTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = false;
            oldModern.Checked = false;
            secret.Checked = false;
            if (!Properties.Settings.Default.AccentClassicTheme || !win10orlater)
            {
                classic.Checked = true;
                classicWindowsColors.Checked = false;
            } else
            {
                classic.Checked = false;
                classicWindowsColors.Checked = true;
            }

            chooseFirstRegion.FlatStyle = FlatStyle.Standard;
            chooseSecondRegion.FlatStyle = FlatStyle.Standard;
            firstRegionNote.FlatStyle = FlatStyle.Standard;
            secondRegionNote.FlatStyle = FlatStyle.Standard;

            firstRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            firstRegionText.BorderStyle = BorderStyle.Fixed3D;
            secondRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            secondRegionText.BorderStyle = BorderStyle.Fixed3D;

            this.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? modernlightbackcolor : modernaccentlightbackcolor;

            menuStrip1.BackColor = Color.Empty;

            manageNote.ForeColor = Color.Empty;
            settings.ForeColor = Color.Empty;
            about.ForeColor = Color.Empty;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? settedaccentcolor : accentColor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = Button.DefaultBackColor;
                chooseSecondRegion.ForeColor = Color.Empty;
            }
            else
            {
                chooseFirstRegion.BackColor = Button.DefaultBackColor;
                chooseFirstRegion.ForeColor = Color.Empty;

                chooseSecondRegion.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? settedaccentcolor : accentColor;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = Color.Empty;
            firstRegionNote.ForeColor = Color.Empty;

            secondRegionNote.BackColor = Color.Empty;
            secondRegionNote.ForeColor = Color.Empty;

            firstRegionTitle.BackColor = Color.Empty;
            firstRegionTitle.ForeColor = Color.Empty;

            firstRegionText.BackColor = Color.Empty;
            firstRegionText.ForeColor = Color.Empty;

            secondRegionTitle.BackColor = Color.Empty;
            secondRegionTitle.ForeColor = Color.Empty;

            secondRegionText.BackColor = Color.Empty;
            secondRegionText.ForeColor = Color.Empty;
        }

        private void switchToClassicDarkTheme()
        {
            modern.Checked = false;
            modernWindowsColors.Checked = false;
            oldModern.Checked = false;
            secret.Checked = false;
            if (!Properties.Settings.Default.AccentClassicTheme || !win10orlater)
            {
                classic.Checked = true;
                classicWindowsColors.Checked = false;
            } else
            {
                classic.Checked = false;
                classicWindowsColors.Checked = true;
            }

            chooseFirstRegion.FlatStyle = FlatStyle.Standard;
            chooseSecondRegion.FlatStyle = FlatStyle.Standard;
            firstRegionNote.FlatStyle = FlatStyle.Standard;
            secondRegionNote.FlatStyle = FlatStyle.Standard;

            firstRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            firstRegionText.BorderStyle = BorderStyle.Fixed3D;
            secondRegionTitle.BorderStyle = BorderStyle.Fixed3D;
            secondRegionText.BorderStyle = BorderStyle.Fixed3D;

            this.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? moderndarkbackcolor : modernaccentdarkbackcolor;

            menuStrip1.BackColor = Color.Empty;

            manageNote.ForeColor = Color.Empty;
            settings.ForeColor = Color.Empty;
            about.ForeColor = Color.Empty;

            createNote.BackColor = Color.Empty;
            createNote.ForeColor = Color.Empty;

            removeNote.BackColor = Color.Empty;
            removeNote.ForeColor = Color.Empty;

            autoSaveNotes.BackColor = Color.Empty;
            autoSaveNotes.ForeColor = Color.Empty;

            language.BackColor = Color.Empty;
            language.ForeColor = Color.Empty;

            theme.BackColor = Color.Empty;
            theme.ForeColor = Color.Empty;

            ukrainian.BackColor = Color.Empty;
            ukrainian.ForeColor = Color.Empty;

            english.BackColor = Color.Empty;
            english.ForeColor = Color.Empty;

            type.BackColor = Color.Empty;
            type.ForeColor = Color.Empty;

            style.BackColor = Color.Empty;
            style.ForeColor = Color.Empty;

            system.BackColor = Color.Empty;
            system.ForeColor = Color.Empty;

            light.BackColor = Color.Empty;
            light.ForeColor = Color.Empty;

            dark.BackColor = Color.Empty;
            dark.ForeColor = Color.Empty;

            modern.BackColor = Color.Empty;
            modern.ForeColor = Color.Empty;

            modernWindowsColors.BackColor = Color.Empty;
            modernWindowsColors.ForeColor = Color.Empty;

            oldModern.BackColor = Color.Empty;
            oldModern.ForeColor = Color.Empty;

            secret.BackColor = Color.Empty;
            secret.ForeColor = Color.Empty;

            classic.BackColor = Color.Empty;
            classic.ForeColor = Color.Empty;

            if (Properties.Settings.Default.SelectedRegion == false)
            {
                chooseFirstRegion.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? settedaccentcolor : accentColor;
                chooseFirstRegion.ForeColor = Color.White;

                chooseSecondRegion.BackColor = Button.DefaultBackColor;
                chooseSecondRegion.ForeColor = Color.Empty;
            }
            else
            {
                chooseFirstRegion.BackColor = Button.DefaultBackColor;
                chooseFirstRegion.ForeColor = Color.Empty;

                chooseSecondRegion.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? settedaccentcolor : accentColor;
                chooseSecondRegion.ForeColor = Color.White;
            }

            firstRegionNote.BackColor = Color.Empty;
            firstRegionNote.ForeColor = Color.Empty;

            secondRegionNote.BackColor = Color.Empty;
            secondRegionNote.ForeColor = Color.Empty;

            firstRegionTitle.BackColor = Color.Empty;
            firstRegionTitle.ForeColor = Color.Empty;

            firstRegionText.BackColor = Color.Empty;
            firstRegionText.ForeColor = Color.Empty;

            secondRegionTitle.BackColor = Color.Empty;
            secondRegionTitle.ForeColor = Color.Empty;

            secondRegionText.BackColor = Color.Empty;
            secondRegionText.ForeColor = Color.Empty;
        }

        private void modern_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ThemeType != 1)
            {
                Properties.Settings.Default.ThemeType = 1;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Theme == 1)
                {
                    system_Click(sender, null);
                } else if (Properties.Settings.Default.Theme == 2)
                {
                    light_Click(sender, null);
                } else
                {
                    dark_Click(sender, null);
                }
            }
        }

        private void modernWindowsColors_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ThemeType != 2)
            {
                Properties.Settings.Default.ThemeType = 2;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Theme == 1)
                {
                    system_Click(sender, null);
                } else if (Properties.Settings.Default.Theme == 2)
                {
                    light_Click(sender, null);
                } else
                {
                    dark_Click(sender, null);
                }
            }
        }

        private void oldModern_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ThemeType != 3)
            {
                Properties.Settings.Default.ThemeType = 3;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Theme == 1)
                {
                    system_Click(sender, null);
                } else if (Properties.Settings.Default.Theme == 2)
                {
                    light_Click(sender, null);
                } else
                {
                    dark_Click(sender, null);
                }
            }
        }

        private void secret_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ThemeType != 5)
            {
                Properties.Settings.Default.ThemeType = 5;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Theme == 1)
                {
                    system_Click(sender, null);
                } else if (Properties.Settings.Default.Theme == 2)
                {
                    light_Click(sender, null);
                } else
                {
                    dark_Click(sender, null);
                }
            }
        }

        private void classic_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ThemeType != 4 || Properties.Settings.Default.AccentClassicTheme)
            {
                Properties.Settings.Default.AccentClassicTheme = false;
                Properties.Settings.Default.ThemeType = 4;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Theme == 1)
                {
                    system_Click(sender, null);
                } else if (Properties.Settings.Default.Theme == 2)
                {
                    light_Click(sender, null);
                } else
                {
                    dark_Click(sender, null);
                }
            }
        }
        
        private void classicWindowsColors_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.AccentClassicTheme)
            {
                Properties.Settings.Default.AccentClassicTheme = true;
                Properties.Settings.Default.ThemeType = 4;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Theme == 1)
                {
                    system_Click(system, null);
                } else if (Properties.Settings.Default.Theme == 2)
                {
                    light_Click(light, null);
                } else
                {
                    dark_Click(dark, null);
                }
            }
        }

        private void manageNote_DropDownOpened(object sender, EventArgs e)
        {
            if ((Properties.Settings.Default.Theme == 3 || Properties.Settings.Default.SystemTheme == true) && Properties.Settings.Default.ThemeType != 4)
            {
                manageNote.ForeColor = Color.Black;
            }
        }

        private void manageNote_DropDownClosed(object sender, EventArgs e)
        {
            if ((Properties.Settings.Default.Theme == 3 || Properties.Settings.Default.SystemTheme == true) && Properties.Settings.Default.ThemeType != 4)
            {
                manageNote.ForeColor = Color.White;
            }
        }

        private void settings_DropDownOpened(object sender, EventArgs e)
        {
            if ((Properties.Settings.Default.Theme == 3 || Properties.Settings.Default.SystemTheme == true) && Properties.Settings.Default.ThemeType != 4)
            {
                settings.ForeColor = Color.Black;
            }
        }

        private void settings_DropDownClosed(object sender, EventArgs e)
        {
            if ((Properties.Settings.Default.Theme == 3 || Properties.Settings.Default.SystemTheme == true) && Properties.Settings.Default.ThemeType != 4)
            {
                settings.ForeColor = Color.White;
            }
        }

        private void createNote_Click(object sender, EventArgs e)
        {
            string firstregion = "Область 1";
            string firstregionedited = "*Область 1";
            string secondregion = "Область 2";
            string secondregionedited = "*Область 2";
            if (Properties.Settings.Default.UseEnglishLanguage)
            {
                firstregion = "1 region";
                firstregionedited = "*1 region";
                secondregion = "2 region";
                secondregionedited = "*2 region";
            }

            bool notSaved = false;

            if (!Properties.Settings.Default.SelectedRegion)
            {
                if (chooseFirstRegion.Text == firstregionedited)
                {
                    DialogResult dialogresult = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережена нотатка в Області 1\n\nБажаєте її зберегти?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved note in 1 region\n\nDo you want to save it?", "Saving note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogresult == DialogResult.Yes)
                    {
                        notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                        notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);

                        chooseFirstRegion.Text = firstregion;
                    }
                    else if (dialogresult == DialogResult.Cancel)
                    {
                        notSaved = true;
                    }
                }
            } else
            {
                if (chooseSecondRegion.Text == secondregionedited)
                {
                    DialogResult dialogresult = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережена нотатка в Області 1\n\nБажаєте її зберегти?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved note in 1 region\n\nDo you want to save it?", "Saving note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogresult == DialogResult.Yes)
                    {
                        notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                        notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);

                        chooseSecondRegion.Text = secondregion;
                    }
                    else if (dialogresult == DialogResult.Cancel)
                    {
                        notSaved = true;
                    }
                }
            }
            if (!notSaved)
            {
                string title = !Properties.Settings.Default.UseEnglishLanguage ? Interaction.InputBox("Введіть заголовок нотатки", "Створення нотатки") : Interaction.InputBox("Enter note title", "Creating note");
                if (title != "")
                {
                    notes.Add(new Note(title));
                    string data = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("Notes.json", data);
                    refreshNotes(false, true);
                }
                else
                {
                    if (!Properties.Settings.Default.UseEnglishLanguage)
                    {
                        MessageBox.Show("Неможливо створити нотатку з порожнім іменем", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else
                    {
                        MessageBox.Show("Impossible to create note with empty note", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void chooseFirstRegion_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SelectedRegion == true)
            {
                Properties.Settings.Default.SelectedRegion = false;
                Properties.Settings.Default.Save();
                
                if (Properties.Settings.Default.ThemeType == 1)
                {
                    chooseFirstRegion.BackColor = settedaccentcolor;
                } else if (Properties.Settings.Default.ThemeType == 2)
                {
                    chooseFirstRegion.BackColor = accentColor;
                } else
                {
                    chooseFirstRegion.BackColor = Color.CornflowerBlue;
                }

                chooseFirstRegion.ForeColor = Color.White;

                if (Properties.Settings.Default.ThemeType == 1)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseSecondRegion.BackColor = modernlightforecolor;
                        chooseSecondRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseSecondRegion.BackColor = moderndarkforecolor;
                        chooseSecondRegion.ForeColor = Color.White;
                    }
                } else if (Properties.Settings.Default.ThemeType == 2)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseSecondRegion.BackColor = modernaccentlightforecolor;
                        chooseSecondRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseSecondRegion.BackColor = modernaccentdarkforecolor;
                        chooseSecondRegion.ForeColor = Color.White;
                    }
                } else if (Properties.Settings.Default.ThemeType == 3)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseSecondRegion.BackColor = Color.White;
                        chooseSecondRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseSecondRegion.BackColor = oldmodernforecolor;
                        chooseSecondRegion.ForeColor = Color.White;
                    }
                } else if (Properties.Settings.Default.ThemeType == 5)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseSecondRegion.BackColor = Color.White;
                        chooseSecondRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseSecondRegion.BackColor = oldmodernforecolor;
                        chooseSecondRegion.ForeColor = Color.White;
                    }
                } else
                {
                    chooseSecondRegion.BackColor = Button.DefaultBackColor;
                    chooseSecondRegion.ForeColor = Color.Empty;
                }
            }
        }

        private void chooseSecondRegion_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SelectedRegion == false)
            {
                Properties.Settings.Default.SelectedRegion = true;
                Properties.Settings.Default.Save();
                
                if (Properties.Settings.Default.ThemeType == 1)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseFirstRegion.BackColor = modernlightforecolor;
                        chooseFirstRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseFirstRegion.BackColor = moderndarkforecolor;
                        chooseFirstRegion.ForeColor = Color.White;
                    }
                } else if (Properties.Settings.Default.ThemeType == 2)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseFirstRegion.BackColor = modernaccentlightforecolor;
                        chooseFirstRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseFirstRegion.BackColor = modernaccentdarkforecolor;
                        chooseFirstRegion.ForeColor = Color.White;
                    }
                } else if (Properties.Settings.Default.ThemeType == 3)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseFirstRegion.BackColor = Color.White;
                        chooseFirstRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseFirstRegion.BackColor = oldmodernforecolor;
                        chooseFirstRegion.ForeColor = Color.White;
                    }
                } else if (Properties.Settings.Default.ThemeType == 5)
                {
                    if (Properties.Settings.Default.Theme == 2 || !Properties.Settings.Default.SystemTheme)
                    {
                        chooseFirstRegion.BackColor = Color.White;
                        chooseFirstRegion.ForeColor = Color.Empty;
                    } else
                    {
                        chooseFirstRegion.BackColor = oldmodernforecolor;
                        chooseFirstRegion.ForeColor = Color.White;
                    }
                } else
                {
                    chooseFirstRegion.BackColor = Button.DefaultBackColor;
                    chooseFirstRegion.ForeColor = Color.Empty;
                }
                
                if (Properties.Settings.Default.ThemeType == 1)
                {
                    chooseSecondRegion.BackColor = settedaccentcolor;
                } else if (Properties.Settings.Default.ThemeType == 2)
                {
                    chooseSecondRegion.BackColor = accentColor;
                } else
                {
                    chooseSecondRegion.BackColor = Color.CornflowerBlue;
                }

                chooseSecondRegion.ForeColor = Color.White;
            }
        }

        private void firstRegionNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!refreshingNotes && Properties.Settings.Default.FirstRegionNote != -1)
            {
                firstNoteLoading = true;

                if (firstRegionNote.SelectedIndex == secondRegionNote.SelectedIndex && secondNoteLoading == false && firstRegionNote.SelectedIndex != -1)
                {
                    secondRegionNote.SelectedIndex = firstPreviousIndex;
                }

                Properties.Settings.Default.FirstRegionNote = firstRegionNote.SelectedIndex;
                Properties.Settings.Default.FirstRegionTitle = notes[firstRegionNote.SelectedIndex].title;

                Properties.Settings.Default.Save();

                firstRegionTitle.Text = notes[firstRegionNote.SelectedIndex].title;
                firstRegionText.Text = notes[firstRegionNote.SelectedIndex].text;

                /*MessageBox.Show("В кожній області мають бути різні нотатки", "Нагадування", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                firstRegionNote.SelectedIndex = firstPreviousIndex;*/

                firstPreviousIndex = firstRegionNote.SelectedIndex;

                firstNoteLoading = false;
            }
        }

        private void secondRegionNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!refreshingNotes && Properties.Settings.Default.SecondRegionNote != -1)
            {
                secondNoteLoading = true;

                if (firstRegionNote.SelectedIndex == secondRegionNote.SelectedIndex && secondRegionNote.SelectedIndex != -1)
                {
                    firstRegionNote.SelectedIndex = secondPreviousIndex;
                }

                Properties.Settings.Default.SecondRegionNote = secondRegionNote.SelectedIndex;
                Properties.Settings.Default.SecondRegionTitle = notes[secondRegionNote.SelectedIndex].title;

                Properties.Settings.Default.Save();

                secondRegionTitle.Text = notes[secondRegionNote.SelectedIndex].title;
                secondRegionText.Text = notes[secondRegionNote.SelectedIndex].text;

                /*MessageBox.Show("В кожній області мають бути різні нотатки", "Нагадування", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                secondRegionNote.SelectedIndex = secondPreviousIndex;*/
                secondPreviousIndex = secondRegionNote.SelectedIndex;

                secondNoteLoading = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DWMCOLORIZATIONCOLORCHANGED)
            {
                uint accentColorRaw = GetAccentColor();
                accentColor = ConvertToColor(accentColorRaw);
                double accentHue = ColorConverter.ColorToHsl(accentColor).H;
                
                modernaccentlightbackcolor = ColorConverter.HslToColor(accentHue, 100, 93.7);
                modernaccentlightforecolor = ColorConverter.HslToColor(accentHue, 74.6, 87.6);
                modernaccentdarkbackcolor = ColorConverter.HslToColor(accentHue, 100, 6.1);
                modernaccentdarkforecolor = ColorConverter.HslToColor(accentHue, 46.9, 12.5);

                if (Properties.Settings.Default.ThemeType == 2)
                {
                    if (Properties.Settings.Default.Theme == 2)
                    {
                        switchToModernAccentLightTheme();
                    }
                    else
                    {
                        switchToModernAccentDarkTheme();
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void NotesForm_Load(object sender, EventArgs e)
        {
            refreshNotes(true);
            
            if (win10orlater)
            {
                uint accentColorRaw = GetAccentColor();
                accentColor = ConvertToColor(accentColorRaw);
                double accentHue = ColorConverter.ColorToHsl(accentColor).H;
                
                modernaccentlightbackcolor = ColorConverter.HslToColor(accentHue, 100, 93.7);
                modernaccentlightforecolor = ColorConverter.HslToColor(accentHue, 74.6, 87.6);
                modernaccentdarkbackcolor = ColorConverter.HslToColor(accentHue, 100, 6.1);
                modernaccentdarkforecolor = ColorConverter.HslToColor(accentHue, 46.9, 12.5);

                /*string modernaccentlightbackcolor = "253, 100, 93.7";
                string modernaccentlightforecolor = "253, 74.6, 87.6";
                string modernaccentdarkbackcolor = "254, 100, 6.1";
                string modernaccentdarkforecolor = "252, 46.9, 12.5";*/
            }

            if (!win10orlater)
            {
                system.Visible = false;
                modernWindowsColors.Visible = false;
                classicWindowsColors.Visible = false;
            }

            if (Properties.Settings.Default.Theme == 1)
            {
                system_Click(sender, null);
            } else if (Properties.Settings.Default.Theme == 2)
            {
                light_Click(sender, null);
            } else
            {
                dark_Click(sender, null);
            }

            if (Properties.Settings.Default.AutomaticLanguage)
            {
                automatic_Click(automatic, null);
            } else if (!Properties.Settings.Default.UseEnglishLanguage)
            {
                ukrainian_Click(ukrainian, null);
            } else
            {
                english_Click(english, null);
            }

            autoSaveNotes.Checked = Properties.Settings.Default.AutoSave;
        }

        private async void firstRegionTitle_TextChanged(object sender, EventArgs e)
        {
            if (!firstNoteLoading)
            {
                bool notSaved = false;
                fnstart = DateTime.UtcNow;

                if (chooseFirstRegion.Text == "*Область 1")
                {
                    notSaved = true;
                } else
                {
                    chooseFirstRegion.Text = "*Область 1";
                }
                save.Enabled = true;

                if (fntimerworking == false)
                {
                    TimeSpan diff = TimeSpan.FromSeconds(3);
                    fntimerworking = true;

                    while ((DateTime.UtcNow - fnstart) < diff && fntimerworking == true)
                    {
                        await Task.Delay(1000);
                    }

                    if (fntimerworking == true)
                    {
                        notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                        refreshNotes();
                        fntimerworking = false;

                        if (!notSaved)
                        {
                            chooseFirstRegion.Text = "Область 1";
                        }
                        save.Enabled = false;
                    }
                }
            }
        }

        private async void secondRegionTitle_TextChanged(object sender, EventArgs e)
        {
            if (!secondNoteLoading)
            {
                bool notSaved = false;
                snstart = DateTime.UtcNow;

                if (chooseSecondRegion.Text == "*Область 2")
                {
                    notSaved = true;
                } else
                {
                    chooseSecondRegion.Text = "*Область 2";
                }
                save.Enabled = true;

                if (sntimerworking == false)
                {
                    TimeSpan diff = TimeSpan.FromSeconds(3);
                    sntimerworking = true;

                    while ((DateTime.UtcNow - snstart) < diff && sntimerworking == true)
                    {
                        await Task.Delay(1000);
                    }

                    if (sntimerworking == true)
                    {
                        notes[Properties.Settings.Default.SecondRegionNote].title = secondRegionTitle.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                        refreshNotes();
                        sntimerworking = false;

                        chooseSecondRegion.Text = "Область 2";
                        save.Enabled = false;
                    }
                }
            }
        }

        private void autoSaveNotes_Click(object sender, EventArgs e)
        {
            if (autoSaveNotes.Checked == false)
            {
                Properties.Settings.Default.AutoSave = autoSaveNotes.Checked = true;
            } else
            {
                Properties.Settings.Default.AutoSave = autoSaveNotes.Checked = false;
            }

            Properties.Settings.Default.Save();
        }

        private void system_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Theme != 1 || !win10orlater)
            {
                Properties.Settings.Default.Theme = 1;
                Properties.Settings.Default.Save();
            }

            if (win10orlater)
            {
                system.Checked = true;
                light.Checked = false;
                dark.Checked = false;

                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    var value = key?.GetValue("AppsUseLightTheme");
                    if (value is int i && i > 0)
                    {
                        light_Click(sender, null);
                    } else
                    {
                        dark_Click(sender, null);
                    }
                }
            } else
            {
                if (!Properties.Settings.Default.SystemTheme)
                {
                    light_Click(sender, null);
                } else
                {
                    dark_Click(sender, null);
                }
            }
        }

        private void light_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Theme != 2 || sender == this)
            {
                if (sender == light || (Properties.Settings.Default.Theme == 1 && !win10orlater))
                {
                    if (sender == light)
                    {
                        Properties.Settings.Default.Theme = 2;
                    }
                    Properties.Settings.Default.SystemTheme = false;

                    system.Checked = false;
                    light.Checked = true;
                    dark.Checked = false;
                } else
                {
                    Properties.Settings.Default.SystemTheme = false;
                }
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.ThemeType == 1 || (Properties.Settings.Default.ThemeType == 2 && !win10orlater))
            {
                switchToModernLightTheme();
            } else if (Properties.Settings.Default.ThemeType == 2 && win10orlater)
            {
                switchToModernAccentLightTheme();
            } else if (Properties.Settings.Default.ThemeType == 3)
            {
                switchToOldModernLightTheme();
            } else
            {
                switchToClassicLightTheme();
            }
        }

        private void dark_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Theme != 3 || sender == this)
            {
                if (sender == dark || (Properties.Settings.Default.Theme == 1 && !win10orlater))
                {
                    if (sender == dark)
                    {
                        Properties.Settings.Default.Theme = 3;
                    }
                    Properties.Settings.Default.SystemTheme = true;

                    system.Checked = false;
                    light.Checked = false;
                    dark.Checked = true;
                } else
                {
                    Properties.Settings.Default.SystemTheme = true;
                }
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.ThemeType == 1 || (Properties.Settings.Default.ThemeType == 2 && !win10orlater))
            {
                switchToModernDarkTheme();
            }
            else if (Properties.Settings.Default.ThemeType == 2 && win10orlater)
            {
                switchToModernAccentDarkTheme();
            }
            else if (Properties.Settings.Default.ThemeType == 3)
            {
                switchToOldModernDarkTheme();
            }
            else if (Properties.Settings.Default.ThemeType == 4)
            {
                switchToClassicDarkTheme();
            }
            else
            {
                switchToSecretDarkTheme();
            }
        }

        private void removeNote_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.SelectedRegion)
            {
                fntimerworking = false;
            }
            else
            {
                sntimerworking = false;
            }

            DialogResult dialogresult = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("Ви дійсно хочете видалити нотатку?", "Видалення нотатки", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You really want to remove note?", "Removing notes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dialogresult == DialogResult.Yes)
            {
                if (!Properties.Settings.Default.SelectedRegion)
                {
                    notes.RemoveAt(Properties.Settings.Default.FirstRegionNote);
                    string data = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("Notes.json", data);
                    if (Properties.Settings.Default.FirstRegionNote < Properties.Settings.Default.SecondRegionNote)
                    {
                        altRegionCorrection = true;
                    }

                    chooseFirstRegion.Text = !Properties.Settings.Default.UseEnglishLanguage ? "Область 1" : "1 region";
                } else
                {
                    notes.RemoveAt(Properties.Settings.Default.SecondRegionNote);
                    string data = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("Notes.json", data);
                    if (Properties.Settings.Default.SecondRegionNote < Properties.Settings.Default.FirstRegionNote)
                    {
                        altRegionCorrection = true;
                    }

                    chooseSecondRegion.Text = !Properties.Settings.Default.UseEnglishLanguage ? "Область 2" : "2 region";
                }

                refreshNotes(false, false, true);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.SelectedRegion)
            {
                notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                string data = JsonConvert.SerializeObject(notes);
                File.WriteAllText("Notes.json", data);
                refreshNotes();
                fntimerworking = false;

                chooseFirstRegion.Text = "Область 1";
                save.Enabled = false;
            } else
            {
                notes[Properties.Settings.Default.SecondRegionNote].title = secondRegionTitle.Text;
                notes[Properties.Settings.Default.SecondRegionNote].text = secondRegionText.Text;
                string data = JsonConvert.SerializeObject(notes);
                File.WriteAllText("Notes.json", data);
                refreshNotes();
                sntimerworking = false;

                chooseSecondRegion.Text = "Область 2";
                save.Enabled = false;
            }
        }

        private void NotesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string firstregion = "Область 1";
            string firstregionedited = "*Область 1";
            string secondregion = "Область 2";
            string secondregionedited = "*Область 2";
            if (Properties.Settings.Default.UseEnglishLanguage)
            {
                firstregion = "1 region";
                firstregionedited = "*1 region";
                secondregion = "2 region";
                secondregionedited = "*2 region";
            }

            if (!Properties.Settings.Default.SelectedRegion)
            {
                fntimerworking = false;
            } else
            {
                sntimerworking = false;
            }

            if (chooseFirstRegion.Text == firstregionedited && chooseSecondRegion.Text == secondregion)
            {
                DialogResult dialogresult = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережена нотатка в Області 1\n\nБажаєте її зберегти?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved note in 1 region\n\nDo you want to save it?", "Saving note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dialogresult == DialogResult.Yes)
                {
                    notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                    notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                    string data = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("Notes.json", data);
                }
                else if (dialogresult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            } else if (chooseFirstRegion.Text == firstregion && chooseSecondRegion.Text == secondregionedited)
            {
                DialogResult dialogresult = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережена нотатка в Області 2\n\nБажаєте її зберегти?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved note in 2 region\n\nDo you want to save it?", "Saving note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dialogresult == DialogResult.Yes)
                {
                    notes[Properties.Settings.Default.SecondRegionNote].title = secondRegionTitle.Text;
                    notes[Properties.Settings.Default.SecondRegionNote].text = secondRegionText.Text;
                    string data = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("Notes.json", data);
                }
                else if (dialogresult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            } else if (chooseFirstRegion.Text == firstregionedited && chooseSecondRegion.Text == secondregionedited)
            {
                DialogResult dialogresult1 = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережені нотатки в 1 та 2 Областях\n\nБажаєте зберегти нотатку в Області 1?", "Збереження нотаток", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved notes in 1 and 2 regions\n\nDo you want to save note in 1 region?", "Saving notes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dialogresult1 == DialogResult.Yes)
                {
                    DialogResult dialogresult2 = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережена нотатка в Області 2\n\nБажаєте зберегти її?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved note in 2 region\n\nDo you want to save it?", "Saving note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogresult2 == DialogResult.Yes)
                    {
                        notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                        notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                        notes[Properties.Settings.Default.SecondRegionNote].title = secondRegionTitle.Text;
                        notes[Properties.Settings.Default.SecondRegionNote].text = secondRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                    }
                    else if (dialogresult2 == DialogResult.No)
                    {
                        notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                        notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                    }
                    else if (dialogresult2 == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                } else if (dialogresult1 == DialogResult.No)
                {
                    DialogResult dialogresult2 = !Properties.Settings.Default.UseEnglishLanguage ? MessageBox.Show("У Вас є незбережена нотатка в Області 2\n\nБажаєте зберегти її?", "Збереження нотатки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : MessageBox.Show("You have an unsaved note in 2 region\n\nDo you want to save it?", "Saving note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogresult2 == DialogResult.Yes)
                    {
                        notes[Properties.Settings.Default.SecondRegionNote].title = secondRegionTitle.Text;
                        notes[Properties.Settings.Default.SecondRegionNote].text = secondRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                    }
                    else if (dialogresult2 == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
                else if (dialogresult1 == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private async void firstRegionText_TextChanged(object sender, EventArgs e)
        {
            if (!firstNoteLoading)
            {
                string firstregion = "Область 1";
                string firstregionedited = "*Область 1";
                if (Properties.Settings.Default.UseEnglishLanguage)
                {
                    firstregion = "1 region";
                    firstregionedited = "*1 region";
                }

                fnstart = DateTime.UtcNow;

                if (chooseFirstRegion.Text != firstregionedited)
                {
                    chooseFirstRegion.Text = firstregionedited;
                }
                save.Enabled = true;

                if (fntimerworking == false)
                {
                    TimeSpan diff = TimeSpan.FromSeconds(3);
                    fntimerworking = true;

                    while ((DateTime.UtcNow - fnstart) < diff && fntimerworking == true)
                    {
                        await Task.Delay(1000);
                    }

                    if (fntimerworking == true)
                    {
                        notes[Properties.Settings.Default.FirstRegionNote].title = firstRegionTitle.Text;
                        notes[Properties.Settings.Default.FirstRegionNote].text = firstRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                        refreshNotes();
                        fntimerworking = false;

                        chooseFirstRegion.Text = firstregion;
                        save.Enabled = false;
                    }
                }
            }
        }

        private async void secondRegionText_TextChanged(object sender, EventArgs e)
        {
            if (!secondNoteLoading)
            {
                string secondregion = "Область 2";
                string secondregionedited = "*Область 2";
                if (Properties.Settings.Default.UseEnglishLanguage)
                {
                    secondregion = "2 region";
                    secondregionedited = "*2 region";
                }

                snstart = DateTime.UtcNow;

                if (chooseSecondRegion.Text != secondregionedited)
                {
                    chooseSecondRegion.Text = secondregionedited;
                }
                save.Enabled = true;

                if (sntimerworking == false)
                {
                    TimeSpan diff = TimeSpan.FromSeconds(3);
                    sntimerworking = true;

                    while ((DateTime.UtcNow - snstart) < diff && sntimerworking == true)
                    {
                        await Task.Delay(1000);
                    }

                    if (sntimerworking == true)
                    {
                        notes[Properties.Settings.Default.SecondRegionNote].title = secondRegionTitle.Text;
                        notes[Properties.Settings.Default.SecondRegionNote].text = secondRegionText.Text;
                        string data = JsonConvert.SerializeObject(notes);
                        File.WriteAllText("Notes.json", data);
                        refreshNotes();
                        sntimerworking = false;

                        chooseSecondRegion.Text = secondregion;
                        save.Enabled = false;
                    }
                }
            }
        }

        private void automatic_Click(object sender, EventArgs e)
        {
            if (sender == automatic)
            {
                Properties.Settings.Default.AutomaticLanguage = true;
                Properties.Settings.Default.Save();

                automatic.Checked = true;
                ukrainian.Checked = false;
                english.Checked = false;
            }

            if (CultureInfo.CurrentCulture.Name == "uk-UA")
            {
                ukrainian_Click(sender, null);
            } else
            {
                english_Click(sender, null);
            }
        }

        private void ukrainian_Click(object sender, EventArgs e)
        {
            if (sender == ukrainian)
            {
                Properties.Settings.Default.AutomaticLanguage = false;
                Properties.Settings.Default.UseEnglishLanguage = false;
                Properties.Settings.Default.Save();

                automatic.Checked = false;
                ukrainian.Checked = true;
                english.Checked = false;
            }

            manageNote.Text = "Керування нотаткою";
            settings.Text = "Налаштування";
            about.Text = "Про програму";

            createNote.Text = "Створити нотатку";
            removeNote.Text = "Видалити нотатку";
            save.Text = "Зберегти";

            autoSaveNotes.Text = "Автозбереження нотаток";
            language.Text = "Мова";
            theme.Text = "Тема";

            automatic.Text = "Автоматично";
            ukrainian.Text = "Українська";
            english.Text = "Англійська";

            type.Text = "Тип";
            style.Text = "Стиль";

            system.Text = "Системна";
            light.Text = "Світла";
            dark.Text = "Темна";

            modern.Text = "Сучасний";
            modernWindowsColors.Text = "Сучасний(Кольори Windows)";
            oldModern.Text = "Старий сучасний";
            secret.Text = "Секретний";
            classic.Text = "Класичний(Win11)";
            classicWindowsColors.Text = "Класичний(Кольори Windows)";

            chooseFirstRegion.Text = "Область 1";
            chooseSecondRegion.Text = "Область 2";
        }

        private void english_Click(object sender, EventArgs e)
        {
            if (sender == english)
            {
                Properties.Settings.Default.AutomaticLanguage = false;
                Properties.Settings.Default.UseEnglishLanguage = true;
                Properties.Settings.Default.Save();

                automatic.Checked = false;
                ukrainian.Checked = false;
                english.Checked = true;
            }

            manageNote.Text = "Manage note";
            settings.Text = "Settings";
            about.Text = "About";

            createNote.Text = "Create note";
            removeNote.Text = "Remove note";
            save.Text = "Save";

            autoSaveNotes.Text = "Autosave notes";
            language.Text = "Language";
            theme.Text = "Theme";

            automatic.Text = "Automatic";
            ukrainian.Text = "Ukrainian";
            english.Text = "English";

            type.Text = "Type";
            style.Text = "Style";

            system.Text = "System";
            light.Text = "Light";
            dark.Text = "Dark";

            modern.Text = "Modern";
            modernWindowsColors.Text = "Modern(Windows colors)";
            oldModern.Text = "Old modern";
            secret.Text = "Secret";
            classic.Text = "Classic(Win11)";
            classicWindowsColors.Text = "Classic(Windows colors)";

            chooseFirstRegion.Text = "1 region";
            chooseSecondRegion.Text = "2 region";
        }

        private void about_Click(object sender, EventArgs e)
        {
            AboutForm AboutForm = new AboutForm();
            AboutForm.ShowDialog();
        }
    }

    public class ThemeWatcher : NativeWindow, IDisposable
    {
        private const int WM_SETTINGCHANGE = 0x001A;
        private readonly NotesForm _form;

        public ThemeWatcher(NotesForm form)
        {
            _form = form;

            this.AssignHandle(form.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETTINGCHANGE && Marshal.PtrToStringUni(m.LParam) == "ImmersiveColorSet")
            {
                _form.Invoke(new Action(() => _form.HandleThemeChange()));
            }
            base.WndProc(ref m);
        }

        public void Dispose()
        {
            this.ReleaseHandle();
        }
    }

    public class ThemeHelper
    {
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode)]
        public static extern void DwmGetColorizationColor(out uint colorizationColor, out bool opaqueBlend);
    }

    public static class ColorConverter
    {
        public static (double H, double S, double L) ColorToHsl(Color color)
        {
            return RgbToHsl(color.R, color.G, color.B);
        }

        public static Color HslToColor(double h, double s, double l)
        {
            var (r, g, b) = HslToRgb(h, s, l);
            return Color.FromArgb(r, g, b);
        }

        private static (int H, int S, int L) RgbToHsl(int r, int g, int b)
        {
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            double h = 0;
            if (delta != 0)
            {
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / delta + (gNorm < bNorm ? 6 : 0);
                }
                else if (max == gNorm)
                {
                    h = (bNorm - rNorm) / delta + 2;
                }
                else
                {
                    h = (rNorm - gNorm) / delta + 4;
                }
                h /= 6;
            }

            double l = (max + min) / 2;
            double s = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));

            return (Convert.ToInt32(h * 360), Convert.ToInt32(s * 100), Convert.ToInt32(l * 100));
        }

        private static (int R, int G, int B) HslToRgb(double h, double s, double l)
        {
            h /= 360;
            s /= 100;
            l /= 100;

            double r = l, g = l, b = l;
            if (s != 0)
            {
                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                r = HueToRgb(p, q, h + 1.0 / 3);
                g = HueToRgb(p, q, h);
                b = HueToRgb(p, q, h - 1.0 / 3);
            }

            return ((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1.0 / 6) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2) return q;
            if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
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
