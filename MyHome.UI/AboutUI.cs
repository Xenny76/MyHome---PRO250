using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyHome.UI
{
    public partial class AboutUI : Form
    {
        public AboutUI()
        {
            InitializeComponent();
            // You can theme here or in Load; we’ll do it in Load to match your Designer wire-up.
        }

        private void AboutUI_Load(object sender, EventArgs e)
        {
            ApplyDarkTheme();
        }

        private void BtnOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ---- Dark theme JUST for this form ----
        private void ApplyDarkTheme()
        {
            // Palette (deep dark + dark red accent)
            Color bg = Color.FromArgb(32, 32, 36);
            Color surface = Color.FromArgb(45, 45, 48);
            Color textPrimary = Color.Gainsboro;
            Color border = Color.FromArgb(62, 62, 66);
            Color accent = Color.FromArgb(180, 16, 16);
            Color accentHover = Color.FromArgb(200, 26, 26);
            Color accentActive = Color.FromArgb(220, 36, 36);

            // Form
            this.BackColor = bg;
            this.ForeColor = textPrimary;
            TryEnableDarkTitleBar(); // Windows 10/11

            // Label
            lblAbout.BackColor = Color.Transparent;   // let the form’s bg show through
            lblAbout.ForeColor = textPrimary;

            // Button
            btnOkay.UseVisualStyleBackColor = false;
            btnOkay.FlatStyle = FlatStyle.Flat;
            btnOkay.BackColor = Color.FromArgb(55, 55, 60);
            btnOkay.ForeColor = Color.Gainsboro;
            btnOkay.FlatAppearance.BorderColor = border;
            btnOkay.FlatAppearance.MouseOverBackColor = accentHover;
            btnOkay.FlatAppearance.MouseDownBackColor = accentActive;
        }

        // ---- Optional: dark window title bar on Win10/11 ----
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private void TryEnableDarkTitleBar()
        {
            try
            {
                if (Environment.OSVersion.Version.Major < 10) return;

                int useDark = 1;
                int attr = Environment.OSVersion.Version.Build >= 18985
                    ? DWMWA_USE_IMMERSIVE_DARK_MODE
                    : DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;

                DwmSetWindowAttribute(this.Handle, attr, ref useDark, sizeof(int));
            }
            catch
            {
                // Silently ignore if not supported
            }
        }
    }
}
