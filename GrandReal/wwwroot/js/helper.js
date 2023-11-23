myModalAlert = function (text, callback, titleText) {
    //спрятано ли модальное окно
    var hasClass = $('.myModalBackground').hasClass("myModalHide");
    if (!hasClass) {
        //если нет запускает анимацию закрытия 
        //и запускает рекурсию пока оно не спрячется что бы вызвать его с новыми данными
        setTimeout(closeMyAlert, 350)
        setTimeout(function () { myModalAlert(text, callback, titleText) }, 900)
    }
    if (hasClass) {
        //Присваивает текст и заголовок и создаёт события на кнопку "Продолжить" если передан callback 
        if (text)
            $('.bodyModal div').html(text);

        if (titleText)
            $('.titleModal h4').text(titleText);

        if (typeof (callback) == typeof (function () { })) {
            $('.footerModal button.btn-primary').off('click');
            $('.footerModal button.btn-primary').click(callback);
        }

        $('.footerModal button.btn-primary').click(closeMyAlert);
        $('.myModalBackground').addClass('myModalShow');
        $('.myModalBackground').removeClass('myModalHide');
    }
    //TODO:Я бы доработал отображение кнопок, порой не нужна кнопка "Отмена"
    //Модально окно находится в _Layout.cshtml
}
//Функция сокрытия модального окна с анимацией
closeMyAlert = function () {
    var hasClass = $('.myModalBackground').hasClass("myModalShow");
    if (hasClass) {
        $('.myModalBackground').addClass('myModalHide');
        $('.myModalBackground').removeClass('myModalShow');
        
    }
    //Скрывает гиф загрузки после скрытия окна полностью
    setTimeout(function () { $('#loadGif').hide(); }, 350)
    

}
//Проверяет заполненость дочерних инпутов с тегом required родитель передётся в параметре
checkInputRequired = function (parentItem) {
    //генерирует сообщение об ошибке
    var errorMessage = "Не все поля заполнены:</br>";
    var errorInputs = "";
    //Ищет не заполненые поля и если не заполнено апает сообщение об ошибке с название поля из placeholder или рядом стоящего label
    $(parentItem).find('input[required]').each((i,e) => {
        if (!$(e).val() && !!e.offsetParent) {
            var labelInp = $(e).parent().find('label').text();
            if (labelInp) {
                errorInputs += `<li>${labelInp}</li>`;
                return;
            }
            if ($(e).attr('placeholder')) 
                errorInputs += $(e).attr('placeholder') + "</br>";
        }

    })
    //если есть ошибочные поля добавляет их к тексту ошибки в список и выводит
    if (errorInputs) {
        errorMessage += "<ul>";
        errorMessage += errorInputs;
        errorMessage += "</ul>";
        myModalAlert(errorMessage);
        return false;
    }
    return true;

}
//Функция получения объекта из формы имя свойства берётся из атрибута name
getObjectFromForm = function (form) {

    var obj = {};

    $(form).find('input[name],select[name],textarea[name]').each((i, e) => {
        var name = $(e).attr('name');
        obj[name] = $(e).val();
    })

    return obj;

}