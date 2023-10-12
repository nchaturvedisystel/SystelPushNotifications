ServiceSchedular = new Object();
ServiceSchedular.MappperId = 0;
ServiceSchedular.ServiceId = 0;
ServiceSchedular.SchedularId = 0;
ServiceSchedular.LastExecutionTime = new Date();
ServiceSchedular.NextExecutionTime = new Date();
ServiceSchedular.StartsFrom = new Date();
ServiceSchedular.EndsOn = new Date();
ServiceSchedular.DailyStartOn = "";
ServiceSchedular.DailyEndsOn = "";
ServiceSchedular.IsActive = 0;
ServiceSchedular.ActionUser = User.UserId;
ServiceSchedular.IsDeleted = 0;

ServiceSchedular.ServiceList = {};
ServiceSchedular.SchedularList = {}; 

//#region -- ServiceSchedular
ServiceSchedular.CreateServiceSchedularOnReady = function () {
    ServiceSchedular.LoadAll();
    Schedular.LoadAll();
    Service.LoadAll();
}

ServiceSchedular.LoadAll = function () {
    ServiceSchedular.ActionUser = User.UserId;
    Ajax.AuthPost("ServiceSchedular/GetServiceSchedular", ServiceSchedular, ServiceSchedularCRUD_OnSuccessCallBack, ServiceSchedularCRUD_OnErrorCallBack);
}
//#region -- Create ServiceSchedular

ServiceSchedular.CreateNew = function () {
    $('#ServiceSchedularModal').modal('show');
    ServiceSchedular.BindSchedularDropDown();
    ServiceSchedular.BindServiceIdDropDown();
    ServiceSchedular.ClearServiceSchedularCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = ServiceSchedular.ValidateAndCreateServiceSchedular;
}

ServiceSchedular.BindSchedularDropDown = function () {
    var select = document.getElementById("SchedularId");
    var data = ServiceSchedular.SchedularList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="SchedularSelectOption_' + data[i].schedularId + '" id="SchedularSelectOption_' + data[i].schedularId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].iName + '</option>';
        select.innerHTML = select.innerHTML + optionHtml;
    }

}
ServiceSchedular.BindServiceIdDropDown = function () {
    var select = document.getElementById("ServiceId");
    var data = ServiceSchedular.ServiceList;
    select.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var optionHtml = '<option value="ServiceSelectOption_' + data[i].serviceId + '" id="ServiceSelectOption_' + data[i].serviceId + '" customData="' + encodeURIComponent(JSON.stringify(data[i])) + '">' + data[i].title + '</option>';
        select.innerHTML = select.innerHTML + optionHtml;
    }

}


ServiceSchedular.ClearServiceSchedularCRUDForm = function () {

    document.getElementById('LastExecutionTime').value = new Date();
    document.getElementById('NextExecutionTime').value = new Date();
    document.getElementById('StartsFrom').value = new Date();
    document.getElementById('EndsOn').value = new Date();
    document.getElementById('DailyStartOn').value = "";
    document.getElementById('DailyEndsOn').value = "";
    document.getElementById('isUserActive').checked = true;
    ServiceSchedular.MappperId = 0;
    ServiceSchedular.LastExecutedOn = new Date();
    ServiceSchedular.NextExecutionTime = new Date();
    ServiceSchedular.StartsFrom = new Date();
    ServiceSchedular.EndsOn = new Date();
    ServiceSchedular.DailyStartOn = "";
    ServiceSchedular.DailyEndsOn = "";
    ServiceSchedular.IsActive = 1;
    ServiceSchedular.IsDeleted = 0;
};

