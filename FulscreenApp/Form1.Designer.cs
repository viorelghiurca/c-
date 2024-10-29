using System.Windows.Forms;

namespace FullscreenApp {
    partial class Form1 {
        private void InitializeComponent() {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.BackgroundImage = global::FulscreenApp.Properties.Resources._3296364;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.Cursor = System.Windows.Forms.Cursors.No;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            MessageBox.Show("Du würdest von Vio gehackt", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
