console.log("js file works!")

$("#calcButton").click(function() 
// function calculate()
{
    let c = document.getElementById("chemName");
    let chemical = c.textContent;
    let p = document.getElementById("chemRW");
    let text = p.textContent;
    let reportableWeight = parseInt(text);

    let amountReleased = $('#amountReleased').val();
    let timeReleased = $('#timeReleased').val();
    let percentReleased = $('#percentReleased').val()/100;

    let totalRelease = (amountReleased * percentReleased).toFixed(2);
    let releasePerHour = (totalRelease / timeReleased).toFixed(2);
    let timeTillReport = (reportableWeight/releasePerHour) - timeReleased;
    let dayRelease = (24*releasePerHour).toFixed(2);
    
    let tr = $("#totalReleased");
    let rph = $("#releasePerHour")
    let dr = $("#dayRelease")
    let ttr = $("timeTillReport")
    $("#totalReleased").empty();
    $("#releasePerHour").empty();
    $("#dayRelease").empty();
    $("#timeTillReport").empty();
    tr.append("Amount released so far: " + totalRelease + " lbs")
    rph.append("Amount released per hour: " + releasePerHour + " lbs")
    dr.append("Assuming continuous release, over the course of 24 hours " + dayRelease + " lbs. will be released.")

    if(dayRelease > reportableWeight) 
    {
        dr.append(" This exceeds the reportable quantity for " + chemical + ". You will need to report this to the proper authorities.")
    }
    console.log(timeTillReport);
    if(timeTillReport > 0)
    {
        ttr.append("This spill is not yet reportable, if the spill is still occuring it will become reportable in " + timeTillReport + " hrs.")
    }

}
)