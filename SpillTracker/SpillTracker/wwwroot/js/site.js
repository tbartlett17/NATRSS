// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log("this site.js is working.");


$(document).ready(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/versionHistory",
        success: displayVersions,
        error: errorFunction
    });
});

function displayVersions(data) {
    console.log(data);
}

function errorFunction() {
    console.log("Something happened, you suck.")
}