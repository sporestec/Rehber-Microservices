
$(function () {
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