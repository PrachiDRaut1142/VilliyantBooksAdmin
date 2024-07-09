$(function () {
    var filter = $('#filterDrpDwn').val();
    var category = $('#categoryDrpDwn').val();
    var availability = $('#availabilityDrpDwn').val();
    var approval = "Approved";
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Purchase/_itemSummary',
        type: 'POST',
        data: { "filter": filter, "category": category, "availability": availability, "approval": approval },
        success: function (result) {

            $('#tblDivId').html(result);
            $('#summarytbl').DataTable();
            $('#m_portlet_loader').css('display', 'none');
        }
    });
});
$('#filterBtn').on('click', function () {
    partialfun();
});
function partialfun() {
    var filter = $('#filterDrpDwn').val();
    var category = $('#categoryDrpDwn').val();
    var availability = $('#availabilityDrpDwn').val();
    var approval = $('#approvalDrpDwn').val();
    var subCategory = $('#subCatDrpDwn').val();
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Purchase/_itemSummary',
        type: 'POST',
        data: { "filter": filter, "category": category, "availability": availability, "approval": approval, "subCategory": subCategory},       
        success: function (result) {           
            $('#tblDivId').html(result);
            $('#summarytbl').DataTable();
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}
$('.clear-Cls').on('click', function () {
    $("#summaryForm").trigger("reset");
    partialfun();
});
$('#MainUl').on('shown.bs.tab','.category-cls', function (e) {
    CallCategories();
});
function CallCategories(){
    var mainCategory = $('#mainCategoryDrpDwn').val();
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Purchase/_categorySummary',
        type: 'POST',
        data: { "mainCategory": mainCategory },
        success: function (result) {

            $('#tblCategory').html(result);
            $('#categorySummarytbl').DataTable();
            $('#m_portlet_loader').css('display', 'none');
        }
    });
}
$('#CategoryFilter').on('click', function () {
    CallCategories();
});
$('.Categoryclear-Cls').on('click', function () {
    $("#CategorySummaryForm").trigger("reset");
    CallCategories();
});