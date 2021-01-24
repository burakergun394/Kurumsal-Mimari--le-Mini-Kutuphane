function onClick(element) {
    document.getElementById("img01").src = element.src;
    document.getElementById("img01").alt = element.alt;
    document.getElementById("modal01").style.display = "block";
}

var open = document.getElementsByClassName("open")[0];
open.onClick = {

}