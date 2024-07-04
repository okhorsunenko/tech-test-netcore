document.addEventListener('DOMContentLoaded', function () {
    function fetchWithTimeout(url, options = {}, timeout = 5000) {
        return new Promise((resolve, reject) => {
            const timer = setTimeout(() => {
                reject(new Error('Request timed out'));
            }, timeout);

            fetch(url, options)
                .then(response => {
                    clearTimeout(timer);
                    resolve(response);
                })
                .catch(error => {
                    clearTimeout(timer);
                    reject(error);
                });
        });
    }
    function fetchGravatarProfile(email, element) {
        const url = `https://gravatar.com/${email}.json`;

        fetchWithTimeout(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                    console.log("Issue with Gravatar service");
                }
                return response.json();
            })
            .then(data => {
                const displayName = data.entry[0].displayName || '<No name>';
                element.textContent = displayName;
            })
            .catch(() => {
                element.textContent = '<No name>';
            });
    }

    document.querySelectorAll('.display-name').forEach(function (element) {
        const email = element.getAttribute('data-email');
        fetchGravatarProfile(email, element);
    });
});