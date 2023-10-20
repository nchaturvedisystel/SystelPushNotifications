using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushNotification.Interface;
using PushNotification.Model;
using System.Data.SqlClient;
using System.Data;

namespace PushNotification.Service
{
    public class SchedularService : IServiceSchedular
    {
        public void ServiceSchedularConfig(ServiceSchedularDTO ServiceSchedular)
        {
            string ConnectionService = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionService);
            {
                connection.Open();
                SqlCommand SPCommand = new SqlCommand("ServiceMaster_CRUD", connection);
                SPCommand.CommandType = CommandType.StoredProcedure;
                {
                    SPCommand.Parameters.AddWithValue("@IName", ServiceSchedular.SchedularName);
                    SPCommand.Parameters.AddWithValue("@ICode", ServiceSchedular.SchedularCode);
                    SPCommand.Parameters.AddWithValue("@IDesc", ServiceSchedular.SchedularDesc);
                    SPCommand.Parameters.AddWithValue("@DataFetchType", ServiceSchedular.SchedularType);
                    SPCommand.Parameters.AddWithValue("@DataFetchProcess", ServiceSchedular.FrequencyInMins);
                    SPCommand.Parameters.AddWithValue("@IsActive", 0);
                    SPCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    SPCommand.Parameters.AddWithValue("@ActionUser", 0);
                    SPCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Data Saved");
            }
        }
    }
}
