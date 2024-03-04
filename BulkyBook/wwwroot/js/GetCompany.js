$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            url: '/Admin/Company/GetAllCompany',
            type: 'GET',
            dataType: 'json'
        },
        "columns": [
            { data: 'companyName', "width": "10%" },
            { data: 'streetAddress', "width": "12%" },
            { data: 'city', "width": "10%" },
            { data: 'state', "width": "10%" },
            { data: 'postalCode', "width": "12%" },
            { data: 'phoneNumber', "width": "12%" },
            {
                data: 'companyID',
                "width": "20%",
                render: function (data) {
                    return `
                         <a href="/Admin/Company/UpsertCompany?id=${data}" class="btn btn-delete mx-2">
                         <i class="bi bi-pencil-square"></i> Edit
                          </a>
                         <button onclick="DeleteCompany(${data})" class="btn btn-delete mx-2">
                                <i class="bi bi-trash-fill"></i> Delete
                        </button>
                        `;
                }
            }
        ]
    });
}

function DeleteCompany(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success m-2",
            cancelButton: "btn btn-danger "
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/Company/DeleteCompany?id=' + id,
                type: 'DELETE',
                success: function (data) {
                    datatable.ajax.reload();
                    swalWithBootstrapButtons.fire({
                        title: "Deleted!",
                        text: "This Company has been deleted.",
                        icon: "success"
                    });
                }
            })
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your Company Detalis are safe :)",
                icon: "error"
            });
        }
    });
}
