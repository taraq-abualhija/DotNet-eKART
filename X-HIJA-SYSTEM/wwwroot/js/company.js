var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblCompany').DataTable({
        ajax: '/admin/company/ApiGetAll',
        columns: [
            { data: 'name', width: "15%" },
            { data: 'address', width: "10%" },
            { data: 'city', width: "10%" },
            { data: 'state', width: "10%" },
            { data: 'postalCode', width: "15%" },
            { data: 'phoneNumber', width: "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class=" btn-group d-flex" role="group">
                    <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit </a>
                    <a onClick=Delete("/admin/company/ApiDelete?id=${data}") class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Delete </a>
                </div>`;
                },
                width: "25%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }

            })
        }
    });
}



