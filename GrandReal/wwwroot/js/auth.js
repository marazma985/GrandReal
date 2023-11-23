//Авторизация: тут вроде всё норм
//единственное я бы сохранял логин и хешированный пароль в куки для отправки на сервер

document.addEventListener("DOMContentLoaded", function (event) {
	$('#sendLogin').on("click", login);

	
	//инициализация 
	onRegister();
	//#region переключатель регистрации и входа
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
//для переключения - смена функций и стилей
onRegister = function () {
	//Получает значения переключателя и отключает кнопку отправки запроса
	var value = $('#loginOrRegister').is(":checked");
	$("#sendLogin").off("click");
	$('#sendLogin').text('...');
	if (value) {
		//Показывает форму регистрации (доп инпуты)
		$('.registerForm').show();
		$('.registerForm').addClass('h-auto-transition');
		$('.registerForm').removeClass('h-none-transition');

		//присваевает функцию на отправку
		$('#sendLogin').on("click", register);

		setTimeout(function () {
			//Задаёт текст кнопки после завершения анимации
			$('#sendLogin').text('Зарегистрироваться');
		}, 500)
	}
	else {
		//примерно аналогично чем код выше только в обратном порядке
		$('.registerForm').addClass('h-none-transition');
		$('.registerForm').removeClass('h-auto-transition');
		
		$('#sendLogin').on("click", login);
		setTimeout(function () {
			$('.registerForm').hide();
			$('#sendLogin').text('Войти');
		}, 500)
	}
		
}
//запрос авторизации
login = function () {
	//вызов функции проверки заполненности полей см. в helper.js
	if (!checkInputRequired($('.form')))
		return;

	//вызов функции сбора данных полей в объект см. в helper.js
	var dataFrom = getObjectFromForm($('.form'));
	$.ajax({
		url: '/Auth/login',
		method: 'post',
		data: dataFrom,
		beforeSend: function () {
			//моё модальное окно см. в helper.js
			myModalAlert('<img id="loadGif" src="/img/load.gif" /> Обработка запроса...')
		},
		success: function (data) {

			if (data.error) {
				myModalAlert(data.error);
			}
			else {
				//сохраняет в локальное хранилище ФИО и в куки ИД юзера для авторизации на каждом запросе к серверу
				localStorage.setItem('userName', `${data.surnameClient} ${data.nameClient[0]}. ${data.patronymicClient[0]}.`);
				document.cookie = `idUser=${data.idClient}`;
				window.location.href = '/Objects'
			}
			
		}
	});
}
//запрос регистрации 
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
			myModalAlert('<img id="loadGif" src="/img/load.gif" /> Обработка запроса...')
		},
		success: function (data) {
			myModalAlert(data);
		}
	});
}

