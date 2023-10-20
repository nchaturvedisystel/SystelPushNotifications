using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.VisualBasic.ApplicationServices;

namespace PushNotification
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
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
            string DBConn = DBText.Text;
            SqlConnection DBConnection = new SqlConnection("Data Source = INMUM-GP-005,49172; User ID = full; Password = P@ssw0rd#1;Initial Catalog=PushNotification");
            //string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString; 
            //SqlConnection DBConnection = new SqlConnection(ConnectionString);
            {
                try
                {
                    DBConnection.Open();
                    SqlCommand DBCommand = new SqlCommand("SELECT COUNT(*) FROM sys.sysdatabases WHERE name=@DbName", DBConnection);
                    DBCommand.Parameters.AddWithValue("@DbName", DBConn);
                    int DatabaseCount = (int)DBCommand.ExecuteScalar();
                    if(DatabaseCount >0 && UsernameText.Text=="Systel" && PassText.Text=="123") 
                    {
                        MessageBox.Show("Database exists, you can proceed further ");
                        
                    }
                    else
                    {
                        MessageBox.Show("You've entered wrong credentials");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    DBConnection.Close();
                }
            }
            
        }

        public void ServerText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void DBText_TextChanged(object sender, EventArgs e)
        {
           // string DB = DBText.Text;
        }

        public void PassText_TextChanged(object sender, EventArgs e)
        {
            //string Password = PassText.Text;
        }

        public void UsernameText_TextChanged(object sender, EventArgs e)
        {
            //string Username = UsernameText.Text;
        }
    }
}