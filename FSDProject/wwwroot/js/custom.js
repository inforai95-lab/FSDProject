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
                object += '<td><a href="#" class="btn btn-primary">Edit</a> || <a href="#" class="btn btn-danger">Delete</a></td>';
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
    $('#EmpId').modal('show');
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
    function HideModelpopup() {
        $('#EmpId').modal('hide');
    }
    function ClearTextBox() {
        $('#Name').val('');
        $('#City').val('');
        $('#State').val('');
        $('#Salary').val('');
    }
}