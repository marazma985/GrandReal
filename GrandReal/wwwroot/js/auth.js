
document.addEventListener("DOMContentLoaded", function (event) {
	$('#sendLogin').on("click", login);

	$('header').hide();

	//#region переключатель

	onRegister();

	$(window).keyup(function (e) {
		var target = $('.checkbox-green input:focus');
		if (e.keyCode == 9 && $(target).length) {
			$(target).parent().addClass('focused');
		}
	});

	$('.checkbox-green input').focusout(function () {
		$(this).parent().removeClass('focused');
	});

	$('#loginOrRegister').on('change', onRegister)

	//#endregion
});
onRegister = function () {
	var value = $('#loginOrRegister').is(":checked");
	$("#sendLogin").off("click");
	if (value) {
		$('.registerForm').show();
		$('#sendLogin').text('Зарегистрироваться');
		$('#sendLogin').on("click", register);
	}
	else {
		$('.registerForm').hide();
		$('#sendLogin').text('Войти');
		$('#sendLogin').on("click", login);
	}
		
}
login = function () {
	var dataFrom = getObjectFromForm($('.form'));
	$.ajax({
		url: '/Auth/login',
		method: 'post',
		data: dataFrom ,
		success: function (data) {
			if (data.error)
				alert(data.error);
			else {
				localStorage.setItem('userName', `${data.surnameClient} ${data.nameClient[0]}. ${data.patronymicClient[0]}.`);
				document.cookie = `idUser=${data.idClient}`;
				window.location.href = '/Objects'
			}
			
		}
	});
}
register = function () {
	var dataFrom = getObjectFromForm($('.form'));
	$.ajax({
		url: '/Auth/CreateClient',
		async: true,
		method: 'post',
		data: {
			client: dataFrom
		},
		success: function (data) {
			alert(data);
		}
	});
}
getObjectFromForm = function (form) {

	var obj = {};

	$(form).find('input[name],select[name],textarea[name]').each((i, e) => {
		var name = $(e).attr('name');
		obj[name] = $(e).val();
	})

	return obj;

}