$(document).ready(function () {
    
    fetchWithAuth("http://localhost:5000/api/collection").then(function (result) {
        if (result.status == 500) {
            swal("You don't have any cards!")
            return;
        }
        
        result.json().then(function (tableData) {
            $('#table').bootstrapTable({
                columns: [{
                    field: 'id',
                    title: '#',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'name',
                    title: 'Name',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'ccost',
                    title: 'Converted Cost',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'fcost',
                    title: 'Full Cost',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'color',
                    title: 'Color',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'power',
                    title: 'Power',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'toughness',
                    title: 'Toughness',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'type',
                    title: 'Type',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'set',
                    title: 'Set',
                    class: 'set-style',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'rarity',
                    title: 'Rarity',
                    align: 'center',
                    halign: 'center'
                }, {
                    field: 'avalibility',
                    title: 'Avalibility',
                    align: 'center',
                    halign: 'center'
                }],
                data: tableData
            });
        
        });
    }).catch(function (reason) {
        swal("No Cards", reason, "error");
    });
    
    var timerid;
    $("#card-name").on('input', function (e) {
        var value = $(this).val();
        if($(this).data("lastval")!= value){

            $(this).data("lastval",value);
            clearTimeout(timerid);

            timerid = setTimeout(function() {
                fetchWithAuth("http://localhost:5000/api/card/search/" + value).then(function (response) {
                    response.json().then(function (data) {
                        $('#searchTable').bootstrapTable({
                            columns: [{
                                field: 'name',
                                title: 'Name',
                                align: 'center',
                                halign: 'center'
                            }, {
                                field: 'cmc',
                                title: 'Converted Cost',
                                align: 'center',
                                halign: 'center'
                            }, {
                                field: 'manaCost',
                                title: 'Full Cost',
                                align: 'center',
                                halign: 'center'
                            }, {
                                field: 'colors',
                                title: 'Color',
                                align: 'center',
                                halign: 'center'
                            }, {
                                field: 'power',
                                title: 'Power',
                                align: 'center',
                                halign: 'center'
                            }, {
                                field: 'toughness',
                                title: 'Toughness',
                                align: 'center',
                                halign: 'center'
                            }, {
                                field: 'types',
                                title: 'Type',
                                align: 'center',
                                halign: 'center'
                            }],
                            data: data
                        });
                    });
                });
            }, 700);
        }
        
    });

    $(window).resize(function () {
        $('#table').bootstrapTable('resetView');
    });
});