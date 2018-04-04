var groupsModel = {}
function getGroupsAndPopulate() {
        getAllUserGroups().then(function (result) {
            groupsModel = result;
            populateGroupsList(result)
        })
    };

    function populateGroupsList(groups) {
        var groupInfoTable = $("#groupInfoTable");
        groupInfoTable.empty();
        $.each(groups, function(index) {
            var groupInfo = groups[index];
            var members = groups[index].users;
                    var row = $("<div class='row' style='width:100%;'></div>")
                    var cell = $("<div style='width:100%'></div>")
                    var link = $("<a style='width: 100%;' data-toggle='collapse' href='#group"+index+"' role='button'>"+groupInfo.groupName+"</a>")
                    var list = $("<div class='collapse' id='group"+index+"'></div>")
                    $.each(members, function(index) {
                        var member = members[index]
                        var element = $("<a>"+member.userName+"</a>")
                        var loanButton = $("<button>Loan</button>")
                        element.click(function(e){
                            createNewUserMessageDialog(groupInfo, member)
                        })
                        loanButton.click(function(e) {
                            
                        })
                        list.append(element)
                        list.append(loanButton)
                    })
                    var newInviteLink = $("<a>+ New Member</a><br />")
                    newInviteLink.click(function(e) {
                        createNewGroupInviteDialog(groupInfo)
                    })
                    list.append(newInviteLink)


                    cell.append(link)
                    cell.append(list)
                    cell.append($('<hr />'))
                    row.append(cell)

                    groupInfoTable.append(row)

        })
    }

    function createNewUserMessageDialog(group, user) {
        var newMessageDialog = $('#newMessageDialog')
        var userTextBox = $('#message-user')
        var sendMessageBtn = $('#create-message-btn')

        userTextBox.text(user.userName)
        sendMessageBtn.click(function(e) {
            var subjectTextBox = $('#message-subject')
            var bodyTextBox = $('#message-body')

            var message = {
                "groupId": group.groupId,
                "subject": subjectTextBox.val(),
                "body": bodyTextBox.val(),
                "recipientId": user.userId
            }
            console.log(message)
            sendUserMessage(message).then(function() {
                newMessageDialog.modal("toggle")
                subjectTextBox.val("")
                bodyTextBox.val("")
            })

        })
        newMessageDialog.modal("toggle")

    }

    function createNewGroupInviteDialog(group) {

        var modal = $('#newGroupInviteDialog')
        var sendGroupInviteBtn = $('#create-invite-btn')
        sendGroupInviteBtn.click(function(e) {
            var userNameTextBox = $('#invite-user')
            var userName = userNameTextBox.val()
            var message = {
                "groupId": group.groupId,
                "subject": "Invite!",
                "body": "No Body :(",
                "recipientUserName": userName
            }
            sendGroupInvite(message).then(function() {
                modal.modal("toggle")
            })
        })

        modal.modal("toggle")

   }

   function createNewLoanDialog(group, user) {
        var userCollection = getOwnedCardsForUser(user.userId);
        console.log(userCollection);
        //TODO
   }



$(document).ready(function () {
    $('#newLoanDialog').modal("toggle")
    $('#create-group-btn').click(function(){
        var groupName = $('#group-name').val();
        createGroup(groupName).then(function() {
            $('#newGroupDialog').modal('toggle')
            getGroupsAndPopulate()
        }).catch(function(error){
            console.log("Error")
        })
    })

    getGroupsAndPopulate()
});