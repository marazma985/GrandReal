myModalAlert = function (text, callback, titleText) {
    var hasClass = $('.myModalBackground').hasClass("myModalHide");
    if (hasClass) {
        if (text)
            $('.bodyModal p').text(text);

        if (titleText) 
            $('.titleModal h4').text(titleText);

        if (typeof (callback) == typeof (function () { })) {
            $('.footerModal button.btn-primary').off('click');
            $('.footerModal button.btn-primary').click(callback);
        }

        $('.footerModal button.btn-primary').click(closeMyAlert);

        $('.myModalBackground').removeClass('myModalHide');
        $('.myModalBackground').addClass('myModalShow');
    }
}
closeMyAlert = function () {
    var hasClass = $('.myModalBackground').hasClass("myModalShow");
    if (hasClass) {
        $('.myModalBackground').removeClass('myModalShow');
        $('.myModalBackground').addClass('myModalHide');
    }
}