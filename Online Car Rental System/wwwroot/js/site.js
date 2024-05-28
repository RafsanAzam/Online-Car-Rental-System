document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById("searchInput");
    var suggestionsBox = document.getElementById("searchSuggestions");

    // Load Search history from local storage
    function loadSearchHistory() {
        return JSON.parse(localStorage.getItem("searchHistory")) || [];
    }

    //Save Search history to local storage
    function saveSearchHistory(query) {
        var history = loadSearchHistory();
        if (!history.includes(query)) {
            history.unshift(query); //  Add to the beginning of the array
            if (history.length > 10) history.pop(); // Limit to last 10 searches
            localStorage.setItem("searchHistory", JSON.stringify(history));
        }
    }

    // Display search history suggestions
    function displaySearchHistory() {
        var history = loadSearchHistory();
        suggestionsBox.innerHTML = "";
        if (history.length > 0) {
            history.forEach(function (item) {
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
    }

    // Save search query when input changes
    searchInput.addEventListener("input", function () {
        var query = searchInput.value;
        if (query.length === 0) {
            suggestionsBox.classList.add("d-none");
            return;
        }

        saveSearchHistory(query); // New line to save the search query

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

    // Display search history on focus if no input
    searchInput.addEventListener("focus", function () {
        if (searchInput.value.length === 0) {
            displaySearchHistory(); // Updated to use the local search history function
        }
        else {
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

    // Clear suggestions on blur
    searchInput.addEventListener("blur", function () {
        setTimeout(function () {
            suggestionsBox.innerHTML = "";
            suggestionsBox.classList.add("d-none");
        }, 200);
    });
    // Save the search history when the search form is submitted
    document.querySelector("form").addEventListener("submit", function (event) {
        saveSearchHistory(searchInput.value);
        //return true; // Allow the form to submit
    });
});
