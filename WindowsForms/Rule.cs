using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PushNotification.Model;
using PushNotification.Service;
namespace PushNotification
{
    public partial class Rule : Form
    {

        public Rule()
        {

            InitializeComponent();
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

            // Create an instance of the data service and insert the data into the database

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
            ServiceSchedularDTO schedularDTO = new ServiceSchedularDTO
            {
                SchedularName = SchedulerName.Text.ToString(),
                SchedularCode = SchedularCodeTxt.Text.ToString(),
                SchedularDesc = SchedularDescText.Text.ToString(),
                FrequencyInMins = FrequencyInMinutes.Text.ToString(),
               
            };
        }
        //This is a comment
        private void SConfigType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SConfigType.DataSource = Enum.GetValues(typeof(SchedularType));
            SConfigType.Items.Add("Recurring");
            SConfigType.Items.Add("Single Time");
        }
    }
}
