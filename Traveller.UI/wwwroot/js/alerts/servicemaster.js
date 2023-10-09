Service = new Object();

Service.ServiceId = 0;
Service.Title = "";
Service.SDesc = "";
Service.AlertType = "";
Service.HasAttachment = 0;
Service.AttachmentType = "";
Service.AttachmentPath = "";
Service.AttachmentFileType = "";
Service.OutputFileName = "";
Service.DataSourceType = "";
Service.DataSourceDef = "";
Service.PostSendDataSourceType = "";
Service.PostSendDataSourceDef = "";
Service.EmailTo = "";
Service.CCTo = "";
Service.BccTo = "";
Service.ASubject = "";
Service.ABody = "";
Service.DBConnid = 0;
Service.AlertConfigId = 0;
Service.SchedularId = 0;
Service.LastExecutedOn = new Date();
Service.NextExecutionTime = new Date();
Service.IsActive = 0;
Service.IsDeleted = 0;
Service.ActionUser = User.UserId;
Service.DBConnectionList = {};
Service.EmailConfigList = {};
Service.SchedularList = {};


//#region -- Service
Service.CreateServiceOnReady = function () {
    Service.LoadAll();

    DBConnection.LoadAll();
    EmailConfig.LoadAll();
    Schedular.LoadAll();
}

Service.LoadAll = function () {
    Service.ActionUser = User.UserId;
    Ajax.AuthPost("Service/GetService", Service, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
}
//#region -- Create Service

Service.CreateNew = function () {
    $('#ServiceModal').modal('show');
    Service.BindDBConnectionDropDown();
    Service.BindEmailConfigDropDown();
    Service.BindSchedularDropDown();
    Service.ClearServiceCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = Service.ValidateAndCreateService;
}


Service.BindDBConnectionDropDown = function () {
    var select = document.getElementById("DBConnid");
    var data = Service.DBConnectionList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="DBConnectionSelectOption_' + data[i].dBConnId + '" id="DBConnectionSelectOption_' + data[i].dBConnId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].connName + '</option>';
        select.innerHTML = select.innerHTML + optionHtml;
    }

}

Service.BindEmailConfigDropDown = function () {
    var select = document.getElementById("AlertConfigId");
    var data = Service.EmailConfigList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="EmailConfigSelectOption_' + data[i].emailConfigId + '" id="EmailConfigSelectOption_' + data[i].emailConfigId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].iName + '</option>';
        select.innerHTML = select.innerHTML + optionHtml;
    }

}

Service.BindSchedularDropDown = function () {
    var select = document.getElementById("SchedularId");
    var data = Service.SchedularList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="SchedularSelectOption_' + data[i].schedularId + '" id="SchedularSelectOption_' + data[i].schedularId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].iName + '</option>';
        select.innerHTML = select.innerHTML + optionHtml;
    }

}


Service.ClearServiceCRUDForm = function () {

    document.getElementById('Title').value = "";
    document.getElementById('ServiceDesc').value = "";
    document.getElementById('AlertType').value = "";
    document.getElementById('HasAttachment').checked = true;
    document.getElementById('ServiceDesc').value = "";
    document.getElementById('AttachmentType').value = "";
    document.getElementById('AttachmentPath').value = "";
    document.getElementById('AttachmentFileType').value = "";
    document.getElementById('OutputFileName').value = "";
    document.getElementById('DataSourceType').value = "";
    document.getElementById('DataSourceDef').value = "";
    document.getElementById('PostSendDataSourceType').value = "";
    document.getElementById('PostSendDataSourceDef').value = "";
    document.getElementById('EmailTo').value = "";
    document.getElementById('CCTo').value = "";
    document.getElementById('BccTo').value = "";
    document.getElementById('ASubject').value = "";
    document.getElementById('ABody').value = "";
    document.getElementById('DBConnid').value = "";
    document.getElementById('AlertConfigId').value = "";
    document.getElementById('SchedularId').value = "";
    document.getElementById('LastExecutedOn').value = new Date();
    document.getElementById('NextExecutionTime').value = new Date();
    document.getElementById('isUserActive').checked = true;

    Service.ServiceId = 0;
    Service.Title = "";
    Service.SDesc = "";
    Service.AlertType = "";
    Service.HasAttachment = 1;
    Service.AttachmentType = "";
    Service.AttachmentPath = "";
    Service.AttachmentFileType = "";
    Service.OutputFileName = "";
    Service.DataSourceType = "";
    Service.DataSourceDef = "";
    Service.PostSendDataSourceType = "";
    Service.PostSendDataSourceDef = "";
    Service.EmailTo = "";
    Service.CCTo = "";
    Service.BccTo = "";
    Service.ASubject = "";
    Service.ABody = "";
    Service.DBConnid = 0;
    Service.AlertConfigId = 0;
    Service.SchedularId = 0;
    Service.LastExecutedOn = new Date();
    Service.NextExecutionTime = new Date();
    Service.IsActive = 1;
    Service.IsDeleted = 0;
};

