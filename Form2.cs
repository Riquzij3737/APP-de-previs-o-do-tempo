using System;
using System.Windows.Forms;

namespace APDT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            string city = textBox1.Text;

            // Cria o Form1 com a cidade
            Form1 form1 = new Form1(city);
            form1.ShowDialog(); // Exibe o Form1 com a cidade
        }
    }
}
