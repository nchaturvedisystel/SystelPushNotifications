using Systel.Notification.Common;
using System.Net.Mail;
using System.Net;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using Systel.Notification.Model;
using Systel.Notification.Interface;
using Systel.Notification.Service;

namespace Systel.Notification.BAL
{
    public class PushNotification
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private EmailConfigurationList _emailConfig;

        private readonly ILogger<PushNotification> _logger;
        private readonly WorkerOptions options;
        private readonly IPushNotification pushNotification;
        private readonly IEmailConfiguration emailConfiguration;
        public PushNotification(ILogger<PushNotification> logger, WorkerOptions options, IPushNotification pushNotification, IEmailConfiguration emailConfiguration)
        {
            _logger = logger;
            this.options = options;
            this.pushNotification = pushNotification;
            this.emailConfiguration = emailConfiguration;
        }


        public void ProcessNotification()
        {

            //EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
            //string EncPwd = encryptDecryptService.EncryptValue("wpuk oulb fbck cowl");

            //Get configuration details to send respective type of notification

            GetConfiguration();

            //Get Notification List
            PushNotificationList pushNotificationList = new PushNotificationList();
            pushNotificationList = GetNotificationList(pushNotificationList);

            //Send Notifications
            pushNotificationList = SendNotification(pushNotificationList);

            //Update Notification Status
            UpdatePushNotifications(pushNotificationList);

        }
        public void GetConfiguration()
        {


            EncryptDecryptService encryptDecryptService = new EncryptDecryptService();

            //Get Email Configuration 
            //IEmailConfiguration emailConfiguration = new EmailConfigurationService(,options);
            _emailConfig = emailConfiguration.GetEmailConfigDetails();

            foreach (EmailConfigurationDTO emailConfigurationDTO in _emailConfig.EmailConfigList)
            {
                emailConfigurationDTO.IPassword = encryptDecryptService.DecryptValue(emailConfigurationDTO.IPassword);
            }

        }
        public PushNotificationList GetNotificationList(PushNotificationList pushNotificationList)
        {
            //IPushNotification pushNotification = new PushNotificationService();
            pushNotificationList = pushNotification.GetPushNotifications();
            return pushNotificationList;
        }
        public PushNotificationList SendNotification(PushNotificationList pushNotificationList)
        {
            EmailConfigurationDTO emailConfigurationDTO = new EmailConfigurationDTO();
            emailConfigurationDTO = _emailConfig.EmailConfigList.FirstOrDefault();

            foreach (PushNotificationDTO item in pushNotificationList.NotificationList)
            {
                SendEmail(item, emailConfigurationDTO);

            }
            return pushNotificationList;
        }
        public PushNotificationDTO SendEmail(PushNotificationDTO pushNotificationDTO, EmailConfigurationDTO emailConfigurationDTO)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfigurationDTO.IFrom);
            mailMessage.To.Add(pushNotificationDTO.NTo);
            mailMessage.Subject = pushNotificationDTO.NSubject;
            mailMessage.Body = pushNotificationDTO.NContent;

            if (File.Exists(pushNotificationDTO.AttachmentPath))
            {
                mailMessage.Attachments.Add(new Attachment(pushNotificationDTO.AttachmentPath));
                //Send Multiple files
                //string[] FileName = new string[] {
                //pushNotificationDTO.AttachmentPath, pushNotificationDTO.AttachmentPath };
                //foreach (string File in FileName)
                //{
                //    Attachment atch = new Attachment(File);
                //    mailMessage.Attachments.Add(atch);
                //}
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = emailConfigurationDTO.IHost;
            smtpClient.Port = Convert.ToInt32(emailConfigurationDTO.IPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailConfigurationDTO.IFrom, emailConfigurationDTO.IPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                pushNotificationDTO.NStatus = "Success";

            }
            catch (Exception ex)
            {
                pushNotificationDTO.NStatus = "Failed";
                pushNotificationDTO.Remarks = ex.Message;
            }

            return pushNotificationDTO;
        }
        public void UpdatePushNotifications(PushNotificationList pushNotificationList)
        {
            DataTable typNotificationMaster = new DataTable();
            typNotificationMaster.Clear();
            typNotificationMaster.Columns.Add("NotificationId");
            typNotificationMaster.Columns.Add("NStatus");
            typNotificationMaster.Columns.Add("Remarks");

            foreach (PushNotificationDTO pushNotificationDTO in pushNotificationList.NotificationList)
            {
                DataRow newRow = typNotificationMaster.NewRow();
                newRow["NotificationId"] = pushNotificationDTO.NotificationId;
                newRow["NStatus"] = pushNotificationDTO.NStatus;
                newRow["Remarks"] = pushNotificationDTO.Remarks;
                typNotificationMaster.Rows.Add(newRow);
            }

            //IPushNotification pushNotification = new PushNotificationService();
            pushNotification.UpdatePushNotifications(typNotificationMaster);
        }

        public void ConvertRPTtoPDF()
        {
            ReportDocument cryRpt = new ReportDocument();

            //cryRpt.Load("D:/Anuprabha/Logs/eInvoice-Triplicate-View.rpt");

            //CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();
            //crystalReportViewer1.ReportSource = cryRpt;

            //crystalReportViewer1.EnableRefresh=true;

            //cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "D:/ASD.pdf");
        }
    }
}
