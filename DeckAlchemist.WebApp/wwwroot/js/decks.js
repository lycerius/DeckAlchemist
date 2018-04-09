$(document).ready(function () {
    
    var deckBuilderCards = [];
    
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
    
    function addCard(nameArray) {
        var postData = nameArray;
        
        postWithAuth('http://localhost:5000/api/card/names', postData).then(function (value) {
            deckBuilderCards = deckBuilderCards.concat(value);
            
            reloadDeckTable(deckBuilderCards);
        });
    }
    
    function removeCards(nameArray) {
        //kill me
        
        var i = nameArray.length;
        while (i--) {
            var z = deckBuilderCards.length;
            
            while (z--) {
                if (deckBuilderCards[z].name === nameArray[i]) {
                    deckBuilderCards.splice(z,  1);
                    break;
                }
            }
        }

        reloadDeckTable(deckBuilderCards);
    }
    
    function startDeckBuilding() {
        //<li><a href="#">Deck 1</a></li>
        
        fetchWithAuth("http://localhost:5000/api/decks/all").then(function (result) {
            if (result.status == 500) {
                swal("There are no meta decks!");
                return;
            }

            result.json().then(function (data) {
                var list = $('#metaDecksPick');
                var myList = $('#metaDecksAdded');
                
                list.empty();
                myList.empty();
                
                deckBuilderCards = [];
                
                var selectedLi = null;
                var deckCache = {};
                data.forEach(function (value) { 
                    var text = value.name + " (" + value.meta + "%)";
                    
                    var newLi = $("<li />").append(
                        $("<a />").text(text)
                    );
                    
                    newLi.attr('data-added', 'not-added');
                    newLi.attr('data-name', value.name);
                    
                    list.append(newLi);
                    
                    newLi.click(function () {
                        if (selectedLi != null) {
                            selectedLi.removeClass("selected");
                        }

                        $(this).addClass("selected");
                        
                        selectedLi = $(this);
                    });
                    
                    var nameArray = [];
                    for (var key in value.cards) {
                        if (value.cards.hasOwnProperty(key)) {
                            nameArray.push(value.cards[key].name);
                        }
                    }
                    
                    deckCache[value.name] = nameArray;
                });
                
                $('#addMeta').click(function () {
                    if (selectedLi != null) {
                        var state = selectedLi.attr('data-added');
                        
                        if (state === 'not-added') {
                            myList.append(selectedLi);
                            selectedLi.attr('data-added', 'added');
                            
                            addCard(deckCache[selectedLi.attr('data-name')]);
                        }
                    }
                });
                
                $('#removeMeta').click(function () {
                    if (selectedLi != null) {
                        var state = selectedLi.attr('data-added');

                        if (state === 'added') {
                            list.append(selectedLi);
                            selectedLi.attr('data-added', 'not-added');
                            
                            removeCards(deckCache[selectedLi.attr('data-name')]);
                        }
                    }
                });
                
                $('#metaSearch').on('input', function () {
                    var value = $(this).val().toLowerCase();
                    
                    if (value.length > 0) {
                        $('#metaDecksPick').find('li').each(function (idx, li) {
                            var cur = $(li);
                            
                            var name = cur.attr('data-name').toLowerCase();
                            
                            if (!name.includes(value)) {
                                cur.hide();
                            } else {
                                cur.show();
                            }
                        })
                    } else {
                        $('#metaDecksPick').find('li').show();
                    }
                });
                
                $('#metaAddedSearch').on('input', function () {
                    var value = $(this).val().toLowerCase();

                    if (value.length > 0) {
                        $('#metaDecksAdded').find('li').each(function (idx, li) {
                            var cur = $(li);

                            var name = cur.attr('data-name').toLowerCase();

                            if (!name.includes(value)) {
                                cur.hide();
                            } else {
                                cur.show();
                            }
                        })
                    } else {
                        $('#metaDecksAdded').find('li').show();
                    }
                });

                $('#completeDeck').click(function () {
                    var name = $('#deckBuilderName').val();

                    if (name === "Deck Name " || name === "") {
                        swal("No Name", "You must enter a name for the deck!", "error");
                        return;
                    }

                    putWithAuth("http://localhost:5000/api/UserDeck/deck", name).then(function (value) {
                        var i = deckBuilderCards.length;
                        while (i--) {
                            var putData = { DeckName: name,  CardName: deckBuilderCards[i].name };
                            putWithAuth("http://localhost:5000/api/UserDeck/deck/card", putData);
                        }
                    }).catch(function (reason) {
                        swal("Deck Creation Failed", "Failed to create the deck :(\nError: " + reason, "error");
                    });
                });
                
                $('#clearAll').click(function () {
                    startDeckBuilding(); //Recall function will clear everything
                })
            }).catch(function (reason) {
                swal("Couldn't start deck builder!", "There was a problem getting the meta decks :(\nError: " + reason, "error");
            });
        }).catch(function (reason) {
            swal("Couldn't start deck builder!", "There was a problem getting the meta decks :(\nError: " + reason, "error");
        });
        
        var html = $('#deckBuilder').html();
        $('.deck-card').html(html);
        
        
    }

    var selectedDeckLi;
    var deckBuilding = false;
    function reloadDeckList() {
        fetchWithAuth("http://localhost:5000/api/UserDeck/all").then(function (result) {
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
                       if (deckBuilding) {
                           var html2 = $('#deckView').html();

                           $('.deck-card').html(html2);

                           deckBuilding = false;
                       }
                       
                       if (selectedDeckLi != null) {
                           selectedDeckLi.removeClass("selected");
                       }
                       
                       $(this).addClass("selected");
                       
                       selectedDeckLi = $(this);
                       
                       fetchAndLoadDeck(value.name);
                   });
                });

                var newLi =  $("<li />").append(
                    $("<a />").text("Add Deck")
                );

                list.append(newLi);
                
                newLi.click(function () {
                    if (!deckBuilding) {
                        startDeckBuilding();
                        
                        deckBuilding = true;
                    }
                });
                
                if (data.length == 0)
                    newLi.click();
                
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

        if (deckBuilding) {
            addCard(nameArray);
            swal(nameArray.length + " Cards Added", "One copy of each card has been added!", "success");
        } else {
            console.log("Searching outside deckbuilder");
        }
    });
});