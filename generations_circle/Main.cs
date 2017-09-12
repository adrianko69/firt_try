using System.Drawing;
using System.Windows.Forms;

namespace generations_circle
{
    public partial class Main : Form
    {
        Graphics g;
        Genetics genetics;

        public Main()
        {
            InitializeComponent();

            g = pnlGraph.CreateGraphics();

            genetics = new Genetics(g);

            genetics.ActionInfo += Genetics_ActionInfo;
        }

        void Genetics_ActionInfo(string info)
        {
            this.richTextBox1.Text += info;
            this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
            this.richTextBox1.ScrollToCaret();
            this.richTextBox1.Refresh();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            genetics.Start();

            //Task.Factory.StartNew(() => genetics.Start());
        }
    }
}
