using PushNotification.Interface;
using PushNotification.Model;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Configuration;

public class EmailConfigService : IEmailConfig
{
    public void InsertEmailConfig(EmailConfigDTO emailConfig)
    {
        string ConnectionService = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(ConnectionService);
        { 
            connection.Open();
            SqlCommand command=new SqlCommand("EmailConfig_CRUD", connection);
            command.CommandType = CommandType.StoredProcedure;
            {
                
                // Set parameters for the stored procedure
                command.Parameters.AddWithValue("@IName", emailConfig.ConnectionName);
                command.Parameters.AddWithValue("@IDesc", emailConfig.Description);
                command.Parameters.AddWithValue("@IHost", emailConfig.Host);
                command.Parameters.AddWithValue("@IPort", emailConfig.Port);
                command.Parameters.AddWithValue("@IFrom", emailConfig.From);
                command.Parameters.AddWithValue("@IPassword", emailConfig.Password);
                command.Parameters.AddWithValue("@IEnableSsl", 0); 
                command.Parameters.AddWithValue("@IsBodyHtml", 0);
                command.Parameters.AddWithValue("@IsActive", 0);
                command.Parameters.AddWithValue("@IsDeleted", 0);
                command.Parameters.AddWithValue("@ActionUser", 0);
                // Add other parameters as needed

                command.ExecuteNonQuery();
            }
            MessageBox.Show("Data Saved");
        }
    }
}
