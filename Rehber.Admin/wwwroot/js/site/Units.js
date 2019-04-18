$(function () {
    getHire();

    /* start=> Add Unit*/
    $('#btn-addUnit').click(function () {
        $('#form').trigger('reset');
        $('#addUnitModel').modal();
    });

    // checkUnit checkBox
    $('#checkUnit').on('change', function () {
        if (this.checked) {
            $("#jqxWidget2").jqxTree('selectItem', null);
            $('.form-check-label').css({ "color": "black", "font-weight": "bold" });
            $('#selectedParentUnit').val('');
        }
        else {
            $('.form-check-label').css({ "color": "black", "font-weight": "normal" });
        }

    });

    // check Treeview item  for adding model
    $('#jqxWidget2').on('select', function (event) {
        var args = event.args;
        var item = $('#jqxWidget2').jqxTree('getItem', args.element);
        $("#checkUnit").prop("checked", false);
        $('.form-check-label').css({ "color": "black", "font-weight": "normal" });
        $('#selectedParentUnit').val(item.label);

    });
    var inputUnitName = $('#inputUnitName');

    //submit add unit
    $('#submitAddUnit').click(function () {
        if (inputUnitName.val() == '') {
            swal('Birim Adı giriniz');
        }
        else if ($('#checkUnit').prop('checked') == false && $('#selectedParentUnit').val() == '') swal('hangi alan altında veya bağımsız birim secğiniz seciniz ');
        else {
            $.ajax({
                type: "POST",
                url: "/Unit/AddUnit?unitName=" + inputUnitName.val() + "&parentUnit=" + $('#selectedParentUnit').val(),
                success: function (unit) {
                    swal("Success!..");
                    $("#addUnitModel").modal("hide");
                    getHire();
                }
            });
        }
    });

    /* end=> Add Unit*/

    /*start=>Delete Unit*/
    $('#btnDeleteUnit').click(function () {

        var item = $('#jqxWidget').jqxTree('getSelectedItem');
        if (item === null || item.label === '') {
            swal('Önce silecek Birim Seçiniz');
        }
        else {
            $('#confirmDeleteUnitName').text(item.label).css('color', 'red');
            $('#deleteModel').modal();
        }
    });

    $('#btnConfirmDelete').click(function () {
        var item = $('#jqxWidget').jqxTree('getSelectedItem');
        $.ajax({
            type: "POST",
            url: "/Unit/DeleteUnit?unitId=" + item.id,
            success: function (result) {
                getHire();
                swal('birim Silindi !');
                $("#deleteModel").modal("hide");
                $("#jqxWidget").jqxTree('selectItem', null);

            }
        });
    });

    /*end=>Delete Unit*/


    /*Start=> EditUnit*/
    // check Edit Unit checkBox
    $('#edtCheckUnit').on('change', function () {
        if (this.checked) {
            $("#jqxWidget3").jqxTree('selectItem', null);
            $('.form-check-label').css({ "color": "black", "font-weight": "bold" });
            $('#edtSelectedParentUnit').val('');
        }
        else {
            $('.form-check-label').css({ "color": "black", "font-weight": "normal" });
        }

    });
    //check tree view for edit model
    $('#jqxWidget3').on('select', function (event) {
        var args = event.args;
        var item = $('#jqxWidget3').jqxTree('getItem', args.element);
        $("#edtCheckUnit").prop("checked", false);
        $('.form-check-label').css({ "color": "black", "font-weight": "normal" });
        $('#edtSelectedParentUnit').val(item.label);

    });


    $('#btnEditUnit').click(function () {
        var selectedItem = $('#jqxWidget').jqxTree('getSelectedItem');
        if (selectedItem === null || selectedItem.label === "") swal('Düzenlenecek Birimi Seçiniz');
        else {
            $.ajax({
                type: "Post",
                url: "/Unit/GetUnitById?unitId=" + selectedItem.id,
                success: function (data) {
                    $('#edtInputUnitName').val(data.unitName);
                    if (data.parentId == null) {
                        $("#edtCheckUnit").prop("checked", true);
                        $('#edtSelectedParentUnit').attr('parentId', "");
                    }

                    else {

                        $('#edtSelectedParentUnit').val(data.parentName);
                        $('#edtSelectedParentUnit').attr('parentId', data.parentId);
                        $("#edtCheckUnit").prop("checked", false);
                    }

                }
            });
            $('#edtUnitModel').modal();
        }
    });

    $('#submitEdtUnit').click(function () {
        var parent = $('#jqxWidget3').jqxTree('getSelectedItem');
        var selectedItemToEdit = $('#jqxWidget').jqxTree('getSelectedItem');
        var unitViewModel =
        {
            unitId: selectedItemToEdit.id,
            unitName: $('#edtInputUnitName').val(),
            parentId: "",
            parentName: $('#edtSelectedParentUnit').val()
        };
        console.log(parent);
        if (parent != null) unitViewModel.parentId = parent.id;

        console.log(JSON.stringify(unitViewModel));
        $.ajax({
            type: "Post",
            url: "/Unit/SaveEditing",
            data: JSON.stringify(unitViewModel),
            contentType: "application/json; charset=utf-8",
            success: function (unit) {
                swal("Success!..");
                getHire();
                //console.log(unit);
                $("#edtUnitModel").modal("hide");
            }
        });
    });




    /*End=> EditUnit*/


    // Unslect tree item and collapseAll
    $('#resetTree').click(function () {
        var item = $('#jqxWidget').jqxTree('getItem', args.element);
        $("#jqxWidget").jqxTree('selectItem', null);
        $('#jqxWidget').jqxTree('collapseAll');
        item.label = "";

    });

    //  show note when hover
    $('#resetTree').hover(function () {
        $('#noteText').css('display', 'block');
    }, function () {
        $('#noteText').css('display', 'none');
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

        $('#jqxWidget').jqxTree({ source: records, width: '500px' });
        $('#jqxWidget2').jqxTree({ source: records, width: '500px' });
        $('#jqxWidget3').jqxTree({ source: records, width: '500px' });

        //var items = $('#jqxWidget2').jqxTree('getItems');
        //$.each(items, function (index, item) {
        //    $(item.element).attr('nodeKey', item.value);
        //});
    }

});