$(function () {
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
                }
            });
        }
    });

    //END OF EMPLOYEE FUNCTIONS
});
