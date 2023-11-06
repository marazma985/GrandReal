// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = (event) => {
    $('#toLogin').on('click', function () { clearLocalStorage(); window.location.href = '/Auth' })
}
clearLocalStorage = function () {
    cookieStore.delete("idUser");
    for (key in localStorage) {
        if (typeof (localStorage[key]) != typeof (function () { }))
            localStorage[key] = null;
    }
}
document.addEventListener("DOMContentLoaded", function (event) {
    if (localStorage.userName)
        $('#userName').text(localStorage.userName);
});