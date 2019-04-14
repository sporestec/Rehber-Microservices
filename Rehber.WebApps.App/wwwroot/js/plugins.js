
$(function () {

    $("#txtArama").on('keyup', function () {
        
        var inputValue = $(this).val();
        if (inputValue.length >= 3) {
            $("#tableContent").empty();
            var selectedItem = $('#jqxTree').jqxTree('getSelectedItem');
            var requestForm =
            {
                employeeName: inputValue,
                unitId: selectedItem.id
            };

            console.log(requestForm);
            $.ajax({
                type: "POST",
                url: "/Home/GetEmployee",
                data: JSON.stringify(requestForm),
                contentType: "application/json; charset=utf-8",
                success: function (list) {
                    console.log(list);
                    var SetData = $("#tableContent");
                    for (var i = 0; i < list.length; i++) {
                        var Data = "<tr class='row_" + list[i].employeeId + "'>" +
                            "<td>" + list[i].telephoneNumber +"</td>" +
                            "<td>" + list[i].firstName + " " + list[i].lastName+"</td>" +
                            "<td>" + list[i].extraInfo + "</td>" +
                            "<td>" + list[i].email + "</td>" +
                            "<td>" + list[i].unitId + "</td>" +
                            "</tr>";
                        SetData.append(Data);

                    }

                }
            });
            
           
           
        }
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

        //var items = $('#jqxWidget2').jqxTree('getItems');
        //$.each(items, function (index, item) {
        //    $(item.element).attr('nodeKey', item.value);
        //});


    }






    
})