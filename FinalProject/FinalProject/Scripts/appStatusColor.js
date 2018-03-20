function appStatusColor()
{
    
    var appS = document.getElementsByClassName('appStatus');

    for (i = 0; i < appS.length; i++) {
        console.log(appS[i].innerText);
        if (appS[i].innerText == "Declined") {
            appS[i].style.color = "red";
        } else if (appS[i].innerText == "Pending") {
            appS[i].style.color = "#c2b200";
        } else if (appS[i].innerText == "Accepted") {
            appS[i].style.color = "green";
        }
    }
}
