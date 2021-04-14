function calculate(reportableWeight, amountReleased, timeReleased, percentReleased) {
    percentReleased = (percentReleased / 100).toFixed(2);
    let totalRelease = (amountReleased * percentReleased).toFixed(2);
    let releasePerHour = (totalRelease / timeReleased).toFixed(2);
    let timeTillReport = ((reportableWeight/releasePerHour) - timeReleased);
    let vals = [totalRelease, releasePerHour, timeTillReport];
    return vals;   
}

export { calculate }