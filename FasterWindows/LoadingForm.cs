using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FasterWindows
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            Shown += LoadingForm_Shown;
        }

        private void LoadingForm_Shown(object sender, EventArgs e)
        {
            LoadMainForm();
        }

        private void LoadMainForm()
        {
            // Perform any necessary initialization or time-consuming operations here.
            // This can include loading data, setting up controls, or any other tasks.

            // Once the loading process is complete, close the loading screen form
            // and show the main form.
            Hide(); // Hide the loading screen form
            var mainForm = new Form1();
            mainForm.Show(); // Show the main form
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }
    }
}