!(function ($) {
    $('#m_portlet_loader').css('display', 'block');
    $.ajax({
        url: '/Dashboard/_Customer/?filter=' + 1,
        type: "POST",
        success: function (result) {
            $('#TodayBasedData').html(result);
            $('#TodayBasedData').css('display', 'block');
            $('#HubBaseData').css('display', 'none');
            $('#ZipCodeBaseData').css('display', 'none');
            $('#m_portlet_loader').css('display', 'none');
        }
    });
    $.selector_cache('#SelectCount').on('change', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        $("#Hub option[value='" + 0 + "']").prop('selected', true);
        $("#ZipCode option[value='" + 0 + "']").prop('selected', true);
        $.ajax({
            url: '/Dashboard/_Customer?filter=' + SelectCountId,
            type: 'POST',
            success: function (result) {
                $('#TodayBasedData').html(result);
                $('#TodayBasedData').css('display', 'block');
                $('#HubBaseData').css('display', 'none');
                $('#ZipCodeBaseData').css('display', 'none');
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    });
    $.selector_cache('#TodayBasedData').on('click', '#TotalDownload', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 1;
        window.location.href = '/Customer/Manage/?a=' + SelectCountId + '&b=' + Status;
    });
    $.selector_cache('#TodayBasedData').on('click', '#Registered', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 2;
        window.location.href = '/Customer/Manage/?a=' + SelectCountId + '&b=' + Status;
    });
    $.selector_cache('#TodayBasedData').on('click', '#Unregistered', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 3;
        window.location.href = '/Customer/UnregisteredCustomer/?a=' + SelectCountId + '&b=' + Status;
    });
    $.selector_cache('#TodayBasedData').on('click', '#RegisteredZeroOrder', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 4;
        window.location.href = '/Customer/Manage/?a=' + SelectCountId + '&b=' + Status;
    });
    $.selector_cache('#TodayBasedData').on('click', '#RegisteredMoreOrder', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var Status = 5;
        window.location.href = '/Customer/Manage/?a=' + SelectCountId + '&b=' + Status;
    });
    $.selector_cache('#Hub').on('change', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var HubId = $('#Hub').prop('value');
        $("#ZipCode option[value='" + 0 + "']").prop('selected', true);
        $.ajax({
            url: '/Dashboard/_CustomerHubFilter/?filter=' + SelectCountId + '&Hub=' + HubId,
            type: 'POST',
            success: function (result) {
                $('#HubBaseData').html(result);
                $('#TodayBasedData').css('display', 'none');
                $('#HubBaseData').css('display', 'block');
                $('#ZipCodeBaseData').css('display', 'none');
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    });
    $.selector_cache('#ZipCode').on('change', function (e) {
        $('#m_portlet_loader').css('display', 'block');
        var SelectCountId = $('#SelectCount').prop('value');
        var ZipCode = $('#ZipCode').prop('value');
        $("#Hub option[value='" + 0 + "']").prop('selected', true);
        $.ajax({
            url: '/Dashboard/_CustomerZipCode/?filter=' + SelectCountId + '&ZipCode=' + ZipCode,
            type: 'POST',
            success: function (result) {
                $('#ZipCodeBaseData').html(result);
                $('#TodayBasedData').css('display', 'none');
                $('#HubBaseData').css('display', 'none');
                $('#ZipCodeBaseData').css('display', 'block');
                $('#m_portlet_loader').css('display', 'none');
            }
        });
    });
})(jQuery);