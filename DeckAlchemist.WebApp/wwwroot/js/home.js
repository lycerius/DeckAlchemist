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

    function reloadBorrowedTable(cards) {
        //Ensure old tables don't override image
        $('#borrowedTable').off('post-body.bs.table');
        $('#borrowedTable').bootstrapTable("destroy");
        $('#borrowedTable').bootstrapTable({
            clickToSelect: true,
            idField: 'id',
            search: true,
            columns: [{
                field: 'state',
                checkbox: true
            }, {
                field: 'amountBorrowed',
                title: 'Amount Borrowed',
                align: 'center',
                halign: 'center'
            }, {
                field: 'name',
                title: 'Name',
                class: 'name-style',
                align: 'center',
                halign: 'center',
                searchable: true
            }, {
                field: 'lender',
                title: 'Lender',
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

        function showImageOnHover() {
            $('#borrowedTable').children('tbody').children('tr[id]').each(function (index) {
                var card = cards[index];

                $(this).mouseenter(function () {
                    getCardImage(card.name).then(function (e) {
                        var src = e.normal;
                        $('#card-borrowed-img').show().attr('src', src);
                    });
                });
            });
        }

        showImageOnHover();

        //Make sure WE ALWAYS SHOW THE LIGHT
        $('#borrowedTable').on('post-body.bs.table', showImageOnHover);
    }

    function reloadLentTable(cards) {
        //Ensure old tables don't override image
        $('#lentTable').off('post-body.bs.table');
        $('#lentTable').bootstrapTable("destroy");
        $('#lentTable').bootstrapTable({
            clickToSelect: true,
            idField: 'id',
            search: true,
            columns: [{
                field: 'state',
                checkbox: true
            }, {
                field: 'name',
                title: 'Name',
                class: 'name-style',
                align: 'center',
                halign: 'center',
                searchable: true
            }, {
                field: 'lender',
                title: 'Lender',
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

        function showImageOnHover() {
            $('#lentTable').children('tbody').children('tr[id]').each(function (index) {
                var card = cards[index];

                $(this).mouseenter(function () {
                    getCardImage(card.name).then(function (e) {
                        var src = e.normal;
                        $('#card-lent-img').show().attr('src', src);
                    });
                });
            });
        }

        showImageOnHover();

        //Make sure WE ALWAYS SHOW THE LIGHT
        $('#lentTable').on('post-body.bs.table', showImageOnHover);
    }

    function reloadCollectionTable(cards) {
        //Ensure old tables don't override image
        $('#table').off('post-body.bs.table');
        $('#table').bootstrapTable("destroy");
        $('#table').bootstrapTable({
            clickToSelect: true,
            idField: 'id',
            search: true,
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
                halign: 'center',
                searchable: true
            }, {
                field: 'lendable',
                title: 'Lendable',
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

        function showImageOnHover() {
            $('#table').children('tbody').children('tr[id]').each(function (index) {
                var card = cards[index];

                $(this).mouseenter(function () {
                    getCardImage(card.name).then(function (e) {
                        var src = e.normal;
                        $('#card-img').show().attr('src', src);
                    });
                });
            });
        }

        showImageOnHover();

        //Make sure WE ALWAYS SHOW THE LIGHT
        $('#table').on('post-body.bs.table', showImageOnHover);
    }

    function reloadCollection() {
        fetchWithAuth("http://" + window.location.hostname + ":5000/api/collection").then(function (result) {
            if (result.status == 500) {
                swal("You don't have any cards!")
                return;
            }

            result.json().then(function (data) {
                var tableData = buildTableFromCollection(data);
                buildBorrowedTableFromCollection(data).then(function (borrowedData) {
                    reloadBorrowedTable(borrowedData);
                });
                
                var lentData = buildLentFromCollection(tableData);

                reloadCollectionTable(tableData);
                reloadLentTable(lentData);
            }).catch(function (reason) {
                swal("Collection Empty", "You don't have any cards :(\nAdd some using the \"Add Cards\" button!", "error");
                reloadCollectionTable({});
            });
        }).catch(function (reason) {
            swal("Couldn't Get Collection", "There was a problem getting your collection :(\nError: " + reason, "error");
            reloadCollectionTable({});
        });
    }

    $('#card-img').hide();

    $(window).resize(function () {
        $('#table').bootstrapTable('resetView');
        $('#searchTable').bootstrapTable('resetView');
    });

    reloadCollection();

    //var timerid;
    $('#search-btn').click(function () {
        var value = $('#card-name').val();

        fetchWithAuth("http://" + window.location.hostname + ":5000/api/card/search/" + value).then(function (response) {
            response.json().then(function (data) {
                reloadSearchTable(data);
            });
        });
    });

    /*$("#card-name").on('input', function (e) {
        var value = $(this).val();
        if($(this).data("lastval") != value) {

            $(this).data("lastval", value);
            clearTimeout(timerid);

            timerid = setTimeout(function () {
                fetchWithAuth("http://localhost:5000/api/card/search/" + value).then(function (response) {
                    response.json().then(function (data) {
                        reloadSearchTable(data);
                    });
                });
            }, 700);
        }
    });*/

    $('#pushCards').click(function (e) {
        var selectedCards = $('#searchTable').bootstrapTable('getSelections');

        console.log(selectedCards);

        var nameArray = [];

        selectedCards.forEach(function (value) {
            nameArray.push(value.name);
        });

        var postData = nameArray;


        putWithAuth("http://" + window.location.hostname + ":5000/api/collection/cards", postData).then(function (value) {
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
            function () {
                var selectedCards = $('#table').bootstrapTable('getSelections');

                console.log(selectedCards);

                var nameArray = [];

                selectedCards.forEach(function (value) {
                    nameArray.push(value.name);
                });

                var postData = nameArray;


                deleteWithAuth("http://" + window.location.hostname + ":5000/api/collection/cards", postData).then(function (value) {
                    swal(nameArray.length + " Cards Removed", "One copy of each card has been removed!", "success");
                    reloadCollection();
                });
            }
        );
    });

    $('#uploadCards').click(function () {
        $('#uploadForm').submit();
    });

    $("#uploadForm").on("submit", function (event) {
        event.preventDefault();
        var form = $(this)[0];
        var postData = new FormData(form);
        console.log(postData);

        formWithAuth("http://" + window.location.hostname + ":5000/api/collection/csv", postData, "POST").then(function (value) {
            swal("Cards Imported", "The cards have been successfully imported!", "success");
            reloadCollection();
        });
    });


    $("#lend").click(function () {
        var lendable = [];
        var notLendable = [];
        var selectedCards = $('#table').bootstrapTable('getSelections');

        if (selectedCards.length == 0) {
            swal("No Cards", "You must select at least one card to toggle!", "error");
            return;
        }

        selectedCards.forEach(function (value) {
            if (value.lendable) {
                notLendable.push({ lenable: false, cardName: value.name });
            } else {
                lendable.push({ lenable: true, cardName: value.name });
            }
        });

        var notLendableFunc = function (fromSwal) {
            if (notLendable.length > 0) {
                swal({
                    title: "Mark Not Lendable?",
                    text: "This will mark " + notLendable.length + " cards as not lendable.\nWould you like to continue?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Yes",
                    closeOnConfirm: true
                },
                    function () {
                        var postData = notLendable;

                        postWithAuth("http://" + window.location.hostname + ":5000/api/collection/mark", postData).then(function (value) {
                            reloadCollection();

                            if (fromSwal)
                                swal("Cards Marked", "A total of " + (lendable.length + notLendable.length) + " cards have been marked!", "success");
                            else
                                swal("Cards Marked", "" + (lendable.length + notLendable.length) + " cards have been marked not lendable!", "success");
                        });
                    }
                );
            } else if (fromSwal) {
                reloadCollection();
                swal("Cards Marked", "" + (lendable.length + notLendable.length) + " cards have been marked lendable!", "success");
            } else {
                swal("No Cards", "You must select at least one card to toggle!", "error");
            }
        };

        if (lendable.length > 0) {
            swal({
                title: "Mark Lendable?",
                text: "This will mark " + lendable.length + " cards as lendable.\nWould you like to continue?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                closeOnConfirm: true
            },
                function () {
                    var postData = lendable;

                    postWithAuth("http://" + window.location.hostname + ":5000/api/collection/mark", postData).then(function (value) {
                        notLendableFunc(true);
                    });
                }
            );
        } else {
            notLendableFunc(false);
        }
    });
    
    $('#return-cards').click(function () {
        var selectedCards = $('#borrowedTable').bootstrapTable('getSelections');
        
        if (selectedCards.length == 0) {
            swal("Select Cards", "Please select at least one borrowed card to return!", "error");
            return;
        }

        swal({
                title: "Return Card" + (selectedCards.length > 1 ? "s" : "" ) + "?",
                text: "This will return " + selectedCards.length + " card" + (selectedCards.length > 1 ? "s" : "" ) + " back to their original owner.\nWould you like to continue?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                closeOnConfirm: true
            },
            function () {
                var promiseArray = [];
                selectedCards.forEach(function (value) {
                    var postData = {
                        fromUser: value.lenderId,
                        cardName: value.name
                    };
                    promiseArray.push(
                        postWithAuth("http://" + window.location.hostname + ":5000/api/collection/lend/remove", postData)
                    );
                });

                Promise.all(promiseArray).then(function (value) {
                    swal("Returned", selectedCards.length +  " card" + (selectedCards.length > 1 ? "s" : "") + " returned!", "success");
                    
                    reloadCollection();
                }).catch(function (reason) {
                    swal("Couldn't Return Cards", "There was a problem returning the cards :(\nError: " + reason, "error");
                })
            }
        );
    });
});