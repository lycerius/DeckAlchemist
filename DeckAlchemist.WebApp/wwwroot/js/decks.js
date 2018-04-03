$(document).ready(function () {
    
    function reloadSearchTable(cards) {
        $('#searchTable').bootstrapTable("destroy");
        $('#searchTable').bootstrapTable({
            clickToSelect: true,
            columns: [{
                field: 'state',
                checkbox: true
            }, {
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
            data: cards
        });
    }
    
    
    function fetchAndLoadDeck(name) {
        
    }
    
    function reloadDeckTable(cards) {
        $('#table').bootstrapTable("destroy");
        $('#table').bootstrapTable({
            clickToSelect: true,
            idField: 'id',
            columns: [{
                field: 'state',
                checkbox: true
            }, {
                field: 'available',
                title: 'Cards Available',
                align: 'center',
                halign: 'center'
            }, {
                field: 'totalAmount',
                title: 'Total Amount',
                align: 'center',
                halign: 'center'
            }, {
                field: 'name',
                title: 'Name',
                class: 'name-style',
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
                title: 'Colors',
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
                field: 'layout',
                title: 'Set',
                class: 'set-style',
                align: 'center',
                halign: 'center'
            }],
            data: cards
        });

        $('tr[id]').each(function (index) {
            var card = cards[index];

            $(this).mouseenter(function() {
                getCardImage(card.name).then(function(e) {
                    var src = e.normal;
                    $('#card-img').show().attr('src', src);
                });
            });
        });
    }

    var selectedDeckLi;
    
    function reloadDeckList() {
        fetchWithAuth("http://localhost:5000/api/decks/all").then(function (result) {
            if (result.status == 500) {
                swal("You don't have any decks!")
                return;
            }

            result.json().then(function (data) {
                var list = $("#deckList");

                list.empty();
                
                data.forEach(function (value) { 
                  var newLi =  $("<li />").append(
                                  $("<a />").text(value.name)
                              ); 
                    
                   list.append(newLi);
                   
                   newLi.click(function () {
                       
                       if (selectedDeckLi != null) {
                           selectedDeckLi.removeClass("selected");
                       }
                       
                       $(this).addClass("selected");
                       
                       selectedDeckLi = $(this);
                       
                       fetchAndLoadDeck(value.name);
                   });
                });
            }).catch(function (reason) {
                swal("Couldn't Create Decks", "There was a problem creating your decks :(\nError: " + reason, "error");
                reloadDeckTable({});
            });
        }).catch(function (reason) {
            swal("Couldn't Get Decks", "There was a problem getting your decks :(\nError: " + reason, "error");
            reloadDeckTable({});
        });
    }

    $('#card-img').hide();

    $(window).resize(function () {
        $('#table').bootstrapTable('resetView');
        $('#searchTable').bootstrapTable('resetView');
    });

    reloadDeckList();

    //var timerid;
    $('#search-btn').click(function () {
        var value = $('#card-name').val();

        fetchWithAuth("http://localhost:5000/api/card/search/" + value).then(function (response) {
            response.json().then(function (data) {
                reloadSearchTable(data);
            });
        });
    });

    $('#pushCards').click(function (e) {
        var selectedCards = $('#searchTable').bootstrapTable('getSelections');

        console.log(selectedCards);

        var nameArray = [];

        selectedCards.forEach(function (value) {
            nameArray.push(value.name);
        });

        var postData = nameArray;


        putWithAuth("http://localhost:5000/api/collection/cards", postData).then(function (value) {
            swal(nameArray.length + " Cards Added", "One copy of each card has been added!", "success");
            reloadCollection();
        });
    });

    $('#remove').click(function (e) {
        swal({
                title: "Are you sure?",
                text: "This will remove one instance of each selected card!\nWould you like to continue?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes, delete them!",
                closeOnConfirm: false
            },
            function() {
                var selectedCards = $('#table').bootstrapTable('getSelections');

                console.log(selectedCards);

                var nameArray = [];

                selectedCards.forEach(function (value) {
                    nameArray.push(value.name);
                });

                var postData = nameArray;


                deleteWithAuth("http://localhost:5000/api/collection/cards", postData).then(function (value) {
                    swal(nameArray.length + " Cards Removed", "One copy of each card has been removed!", "success");
                    reloadCollection();
                });
            }
        );
    });

    $('#uploadCards').click(function () {
        $('#uploadForm').submit();
    });

    $("#uploadForm").on( "submit", function( event ) {
        event.preventDefault();
        var form = $(this)[0];
        var postData = new FormData(form);
        console.log(postData);

        formWithAuth("http://localhost:5000/api/collection/csv", postData, "POST").then(function (value) {
            swal("Cards Imported", "The cards have been successfully imported!", "success");
            reloadCollection();
        });
    });
});