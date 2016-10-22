using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Nini.Config;
using System.Security.Principal;
using System.Reflection;      //For Assembly
using System.IO;
using DllInject;
using N_InjectorDLL;

namespace N_InjectorDLL
{
    public partial class Form1 : Form
    {
        public List<String> fullFileName;
        public string Projectname = "N-Injector DLL v1.0";
        public Form1()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void LoadProcesses() // cargar procesos en el combobox
        {
            comboBox1.Items.Clear();
            Process[] MyProcess = Process.GetProcesses();
            for (int i = 0; i < MyProcess.Length; i++)
                comboBox1.Items.Add(string.Format("{0} - {1}", MyProcess[i].ProcessName, MyProcess[i].Id));
        }

        private void button1_Click(object sender, EventArgs e) // cargar dll
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Archivo DLL|*.dll";
            open.Title = "Selecionar el archivo DLL";
            open.ShowDialog();

            listBox1.Items.AddRange(open.FileNames);
        }

        private void button2_Click(object sender, EventArgs e) // cargar lista dll
        {
            this.listBox1.Items.Clear();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivo txt (*.ini)|*.ini";
            try
            {
                open.ShowDialog();
                StreamReader import = new StreamReader(Convert.ToString(open.FileName));
                while (import.Peek() >= 0)
                {
                    listBox1.Items.Add(Convert.ToString(import.ReadLine()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e) // guardar lista dll
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "NConfig.ini";
            save.Filter = "Config | *.ini";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    writer.WriteLine(listBox1.Items[i].ToString());
                }
                writer.Dispose();
                writer.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e) // eliminar dll
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
            }
        }
    }
}
