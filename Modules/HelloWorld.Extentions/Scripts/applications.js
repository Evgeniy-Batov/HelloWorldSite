$(document).ready(function () {

    function buildAppInfo() {
        var result =  
        {
            appId:       $('input#Application_MessageId').val(),
            accessToken: $('input#Application_AccessToken').val()
        }
        return result;
    }

    $('a.confirmTrigger').click(function () {
        $("#confirm-dialog-confirm").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Подтверждаю": function () {
                    var that = this;
                    $.post('/Contact/ConfirmRegistration', buildAppInfo(), function () {
                        $(that).dialog("close");
                        //$.amaran({
                        //    content: {
                        //        title: 'Подтверждение анкеты',
                        //        message: 'Подтверждение Вашей регистрации прошло успешно. Мы отправили письмо с деталями Вам на почту.'
                        //    },
                        //    theme: 'blur'
                        //});
                        $('a.confirmTrigger').hide();
                        $('label.appStatusLbl').text('Анкета подтвреждена');

                        $("#confirm-complete-dialog").dialog({
                            modal: true,
                            buttons: {
                                Ok: function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    });
                },
                "Не подтверждаю": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
    
    $('a.removeTrigger').click(function () {
        $("#cancel-dialog-confirm").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Отмена регистрации": function () {
                    var that = this;
                    $.post('/Contact/CancelRegistration', buildAppInfo(), function () {
                        $(that).dialog("close");
                        $.amaran({
                            content: {
                                title: 'Отмена регистрации',
                                message: 'Ваша анкета удалена!'
                            },
                            theme: 'blur'
                        });
                        window.location.href = '/'
                    });
                },
                "Закрыть": function () {
                    $(this).dialog("close");
                }
            }
        });
    });


    $('select#Application_SelectedCourseId').change(function () {
        $.get('/Contact/FlowsByCourse', { courseId: $(this).val() }, function (response) {
            var sel$ = $('select#Application_SelectedGroupId');
            sel$.empty()
            .append('<option selected="selected" value="">Выберите группу</option>')
            $.each(response, function (i, obj) {
                sel$.append('<option value="' + obj.FlowId + '">' + obj.CustomName + '</option>');
            });
        });
    });


    $(".anyTimeCheck").change(function () {
        if (this.checked) {
            $(".days :input").attr("disabled", true);
        }
        else {
            $(".days :input").attr("disabled", false);
        }
    });

    if ($(".anyTimeCheck").prop('checked')) {
        $(".days :input").attr("disabled", true);
    }


    //Recaptcha._alias_finish_reload = Recaptcha.finish_reload;
    //Recaptcha.finish_reload = function (challenge, b, c) {
    //    //Do stuff with challenge here
    //    $(".signupform").validate();
    //    $('input#recaptcha_response_field').rules("add",
    //        {
    //            required: true,
    //            messages: {
    //                required: 'Введите контрольные слова'
    //            }
    //        });
    //    Recaptcha._alias_finish_reload(challenge, b, c);
    //}


    var url = window.location.href;
    var lastPart = url.substr(url.lastIndexOf('/') + 1);

    if (lastPart === "Confirm") {
        $('a.confirmTrigger').trigger('click');
    }
    else if (lastPart === "Cancel") {
        $('a.removeTrigger').trigger('click');
    }
});