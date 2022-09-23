using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Module_04_Task_3
{
    public partial class Form1 : Form
    {
        Mutex mutex;

        public Form1()
        {
            Examination();
        }

        public void Examination()
        {
            mutex = new Mutex(true, "Module_04_Task_3");

            var procc = Process.GetProcesses().Where(n => n.ProcessName is "Module_04_Task_3");

            if (procc.Count() > 3)
            {
                Environment.Exit(3);
            }
            else
            {
                InitializeComponent();
            }
        }
    }
}
