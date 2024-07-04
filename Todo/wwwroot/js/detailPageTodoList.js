document.addEventListener("DOMContentLoaded", function () {
    // Show/hide done items
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

    // Updating item rank
    function updateItemRank(itemId, newRank) {
        fetch(`/api/todoitems/${itemId}/rank`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newRank)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                window.location.reload();
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }

    document.querySelectorAll('.update-rank-btn').forEach(button => {
        button.addEventListener('click', function () {
            const itemId = this.getAttribute('data-item-id');
            const inputField = document.querySelector(`.rank-input[data-item-id="${itemId}"]`);
            const newRank = parseInt(inputField.value);
            updateItemRank(itemId, newRank);
        });
    });

    // Sorting by rank Asc/Desc
    const sortAsc = document.getElementById('sortAsc');
    const sortDesc = document.getElementById('sortDesc');
    const list = document.querySelector('.list-group');

    function sortItems(ascending = true) {
        let items = [...list.children];
        items.sort((one, two) => {
            let rankOne = parseInt(one.dataset.rank);
            let rankTwo = parseInt(two.dataset.rank);
            return ascending ? rankOne - rankTwo : rankTwo - rankOne;
        });

        items.forEach(item => list.appendChild(item));
    }

    sortAsc.addEventListener('click', () => sortItems(true));
    sortDesc.addEventListener('click', () => sortItems(false));
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
        })
        .catch((error) => {
            console.error('Error:', error);
        });
});