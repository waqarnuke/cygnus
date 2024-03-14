var dataTable;
$( document ).ready(function() {
    
    loadDataTable();
});
function loadDataTable() {
    
    dataTable = $("#tblData").DataTable( {
        ajax: {
            url: '/Admin/Product/GetAll',
        },
        columns : [
            { data: "title", "width" :"25%" },
            { data: 'isbn' , "width" :"15%" },
            { data: 'listPrice' , "width" :"10%"},
            { data: 'author' , "width" :"20%" },
            { data: 'category.name' , "width" :"15%" },
            { 
                data: 'id', 
                "render" : function(data){
                    return '<div class="w-75 btn-group text-center" role="group">'+ 
                    '<a href="/admin/Product/upsert?id='+data+' "><i class="bi bi-pencil-square text-primary "></i> </a>'+
                    '<a onClick=Delete('+data+') class="ps-3"><i class="bi bi-trash3 text-primary"></i></a>'
                    +'</div>'
                },
                "width" :"10%" 
            }
        ],
        columnDefs: [
            { orderable: false, targets: [ 5, ] } //This part is ok now
        ]
    } );
    
}


function Delete(id){
    console.log(id)
    url='/Admin/Product/Delete/' +id ;
    console.log(url)
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
                url:url,
                type : 'DELETE',
                success:function(data){
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })      
        }
    });
}
