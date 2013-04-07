using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MouseRestrict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        RamGecTools.MouseHook mouseHook = new RamGecTools.MouseHook();

        private void Form1_Load(object sender, EventArgs e)
        {
            mouseHook.MouseMove += new RamGecTools.MouseHook.MouseHookCallback(mouseHook_MouseMove);

            mouseHook.Install();

            tbProcessName.Text = Properties.Settings.Default.app;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        void mouseHook_MouseMove()
        {
            System.Diagnostics.Process[] aProc = System.Diagnostics.Process.GetProcessesByName(tbProcessName.Text);


            Point mPos = Cursor.Position;

            String pos = mPos.X.ToString() + " " + mPos.Y.ToString();
            lblMousePos.Text = pos;

            if (aProc.Length > 0 && aProc[0].MainWindowHandle == GetForegroundWindow())
            {

                var currentProc = System.Diagnostics.Process.GetCurrentProcess();
                string name = currentProc.ProcessName;
          
                var pScreen = Screen.PrimaryScreen;
            
                Size s = new System.Drawing.Size(pScreen.Bounds.Width, pScreen.Bounds.Height);
                Point p = new Point(0, 0);
                Cursor.Clip = new Rectangle(p, s);

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tbProcessName_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.app = tbProcessName.Text;
            Properties.Settings.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://matthewvlietstra.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://keyboardmousehooks.codeplex.com/");
        }
    }
}