Service.ValidateAndCreateService = function () {

    var dbConnidSelect = document.getElementById("DBConnid");
    var alertConfigIdSelect = document.getElementById("AlertConfigId");
    var schedularIdSelect = document.getElementById("SchedularId");


    Service.ActionUser = User.UserId;

    Service.Title = document.getElementById('Title').value;
    Service.SDesc = document.getElementById('ServiceDesc').value ;
    Service.AlertType = document.getElementById('AlertType').value;
    Service.HasAttachment = 1;
    Service.AttachmentType = document.getElementById('AttachmentType').value;
    Service.AttachmentPath = document.getElementById('AttachmentPath').value;
    Service.AttachmentFileType = document.getElementById('AttachmentFileType').value;
    Service.OutputFileName = document.getElementById('OutputFileName').value;
    Service.DataSourceType = document.getElementById('DataSourceType').value;
    Service.DataSourceDef = document.getElementById('DataSourceDef').value;
    Service.PostSendDataSourceType = document.getElementById('PostSendDataSourceType').value;
    Service.PostSendDataSourceDef = document.getElementById('PostSendDataSourceDef').value;
    Service.EmailTo = document.getElementById('EmailTo').value;
    Service.CCTo = document.getElementById('CCTo').value;
    Service.BccTo = document.getElementById('BccTo').value;
    Service.ASubject = document.getElementById('ASubject').value;
    Service.ABody = document.getElementById('ABody').value;


    Service.DBConnid = JSON.parse(decodeURIComponent(dbConnidSelect.options[dbConnidSelect.selectedIndex].getAttribute("customData"))).dBConnid;
    Service.AlertConfigId = JSON.parse(decodeURIComponent(alertConfigIdSelect.options[alertConfigIdSelect.selectedIndex].getAttribute("customData"))).emailConfigId;
    Service.SchedularId = JSON.parse(decodeURIComponent(schedularIdSelect.options[schedularIdSelect.selectedIndex].getAttribute("customData"))).schedularId;

    //Service.DBConnid = document.getElementById('DBConnid').value;
    //Service.AlertConfigId = document.getElementById('AlertConfigId').value;
    //Service.SchedularId = document.getElementById('SchedularId').value;

    Service.LastExecutedOn = new Date(document.getElementById("LastExecutedOn").value);
    Service.LastExecutedOn = new Date(document.getElementById("NextExecutionTime").value);

    Service.IsActive = 1;


    if (Service.Title.trim() === '' || Service.SDesc.trim() === '' || Service.AlertType.trim() === '' || Service.AttachmentType.trim() === ''
        || Service.AttachmentPath.trim() === '' || Service.AttachmentFileType.trim() === '' || Service.OutputFileName.trim() === '' || Service.DataSourceType.trim() === ''
        || Service.DataSourceDef.trim() === '' || Service.PostSendDataSourceType.trim() === '' || Service.PostSendDataSourceDef.trim() === '' || Service.EmailTo.trim() === ''
        || Service.DBConnid.trim() === 0 || Service.AlertConfigId.trim() === 0 || Service.SchedularId.trim() === 0  ) {
        // Display error message
        document.getElementById('error-message').innerText = 'All are mandatory fields.';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("Service/GetService", Service, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);

    }

}

