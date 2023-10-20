
EmailConfig = new Object();

EmailConfig.EmailConfigId = 0;
EmailConfig.IName = "";
EmailConfig.IDesc = "";
EmailConfig.IHost = "";
EmailConfig.IPort = "";
EmailConfig.IFrom = "";
EmailConfig.IPassword = "";
EmailConfig.IEnableSsl = false;
EmailConfig.IsBodyHtml = true;
EmailConfig.IsActive = 0;
EmailConfig.ActionUser = User.UserId;
EmailConfig.IsDeleted = 0;

const regex_pattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

//#region -- EmailConfig
EmailConfig.CreateEmailConfigOnReady = function () {
    EmailConfig.LoadAll();
}

EmailConfig.LoadAll = function () {
    EmailConfig.ActionUser = User.UserId;
    Ajax.AuthPost("EmailConfig/GetEmailConfig", EmailConfig, EmailConfigCRUD_OnSuccessCallBack, EmailConfigCRUD_OnErrorCallBack);
}
//#region -- Create EmailConfig

EmailConfig.CreateNew = function () {
    $('#EmailConfigModal').modal('show');
    EmailConfig.ClearEmailConfigCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = EmailConfig.ValidateAndCreateEmailConfig;
}

EmailConfig.ClearEmailConfigCRUDForm = function () {
    document.getElementById('IName').value = "";
    document.getElementById('IDesc').value = "";
    document.getElementById('IHost').value = "";
    document.getElementById('IPort').value = "";
    document.getElementById('IFrom').value = "";
    document.getElementById('IPassword').value = "";
    document.getElementById('IEnableSsl').checked = false;
    document.getElementById('IsBodyHtml').checked = true;
    document.getElementById('isUserActive').checked = true;

    EmailConfig.EmailConfigId = 0;
    EmailConfig.IName = "";
    EmailConfig.IDesc = "";
    EmailConfig.IHost = "";
    EmailConfig.IPort = "";
    EmailConfig.IFrom = "";
    EmailConfig.IPassword = "";
    EmailConfig.IEnableSsl = false;
    EmailConfig.IsBodyHtml = true;
    EmailConfig.IsActive = 1;
    EmailConfig.IsDeleted = 0;
};

