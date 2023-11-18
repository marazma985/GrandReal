myModalAlert = function (text, callback, titleText) {
    var hasClass = $('.myModalBackground').hasClass("myModalHide");
    if (!hasClass) {
        setTimeout(closeMyAlert, 350)
        setTimeout(function () { myModalAlert(text, callback, titleText) }, 900)
    }
    if (hasClass) {
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
}
closeMyAlert = function () {
    var hasClass = $('.myModalBackground').hasClass("myModalShow");
    if (hasClass) {
        $('.myModalBackground').addClass('myModalHide');
        $('.myModalBackground').removeClass('myModalShow');
        
    }
    setTimeout(function () { $('#loadGif').hide(); }, 350)
    

}
checkInputRequired = function (parentItem) {
    var errorMessage = "Не все поля заполнены:</br>";
    var errorInputs = "";
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
    if (errorInputs) {
        errorMessage += "<ul>";
        errorMessage += errorInputs;
        errorMessage += "</ul>";
        myModalAlert(errorMessage);
        return false;
    }
    return true;

}