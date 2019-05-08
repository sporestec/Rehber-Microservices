$(function () {


    // input arama
    $("#txtArama").on('keyup', function () {
        if ($("#txtArama").val() === "") {

            $("#tableContent").empty();
        }
        else {
            arama();

        }
    });
    // selected birim
    $('#jqxTree').on('select', function (event) {
        arama();

    });

    // reset form
    $("#resetAll").click(function () {
        $("#tableContent").empty();
        $("#txtArama").val("");
        var item = $('#jqxTree').jqxTree('getItem', args.element);
        $("#jqxTree").jqxTree('selectItem', null);
        $('#jqxTree').jqxTree('collapseAll');
    });
    // hover reset and shw text
    $("#resetAll").hover(function () {
        $('.resetText').css('display', 'block');
    }, function () {
        $('.resetText').css('display', 'none');
    });


    getHire();
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
            url: '/Home/GetAllUnits',
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

        $('#jqxTree').jqxTree({ source: records, width: '500px' });

    }

    function arama() {
        var inputValue = $("#txtArama").val();
           
            $("#tableContent").empty();
            var selectedItem = $('#jqxTree').jqxTree('getSelectedItem');
            var requestForm =
            {
                employeeName: inputValue,
                unitId: selectedItem.id
            };
            // console.log(requestForm);
            $.ajax({
                type: "POST",
                url: "/Home/GetEmployee",
                data: JSON.stringify(requestForm),
                contentType: "application/json; charset=utf-8",
                success: function (list) {
                    if (list !== null) {
                        var SetData = $("#tableContent");
                        for (var i = 0; i < list.length; i++) {
                            var Data = "<tr onclick='getEmpolyeeData(" + list[i].employeeId + ")' class='row_" + list[i].employeeId + "'>" +
                                "<td>" + list[i].telephoneNumber + "</td>" +
                                "<td>" + list[i].firstName + " " + list[i].lastName + "</td>" +
                                "<td>" + list[i].extraInfo + "</td>" +
                                "<td>" + list[i].email + "</td>" +
                                "<td>" + list[i].unitName + "</td>" +
                                "</tr>";
                            SetData.append(Data);
                        }
                    }
                    else {
                        $("#tableContent").empty();
                    }

                }
            });


            getEmpolyeeData = function (employeeId) {
                $('#exampleModalCenter').modal('show');
            };

        




    }
});
