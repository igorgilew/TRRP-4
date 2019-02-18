using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServersForApiConfigurator
{
    public partial class Form1 : Form
    {
        Server server8888;
        Server server8889;
        Server server8890;

        public Form1()
        {
            InitializeComponent();
            server8888 = new Server(8888, this);
            server8889 = new Server(8889, this);
            server8890 = new Server(8890, this);
        }
        private void button1_Click(object sender, EventArgs e)
        {           
            Parallel.Invoke(server8888.StartServer);
            status1.Text = "Работает";
            btn1.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server8888.StopServer();
            status1.Text = "Не работает";
            btn1.Enabled = false;
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Parallel.Invoke(server8889.StartServer);
            status2.Text = "Работает";
            btn2.Enabled = true;
            button3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            server8889.StopServer();
            status2.Text = "Не работает";
            btn2.Enabled = false;
            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Parallel.Invoke(server8890.StartServer);
            status3.Text = "Работает";
            btn3.Enabled = true;
            button4.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            server8889.StopServer();
            status3.Text = "Не работает";
            btn3.Enabled = false;
            button4.Enabled = true;
        }
    }
}
