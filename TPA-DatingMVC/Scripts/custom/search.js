$(document).ready(() => {
    console.log("Script loaded: script.js");
});

$("#SearchField").on("keyup", FilterUsers);

function FilterUsers() {
    var searchString = $("#SearchField").val();
    var users = document.getElementsByClassName("personal-info");
    var usersArray = $.makeArray(users);
    for (var i = 0; i < usersArray.length; i++) {
        if (!usersArray[i].innerHTML.toString().toUpperCase().includes(searchString.toUpperCase())) {
            if (!$(usersArray[i]).parents().eq(4).hasClass("d-none")) {
                $(usersArray[i]).parents().eq(4).addClass("d-none");
            }
        }
    }
    for (var i = 0; i < usersArray.length; i++) {
        if (usersArray[i].innerHTML.toString().toUpperCase().includes(searchString.toUpperCase())) {
            if ($(usersArray[i]).parents().eq(4).hasClass("d-none")) {
                $(usersArray[i]).parents().eq(4).removeClass("d-none");
            }
        }
    }
}