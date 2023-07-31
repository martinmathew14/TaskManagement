// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    PopulateAllTasks();


    $("#frmTask").validate({

        rules: {
            "Name": {
                required: true
            },
            "Status": {
                required: true
            }
        },
            message: {
                "Name": {
                    required: "The Task Name field is required."
                },
                "Status": {
                    required: "The Status field is required"
                },
            }
    });

    
});



function PopulateAllTasks() {

    $.ajax({
        url: 'http://localhost:5276/api/Task',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var tableBody = $("#TaskTable tbody");
            tableBody.empty();
            $.each(data, function (index, task) {
                tableBody.append('<tr><td>' + task.taskId + '</td><td>'
                    + task.name + '</td><td>' + task.priority
                    + '</td><td>' + task.status + '</td>'
                    + '</td><td>'
                    + "<a href='#!' onclick='return GetTaskById(" + task.taskId + ");'>Edit</a>"
                    +'</td>'
                    + '</td><td>' + "<a href='#!' onclick='return DeleteById(" + task.taskId + ");'>Delete</a>" + '</td>'
                    +'</tr > ');
            });
        },
        error: function () {
            alert('Error occurred while fetching data.');
        }
    });

}


function GetTaskById(taskId) {

    $.ajax({
        url: 'http://localhost:5276/api/Task/' + taskId,
        type: 'GET',
        dataType: 'json',
        success: function (task) {


            $("#txtTaskName").val(task.name);
            $("#txtPriority").val(task.priority);
            $("#hdnTaskId").val(task.taskId);
            $("#ddlStatus option").map(function () {
                if ($(this).text() == task.status) return this;
            }).prop('selected', 'selected');

        },
        error: function () {
            alert('Error occurred while fetching data.');
        }
    });

}

function DeleteById(taskId) {

    $.ajax({
        url: 'http://localhost:5276/api/Task/' + taskId ,
        type: 'Delete',
        dataType: 'text',
        
        success: function (task) {
            PopulateAllTasks();
            ClearAll();
            $("#divSucess").text("Deleted Sucessfully");
            $("#divFailure").text("");
        },
        error: function () {
            $("#divSucess").text("");
            $("#divFailure").text("Only completed task can be deleted.");
        }
    });

}

function ClearAll() {

    $("#txtTaskName").val('');
    $("#txtPriority").val('');
    $("#hdnTaskId").val('');
    $("#ddlStatus option").map(function () {
        if ($(this).text() == '') return this;
    }).prop('selected', 'selected');
    $("#divFailure").text("");
    $("#divSucess").text("");
}