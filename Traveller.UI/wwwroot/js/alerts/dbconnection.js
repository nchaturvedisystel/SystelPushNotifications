
DBConnection = new Object();

DBConnection.DBConnId = 0;
DBConnection.ConnName = "";
DBConnection.ServerName = "";
DBConnection.UserName = "";
DBConnection.Passwrd = "";
DBConnection.DBName = "";
DBConnection.IsActive = 0;
DBConnection.ActionUser = User.UserId;
DBConnection.IsDeleted = 0;


//#region -- DBConnection
DBConnection.CreateDBConnectionOnReady = function () {
    DBConnection.LoadAll();
}

DBConnection.LoadAll = function () {
    DBConnection.ActionUser = User.UserId;
    Ajax.AuthPost("DBConnection/GetDBConnection", DBConnection, DBConnectionCRUD_OnSuccessCallBack, DBConnectionCRUD_OnErrorCallBack);
}
//#region -- Create DBConnection

DBConnection.CreateNew = function () {
    $('#DBConnectionModal').modal('show');
    DBConnection.ClearDBConnectionCRUDForm();
    //document.getElementById('modalSaveButton').innerHTML = "Create New DB Connection";
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = DBConnection.ValidateAndCreateDBConnection;
}

DBConnection.ClearDBConnectionCRUDForm = function () {
    document.getElementById('ConnName').value = "";
    document.getElementById('ServerName').value = "";
    document.getElementById('UserName').value = "";
    document.getElementById('Passwrd').value = "";
    document.getElementById('DBName').value = "";
    document.getElementById('isUserActive').checked = true;
    DBConnection.DBConnId = 0;
    DBConnection.ConnName = "";
    DBConnection.ServerName = "";
    DBConnection.UserName = "";
    DBConnection.Passwrd = "";
    DBConnection.DBName = "";
    DBConnection.IsActive = 1;
    DBConnection.IsDeleted = 0;
};

