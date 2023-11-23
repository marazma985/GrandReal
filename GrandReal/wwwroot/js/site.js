// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = (event) => {
    //обработчик события на вход
    $('#toLogin').on('click', function () { clearLocalStorage(); window.location.href = '/Auth' })
}
clearLocalStorage = function () {
    //очищает куки и докальное хранилище
    cookieStore.delete("idUser");
    for (key in localStorage) {
        if (typeof (localStorage[key]) != typeof (function () { }))
            localStorage[key] = null;
    }
}
document.addEventListener("DOMContentLoaded", function (event) {
    //Если есть имя в локальном хранилище то выводит его
    if (localStorage.userName)
        $('#userName').text(localStorage.userName);
});