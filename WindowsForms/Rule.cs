using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using PushNotification.Interface;
using PushNotification.Model;
using PushNotification.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PushNotification
{
    public partial class Rule : Form
    {
        private int recordId;
        private string SelectedFilePath;
        private string SelectedFileName;

        public Rule(int selectedId = 0)
        {

            InitializeComponent();
            recordId = selectedId;
            PopulateFormData();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void RuleReport_Click(object sender, EventArgs e)
        {

        }


        public void label3_Click(object sender, EventArgs e)
        {

        }

        public void label1_Click(object sender, EventArgs e)
        {

        }


        public void label1_Click_1(object sender, EventArgs e)
        {

        }

        public void RuleSetDSCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {

        }

        public void label1_Click_2(object sender, EventArgs e)
        {

        }

        public void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        public void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "hh:mm:ss MMMM dd, yyyy";
        }

        public void DBConfigSave_Click(object sender, EventArgs e)
        {

        }

        public void EmailConfSave_Click(object sender, EventArgs e)
        {

        }

        public void EmailConfig_Click(object sender, EventArgs e)
        {
        }

        public void EmailConfClose_Click(object sender, EventArgs e)
        {
        }

        public void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        public void button1_Click_1(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
        }

        public void SConfigClose_Click(object sender, EventArgs e)
        {

        }

        public void folderBrowserDialog3_HelpRequest(object sender, EventArgs e)
        {

        }

        public void ASMBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        public void button3_Click(object sender, EventArgs e)
        {

        }

        public void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "hh:mm:ss MMMM dd, yyyy";
        }

        public void ServiceScheduler_Click(object sender, EventArgs e)
        {

        }

        public void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "hh:mm:ss MMMM dd, yyyy";
        }

        public void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker5.Format = DateTimePickerFormat.Custom;
            dateTimePicker5.CustomFormat = "hh:mm:ss MMMM dd, yyyy";
        }

        public void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker6.Format = DateTimePickerFormat.Custom;
            dateTimePicker6.CustomFormat = "hh:mm:ss MMMM dd, yyyy";
        }

        public void DBConfig_Click(object sender, EventArgs e)
        {

        }

        public void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        public void Rule_Load(object sender, EventArgs e)
        {

        }


        public void EmailConfirm_Click(object sender, EventArgs e)
        {
            EmailConfigDTO emailConfig = new EmailConfigDTO
            {
                ConnectionName = TitleText.Text.ToString(),
                Description = DescText.Text.ToString(),
                Host = HostConf.Text.ToString(),
                Port = PortConfig.Text.ToString(),
                From = FromConfig.Text.ToString(),
                Password = PassConfig.Text.ToString(),
            };
            EmailConfigService emailConfigService = new EmailConfigService();
            emailConfigService.InsertEmailConfig(emailConfig);

        }

        private void PortConfig_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmailFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassConfig_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                ServiceSchedularDTO schedularDTO = new ServiceSchedularDTO
                {
                    SchedularName = SchedulerName.Text,
                    SchedularCode = SchedularCodeTxt.Text,
                    SchedularDesc = SchedularDescText.Text,
                    SchedularType = SConfigType.Text,
                    FrequencyInMins = Convert.ToInt32(SchedularFreq.Text),
                };

                SchedularService schedularService = new SchedularService();
                schedularService.CreateSchedular(schedularDTO);

                MessageBox.Show("Data Saved Successfully");
            }
            catch (Exception ex)
            {
                // Handle and log the exception, or show an error message
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AlertServiceMaster_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();

                // Check for duplicate title
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM AlertsServiceMaster WHERE Title = @Title AND ServiceId != @ServiceId", connection))
                {
                    checkCommand.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 100)).Value = ASMTitle.Text;
                    checkCommand.Parameters.Add(new SqlParameter("@ServiceId", SqlDbType.Int)).Value = recordId;

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("A record with the same title already exists. Please choose a different title.");
                        return;
                    }
                }

                using (SqlCommand command = new SqlCommand("AlertsServiceMaster_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 100)).Value = ASMTitle.Text;
                    command.Parameters.AddWithValue("@SDesc", "This is New Email");
                    command.Parameters.AddWithValue("@HasAttachment", 1);
                    command.Parameters.AddWithValue("@DBConnid", 1);
                    command.Parameters.AddWithValue("@AlertConfigId", 1);
                    command.Parameters.AddWithValue("@SchedularId", 1);
                    command.Parameters.AddWithValue("@LastExecutedOn", 1);
                    command.Parameters.AddWithValue("@NextExecutionTime", 1);
                    command.Parameters.AddWithValue("@IsActive", 1);
                    command.Parameters.AddWithValue("@IsDeleted", 0);
                    command.Parameters.AddWithValue("@ActionUser", 1);
                    command.Parameters.Add(new SqlParameter("@AlertType", SqlDbType.VarChar, 100)).Value = ASMType.Text;
                    command.Parameters.Add(new SqlParameter("@AttachmentType", SqlDbType.VarChar, 100)).Value = ASMAttachment.Text;
                    command.Parameters.Add(new SqlParameter("@AttachmentPath", SqlDbType.VarChar, 100)).Value = SelectedFilePath;
                    command.Parameters.Add(new SqlParameter("@AttachmentFileType", SqlDbType.VarChar, 100)).Value = ASMFileType.Text;
                    command.Parameters.Add(new SqlParameter("@DataSourceType", SqlDbType.VarChar, 100)).Value = ASMOutsource.Text;
                    command.Parameters.Add(new SqlParameter("@PostSendDataSourceType", SqlDbType.VarChar, 100)).Value = ASMPostDataSrv.Text;
                    command.Parameters.Add(new SqlParameter("@EmailTo", SqlDbType.VarChar, 100)).Value = ASMEmail.Text;
                    command.Parameters.Add(new SqlParameter("@CCTo", SqlDbType.VarChar, 100)).Value = ASMCC.Text;
                    command.Parameters.Add(new SqlParameter("@BccTo", SqlDbType.VarChar, 100)).Value = ASMBcc.Text;
                    command.Parameters.Add(new SqlParameter("@ASubject", SqlDbType.VarChar, 100)).Value = ASMSubject.Text;
                    command.Parameters.Add(new SqlParameter("@ABody", SqlDbType.VarChar, 100)).Value = ASMBody.Text;
                    command.Parameters.Add(new SqlParameter("@OutputFileName", SqlDbType.VarChar, 100)).Value = ASMOutputFileName.Text;
                    command.Parameters.Add(new SqlParameter("@PostSendDataSourceDef", SqlDbType.VarChar, 100)).Value = ASMPostSrcDef.Text;
                    command.Parameters.Add(new SqlParameter("@DataSourceDef", SqlDbType.VarChar, 100)).Value = ASMDatasourceDef.Text;

                    if (recordId > 0)
                    {
                        // Update the existing record
                        command.Parameters.AddWithValue("@ServiceId", recordId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data Updated");
                    }
                    else
                    {
                        // This is an insert operation
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data Saved");
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void ASMBrowse_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SelectedFilePath = openFileDialog.FileName;
                    SelectedFileName = Path.GetFileName(SelectedFilePath);
                    ASMBrowse.Text = SelectedFileName;
                }
            }
        }
        private void FillFormFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM DBConnectionMaster WHERE DBConnId = @DBConnId", connection))
                {
                    command.Parameters.Add(new SqlParameter("@DBConnId", SqlDbType.Int)).Value = 1;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ConName.Text = reader["ConnName"].ToString();
                        ServerName.Text = reader["ServerName"].ToString();
                        UserName.Text = reader["UserName"].ToString();
                        Passwrd.Text = reader["Passwrd"].ToString();
                        DBName.Text = reader["DBName"].ToString();

                        // Set other controls as needed
                    }
                }
            }
        }
        private void DBConfigTab_Load(object sender, EventArgs e)
        {
            FillFormFromDatabase();
        }
        private void PopulateServiceVariablesData()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT VarInstance, VarValue, VarType FROM AlertsServiceVariables WHERE VariableId = @VariableId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@VariableId", SqlDbType.Int)).Value = 1;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the form's controls with data from the database
                            VarInstanceTextBox.Text = reader["VarInstance"].ToString();
                            VarValueTextBox.Text = reader["VarValue"].ToString();
                            VarTypeTextBox.Text = reader["VarType"].ToString();

                            reader.Close();
                        }
                    }
                }
            }
        }
        private void PopulateFormData()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT Title, AlertType, AttachmentType, AttachmentPath, AttachmentFileType, DataSourceType, PostSendDataSourceType, EmailTo, CCTo, BccTo, ASubject, ABody, OutputFileName, PostSendDataSourceDef, DataSourceDef FROM AlertsServiceMaster WHERE ServiceId = @RecordID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@RecordID", SqlDbType.Int)).Value = recordId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the form's controls with data from the database
                            ASMTitle.Text = reader["Title"].ToString();
                            ASMType.Text = reader["AlertType"].ToString();
                            ASMAttachment.Text = reader["AttachmentType"].ToString();
                            ASMFileType.Text = reader["AttachmentFileType"].ToString();
                            ASMOutsource.Text = reader["DataSourceType"].ToString();
                            ASMPostDataSrv.Text = reader["PostSendDataSourceType"].ToString();
                            ASMEmail.Text = reader["EmailTo"].ToString();
                            ASMCC.Text = reader["CCTo"].ToString();
                            ASMBcc.Text = reader["BccTo"].ToString();
                            ASMSubject.Text = reader["ASubject"].ToString();
                            ASMBody.Text = reader["ABody"].ToString();
                            ASMOutputFileName.Text = reader["OutputFileName"].ToString();
                            ASMPostSrcDef.Text = reader["PostSendDataSourceDef"].ToString();
                            ASMDatasourceDef.Text = reader["DataSourceDef"].ToString();
                            reader.Close();
                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();

                // Check for duplicate Connection Name
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM DBConnectionMaster WHERE ConnName = @ConnName AND DBConnId != @DBConnId", connection))
                {
                    checkCommand.Parameters.Add(new SqlParameter("@ConnName", SqlDbType.VarChar, 100)).Value = ConName.Text;
                    checkCommand.Parameters.Add(new SqlParameter("@DBConnId", SqlDbType.Int)).Value = recordId;

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("A record with the same Connection Name already exists. Please choose a different Connection Name.");
                        return;
                    }
                }

                using (SqlCommand command = new SqlCommand("DBConnectionMaster_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@DBConnId", SqlDbType.Int)).Value = recordId;
                    command.Parameters.Add(new SqlParameter("@ConnName", SqlDbType.VarChar, 100)).Value = ConName.Text;
                    command.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar, 200)).Value = ServerName.Text;
                    command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 100)).Value = UserName.Text;
                    command.Parameters.Add(new SqlParameter("@Passwrd", SqlDbType.VarChar, 200)).Value = Passwrd.Text;
                    command.Parameters.Add(new SqlParameter("@DBName", SqlDbType.VarChar, 100)).Value = DBName.Text;
                    command.Parameters.AddWithValue("@IsActive", 1); 
                    command.Parameters.AddWithValue("@IsDeleted", 0); 
                    command.Parameters.Add(new SqlParameter("@ActionUser", SqlDbType.Int)).Value = 1; 

                    command.ExecuteNonQuery();
                    MessageBox.Show(recordId > 0 ? "Data Updated" : "Data Saved");
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM AlertsServiceVariables WHERE VarInstance = @VarInstance AND ServiceId = @ServiceId", connection))
                {
                    checkCommand.Parameters.Add(new SqlParameter("@VarInstance", SqlDbType.VarChar, 100)).Value = VarInstanceTextBox.Text;
                    checkCommand.Parameters.Add(new SqlParameter("@ServiceId", SqlDbType.Int)).Value = 1; // Provide the correct ServiceId

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("A record with the same VarInstance already exists within this service. Please choose a different VarInstance.");
                        return;
                    }
                }

                using (SqlCommand command = new SqlCommand("AlertsServiceVariables_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@VariableId", SqlDbType.Int)).Value = 0; // Set this based on your requirements
                    command.Parameters.Add(new SqlParameter("@ServiceId", SqlDbType.Int)).Value = 2; // Provide the correct ServiceId
                    command.Parameters.Add(new SqlParameter("@VarInstance", SqlDbType.VarChar, 100)).Value = VarInstanceTextBox.Text;
                    command.Parameters.Add(new SqlParameter("@VarValue", SqlDbType.VarChar, 100)).Value = VarValueTextBox.Text;
                    command.Parameters.Add(new SqlParameter("@VarType", SqlDbType.VarChar, 50)).Value = VarTypeTextBox.Text;
                    command.Parameters.AddWithValue("@IsActive", 1); // You may need to adjust this based on your requirements
                    command.Parameters.AddWithValue("@IsDeleted", 0); // You may need to adjust this based on your requirements
                    command.Parameters.Add(new SqlParameter("@ActionUser", SqlDbType.Int)).Value = 1; // You may need to adjust this based on your requirements

                    command.ExecuteNonQuery();
                    MessageBox.Show(5 > 0 ? "Data Updated" : "Data Saved");
                }
            }
        }
        }
    }