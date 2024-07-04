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

document.getElementById("addItemForm").addEventListener("submit", function (e) {
    e.preventDefault();

    var formData = new FormData(this);
    var jsonData = {};
    formData.forEach((value, key) => { jsonData[key] = value; });
    console.log(formData);
    console.log(jsonData);

    jsonData["Importance"] = parseInt(jsonData["Importance"]);

    fetch('/api/todoitems', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(jsonData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Success:', data);
            window.location.reload();
            // Optionally, refresh the list or add the new item directly to the DOM
        })
        .catch((error) => {
            console.error('Error:', error);
        });
});