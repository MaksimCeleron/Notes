using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Notes
{
    public partial class AboutForm : Form
    {
        private const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
        private ThemeWatcherAboutForm _themeWatcher;

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

        public AboutForm()
        {
            InitializeComponent();
            _themeWatcher = new ThemeWatcherAboutForm(this);
            CheckWindowsVersion();
        }

        public void HandleThemeChange()
        {
            var isLightTheme = IsLightTheme();

            if (isLightTheme)
            {
                switchToLightTheme();
            }
            else
            {
                switchToDarkTheme();
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

        private void switchToSystemTheme()
        {
            if (win10orlater)
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    var value = key?.GetValue("AppsUseLightTheme");
                    if (value is int i && i > 0)
                    {
                        switchToLightTheme();
                    }
                    else
                    {
                        switchToDarkTheme();
                    }
                }
            }
            else
            {
                if (!Properties.Settings.Default.SystemTheme)
                {
                    switchToLightTheme();
                }
                else
                {
                    switchToDarkTheme();
                }
            }
        }

        private void switchToLightTheme()
        {
            if (Properties.Settings.Default.ThemeType == 1 || (Properties.Settings.Default.ThemeType == 2 && !win10orlater))
            {
                switchToModernLightTheme();
            }
            else if (Properties.Settings.Default.ThemeType == 2 && win10orlater)
            {
                switchToModernAccentLightTheme();
            }
            else if (Properties.Settings.Default.ThemeType == 3)
            {
                switchToOldModernLightTheme();
            }
            else if (Properties.Settings.Default.ThemeType == 4)
            {
                switchToClassicLightTheme();
            } else
            {
                switchToSecretLightTheme();
            }
        }

        private void switchToDarkTheme()
        {
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
            } else
            {
                switchToSecretDarkTheme();
            }
        }

        private void switchToModernLightTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Popup;
            telegramChannel.FlatStyle = FlatStyle.Popup;
            github.FlatStyle = FlatStyle.Popup;

            this.BackColor = modernlightbackcolor;

            youtubeChannel.BackColor = modernlightforecolor;
            youtubeChannel.ForeColor = Color.Empty;

            telegramChannel.BackColor = modernlightforecolor;
            telegramChannel.ForeColor = Color.Empty;

            github.BackColor = settedaccentcolor;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.Empty;
            programVersion.ForeColor = Color.Empty;
            programDeveloper.ForeColor = Color.Empty;
            programDescription.ForeColor = Color.Empty;
        }

        private void switchToModernDarkTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Popup;
            telegramChannel.FlatStyle = FlatStyle.Popup;
            github.FlatStyle = FlatStyle.Popup;

            this.BackColor = moderndarkbackcolor;

            youtubeChannel.BackColor = moderndarkforecolor;
            youtubeChannel.ForeColor = Color.White;

            telegramChannel.BackColor = moderndarkforecolor;
            telegramChannel.ForeColor = Color.White;

            github.BackColor = settedaccentcolor;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.White;
            programVersion.ForeColor = Color.White;
            programDeveloper.ForeColor = Color.White;
            programDescription.ForeColor = Color.White;
        }

        private void switchToModernAccentLightTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Popup;
            telegramChannel.FlatStyle = FlatStyle.Popup;
            github.FlatStyle = FlatStyle.Popup;

            this.BackColor = modernaccentlightbackcolor;

            youtubeChannel.BackColor = modernaccentlightforecolor;
            youtubeChannel.ForeColor = Color.Empty;

            telegramChannel.BackColor = modernaccentlightforecolor;
            telegramChannel.ForeColor = Color.Empty;

            github.BackColor = accentColor;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.Empty;
            programVersion.ForeColor = Color.Empty;
            programDeveloper.ForeColor = Color.Empty;
            programDescription.ForeColor = Color.Empty;
        }

        private void switchToModernAccentDarkTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Popup;
            telegramChannel.FlatStyle = FlatStyle.Popup;
            github.FlatStyle = FlatStyle.Popup;

            this.BackColor = modernaccentdarkbackcolor;

            youtubeChannel.BackColor = modernaccentdarkforecolor;
            youtubeChannel.ForeColor = Color.White;

            telegramChannel.BackColor = modernaccentdarkforecolor;
            telegramChannel.ForeColor = Color.White;

            github.BackColor = accentColor;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.White;
            programVersion.ForeColor = Color.White;
            programDeveloper.ForeColor = Color.White;
            programDescription.ForeColor = Color.White;
        }

        private void switchToOldModernLightTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Standard;
            telegramChannel.FlatStyle = FlatStyle.Standard;
            github.FlatStyle = FlatStyle.Standard;
            
            this.BackColor = Color.White;

            youtubeChannel.BackColor = Color.White;
            youtubeChannel.ForeColor = Color.Empty;

            telegramChannel.BackColor = Color.White;
            telegramChannel.ForeColor = Color.Empty;

            github.BackColor = Color.CornflowerBlue;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.Empty;
            programVersion.ForeColor = Color.Empty;
            programDeveloper.ForeColor = Color.Empty;
            programDescription.ForeColor = Color.Empty;
        }

        private void switchToOldModernDarkTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Standard;
            telegramChannel.FlatStyle = FlatStyle.Standard;
            github.FlatStyle = FlatStyle.Standard;

            this.BackColor = oldmodernbackcolor;

            youtubeChannel.BackColor = oldmodernforecolor;
            youtubeChannel.ForeColor = Color.White;

            telegramChannel.BackColor = oldmodernforecolor;
            telegramChannel.ForeColor = Color.White;

            github.BackColor = Color.CornflowerBlue;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.White;
            programVersion.ForeColor = Color.White;
            programDeveloper.ForeColor = Color.White;
            programDescription.ForeColor = Color.White;
        }

        private void switchToSecretLightTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Popup;
            telegramChannel.FlatStyle = FlatStyle.Popup;
            github.FlatStyle = FlatStyle.Popup;

            this.BackColor = Color.White;

            youtubeChannel.BackColor = Color.White;
            youtubeChannel.ForeColor = Color.Empty;

            telegramChannel.BackColor = Color.White;
            telegramChannel.ForeColor = Color.Empty;

            github.BackColor = Color.CornflowerBlue;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.Empty;
            programVersion.ForeColor = Color.Empty;
            programDeveloper.ForeColor = Color.Empty;
            programDescription.ForeColor = Color.Empty;
        }

        private void switchToSecretDarkTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Popup;
            telegramChannel.FlatStyle = FlatStyle.Popup;
            github.FlatStyle = FlatStyle.Popup;

            this.BackColor = oldmodernbackcolor;

            youtubeChannel.BackColor = oldmodernforecolor;
            youtubeChannel.ForeColor = Color.White;

            telegramChannel.BackColor = oldmodernforecolor;
            telegramChannel.ForeColor = Color.White;

            github.BackColor = Color.CornflowerBlue;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.White;
            programVersion.ForeColor = Color.White;
            programDeveloper.ForeColor = Color.White;
            programDescription.ForeColor = Color.White;
        }

        private void switchToClassicLightTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Standard;
            telegramChannel.FlatStyle = FlatStyle.Standard;
            github.FlatStyle = FlatStyle.Standard;

            this.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? modernlightbackcolor : modernaccentlightbackcolor;

            youtubeChannel.BackColor = Button.DefaultBackColor;
            youtubeChannel.ForeColor = Color.Empty;

            telegramChannel.BackColor = Button.DefaultBackColor;
            telegramChannel.ForeColor = Color.Empty;

            github.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? settedaccentcolor : accentColor;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.Empty;
            programVersion.ForeColor = Color.Empty;
            programDeveloper.ForeColor = Color.Empty;
            programDescription.ForeColor = Color.Empty;
        }

        private void switchToClassicDarkTheme()
        {
            youtubeChannel.FlatStyle = FlatStyle.Standard;
            telegramChannel.FlatStyle = FlatStyle.Standard;
            github.FlatStyle = FlatStyle.Standard;

            this.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? moderndarkbackcolor : modernaccentdarkbackcolor;

            youtubeChannel.BackColor = Button.DefaultBackColor;
            youtubeChannel.ForeColor = Color.Empty;

            telegramChannel.BackColor = Button.DefaultBackColor;
            telegramChannel.ForeColor = Color.Empty;

            github.BackColor = !Properties.Settings.Default.AccentClassicTheme || !win10orlater ? settedaccentcolor : accentColor;
            github.ForeColor = Color.White;

            programName.ForeColor = Color.White;
            programVersion.ForeColor = Color.White;
            programDeveloper.ForeColor = Color.White;
            programDescription.ForeColor = Color.White;
        }

        private void switchToAutomaticLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == "uk-UA")
            {
                switchToUkrainianLanguage();
            }
            else
            {
                switchToEnglishLanguage();
            }
        }

        private void switchToUkrainianLanguage()
        {
            Text = "Про додаток";
            programVersion.Text = "Версія 2.0";
            programDeveloper.Text = "Розробник: MaksimCeleron";
            programDescription.Text = "Нотатки, написані на C#";
            youtubeChannel.Text = "YouTube канал";
            telegramChannel.Text = "Telegram канал";
            github.Text = "Інші додатки";
        }

        private void switchToEnglishLanguage()
        {
            Text = "About";
            programVersion.Text = "Version 2.0";
            programDeveloper.Text = "Developer: MaksimCeleron";
            programDescription.Text = "Notes, written on C#";
            youtubeChannel.Text = "YouTube channel";
            telegramChannel.Text = "Telegram channel";
            github.Text = "Other apps";
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
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

            if (Properties.Settings.Default.Theme == 1)
            {
                switchToSystemTheme();
            }
            else if (Properties.Settings.Default.Theme == 2)
            {
                switchToLightTheme();
            }
            else
            {
                switchToDarkTheme();
            }

            if (Properties.Settings.Default.AutomaticLanguage)
            {
                switchToAutomaticLanguage();
            } else if (!Properties.Settings.Default.UseEnglishLanguage)
            {
                switchToUkrainianLanguage();
            }
            else
            {
                switchToEnglishLanguage();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCR7RC5PCfs4-RZz6TUrBQDA");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/MaksimCeleronCh");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MaksimCeleron");
        }
    }

    public class ThemeWatcherAboutForm : NativeWindow, IDisposable
    {
        private const int WM_SETTINGCHANGE = 0x001A;
        private readonly AboutForm _form;

        public ThemeWatcherAboutForm(AboutForm form)
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
}
