let ThisPage = new function () {
    this.ServicesPopupMenuVisible;
    this.ServicesMenuShow = function () {
        let popUpMenu = document.getElementById("ServicesPopupMenu");
        
        if (this.ServicesPopupMenuVisible) {
            popUpMenu.style.display = "none";
            popUpMenu.style.visibility = "hidden";
            this.ServicesPopupMenuVisible = false;
            return;
        }

        let servicesMenu = document.getElementById("ServicesMenu");
        popUpMenu.style.display = "flex";
        popUpMenu.style.flexDirection = "column";
        popUpMenu.style.left = Math.round(servicesMenu.offsetLeft + pageYOffset - 35) + "px";
        popUpMenu.style.top = "82px";
        popUpMenu.style.visibility = "visible";
        this.ServicesPopupMenuVisible = true;
        event.stopPropagation();
    }
}

ThisPage.ServicesMenuShow.ServicesPopupMenuVisible = false;
window.onclick = function (event) {
    if (ThisPage.ServicesPopupMenuVisible) {
        ThisPage.ServicesMenuShow();
    }
}





