// Calling All Partail View 
$(document).ready(function () {
    var branch = $('#branch').val();
    GetTotalItemCount();
});
function GetTotalItemCount() {
    var hubId = $('#hubId').val();
    var ItemId = $('#ItemId').val();
    var info = {
        ItemId: ItemId,
        featured: featured,
        Approval: Approval,
        CoupenDisc: CoupenDisc,
        hubId: hubId,
        Itemnever: Itemnever,
        FoodSubType: FoodSubType,
        FoodSubType1: FoodSubType1,
        FoodSubType1: FoodSubType1,
    }
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Management/ItemDashboard?hubId=' + hubId,
        type: 'GET',
        data: info,
        success: function (response) {
            $('#CoupenDiscId').html(response.CoupenDisc);
            $('#approvel').html(reponse.Approval);
            $('#featured').html(response.featured);
            $('#itemcount').html(response.itemid);
            $('#neverorder').html(response.Itemnever);
            $('#vegcount').html(response.FoodSubType);
            $('#vegcount').html(response.FoodSubType1);           
            $('#m_portlet_loader').css('display', 'none');
        },
        error: function (er, sw, sq) {
            alert("Error occured in application");
        }
    });
}