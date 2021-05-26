function search()    
{    
    console.log("Working");
    var input, filter, table, tr, td, i, td1;    
    input = document.getElementById("searchTextBoxid"); //to get typed in keyword    
    filter = input.value.toUpperCase(); //to avoid case sensitive search, if case sensitive search is required then comment this line    
    table =document.getElementById("mainTableid"); //to get the html table    
    tr = table.getElementsByTagName("tr"); //to access rows in the table    
    var countvisble=0; //to hide and show the alert label    

    //table.style.display = "block";
    //console.log("table should be un hidden");

    
        // Search all table rows, hide those who don't match the search key word    
        for (i = 0; i < tr.length; i++)
        {
            //$("tr").attr('class').show();
            td = tr[i].getElementsByTagName("td")[0];
            td1 = tr[i].getElementsByTagName("td")[1];
            td2 = tr[i].getElementsByTagName("td")[2]; //search keyword searched only in 1st column of the table, if you want to search other columns then change [0] to [1] or any required column number    
            if (td || td1 || td2) {
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1 || td1.innerHTML.toUpperCase().indexOf(filter) > -1 || td2.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    countvisble++;
                    tr[i].style.display = "";
                    document.getElementById("NotExist").style.display = "none";
                }
                else {
                    tr[i].style.display = "none";
                    document.getElementById("NotExist").style.display = "none";
                }
            }
        }
    

    if(countvisble==0) //displays the alert message label    
    {    
        document.getElementById("NotExist").style.display = "Block";    
        document.getElementById("NotExist").innerHTML = "Does not exist!";    
    }    
}   

// part of js code found at https://www.c-sharpcorner.com/article/custom-search-using-client-side-code/


$("input").click(function (e) {
    if (e.target.id.substring(0, 11) == "addChemBtn_")
    {
        var chemId = e.target.id.substring(11);
        var chemName = e.target.parentNode.parentNode.id;

        //hide chem selector
        $('#addChem').modal('hide');

        //open new modal with selected chem
        $('#addChem').on('hidden.bs.modal', function () {

            $("#chemDetailsTitle").empty();
            $("#chemDetailsTitle").append(chemName);

            $("#chemId").empty();
            $("#chemId").append(chemId);

            $("#formConcentration").val('');
            $("#formTemperature").val('')


            $('#ChemDetails').modal('show');
        })
    }
});



function saveChemical()
{
    // disable button
    $("#saveChemBtn").prop("disabled", true);
    // add spinner to button
    $("#saveChemBtn").html(
        `<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...`
    );

    var chemData = {
        concentration: $("#formConcentration").val(),
        chemicalTemperature: $("#formTemperature").val(),
        ChemicalTemperatureUnits: $("#formTempUnits").val(),
        chemicalStateId: $("#formChemState").val(),
        chemicalId: $("#chemId").text(),
        facilityId: $("#facilityId").text()
    };
    //console.log("fac id: " + chemData.chemId);

    eCon = document.getElementById("errorCon");
    eTemp = document.getElementById("errorTemp");

    $("#errorCon").empty();
    $("#errorTemp").empty();

    if(chemData.concentration > 100) 
    {
        eCon.append("Contentration Cannot be more than 100");
    }
    else if(chemData.concentration < 1)
    {
        eCon.append("Concentration must be higher than 0");
    }
    else if(chemData.concentration.length < 1)
    {
        eCon.append("Concentration value is required");
    }
    else if(chemData.chemicalTemperature > 150)
    {
        eTemp.append("Chemical Temperature cannot exceed 150");
    }
    else if(chemData.chemicalTemperature < -50)
    {
        eTemp.append("Chemical Temperature cannot exceed -50");
    }
    else if(chemData.chemicalTemperature.length < 1)
    {
        eTemp.append("Chemical Temperature is required");
    }
    else
    {
        var jsonChemData = JSON.stringify(chemData);

        $.ajax({
            type: "POST",
            dataType: "json",
            data: { chemData: jsonChemData },
            url: "/Facilities/SaveChemical",
            success: updateTable,
            error: errorOnAjax
        });  
    }
    
     // disable button
     $("#saveChemBtn").prop("disabled", false);
     // add spinner to button
     $("#saveChemBtn").html(
         "Add Chem"
     );


    
}

function errorOnAjax() {
    console.log("ERROR in ajax request");
    alert("ERROR in ajax request");
}

function updateTable(data) {
    //insert the new chem to the table
    var table = document.getElementById("facilityChemsTbl");
   
    //for (var i = 0, d; d = data[i]; i++) {

    //    var row = table.insertRow();
    //    var cell1 = row.insertCell(); //Chem Name
    //    var cell2 = row.insertCell(); //Concentration
    //    var cell3 = row.insertCell(); //Temperature
    //    var cell4 = row.insertCell(); //Chem State

    //    cell1.innerHTML = "chem name";
    //    cell2.innerHTML = "conc";
    //    cell3.innerHTML = "temp";
    //    cell4.innerHTML = "state";
    //    table.append(row);
    //}

    console.log(data);
    //alert("success on ajax");
    $('#ChemDetails').modal('hide');
    window.location.reload();
}
