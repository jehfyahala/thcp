//nuevo
function positions_search() {
    //variables
    const search = document.getElementById("txt-search-positions").value;

    const currentURL = window.location.href.substring(
        window.location.href.lastIndexOf('/') + 1
    ).split("?")[0];

    window.location = `${currentURL}?search=${search}`;

}

const positions_input_search = document.getElementById("txt-search-positions");

positions_input_search.addEventListener("keyup", function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        positions_search();
    }
});