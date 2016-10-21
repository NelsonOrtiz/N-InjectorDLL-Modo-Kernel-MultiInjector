using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Injector_DLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.Click += button2_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Archivo DLL|*.dll";
            open.Title = "Seleccionar archivo DLL";
            open.ShowDialog();

            listBox1.Items.AddRange(open.FileNames);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            const string sPath = "NDLLs.ini";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item);
            }

            SaveFile.Close();

            MessageBox.Show("Lista de DLL's guardado!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopulateList("some file path here");
        }

        private void PopulateList(string filePath)
        {
            var items = new List<string>();
            using (var stream = File.OpenRead(filePath))  // open file
            using (var reader = new TextReader(stream))   // read the stream with TextReader
            {
                string line;

                // read until no more lines are present
                while ((line = reader.ReadLine()) != null)
                {
                    items.Add(line);
                }
            }

            // add the ListBox items in a bulk update instead of one at a time.
            listBox.AddRange(items);
        }
    }
}