//#endregion -- Create Service

//#region -- Show Service
function ServiceCRUD_OnSuccessCallBack(data) {
    $('#ServiceModal').modal('hide');
    console.log(data);
    if (data && data.servicemasterList && data.servicemasterList.length > 0) {
        data = data.servicemasterList;
        Service.BindServiceList(data);
    }
}

function ServiceCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

Service.BindServiceList = function (data) {
    console.log(data);
    if (data && data.length > 0) {
        var body = document.getElementById("TemplateListBody");
        body.innerHTML = "";
        let SrNo = 0;
        for (var i = 0; i < data.length; i++) {
            SrNo += 1;
            var RowHtml = ('<tr>'
                + '                <td class="dtr-control sorting_1" style="border-left: 5px solid #' + Util.WCColors[i] + ';">' + SrNo + '</td>'
                + '                <td>' + data[i].title + '</td>'
                + '                <td>' + data[i].alertType + '</td>'
                + '                <td>' + data[i].attachmentType + '</td>'
                + '                <td>'
                + '                     <input type="checkbox" id="ServiceIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="Service.ServiceStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="Service.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="Service.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="far fa-trash-alt"></i> Delete'
                + '                             </button>'
                + '                         </div>'
                + '                    </div>'
                + '               </td> '
                + '            </tr>'
                + '');


            body.innerHTML = body.innerHTML + RowHtml;
        }
    }
    else {
        var body = document.getElementById("TemplateListBody");
        body.innerHTML = ('<tr>'
            + '<td  colspan = "7">'
            + ' <font style="color:red;">No Records found..</font>'
            + '        </td>'
            + '    </tr>');
    }
    Service.ClearServiceCRUDForm();
}
//#endregion -- Show User Role



Service.ServiceStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.serviceId = data.serviceId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("Service/UpdateStatusService", dbConnData, UpdateService_OnSuccesscallback, UpdateService_OnErrorCallBack);

}
function UpdateService_OnSuccesscallback(response) {
    if (response.isActive === 1) {

        Toast.create("Success", "Service Active", TOAST_STATUS.SUCCESS, 1500);
    } else {
        Toast.create("Success", "Service Inactive", TOAST_STATUS.WARNING, 1500);
    }
}

