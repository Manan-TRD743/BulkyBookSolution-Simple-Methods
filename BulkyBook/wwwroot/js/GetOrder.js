
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else if (url.includes("pending")) {
        loadDataTable("pending");
    }
    else if (url.includes("completed")) {
        loadDataTable("completed");
    }
    else if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else {
        loadDataTable("all");
    }
    
});
function loadDataTable(status) {

    datatable = $('#tblData').DataTable({
        "ajax": {
            url:  '/Admin/Order/getall?status=' + status , // Corrected URL
            type: 'GET', // Assuming it's a GET request
            dataType: 'json' // Assuming the response is JSON
        },
        "columns": [
            { data: 'orderHeaderID', "width": "10%" },
            { data: 'userName', "width": "20%" },
            { data: 'userPhoneNumber', "width": "15%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {

                data: 'orderHeaderID',
                "width": "20%",
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                         <a href="/Admin/Order/OrderDetails?OrderID=${data}" class="btn btn-delete mx-2">
                         <i class="bi bi-pencil-square"></i>
                          </a>
                         </div> `
                }
            }
        ]
    });
}
