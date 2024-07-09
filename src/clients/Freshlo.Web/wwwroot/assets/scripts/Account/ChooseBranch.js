!(function (e) {
    $('#choosebranchId').on('click', function () {
        var branchId = $("#branch").prop('value');
        if (branchId == "0") {
            swal.fire({
                title: "Please Select Branch",
                type: "error",
                confirmButtonText: "Ok!"
            }).then(function (e) {
                if (e.value) {                  
                    location.reload();
                }
            });
        }
        else {
            Cookies.set('BranchId', branchId);
            window.location.href = "/Sale/PendingOrders"
        }       
    }); 
})();