DBConnection.ValidateAndCreateDBConnection = function () {

    DBConnection.ActionUser = User.UserId;
    DBConnection.ConnName = document.getElementById("ConnName").value;
    DBConnection.ServerName = document.getElementById("ServerName").value;
    DBConnection.UserName = document.getElementById("UserName").value;
    DBConnection.Passwrd = document.getElementById("Passwrd").value;
    DBConnection.DBName = document.getElementById("DBName").value;
    DBConnection.IsActive = 1;

    if (DBConnection.ConnName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Connection Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (DBConnection.ServerName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Server Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (DBConnection.UserName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide User Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (DBConnection.DBName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide DB Name!';
        document.getElementById('error-message').style.display = 'block';
    } 

    else if (DBConnection.Passwrd.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Password!';
        document.getElementById('error-message').style.display = 'block';
    }

    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("DBConnection/GetDBConnection", DBConnection, DBConnectionCRUD_OnSuccessCallBack, DBConnectionCRUD_OnErrorCallBack);

    }

}

//#endregion -- Create DBConnection


//#region -- Show DBConnection
function DBConnectionCRUD_OnSuccessCallBack(data) {
    $('#DBConnectionModal').modal('hide');
    console.log(data);
    if (data && data.dbConnList && data.dbConnList.length > 0) {
        data = data.dbConnList;

        if (Navigation.MenuCode == "DBANN")
            DBConnection.BindDBConnectionList(data);
        else if (Navigation.MenuCode == "SCANN")
            Service.DBConnectionList = data;
    }
    //DBConnection.ClearDBConnectionCRUDForm();
}

function DBConnectionCRUD_OnErrorCallBack(error) {
    Util.DisplayAutoCloseErrorPopUp("Error Occurred..", 1500);
}

DBConnection.BindDBConnectionList = function (data) {
    console.log(data);
    if (data && data.length > 0) {
        var body = document.getElementById("TemplateListBody");
        body.innerHTML = "";
        let SrNo = 0;
        for (var i = 0; i < data.length; i++) {
            SrNo += 1;
            var RowHtml = ('<tr>'
                + '                <td class="dtr-control sorting_1" style="border-left: 5px solid #' + Util.WCColors[i] + ';">' + SrNo + '</td>'
                + '                <td>' + data[i].connName + '</td>'
                + '                <td>' + data[i].serverName + '</td>'
                + '                <td>' + data[i].userName + '</td>'
/*                + '                <td>' + data[i].passwrd + '</td>'*/
                + '                <td>' + data[i].dbName + '</td>'
                + '                <td>'
                + '                     <input type="checkbox" id="DBConnectionIsActive"' + (data[i].isActive ? ' checked' : '') + ' onchange="DBConnection.DBConnectionStatusUpdate(this,\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')" >'
                + '                </td>'
                + '                <td class="text-center">'
                + '                    <div class="btn-group dots_dropdown">'
                + '                         <button type="button" class="dropdown-toggle" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false">'
                + '                             <i class="fas fa-ellipsis-v"></i>'
                + '                         </button>'
                + '                         <div class="dropdown-menu dropdown-menu-right shadow-lg">'
                + '                             <button class="dropdown-item" type="button" onclick="DBConnection.Update(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
                + '                                 <i class="fa fa-edit"></i> Edit'
                + '                             </button>'
                + '                             <button class="dropdown-item" type="button" onclick="DBConnection.Delete(\'' + encodeURIComponent(JSON.stringify(data[i])) + '\')">'
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
    DBConnection.ClearDBConnectionCRUDForm();
}
//#endregion -- Show User Role



DBConnection.DBConnectionStatusUpdate = function (sender, data) {
    data = JSON.parse(decodeURIComponent(data));
    let dbConnData = {}
    dbConnData.DBConnId = data.dbConnId;
    dbConnData.isActive = sender.checked ? 1 : 0
    dbConnData.actionUser = User.UserId;

    Ajax.AuthPost("DBConnection/UpdateStatusDBConnection", dbConnData, UpdateDBConnection_OnSuccesscallback, UpdateDBConnection_OnErrorCallBack);

}
    function UpdateDBConnection_OnSuccesscallback(response) {
        if (response.isActive === 1) {

            Toast.create("Success", "DB Connection Active", TOAST_STATUS.SUCCESS, 1500);
        } else {
            Toast.create("Success", "DB Connection Inactive", TOAST_STATUS.WARNING, 1500);
        }
        DBConnection.LoadAll();
    }

    function UpdateDBConnection_OnErrorCallBack(data) {
        Toast.create("Danger", "Some Error occured", TOAST_STATUS.DANGER, 1500);
    }


//#region -- Delete User DBConnection
DBConnection.Delete = function (dbconn) {
    dbconn = JSON.parse(decodeURIComponent(dbconn));
    var Title = 'Are you sure, you want to delete ' + dbconn.connName + ' ?';
    Util.DeleteConfirm(dbconn, Title, DeleteDBConnection);
}
function DeleteDBConnection(dbconn) {
    dbconn.IsDeleted = 1;
    dbconn.IsActive = 0;
    dbconn.actionUser = User.UserId;
    Ajax.AuthPost("DBConnection/GetDBConnection", dbconn, DBConnectionCRUD_OnSuccessCallBack, DBConnectionCRUD_OnErrorCallBack);
}

//#endregion -- Delete User DBConnection

//#region -- Update User DBConnection
DBConnection.SetDBConnCRUDForm = function (dbconn) {
    document.getElementById('DBConnId').value = dbconn.dbConnId;
    document.getElementById('ConnName').value = dbconn.connName;
    document.getElementById('ServerName').value = dbconn.serverName;
    document.getElementById('UserName').value = dbconn.userName;
    //document.getElementById('Passwrd').value = dbconn.passwrd;
    document.getElementById('DBName').value = dbconn.dbName;
    document.getElementById('isUserActive').checked = dbconn.isActive;


};
DBConnection.Update = function (dbconn) {

    dbconn = JSON.parse(decodeURIComponent(dbconn));
    $('#DBConnectionModal').modal('show');
    DBConnection.SetDBConnCRUDForm(dbconn);
    document.getElementById('error-message').style.display = 'none';
    document.getElementById('modalSaveButton').onclick = DBConnection.ValidateAndUpdateDBConnection;
    //document.getElementById('modalSaveButton').innerHTML = "Update DB Connection";
}

DBConnection.ValidateAndUpdateDBConnection = function (dbconn) {
    dbconn.ActionUser = User.UserId;
    dbconn.connName = document.getElementById('ConnName').value;
    dbconn.serverName = document.getElementById('ServerName').value;
    dbconn.userName = document.getElementById('UserName').value;
    dbconn.passwrd = document.getElementById('Passwrd').value;
    dbconn.dbName = document.getElementById('DBName').value;
    dbconn.dbConnId = document.getElementById('DBConnId').value;
    dbconn.isActive = document.getElementById('isUserActive').checked ? 1 : 0;

    //if (dbconn.connName.trim() === '' || dbconn.serverName.trim() === '' || dbconn.userName.trim() === '' || dbconn.dbName.trim() === '') {
    //    document.getElementById('error-message').innerText = 'All are mandatory fields.';
    //    document.getElementById('error-message').style.display = 'block';
    //}

    //else {
    //    document.getElementById('error-message').style.display = 'none';
    //    Ajax.AuthPost("DBConnection/GetDBConnection", dbconn, DBConnectionCRUD_OnSuccessCallBack, DBConnectionCRUD_OnErrorCallBack);

    //}


    if (dbconn.connName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Connection Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.serverName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide Server Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.userName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide User Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else if (dbconn.dbName.trim() === '') {
        document.getElementById('error-message').innerText = 'Please Provide DB Name!';
        document.getElementById('error-message').style.display = 'block';
    }

    else {
        // Hide error message if fields are not blank
        document.getElementById('error-message').style.display = 'none';
        // Perform AJAX request
        Ajax.AuthPost("DBConnection/GetDBConnection", dbconn, DBConnectionCRUD_OnSuccessCallBack, DBConnectionCRUD_OnErrorCallBack);

    }
    
}


//#endregion -- Update User DBConnection

//#endregion -- DBConnection

DBConnection.CloseModal = function () {
    $('#DBConnectionModal').modal('hide');

}





