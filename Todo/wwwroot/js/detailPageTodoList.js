document.addEventListener("DOMContentLoaded", function () {
    var toggleButton = document.getElementById("toggleDoneItems");
    var isHidden = false;

    toggleButton.addEventListener("click", function () {
        var items = document.querySelectorAll("li[data-item-is-done='True']");
        items.forEach(function (item) {
            if (isHidden) {
                item.style.display = "";
            } else {
                item.style.display = "none";
            }
        });

        isHidden = !isHidden;
        toggleButton.textContent = isHidden ? "Show Done Items" : "Hide Done Items";
    });
});