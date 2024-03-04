
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {

    datatable = $('#tblData').DataTable({
        "ajax": {
            url: '/Admin/Product/GetAll', // Corrected URL
            type: 'GET', // Assuming it's a GET request
            dataType: 'json' // Assuming the response is JSON
        },
        "columns": [
            { data: 'productTitle', "width": "25%" },
            { data: 'productISBN', "width": "10%" },
            { data: 'productListPrice', "width": "10%" },
            { data: 'productAuthor', "width": "10%" },
            { data: 'category.categoryName', "width": "10%" },
            {
                
                data: 'productID',
                "width": "25%",
                render: function (data) {
                    return `
                         <a href="/Admin/Product/UpsertProduct?id=${data}" class="btn btn-delete mx-2">
                         <i class="bi bi-pencil-square"></i> Edit
                          </a>
                         <a OnClick=DeleteProduct('/Admin/Product/DeleteProduct?id=${data}') class="btn btn-delete mx-2">
                                <i class="bi bi-trash-fill"></i> Delete
                        </a>
                        `;
                }
            }
        ]
    });
}

function DeleteProduct(url) {
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
                url: url,
                type: 'DELETE',
                success: function (data) {
                    datatable.ajax.reload();
                    swalWithBootstrapButtons.fire({
                        title: "Deleted!",
                        text: "This Product has been deleted.",
                        icon: "success"
                    });
                }
            })
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your Product is safe :)",
                icon: "error"
            });
        }
    });
}