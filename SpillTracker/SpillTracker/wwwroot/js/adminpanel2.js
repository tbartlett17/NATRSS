$("#EPCRAscrapeBtn").click(function (e) {
    var inputXpath = $("#EPCRAtableXpath").val();
    console.log("running EPCRA Scrapper with xpath: " + inputXpath);
    $("#EPCRAoverlay").css({ display: "flex" });

    $.ajax({
        type: "POST",
        async: false,
        data: { xpath: inputXpath },
        url: "/Admin/ScrapeEPCRAtable",
        success: successParse,
        error: errorOnAjax
    });

    $("#EPCRAoverlay").css({ display: "none" });
});

$("#CERCLAscrapeBtn").click(function (e) {
    var inputXpath = $("#CERCLAtableXpath").val();
    console.log("running CERCLA Scrapper with xpath: " + inputXpath);
    $("#CERCLAoverlay").css({ display: "flex" });

    $.ajax({
        type: "POST",
        async: false,
        data: { xpath: inputXpath },
        url: "/Admin/ScrapeCERCLAtable",
        success: successParse,
        error: errorOnAjax
    });

    $("#CERCLAoverlay").css({ display: "none" });
});

//$("#createBtn").click(function()
    function create()
    {
        //let name = document.getElementById("companyName");

        
            var name = $("#companyName").val()


        // var jsonData = JSON.stringify(name);
        if(name != "" && name.length >= 3) {
           
            $.ajax({
            type: "POST",
            dataType: "json",
            data: {name, name},
            url: "/Admin/Exist",
            success: successExist,
            error: errorOnAjax
        })
        }
        else
        {
            document.getElementById("clickBtn").click();
            
        }
    }   
//)
function clear() 
{
    document.getElementById("error").style.display = "none";
}


//$("#UpdateChemsBtn").click(function (e) {

//    console.log("attempting to update the chemical list via PubChem API calls");

//});

function errorOnAjax() {
    console.log("ERROR in ajax request");
    alert("ERROR in ajax request");
    $("#outputLog").val += ("ERROR in ajax request\n");
    $("#overlay").css({ display: "none" });
}

function successParse(data) {
    $("#outputLog").val += ("success\n");
    alert("success on ajax request");
    //$("#overlay").css({ display: "none" });
}

function successExist(data) {
    console.log(data);
    if(data == false) {
        document.getElementById("clickBtn").click();
        document.getElementById("error").style.display = "none";
        document.getElementById("success").style.display = "block";
    }
    else
    {
       document.getElementById("error").style.display = "block";
       document.getElementById("success").style.display = "none";
    }
}