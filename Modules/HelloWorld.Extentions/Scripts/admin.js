$(function () {
    
    $(window).bind('resize', function () {

        $('table').setGridWidth($(window).width() - 50);
        $('table').setGridHeight($(window).height() - 350);

    });

    $("#tabs").tabs();

    $('#nestedtabs').tabs({ heightStyle: "auto" });
    $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left").addClass("ui-corner-right");
    //$("div#onlinesupport").onlinechatmanager();

    $(document).on('focusin', function (e) {
        if ($(e.target).closest(".mce-window").length) {
            e.stopImmediatePropagation();
        }
    });

    $('a.newFeedbackLink').click(function () {
        addFeedbackHandler();
    });

    $('a.newGroupLink').click(function () {
        addItemHandler();
    });

    $('a.newCourseLink').click(function () {
        addCourseHandler();
    }); 

    $('a.newMaingDetailLink').click(function () {
        addMainPageItemHandler();
    });

    if (typeof (tinymce) != "undefined") {

        var editorObj = tinymce.init({
            selector: "textarea.heditor",
            height: 300,
            plugins: [
           "advlist autolink lists link image charmap print preview anchor",
           "searchreplace visualblocks code fullscreen",
           "insertdatetime media table contextmenu paste"
            ],
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
            toolbar2: "print preview media | forecolor backcolor emoticons",
            image_advtab: true,
            cleanup: false,
            relative_urls: false,
            valid_elements: "*[*]",
            forced_root_block: false,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ]
        });
    }

    $("div#tabs").on('click', '.localhtmleditortrigger', function (e) {
        var boundElement = $('#' + $(this).attr('data-bound-element'),$(this).parent());
        var oldValue = boundElement.val();
        tinyMCE.activeEditor.setContent(oldValue);
        $('div#html-edit-dialog-form').one('dialogclose', function (event) {
            boundElement.val(tinyMCE.activeEditor.getContent());
        });
        $("div#html-edit-dialog-form").dialog("open");
    });

    $('.htmleditortrigger').click(function ()
    {
        var boundElement = $('#' + $(this).attr('data-bound-element'));
        var oldValue = boundElement.val();
        tinyMCE.activeEditor.setContent(oldValue);
        $('div#html-edit-dialog-form').one('dialogclose', function (event) {
            boundElement.val(tinyMCE.activeEditor.getContent());
        });
        $("div#html-edit-dialog-form").dialog("open");
    });


    $("#maindetailstable").jqGrid({
        url: 'manager/MainPageItems',
        autowidth:true,
        datatype: "json",
        colNames: ['#', 'Номер','Экшн', 'Параметры', 'Контроллер', 'Изображение','CSS','Текст','Заголовок','Действия'],
        colModel: [
            { name: 'Id', index: '#', width: 40},
            { name: 'Order', index: 'Номер', width: 50},
            { name: 'DestinationAction', index: 'Экшн', width: 50 },
            { name: 'DestinationActionParams', index: 'Параметры', width: 80 },
            { name: 'DestinationController', index: 'Контроллер', width: 80},
            { name: 'ImgUrl', index: 'Изображение', width: 80 },
            { name: 'ItemCss', index: 'CSS', width: 35 },
            { name: 'ItemText', index: 'Текст', width: 35 },
            { name: 'ItemTitle', index: 'Заголовок', width: 80 },
            { name: '', index: 'Действия',formatter: pageItemsFormatter, width: 100 }
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        pager: '#maindetailstablepager',
        sortname: 'id',
        viewrecords: true,
        sortorder: "acs",
        caption: "Элементы главной страницы"
    });
    jQuery("#maindetailstable").jqGrid('navGrid', '#maindetailstablepager', { search: false, edit: false, add: false, del: false });

    $("#list2").jqGrid({
        url: 'manager/offlinemessages',
        datatype: "json",
        colNames: ['#', 'Дата', 'Имя', 'Email', 'Тел', 'Тема', 'Сообщение'],
        colModel: [
            { name: 'MessageId', index: '#', width: 35 },
            { name: 'PostedOn', index: 'Дата', formatter: "date", formatoptions: { newformat: "m/d/Y" }, width: 71, sortable: false },
            { name: 'Name', index: ' Имя', width: 100, sortable: false },
            { name: 'Email', index: 'Email', formatter: emailFormat, width: 150, align: "right", sortable: false },
            { name: 'Phone', index: 'Тел', width: 100, align: "right", sortable: false },
            { name: 'Topic', index: 'Тема', width: 80, align: "right", sortable: false },
            { name: 'Message', index: 'Сообщение', width: 320, sortable: false, cellattr: function (rowId, tv, rawObject, cm, rdata) { return 'style="white-space: nowrap;' } }
        ],
        rowNum: 10,
        rowList: [10, 20, 30,50],
        pager: '#pager2',
        sortname: 'id',
        viewrecords: true,
        sortorder: "desc",
        caption: "Пользовательские off line сообщения",
        autowidth: false,
        width: $(window).width() - 50,
        height: $(window).height() - 350,
        ondblClickRow: function (id) {
            $(this).jqGrid('viewGridRow', id, { caption: "Сообщение пользователя" });
        }
    });
    jQuery("#list2").jqGrid('navGrid', '#pager2', { search:false, edit: false, add: false, del: false });

    function emailFormat(cellvalue, options, rowObject) {
        return cellvalue;//'<a href=mailto:"' + cellvalue + '">' + cellvalue + '<a/>';
    }

    function pageItemsFormatter(cellvalue, options, rowObject)
    {
        return '<a class="editLink"  data-id=' + rowObject.GroupId + ' href=#">' + 'Edit' + '<a/>' + ' ' + '<a class="removeLink" data-id=' + rowObject.GroupId + ' href=#>' + 'Remove' + '<a/>';
    }

    function detailsFormatter(cellvalue, options, rowObject) 
    {
        return '<a class="editInfoLink"  data-id=' + rowObject.CourseId + ' href=#">' + 'Info' + '</a>' +' ' +'<a class="editPriceLink" data-id='+rowObject.CourseId + ' href=#>Price</a>';
    }

    function actionsFormatter(cellvalue, options, rowObject) {
        return '<a class="editLink"  data-id=' + rowObject.GroupId + ' href=#">' + 'Edit' + '<a/>' + ' ' + '<a class="removeLink" data-id=' + rowObject.GroupId + ' href=#>' + 'Remove' + '<a/>' + ' ' + '<a class="editSEOLink"  data-id=' + rowObject.GroupId + ' href=#>' + 'SEO' + '<a/>';
    }

    function feedbackactionsFormatter(cellvalue, options, rowObject) {
        return '<a class="editLink"  data-id=' + rowObject.GroupId + ' href=#">' + 'Edit' + '<a/>' + ' ' + '<a class="removeLink" data-id=' + rowObject.GroupId + ' href=#>' + 'Remove' + '<a/>' + ' ';
    }

    var reloader = function () {

        var courseId = $(this).val();
        var panel = $('div#floweditpanel');

        $('a.newFlowTrigger').css('visibility', 'visible');
        $('a.newFlowTrigger').attr('data-course-id', courseId);

        $('table', panel).jqGrid('GridDestroy');
        panel.append('<table id="viewflows"></table>');

        $('table', panel).jqGrid({
            url: '/Contact/FlowsByCourse?withHistory=true&courseId=' + courseId,
            datatype: "json",
            colNames: ['#', 'Дата начала', 'Дата окончания', 'ID курса', 'Название', 'Ориентир. дата начала', 'Дата начала', 'Статус', 'Операции'],
            colModel:
            [
                { name: 'FlowId', index: '#', key: true, width: 20, sortable: false },
                { name: 'ActualStartDate', index: 'Дата начала', hidden: true, formatter: "date", formatoptions: { newformat: "d/m/Y" }, width: 100, sortable: false },
                { name: 'ActualEndDate', index: 'Дата окончания', hidden: true, formatter: "date", formatoptions: { newformat: "d/m/Y" }, width: 100, sortable: false },
                { name: 'CourseId', index: 'ID курса', hidden: true, sortable: false },
                { name: 'CustomName', index: 'Название', width: 250, sortable: false },
                { name: 'EstimatedStartDate', formatter: "date", formatoptions: { newformat: "d/m/Y" }, index: 'Ориентир. дата', width: 100, sortable: false },
                { name: 'StartDateStr', index: 'Дата начала', width: 100, sortable: false },
                { name: 'StatusStr', index: 'Статус', width: 100, sortable: false },
                { name: '', index: 'Действия', formatter: flowItemsFormatter, width: 100 }
            ],
            sortname: 'FlowId',
            celledit: false,
            viewrecords: true,
            sortorder: "asc",
            caption: "Потоки",
            recreateForm: true,
            autowidth: true,
            height: $(window).height() - 350,
        });

        function flowItemsFormatter(cellvalue, options, rowObject) {
            return '<a class="editFlowLink" data-id=' + rowObject.FlowId + ' href=#>' + 'Edit' + '<a/>' + ' ' + '<a class="removeFlowLink" data-id=' + rowObject.FlowId + ' href=#>' + 'Remove' + '<a/>';
        }
    };

    $('select#courseSelector2').change(reloader);
    $('button.reloader').click(function () {
        flowReloader.apply($('select#flowSelector'));
    });

    $('select#courseSelector').change(function () {

        var panel = $('div#registrationsPanel');
        $('table', panel).jqGrid('GridDestroy');

        $.get('/Contact/FlowsByCourse?withHistory=true', { courseId: $(this).val() }, function (response) {
            var sel$ = $('select#flowSelector');
            sel$.empty()
            .append('<option selected="selected" value="">Выберите группу</option>')
            .append('<option value="-1">Все группы</option>')
            $.each(response, function (i, obj) {
                sel$.append('<option value="' + obj.FlowId + '">' + obj.CustomName + '</option>');
            });
        });
    });

    var courseReloader = function () {

    };


    function signupOpFormatter(cellvalue, options, rowObject)
    {
        return '<a target="_blank"  href= "/Contact/EditApplication?appId=' + rowObject.MessageId + '&accessT=' + rowObject.AccessTokenEncoded + '">Анкета<a/> <a class="flowchangetrigger" data-application-id="' + rowObject.MessageId + '"data-flow-id="' + rowObject.FlowId + '" href="#">Поток<a/>';
    }
    
    var flowReloader = function () {
        var courseId = +$('select#courseSelector').val();
        var flowId = $(this).val();

        //$.get('/Contact/ApplicationsByFlow?courseId=' + courseId + '&flowId=' + flowId, function (response) {
        //re- create grid 
        var panel = $('div#registrationsPanel');
        $('table', panel).jqGrid('GridDestroy');
        panel.append('<table id="viewsignups"></table>');

        $('table', panel).jqGrid({
            url: '/Contact/ApplicationsByFlow?courseId=' + courseId + '&flowId=' + flowId,
            datatype: "json",
            colNames: ['#', 'Дата', 'Фамилия', 'Имя', 'Email', 'Телефон', 'Комметарий', 'Операции'],
            colModel:
            [
                { name: 'MessageId', index: '#', key: true, width: 25 },
                { name: 'PostedOn', index: 'Дата', formatter: 'date', width: 80 },
                { name: 'LastName', index: 'Фамилия', width: 100 },
                { name: 'Name', index: 'Имя', width: 100 },
                { name: 'Email', index: 'Email', width: 150 },
                { name: 'Phone', index: 'Телефон', width: 120 },
                { name: 'Message', index: 'Комметарий' },
                { name: '', index: 'Операции', width: 85, formatter: signupOpFormatter }//,
                //{ name: 'Condition', index: 'Условие', width: 120, editable: true, edittype: "textarea", editoptions: { rows: "3", cols: "30", size: 150 } },
                //{ name: 'CustomCSS', index: 'CSS', width: 80, editable: true, editType: "text", editoptions: { size: 20, maxlengh: 20, size: 25 } },
                //{
                //    name: 'ValidTill', index: 'Актуально до', formatter: 'date', width: 120, editable: true, edittype: "text", editoptions: {
                //        size: 25, dataInit: function (element) {
                //            $(element).datepicker({ dateFormat: 'mm.dd.yy' })
                //        }
                //    }
                //},
                //{ name: 'ShortCondition', index: 'Краткое описание', width: 150, editable: true, edittype: "text", editoptions: { size: 25 } }
            ],
            sortname: 'PriceId',
            celledit: false,
            viewrecords: true,
            sortorder: "asc",
            caption: "Заявки на прохождение курса",
            recreateForm: true,
            autowidth: true,
            height: $(window).height() - 350,
            rowNum: 1000
        });
    };

    $('select#flowSelector').change(function () {
        flowReloader.apply($('select#flowSelector'));
    });

    function addMainPageItemHandler() {
        var dlg = $('div#editmainpageitem');
        $('input#cItemOrder', dlg).val('');
        $('input#cDestinationAction', dlg).val('');
        $('input#cDestinationActionParams', dlg).val('');
        $('input#cDestinationController', dlg).val('');
        $('input#cImgUrl', dlg).val('');
        $('textarea#cItemText', dlg).val('');
        $('input#cItemTitle', dlg).val('');
        $('input#cItemCss', dlg).val('');
        $('input[type=submit]').val('Добавить');
        dlg.dialog('open');
    }

    function deleteFeedbackHandler()
    {
        var rowId = $("#feedbacktable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#feedbacktable").getRowData(rowId);
            var dlg = $("<div>Вы уверены что хотите удалить отзыв " + rowData.Id + " </div>").dialog({
                buttons: {
                    "Удалить": function () {
                        $.ajax({
                            url: 'manager/DeleteFeedback',
                            type: 'POST',
                            data: { 'id': rowData.Id},
                            success: function () {
                                $(dlg).dialog("close");
                                jQuery("#feedbacktable").trigger("reloadGrid")
                            },
                            error: function () {
                                $(dlg).dialog("close");
                            }
                        });
                    },
                    "Отмена": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    }


    function addFeedbackHandler() {
        $.blockUI();

        $.get('manager/courses', {},
        function success(data) {

            var select = $('select', 'div#feedback-editor');
            select.empty();
            $.each(data.rows, function () {
                select.append('<option value="' + this.CourseId + '">' + this.CourseName + '</option>');
            });

            $('#cFeedbackid', 'form.edirFeedbackForm').val('-1');
            $('#cStudentName', 'form.edirFeedbackForm').val('');
            $('#cCource', 'form.edirFeedbackForm').val('');

            $('#cImageRef', 'form.edirFeedbackForm').val('');
            $('#cVkRef', 'form.edirFeedbackForm').val('');
            $('#cFeedBack', 'form.edirFeedbackForm').val('');

            $('div#feedback-editor').dialog("open");

            $.unblockUI();
        });
    }

    function editFeedbackHandle() {

        var rowId = $("#feedbacktable").jqGrid('getGridParam', 'selrow');
        var rowData = $("#feedbacktable").getRowData(rowId);

        $.blockUI();

        $.get('manager/courses', {},
        function success(data) {

            var select = $('select', 'div#feedback-editor');
            select.empty();
            $.each(data.rows, function () {
                select.append('<option value="' + this.CourseId + '">' + this.CourseName + '</option>');
            });

            $('#cFeedbackid', 'form.edirFeedbackForm').val(rowData.Id);
            $('#cStudentName', 'form.edirFeedbackForm').val(rowData.StudentName);
            $('#cCource', 'form.edirFeedbackForm').find("option[text='" + rowData.CourseName + "']").attr("selected", true);
            $('#cImageRef', 'form.edirFeedbackForm').val(rowData.ImageRef);
            $('#cVkRef', 'form.edirFeedbackForm').val(rowData.VKProfileRef);
            $('#cFeedBack', 'form.edirFeedbackForm').val(rowData.FeedBack);

            $('div#feedback-editor').dialog("open");

            $.unblockUI();
        });
    }


    function addCourseHandler() {
        $.blockUI();
        //load groups before 
        $.get('manager/coursegroups',
            {
                "page": 1,
                "rows": 100,
                "sidx": "",
                "sord": ""
            },
            function success(data) {
                $.unblockUI();
                var select = $('select', 'div#edit-course-form');
                select.empty();
                $.each(data.rows, function () {
                    select.append('<option value="' + this.GroupId + '">' + this.GroupName + '</option>');
                });

                $('input#ccoursename', 'form.editCourseForm').val('');
                $('select#ccoursegroup', 'form.editCourseForm').val('');
                $('input#cBreadcrumb', 'form.editCourseForm').val('');
                $('input#cRoute', 'form.editCourseForm').val('');
                $('input#cPricePerMonth', 'form.editCourseForm').val('');
                $('input#cCourseImageRef', 'form.editCourseForm').val('');
                $('input#cWhatIsItHtml', 'form.editCourseForm').val('');
                $('input#cWhoRequresIt', 'form.editCourseForm').val('');
                $('input#cWhatForHtml', 'form.editCourseForm').val('');
                $('input#cHowToAchieve', 'form.editCourseForm').val('');
                $('input#cWhatIsRequired', 'form.editCourseForm').val('');
                $('input#cOrder', 'form.editCourseForm').val('');
                $('input#cPreviousCourse', 'form.editCourseForm').val('');
                $('input#cNextCourse', 'form.editCourseForm').val('');

                $('#cCoursePageHtml', 'form.editCourseForm').val('');
                $('#cMenuItemStyle', 'form.editCourseForm').val('');
                $('#cIsNew', 'form.editCourseForm').attr('checked', false);

                $('div#edit-course-form').dialog("open");
            });
    }

    function OnPriceModalClose()
    {
        var $this = $(this);

        $('table', $this).jqGrid('GridDestroy');
        $("#editPrice", $this).unbind('click');
        $("#addPrice", $this).unbind('click');
        $("#removePrice", $this).unbind('click');
    }

    function editCoursePriceHandler()
    {
        var rowId = $("#coursetable").jqGrid('getGridParam', 'selrow');
        var rowData = jQuery("#coursetable").getRowData(rowId);
        $('#CourseId', 'div#course-prices-form').val(rowData.CourseId);
        if (rowId) {
            $.get('manager/GetCoursePrices',
                {courseId:rowData.CourseId},
                function success(data)
                {
                    var form = $('div#course-prices-form');

                    form.prepend('<table id="editpricegrid"></table>');

                    $('table', form).jqGrid({
                        url: 'manager/GetCoursePrices?courseId=' + rowData.CourseId,
                        datatype: "json",
                        colNames: ['#', 'ID Курса', 'Цена', 'Условие', 'CSS', 'Актуально до', 'Краткое описание'],
                        colModel:
                        [
                            { name: 'PriceId', index: '#', key:true, width: 55, editable: false},
                            { name: 'CourseId', index: 'ID Курса', width: 56, editable: false },
                            { name: 'Price', index: 'Цена', width: 60, editable: true, required:true, editType: "text", editoptions: { size: 5, maxlengh: 20 } },
                            { name: 'Condition', index: 'Условие', width: 120, editable: true, edittype: "textarea", editoptions: { rows: "3", cols: "30", size: 150 } },
                            { name: 'CustomCSS', index: 'CSS', width: 80, editable: true, editType: "text", editoptions: { size: 20, maxlengh: 20, size: 25 } },
                            {
                                name: 'ValidTill', index: 'Актуально до', formatter: 'date', width: 120, editable: true, edittype: "text", editoptions: {
                                    size: 25, dataInit: function (element) {
                                        $(element).datepicker({ dateFormat: 'mm.dd.yy' })
                                    }
                                }
                            },
                            { name: 'ShortCondition', index: 'Краткое описание', width: 150, editable: true, edittype: "text", editoptions: { size: 25 } }
                        ],
                        sortname: 'PriceId',
                        celledit : false,
                        viewrecords: true,
                        sortorder: "asc",
                        caption:"Цены курса",
                        editurl: "manager/UpdateCoursePrice?courseId=" + rowData.CourseId,
                        recreateForm: true
                    });

                    $("#editPrice", form).click(function () {
                        var gr = $('#editpricegrid', form).jqGrid('getGridParam', 'selrow');
                        var data = $('#editpricegrid', form).jqGrid('getRowData', gr);
                        if (gr != null) jQuery("#editpricegrid").jqGrid('editGridRow', gr, { closeAfterEdit: true, height: 300,width:400,reloadAfterSubmit: true});
                        else alert("Please Select Row");
                    });

                    $("#addPrice", form).click(function () {
                        $('#editpricegrid', form).jqGrid('editGridRow', "new", { closeAfterAdd: true,height: 300, width: 400, reloadAfterSubmit: true });
                    });

                    $("#removePrice", form).click(function () {
                        var gr = $('#editpricegrid', form).jqGrid('getGridParam', 'selrow');
                        if (gr != null) {
                            var data = $('#editpricegrid', form).jqGrid('getRowData', gr);
                            $('#editpricegrid', form).jqGrid('delGridRow', gr, { reloadAfterSubmit: true, delData: data });
                        }
                        else
                            alert("Please Select Row to delete!");
                    });


                    form.dialog("open");
                });
        }
    }

    function editCourseInfoHandler() {
        var rowId   = $("#coursetable").jqGrid('getGridParam', 'selrow');
        var rowData = jQuery("#coursetable").getRowData(rowId);
        $('#CourseId', 'div#course-details-form').val(rowData.CourseId);
        if (rowId) {
            $.get('manager/GetCourseInfo',
                { courseId: rowData.CourseId },
                function success(data) {
                    var form = $('div#course-details-form');
                    $('#CourseId', form).val(data.CourseId);
                    $('#cRenderLength', form).prop('checked',data.RenderLength);
                    $('#cLength', form).val(data.LengthHtml);
                    $('#cRenderSchedule', form).prop('checked', data.RenderSchedule);
                    $('#cSchedule', form).val(data.ScheduleHtml);
                    $('#cRenderSyllabus', form).prop('checked',data.RenderSyllabus);
                    $('#cSyllbus', form).val(data.SyllabusHtml);
                    $('#cRenderPrice', form).prop('checked', data.RenderPrice);
                    $('#cPrice', form).val(data.PriceHtml);
                    $('#сRenderAction', form).prop('checked',data.RenderAction);
                    $('#cActions', form).val(data.ActionHtml);
                    $('#cRenderNews', form).prop('checked', data.RenderNews);
                    $('#cNews', form).val(data.NewsHtml);
                    $('#cRenderSignUp', form).prop('checked',data.RenderSignUp);
                    $('#cSignUp', form).val(data.SignUpHtml);
                    $('#cRenderVk', form).prop('checked', data.RenderVK);
                    $('#cRenderOK', form).prop('checked', data.RenderOK);
                    $('#cRenderFB', form).prop('checked', data.RenderFB);
                    $('#cJs', form).val(data.ExtraJS);


                    form.dialog("open");
                });
        }
    }

    function editflowhandle()
    {
        var rowId = $("#viewflows").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#viewflows").getRowData(rowId);
            var form = $('div#flow-dialog-form');

            $('#cActualEndDate', form).val(rowData.ActualEndDate);
            $('#cActualStartDate', form).val(rowData.ActualStartDate);
            $('#cCourseId', form).val(rowData.CourseId);
            $('#cCustomName', form).val(rowData.CustomName);
            $('#сEstimatedStartDate', form).val(rowData.EstimatedStartDate);
            $('#cFlowId', form).val(rowData.FlowId);
            $('#cStartDateStr', form).val(rowData.StartDateStr);

            $("select#cStatus option").filter(function () {
                //may want to use $.trim in here
                return $(this).text() == rowData.StatusStr;
            }).prop('selected', true);

            //$('#cStatus', form).text(rowData.StatusStr);

            form.dialog("open");
        }
    }

    function editCourseHandle() {
        //load groups before 
        $.blockUI();
        $.get('manager/coursegroups',
            {
                "page": 1,
                "rows": 100,
                "sidx": "",
                "sord": ""
            },
            function success(data) {
                $.unblockUI();
                var select = $('select', 'div#edit-course-form');
                select.empty();
                $.each(data.rows, function () {
                    select.append('<option value="' + this.GroupId + '">' + this.GroupName + '</option>');
                });

                var rowId = $("#coursetable").jqGrid('getGridParam', 'selrow');
                if (rowId) {
                    var rowData = jQuery("#coursetable").getRowData(rowId);

                    $('input#ccoursename', 'form.editCourseForm').val(rowData.CourseName);
                    $('select#ccoursegroup', 'form.editCourseForm').val(rowData.CourseGroupId);
                    $('input#cBreadcrumb', 'form.editCourseForm').val(rowData.Breadcrumb);
                    $('input#cRoute', 'form.editCourseForm').val(rowData.Route);
                    $('input#cPricePerMonth', 'form.editCourseForm').val(rowData.PricePerMonth);
                    $('input#cCourseImageRef', 'form.editCourseForm').val(rowData.CourseImageRef);
                    $('input#cWhatIsItHtml', 'form.editCourseForm').val(rowData.WhatIsItHtml);
                    $('input#cWhoRequresIt', 'form.editCourseForm').val(rowData.WhoRequresIt);
                    $('input#cWhatForHtml', 'form.editCourseForm').val(rowData.WhatForHtml);
                    $('input#cHowToAchieve', 'form.editCourseForm').val(rowData.HowToAchieve);
                    $('input#cWhatIsRequired', 'form.editCourseForm').val(rowData.WhatIsRequired);
                    $('input#cOrder', 'form.editCourseForm').val(rowData.Order);
                    $('#cPreviousCourse', 'form.editCourseForm').val(rowData.PreviousCourse);
                    $('#cNextCourse', 'form.editCourseForm').val(rowData.NextCourse);

                    $('#cCoursePageHtml', 'form.editCourseForm').val(rowData.CustomPageHtml);
                    $('#cMenuItemStyle', 'form.editCourseForm').val(rowData.MenuItemStyle);
                    $('#cIsNew', 'form.editCourseForm').attr('checked', rowData.IsNew == "true");

                    var idInput = $('<input>').attr('type', 'hidden').appendTo('form.editCourseForm');
                    idInput.val(rowData.CourseId);
                    idInput.attr('name', 'CourseId');

                    $('input[type=submit]', 'form.editCourseForm').val('Обновить');

                    $('div#edit-course-form').one('dialogclose', function (event) {
                        $('input[type=hidden]', this).remove();
                        $('input[type=submit]', this).val('Создать');
                    });

                    $('div#edit-course-form').dialog("open");
                }
            });
    }

    function delMainPageItemHandler() {
        var rowId = $("#maindetailstable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#maindetailstable").getRowData(rowId);
            var dlg = $("<div>Вы уверены что хотите удалить элемент " + rowData.ItemTitle + " </div>").dialog({
                buttons: {
                    "Удалить": function () {
                        $.ajax({
                            url: 'manager/DeleteMainPageItem',
                            type: 'POST',
                            data: { 'itemId': rowData.Id },
                            success: function () {
                                $(dlg).dialog("close");
                                jQuery("#maindetailstable").trigger("reloadGrid")
                            },
                            error: function () {
                                $(dlg).dialog("close");
                            }
                        });
                    },
                    "Отмена": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    }
    

    function delCourseHandler() {
        var rowId = $("#coursetable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#coursetable").getRowData(rowId);
            var dlg = $("<div>Вы уверены что хотите удалить курс " + rowData.CourseName + " </div>").dialog({
                buttons: {
                    "Удалить": function () {
                        $.ajax({
                            url: 'manager/DeleteCourse',
                            type: 'POST',
                            data: { 'courseId': rowData.CourseId },
                            success: function () {
                                $(dlg).dialog("close");
                                jQuery("#coursetable").trigger("reloadGrid")
                            },
                            error: function () {
                                $(dlg).dialog("close");
                            }
                        });
                    },
                    "Отмена": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    }

    function addItemHandler() {

        $('input#cname', 'form.editGroupForm').val('');
        $('input#corder', 'form.editGroupForm').val('');
        $('input#cBreadcrumb', 'form.editGroupForm').val('');
        $('input#cRouteName', 'form.editGroupForm').val('');
        $('input#cImagePath', 'form.editGroupForm').val('');
        $('input#cTargets', 'form.editGroupForm').val('');
        $('input#cMethods', 'form.editGroupForm').val('');
        $('input#cSolutions', 'form.editGroupForm').val('');
        $('input#cIsNew', 'form.editGroupForm').attr('checked', false);
        $('input#cMenuItemStyle', 'form.editGroupForm').val('');
        $('input#cGroupPageHtml', 'form.editGroupForm').val('');

        $('div#group-dialog-form').dialog("open");
    }

    function selectRowById(id, gridSelector) {
        if (gridSelector)
            jQuery(gridSelector).jqGrid('setSelection', id);
        else
            jQuery("#coursegrouptable").jqGrid('setSelection',id);
    }

    function editMainPageItemHandle()
    {
        var rowId = $("#maindetailstable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#maindetailstable").getRowData(rowId);

            $('#cItemOrder', 'form.editmainitem').val(rowData.Order);
            $('#cDestinationAction', 'form.editmainitem').val(rowData.DestinationAction);
            $('#cDestinationActionParams', 'form.editmainitem').val(rowData.DestinationActionParams);
            $('#cDestinationController', 'form.editmainitem').val(rowData.DestinationController);
            $('#cImgUrl', 'form.editmainitem').val(rowData.ImgUrl);
            $('#cItemCss', 'form.editmainitem').val(rowData.ItemCss);
            $('#cItemText', 'form.editmainitem').val(rowData.ItemText);
            $('#cItemTitle', 'form.editmainitem').val(rowData.ItemTitle);

            var idInput = $('<input>').attr('type', 'hidden').appendTo('form.editmainitem');
            idInput.val(rowData.Id);
            idInput.attr('name', 'Id');

            $('input[type=submit]', 'form.editmainitem').val('Обновить');


            $('div#editmainpageitem').one('dialogclose', function (event) {
                $('input[type=hidden]', this).remove();
                $('input[type=submit]', this).val('Добавить');
            });

            $('#editmainpageitem').dialog("open");
        }
    }

    function editItemHandle(r) {
        var rowId = $("#coursegrouptable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#coursegrouptable").getRowData(rowId);
            
            $('input#cname', 'form.editGroupForm').val(rowData.GroupName);
            $('input#corder', 'form.editGroupForm').val(rowData.Order);
            $('input#cBreadcrumb', 'form.editGroupForm').val(rowData.BreadCrumb);
            $('input#cRouteName', 'form.editGroupForm').val(rowData.RouteName);
            $('input#cImagePath', 'form.editGroupForm').val(rowData.ImagePath);
            $('input#cTargets', 'form.editGroupForm').val(rowData.GoalsMarkup);
            $('input#cMethods', 'form.editGroupForm').val(rowData.MethodMarkup);
            $('input#cSolutions', 'form.editGroupForm').val(rowData.SolutionsMarkup);

            $('input#cIsNew', 'form.editGroupForm').attr('checked',rowData.IsNew == 'true');
            $('input#cMenuItemStyle', 'form.editGroupForm').val(rowData.MenuItemStyle);
            $('input#cGroupPageHtml', 'form.editGroupForm').val(rowData.CustomPageHtml);

            var idInput = $('<input>').attr('type', 'hidden').appendTo('form.editGroupForm');
            idInput.val(rowData.GroupId);
            idInput.attr('name','GroupId');

            $('input[type=submit]', 'form.editGroupForm').val('Обновить');


            $('div#group-dialog-form').one('dialogclose', function (event) {
                $('input[type=hidden]', this).remove();
                $('input[type=submit]', this).val('Создать');
            });

            $('div#group-dialog-form').dialog("open");
        }
    }

    function delItemHandler() {
        var rowId = $("#coursegrouptable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#coursegrouptable").getRowData(rowId);
            var dlg = $("<div>Вы уверены что хотите удалить группу " + rowData.GroupName + " </div>").dialog({
                buttons: {
                    "Удалить": function () {
                        $.ajax({
                            url: 'manager/DeleteGroup',
                            type: 'POST',
                            data: { 'groupId': rowData.GroupId },
                            success: function () {
                                $(dlg).dialog("close");
                                jQuery("#coursegrouptable").trigger("reloadGrid")
                            },
                            error: function () {
                                $(dlg).dialog("close");
                            }
                        });
                    },
                    "Отмена": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    }

    $("#coursetable").on('click', 'a.editSEOLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#coursetable");

        var rowId = $("#coursetable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#coursetable").getRowData(rowId);

            $('#CourseId', 'div#seo-dialog-form').val(rowData.CourseId);
            $('#GroupId', 'div#seo-dialog-form').val('');
            $('#ch1', 'div#seo-dialog-form').val(rowData.TitleH1);
            $('#ctitle', 'div#seo-dialog-form').val(rowData.PageTitle);
            $('#cdescription', 'div#seo-dialog-form').val(rowData.Description);
            $('#ckeywords', 'div#seo-dialog-form').val(rowData.KeyWords);
            $('#crobots', 'div#seo-dialog-form').val(rowData.Robots);

            $('div#seo-dialog-form').dialog("open");
        }
    });

    
    $("#coursegrouptable").on('click', 'a.editSEOLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid);

        var rowId = $("#coursegrouptable").jqGrid('getGridParam', 'selrow');
        if (rowId) {
            var rowData = jQuery("#coursegrouptable").getRowData(rowId);
            $('#CourseId', 'div#seo-dialog-form').val('');
            $('#GroupId', 'div#seo-dialog-form').val(rowData.GroupId);
            $('#ch1', 'div#seo-dialog-form').val(rowData.TitleH1);
            $('#ctitle', 'div#seo-dialog-form').val(rowData.PageTitle);
            $('#cdescription', 'div#seo-dialog-form').val(rowData.Description);
            $('#ckeywords', 'div#seo-dialog-form').val(rowData.KeyWords);
            $('#crobots', 'div#seo-dialog-form').val(rowData.Robots);

            $('div#seo-dialog-form').dialog("open");
        }
    });


    $('#global-tabs-12').on('click', 'a.flowchangetrigger', function (e)
    {
        var destinationSElect = $('select#newstudentflowselector', 'div#student-flow-form');
        destinationSElect.empty();

        var applicationId = $(this).attr('data-application-id');
        var flowID = $(this).attr('data-flow-id');

        $("#flowSelector option").each(function () {
            if ($(this).val() > 0) {
                if ($(this).val() == flowID) {
                    destinationSElect.append('<option selected="selected" value="' + $(this).val() + '">' + $(this).text() + '</option>');
                }
                else {
                    destinationSElect.append('<option value="' + $(this).val() + '">' + $(this).text() + '</option>');
                }
            }
        });

        $('input#newstudentflowappid').val(applicationId);
        $('div#student-flow-form').dialog('open');
        return false;
    });

    $("#coursegrouptable").on('click','a.removeLink', function (e)
    {
        var id    = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid)
        delItemHandler();
        return false;
    });

    $('input#editFeedbackTrigger').click(function (e) {
        
        $.ajax({
            url: 'manager/UpdateFeedback',
            type: 'POST',
            data: $('form.edirFeedbackForm').serialize(),
            success: function () {
                $('div#feedback-editor').dialog("close");
                $("#feedbacktable").trigger("reloadGrid")
            },
            error: function () {
                alert('error');
            }
        });
    });


    $('input.applicationflowupdatetrigger').click(function (e) {
        var appToUpdate = $('input#newstudentflowappid').val();
        var newFlowId   = $('select#newstudentflowselector').val();

        $.ajax({
            url: 'manager/UpdateApplicationFlow',
            type: 'POST',
            data: { appid: appToUpdate, flowid: newFlowId },
            success: function () {
                $('div#student-flow-form').dialog("close");
                jQuery("#viewsignups").trigger("reloadGrid")
            },
            error: function () {
                alert('error');
            }
        });
        return false;
    });

    $('#coursetable').on('click', 'a.editInfoLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#coursetable");
        editCourseInfoHandler();
        return false;
    });

    $("#coursetable").on('click', 'a.editPriceLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#coursetable");
        editCoursePriceHandler();
        return false;
    });

    $('#maindetailstable').on('click', 'a.editLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, '#maindetailstable');
        editMainPageItemHandle();
        return false;
    });


    $('#feedbacktable').on('click', 'a.editLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, '#feedbacktable');
        editFeedbackHandle();
        return false;
    });

    $('#feedbacktable').on('click', 'a.removeLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, '#feedbacktable');
        deleteFeedbackHandler();
        return false;
    });


    $("#maindetailstable").on('click', 'a.removeLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#maindetailstable");
        delMainPageItemHandler();
        return false;
    });

    $('#floweditpanel').on('click', 'a.editFlowLink', function (e) {
        
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#viewflows");
        editflowhandle();
        return false;
    });

    $('#floweditpanel').on('click', 'a.removeFlowLink', function (e) {
        alert('Удаление потоков не предусмотрено');
    });

    $("#coursegrouptable").on('click', 'a.editLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid);
        editItemHandle();
        false;
    });

    $("#coursetable").on('click', 'a.removeLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#coursetable")
        delCourseHandler();
        return false;
    });

    $("#coursetable").on('click', 'a.editLink', function (e) {
        var id = parseInt($(this).attr('data-id'));
        var rowid = $(this).closest('tr').attr('id');
        selectRowById(rowid, "#coursetable")
        editCourseHandle();
        return false;
    });


    $("#feedbacktable").jqGrid({
        url: 'manager/LoadFeedbacks',
        height: 450,
        datatype: "json",
        colNames: ['#', 'StudentName', 'CourseName', 'FeedBack', 'ImageRef', 'VKProfileRef', 'FBProfileRef', 'OKProfileRef','PostTime'],
        colModel: [
            { name: 'Id', index: '#', width: 35 },
            { name: 'StudentName', index: 'StudentName', width: 180 },
            { name: 'CourseName', index: 'CourseName', width: 180, hidden: true },
            { name: 'FeedBack', index: 'FeedBack', width: 350 },
            { name: 'ImageRef', index: 'ImageRef', width: 200, hidden: true },
            { name: 'VKProfileRef', index: 'VKProfileRef', width: 100, hidden: true },
            { name: 'FBProfileRef', index: 'FBProfileRef', formatter: feedbackactionsFormatter, width: 105 },
            { name: 'OKProfileRef', index: 'OKProfileRef', hidden: true },
            { name: 'PostTime', index: 'PostTime', hidden: true }
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        pager: '#feedbackpager',
        sortname: 'PostTime',
        viewrecords: true,
        sortorder: "asc",
        caption: "Отзывы о курсах"
    });
    jQuery("#feedbacktable").jqGrid('navGrid', '#feedbackpager', { search: false, addfunc: addFeedbackHandler, editfunc: editFeedbackHandle, delfunc: deleteFeedbackHandler, edit: true, add: true, del: true });


    $("#coursetable").jqGrid({
        url: 'manager/Courses',
        autowidth: false,
        shrinkToFit: false,
        width: $(window).width() - 50,
        height: $(window).height() - 350,
        datatype: "json",
        colNames: ['#', 'Название', 'ID Группы','Группа','Breadcrumb', 'Маршрут', 'Действия', 'Цена за месяц',
                   'Изображение', 'Что это HTML', 'Кому это нужно', 'Для чего это', 'Как получить результат',
                   'Что требуется', 'Порядок', 'TitleH1', 'Description', 'KeyWords', 'Robots', 'PageTitle', 'Детали', 'След', 'Пред',
                   'CustomPageHtml', 'MenuItemStyle', 'IsNew'],
        colModel: [
            { name: 'CourseId', index: '#', width: 35 },
            { name: 'CourseName', index: 'Название', width: 180 },
            { name: 'CourseGroupId', index: 'ID Группы', width: 180, hidden: true },
            { name: 'CourseGroupName', index: 'Группа', width: 180},
            { name: 'Breadcrumb', index: 'Breadcrumb', width: 200,hidden:true },
            { name: 'Route', index: 'Маршрут', width: 100},
            { name: '', index: 'Действия', formatter: actionsFormatter, width: 105 },
            { name: 'PricePerMonth', index: 'Цена за месяц', hidden: true },
            { name: 'CourseImageRef', index: 'Изображение', hidden: true },
            { name: 'WhatIsItHtml', index: 'Что это HTML', hidden: true },
            { name: 'WhoRequresIt', index: 'Кому это нужно', hidden: true },
            { name: 'WhatForHtml', index: 'Для чего это', hidden: true },
            { name: 'HowToAchieve', index: 'Как получить результат', hidden: true },
            { name: 'WhatIsRequired', index: 'Что требуется', hidden: true },
            { name: 'Order', index: 'Порядок', hidden: true },
            { name: 'TitleH1', index: 'TitleH1', hidden: true },
            { name: 'Description', index: 'Description', hidden: true },
            { name: 'KeyWords', index: 'KeyWords', hidden: true },
            { name: 'Robots', index: 'Robots', hidden: true },
            { name: 'PageTitle', index: 'PageTitle', hidden: true },
            { name: '', index: 'Детали', width: 60, formatter: detailsFormatter },
            { name: 'NextCourse', index: 'След', hidden: true },
            { name: 'PreviousCourse', index: 'Пред', hidden: true },
            { name: 'CustomPageHtml', index: 'CustomPageHtml', hidden: true },
            { name: 'MenuItemStyle', index: 'MenuItemStyle', hidden: true },
            { name: 'IsNew', index: 'IsNew', hidden: true }
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        pager: '#coursepager',
        sortname: 'GroupId',
        viewrecords: true,
        sortorder: "asc",
        caption: "Курсы"
    });
    jQuery("#coursetable").jqGrid('navGrid', '#coursepager', { search: false, addfunc: addCourseHandler, editfunc: editCourseHandle, delfunc: delCourseHandler, edit: true, add: true, del: true });



    $("#coursegrouptable").jqGrid({
        url: 'manager/coursegroups',
        height: 450,
        datatype: "json",
        colNames: ['#', 'Название', 'Breadcrumb', 'Маршрут', 'Действия', 'Изображение', 'Цели HTML', 'Методы HTML', 'Решения HTML', 'Порядок', 'TitleH1', 'Description', 'KeyWords', 'Robots', 'PageTitle', 'CustomPageHtml', 'MenuItemStyle', 'IsNew'],
        colModel: [
            { name: 'GroupId', index: '#', width: 35 },
            { name: 'GroupName', index: 'Название', width: 180 },
            { name: 'BreadCrumb', index: 'Breadcrumb',width:200 },
            { name: 'RouteName', index: 'Маршрут', width: 80 },
            { name: '', index: 'Actions', formatter: actionsFormatter },
            { name: 'ImagePath', index: 'Изображение', hidden: true },
            { name: 'GoalsMarkup', index: 'Цели HTML', hidden: true },
            { name: 'MethodMarkup', index: 'Методы HTML', hidden: true },
            { name: 'SolutionsMarkup', index: 'Решения HTML', hidden: true },
            { name: 'Order', index: 'Порядок', hidden: true },
            { name: 'TitleH1', index: 'TitleH1', hidden: true },
            { name: 'Description', index: 'Description', hidden: true },
            { name: 'KeyWords', index: 'KeyWords', hidden: true },
            { name: 'Robots', index: 'Robots', hidden: true },
            { name: 'PageTitle', index: 'PageTitle', hidden: true },
            { name: 'CustomPageHtml', index: 'CustomPageHtml', hidden: true },
            { name: 'MenuItemStyle', index: 'MenuItemStyle', hidden: true },
            { name: 'IsNew', index: 'IsNew', hidden: true }
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        pager: '#coursegrouppager',
        sortname: 'GroupId',
        viewrecords: true,
        sortorder: "asc",
        caption: "Группы курсов"
    });
    jQuery("#coursegrouptable").jqGrid('navGrid', '#coursegrouppager', { addfunc: addItemHandler, editfunc: editItemHandle, delfunc: delItemHandler, edit: true, add: true, del: true });


    $('form.editmainitem').validate({
        submitHandler: function (form) {
            var operation = $('input[type=submit]', form).val();
            if (operation == 'Добавить') {
                $.ajax({
                    url: 'manager/PostMainPageItem',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: function () {
                        $('div#editmainpageitem').dialog("close");
                        jQuery("#maindetailstable").trigger("reloadGrid")
                    },
                    error: function () {
                        alert('Ошибка создания');
                    }
                });
            }
            else {
                $.ajax({
                    url: 'manager/UpdateMainPageItem',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: function () {
                        $('div#editmainpageitem').dialog("close");
                        jQuery("#maindetailstable").trigger("reloadGrid")
                    },
                    error: function () {
                        alert('Ошибка обновления');
                    }
                });
            }
        }
    });


    $('form#editCourseInfoForm').validate({
        submitHandler: function (form) {
            $.ajax({
                url: 'manager/UpdateCourseInfo',
                type: 'POST',
                data: $(form).serialize(),
                success: function () {
                    $('div#course-details-form').dialog("close");
                    jQuery("#coursetable").trigger("reloadGrid")
                },
                error: function () {
                    alert('error');
                }
            });
        }
    });

    $('form#editCourseForm').validate({
        submitHandler: function (form) {
            var operation = $('input[type=submit]', form).val();
            if (operation == 'Обновить') {
                $.ajax({
                    url: 'manager/UpdateCourse',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: function () {
                        $('div#editc-аourse-form').dialog("close");
                        jQuery("#coursetable").trigger("reloadGrid")
                    },
                    error: function () {
                        alert('error');
                    }
                });
            }
            else{
                $.ajax({
                    url: 'manager/PostCourse',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: function () {
                        $('div#edit-course-form').dialog("close");
                        jQuery("#coursetable").trigger("reloadGrid")
                    },
                    error: function () {
                        alert('error');
                    }
                });
            }
        }
    });

    $("form#edidPageHeadersForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: 'manager/UpdatePageHeaders',
                type: 'POST',
                data: $(form).serialize(),
                success: function () {
                    $('div#seo-dialog-form').dialog("close");
                    jQuery("#coursegrouptable").trigger("reloadGrid");
                    jQuery("#coursetable").trigger("reloadGrid")
                },
                error: function () {
                    alert('error');
                }
            });
        }
    });

    $("form#editGroupForm").validate({
        submitHandler: function (form) {
            var operation = $('input[type=submit]', form).val();
            if (operation == 'Обновить') {
                $.ajax({
                    url: 'manager/UpdateGroup',
                    type: 'POST',
                    data: $(form).serialize(),
                    success: function () {
                        $('div#group-dialog-form').dialog("close");
                        jQuery("#coursegrouptable").trigger("reloadGrid")
                    },
                    error: function () {
                        alert('error');
                    }
                });
            }
            else{
                $.ajax({
                    url: 'manager/PostGroup',
                    type:'POST',
                    data: $(form).serialize(),
                    success: function () {
                        $('div#group-dialog-form').dialog("close");
                        jQuery("#coursegrouptable").trigger("reloadGrid")
                    },
                    error: function () {
                        alert('error');
                    }
                });
            }
        }
    });

    $('div#course-details-form').dialog({
        autoOpen: false,
        height: 540,
        width: 700,
        modal: true,
        resizable: false
    });

    $('div#seo-dialog-form').dialog({
        autoOpen: false,
        height: 600,
        width: 700,
        modal: true,
        resizable: false
    });

    $("div#group-dialog-form").dialog({
        autoOpen: false,
        height: 500,
        width: 700,
        modal: true,
        resizable: false
    });

    $("div#edit-course-form").dialog({
        autoOpen: false,
        height: 500,
        width: 700,
        modal: true,
        resizable: false
    });

    $("div#course-prices-form").dialog({
        autoOpen: false,
        height: 320,
        width: 700,
        modal: false,
        resizable: false,
        close:OnPriceModalClose
    });

    $('div#editmainpageitem').dialog({
        autoOpen: false,
        height: 600,
        width: 700,
        modal: true,
        resizable: false
    });

    $('div#student-flow-form').dialog({
        autoOpen: false,
        height: 100,
        width: 350,
        modal: true,
        resizable: false
    });

    var editFlowDialog =  $('div#flow-dialog-form').dialog({
        autoOpen: false,
        height: 350,
        width: 600,
        modal: true,
        resizable: false
    });

    var feedbackEditor = $('div#feedback-editor').dialog(
    {
        width: 600,
        autoOpen: false,
        modal: true,
        resizable: false
    });


    $('.dateselector', editFlowDialog).datepicker({ dateFormat: 'dd/mm/yy' });

    $("div#html-edit-dialog-form").dialog({
        autoOpen: false,
        modal: true,
        height: 470,
        width: 700,
        resizable: true,
        closeOnEscape: false,
        resize: function (event, ui) {
            var resizeHeight = ui.size.height - 180;
            var resizeWidth  = ui.size.width  - 30;
            tinyMCE.activeEditor.theme.resizeTo(resizeWidth, resizeHeight);
        }
    });


    $('a.newFlowTrigger').click(function(){
        
        var form = $('div#flow-dialog-form');
        var courseId = $(this).attr('data-course-id');

        $('#cActualEndDate', form).val('');
        $('#cActualStartDate', form).val('');
        $('#cCourseId', form).val(courseId);
        $('#cCustomName', form).val('');
        $('#сEstimatedStartDate', form).val('');
        $('#cFlowId', form).val(-1);
        $('#cStartDateStr', form).val('');
        $('#cStatus', form).val(0);

        form.dialog("open");
    
        return false;    
    });

    $("#editFlowForm").submit(function() 
    {
        var url = "manager/UpdateFlow"; // the script where you handle the form input.
        var dialogHolder = $('div#flow-dialog-form');


        $.ajax({
            type: "POST",
            url: url,
            data: $("#editFlowForm").serialize(), // serializes the form's elements.
            success: function(data)
            {
                dialogHolder.dialog('close');
                $("#viewflows").trigger("reloadGrid");
            },
            error:function(data)
            {
                alert('error'); 
            }
        });

        return false; // avoid to execute the actual submit of the form.
    });
});