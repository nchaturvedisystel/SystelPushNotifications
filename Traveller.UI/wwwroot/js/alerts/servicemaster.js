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
Service.DBConnId = 0;
Service.AlertConfigId = 0;
Service.SchedularId = 0;
Service.LastExecutedOn =  new Date();
Service.NextExecutionTime = new Date();
Service.IsActive = 0;
Service.IsDeleted = 0;
Service.ActionUser = User.UserId;
Service.DBConnectionList = {};
Service.EmailConfigList = {};
Service.SchedularList = {};

const regex_pattern_Service = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

//#region -- Service
Service.CreateServiceOnReady = function () {
    Service.LoadAll();

    DBConnection.LoadAll();
    EmailConfig.LoadAll();
    Schedular.LoadAll();

    //ClassicEditor
    //    .create(document.querySelector('#editor'))
    //    .catch(error => {
    //        console.error(error);
    //    });


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
    var select = document.getElementById("DBConnId");
    var data = Service.DBConnectionList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="DBConnectionSelectOption_' + data[i].dbConnId + '" id="DBConnectionSelectOption_' + data[i].dbConnId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].connName + '</option>';
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
    //document.getElementById('DBConnid').value = "";
    //document.getElementById('AlertConfigId').value = "";
    //document.getElementById('SchedularId').value = "";
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
    //Service.DBConnid = 0;
    //Service.AlertConfigId = 0;
    //Service.SchedularId = 0;
    Service.LastExecutedOn = new Date();
    Service.NextExecutionTime = new Date();
    Service.IsActive = 1;
    Service.IsDeleted = 0;
};

