function fetchMessagesAndShow() {
    $("#message-body-content").empty();
    getMessages().then(function (messagesJson) {
        populateInbox(messagesJson);
    })
}

function populateInbox(messagesJson) {
    var inboxContainer = $("#inbox-content")
    var messageContainer = $("<div></div>")
    var list = $("<ul class='list-group'></ul>")
    $.each(messagesJson, function (index) {
        var message = messagesJson[index];
        var subject = message['subject']

        var entry = $("<li class='list-group-item'>" + subject + "</li>")
        entry.click(function (e) {
            displayMessageContent(message);
        })

        entry.mouseover(function (e) {

            entry.addClass("active")
        })

        entry.mouseout(function (e) {
            entry.removeClass("active")
        })
        list.append(entry)

    })
    messageContainer.append(list);
    inboxContainer.empty();
    inboxContainer.append(messageContainer);
}

function displayMessageContent(message) {
    getUserName(message["senderId"]).then(function (userName) {
        var messageBodyDiv = $("#message-body-content")
        var actionBar = $("#message-action-bar")
        var messageCard = $("<div class='card' style='height:100%'></div>")
        var cardBody = $("<div class='card-body'></div>")
        var messageTitle = $("<h5 class='card-title'>" + message["subject"] + "</h5>")
        var messageBody = $("<p class='card-text'>" + message["body"] + "</p>")
        var messageSender = $("<h6 class='card-subtitle mb-2 text-muted'>" + userName + "</h6>")

        cardBody.append(messageTitle);
        cardBody.append(messageSender);
        cardBody.append(messageBody);

        if (message["type"] == "Loan") {
            actionBar.empty();
            var acceptLoanButton = $("<a href='#' class='card-link'>Accept Loan</a>")
            var requestedCards = $("<ul class='list-group'></ul>")
            var keys = Object.keys(message["requestedCardsAndAmounts"])
            $.each(keys, function (index) {
                var requestedCard = keys[index]
                var amount = message["requestedCardsAndAmounts"][requestedCard]
                requestedCards.append($("<li class='list-group-item'>" + amount + " " + requestedCard + "</li>"))
            })

            acceptLoanButton.click(function (e) {
                e.preventDefault()
                acceptLoanRequest(message["messageId"]).then(function () {
                    swal("", "Accepted Loan Request", "success")
                    fetchMessagesAndShow();
                })
            })
            cardBody.append(acceptLoanButton)
            cardBody.append(requestedCards)
        } else if (message["type"] == "Group") {

            var acceptInviteButton = $("<a href='#' class='card-link'>Accept Invite</a>")
            acceptInviteButton.click(function (e) {
                e.preventDefault();
                acceptGroupInvite(message["messageId"]).then(function () {
                    swal("", "Group Invite Accepted!", "success")
                    fetchMessagesAndShow();
                })
            })
            cardBody.append(acceptInviteButton)
        }
        var deleteMessageButton = $("<a href='#' class='card-link'>Delete</a>")
        deleteMessageButton.click(function (e) {
            deleteMessage(message["messageId"]).then(function () {
                fetchMessagesAndShow();
            })
        })
        cardBody.append(deleteMessageButton)
        messageCard.append(cardBody)
        messageBodyDiv.empty();
        messageBodyDiv.append(messageCard);

    })

}

$(document).ready(function () {
    fetchMessagesAndShow();
})