EmailConfig.ValidateAndCreateEmailConfig = function () {

    EmailConfig.ActionUser = User.UserId;
    EmailConfig.IName = document.getElementById("IName").value;
    EmailConfig.IDesc = document.getElementById("IDesc").value;
    EmailConfig.IHost = document.getElementById("IHost").value;
    EmailConfig.IPort = document.getElementById("IPort").value;
    EmailConfig.IFrom = document.getElementById("IFrom").value;
    EmailConfig.IPassword = document.getElementById("IPassword").value;
    EmailConfig.IsActive = 1;
    EmailConfig.IEnableSsl = false;
    EmailConfig.IsBodyHtml = true;

    if (EmailConfig.IName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Email Configuration Name !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if ( EmailConfig.IHost.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Host !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (EmailConfig.IPort.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Port !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (EmailConfig.IFrom.trim() === '' || (!regex_pattern.test(EmailConfig.IFrom))) {
        document.getElementById('error-message').innerText = 'Please Provide valid From (EmailID) !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (EmailConfig.IPassword.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Password !';
        document.getElementById('error-message').style.display = 'block';
    }

    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("EmailConfig/GetEmailConfig", EmailConfig, EmailConfigCRUD_OnSuccessCallBack, EmailConfigCRUD_OnErrorCallBack);
    }

}

//#endregion -- Create EmailConfig

//#region -- Show EmailConfig
function EmailConfigCRUD_OnSuccessCallBack(data) {
    $('#EmailConfigModal').modal('hide');
    console.log(data);
    if (data && data.emailConfigList && data.emailConfigList.length > 0) {
        data = data.emailConfigList;

        if (Navigation.MenuCode == "ECANN")
            EmailConfig.BindEmailConfigList(data);
        else if (Navigation.MenuCode == "SCANN")
            Service.EmailConfigList = data;
        
    }

    //EmailConfig.ClearEmailConfigCRUDForm();
}



function EmailConfigCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

EmailConfig.BindEmailConfigList = function (data) {
    console.log(data);
    if (data && data.length > 0) {
        var body = document.getElementById("TemplateListBody");
        body.innerHTML = "";
        let SrNo = 0;
        for (var i = 0; i < data.length; i++) {
            SrNo += 1;
            var RowHtml = ('<tr>'
                + '                <td class="dtr-control sorting_1" style="border-left: 5px solid #' + Util.WCColors[i] + ';">' + SrNo + '</td>'
                + '                <td>' + data[i].iName + '</td>'
/*                + '                <td>' + data[i].iDesc + '</td>'*/
                + '                <td>' + data[i].iHost + '</td>'
                + '                <td>' + data[i].iPort + '</td>'
                + '                <td>' + data[i].iFrom + '</td>'
/*                + '                <td>' + data[i].iPassword + '</td>'*/
                + '                <td>'
                + '                     <input type="checkbox" id="EmailConfigIEnableSsl"' + (data[i].iEnableSsl ? ' checked' : '') + ' >'
                + '                </td>'
                + '                <td>'
                + '                     <input type="checkbox" id="EmailConfigIsBodyHtml"' + (data[i].isBodyHtml ? ' checked' : '') + '  >'
                + '                </td>'
                + '                <td>'
                + '                     <input type="checkbox" id="EmailConfigIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="EmailConfig.EmailConfigStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="EmailConfig.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="EmailConfig.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
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
    EmailConfig.ClearEmailConfigCRUDForm();
}
//#endregion -- Show User Role



EmailConfig.EmailConfigStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.EmailConfigId = data.emailConfigId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("EmailConfig/UpdateStatusEmailConfig", dbConnData, UpdateEmailConfig_OnSuccesscallback, UpdateEmailConfig_OnErrorCallBack);

}
function UpdateEmailConfig_OnSuccesscallback(response) {
    if (response.isActive === 1) {

        Toast.create("Success", "Email Config Active", TOAST_STATUS.SUCCESS, 1500);
    } else {
        Toast.create("Success", "Email Config Inactive", TOAST_STATUS.WARNING, 1500);
    }
    EmailConfig.LoadAll();
}

function UpdateEmailConfig_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User EmailConfig
EmailConfig.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.iName + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteEmailConfig);
}
function DeleteEmailConfig(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("EmailConfig/GetEmailConfig", dbconn, EmailConfigCRUD_OnSuccessCallBack, EmailConfigCRUD_OnErrorCallBack);
}

//#endregion -- Delete User EmailConfig

//#region -- Update User EmailConfig
EmailConfig.SetEmailConfigCRUDForm = function (dbconn) {
    document.getElementById('EmailConfigId').value = dbconn.emailConfigId;
    document.getElementById('IName').value = dbconn.iName;
    document.getElementById('IDesc').value = dbconn.iDesc;
    document.getElementById('IHost').value = dbconn.iHost;
    document.getElementById('IPort').value = dbconn.iPort;
    document.getElementById('IFrom').value = dbconn.iFrom;
    //document.getElementById('IPassword').value = dbconn.iPassword;
    document.getElementById('IEnableSsl').checked = dbconn.iEnableSsl;
    document.getElementById('IsBodyHtml').checked = dbconn.isBodyHtml;
    document.getElementById('isUserActive').checked = dbconn.isActive===1;


};
EmailConfig.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#EmailConfigModal').modal('show');
    EmailConfig.SetEmailConfigCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = EmailConfig.ValidateAndUpdateEmailConfig;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

EmailConfig.ValidateAndUpdateEmailConfig = function (dbconn) {
    dbconn.ActionUser = User.UserId;
    dbconn.iName = document.getElementById('IName').value;
    dbconn.iDesc = document.getElementById('IDesc').value;
    dbconn.iHost = document.getElementById('IHost').value;
    dbconn.iPort = document.getElementById('IPort').value;
    dbconn.iFrom = document.getElementById('IFrom').value;
    dbconn.iPassword = document.getElementById('IPassword').value;

    dbconn.emailConfigId = document.getElementById('EmailConfigId').value;
    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;
    dbconn.iEnableSsl = document.getElementById('IEnableSsl').checked ? true : false;
    dbconn.isBodyHtml = document.getElementById('IsBodyHtml').checked ? true : false;


    //if (dbconn.iName.trim() === '' || dbconn.iHost.trim() === '' || dbconn.iPort.trim() === '' || dbconn.iFrom.trim() === '') {
    //    document.getElementById('error-message').innerText = 'All are mandatory fields.';
    //    document.getElementById('error-message').style.display = 'block';
    //}
    //else {
    //    document.getElementById('error-message').style.display = 'none';
    //    Ajax.AuthPost("EmailConfig/GetEmailConfig", dbconn, EmailConfigCRUD_OnSuccessCallBack, EmailConfigCRUD_OnErrorCallBack);

    //}


    if (dbconn.iName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Email Configuration Name !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.iHost.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Host !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.iPort.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Port !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.iFrom.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide From !';
        document.getElementById('error-message').style.display = 'block';
    }

    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("EmailConfig/GetEmailConfig", dbconn, EmailConfigCRUD_OnSuccessCallBack, EmailConfigCRUD_OnErrorCallBack);
    }


    
}

EmailConfig.CloseModal = function () {
    $('#EmailConfigModal').modal('hide');

}
//#endregion -- Update User EmailConfig

//#endregion -- EmailConfig





