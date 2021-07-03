//nuevo
function employees_search(){
    //variables
    const search = document.getElementById("txt-search-employees").value;

    const currentURL = window.location.href.substring(
        window.location.href.lastIndexOf('/') + 1
    ).split("?")[0];

    window.location = `${currentURL}?search=${search}`;

}

const employees_input_search = document.getElementById("txt-search-employees");

employees_input_search.addEventListener("keyup", function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        employees_search();
    }
});