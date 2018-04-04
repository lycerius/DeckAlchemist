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
                    var row = $("<div class='row'></div>")
                    var cell = $("<div></td>")
                    var link = $("<a style='width: 100%;' data-toggle='collapse' href='#group"+index+"' role='button'>"+groupInfo.groupName+"</a>")
                    var list = $("<div class='collapse' id='group"+index+"'></div>")
                    $.each(members, function(index) {
                        var member = members[index]
                        var element = $("<a>"+member.userName+"</a>")
                        element.click(function(e){
                            createNewUserMessageDialog(groupInfo, member)
                        })
                        list.append(element)
                    })

                    cell.append(link)
                    cell.append(list)
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




$(document).ready(function () {
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