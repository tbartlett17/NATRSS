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
    let timeTillReport = (reportableWeight/releasePerHour) - timeReleased;
    let dayRelease = (24*releasePerHour).toFixed(2);
    
    let tr = $("#totalReleased");
    let rph = $("#releasePerHour");
    let dr = $("#dayRelease");
    let ttr = $("#timeTillReport");
    $("#totalReleased").empty();
    $("#releasePerHour").empty();
    $("#dayRelease").empty();
    $("#timeTillReport").empty();

    if(totalRelease > 0) 
    {
        tr.append("Amount released so far: " + totalRelease + " lbs")
        rph.append("Amount released per hour: " + releasePerHour + " lbs")
        dr.append("Assuming continuous release, over the course of 24 hours " + dayRelease + " lbs. will be released.")
    }
    

    if(dayRelease > reportableWeight) 
    {
        dr.append(" This exceeds the reportable quantity for " + chemical + ". You will need to report this to the proper " + `<a asp-controller="ContactInfoes" asp-action="Index">authorities</a>.`)
        if(cercla == "CERCLA" && cercla != null) 
        {
            dr.append(" and notify the National Response Center at 1-800-424-8802")
        }
    }

    if(timeTillReport > 0)
    {
        ttr.append("This spill is not yet reportable, at the current release rate of " + releasePerHour + " lbs/hr this spill will become reportable in " + timeTillReport + " hrs.")
    }

}
)