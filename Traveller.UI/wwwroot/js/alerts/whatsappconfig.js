
WhatsAppConfig = new Object();

WhatsAppConfig.WAConfigId = 0;
WhatsAppConfig.IName = "";
WhatsAppConfig.IDesc = "";
WhatsAppConfig.WAUrl = "";
WhatsAppConfig.AccessToken = "";
WhatsAppConfig.IType = "";
WhatsAppConfig.IsActive = 0;
WhatsAppConfig.ActionUser = User.UserId;
WhatsAppConfig.IsDeleted = 0;

//#region -- WhatsAppConfig
WhatsAppConfig.CreateWhatsAppConfigOnReady = function () {
    WhatsAppConfig.LoadAll();
}

WhatsAppConfig.LoadAll = function () {
    WhatsAppConfig.ActionUser = User.UserId;
    Ajax.AuthPost("WhatsAppConfig/GetWhatsAppConfig", WhatsAppConfig, WhatsAppConfigCRUD_OnSuccessCallBack, WhatsAppConfigCRUD_OnErrorCallBack);
}
//#region -- Create WhatsAppConfig

WhatsAppConfig.CreateNew = function () {
    $('#WhatsAppConfigModal').modal('show');
    WhatsAppConfig.ClearWhatsAppConfigCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = WhatsAppConfig.ValidateAndCreateWhatsAppConfig;
}

WhatsAppConfig.ClearWhatsAppConfigCRUDForm = function () {
    document.getElementById('IName').value = "";
    document.getElementById('IDesc').value = "";
    document.getElementById('WAUrl').value = "";
    document.getElementById('AccessToken').value = "";
    document.getElementById('IType').value = "";
    document.getElementById('isUserActive').checked = true;

    WhatsAppConfig.WAConfigId = 0;
    WhatsAppConfig.IName = "";
    WhatsAppConfig.IDesc = "";
    WhatsAppConfig.WAUrl = "";
    WhatsAppConfig.AccessToken = "";
    WhatsAppConfig.IType = "";
    WhatsAppConfig.IsActive = 1;
    WhatsAppConfig.IsDeleted = 0;
};

WhatsAppConfig.ValidateAndCreateWhatsAppConfig = function () {

    WhatsAppConfig.ActionUser = User.UserId;
    WhatsAppConfig.IName = document.getElementById("IName").value;
    WhatsAppConfig.IDesc = document.getElementById("IDesc").value;
    WhatsAppConfig.WAUrl = document.getElementById("WAUrl").value;
    WhatsAppConfig.AccessToken = document.getElementById("AccessToken").value;
    WhatsAppConfig.IType = document.getElementById("IType").value;
    WhatsAppConfig.IsActive = 1;

    if (WhatsAppConfig.IName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide WhatsApp Configuration Name !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (WhatsAppConfig.WAUrl.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide WhatsApp URL !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (WhatsAppConfig.AccessToken.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide WhatsApp AccessToken !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (WhatsAppConfig.IType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Message Type !';
        document.getElementById('error-message').style.display = 'block';
    }

    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("WhatsAppConfig/GetWhatsAppConfig", WhatsAppConfig, WhatsAppConfigCRUD_OnSuccessCallBack, WhatsAppConfigCRUD_OnErrorCallBack);
    }

}

//#endregion -- Create WhatsAppConfig

//#region -- Show WhatsAppConfig
function WhatsAppConfigCRUD_OnSuccessCallBack(data) {
    $('#WhatsAppConfigModal').modal('hide');
    console.log(data);
    if (data && data.whatsAppConfigList && data.whatsAppConfigList.length > 0) {
        data = data.whatsAppConfigList;

     WhatsAppConfig.BindWhatsAppConfigList(data);

    }
}


function WhatsAppConfigCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

WhatsAppConfig.BindWhatsAppConfigList = function (data) {
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
                + '                <td>' + data[i].waUrl + '</td>'
/*               + '                 <td>' + data[i].accessToken + '</td>'*/
                + '                <td>' + data[i].iType + '</td>'
                + '                <td>'
                + '                     <input type="checkbox" id="WhatsAppConfigIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="WhatsAppConfig.WhatsAppConfigStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="WhatsAppConfig.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="WhatsAppConfig.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
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
    WhatsAppConfig.ClearWhatsAppConfigCRUDForm();
}
//#endregion -- Show User Role

WhatsAppConfig.WhatsAppConfigStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.WAConfigId = data.waConfigId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("WhatsAppConfig/UpdateStatusWhatsAppConfig", dbConnData, UpdateWhatsAppConfig_OnSuccesscallback, UpdateWhatsAppConfig_OnErrorCallBack);

}
function UpdateWhatsAppConfig_OnSuccesscallback(response) {
    if (response.isActive === 1) {

        Toast.create("Success", "WhatsApp Config Active", TOAST_STATUS.SUCCESS, 1500);
    } else {
        Toast.create("Success", "WhatsApp Config Inactive", TOAST_STATUS.WARNING, 1500);
    }
    WhatsAppConfig.LoadAll();
}

function UpdateWhatsAppConfig_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User WhatsAppConfig
WhatsAppConfig.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.iName + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteWhatsAppConfig);
}
function DeleteWhatsAppConfig(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("WhatsAppConfig/GetWhatsAppConfig", dbconn, WhatsAppConfigCRUD_OnSuccessCallBack, WhatsAppConfigCRUD_OnErrorCallBack);
}

//#endregion -- Delete User WhatsAppConfig

//#region -- Update User WhatsAppConfig
WhatsAppConfig.SetWhatsAppConfigCRUDForm = function (dbconn) {
    document.getElementById('WAConfigId').value = dbconn.waConfigId;
    document.getElementById('IName').value = dbconn.iName;
    document.getElementById('IDesc').value = dbconn.iDesc;
    document.getElementById('WAUrl').value = dbconn.waUrl;
    document.getElementById('AccessToken').value = dbconn.accessToken;
    document.getElementById('IType').value = dbconn.iType;
    document.getElementById('isUserActive').checked = dbconn.isActive === 1;

};
WhatsAppConfig.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#WhatsAppConfigModal').modal('show');
    WhatsAppConfig.SetWhatsAppConfigCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = WhatsAppConfig.ValidateAndUpdateWhatsAppConfig;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

WhatsAppConfig.ValidateAndUpdateWhatsAppConfig = function (dbconn) {
    dbconn.ActionUser = User.UserId;
    dbconn.iName = document.getElementById('IName').value;
    dbconn.iDesc = document.getElementById('IDesc').value;
    dbconn.waUrl = document.getElementById('WAUrl').value;
    dbconn.accessToken = document.getElementById('AccessToken').value;
    dbconn.iType = document.getElementById('IType').value;

    dbconn.waConfigId = document.getElementById('WAConfigId').value;
    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;


    if (dbconn.iName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide WhatsApp Configuration Name !';
        document.getElementById('error-message').style.display = 'block';
    }


    else if (dbconn.waUrl.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide WhatsApp URL !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.accessToken.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide WhatsApp AccessToken !';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.iType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Message Type !';
        document.getElementById('error-message').style.display = 'block';
    }


    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("WhatsAppConfig/GetWhatsAppConfig", dbconn, WhatsAppConfigCRUD_OnSuccessCallBack, WhatsAppConfigCRUD_OnErrorCallBack);
    }



}

WhatsAppConfig.CloseModal = function () {
    $('#WhatsAppConfigModal').modal('hide');

}
//#endregion -- Update User WhatsAppConfig

//#endregion -- WhatsAppConfig





