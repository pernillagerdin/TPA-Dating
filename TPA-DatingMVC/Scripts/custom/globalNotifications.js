$(document).ready(() => {
    console.log("script loaded: globalNotifications.js");
    SetNumber();
});

function SetNumber() {
    $.ajax({
        type: "POST",
        url: "/Notifications/GetNumber/",
        success: function (data) {
            $("#NotificationNumberSpan").text(data.Number);
        },
        error: () => { console.log("something went wrong");}
    });
}

$("body").on("click", "#GlobalNotifications", ToggleNotificationDiv);
$(".fa-bell").hover(ToggleBell);
function ToggleBell() {
    if ($(".fa-bell").hasClass("far")) {
        $(".fa-bell").removeClass("far");
        $(".fa-bell").addClass("fas");
    } else if ($(".fa-bell").hasClass("fas")) {
        $(".fa-bell").removeClass("fas");
        $(".fa-bell").addClass("far");
    }
}

function ToggleNotificationDiv() {
    if ($("#NotificationPopUpDiv").hasClass("d-none")) {
        GetContent();
    } else {
        ToggleNotificationDisplay()
    }
}

function GetContent() {
    $.ajax({
        type: "POST",
        url: "/Notifications/GetContent/",
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            $("#NotificationPopUpDiv").html(data);
            CreateNotificationPopper();
        },
        error: () => { console.log("something went wrong"); }
    });
}
function CreateNotificationPopper() {
    var ref = $("#GlobalNotifications");
    var pop = $("#NotificationPopUpDiv");
    new Popper(ref, pop,{
        placement: "bottom",
        modifiers: {
            offset: {
                enbled: true,
                offset: "0, 10"
            }
        }
    });
    ToggleNotificationDisplay();
}
function ToggleNotificationDisplay() {
    $("#NotificationPopUpDiv").toggleClass("d-none")
}