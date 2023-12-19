let ThisPage = new function () {

    this.LanguagesPopupMenuVisible;
    this.LanguagesMenuShow = function () {
        let popUpMenu = document.getElementById("LanguagesPopupMenu");

        if (this.LanguagesPopupMenuVisible) {
            popUpMenu.style.display = "none";
            this.LanguagesPopupMenuVisible = false;
            return;
        }

        popUpMenu.style.display = "flex";
        this.LanguagesPopupMenuVisible = true;
        event.stopPropagation();
    }
}

ThisPage.LanguagesPopupMenuVisible = false;
window.onclick = function (event) {
    if (ThisPage.LanguagesPopupMenuVisible) {
        ThisPage.LanguagesMenuShow();
    }
}