ServiceSchedular.ValidateAndCreateServiceSchedular = function () {

    var ServiceIdSelect = document.getElementById("ServiceId");
    var SchedularIdSelect = document.getElementById("SchedularId");

    ServiceSchedular.ActionUser = User.UserId;

    ServiceSchedular.LastExecutionTime = document.getElementById("LastExecutionTime").value;
    ServiceSchedular.NextExecutionTime = document.getElementById("NextExecutionTime").value;
    ServiceSchedular.StartsFrom = document.getElementById("StartsFrom").value;
    ServiceSchedular.EndsOn = document.getElementById("EndsOn").value;
    ServiceSchedular.DailyStartOn = document.getElementById("DailyStartOn").value;
    ServiceSchedular.DailyEndsOn = document.getElementById("DailyEndsOn").value;

    ServiceSchedular.ServiceId = JSON.parse(decodeURIComponent(ServiceIdSelect.options[ServiceIdSelect.selectedIndex].getAttribute("customData"))).serviceId;
    ServiceSchedular.SchedularId = JSON.parse(decodeURIComponent(SchedularIdSelect.options[SchedularIdSelect.selectedIndex].getAttribute("customData"))).schedularId;
    ServiceSchedular.IsActive = 1;

    //var ValidationMsg = " Please provide ";
    //ValidationMsg += (ServiceSchedular.LastExecutionTime.trim() === '') ? " LastExecutionTime," : '';
    //ValidationMsg += (ServiceSchedular.NextExecutionTime.trim() === '') ? " NextExecutionTime," : '';
    //ValidationMsg += (ServiceSchedular.StartsFrom.trim() === '') ? " StartsFrom," : '';
    //ValidationMsg += (ServiceSchedular.EndsOn.trim() === '') ? " EndsOn," : '';
    //ValidationMsg += (ServiceSchedular.DailyStartOn.trim() === '') ? " DailyStartOn," : '';
    //ValidationMsg += (ServiceSchedular.DailyEndsOn.trim() === '') ? " DailyEndsOn," : '';
    //if (ValidationMsg.trim() != "Please provide") {
    //    alert(ValidationMsg);
    //}
    //else {
    //    Ajax.AuthPost("ServiceSchedular/GetServiceSchedular", ServiceSchedular, ServiceSchedularCRUD_OnSuccessCallBack, ServiceSchedularCRUD_OnErrorCallBack);
    //}


    if (ServiceSchedular.LastExecutionTime.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide LastExecutionTime !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (ServiceSchedular.NextExecutionTime.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide NextExecutionTime !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (ServiceSchedular.StartsFrom.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide StartsFrom !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (ServiceSchedular.EndsOn.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide EndsOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (ServiceSchedular.DailyStartOn.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide DailyStartOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (ServiceSchedular.DailyEndsOn.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide DailyEndsOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("ServiceSchedular/GetServiceSchedular", ServiceSchedular, ServiceSchedularCRUD_OnSuccessCallBack, ServiceSchedularCRUD_OnErrorCallBack);
    }

}

//#endregion -- Create ServiceSchedular

//#region -- Show ServiceSchedular
function ServiceSchedularCRUD_OnSuccessCallBack(data) {
    $('#ServiceSchedularModal').modal('hide');
    console.log(data);
    if (data && data.serviceschedularList && data.serviceschedularList.length > 0) {
        data = data.serviceschedularList;
         ServiceSchedular.BindServiceSchedularList(data);
    }



    //ServiceSchedular.ClearServiceSchedularCRUDForm();

}

function ServiceSchedularCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

ServiceSchedular.BindServiceSchedularList = function (data) {
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
                + '                <td>' + data[i].iName + '</td>'
                + '                <td>' + data[i].lastExecutionTime + '</td>'
                + '                <td>' + data[i].nextExecutionTime + '</td>'
                + '                <td>' + data[i].startsFrom + '</td>'
                + '                <td>' + data[i].endsOn + '</td>'
                + '                <td>' + data[i].dailyStartOn + '</td>'
                + '                <td>' + data[i].dailyEndsOn + '</td>'
                + '                <td>'
                + '                     <input type="checkbox" id="ServiceSchedularIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="ServiceSchedular.ServiceSchedularStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="ServiceSchedular.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="ServiceSchedular.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
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
            + '<td  colspan = "10">'
            + ' <font style="color:red;">No Records found..</font>'
            + '        </td>'
            + '    </tr>');
    }
    ServiceSchedular.ClearServiceSchedularCRUDForm();
}
//#endregion -- Show User Role



ServiceSchedular.ServiceSchedularStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.mappperId = data.mappperId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("ServiceSchedular/UpdateStatusServiceSchedular", dbConnData, UpdateServiceSchedular_OnSuccesscallback, UpdateServiceSchedular_OnErrorCallBack);

}
function UpdateServiceSchedular_OnSuccesscallback(response) {
    if (response.isActive === 1) {

        Toast.create("Success", "ServiceSchedular Active", TOAST_STATUS.SUCCESS, 1500);
        
    } else {
        Toast.create("Success", "ServiceSchedular Inactive", TOAST_STATUS.WARNING, 1500);
    }
    ServiceSchedular.LoadAll();
}

function UpdateServiceSchedular_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User ServiceSchedular
ServiceSchedular.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.title + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteServiceSchedular);
}
function DeleteServiceSchedular(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("ServiceSchedular/GetServiceSchedular", dbconn, ServiceSchedularCRUD_OnSuccessCallBack, ServiceSchedularCRUD_OnErrorCallBack);
}

