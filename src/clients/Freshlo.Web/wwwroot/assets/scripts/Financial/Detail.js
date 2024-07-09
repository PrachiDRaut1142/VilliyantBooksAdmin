!(function () {
    var $Entry_Type = $('#Entry_Type'),
        $InPaymentType = $('#InPaymentType'),
        $OutPaymentType = $('#OutPaymentType'),
        $Payment_Type = $('#Payment_Type'),
        $Other_Payment = $('#Other_Payment'),
        $Reference_No = $('#Reference_No'),
        $Received_From = $('#Received_From'),
        $Paid_To = $('#Paid_To'),
        $Payment_Status = $('#Payment_Status'),
        $Total_Amount = $('#Total_Amount'),
        $Partail_Amount = $('#Partail_Amount'),
        $Payment_Mode = $('#Payment_Mode'),
        $Paid_On = $('#Paid_On'),
        $Remark = $('#Remark'),
        $FinanceUpdateFrom = $('#FinanceUpdateFrom'),
        $nextBtnUpdateForm = $('#nextBtn');
    $nextBtnUpdateForm.on('click', function () {
        $FinanceUpdateFrom.submit();
    });
    $(document).ready(function () {
        if ($('#Payment_Status').prop('value') == 1)
        {
            $(":disabled", $('#Entry_Type').attr("disabled"), true);
            $(":disabled", $('#Payment_Type').attr("disabled"), true);
            $(":disabled", $('#Other_Payment').attr("disabled"), true);
            $(":disabled", $('#Reference_No').attr("disabled"), true);
            $(":disabled", $('#Received_From').attr("disabled"));
            $(":disabled", $('#Paid_To').attr("disabled"),true);
            $(":disabled", $('#Payment_Status').attr("disabled"),true);
            $(":disabled", $('#Total_Amount').attr  ("disabled"),true);
            $(":disabled", $('#Partail_Amount').attr("disabled"),true);
            $(":disabled", $('#Payment_Mode').removeAttr("disabled"));
            $(":disabled", $('#m_datetimepicker_8').attr("disabled"),true);
            $(":disabled", $('#Remark').attr("disabled"),true);
            $nextBtnUpdateForm.on('click', function () {
                $(":disabled", $('#Entry_Type').removeAttr("disabled"));
                $(":disabled", $('#Payment_Type').removeAttr("disabled"));
                $(":disabled", $('#Other_Payment').removeAttr("disabled"));
                $(":disabled", $('#Reference_No').removeAttr("disabled"));
                $(":disabled", $('#Received_From').removeAttr("disabled"));
                $(":disabled", $('#Paid_To').removeAttr("disabled"));
                $(":disabled", $('#Payment_Status').removeAttr("disabled"));
                $(":disabled", $('#Total_Amount').removeAttr("disabled"));
                $(":disabled", $('#Partail_Amount').removeAttr("disabled"));
                $(":disabled", $('#Payment_Mode').removeAttr("disabled"));
                $(":disabled", $('#m_datetimepicker_8').removeAttr("disabled"));
                $(":disabled", $('#Remark').removeAttr("disabled"));
                alert('Sorry You cannot Update Details Once Payment Fully Paid');
            });
        }
        if ($('#Payment_Status').prop('value') == 2 || $('#Payment_Status').prop('value') == 3) {
            $(":disabled", $('#Entry_Type').removeAttr("disabled"));
            $("select[name=Inward_Payment_Type]").removeAttr("disabled");
            $("select[name=Outward_Payment_Type]").removeAttr("disabled");
            $(":disabled", $('#Other_Payment').removeAttr("disabled"));
            $(":disabled", $('#Reference_No').removeAttr("disabled"));
            $(":disabled", $('#Received_From').removeAttr("disabled"));
            $(":disabled", $('#Paid_To').removeAttr("disabled"));
            $(":disabled", $('#Payment_Status').removeAttr("disabled"));
            $(":disabled", $('#Total_Amount').removeAttr("disabled"));
            $(":disabled", $('#Partail_Amount').removeAttr("disabled"));
            $(":disabled", $('#Payment_Mode').removeAttr("disabled"));
            $(":disabled", $('#m_datetimepicker_8').removeAttr("disabled"));
            $(":disabled", $('#Remark').removeAttr("disabled"));  
            $nextBtnUpdateForm.on('click', function () {
            alert('Financial Data Update Successfully');
            });
        }  
    });
})(jQuery);







 


