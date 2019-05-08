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
        $("#employeeDataTabel").empty();
        getEmpolyeeData = function (employeeId) {

            $("#employeeDataTabel").empty();
            // Get Employee Info
            $.ajax({
                type: "Post",
                data: JSON.stringify(employeeId),
                contentType: "application/json",
                url: "/Home/GetEmployeeInfo/",
                success: function (employee) {
                    var table = "<tr>" +
                        "<td> Ad</td>" +
                        "<td>" + employee.firstName + "</td>" + "</tr>" +
                        "<tr>" +
                        "<td> Soyad</td>" +
                        "<td>" + employee.lastName + "</td>" + "</tr>" +
                        "<tr>" +
                        "<td> WebSite</td>" +
                        "<td>" + employee.webSite + "</td>" + "</tr>" +
                        "<tr>" +
                        "<td> Email</td>" +
                        "<td>" + employee.email + "</td>" + "</tr>" +
                        "<tr>" +
                        "<td> Telefon Numarası</td>" +
                        "<td>" + employee.telephoneNumber + "</td>" + "</tr>" +
                        "<tr>" +
                        "<td> Birim</td>" +
                        "<td>" + employee.unitName + "</td>" + "</tr>"
                    $("#employeeDataTabel").append(table);
                    $("#ModalBaslik").text(employee.firstName + " "+employee.lastName);
                }

            });
            //Get employee Foto
            $.ajax({
                type: "Post",
                data: JSON.stringify(employeeId),
                contentType: "application/json",
                url: "/Home/GetEmployeeFoto/",
                success: function (employeeFoto) {

                    //$('#photo').src('data:image/jpeg;base64,' + hexToBase64(''+employeeFoto.binaryData+''));
                    src = 'data:image/png;base64,' + btoa('' + employeeFoto.binaryData + '');
                    $("#imageDiv").prepend('<img id="photo" src="data: image / jpg; base64, ' + employeeFoto.binaryData + '"alt="" class= "pull-left" style = "margin: 0 20px 20px 0;width: 150px;" />');
                }
            });
            $('#exampleModalCenter').modal('show');
            $("#photo").remove();
        };
    }
});
