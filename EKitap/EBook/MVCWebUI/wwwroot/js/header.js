var isOpen = false;
var navbarClose = document.getElementById("navClose");
var navbarOpen = document.getElementById("navOpen");
var navbarMiddle = document.getElementsByClassName("navbar__middle")[0]
var navbarRight = document.getElementsByClassName("navbar__right")[0]

function toggleNavbar() {
    if (!isOpen) {
        navbarMiddle.style.display = "block";
        navbarRight.style.display = "block";
        navbarOpen.style.display = "none";
        navbarClose.style.display = "block"
        isOpen = true;
    }
}

function closeNavbar() {
    if (isOpen) {
        navbarMiddle.style.display = "none";
        navbarRight.style.display = "none";
        navbarOpen.style.display = "block";
        navbarClose.style.display = "none"
        isOpen = false;
    }
}

window.onresize = function (event) {
    if (screen.width > 992) {
        navbarMiddle.style.display = "block";
        navbarRight.style.display = "block";
    }
};