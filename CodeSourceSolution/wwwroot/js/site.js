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
        })
    }
    $(document).on("click", "#btnAdd", function (e) {
        e.preventDefault();
    LoadData();
    })
    $(document).on("click", "#DeleteFormat", function (e) {
        e.preventDefault();
    $(this).parent().parent().remove();
    })
