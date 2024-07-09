!(function () {
    var accesserrorid = $('#AccesserrorId').text();
    if (accesserrorid == '1') {
        swal.fire({
            title: "Access is denied from this " + $('#AccesserrorIP').text() + " Ip address",
            type: "Error",
            confirmButtonText: "Ok!"
        }).then(function (e) {
            if (e.value) {
                $('#AccesserrorId').text(0)
                //location.reload();
            }
        });
    }
    $.selector_cache('#m_login_forget_password').on('click', function (e) {
        window.location.href ="/Account/ForgotPassword/"
    });
    $.selector_cache('#formSubmit').on('click', function (e) {
        var numberOfErrors = 0;
        var lUserID = $.selector_cache('#EmailId').prop('value').trim();
        if (lUserID === "") {
            numberOfErrors++;
            $.selector_cache('#EmailId').addClass('error').attr("placeholder", "Please enter user id");
        }
        var lPassword = $.selector_cache('#Password').prop('value');
        if (lPassword) { }
        else {
            numberOfErrors++;
            $.selector_cache('#Password').addClass('error').attr("placeholder", "Please enter a password");
        }
        if (numberOfErrors === 0) {
            $.selector_cache('#loginForm').submit();
        }
    });     
})();