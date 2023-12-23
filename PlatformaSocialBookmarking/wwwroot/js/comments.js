$(document).ready(function () {
    // Enable inline editing for comments
    $(".edit-comment").on("click", function () {
        var commentId = $(this).data("comment-id");

        // Perform AJAX request to load the inline edit view
        $.ajax({
            url: "/Comments/Edit/" + commentId,
            method: "GET",
            success: function (data) {
                // Replace the comment container with the loaded partial view
                $(".comment-container[data-comment-id='" + commentId + "']").html(data);
            },
            error: function (error) {
                console.error("Error loading inline edit view:", error);
            }
        });
    });

    // Load the inline edit view
    $(document).on("click", ".edit-comment", function () {
        var commentId = $(this).data("comment-id");

        // Perform AJAX request to load the inline edit view
        $.ajax({
            url: "/Comments/Edit/" + commentId,
            method: "GET",
            success: function (data) {
                // Replace the comment container with the loaded partial view
                $(".comment-container[data-comment-id='" + commentId + "']").html(data);
            },
            error: function (error) {
                console.error("Error loading inline edit view:", error);
            }
        });
    });

    // Save edited comment
    $(document).on("click", ".comment-container .save-comment", function () {
        var commentId = $(this).data("comment-id");
        var editedCommentContent = $(this).closest(".comment-container").find("textarea").val();

        // Perform AJAX request to update the comment on the server
        $.ajax({
            url: "/Comments/UpdateComment", // Update with your actual controller action
            method: "POST",
            data: { commentId: commentId, editedCommentContent: editedCommentContent },
            success: function (data) {
                // Reload the page to return to the original view
                location.reload();
            },
            error: function (error) {
                console.error("Error updating comment:", error);
            }
        });
    });


    // Cancel editing
    $(document).on("click", ".comment-container .cancel-edit", function () {
        var commentId = $(this).data("comment-id");

        // Reload the entire page to return to the original view
        location.reload();
    });



});
