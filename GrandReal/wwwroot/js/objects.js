
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
			if (data.error)
				alert(data.error);
			else {
				$(elem).addClass("press", 1000);
				$(elem).parent().find('span').addClass("press", 1000);
			}

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
			if (data.error)
				alert(data.error);
			else {
				$(elem).removeClass("press", 1000);
				$(elem).parent().find('span').removeClass("press", 1000);
			}

		}
	});
}
