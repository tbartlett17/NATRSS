

function getCoords() {

    // add spinner to button
    $("#submitBtn").html(
        `<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...`
    );

    let fac = $("#facility").val();
    let ind = $("#industry").val();

    var address = {
        street: $("#streetAddress").val(),
        city: $("#city").val(),
        state: $("#state").val(),
        postalCode: $("#zip").val()
    };

    let eName = document.getElementById("errorName");
    let eStreet = document.getElementById("errorStreet");
    let eCity = document.getElementById("errorCity");
    let eZip = document.getElementById("errorZip");
    let eIndustry = document.getElementById("errorIndustry");

    $("#errorName").empty();
    $("#errorStreet").empty();
    $("#errorCity").empty();
    $("#errorZip").empty();
    $("#errorIndustry").empty();

    if(fac.length > 20) 
    {
        eName.append("Facility name too long");
    }
    else if(fac.length < 1)
    {
        eName.append("Facility name is missing");
    }
    else if(address.street.length > 35)
    {
        eStreet.append("Street name is too long");
    }
    else if(address.street.length < 1)
    {
        eStreet.append("Must enter a street address");
    }
    else if(address.city.length > 17)
    {
        eCity.append("City name is too long");
    }
    else if(address.city.length < 1)
    {
        eCity.append("Must enter a city");
    }
    else if(address.postalCode.length > 5)
    {
        eZip.append("Zip code length is too long");
    }
    else if(address.postalCode.length < 1) 
    {
        eZip.append("Zip code must be entered");
    }
    else if(ind.length > 30)
    {
        eIndustry.append("Industry length is too long");
    }
    else
    {
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
    /*let sa = document.getElementById("streetaddress")
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

*/
}
)