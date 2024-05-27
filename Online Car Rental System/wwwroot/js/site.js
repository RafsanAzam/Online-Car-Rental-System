document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById("searchInput");
    var suggestionsBox = document.getElementById("searchSuggestions");

    searchInput.addEventListener("input", function () {
        var query = searchInput.value;
        if (query.length === 0) {
            suggestionsBox.classList.add("d-none");
            return;
        }

        fetch(`/Car/GetSuggestions?query=${query}`)
            .then(response => response.json())
            .then(data => {
                suggestionsBox.innerHTML = "";
                if (data.length > 0) {
                    data.forEach(item => {
                        var suggestionItem = document.createElement("div");
                        suggestionItem.className = "suggestion-item";
                        suggestionItem.textContent = item;
                        suggestionItem.addEventListener("click", function () {
                            searchInput.value = item;
                            suggestionsBox.classList.add("d-none");
                        });
                        suggestionsBox.appendChild(suggestionItem);
                    });
                    suggestionsBox.classList.remove("d-none");
                } else {
                    suggestionsBox.classList.add("d-none");
                }
            });
    });

    searchInput.addEventListener("focus", function () {
        if (searchInput.value.length === 0) {
            fetch(`/Car/GetRecentSearches`)
                .then(response => response.json())
                .then(data => {
                    suggestionsBox.innerHTML = "";
                    if (data.length > 0) {
                        data.forEach(item => {
                            var suggestionItem = document.createElement("div");
                            suggestionItem.className = "suggestion-item";
                            suggestionItem.textContent = item;
                            suggestionItem.addEventListener("click", function () {
                                searchInput.value = item;
                                suggestionsBox.classList.add("d-none");
                            });
                            suggestionsBox.appendChild(suggestionItem);
                        });
                        suggestionsBox.classList.remove("d-none");
                    } else {
                        suggestionsBox.classList.add("d-none");
                    }
                });
        }
    });

    searchInput.addEventListener("blur", function () {
        setTimeout(function () {
            suggestionsBox.classList.add("d-none");
        }, 200);
    });
});
