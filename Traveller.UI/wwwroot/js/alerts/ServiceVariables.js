ServiceVariables = new Object();
ServiceVariables.VariableId = 0;
ServiceVariables.ServiceId = 0;
ServiceVariables.VarInstance = "";
ServiceVariables.VarValue = "";
ServiceVariables.VarType = "";
ServiceVariables.IsActive = 0;
ServiceVariables.ActionUser = User.UserId;
ServiceVariables.IsDeleted = 0;

ServiceVariables.ServiceList = {};

//#region -- ServiceVariables
ServiceVariables.CreateServiceVariablesOnReady = function () {
    ServiceVariables.LoadAll();
    Service.LoadAll();

}

ServiceVariables.LoadAll = function () {
    ServiceVariables.ActionUser = User.UserId;
    Ajax.AuthPost("ServiceVariables/GetServiceVariables", ServiceVariables, ServiceVariablesCRUD_OnSuccessCallBack, ServiceVariablesCRUD_OnErrorCallBack);
}
//#region -- Create ServiceVariables

ServiceVariables.CreateNew = function () {
    $('#ServiceVariablesModal').modal('show');
    ServiceVariables.BindServiceIdDropDown();
    ServiceVariables.ClearServiceVariablesCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = ServiceVariables.ValidateAndCreateServiceVariables;
}

ServiceVariables.BindServiceIdDropDown = function () {
    var select = document.getElementById("ServiceId");
    var data = ServiceVariables.ServiceList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="ServiceSelectOption_' + data[i].serviceId + '" id="ServiceSelectOption_' + data[i].serviceId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].title + '</option>';
        select.innerHTML = select.innerHTML + optionHtml;
    }

}

ServiceVariables.ClearServiceVariablesCRUDForm = function () {

    document.getElementById('VarValue').value = "";
    document.getElementById('VarInstance').value = "";
    document.getElementById('VarType').value = "";
    document.getElementById('isUserActive').checked = true;
    ServiceVariables.VariableId = 0;
    //ServiceVariables.ServiceId = 0;
    ServiceVariables.VarValue = "";
    ServiceVariables.VarInstance = "";
    ServiceVariables.VarType = "";
    Schedular.IsActive = 1;
    Schedular.IsDeleted = 0;
};

