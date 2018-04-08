function fetchMessagesAndShow() {
    getMessages().then(function(messagesJson) {
        populateInbox(messagesJson);
    })
}

function populateInbox(messagesJson) {
    var inboxContainer = $("#inbox-content")
    var messageContainer = $("<div></div>")
    $.each(messagesJson, function(index){
        var message = messagesJson[index];
        var subject = message['subject']
        var entry = $("<p>"+subject+"</p>")
        entry.click(function(e) {
            displayMessageContent(message);
        })
        messageContainer.append(entry);
    })
    inboxContainer.empty();
    inboxContainer.append(messageContainer);
}

function displayMessageContent(message) {
    var messageBody = $("#message-body-content")
    var actionBar = $("#message-action-bar")
    messageBody.text(message["body"])
    if(message["type"] == "Loan") {
        actionBar.empty();
        var acceptLoanButton = $("<button class='form-control btn'>Accept</button>")
        acceptLoanButton.click(function(e){
            acceptLoanRequest(message["messageId"]).then(function() {
                swal("Accepted")
            })
        })
        actionBar.append(acceptLoanButton)
    } else if(message["type"] == "Group") {
        actionBar.empty();
        var acceptInviteButton = $("<button class='form-control btn'>Accept</button>")
        acceptInviteButton.click(function(e) {
            acceptGroupInvite(message["messageId"]).then(function() {
                swal("Now in group")
            })
        })
        actionBar.append(acceptInviteButton)
    }
}

$(document).ready(function() {
    fetchMessagesAndShow();
})