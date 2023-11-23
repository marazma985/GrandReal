
document.addEventListener("DOMContentLoaded", function (event) {

    //Вешает всем импутам с классом numbersOnly обрезание символов и добавляет пробелы для разделения например (1 000 000) - каждые 3 символа с конца
    document.querySelectorAll(".numbersOnly").forEach(function (elem) {
        elem.addEventListener('input', function (event) {
            if (!parseInt(event.data) && event.data != "0" && event.data != null)
                this.value = this.value.substring(0, this.value.length - event.data.length);
            const num = this.value.replace(/\s/g, '');
            if (parseInt(num))
                this.value = (parseInt(num)).toLocaleString('ru-Ru');
        });
    })
});