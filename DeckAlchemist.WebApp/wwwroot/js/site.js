promiseQueue = [];

/*
    * Fetches a resource from an endpoint that expects authorization.
    * Achieves this by automatically appending the firebase id token
    * for the currently signed in user to the request header. Returns a promise
    * .then(function(result)): result=response
    * .catch(function(error)): error = either firebase error or fetch error
    * Params: [url] = authenticated endpoint, [fetchProps] = optional properties to use with the fetch call
    * For more information on how to use fetch: "https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch"
    */
function fetchWithAuth(url, fetchProps = {}) {
    return new Promise(function (resolve, reject) {
        try {
            var u = firebase.auth().currentUser;
            
            var toRum = function doAuth(currentUser) {
                try {
                    currentUser.getIdToken(true).then(function (idToken) {
                        if (!fetchProps.headers) fetchProps.headers = {};
                        fetchProps.headers['Authorization'] = "Bearer " + idToken;
                        fetch(url, fetchProps).then(function (result) {
                            resolve(result);
                        }).catch(function (error) {
                            reject(error);
                        });
                    });
                } catch (error) {
                    reject(error);
                }
            };
            
            if (u == null) {
                promiseQueue.push(toRum);
            } else {
                toRum(u);
            }
            
            
        } catch (error) {
            reject(error);
        }
    });
}

function buildTableFromCollection(collection) {
    var cardInfo = collection.cardInfo;
    var owned = collection.userCollection.ownedCards;
    var borrowed = collection.userCollection.borrowedCards;
    
    var result = [];
    
    var id = 1;
    for (var name in owned) {
        if (owned.hasOwnProperty(name)) {
            /*
            available
            cardId
            inDecks
            lentTo
            totalAmount
             */
            var c = owned[name];
            
            /*
            cmc
            colors
            imageName
            layout
            legality
            manaCost
            name
            power
            subTypes
            text
            toughness
            type
            types
            _id
             */
            var info = cardInfo[c.cardId];
            
            var newCard = Object.assign({
                available: c.available,
                inDecks: c.inDecks,
                lentTo: c.lentTo,
                totalAmount: c.totalAmount,
                id: id
            }, info);
            
            result.push(newCard);
            id++;
        }
    }
    
    return result;
}

function formWithAuth(aUrl, aData, aType) {
    return new Promise(function (resolve, reject) {
        try {
            var u = firebase.auth().currentUser;

            var toRun = function (currentUser) {
                try {
                    currentUser.getIdToken(true).then(function (idToken) {
                        $.ajax({
                            type: aType,
                            url: aUrl,
                            beforeSend: function(request) {
                                request.setRequestHeader("Authorization", "Bearer " + idToken);
                            },
                            data: aData,
                            contentType: false,
                            processData: false,
                            success: function(data){
                                resolve(data);
                            },
                            error: function (xhr, textStatus, error) {
                                console.log(xhr.statusText);
                                console.log(textStatus);
                                console.log(error);
                                reject(xhr);
                            }
                        });
                    })
                } catch (error) {
                    reject(error);
                }
            };

            if (u == null) {
                promiseQueue.push(toRun);
            } else {
                toRun(u);
            }
        } catch (error) {
            reject(error);
        }
    });
}

function ajaxWithAuth(aUrl, aData, aType) {
    return new Promise(function (resolve, reject) {
        try {
            var u = firebase.auth().currentUser;
            
            var toRun = function (currentUser) {
                try {
                    currentUser.getIdToken(true).then(function (idToken) {
                        $.ajax({
                            type: aType,
                            contentType: "application/json",
                            url: aUrl,
                            beforeSend: function(request) {
                                request.setRequestHeader("Authorization", "Bearer " + idToken);
                            },
                            data: JSON.stringify(aData),
                            success: function(data){
                                resolve(data);
                            },
                            error: function (xhr, textStatus, error) {
                                console.log(xhr.statusText);
                                console.log(textStatus);
                                console.log(error);
                                reject(xhr);
                            },
                            traditional: true
                        });
                    })
                } catch (error) {
                    reject(error);
                }
            };
            
            if (u == null) {
                promiseQueue.push(toRun);
            } else {
                toRun(u);
            }
        } catch (error) {
            reject(error);
        }
    });
}

function postWithAuth(aUrl, aData) {
    return ajaxWithAuth(aUrl, aData, "POST");
}

function putWithAuth(aUrl, aData) {
    return ajaxWithAuth(aUrl, aData, "PUT");
}

