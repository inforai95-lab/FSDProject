$(document).ready(function () {
    ShowEmployeeData();
});
function ShowEmployeeData() {   
    $.ajax({
        url: '/Employee/EmployeeList',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, statu, xhr) {
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.id + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.city + '</td>';
                object += '<td>' + item.state + '</td>';
                object += '<td>' + item.salary + '</td>';
                object += '<td><a href="#" class="btn btn-primary"onclick="Edit(' + item.id + ')">Edit</a> || <a href="#" class="btn btn-danger"onclick="Delete(' + item.id + ')">Delete</a></td>';
                object += '</tr>';
            });
            $('#table_data').html(object);
        },
        error: function () {
            alert("data can't get");

        },

    });
};
$('#btnAddEmployee').click(function () {
    ClearTextBox();
    $('#EmpId').modal('show');  
    $('#divId').hide();
    $('#btnAdd').css('display', 'block');
    $('#btnupdate').css('display', 'none');
    $('#UpdateEmp').text('Add Employee');
})
function AddEmployee() {  
    var objdata = {
        Name:$('#Name').val(),
        City:$('#City').val(),
        State:$('#State').val(),
        Salary:$('#Salary').val()
    }
    $.ajax({
        url: '/Employee/AddEmployee',
        type: 'Post',
        data: objdata,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'json',
        success: function() {
            alert('data save');
            ClearTextBox();
            ShowEmployeeData();
            HideModelpopup();

        },
        error: function () {
            alert("data not save");
        }
    });
}
function ClearTextBox() {
    $('#EmployeeId').val(''),
    $('#Name').val('');
    $('#City').val('');
    $('#State').val('');
    $('#Salary').val('');
}
function HideModelpopup() {
    $('#EmpId').modal('hide');
}
function Delete(id) {
    if (confirm('Are you sure you want to delete the data')) {
        $.ajax({
            url: '/Employee/Delete?id=' + id,
            type: 'POST', 
            success: function (response) {
                alert("Delete successful");
                ShowEmployeeData();
            },
            error: function (xhr, status, error) {
                alert("Failed to delete data");
                console.log(error);
            }
        });

    }
 
}
function Edit(id) {
    debugger
    $.ajax({
        url: '/Employee/Edit?id=' + id,
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'json',

        success: function (response) {        
            $('#EmpId').modal('show');         
            $('#EmployeeId').val(response.id);
            $('#Name').val(response.name);
            $('#City').val(response.city);
            $('#State').val(response.state);
            $('#Salary').val(response.salary);
            $('#btnAdd').hide();
            $('#btnupdate').show();
            $('#UpdateEmp').text('Update Record');
        },

        error: function (xhr, status, error) {
            alert("Data not loaded");
            console.log("Status:", status);
            console.log("Error:", error);
            console.log("Response:", xhr.responseText);
        }
    });
}
function UpdateEmployee() {
    var objdata = {
        Id: $('#EmployeeId').val(),
        Name: $('#Name').val(),
        City: $('#City').val(),
        State: $('#State').val(),
        Salary: $('#Salary').val()
    }
    $.ajax({
        url: '/Employee/Update',
        type: 'Post',
        data: objdata,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'json',
        success: function () {
            alert('data save');
            ClearTextBox();
            ShowEmployeeData();
            HideModelpopup();

        },
        error: function () {
            alert("data not save");
        }
    });

}