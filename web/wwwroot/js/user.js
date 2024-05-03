var dataTable;
$( document ).ready(function() {
    
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
                data: 'id',  
                "render" :
                function(data){
                    return '<div class="w-75 btn-group text-center" role="group">'+ 
                    '<a href="/admin/Product/upsert?id='+data+' "><i class="bi bi-pencil-square text-primary "></i> </a>'
                    +'</div>'
                },
                "width" :"10%" 
            }
        ],
        columnDefs: [
            { orderable: false } //This part is ok now
        ]
    } );
    
}


