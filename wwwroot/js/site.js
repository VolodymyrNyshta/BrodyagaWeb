// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function DocumentClick(ATarget) {
    alert(ATarget);
}

function PlatoonMenuClick(ATarget) {
    //alert(ATarget.className);
    if (ATarget.className == 'menu-item') {
        var vSubMnu = ATarget.getElementsByClassName('submenu');
        closeMenu();
        vSubMnu[0].style.display = 'block';
    }
}

function closeMenu() {
    //var menu = document.getElementById('nav');
    var subm = document.getElementsByClassName('submenu');
    for (var i = 0; i < subm.length; i++) {
        subm[i].style.display = "none";
    }
}