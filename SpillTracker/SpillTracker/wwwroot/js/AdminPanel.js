

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