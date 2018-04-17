$(document).ready(function () {
    
    var deckBuilderCards = [];
    var firstShow = true;
    var currentDeck = "";
    
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
        fetchWithAuth("http://" + window.location.hostname + ":5000/api/UserDeck/deck/" + name).then(function (response) { 
            response.json().then(function (value) {
                console.log(value);
                
                var table = buildTableFromDeck(value);
                
                reloadDeckTable(table);
                
                currentDeck = name;
            }).catch(function (reason) {
                swal("Error", "Couldn't load the contents of the deck " + name + "\nError: " + reason, "error");
            })
        }).catch(function (reason) { 
            swal("Error", "Couldn't get the contents of the deck " + name + "\nError: " + reason, "error");
        });
    }
    
    function reloadDeckTable(cards) {
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
                field: 'name',
                title: 'Name',
                class: 'name-style',
                align: 'center',
                halign: 'center',
                searchable: true
            }, {
                field: 'amount',
                title: 'Amount',
                align: 'center',
                halign: 'center'
            }],
            data: cards
        });
        function showImageOnHover() {
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

        showImageOnHover();

        //Make sure WE ALWAYS SHOW THE LIGHT
        $('#table').on('post-body.bs.table', showImageOnHover);
    }
    
    function checkCompatiblitiy() {
        var nameArray = [];
        $("[data-added='added']").each(function () {
            var name = $(this).attr('data-name');
            nameArray.push(name);
        });
        
        
        postWithAuth("http://" + window.location.hostname + ":5000/api/decks/search", nameArray).then(function (value) {
            $("[data-added='added']").each(function () {
                var name = $(this).attr('data-name');

                if (value.indexOf(name) == -1) {
                    $(this).addClass('bad');
                }
            });
        });
    }
    
    function addCard(nameArray, cardCountCache) {
        var postData = nameArray;
        
        postWithAuth('http://' + window.location.hostname + ':5000/api/card/names', postData).then(function (value) {
            value.forEach(function (card) { 
                card.amount = cardCountCache[card.name];
            });
            
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
        
        fetchWithAuth("http://" + window.location.hostname + ":5000/api/decks/all").then(function (result) {
            if (result.status == 500) {
                swal("There are no meta decks!");
                return;
            }

            result.json().then(function (data) {
                var list = $('#metaDecksPick');
                var myList = $('#metaDecksAdded');

                $('#addMeta').off('click');

                $('#removeMeta').off('click');

                $('#metaSearch').off('input');

                $('#metaAddedSearch').off('input');

                $('#completeDeck').off('click');

                $('#clearAll').off('click');
                
                list.empty();
                myList.empty();
                
                deckBuilderCards = [];
                
                var selectedLi = null;
                var deckCache = {};
                var cardCountCache = {};
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

                    var temp = {};

                    for (var name in value.cards) {
                        if (value.cards.hasOwnProperty(name)) {
                            temp[name] = value.cards[name].count;
                        }
                    }
                    
                    cardCountCache[value.name] = temp;
                });
                
                $('#addMeta').click(function () {
                    if (selectedLi != null) {
                        var state = selectedLi.attr('data-added');
                        
                        if (state === 'not-added') {
                            myList.append(selectedLi);
                            selectedLi.attr('data-added', 'added');
                            var deckName = selectedLi.attr('data-name');
                            
                            addCard(deckCache[deckName], cardCountCache[deckName]);
                            checkCompatiblitiy();
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
                            checkCompatiblitiy();
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

                    putWithAuth("http://" + window.location.hostname + ":5000/api/UserDeck/deck", name).then(function (value) {
                        var i = deckBuilderCards.length;
                        while (i--) {
                            var putData = { DeckName: name,  CardName: deckBuilderCards[i].name };
                            var temp = i;
                            var count = deckBuilderCards[i].amount;
                            
                            while (count--) {
                                putWithAuth("http://" + window.location.hostname + ":5000/api/UserDeck/deck/card", putData).catch(function (reason) {
                                    swal("Error Adding Card", "Failed to add the card " + deckBuilderCards[temp].name + " to the deck :(\nError: " + reason, "error");
                                });
                            }
                        }
                        
                        swal("Deck Created!", "The deck " + name + " has been created successfully!", "success");
                        
                        reloadDeckList();
                    }).catch(function (reason) {
                        swal("Deck Creation Failed", "Failed to create the deck :(\nError: " + reason, "error");
                    });
                });
                
                $('#clearAll').click(function () {
                    startDeckBuilding(); //Recall function will clear everything
                    deckBuilderCards = [];
                    reloadDeckTable(deckBuilderCards);
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
        fetchWithAuth("http://" + window.location.hostname + ":5000/api/UserDeck/all").then(function (result) {
            if (result.status == 500) {
                swal("You don't have any decks!")
                return;
            }

            result.json().then(function (data) {
                var list = $("#deckList");

                list.empty();
                
                var firstLi = null;
                
                data.forEach(function (value) { 
                  var newLi =  $("<li />").append(
                                  $("<a />").text(value.deckName)
                              ); 
                    
                   list.append(newLi);
                   
                   newLi.click(function () {
                       if (deckBuilding || firstShow) {
                           var html2 = $('#deckView').html();

                           $('.deck-card').html(html2);

                           deckBuilding = false;
                           firstShow = false;
                       }
                       
                       if (selectedDeckLi != null) {
                           selectedDeckLi.removeClass("selected");
                       }
                       
                       $(this).addClass("selected");
                       
                       selectedDeckLi = $(this);
                       
                       fetchAndLoadDeck(value.deckName);
                       
                       $('#deckName').text(value.deckName);
                   });
                   
                   if (firstLi == null) {
                       firstLi = newLi;
                   }
                });

                var newLi =  $("<li />").append(
                    $("<a />").text("Add Deck")
                );

                list.append(newLi);
                
                newLi.click(function () {
                    if (!deckBuilding) {
                        if (selectedDeckLi != null) {
                            selectedDeckLi.removeClass("selected");
                        }

                        $(this).addClass("selected");
                        
                        selectedDeckLi = $(this);
                        
                        startDeckBuilding();
                        
                        deckBuilding = true;
                    }
                });
                
                if (data.length == 0)
                    newLi.click();
                else
                    firstLi.click();
                   
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

        fetchWithAuth("http://" + window.location.hostname + ":5000/api/card/search/" + value).then(function (response) {
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
            var i = nameArray.length;
            while (i--) {
                var putData = { DeckName: currentDeck,  CardName: nameArray[i] };
                
                putWithAuth("http://" + window.location.hostname + ":5000/api/UserDeck/deck/card", putData).catch(function (reason) {
                    swal("Error", "Error adding " + putData.CardName + "\nError: " + reason,  "error");
                });
            }
            
            swal("Deck Updated", nameArray.length + " cards have been added!", "success");
        }
    });
    
    $("#remove").click(function () {
        var selectedCards = $('#table').bootstrapTable('getSelections');

        console.log(selectedCards);

        var nameArray = [];

        selectedCards.forEach(function (value) {
            nameArray.push(value.name);
        });

        if (deckBuilding) {
            addCard(nameArray);
            swal(nameArray.length + " Cards Added", "One copy of each card has been added!", "success");
        } else {
            var i = nameArray.length;
            while (i--) {
                var putData = { DeckName: currentDeck,  CardName: nameArray[i] };

                deleteWithAuth("http://" + window.location.hostname + ":5000/api/UserDeck/deck/card", putData).catch(function (reason) {
                    swal("Error", "Error removing " + putData.CardName + "\nError: " + reason,  "error");
                });
            }

            swal("Deck Updated", nameArray.length + " cards have been removed!", "success");
        }
    });
});