Service.ValidateAndCreateService = function () {


    var NewService = {};


    var dbConnidSelect = document.getElementById("DBConnId");
    var alertConfigIdSelect = document.getElementById("AlertConfigId");
    var schedularIdSelect = document.getElementById("SchedularId");

    NewService.ActionUser = User.UserId;
    NewService.Title = document.getElementById('Title').value;
    NewService.SDesc = document.getElementById('ServiceDesc').value ;
    NewService.AlertType = document.getElementById('AlertType').value;
    NewService.HasAttachment = 1;
    NewService.AttachmentType = document.getElementById('AttachmentType').value;
    NewService.AttachmentPath = document.getElementById('AttachmentPath').value;
    NewService.AttachmentFileType = document.getElementById('AttachmentFileType').value;
    NewService.OutputFileName = document.getElementById('OutputFileName').value;
    NewService.DataSourceType = document.getElementById('DataSourceType').value;
    NewService.DataSourceDef = document.getElementById('DataSourceDef').value;
    NewService.PostSendDataSourceType = document.getElementById('PostSendDataSourceType').value;
    NewService.PostSendDataSourceDef = document.getElementById('PostSendDataSourceDef').value;
    NewService.EmailTo = document.getElementById('EmailTo').value;
    NewService.CCTo = document.getElementById('CCTo').value;
    NewService.BccTo = document.getElementById('BccTo').value;
    NewService.ASubject = document.getElementById('ASubject').value;
    NewService.ABody = document.getElementById('ABody').value;

    NewService.DBConnId = JSON.parse(decodeURIComponent(dbConnidSelect.options[dbConnidSelect.selectedIndex].getAttribute("customData"))).dbConnId;
    NewService.AlertConfigId = JSON.parse(decodeURIComponent(alertConfigIdSelect.options[alertConfigIdSelect.selectedIndex].getAttribute("customData"))).emailConfigId;
    NewService.SchedularId = JSON.parse(decodeURIComponent(schedularIdSelect.options[schedularIdSelect.selectedIndex].getAttribute("customData"))).schedularId;

    //Service.DBConnid = document.getElementById('DBConnid').value;
    //Service.AlertConfigId = document.getElementById('AlertConfigId').value;
    //Service.SchedularId = document.getElementById('SchedularId').value;

    //Service.LastExecutedOn = new Date(document.getElementById("LastExecutedOn").value);
    //Service.NextExecutionTime = new Date(document.getElementById("NextExecutionTime").value);

    NewService.LastExecutedOn = document.getElementById("LastExecutedOn").value;
    NewService.NextExecutionTime = document.getElementById("NextExecutionTime").value;

    NewService.IsActive = 1;


    // Perform validation
    //var ValidationMsg = " Please provide ";
    //ValidationMsg += (NewService.Title.trim() === '') ? " Title," : '';
    //ValidationMsg += (NewService.SDesc.trim() === '') ? " Desc," : '';
    //ValidationMsg += (NewService.AlertType.trim() === '') ? " AlertType," : '';
    //ValidationMsg += (NewService.AttachmentType.trim() === '') ? " AttachmentType," : '';
    //ValidationMsg += (NewService.AttachmentPath.trim() === '') ? " AttachmentPath," : '';
    //ValidationMsg += (NewService.AttachmentFileType.trim() === '') ? " AttachmentFileType," : '';
    //ValidationMsg += (NewService.OutputFileName.trim() === '') ? " OutputFileName," : '';
    //ValidationMsg += (NewService.DataSourceType.trim() === '') ? " DataSourceType," : '';
    //ValidationMsg += (NewService.DataSourceDef.trim() === '') ? " DataSourceDef," : '';
    //ValidationMsg += (NewService.PostSendDataSourceType.trim() === '') ? " PostSendDataSourceType," : '';
    //ValidationMsg += (NewService.PostSendDataSourceDef.trim() === '') ? " PostSendDataSourceDef," : '';
    //ValidationMsg += (NewService.EmailTo.trim() === '') ? " EmailTo," : '';
    //ValidationMsg += (NewService.CCTo.trim() === '') ? " CCTo," : '';
    //ValidationMsg += (NewService.ASubject.trim() === '') ? " Subject," : '';
    //ValidationMsg += (NewService.ABody.trim() === '') ? " Body," : '';
    //ValidationMsg += (NewService.LastExecutedOn.trim() === '') ? " LastExecutedOn," : '';
    //ValidationMsg += (NewService.NextExecutionTime.trim() === '') ? " NextExecutionTime," : '';

    //if (ValidationMsg.trim() != "Please provide") {
    //    alert(ValidationMsg);
    //}
    //else {
    //    Ajax.AuthPost("Service/GetService", NewService, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
    //}

    //
    if (NewService.Title.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Service Name !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.SDesc.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Service Description !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.AlertType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Alert Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.AttachmentType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Attachment Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.AttachmentPath.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Attachment Path !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.AttachmentFileType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Attachment FileType !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.OutputFileName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Output FileName !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.DataSourceType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide DataSource Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.DataSourceDef.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide DataSource Def !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.PostSendDataSourceType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Post Send DataSource Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.PostSendDataSourceDef.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Post Send DataSource Def !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (NewService.EmailTo.trim() === '' || (!regex_pattern_Service.test(NewService.EmailTo))) {
        document.getElementById('error-message').innerText = 'Please Provide valid EmailTo (EmailID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (NewService.CCTo.trim() === '' || (!regex_pattern_Service.test(NewService.CCTo))) {
        document.getElementById('error-message').innerText = 'Please Provide valid CCTo (EmailID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (NewService.BccTo.trim() != '' && (!regex_pattern_Service.test(NewService.BccTo))) {
        document.getElementById('error-message').innerText = 'Please Provide valid BccTo (Email ID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (NewService.ASubject.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Email Subject !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.ABody.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Email Body !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.LastExecutedOn.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Last ExecutedOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (NewService.NextExecutionTime.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Next ExecutionTime !';
        document.getElementById('error-message').style.display = 'block';
    } 
    else {
        document.getElementById('error-message').style.display = 'none';
        Ajax.AuthPost("Service/GetService", NewService, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
    }

}

//#endregion -- Create Service

//#region -- Show Service
function ServiceCRUD_OnSuccessCallBack(data) {
    $('#ServiceModal').modal('hide');
    console.log(data);
    //if (data && data.servicemasterList && data.servicemasterList.length > 0) {
    //    data = data.servicemasterList;
    //    Service.BindServiceList(data);
    //}

    if (data && data.servicemasterList && data.servicemasterList.length > 0) {
        data = data.servicemasterList;

        if (Navigation.MenuCode == "SCANN")
            Service.BindServiceList(data);
        else if (Navigation.MenuCode == "SVANN")
            ServiceVariables.ServiceList = data;
        else if (Navigation.MenuCode == "SSANN")
            ServiceSchedular.ServiceList = data;
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

                + '                <td>' + data[i].schedularName + '</td>'
                + '                <td>' + data[i].connName + '</td>'
                + '                <td>' + data[i].emailConfigName + '</td>'

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
    Service.LoadAll();

}

function UpdateService_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User Service
Service.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.title + ' ?';
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
    document.getElementById('ServiceDesc').value = dbconn.sDesc;
    document.getElementById('AlertType').value = dbconn.alertType;
    document.getElementById('HasAttachment').checked = dbconn.hasAttachment;
    document.getElementById('AttachmentType').value = dbconn.attachmentType;
    document.getElementById('AttachmentPath').value = dbconn.attachmentPath;
    document.getElementById('AttachmentFileType').value = dbconn.attachmentFileType;
    document.getElementById('OutputFileName').value = dbconn.outputFileName;
    document.getElementById('DataSourceType').value = dbconn.dataSourceType;
    document.getElementById('DataSourceDef').value = dbconn.dataSourceDef;
    document.getElementById('PostSendDataSourceType').value = dbconn.postSendDataSourceType;
    document.getElementById('PostSendDataSourceDef').value = dbconn.postSendDataSourceDef;
    document.getElementById('EmailTo').value = dbconn.emailTo;
    document.getElementById('CCTo').value = dbconn.ccTo;
    document.getElementById('BccTo').value = dbconn.bccTo;
    document.getElementById('ASubject').value = dbconn.aSubject;
    document.getElementById('ABody').value = dbconn.aBody;

    document.getElementById('DBConnId').value = ("DBConnectionSelectOption_" + dbconn.dbConnId);
    document.getElementById('AlertConfigId').value = ("EmailConfigSelectOption_" + dbconn.alertConfigId);
    document.getElementById('SchedularId').value = ("SchedularSelectOption_" + dbconn.schedularId);

    //document.getElementById('DBConnid').value = dbconn.dBConnid;
    //document.getElementById('AlertConfigId').value = dbconn.alertConfigId;
    //document.getElementById('SchedularId').value = dbconn.schedularId;

    document.getElementById('LastExecutedOn').value = dbconn.lastExecutedOn;//.split("T")[0];
    document.getElementById('NextExecutionTime').value = dbconn.nextExecutionTime;//.split("T")[0];
    document.getElementById('isUserActive').checked = dbconn.isActive;

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

    var UpdateService = {};

    var dbConnidSelect = document.getElementById("DBConnId");
    var alertConfigIdSelect = document.getElementById("AlertConfigId");
    var schedularIdSelect = document.getElementById("SchedularId");

    UpdateService.ActionUser = User.UserId;

    UpdateService.title = document.getElementById('Title').value ;
    UpdateService.sDesc = document.getElementById('ServiceDesc').value;
    UpdateService.alertType = document.getElementById('AlertType').value;
    UpdateService.hasAttachment = document.getElementById('HasAttachment').checked ? 1 : 0;
    UpdateService.attachmentType = document.getElementById('AttachmentType').value;
    UpdateService.attachmentPath = document.getElementById('AttachmentPath').value;
    UpdateService.attachmentFileType = document.getElementById('AttachmentFileType').value;
    UpdateService.outputFileName = document.getElementById('OutputFileName').value;
    UpdateService.dataSourceType = document.getElementById('DataSourceType').value;
    UpdateService.dataSourceDef = document.getElementById('DataSourceDef').value;
    UpdateService.postSendDataSourceType = document.getElementById('PostSendDataSourceType').value;
    UpdateService.postSendDataSourceDef = document.getElementById('PostSendDataSourceDef').value;
    UpdateService.emailTo = document.getElementById('EmailTo').value;
    UpdateService.cCTo = document.getElementById('CCTo').value;
    UpdateService.bccTo = document.getElementById('BccTo').value;
    UpdateService.aSubject = document.getElementById('ASubject').value;
    UpdateService.aBody = document.getElementById('ABody').value;

    UpdateService.dbConnId = JSON.parse(decodeURIComponent(dbConnidSelect.options[dbConnidSelect.selectedIndex].getAttribute("customData"))).dbConnId;
    UpdateService.alertConfigId = JSON.parse(decodeURIComponent(alertConfigIdSelect.options[alertConfigIdSelect.selectedIndex].getAttribute("customData"))).emailConfigId;
    UpdateService.schedularId = JSON.parse(decodeURIComponent(schedularIdSelect.options[schedularIdSelect.selectedIndex].getAttribute("customData"))).schedularId;

    //UpdateService.dBConnId = document.getElementById('DBConnId').value;
    //UpdateService.alertConfigId  = document.getElementById('AlertConfigId').value;
    //UpdateService.schedularId  = document.getElementById('SchedularId').value;

    UpdateService.lastExecutedOn = document.getElementById('LastExecutedOn').value;
    UpdateService.nextExecutionTime = document.getElementById('NextExecutionTime').value;

    UpdateService.serviceId = document.getElementById('ServiceId').value;
    UpdateService.isActive = document.getElementById('isUserActive').checked ? 1 : 0;

    if (UpdateService.title.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Service Name !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.sDesc.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Service Description !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.alertType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Alert Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.attachmentType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Attachment Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.attachmentPath.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Attachment Path !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.attachmentFileType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Attachment FileType !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.outputFileName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Output FileName !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.dataSourceType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide DataSource Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.dataSourceDef.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide DataSource Def !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.postSendDataSourceType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Post Send DataSource Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.postSendDataSourceDef.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Post Send DataSource Def !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (UpdateService.emailTo.trim() === '' || (!regex_pattern_Service.test(UpdateService.emailTo))) {
        document.getElementById('error-message').innerText = 'Please Provide valid EmailTo (EmailID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (UpdateService.cCTo.trim() === '' || (!regex_pattern_Service.test(UpdateService.cCTo))) {
        document.getElementById('error-message').innerText = 'Please Provide valid CCTo (EmailID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (UpdateService.bccTo.trim() != '' && (!regex_pattern_Service.test(UpdateService.bccTo))) {
        document.getElementById('error-message').innerText = 'Please Provide valid BccTo (Email ID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (UpdateService.aSubject.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Email Subject !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.aBody.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Email Body !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.lastExecutedOn.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Last ExecutedOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (UpdateService.nextExecutionTime.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Next ExecutionTime !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        document.getElementById('error-message').style.display = 'none';
        Ajax.AuthPost("Service/GetService", UpdateService, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
    }


    // Perform validation
    //var ValidationMsg = " Please provide ";
    //ValidationMsg += (UpdateService.title.trim() === '') ? " Title," : '';
    //ValidationMsg += (UpdateService.sDesc.trim() === '') ? " Desc," : '';
    //ValidationMsg += (UpdateService.alertType.trim() === '') ? " AlertType," : '';
    //ValidationMsg += (UpdateService.attachmentType.trim() === '') ? " AttachmentType," : '';
    //ValidationMsg += (UpdateService.attachmentPath.trim() === '') ? " AttachmentPath," : '';
    //ValidationMsg += (UpdateService.attachmentFileType.trim() === '') ? " AttachmentFileType," : '';
    //ValidationMsg += (UpdateService.outputFileName.trim() === '') ? " OutputFileName," : '';
    //ValidationMsg += (UpdateService.dataSourceType.trim() === '') ? " DataSourceType," : '';
    //ValidationMsg += (UpdateService.dataSourceDef.trim() === '') ? " DataSourceDef," : '';
    //ValidationMsg += (UpdateService.postSendDataSourceType.trim() === '') ? " PostSendDataSourceType," : '';
    //ValidationMsg += (UpdateService.postSendDataSourceDef.trim() === '') ? " PostSendDataSourceDef," : '';
    //ValidationMsg += (UpdateService.emailTo.trim() === '') ? " EmailTo," : '';
    //ValidationMsg += (UpdateService.cCTo.trim() === '') ? " CCTo," : '';
    //ValidationMsg += (UpdateService.aSubject.trim() === '') ? " Subject," : '';
    //ValidationMsg += (UpdateService.aBody.trim() === '') ? " Body," : '';
    //ValidationMsg += (UpdateService.lastExecutedOn.trim() === '') ? " LastExecutedOn," : '';
    //ValidationMsg += (UpdateService.nextExecutionTime.trim() === '') ? " NextExecutionTime," : '';

    //if (ValidationMsg.trim() != "Please provide") {
    //    alert(ValidationMsg);
    //}
    //else {
    //    Ajax.AuthPost("Service/GetService", UpdateService, ServiceCRUD_OnSuccessCallBack, ServiceCRUD_OnErrorCallBack);
    //}


}
//#endregion -- Update User Service

//#endregion -- Service


Service.CloseModal = function () {
    $('#ServiceModal').modal('hide');

}




