console.log("js file works!")

// function calculate(reportableWeight, amountReleased, timeReleased, percentReleased) {
//     let totalRelease = (amountReleased * percentReleased).toFixed(2);
//     let releasePerHour = (totalRelease / timeReleased).toFixed(2);
//     let timeTillReport = ((reportableWeight/releasePerHour) - timeReleased).toFixed(2);
//     console.log(timeTillReport);
//     let vals = [totalRelease, releasePerHour, timeTillReport];
//     return vals;
    
// }


$("#calcButton").click(function() 
{
    let x = 0;
    let c = document.getElementById("chemName");
    let chemical = c.textContent;
    let p = document.getElementById("chemRW");
    let text = p.textContent;
    let reportableWeight = parseInt(text);
    let t = document.getElementById("cercla");
    let cercla = t.textContent;

    let amountReleased = $('#amountReleased').val();
    let timeReleased = $('#timeReleased').val();
    let percentReleased = $('#percentReleased').val();

    let eone = document.getElementById("errorAM");
    let etwo = document.getElementById("errorTR");
    let ethree = document.getElementById("errorPM");

    $("#errorAM").empty();
    $("#errorTR").empty();
    $("#errorPM").empty();

    var reg = new RegExp('^[0-9]+$');

    if(!reg.test(amountReleased))
    {
        eone.append("Amount Released is not a number");
        document.getElementById("errorAM").style.color = "red";
        x = 1;
        eone.innerHTML += "<br>";
    }
    if(!reg.test(timeReleased))
    {
        etwo.append("Time of Release is not a number");
        document.getElementById("errorTR").style.color = "red";
        x = 1;
        etwo.innerHTML += "<br>";
    }
    if(!reg.test(percentReleased))
    {
        ethree.append("Percent Released is not a number");
        document.getElementById("errorPM").style.color = "red";
        x = 1;
        ethree.innerHTML += "<br>";
    }
    if(percentReleased > 100) 
    {
        document.getElementById("errorPM").style.color = "red";
        ethree.append("Percent Released cannot be greater than 100"); 
        x = 1;
        ethree.innerHTML += "<br>";
    }
    if(percentReleased < 0) 
    {
        document.getElementById("errorPM").style.color = "red";
        ethree.append("Percent Released cannot be negative"); 
        x = 1;
        ethree.innerHTML += "<br>";
    }
    if(amountReleased < 0) 
    {
        document.getElementById("errorAM").style.color = "red";
        eone.append("Amount Released cannot be negative"); 
        x = 1;
        eone.innerHTML += "<br>";
    }
    if(timeReleased < 0) 
    {
        document.getElementById("errorTR").style.color = "red";
        etwo.append("Time of Release cannot be negative"); 
        x = 1;
        etwo.innerHTML += "<br>";
    }


    percentReleased = percentReleased / 100;

    let totalRelease = (amountReleased * percentReleased).toFixed(2);
    let releasePerHour = (totalRelease / timeReleased).toFixed(2);
    let timeTillReport = ((reportableWeight/releasePerHour) - timeReleased).toFixed(2);
    let dayRelease = (24*releasePerHour).toFixed(2);
    
    let tr = $("#totalReleased");
    let rph = $("#releasePerHour");
    let dr = $("#dayRelease");
    let ttr = $("#timeTillReport");
    let ttrs = $("#timeTillReports");
    let headerGood = $("#headergood");
    let headerBad = $("#headerbad");
    let mathAR = $("#mathAR");
    let mathTR = $("#mathTR");
    let mathTTR = $("#mathTTR");
    
    $("#totalReleased").empty();
    $("#releasePerHour").empty();
    $("#dayRelease").empty();
    $("#timeTillReport").empty();
    $("#timeTillReports").empty();
    $("#headergood").empty();
    $("#headerbad").empty();
    $("#mathAR").empty();
    $("#mathTR").empty();
    $("#mathTTR").empty();
    
    if(x == 1)
    {
        document.getElementById("modal-content").style.backgroundColor = "orange";
        headerBad.append("Error in one or more input boxes!")
    }
    else if(amountReleased == '' || timeReleased == '' || percentReleased == '') 
    {
        document.getElementById("modal-content").style.backgroundColor = "orange";
        headerBad.append("Please enter information into all of the input boxes!")
    }
    else
    {
        document.getElementById("stats").style.display = "block"
        tr.append(totalRelease + " lbs")
        rph.append(releasePerHour + " lbs/hr")
        mathTR.append(amountReleased + " x " + percentReleased);
        mathAR.append(totalRelease + "/" + timeReleased);
        mathTTR.append("(" + reportableWeight + "/" + releasePerHour + ") " + "- " + timeReleased)
        if(timeTillReport < 0)
        {
            ttrs.append("N/A")
        }
        else if(timeTillReport < 24 && timeTillReport >= 1) 
        {
            ttrs.append(timeTillReport + " hrs.");
        }
        else if(timeTillReport < 1)
        {
            timeTillReport = (timeTillReport * 60).toFixed(2);
            ttrs.append(timeTillReport + " minutes")
        }
        else
        {
            timeTillReport = (timeTillReport / 24).toFixed(2);
            ttrs.append(timeTillReport + " days");
            
        }
    
        timeTillReport = ((reportableWeight/releasePerHour) - timeReleased).toFixed(2);
        if(totalRelease >= reportableWeight)
        {
            document.getElementById("modal-content").style.backgroundColor = "tomato";
            headerBad.append("Notice: The chemical spill needs to be reported to the proper authorities.")
            if(cercla == "CERCLA" && cercla != null) 
            {
                dr.append(" Since this is a CERCLA chemical you will need to contact the National Response Center at 1-800-424-8802")
            }
        }
        else 
        {
            document.getElementById("modal-content").style.backgroundColor = "springgreen";
            headerGood.append("Notice: The chemical spill has not reached the reportable quantity")
            ttr.append("At the current release rate of " + releasePerHour + " lbs/hr this spill will become reportable in ")
            if(timeTillReport < 24 && timeTillReport >= 1) 
            {
                ttr.append(timeTillReport + " hrs.");
            }
            else if(timeTillReport < 1)
            {
                timeTillReport = (timeTillReport * 60).toFixed(2);
                ttr.append(timeTillReport + " minutes")
            }
            else
            {
                timeTillReport = (timeTillReport / 24).toFixed(2);
                ttr.append(timeTillReport + " days");
            }
        }
    }
}
)
