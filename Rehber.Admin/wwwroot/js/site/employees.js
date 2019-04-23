﻿var pageNumber = 1;
var bulundu = false;

$(function () {
    GetEmployees(pageNumber);
    getHire();
    //EMPLOYEE FUNCTIONS
    $('#btn-addEmployee').click(function () {
        $('#newEmployeeForm').trigger('reset');
        $('#addEmployeeModel').modal();
    });



    function getHire() {
        var source =
        {
            datatype: "json",
            datafields: [
                { name: 'unitId' },
                { name: 'parentId' },
                { name: 'unitName' }
            ],
            id: 'unitId',
            url: '/Unit/GetAllUnit',
            async: false
        };

        // create data adapter.
        var dataAdapter = new $.jqx.dataAdapter(source);
        dataAdapter.dataBind();
        var records = dataAdapter.getRecordsHierarchy('unitId', 'parentId', 'items', [
            { name: 'unitName', map: 'label' },
            { name: 'unitId', map: 'id' },
            { name: 'parentId ', map: 'parentId' }
        ]);

        $('#jqxWidgetAddEmployee').jqxTree({ source: records, width: '500px' });
    }

    // check Treeview item for adding model
    $('#jqxWidgetAddEmployee').on('select', function (event) {
        var args = event.args;
        var item = $('#jqxWidgetAddEmployee').jqxTree('getItem', args.element);
        $('.form-check-label').css({ "color": "black", "font-weight": "normal" });
        $('#selectedParentUnit').val(item.label);
    });


    //submit add unit
    $('#submitAddEmployee').click(function () {
        var selectedUnit = $('#jqxWidgetAddEmployee').jqxTree('getSelectedItem');
        if ($('#inputEmployeeFirstName').val() === '')
            swal('Ad Bilgisi Zorunludur');
        else if (selectedUnit === null)
            swal('Birim Bilgisi Zorunludur.');
        else if ($('#inputEmployeeLastName').val() === '')
            swal('Soyad Bilgisi Zorunludur.');
        else {

            var employee = {
                firstName: $('#inputEmployeeFirstName').val(),
                lastName: $('#inputEmployeeLastName').val(),
                email: $('#inputEmployeeEmail').val(),
                telephoneNumber: $('#inputEmployeePhoneNumber').val(),
                webSite: $('#inputEmployeeWebsite').val(),
                unitId: selectedUnit.id,
                extraInfo: $('#inputEmployeeExtra').val()
            };

            $.ajax({
                type: "POST",
                url: "/Employee/AddEmployee",
                data: JSON.stringify(employee),
                contentType: "application/json",
                success: function (employee) {
                    swal("Başarıyla Eklendi..");
                    $("#addEmployeeModel").modal("hide");
                    getHire();
                },
                error: function () {
                    swal("Hata Oluştu Lütfen Tekrar Deneyiniz.")
                }
            });
        }
    });

    $("#lastPage").click(function () {
        if (pageNumber > 1) {
            pageNumber = pageNumber - 1;
        }
        GetEmployees(pageNumber);
    });

    $("#nextPage").click(function () {
        if (bulundu) {
            pageNumber = pageNumber + 1;
        }
        GetEmployees(pageNumber);
    });


    //END OF EMPLOYEE FUNCTIONS
});

//Functions
function deleteEmployee(id) {
    $.ajax({
        type: "DELETE",
        url: "/Employee/DeleteEmployee?id=" + id,
        success: function (result) {
            swal("Başarıyla Silindi..");
            var eTable = $("#employeesTable");
            eTable.find("tr:gt(0)").empty();
            $('#noResult').text("Yükleniyor.....");
            $('#noResult').show();
            setTimeout(() => {
                GetEmployees(pageNumber);
            }, 2500);

        },
        error: function () {
            swal("Hata Oluştu Lütfen Tekrar Deneyiniz.");
        }
    });
}

function GetEmployees(pageNumber) {
    var eTable = $("#employeesTable");
    eTable.find("tr:gt(0)").empty();
    $('#noResult').text("Yükleniyor.....");
    $('#noResult').show();
    $.ajax({
        type: "GET",
        url: "/Employee/GetWithFilter?pagesize=" + 100 + "&pagenumber=" + pageNumber,
        success: function (list) {
            $('#noResult').hide();
            if (list.length === 0) {
                $('#noResult').text("Kayıt Bulunmadı.....");
                $('#noResult').show();
                bulundu = false;
            }
            else {
                bulundu = true;
            }
            for (var i = 0; i < list.length; i++) {
                var row = "<tr class='row_" + list[i].employeeId + "'>" +
                    "<td>" + list[i].firstName + "</td>" +
                    "<td>" + list[i].lastName + "</td>" +
                    "<td>" + list[i].unitName + "</td>" +
                    "<td>" + list[i].telephoneNumber + "</td>" +
                    "<td>" + list[i].email + "</td>" +
                    "<td>" + list[i].webSite + "</td>" +
                    "<td>" + list[i].extraInfo + "</td>" +
                    "<td><a style='cursor:pointer'><i class='fa fa-edit'></i></a></td>" +
                    "<td><a onclick='deleteEmployee(" + list[i].employeeId + ");' style='cursor:pointer'><i class='fa fa-trash'></i></a></td>" +
                    "</tr>";
                eTable.append(row);
            }
        },
        error: function () {
            $('#noResult').text("Error: GetEmployees.....");
            $('#noResult').show();
        }
    });
}