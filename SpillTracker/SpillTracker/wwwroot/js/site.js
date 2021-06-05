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
    let list = document.createElement("ul");
    for (let i = 0; i < data.length; i++) {
        let tag = document.createElement("p");
        tag.className = "commitSpacing";
        tag.append(data[i]["date"] + " " + data[i]["commitId"] + " " + data[i]["commitMessage"]);
        list.append(tag)
        $("#versionH").append(list);
    }
}

/*function disclaimer() {
    //make this a modal displaying this information
}*/


function errorFunction() {
    console.log("Something bad happened, try again.")
}