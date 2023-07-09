//let table = new DataTable('#menuTable');
var dataTable;
$(document).ready(function () {
    dataTable = $('#orderTable').DataTable({
        "ajax": {
            "url": "/api/order",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "id", "width": "15%" }, 
            { "data": "pickupName", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpTime", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                            <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2"><i class="bi bi-pencil-square"></i></a>
                            </div>`
                },
                "width": "15%"
            }
        ],
        "width":"100%"
    })
})
