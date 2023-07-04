using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace FasterWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get the Windows version and edition
            string edition = GetEditionFromRegistry();

            // Set the text of the label
            lblWindowsInfo.Text = $"{edition}";

            // Get the CPU information
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
            ManagementObjectCollection collection1 = searcher1.Get();

            // Iterate through the CPU information (assuming only one CPU)
            foreach (ManagementObject obj in collection1)
            {
                // Get the CPU name
                string cpuName = obj["Name"].ToString();

                // Display the CPU name in the label
                labelCPUName.Text = cpuName;
            }
            // Get the GPU information
            ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController");
            ManagementObjectCollection collection2 = searcher2.Get();

            // Iterate through the GPU information (assuming only one GPU)
            foreach (ManagementObject obj in collection2)
            {
                // Get the GPU name
                string gpuName = obj["Name"].ToString();

                // Display the GPU name in the label
                labelGPUName.Text = gpuName;
            }
        }

        private string GetEditionFromRegistry()
        {
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string edition = registryKey.GetValue("ProductName")?.ToString();
            registryKey.Close();

            return edition ?? "Unknown";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string tempFolderPath = Path.GetTempPath();
                DirectoryInfo tempDirectory = new DirectoryInfo(tempFolderPath);

                foreach (FileInfo file in tempDirectory.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (IOException)
                    {
                        // File is in use, skip deletion
                    }
                }

                foreach (DirectoryInfo directory in tempDirectory.GetDirectories())
                {
                    try
                    {
                        directory.Delete(true);
                    }
                    catch (IOException)
                    {
                        // Directory is in use, skip deletion
                    }
                }

                MessageBox.Show("Files cleaned successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while cleaning files: {ex.Message}");
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string command = "powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61";

            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c " + command,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (Process process = new Process { StartInfo = processInfo })
            {
                process.Start();
                process.WaitForExit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                const string registryKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost";

                // Set the value to disable SmartScreen
                Registry.SetValue(registryKeyPath, "EnableWebContentEvaluation", 0, RegistryValueKind.DWord);

                MessageBox.Show("Windows SmartScreen has been disabled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                const string registryKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";

                // Set the value to disable UAC
                Registry.SetValue(registryKeyPath, "EnableLUA", 0, RegistryValueKind.DWord);

                MessageBox.Show("Windows UAC has been disabled. Please restart the system for changes to take effect.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}