using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace PushNotification
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string Server = ServerText.Text;
            string Username = UsernameText.Text;
            string password = PassText.Text;
            string DB = DBText.Text;
            //if (Server == "INMUM-GP-005,49172" && Username == "full" && password == "123456" && DB == "PushNotification")
            if(Server=="123456")
            {
                Rule rule = new Rule();
                rule.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Credentials");
            }


        }

        private void ServerText_TextChanged(object sender, EventArgs e)
        {
        
        }
    }
}