
document.addEventListener("DOMContentLoaded", function (event) {
	$('#sendLogin').on("click", login);


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
		//
		$('.registerForm').show();
		$('#sendLogin').text('...');
		$('.registerForm').addClass('h-auto-transition');
		$('.registerForm').removeClass('h-none-transition');
		
		$('#sendLogin').on("click", register);

		setTimeout(function () {
			$('#sendLogin').text('Зарегистрироваться');
		}, 500)
	}
	else {
		$('.registerForm').addClass('h-none-transition');
		$('.registerForm').removeClass('h-auto-transition');
		
		$('#sendLogin').text('...');
		$('#sendLogin').on("click", login);
		setTimeout(function () {
			$('.registerForm').hide();
			$('#sendLogin').text('Войти');
		}, 500)
	}
		
}
login = function () {
	if (!checkInputRequired($('.form')))
		return;

	var dataFrom = getObjectFromForm($('.form'));
	$.ajax({
		url: '/Auth/login',
		method: 'post',
		data: dataFrom,
		beforeSend: function () {
			myModalAlert('<img id="loadGif" src="~/img/load.gif" /> Обработка запроса...')
		},
		success: function (data) {

			if (data.error) {
				myModalAlert(data.error);
			}
			else {
				localStorage.setItem('userName', `${data.surnameClient} ${data.nameClient[0]}. ${data.patronymicClient[0]}.`);
				document.cookie = `idUser=${data.idClient}`;
				window.location.href = '/Objects'
			}
			
		}
	});
}
register = function () {
	if (!checkInputRequired($('.form')))
		return;

	var dataFrom = getObjectFromForm($('.form'));
	$.ajax({
		url: '/Auth/CreateClient',
		async: true,
		method: 'post',
		data: {
			client: dataFrom
		},
		beforeSend: function () {
			myModalAlert('<img id="loadGif" src="~/img/load.gif" /> Обработка запроса...')
		},
		success: function (data) {
			myModalAlert(data);
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
