﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MyFirstService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotificationService
{
    public class CrystalReportServ
    {
        protected readonly Logging logging = new Logging();
        public CrystalReportServ() { }

        public void GeneratePDFReport(string reportPath, string pdfPath, Dictionary<string, dynamic> keyValuePairs, string ServerName, string DBUserName, string DBPassword, string DBName)
        {
            try
            {
                logging.LogInfo("Generating attachment from Crystal Report started");
                ReportDocument rpt = new ReportDocument();
                rpt.Load(reportPath);
                rpt.SetDatabaseLogon(DBUserName, DBPassword, ServerName, DBName);
                logging.LogInfo("NotificationService.CrystalReportServ/GeneratePDFReport, Report Credentials : ");
                logging.LogInfo($"DBServer:{ServerName};DBName:{DBName};Username:{DBUserName};Password:{DBPassword};");
                foreach (KeyValuePair<string, dynamic> keyValue in keyValuePairs)
                {
                    string inputParamName = keyValue.Key;
                    string inputParamValue = keyValue.Value;
                    rpt.SetParameterValue(0, inputParamValue);
                }
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
            }
            catch (Exception ex)
            {
                logging.LogError("NotificationService.CrystalReportServ/GeneratePDFReport : " + ex.Message);
                throw ex;
            }
        }

    }
}
