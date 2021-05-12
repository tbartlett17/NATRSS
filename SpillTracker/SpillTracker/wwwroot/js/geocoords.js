

function getCoords() {

    // add spinner to button
    $("#submitBtn").html(
        `<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...`
    );

    var address = {
        street: $("#streetAddress").val(),
        city: $("#city").val(),
        state: $("#state").val(),
        postalCode: $("#zip").val()
    };

    var jsonAddress = JSON.stringify(address);

    console.log(jsonAddress);

    $.ajax({
        type: "POST",
        dataType: "json",
        data: { streetAddress: jsonAddress },
        url: "/GeoCoordinates/GetCoords",
        success: successOnAjax,
        error: errorOnAjax
    });
}


function errorOnAjax() {
    console.log("ERROR in ajax request");
    alert("invalid address, double check you entered the right thing");
}

function successOnAjax(data) {
    console.log(data);
    //alert("success on ajax");

    //data = {"Longitude":"-123.047074","Latitude":"44.623521"}

    var coords = JSON.parse(data);
    var location = coords.Latitude + ", " + coords.Longitude;

    console.log(location);

    $("#location").val(location);

    document.getElementById("submitForm").click();
    //document.forms[0].submit();
}

$("#facBtn").click(function()
{
    /*Street Address City State Zip*/
    let sa = document.getElementById("streetaddress")
    let streetAddress = sa.textContent;
    let c = document.getElementById("city")
    let city = c.textContent;
    let s = document.getElementById("state")
    let state = s.textContent;
    let z = document.getElementById("zip")
    let zip = z.textContent;


    let streetAddress = $("#streetaddress").val();
    let yellowModal = $("#yellowModal")

    $("#streetaddress").empty();
    $("#city").empty();
    $("#state").empty();
    $("#zip").empty();
    $("#yellowModal").empty();

    if (streetAddress == '' || city == '' || state == '' || zip == '') {
        document.getElementById("modal-content").style.backgroundColor = "yellow";
        yellowModal.append("Please enter information into all of the input boxes!")
    }
    else {
       
    }


}
)