//#endregion -- Delete User ServiceSchedular

//#region -- Update User ServiceSchedular
ServiceSchedular.SetServiceSchedularCRUDForm = function (dbconn) {

    document.getElementById('MappperId').value = dbconn.mappperId;

    document.getElementById('LastExecutionTime').value = dbconn.lastExecutionTime;//.split("T")[0];
    document.getElementById('NextExecutionTime').value = dbconn.nextExecutionTime;//.split("T")[0];
    document.getElementById('StartsFrom').value = dbconn.startsFrom;//.split("T")[0];
    document.getElementById('EndsOn').value = dbconn.endsOn;//.split("T")[0];
    document.getElementById('DailyStartOn').value = dbconn.dailyStartOn;
    document.getElementById('DailyEndsOn').value = dbconn.dailyEndsOn;

    document.getElementById('ServiceId').value = ("ServiceSelectOption_" + dbconn.serviceId);
    document.getElementById('SchedularId').value = ("SchedularSelectOption_" + dbconn.schedularId);

    document.getElementById('isUserActive').checked = dbconn.isActive === 1;

};
ServiceSchedular.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#ServiceSchedularModal').modal('show');
    ServiceSchedular.BindSchedularDropDown();
    ServiceSchedular.BindServiceIdDropDown();
    ServiceSchedular.SetServiceSchedularCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = ServiceSchedular.ValidateAndUpdateServiceSchedular;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

ServiceSchedular.ValidateAndUpdateServiceSchedular = function (dbconn) {

    var ServiceIdSelect = document.getElementById("ServiceId");
    var schedularIdSelect = document.getElementById("SchedularId");

    dbconn.ActionUser = User.UserId;

    dbconn.lastExecutionTime = document.getElementById('LastExecutionTime').value;
    dbconn.nextExecutionTime = document.getElementById('NextExecutionTime').value;
    dbconn.startsFrom = document.getElementById('StartsFrom').value;
    dbconn.endsOn = document.getElementById('EndsOn').value;
    dbconn.dailyStartOn = document.getElementById('DailyStartOn').value;
    dbconn.dailyEndsOn = document.getElementById('DailyEndsOn').value;
    dbconn.mappperId = document.getElementById('MappperId').value;
    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;

    dbconn.serviceId = JSON.parse(decodeURIComponent(ServiceIdSelect.options[ServiceIdSelect.selectedIndex].getAttribute("customData"))).serviceId;
    dbconn.schedularId = JSON.parse(decodeURIComponent(schedularIdSelect.options[schedularIdSelect.selectedIndex].getAttribute("customData"))).schedularId;


    // Perform validation
    //var ValidationMsg = " Please provide ";
    //ValidationMsg += (dbconn.lastExecutionTime.trim() === '') ? " LastExecutionTime," : '';
    //ValidationMsg += (dbconn.nextExecutionTime.trim() === '') ? " NextExecutionTime," : '';
    //ValidationMsg += (dbconn.startsFrom.trim() === '') ? " StartsFrom," : '';
    //ValidationMsg += (dbconn.endsOn.trim() === '') ? " EndsOn," : '';
    //ValidationMsg += (dbconn.dailyStartOn.trim() === '') ? " DailyStartOn," : '';
    //ValidationMsg += (dbconn.dailyEndsOn.trim() === '') ? " DailyEndsOn," : '';

    //if (ValidationMsg.trim() != "Please provide") {
    //    alert(ValidationMsg);
    //}
    //else {
    //    Ajax.AuthPost("ServiceSchedular/GetServiceSchedular", dbconn, ServiceSchedularCRUD_OnSuccessCallBack, ServiceSchedularCRUD_OnErrorCallBack);
    //}


    if (dbconn.lastExecutionTime.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide LastExecutionTime !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.nextExecutionTime.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide NextExecutionTime !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.startsFrom.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide StartsFrom !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.endsOn.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide EndsOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.dailyStartOn.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide DailyStartOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.dailyEndsOn.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide DailyEndsOn !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("ServiceSchedular/GetServiceSchedular", dbconn, ServiceSchedularCRUD_OnSuccessCallBack, ServiceSchedularCRUD_OnErrorCallBack);
    }
   
}
//#endregion -- Update User ServiceSchedular

//#endregion -- ServiceSchedular

ServiceSchedular.CloseModal = function () {
    $('#ServiceSchedularModal').modal('hide');

}