function deleteWithAuth(aUrl, aData) {
    return ajaxWithAuth(aUrl, aData, "DELETE");
}

function getCardImage(cardName) {
    const scryImageSearchURI = "https://api.scryfall.com/cards/named?exact=";
    return new Promise(function (resolve, reject) {
        try {
            fetch(scryImageSearchURI+cardName).then(function(result) {
                return result.json()
            }).then(function(json){
                if (json.image_uris == null) {
                    resolve(json.card_faces[0].image_uris);
                } else {
                    resolve(json.image_uris)
                }
            }).catch(function(error){
                reject(error)
            })
        } catch(error) {
            reject(error)
        }
    });
}

function getGroups() {
    return new Promise(function (resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/user").then(function (result) {
            return result.json();
        }).then(function (json) {
            resolve(json.groups)
        }).catch(function (error) {
            reject(error)
        });
    })
}

function getAllUserGroups() {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/group/all").then(function(result) {
            return result.json();
        }).then(function(json){
            resolve(json)
        }).catch(function(error){
            reject(error);
        })
    })
}

function getGroupInfo(groupId) {
    return new Promise(function (resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/group/" + groupId).then(function (result) {
            return result.json();
        }).then(function(json){
            resolve(json);
        }).catch(function (error) {
            reject(error)
        })
    });
}

function getUserNamesByUserIds(userIds) {
    return new Promise(function(resolve, reject){
        fetchWithAuth("http://localhost:5000/api/user/names", 
            {
                method: "POST", 
                body: JSON.stringify(userIds), 
                headers: {
                    'content-type': "application/json"
                }
            }).then(function(result) {
            return result.json();
        }).then(function(json){
            resolve(json);
        }).catch(function(error){
            reject(error)
        })
    });
}

function createGroup(groupName) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/group/"+groupName+"/create", {method: "POST"}).then(function() {
            resolve()
        }).catch(function(error) {
            reject(error)
        })
    });
}

function sendUserMessage(message) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/message/send/user", 
            {
                method: "POST",
                body: JSON.stringify(message),
                headers: {
                    'content-type': "application/json"
                }
            }).then(function() {
            resolve()
        }).catch(function(error) {
            reject(error)
        })
    })
}

function sendGroupInvite(message) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/message/send/invite", 
            {
                method: "POST",
                body: JSON.stringify(message),
                headers: {
                    'content-type': "application/json"
                }
            }).then(function() {
            resolve()
        }).catch(function(error) {
            reject(error)
        })
    })
}

function getOwnedCardsForUser(userId) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/collection", {
        method: "POST",
        body: JSON.stringify(userId),
        headers: {
            'content-type': "application/json"
        }
    }).then(function(result){
        return result.json();
    }).then(function(json){
        resolve(json)
    }).catch(function(error){
        reject(error);
    })

    })

}

function acceptLoanRequest(messageId) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/message/accept/loan", {
            method: "POST",
            body: JSON.stringify(messageId),
            headers: {
                'content-type': "application/json"
            }  
        }).then(function(response) {
            resolve()
        }).catch(function(error){
            reject(error)
        })
    })
}

function acceptGroupInvite(messageId) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/message/accept/invite", {
            method: "POST",
            body: JSON.stringify(messageId),
            headers: {
                'content-type': "application/json"
            }  
        }).then(function(response) {
            resolve();
        }).catch(function(error){
            reject(error)
        })
    })
}

function sendLoanRequest(message) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/message/send/loan", {
             method: "POST",
             body: JSON.stringify(message),
             headers: {
                 'content-type': "application/json"
             }
        }).then(function() {
            resolve();
        }).catch(function(error){
            reject(error);
        })
    })
}

function getMessages() {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/message/all").then(function(response){
            return response.json();
        }).then(function(json){
            resolve(json);
        }).catch(function(error) {
            reject(error);
        })
    });
}


function getUserName(userId) {
    return new Promise(function(resolve, reject) {
        fetchWithAuth("http://localhost:5000/api/user/name/"+userId).then(function(result) {
            console.log(result)
            return result.text();
        }).then(function(json){
            resolve(json);
        }).catch(function(error){
            reject(error);
        })
    })
}

function authorizeOrLogin() {
    //if(firebase.auth().currentUser == null) window.location = "/"
}

$(document).ready(function(){
    "use strict";

    firebase.auth().onAuthStateChanged(function(user) {
        if (user) {
            //Flush
            promiseQueue.forEach(function (f) {
                f(user);
            })
        }
    });
});
