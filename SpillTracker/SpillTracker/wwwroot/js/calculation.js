console.log("js file works!")

$("#calcButton").click(function() 
// function calculate()
{
    let c = document.getElementById("chemName");
    let chemical = c.textContent;
    let p = document.getElementById("chemRW");
    let text = p.textContent;
    let reportableWeight = parseInt(text);
    let t = document.getElementById("cercla");
    let cercla = t.textContent;

    let amountReleased = $('#amountReleased').val();
    let timeReleased = $('#timeReleased').val();
    let percentReleased = $('#percentReleased').val()/100;

    let totalRelease = (amountReleased * percentReleased).toFixed(2);
    let releasePerHour = (totalRelease / timeReleased).toFixed(2);
    let timeTillReport = ((reportableWeight/releasePerHour) - timeReleased).toFixed(2);
    let dayRelease = (24*releasePerHour).toFixed(2);
    
    let tr = $("#totalReleased");
    let rph = $("#releasePerHour");
    let dr = $("#dayRelease");
    let ttr = $("#timeTillReport");
    let headerGood = $("#headergood")
    let headerBad = $("#headerbad")
    
    $("#totalReleased").empty();
    $("#releasePerHour").empty();
    $("#dayRelease").empty();
    $("#timeTillReport").empty();
    $("#headergood").empty();
    $("#headerbad").empty();
     
    
    tr.append("Amount released so far: " + totalRelease + " lbs")
    rph.append("Amount released per hour: " + releasePerHour + " lbs")
 
    
    if(totalRelease > reportableWeight)
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
        if(timeTillReport < 24 && timeTillReport > 1) 
        {
            ttr.append(timeTillReport + " hrs.");
        }
        else if(timeTillReport < 1)
        {
            timeTillReport = (timeTillReport * 60);
            ttr.append(timeTillReport + " minutes")
        }
        else
        {
            timeTillReport = (timeTillReport / 24).toFixed(2);
            ttr.append(timeTillReport + " days");
        }
    }
}
)