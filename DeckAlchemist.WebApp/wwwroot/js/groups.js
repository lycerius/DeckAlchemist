$(document).ready(function () {

    function getGroupsAndPopulate() {
        getGroups().then(function (result) {
            console.log("Groups got")
            console.log(result);
            populateGroupsList(result)
        })
    };

    function populateGroupsList(groups) {
        var groupInfoTable = $("#groupInfoTable");
        groupInfoTable.empty();
        $.each(groups, function(index) {
            
            var groupId = groups[index];
            getGroupInfo(groupId).then(function(groupInfo) {
                console.log(groupInfo)
            })
        })
    }

    getGroupsAndPopulate()
});