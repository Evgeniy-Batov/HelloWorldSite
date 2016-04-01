String.prototype.endsWith = function(suffix) {
    return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

$(function () {

    $( "#tabs" ).tabs();

    var selectedCourseId;

    var addTo = function (email) {
        var toElement = $('#cTo');

        if (toElement.val() == ''){
            toElement.val(email);
        }
        else if (toElement.val().endsWith(';')) {
            toElement.val(toElement.val() + email);
        }
        else {
            toElement.val(toElement.val() + ';' + email);
        }
    };

    var addCC = function () {
    };


    $('input#btnAllEmails').click(function () {
        $('#contactlist option').each(function (i, selected) {
            addTo($(selected).val())
        });
    });

    $('input#btnSelEmails').click(function () {
        $('#contactlist :selected').each(function (i, selected) {
            addTo( $(selected).val())
        });
    });

    $('input#btnAllStudents').click(function () {
        $('#studentslist option').each(function (i, selected) {
            addTo($(selected).val())
        });
    });

    $('input#btnSelStudents').click(function () {
        $('#studentslist :selected').each(function (i, selected) {
            addTo($(selected).val())
        });
    });


    $('select#courseSelector').change(function () {
        selectedCourseId = $(this).val();
        $.get('/Contact/FlowsByCourse?withHistory=true', { courseId: $(this).val() }, function (response) {
            var sel$ = $('select#cFlowSelector');
            sel$.empty()
            .append('<option selected="selected" value="">Выберите группу</option>')
            .append('<option value="-1">Все группы</option>')
            $.each(response, function (i, obj) {
                sel$.append('<option value="' + obj.FlowId + '">' + obj.CustomName + '</option>');
            });
        });
    });

    $('select#cFlowSelector').change(function () {
        var sel$ = $('select#contactlist');
        sel$.empty();
        var selectedFlow = $(this).val();

        var studentsList = $('select#studentslist');
        studentsList.empty();

        $.get('/Contact/ApplicationsByFlow?courseId=' + selectedCourseId + '&flowId=' + selectedFlow, { courseId: selectedCourseId, flowId: selectedFlow }, function (response) {
            $.each(response, function (index, value) {
                sel$.append('<option title="'+ value.Email + '" value="' + value.Email + '">' + value.LastName + ' ' + value.Name + '</option>');
            });
        });

        $.get('/Manager/StudentsByFlow', { flowId: selectedFlow }, function (response) {
            $.each(response, function (index, value) {
                studentsList.append('<option title="' + value.Email + '" value="' + value.Email + '">' + value.LastName + ' ' + value.FirstName + '</option>');
            });
        });
    });

    $('#emailsendtrigger').click(function () {
        var reqData =
            {
                To: $('#cTo').val(),
                CC: $('#cCC').val(),
                Subj: $('#cSubj').val(),
                Body:tinyMCE.activeEditor.getContent()
            }

        $.ajax({
            url: '/Manager/SendEmails',
            type: 'POST',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data:  JSON.stringify({ model: reqData }),
            success: function () {
                $('#cTo').val('');
                $('#cCC').val('');
                $('#cSubj').val('');
                $('#cBody').val('');
                alert(arguments[0].Message);
            },
            error: function () {
                alert('Ошибка создания');
            }
        });
        return false;
    });


    var editorObj = tinymce.init({
        selector: "textarea#cBody",
        height: 300,
        plugins: [
       "advlist autolink lists link image charmap print preview anchor",
       "searchreplace visualblocks code fullscreen",
       "insertdatetime media table contextmenu paste"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        toolbar2: "print preview media | forecolor backcolor emoticons",
        image_advtab: true,
        relative_urls: false,
        valid_elements: "*[*]",
        forced_root_block: false//,
        //templates: [
        //    { title: 'Test template 1', content: 'Test 1' },
        //    { title: 'Test template 2', content: 'Test 2' }
        //]
    });
});