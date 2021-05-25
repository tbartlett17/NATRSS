window.onload = function() {
    var id = window.sessionStorage.getItem("id");
    if(id != null) {
        document.getElementById(id).style.backgroundColor = "#536abe";
        document.getElementById(id).style.color = "white";
    }
}

function search()    
{    
    console.log("Working");
    var input, filter, table, tr, td, i, td1;    
    input = document.getElementById("searchTextBoxid"); //to get typed in keyword    
    filter = input.value.toUpperCase(); //to avoid case sensitive search, if case sensitive search is required then comment this line    
    table =document.getElementById("mainTableid"); //to get the html table    
    tr = table.getElementsByTagName("tr"); //to access rows in the table    
    var countvisble=0; //to hide and show the alert label    
    // Search all table rows, hide those who don't match the search key word    
    for(i=0;i<tr.length;i++)    
    {    
        td=tr[i].getElementsByTagName("td")[0];
        td1=tr[i].getElementsByTagName("td")[1];
        td2=tr[i].getElementsByTagName("td")[3]; //search keyword searched only in 1st column of the table, if you want to search other columns then change [0] to [1] or any required column number    
        if(td || td1 || td2)    
        {    
            if(td.innerHTML.toUpperCase().indexOf(filter)>-1 || td1.innerHTML.toUpperCase().indexOf(filter)>-1 || td2.innerHTML.toUpperCase().indexOf(filter)>-1)    
            {    
                countvisble++;    
                tr[i].style.display="";    
                document.getElementById("NotExist").style.display = "none";    
            }    
            else    
            {    
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


document.querySelectorAll('a').forEach(item => {
    item.addEventListener('click', function() {
      //handle click
    window.sessionStorage.setItem("id", this.id);
    })
  })

