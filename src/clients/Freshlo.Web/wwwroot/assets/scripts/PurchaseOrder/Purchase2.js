!(function () {
    $(document).ready(function () {
        try {
            var currentDate = new Date();
            currentDate.setDate(currentDate.getDate() + 1);
            var day = currentDate.getDate();
            var month = currentDate.getMonth() + 1;
            var year = currentDate.getFullYear();
            $('#deliverydate').val(year + "/" + month + "/" + day + " - " + year + "/" + month + "/" + day);
        } catch (e) {}
    });
    $(function () {
        $('.datetimerange').daterangepicker({
            "showDropdowns": true,
            "showWeekNumbers": true,
            "timePicker": false,
            //"timePickerIncrement": 15,
            ranges: {
                'Tomorrow': [moment().add(1, 'day'), moment().add(1, 'day')],
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                //'Tomorrow': [moment().add(1, 'days'), moment().add(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                'This week': [moment().startOf('week'), moment().endOf('week')],
                'Next 7 days': [moment(), moment().add(6, 'days')]
            },
            "locale": {
                "format": "YYYY/MM/DD",
                "separator": " - ",
                "applyLabel": "Apply",
                "cancelLabel": "Cancel",
                "fromLabel": "From",
                "toLabel": "To",
                "customRangeLabel": "Custom",
                "weekLabel": "W",
                "daysOfWeek": [
                    "Su",
                    "Mo",
                    "Tu",
                    "We",
                    "Th",
                    "Fr",
                    "Sa"
                ],
                "monthNames": [
                    "January",
                    "February",
                    "March",
                    "April",
                    "May",
                    "June",
                    "July",
                    "August",
                    "September",
                    "October",
                    "November",
                    "December"
                ],
                "firstDay": 1
            },
            "linkedCalendars": false,
            "alwaysShowCalendars": false,
            //"startDate": "01/05/2018",
            //"endDate": "31/05/2018"
        }, function (start, end, label) {
            console.log('New date range selected: ' + start.format('YYYY/MM/DD') + ' to ' + end.format('YYYY/MM/DD') + ' (predefined range: ' + label + ')');
        });
        $.ajax({
            url: '/PurchaseOrder/_AppendPurchase?datefrom=&dateto=&CreateFor=',
            type: 'GET',
            success: function (data) {
                if (data.ItemId === null) {
                    swal("Oops!", "No sales order on this date", "");
                }
                if (data !== "") {
                    $('#Itemtbl_Body').html(data);
                    $('#ProductTblId').dataTable();
                }
            }
        });
    });
    $('#SearchQuantity').select2({
        containerCssClass: "select2-data-ajax",
        placeholder: 'Select Product',
        ajax: {
            url: '/Purchase/ItemSelectList',
            dataType: "json",
            delay: 250,
            data: function (e) {
                return {
                    name: e.term,
                    page: e.page
                };
            },
            processResults: function (data, t) {
                var results = [];
                for (i = 0; i < data.Data.length; i++) {
                    results.push({
                        id: data.Data[i].itemId,
                        text: data.Data[i].pluName,
                        measurement: data.Data[i].measurement,
                        weight: data.Data[i].weight,
                        sellingPrice: data.Data[i].sellingPrice,
                        category: data.Data[i].category,
                        sellingPrice: data.Data[i].sellingPrice,
                        itemType: data.Data[i].itemType,
                        CategoryName: data.Data[i].categoryName,
                        MarketPrice: data.Data[i].marketPrice,
                        itemstock: data.Data[i].totalStock,
                        stockId: data.Data[i].stockId,
                        Purchaseprice: data.Data[i].purchaseprice,
                        DiscountPerctg: data.Data[i].discountPerctg,
                        ProfitMargin: data.Data[i].profitMargin,
                        Approval: data.Data[i].approval
                    });
                }
                return {
                    results: results,
                    pagination: {
                        more: false
                    }
                };
            },
            cache: !0
        },
        escapeMarkup: function (e) {
            return e;
        },
        minimumInputLength: 1
    });
    $.selector_cache('#SearchQuantity').on('change', function (e) {
        var test = $(this).select2('data');
        var parameter = test[0].id;
        var name = test[0].text;
        var weight = test[0].weight;
        var measure = test[0].measurement;
        var purchasePrice = test[0].Purchaseprice;
        var sellingPrice = test[0].sellingPrice;
        var marketPrice = test[0].MarketPrice;
        var itemType = test[0].itemType;
        var weight = test[0].weight;
        var profitMargin = test[0].ProfitMargin;
        var approval = test[0].Approval;
        var categoryName = test[0].CategoryName;
        var itemstock = test[0].itemstock
        //////////////////////////////////
        if (itemType == "Packed") {
            $('#spnProductMeas').text(+weight + " " + measure + " X Rs. " + sellingPrice + "");
        }
        else { $('#spnProductMeas').text("" + weight + " " + measure + " X Rs.  " + sellingPrice + ""); }
        $('#measure').text("" + measure + "");
        $('#purchasePrice').prop('value', purchasePrice);
        $('#sellingPrice').prop('value', sellingPrice);
        $('#marketPrice').prop('value', marketPrice);
        $('#itemId').text(parameter);
        $('#productName').text(name);
        $('#profitPer').prop('value', profitMargin);
        $('#itemWight').prop('value', weight);
        $('#categoryName').prop('value', categoryName);
        $('#itemstock').prop('value', itemstock);
        //////////////////////////////////
        if (approval == "Pending") {
            swal({
                title: "Item is Unapproved!",
                text: "Would u like to Approve it?",
                type: "warning",
                showCancelButton: !0,
                confirmButtonText: "Yes!"
            }).then(function (e) {
                if (e.value) {
                    $.ajax({
                        url: '/ItemMaster/ApprovetheItem',
                        type: 'POST',
                        dataType: "json",
                        data: {
                            "itemId": parameter,
                            "approval": 'Approved'
                        },
                        success: function (data) {
                            if (data.IsSuccess == true) {
                                swal.fire("Item Approved", "", "success");
                            }
                            else swal.fire("error", "Fail to Approve!", "error");
                        }
                    });
                }
            });
        }
    });
    function TotalSum() {
        var total_Qty = 0;
        $.makeArray($('#ProductTblId  tr[id]').map(function () {
            var Qty = $('#ttlQty_' + this.id).val();
            total_Qty += parseFloat(Qty);
        }));
        var items = $("table[id='ProductTblId'] > tbody > tr").length
        $('#TotalItem').val(items);
        $('#TotalQuantity').val(total_Qty);
    }

    $('#btn_SaveCon').on('click', function () {
        var error = 0;
        if ($('#deliverydate').val().trim().length < 1 || $('#deliverydate').val() === "") {
            $('#Spndate').html("This field required").css('display', 'block');
            error++;
        }
        if ($('#CreateFor').val() === null) {
            $('#SpnOrderFor').html("This field required").css('display', 'block');
            error++;
        }
        var deliverydate = $('#deliverydate').val();
        var dataArray = deliverydate.split('-');
        var datefrom = dataArray[0];
        var dateto = dataArray[1];
        var quan;
        var i = 0;
        var CreateFor = $('#CreateFor').val();
        if (error == 0)
        {
            $.ajax({
                url: '/PurchaseOrder/_AppendPurchase?datefrom=' + datefrom + '&dateto=' + dateto + '&CreateFor=' + CreateFor,
                type: 'GET',
                success: function (data) {

                    if (data.ItemId === null) {
                        swal("Oops!", "No sales order on this date", "");
                    }
                    if (data !== "") {
                        $('#Itemtbl_Body').html(data);
                        $('#ProductTblId').dataTable();
                        TotalSum();
                    }
                }
            });
        }
        var wizard = $('#wizadP').text();
        var procurement = $('#procuremntId').prop('value');
        var hub = $("#hubId option:selected").text();
        var TotalQuantity = $('#TotalQuantity').prop('value');
        var TotaItem = $('#TotalItem').prop('value');
        var vendorId = $("#vendorId option:selected").text();
        var proc_date = $('#m_datetimepicker_6').prop('value');
        var deleiverydate = $('#deliverydate').prop('value');
        var CreateFor = $('#CreateFor').val();
        if (wizard == 2)
        {
            $('#h_Procument').text(": " + procurement);
            $('#h_Hub').text(": " + hub);
            $('#h_TotalQty').text(": " + TotalQuantity);
            $('#h_TotalItem').text(": " + TotaItem);
            $('#h_vendor').text(": " + vendorId);
            $('#h_Procdate').text(": " + proc_date);
            $('#h_delDate').text(": " + deleiverydate);
            $('#h_CreateFor').text(": " + CreateFor);
        }
    });

    $.selector_cache('#addItemBtn').on('click', function () {
        var id = $('#itemId').text();
        var purchasePrice = $('#purchasePrice').prop('value');
        var Product = $('#productName').text();
        var total_Qty = $("#ttl_Qty").prop('value');
        var categoryName = $('#categoryName').prop('value');
        var itemstock = $('#itemstock').prop('value');
        var html = "";
        if (id != "") {
            $('#Itemtbl_Body').append('<tr id=' + id + '>' +
                '<td id="pluName_' + id + '">' +
                '<a href="#" class="text-link">' + Product + '</a>' +
                '</td>' +
                '<td id="categoryName_' + id + '">' + categoryName + '</td>' +
                '<td>' +
                '<input type="text" value="' + purchasePrice + '" id="purPrice_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + total_Qty + '" id="ttlQty_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + itemstock + '" id="itemstock_' + id + '" class="form-control m-input m-input--solid" style="width:150px;">' +
                '</td>' +
                '<td>' +
                '<a href="#" onclick=remove(\'' + id + '\')><i class="flaticon-delete text-danger" ></i></a>' +
                '</td>' +
                '<td>' +
                '<input type="text" value="' + categoryName + '" id="categoryName_' + id + '" class="form-control m-input m-input--solid" style="width:150px;display:none">' +
                '</td>' +
                '</tr>');
            $('#Itemtbl_Body').html(data);
            $('#ProductTblId').dataTable();
            $('.clearitem').val('');
            $('.clearitem').text('');
            TotalSum();
        }
    });

    $('#submitId').on('click', function () {
        TotalSum();
        var procurement = $('#procuremntId').prop('value');
        var hub = $('#hubId').prop('value');
        var TotalQuantity = $('#TotalQuantity').prop('value');
        var TotaItem = $('#TotalItem').prop('value');
        var vendorId = $("#vendorId").prop('value');
        var proc_date = $('#m_datetimepicker_6').prop('value');
        var deleiverydate = $('#deliverydate').prop('value');
        var deliverydate = $('#deliverydate').val();
        var dataArray = deliverydate.split('-');
        var datefrom = dataArray[0];
        var dateto = dataArray[1];
        var CreateFor = $('#CreateFor').prop('value');
        var MapArr = [];
        $.each($('#Itemtbl_Body tr[id]').map(function () {
            var Array = {
                ItemId: this.id,
                pluName: $('#pluName_' + this.id).text(),
                categoryName: $('#categoryName_' + this.id).val(),
                TotalQuantity: $('#ttlQty_' + this.id).val(),
                purchasePrice: $('#purPrice_' + this.id).val(),
                currentsock: $('#itemstock_' + this.id).val()
            };
            MapArr.push(Array);
        }));
        var dicData = {
            Procurement_Type: procurement,
            Branch: hub,
            TotalQuantity: TotalQuantity,
            purchasePrice: purchasePrice,
            Plu_Count: TotaItem,
            Vendor: vendorId,
            proc_date: proc_date,
            deleiverydate: deleiverydate,
            List: MapArr
        };
        var purchslist = MapArr
        $.ajax({
            url: '/PurchaseOrder/_AppendPurchase?datefrom=' + datefrom + '&dateto=' + dateto + '&CreateFor=' + CreateFor + '&purchslist=' + purchslist,
            type: 'GET',
            data: dicData,
            success: function (data) {
                if (data.IsSuccess == true) {
                    alert('Hi');
                }
                else {
                    swal.fire('Oops!', 'Something went wrong!', 'warning');
                }
            }
        });
    });
    function remove(id) {
        $('#' + id).remove();
        $('#ProductTblId').dataTable();
        TotalSum();
        swal.fire('success', 'One item removed!', 'success');
    }
    $('#btnBack').on('click', function () {
    });
})(jQuery);


