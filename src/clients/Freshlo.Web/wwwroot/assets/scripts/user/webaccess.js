!(function () {
    $('#btnSubmitId').on('click', function () {
        var numberOfErrors = 0;
        if ($('#m_select2_3').prop('value') == 0) {
            numberOfErrors++;
            $('#EmployeeId_Err').css("display", "block");
        }
        if (!$('#MacAddress').prop('value')) {
            numberOfErrors++;
            $('#MacAddress_Err').css("display", "block");
        }
        if (!$('#WhoisIp').prop('value')) {
            numberOfErrors++;
            $('#WhoisIp_Err').css("display", "block");
        }
        if (numberOfErrors == 0) {
            $('#UserWebAccessformid').submit();
        }
    });
    $('#btnWhitelistSubmitId').on('click', function () {
        var numberOfErrors = 0;
        if ($('.whitelistemployee').prop('value') == "") {
            numberOfErrors++;
            $('#EmployeeIds_Err').css("display", "block");
        }
        if (numberOfErrors == 0) {
            $('#WhitelistformId').submit();
        }
    });
    $('.Whitelistedit').on('click', function () {
        var select = $('.whitelistemployee');
        $.ajax({
            url: '/User/GetIpMappedWhitelistEmployee',
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                select.empty();
                var ST = data.getEmployeeNameSLbyID;
                $.each(data.getEmployeeNameSLbyID, function () {
                    select.append($("<option />").val(this.value).text(this.text).attr("selected", "true"));
                });
                $.each(data.getEmployeeNameSL, function () {
                    select.append($("<option />").val(this.value).text(this.text));
                });
            }
        });
        $('#whitelistControlEdit').modal('show');
    })
    $('.AccesscontroleditId').on('click', function () {
        var mySendId = $(this).data('id');
        var accessdetail = mySendId.split('_');
        $('#Id').val(accessdetail[0]);
        //$('#EmployeeId').val(data[0].multipleEmployeeId);
        $('#MacAddress').val(accessdetail[2]);
        $('#WhoisIp').val(accessdetail[3]);
        var select = $('#m_select2_3');
        $.ajax({
            url: '/User/GetIpMappedEmployee',
            type: 'POST',
            dataType: 'json',
            data: { WebAccressId: accessdetail[0] },
            success: function (data) {
                select.empty();
                var ST = data.getEmployeeNameSLbyID;
                $.each(data.getEmployeeNameSLbyID, function () {
                    select.append($("<option />").val(this.value).text(this.text).attr("selected", "true"));
                });
                $.each(data.getEmployeeNameSL, function () {
                    select.append($("<option />").val(this.value).text(this.text));
                });
            }
        });
        $('#accessControlEdit').modal('show');
    });
    $('#accesscontrolcreate').on('click', function () {
        $('#Id').val('');
        $('#MacAddress').val('');
        $('#WhoisIp').val('');
        var select = $('#m_select2_3');
        $.ajax({
            url: '/User/GetAllEmployee',
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                select.empty();
                var ST = data.getEmployeeNameSL;
                $.each(data.getEmployeeNameSL, function () {
                    select.append($("<option />").val(this.value).text(this.text));
                });
            }
        });
        $('#accessControlEdit').modal('show');
    });
    $('#whitelistcontrolcreate').on('click', function () {
        $('#whitelistControlEdit').modal('show');
    });
    $('#DisableId').on('click', function () {
        var atLeastOneIsChecked = $('input[name="Check[]"]:checked').length;
        if (atLeastOneIsChecked > 0) {
            var checkboxdata = [];
            var inputElements = document.getElementsByClassName('messageCheckbox');
            for (var i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    checkedValue = inputElements[i].value;
                    checkboxdata.push(checkedValue);
                }
            }
            var Checkboxidsdata = {
                "Checkboxid": checkboxdata
            };
            $.ajax({
                url: '/User/webAccessStatusChange/',
                type: 'Post',
                dataType: 'json',
                data: { 'webaccessInfo': Checkboxidsdata, 'Status': 2 },
                success: function (data) {
                    if (data == true) {
                        swal.fire({
                            title: "Saved Successfully!",
                            type: "success",
                            confirmButtonText: "Ok!"
                        }).then(function (e) {
                            if (e.value) {
                                location.reload();
                            }
                        });
                    }
                    else {
                        swal.fire("Error", "Something went wrong. Please try again later", "error");
                    }
                }
            });
        }
        else {
            swal.fire("Select Checkbox!", "", "warning");
        }
    });
    $('#EnableId').on('click', function () {
        var atLeastOneIsChecked = $('input[name="Check[]"]:checked').length;
        if (atLeastOneIsChecked > 0) {
            var checkboxdata = [];
            var inputElements = document.getElementsByClassName('messageCheckbox');
            for (var i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    checkedValue = inputElements[i].value;
                    checkboxdata.push(checkedValue);
                }
            }
            var Checkboxidsdata = {
                "Checkboxid": checkboxdata
            };
            $.ajax({
                url: '/User/webAccessStatusChange/',
                type: 'Post',
                dataType: 'json',
                data: { 'webaccessInfo': Checkboxidsdata, 'Status': 1 },
                success: function (data) {
                    if (data == true) {
                        swal.fire({
                            title: "Saved Successfully!",
                            type: "success",
                            confirmButtonText: "Ok!"
                        }).then(function (e) {
                            if (e.value) {
                                location.reload();
                            }
                        });
                    }
                    else {
                        swal.fire("Error", "Something went wrong. Please try again later", "error");
                    }
                }
            });
        }
        else {
            swal.fire("Select Checkbox!", "", "warning");
        }
    });
    $('#selectallid').on('click', function () {
        if ($('input[type=checkbox]').prop('checked') == true) {
            $("input:checkbox").prop('checked', $(this).is(":checked"));
        } else {
            $("input:checkbox").prop('checked', false);
        }
    });
    $('#globalIpId').on('click', function () {
        $('#GlobalId').val('');
        $('#GlobalIp').val('');
        $('#globalIpEdit').modal('show');
    });
    $('#btnGlobalIpSubmitId').on('click', function () {
        var numberOfErrors = 0;
        if ($('#GlobalIp').prop('value') == 0) {
            numberOfErrors++;
            $('#GlobalIp_Err').css("display", "block");
        }
        if (numberOfErrors == 0) {
            $('#GlobalIpformid').submit();
        }
    });
    $('.GlobalIpcontroleditId').on('click', function () {
        var mySendId = $(this).data('id');
        var accessdetail = mySendId.split('_');
        $('#GlobalId').val(accessdetail[0]);
        $('#GlobalIp').val(accessdetail[1]);
        $('#globalIpEdit').modal('show');
    });
    $('#DisableglobalId').on('click', function () {
        var atLeastOneIsChecked = $('input[name="Checkglobal[]"]:checked').length;
        if (atLeastOneIsChecked > 0) {
            var checkboxdata = [];
            var inputElements = document.getElementsByClassName('globalmessageCheckbox');
            for (var i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    checkedValue = inputElements[i].value;
                    checkboxdata.push(checkedValue);
                }
            }
            var Checkboxidsdata = {
                "Checkboxid": checkboxdata
            };
            $.ajax({
                url: '/User/globalAccessStatusChange/',
                type: 'Post',
                dataType: 'json',
                data: { 'globalaccessInfo': Checkboxidsdata, 'Status': 2 },
                success: function (data) {
                    if (data == true) {
                        swal.fire({
                            title: "Saved Successfully!",
                            type: "success",
                            confirmButtonText: "Ok!"
                        }).then(function (e) {
                            if (e.value) {
                                location.reload();
                            }
                        });
                    }
                    else {
                        swal.fire("Error", "Something went wrong. Please try again later", "error");
                    }
                }
            });
        }
        else {
            swal.fire("Select Checkbox!", "", "warning");
        }
    });
    $('#EnableglobalId').on('click', function () {
        var atLeastOneIsChecked = $('input[name="Checkglobal[]"]:checked').length;
        if (atLeastOneIsChecked > 0) {
            var checkboxdata = [];
            var inputElements = document.getElementsByClassName('globalmessageCheckbox');
            for (var i = 0; inputElements[i]; ++i) {
                if (inputElements[i].checked) {
                    checkedValue = inputElements[i].value;
                    checkboxdata.push(checkedValue);
                }
            }
            var Checkboxidsdata = {
                "Checkboxid": checkboxdata
            };
            $.ajax({
                url: '/User/globalAccessStatusChange/',
                type: 'Post',
                dataType: 'json',
                data: { 'globalaccessInfo': Checkboxidsdata, 'Status': 1 },
                success: function (data) {
                    if (data == true) {
                        swal.fire({
                            title: "Saved Successfully!",
                            type: "success",
                            confirmButtonText: "Ok!"
                        }).then(function (e) {
                            if (e.value) {
                                location.reload();
                            }
                        });
                    }
                    else {
                        swal.fire("Error", "Something went wrong. Please try again later", "error");
                    }
                }
            });
        }
        else {
            swal.fire("Select Checkbox!", "", "warning");
        }
    });
    $('#WhoisIp').on('keyup', function () {
        $.ajax({
            url: '/User/ValidateWebAccessIP/',
            type: 'Post',
            dataType: 'json',
            data: { 'WhoisIp': $('#WhoisIp').val()},
            success: function (data) {
                if (data == true) {
                    $('#WhoisIpValidate_Err').css("display", "block");
                    $('#btnSubmitId').prop('disabled', true);
                }
                else {
                    $('#WhoisIpValidate_Err').css("display", "none");
                    $('#btnSubmitId').prop('disabled', false);
                }
            }
        });
    });
    $('#GlobalIp').on('keyup', function () {
        $.ajax({
            url: '/User/ValidateGlobalIp/',
            type: 'Post',
            dataType: 'json',
            data: { 'GlobalIP': $('#GlobalIp').val() },
            success: function (data) {
                if (data == true) {
                    $('#GlobalIpValidate_Err').css("display", "block");
                    $('#btnGlobalIpSubmitId').prop('disabled', true);
                }
                else {
                    $('#GlobalIpValidate_Err').css("display", "none");
                    $('#btnGlobalIpSubmitId').prop('disabled', false);
                }
            }
        });
    });
})();