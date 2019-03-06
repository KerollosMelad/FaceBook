// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*--- emojies show on text area ---*/
$("#MyPosts").on("click", '.add-smiles > span', function () {
    $(this).parent().siblings(".smiles-bunch").toggleClass("active");
});

jQuery("#MyPosts").on("keydown", ".post-comt-box textarea", function (event) {

    /** change the behavior of Enter Button to submit the form **/
    if (event.keyCode == 13) {
        $(".post-comt-box button[type='submit']").click();
    }
});

/** add emojies to text area **/
$("#MyPosts").on("click", ".post-comt-box .smiles-bunch i", function () {
    //$(".post-comt-box textarea").val($(".post-comt-box textarea").val());
});