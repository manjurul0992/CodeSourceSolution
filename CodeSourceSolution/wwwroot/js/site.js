// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    function LoadData(FormatId = null) {
        var FormatContainer = $("#FormatContainer")
        $.ajax({
            url: "/Players/AddNewFormats/" + FormatId ?? "",
            type: "GET",
            success: function (data) {
                FormatContainer.append(data);
            }
        });
    }
$(document).on("click", "#btnAdd", function (e) {
    e.preventDefault();
    LoadData();
});
$(document).on("click", "#DeleteFormat", function (e) {
    e.preventDefault();

    // Get the parent container of the clicked button (the format row)
    var formatRow = $(this).parent().parent();

    // Check if there's only one format row remaining (including the clicked one)
    if (formatRow.siblings().length === 1) {
        // Don't allow deletion if it's the last row
        return;
    }

    // Remove the format row
    formatRow.remove();

    //// Check if there's one format row remaining after deletion
    //if ($("#FormatContainer").children().length === 1) {
    //    // Disable the Delete button if there's only one row left
    //    $("#DeleteFormat").prop("disabled", true);
    //}
});