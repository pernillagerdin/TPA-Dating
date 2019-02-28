$(document).ready(() => {
    console.log("Script loaded: profile-requests.js");
});

$("body").on("click", "#RequestBtn", HandleRequest);
$("body").on("click", "#AcceptBtn", AcceptRequest);
$("body").on("click", "#DeclineBtn", DeclineRequest);

function HandleRequest() {
    var profileID = $(this).parents().eq(1).data("profile-id");
    if ($("#RequestBtn").text() == "Send Request") {
        $.ajax({
            type: "POST",
            url: "/api/ContactApi/SendRequest/" + profileID,
            success: () => {
                $("#RequestBtn").text("Cancel Request");
            },
            error: () => { console.log("something went wrong"); }

        });
    } else if ($("#RequestBtn").text() == "Cancel Request") {
        $.ajax({
            type: "POST",
            url: "/api/ContactApi/CancelRequest/" + profileID,
            success: () => {
                $("#RequestBtn").text("Send Request");
            },
            error: () => { console.log("something went wrong"); }

        });
    }
}

function AcceptRequest() {
    var profileID = $(this).parents().eq(2).data("profile-id");
    $.ajax({
        type: "POST",
        url: "/api/ContactApi/AcceptRequest/" + profileID,
        success: () => {
            $("#RequestBtnGroup").addClass("d-none");
            UpdateContacts(profileID);
            SetNumber();
        },
        error: () => { console.log("something went wrong"); }

    });
}
function DeclineRequest() {
    var profileID = $(this).parents().eq(2).data("profile-id");
    $.ajax({
        type: "POST",
        url: "/api/ContactApi/DeclineRequest/" + profileID,
        success: () => {
            $("#RequestBtnGroup").addClass("d-none");
            $("#RequestBtn").removeClass("d-none");
        },
        error: () => { console.log("something went wrong"); }

    });
}

function UpdateContacts(profileID) {
    $.ajax({
        type: "POST",
        url: "/Profile/UpdateContacts/" + profileID,
        success: function (data) {
            $("#Contacts").html(data);
        },
        error: () => { console.log("something went wrong"); }
    });
}