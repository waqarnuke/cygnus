var dataTable;
$(document).ready(function() {
    
    loadDataTable();
});
function loadDataTable() {
    
    dataTable = $("#tblData").DataTable( {
        ajax: {
            url: '/Admin/User/GetAll',
        },
        columns : [
            { data: "displayName", "width" :"25%" },
            { data: 'email' , "width" :"15%" },
            { data: 'phoneNumber' , "width" :"10%"},
            { data: 'compnay.name' , "width" :"20%" },
            { data: 'role' , "width" :"15%" },
            { 
                data: {id:'id', lockoutEnd:'lockoutEnd'},  
                "render" :
                function(data){
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if(lockout > today)
                    {
                        return `
                        <div text-center">
                        <a onclick=LockUnLock('${data.id}') class="btn btn-danger" style="curson:pointer; width:100px"><i class="bi bi-lock-fill"></i> UnLock </a>
                        <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger" style="curson:pointer; width:100px">
                            <i class="bi bi-pencil-square"></i> Permission
                        </a>
                        </div>
                        `
                    }
                    else
                    {
                        return `
                        <div text-center">
                            <a onclick=LockUnLock('${data.id}') class="btn btn-success" style="curson:pointer; width:100px"><i class="bi bi-unlock-fill"></i> UnLock </a>
                            <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger" style="curson:pointer; width:100px">
                                <i class="bi bi-pencil-square"></i> Permission
                            </a>
                        </div>
                        `
                    }
                },
                "width" :"10%" 
            }
        ],
        columnDefs: [
            { orderable: false } //This part is ok now
        ]
    } );
    
}

function LockUnLock(id){
    $.ajax({
        type:"POST",
        url:'/Admin/User/LockUnLock',
        data:JSON.stringify(id),
        contentType: "application/json",
        success: function(data){
            if(data.success){
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        } 
    });
}