function UpdateService_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User Service
Service.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.iName + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteService);
}
function DeleteService(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("Service/GetService", dbconn, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
}

//#endregion -- Delete User Service

//#region -- Update User Service
Service.SetServiceCRUDForm = function (dbconn) {



    document.getElementById('ServiceId').value = dbconn.serviceId;
    document.getElementById('Title').value = dbconn.title;
    document.getElementById('ServiceDesc').value = dbconn.serviceDesc;
    document.getElementById('AlertType').value = dbconn.alertType;
    document.getElementById('HasAttachment').checked = dbconn.hasAttachment;
    document.getElementById('ServiceDesc').value = dbconn.serviceDesc;
    document.getElementById('AttachmentType').value = dbconn.attachmentType;
    document.getElementById('AttachmentPath').value = dbconn.attachmentPath;
    document.getElementById('AttachmentFileType').value = dbconn.attachmentFileType;
    document.getElementById('OutputFileName').value = dbconn.outputFileName;
    document.getElementById('DataSourceType').value = dbconn.dataSourceType;
    document.getElementById('DataSourceDef').value = dbconn.dataSourceDef;
    document.getElementById('PostSendDataSourceType').value = dbconn.postSendDataSourceType;
    document.getElementById('PostSendDataSourceDef').value = dbconn.postSendDataSourceDef;
    document.getElementById('EmailTo').value = dbconn.emailTo;
    document.getElementById('CCTo').value = dbconn.cCTo;
    document.getElementById('BccTo').value = dbconn.bccTo;
    document.getElementById('ASubject').value = dbconn.aSubject;
    document.getElementById('ABody').value = dbconn.aBody;

    document.getElementById('DBConnid').value = ("DBConnectionSelectOption_" + data.dBConnid);
    document.getElementById('AlertConfigId').value = ("EmailConfigSelectOption_" + data.emailConfigId);
    document.getElementById('SchedularId').value = ("SchedularSelectOption_" + data.schedularId);

    //document.getElementById('DBConnid').value = dbconn.dBConnid;
    //document.getElementById('AlertConfigId').value = dbconn.alertConfigId;
    //document.getElementById('SchedularId').value = dbconn.schedularId;

    document.getElementById('LastExecutedOn').value = dbconn.lastExecutedOn;
    document.getElementById('NextExecutionTime').value = dbconn.nextExecutionTime;
    document.getElementById('isUserActive').checked = dbconn.isActive === 1;

};
Service.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#ServiceModal').modal('show');
    Service.BindDBConnectionDropDown();
    Service.BindEmailConfigDropDown();
    Service.BindSchedularDropDown();
    Service.SetServiceCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = Service.ValidateAndUpdateService;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

Service.ValidateAndUpdateService = function (dbconn) {

    var dbConnidSelect = document.getElementById("DBConnid");
    var alertConfigIdSelect = document.getElementById("AlertConfigId");
    var schedularIdSelect = document.getElementById("SchedularId");


    dbconn.ActionUser = User.UserId;

    dbconn.title = document.getElementById('Title').value ;
    dbconn.serviceDesc = document.getElementById('ServiceDesc').value;
    dbconn.alertType = document.getElementById('AlertType').value;
    dbconn.hasAttachment = document.getElementById('HasAttachment').checked ? true : false;
    dbconn.serviceDesc = document.getElementById('ServiceDesc').value;
    dbconn.attachmentType = document.getElementById('AttachmentType').value;
    dbconn.attachmentPath = document.getElementById('AttachmentPath').value;
    dbconn.attachmentFileType = document.getElementById('AttachmentFileType').value;
    dbconn.outputFileName = document.getElementById('OutputFileName').value;
    dbconn.dataSourceType = document.getElementById('DataSourceType').value;
    dbconn.dataSourceDef = document.getElementById('DataSourceDef').value;
    dbconn.postSendDataSourceType = document.getElementById('PostSendDataSourceType').value;
    dbconn.postSendDataSourceDef = document.getElementById('PostSendDataSourceDef').value;
    dbconn.emailTo = document.getElementById('EmailTo').value;
    dbconn.cCTo = document.getElementById('CCTo').value;
    dbconn.bccTo = document.getElementById('BccTo').value;
    dbconn.aSubject = document.getElementById('ASubject').value;
    dbconn.aBody = document.getElementById('ABody').value;

    dbconn.UserGroupId = JSON.parse(decodeURIComponent(dbConnidSelect.options[dbConnidSelect.selectedIndex].getAttribute("customData"))).dBConnid;
    dbconn.UserGroupId = JSON.parse(decodeURIComponent(alertConfigIdSelect.options[alertConfigIdSelect.selectedIndex].getAttribute("customData"))).emailConfigId;
    dbconn.UserGroupId = JSON.parse(decodeURIComponent(schedularIdSelect.options[schedularIdSelect.selectedIndex].getAttribute("customData"))).schedularId;

    //dbconn.dBConnid = document.getElementById('DBConnid').value;
    //dbconn.alertConfigId  = document.getElementById('AlertConfigId').value;
    //dbconn.schedularId  = document.getElementById('SchedularId').value;

    dbconn.lastExecutedOn = document.getElementById('LastExecutedOn').value;
    dbconn.nextExecutionTime = document.getElementById('NextExecutionTime').value;
    dbconn.serviceId = document.getElementById('ServiceId').value;
    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;
    Ajax.AuthPost("Service/GetService", dbconn, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
}
//#endregion -- Update User Service

//#endregion -- Service





