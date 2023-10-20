Schedular = new Object();
Schedular.SchedularId = 0;
Schedular.IName = "";
Schedular.ICode = "";
Schedular.IDesc = "";
Schedular.FrequencyInMinutes = 0;
Schedular.SchedularType = "";
Schedular.IsActive = 0;
Schedular.ActionUser = User.UserId;
Schedular.IsDeleted = 0;


//#region -- Schedular
Schedular.CreateSchedularOnReady = function () {
    Schedular.LoadAll();
}

Schedular.LoadAll = function () {
    Schedular.ActionUser = User.UserId;
    Ajax.AuthPost("Schedular/GetSchedular", Schedular, SchedularCRUD_OnSuccessCallBack, SchedularCRUD_OnErrorCallBack);
}
//#region -- Create Schedular

Schedular.CreateNew = function () {
    $('#SchedularModal').modal('show');
    Schedular.ClearSchedularCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = Schedular.ValidateAndCreateSchedular;
}

Schedular.ClearSchedularCRUDForm = function () {
    document.getElementById('SchedularName').value = "";
    document.getElementById('SchedularCode').value = "";
    document.getElementById('SchedularDesc').value = "";
    document.getElementById('FrequencyInMinutes').value = 0;
    document.getElementById('SchedularType').value = "";
    document.getElementById('isUserActive').checked = true;
    Schedular.SchedularId = 0;
    Schedular.IName = "";
    Schedular.ICode = "";
    Schedular.IDesc = "";
    Schedular.SchedularType = "";
    Schedular.FrequencyInMinutes = 0;
    Schedular.IsActive = 1;
    Schedular.IsDeleted = 0;
};

Schedular.ValidateAndCreateSchedular = function () {

    Schedular.ActionUser = User.UserId;
    Schedular.IName = document.getElementById("SchedularName").value;
    Schedular.ICode = document.getElementById("SchedularCode").value;
    Schedular.IDesc = document.getElementById("SchedularDesc").value;
    Schedular.FrequencyInMinutes = document.getElementById("FrequencyInMinutes").value;
    Schedular.SchedularType = document.getElementById("SchedularType").value;
    Schedular.IsActive = 1;

    if (Schedular.IName.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Schedular Name !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (Schedular.ICode.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Schedular Code !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (Schedular.IDesc.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Description !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (Schedular.FrequencyInMinutes.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Frequency In Minutes !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (Schedular.SchedularType.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Schedular Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';

        // Perform AJAX request
        Ajax.AuthPost("Schedular/GetSchedular", Schedular, SchedularCRUD_OnSuccessCallBack, SchedularCRUD_OnErrorCallBack);

    }

}

//#endregion -- Create Schedular

//#region -- Show Schedular
function SchedularCRUD_OnSuccessCallBack(data) {
    $('#SchedularModal').modal('hide');
    console.log(data);
    if (data && data.schedularmasterList && data.schedularmasterList.length > 0) {
        data = data.schedularmasterList;
        if (Navigation.MenuCode == "SRANN")
            Schedular.BindSchedularList(data);
        else if (Navigation.MenuCode == "SCANN")
            Service.SchedularList = data;

        else if (Navigation.MenuCode == "SSANN")
            ServiceSchedular.SchedularList = data;
    }
    //Schedular.ClearSchedularCRUDForm();

}

function SchedularCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

Schedular.BindSchedularList = function (data) {
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
                + '                <td>' + data[i].iCode + '</td>'
/*                + '                <td>' + data[i].iDesc + '</td>'*/
                + '                <td>' + data[i].frequencyInMinutes + '</td>'
                + '                <td>' + data[i].schedularType + '</td>'
                + '                <td>'
                + '                     <input type="checkbox" id="SchedularIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="Schedular.SchedularStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="Schedular.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="Schedular.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
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
    Schedular.ClearSchedularCRUDForm();
}
//#endregion -- Show User Role



Schedular.SchedularStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.schedularId = data.schedularId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("Schedular/UpdateStatusSchedular", dbConnData, UpdateSchedular_OnSuccesscallback, UpdateSchedular_OnErrorCallBack);

}
function UpdateSchedular_OnSuccesscallback(response) {
    if (response.isActive === 1) {

        Toast.create("Success", "Schedular Active", TOAST_STATUS.SUCCESS, 1500);
        
    } else {
        Toast.create("Success", "Schedular Inactive", TOAST_STATUS.WARNING, 1500);
    }
    Schedular.LoadAll();
}

function UpdateSchedular_OnErrorCallBack(data) {
    Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
}


//#region -- Delete User Schedular
Schedular.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.iName + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteSchedular);
}
function DeleteSchedular(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("Schedular/GetSchedular", dbconn, SchedularCRUD_OnSuccessCallBack, SchedularCRUD_OnErrorCallBack);
}

//#endregion -- Delete User Schedular

//#region -- Update User Schedular
Schedular.SetSchedularCRUDForm = function (dbconn) {

    document.getElementById('SchedularId').value = dbconn.schedularId;
    document.getElementById('SchedularName').value = dbconn.iName;
    document.getElementById('SchedularCode').value = dbconn.iCode;
    document.getElementById('SchedularDesc').value = dbconn.iDesc;
    document.getElementById('FrequencyInMinutes').value = dbconn.frequencyInMinutes;
    document.getElementById('SchedularType').value = dbconn.schedularType;
    document.getElementById('isUserActive').checked = dbconn.isActive === 1;


};
Schedular.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#SchedularModal').modal('show');
    Schedular.SetSchedularCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = Schedular.ValidateAndUpdateSchedular;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

Schedular.ValidateAndUpdateSchedular = function (dbconn) {
    dbconn.ActionUser = User.UserId;
    dbconn.iName = document.getElementById('SchedularName').value;
    dbconn.iCode = document.getElementById('SchedularCode').value;
    dbconn.iDesc = document.getElementById('SchedularDesc').value;
    dbconn.frequencyInMinutes = document.getElementById('FrequencyInMinutes').value;
    dbconn.schedularType = document.getElementById('SchedularType').value;
    dbconn.schedularId = document.getElementById('SchedularId').value;
    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;
    //Ajax.AuthPost("Schedular/GetSchedular", dbconn, SchedularCRUD_OnSuccessCallBack, SchedularCRUD_OnErrorCallBack);


    if (dbconn.iName.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Schedular Name !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.iCode.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Schedular Code !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.iDesc.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Description !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.frequencyInMinutes.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Frequency In Minutes !';
        document.getElementById('error-message').style.display = 'block';
    }
    else if (dbconn.schedularType.trim() === '') {
        // Display error message
        document.getElementById('error-message').innerText = 'Please Provide Schedular Type !';
        document.getElementById('error-message').style.display = 'block';
    }
    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';

        // Perform AJAX request
        Ajax.AuthPost("Schedular/GetSchedular", dbconn, SchedularCRUD_OnSuccessCallBack, SchedularCRUD_OnErrorCallBack);

    }


}
//#endregion -- Update User Schedular

//#endregion -- Schedular

Schedular.CloseModal = function () {
    $('#SchedularModal').modal('hide');

}



