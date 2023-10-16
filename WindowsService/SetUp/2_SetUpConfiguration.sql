SELECT * FROM dbo.DBConnectionMaster;

UPDATE dbo.DBConnectionMaster 
SET ServerName = 'KDSRV001\SAPSERVER',
	UserName = 'sa',
	Passwrd = 'ddb727bdde6c77c323ba032390f6ba46',
	DBName = 'KDPL'
WHERE DBConnId = 1;

SELECT * FROM [dbo].[AlertsServiceMaster]

UPDATE [dbo].[AlertsServiceMaster]
SET AttachmentPath = 'D:\Setup\OtherSetupsDoNotDelete\Notifications\EmailNotification\eInvoice-Triplicate-View.rpt',
	EmailTo = '<Email>,noreply@khemanigroup.com',
	CCTo = '',
	BccTo = '',
	ABody = 'This email is not monitered. Please contact on santosh@khemanigroup.com or sandip@khemanigroup.com in case of query.',
	DataSourceDef = 'SELECT TOP 1    (T0.DocEntry), T0.DocNum, T0.CardCode,    ISNULL(T1.E_Mail,''noreply@khemanigroup.com'') AS ''Email''    FROM OINV T0 LEFT JOIN OCRD T1     ON T0.CardCode = T1.CardCode    WHERE T0.U_EmailSent = ''N'' AND T0.DocType = ''I''    AND T0.DocCur = ''INR'' AND T0.CardCode NOT IN (''C0000276'',''C0000212'')',
	PostSendDataSourceDef = 'UPDATE OINV SET U_EmailSent = ''Y'' WHERE DocEntry=''[%0]'' '
WHERE ServiceId = 1;

SELECT * FROM [dbo].[AlertsSchedular]

UPDATE [dbo].[AlertsSchedular]
SET IName = 'Every 1 Minutes',
	ICode = 'E1M',
	IDesc = 'This will run the services as interval of every 1 minutes',
	FrequencyInMinutes = 1
WHERE SchedularId = 1

SELECT * FROM [dbo].[EmailConfig]

UPDATE [dbo].[EmailConfig]
SET IName = 'InvoiceEmail',
	IDesc = 'Send Invoice Email Setup',
	IHost = 'smtp.rediffmailpro.com',
	IPort = 587,
	IFrom = 'noreply@khemanigroup.com',
	IPassword = 'f6fd816c95cbbc23cac26b76ad6a0182'
WHERE EmailConfigId = 1;