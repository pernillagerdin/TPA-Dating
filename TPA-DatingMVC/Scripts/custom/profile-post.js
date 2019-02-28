$(document).ready(() => {
    console.log("Script loaded: profile-post.js");
});

$("#PostWall").on("click", "#SubmitPost", AddPost);


function AddPost(passedThis) {
    if ($("#PostContent").val() != "") {
        var profileID = $(passedThis.currentTarget).parents().eq(6).data("profile-id");
        var model = { PostContent: $("#PostContent").val(), PostToID: profileID };
        $.ajax({
            type: "POST",
            url: "/api/PostApi/AddPost/",
            data: JSON.stringify(model),
            contentType: "application/json;charset=UTF-8",
            success: () => {
                UpdatePostWall(profileID);
            },
            error: () => { console.log("An error occured"); }
        });
    }
}
function UpdatePostWall(profileID) {
    $.ajax({
        type: "POST",
        url: "/Profile/UpdatePostWall/" + profileID,
        success: function (data) {
            $("#PostWall").html(data);
        },
        error: () => { console.log("Error: Unable to update"); }
    });
}