ServiceVariables.ValidateAndCreateServiceVariables = function () {

    var serviceIdSelect = document.getElementById("ServiceId");

    //ServiceVariables.ActionUser = User.UserId;
    ServiceVariables.VarValue = document.getElementById("VarValue").value;
    ServiceVariables.VarInstance = document.getElementById("VarInstance").value;
    ServiceVariables.VarType = document.getElementById("VarType").value;

    ServiceVariables.ServiceId = JSON.parse(decodeURIComponent(serviceIdSelect.options[serviceIdSelect.selectedIndex].getAttribute("customData"))).serviceId;

    ServiceVariables.IsActive = 1;

    // Perform validation
    //var ValidationMsg = " Please provide ";
    //ValidationMsg += (ServiceVariables.VarInstance.trim() === '') ? " VarInstance," : '';
    //ValidationMsg += (ServiceVariables.VarValue.trim() === '') ? " VarValue," : '';
    //ValidationMsg += (ServiceVariables.VarType.trim() === '') ? " VarType," : '';

    //if (ValidationMsg.trim() != "Please provide") {
    //    alert(ValidationMsg);
    //}
    //else {
    //    Ajax.AuthPost("ServiceVariables/GetServiceVariables", ServiceVariables, ServiceVariablesCRUD_OnSuccessCallBack, ServiceVariablesCRUD_OnErrorCallBack);
    //}

    if (ServiceVariables.VarInstance.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide VarInstance !';
        document.getElementById('error-message').style.display = 'block';
    }
   
    else if (ServiceVariables.VarValue.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide VarValue !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (ServiceVariables.VarType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide VarType !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("ServiceVariables/GetServiceVariables", ServiceVariables, ServiceVariablesCRUD_OnSuccessCallBack, ServiceVariablesCRUD_OnErrorCallBack);
    }

}

//#endregion -- Create ServiceVariables

//#region -- Show ServiceVariables
function ServiceVariablesCRUD_OnSuccessCallBack(data) {
    $('#ServiceVariablesModal').modal('hide');
    console.log(data);
    if (data && data.alertsserviceVariablesList && data.alertsserviceVariablesList.length > 0) {
        data = data.alertsserviceVariablesList;
        ServiceVariables.BindServiceVariablesList(data);      
    }   

}

function ServiceVariablesCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

ServiceVariables.BindServiceVariablesList = function (data) {
    console.log(data);
    if (data && data.length > 0) {
        var body = document.getElementById("TemplateListBody");
        body.innerHTML = "";
        let SrNo = 0;
        for (var i = 0; i < data.length; i++) {
            console.log(data[i])
            SrNo += 1;
            var RowHtml = ('<tr>'
                + '                <td class="dtr-control sorting_1" style="border-left: 5px solid #' + Util.WCColors[i] + ';">' + SrNo + '</td>'
                + '                <td>' + data[i].title + '</td>'
                + '                <td>' + data[i].varInstance  + '</td>'
                + '                <td>' + data[i].varValue + '</td>'
                + '                <td>' + data[i].varType + '</td>'
                + '                <td>'
                + '                     <input type="checkbox" id="ServiceVariableIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="ServiceVariables.ServiceVariableStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="ServiceVariables.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="ServiceVariables.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
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
    ServiceVariables.ClearServiceVariablesCRUDForm();
}
//#endregion -- Show ServiceVariables



ServiceVariables.ServiceVariableStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.variableId = data.variableId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("ServiceVariables/UpdateStatusServiceVariables", dbConnData, UpdateServiceVariables_OnSuccesscallback, UpdateServiceVariables_OnErrorCallBack);

}
function UpdateServiceVariables_OnSuccesscallback(response) {
    if (response.isActive === 1) {

        Toast.create("Success", "ServiceVariable Active", TOAST_STATUS.SUCCESS, 1500);
    } else {
        Toast.create("Success", "ServiceVariable  Inactive", TOAST_STATUS.WARNING, 1500);
    }
    ServiceVariables.LoadAll();
}

function UpdateServiceVariables_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User Schedular
ServiceVariables.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.title + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteServiceVariables);
}
function DeleteServiceVariables(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("ServiceVariables/GetServiceVariables", dbconn, ServiceVariablesCRUD_OnSuccessCallBack, ServiceVariablesCRUD_OnErrorCallBack);
}

//#endregion -- Delete User Schedular

//#region -- Update User Schedular
ServiceVariables.SetServiceVariablesCRUDForm = function (dbconn) {

    document.getElementById('VariableId').value = dbconn.variableId;
    document.getElementById('VarInstance').value = dbconn.varInstance;
    document.getElementById('VarValue').value = dbconn.varValue;
    document.getElementById('VarType').value = dbconn.varType;

    document.getElementById('ServiceId').value = ("ServiceSelectOption_" + dbconn.serviceId);

    document.getElementById('isUserActive').checked = dbconn.isActive === 1;


};
ServiceVariables.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#ServiceVariablesModal').modal('show');
    ServiceVariables.BindServiceIdDropDown();
    ServiceVariables.SetServiceVariablesCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = ServiceVariables.ValidateAndUpdateServiceVariables;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

ServiceVariables.ValidateAndUpdateServiceVariables = function (dbconn) {
    //dbconn.ActionUser = User.UserId;

    var dbserviceidSelect = document.getElementById("ServiceId");

    dbconn.varInstance = document.getElementById('VarInstance').value;
    dbconn.varValue = document.getElementById('VarValue').value;
    dbconn.varType = document.getElementById('VarType').value;
    dbconn.variableId = document.getElementById('VariableId').value;

    dbconn.serviceId = JSON.parse(decodeURIComponent(dbserviceidSelect.options[dbserviceidSelect.selectedIndex].getAttribute("customData"))).serviceId;

    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;

    // Perform validation
    //var ValidationMsg = " Please provide ";
    //ValidationMsg += (dbconn.varInstance.trim() === '') ? " varInstance," : '';
    //ValidationMsg += (dbconn.varValue.trim() === '') ? " varValue," : '';
    //ValidationMsg += (dbconn.varType.trim() === '') ? " varType," : '';

    //if (ValidationMsg.trim() != "Please provide") {
    //    alert(ValidationMsg);
    //}
    //else {
    //    Ajax.AuthPost("ServiceVariables/GetServiceVariables", dbconn, ServiceVariablesCRUD_OnSuccessCallBack, ServiceVariablesCRUD_OnErrorCallBack);
    //}

    if (dbconn.varInstance.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide VarInstance !';
        document.getElementById('error-message').style.display = 'block';
    }
 
    else if (dbconn.varValue.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide VarValue !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.varType.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide VarType !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("ServiceVariables/GetServiceVariables", dbconn, ServiceVariablesCRUD_OnSuccessCallBack, ServiceVariablesCRUD_OnErrorCallBack);
    }


}
//#endregion -- Update ServiceVariables

//#endregion -- ServiceVariables

ServiceVariables.CloseModal = function () {
    $('#ServiceVariablesModal').modal('hide');

}



