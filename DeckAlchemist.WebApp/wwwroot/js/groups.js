$(document).ready(function () {

    function getGroupsAndPopulate() {
        getGroups().then(function (result) {
            console.log("Groups got")
            console.log(result);
            populateGroupList(result)
        })
    }

    function populateGroupList(result) {

    }

    getGroupsAndPopulate()
});