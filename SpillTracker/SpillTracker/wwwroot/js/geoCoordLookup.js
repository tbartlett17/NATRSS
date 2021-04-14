

function getCoords()
{

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


function errorOnAjax()
{
    console.log("ERROR in ajax request");
    alert("invalid address, double check you entered the right thing");
}

function successOnAjax(data)
{
    console.log(data);
    alert("success on ajax");

    $("#location").append(data);


    document.forms[0].submit();
}
