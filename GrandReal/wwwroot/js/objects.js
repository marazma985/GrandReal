
document.addEventListener("DOMContentLoaded", function (event) {
    $(function () {
		$(".liked i").on('click', function () {
			var idObject = Number($(this).parent().attr('id').replace('likedObject-', ''));
			likeOrRemoveLikeObject(this, idObject);
            
        });
    });
});
likeOrRemoveLikeObject = function (elem, idObject) {
	if (!$(elem).hasClass("press"))
		likeObject(elem, idObject);
	else
		removeLikeObject(elem, idObject);
}
likeObject = function (elem, idObject) {
	$.ajax({
		url: '/Objects/LikeObject',
		method: 'post',
		data: {
			idObject: idObject
		},
		success: function (data) {
			
			if(data.message) {
				$(elem).addClass("press", 1000);
				$(elem).parent().find('span').addClass("press", 1000);
			}
			else if (data.error)
				myModalAlert(data.error);
			else
				window.location.href = '/Auth'

		}
	});
}
removeLikeObject = function (elem, idObject) {
	$.ajax({
		url: '/Objects/RemoveLikeObject',
		method: 'post',
		data: {
			idObject: idObject
		},
		success: function (data) {
			if (data.message) {
				$(elem).removeClass("press", 1000);
				$(elem).parent().find('span').removeClass("press", 1000);
			}
			else if (data.error)
				myModalAlert(data.error);
			else
				window.location.href = '/Auth'

		}
	});
}
submitApplication = function (idObject, elem) {
	$.ajax({
		url: '/Objects/SubmitApplication',
		method: 'post',
		data: {
			idObject: idObject
		},
		beforeSend: function () {
			myModalAlert('<img id="loadGif" src="/img/load.gif" /> Обработка запроса...')
		},
		success: function (data) {
			closeMyAlert();
			
			if (data.message) {
				myModalAlert(data.message);
				$(elem).attr('disabled','dusabled')
				$(elem).text('Заявка оставлена')
			}
				
			else if (data.error)
				myModalAlert(data.error);
			else
				window.location.href = '/Auth'

		}
	});
}
