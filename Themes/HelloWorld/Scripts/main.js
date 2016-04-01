$(document).ready(function () {

    $("#owl-demo").owlCarousel({
        autoPlay: 995000,
 	      items : 4,
 	      navigation: true,
 	      pagination: false,
 	      navigationText: ["",""],
      	itemsDesktop : [1199,4],
      	itemsDesktopSmall : [979,2]
    });

    $("#owl-demo2").owlCarousel({
        autoPlay: 995000,
        items: 3,
        lazyLoad: true,
        lazyEffect: "",
        navigation: true,
        navigationText: ["",""],
        itemsDesktop : [1199,3],
        itemsDesktopSmall: [979, 3]
    });

    $(".button_call-back").click(function (e) {
        $('form.rightRegistrationForm input[type="text"]').tooltipster('hide');
        $('form.rightRegistrationForm input[type="tel"]').tooltipster('hide');
        $('form.rightRegistrationForm input[type="email"]').tooltipster('hide');
        $('form.rightRegistrationForm select').tooltipster('hide');
        e.preventDefault();
        $("#call-back").toggleClass("call-back_add");
    });
    $('select').styler();
    $('.one-review:nth-child(2n),.course-soon:nth-child(2n)').css('float','right');


    $(document).ready(function () {
        // hide #back-top first 
  	    if ($(this).scrollTop() > 200) {
  	        $('#back-top').show();
  	    } else {
  	        $('#back-top').hide();
  	    }

        $(function () { 
            $(window).scroll(function () {
                
                var bottomButton = $('div.button_call-back').offset().top + $('div.button_call-back').height();
                var footerTop    = $('.footer').offset().top;
                var diff         = bottomButton - footerTop;

                if (diff > 0)
                {
                    $('div.button_call-back').animate({ marginTop: '-=' + diff +'px' }, 0);
                }


                //console.log('footer' + $('.footer').height());
                //console.log('body' + $('body').height());
                //console.log('scrollTop' + $(this).scrollTop());
            
              if ($(this).scrollTop() > 200) { 
              $('#back-top').fadeIn(); 
              } else { 
                  $('#back-top').fadeOut(); 
              }

              console.log()

            }); // scroll body to 0px on click 
            $('#back-top a').click(function () { 
              $('body,html').animate({ scrollTop: 0 }, 800); 
              return false; 
            }); 
        }); 
    });
    //$(function () {

    //    // show hide subnav depending on scroll direction
    //    var position = $(window).scrollTop();

        //$(window).scroll(function () {
        //    var scroll     = $(window).scrollTop();
        //    var bodyHeight = $('.wrapper').height();
        //    bodyHeight = bodyHeight-1000;
        //    alert(bodyHeight)
        //    $('#back-top').css('bottom', '20px');

        //    //if (scroll > bodyHeight) {
        //    //    $('#back-top').css('bottom','400px');
        //    //} else {
        //    //   $('#back-top').css('bottom','20px');              
        //    //}
        //    position = scroll;
        //});

    //});
});


$(document).ready(function () {

    $('form.rightRegistrationForm input[type="text"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'left',
        positionTracker:true
    });

    $('form.rightRegistrationForm input[type="tel"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'left',
        positionTracker: true
    });

    $('form.rightRegistrationForm input[type="email"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'left',
        positionTracker: true
    });

    $('form.rightRegistrationForm select').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'left',
        positionTracker: true
    });

    $('form#footer-form input[type="text"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'top',
        positionTracker: true
    });

    $('form#footer-form input[type="email"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'top',
        positionTracker: true
    });

    $('form#footer-form textarea').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'right',
        positionTracker: true
    });


    $('form.signupFormGlobal input[type="text"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'right',
        positionTracker: true
    });

    $('form.signupFormGlobal input[type="tel"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'right',
        positionTracker: true
    });

    $('form.signupFormGlobal input[type="email"]').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'right',
        positionTracker: true
    });

    $('form.signupFormGlobal textarea').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'right',
        positionTracker: true
    });

    $('form.signupFormGlobal').validate({
        errorPlacement: function (error, element) {

            $(element).tooltipster('update', $(error).text());

            if ($(error).text() != '') {
                $(element).tooltipster('show');
                $(element).tooltipster('update', $(error).text());
            }
        },
        success: function (label, element) {
            $(element).tooltipster('hide');
        }
    });

    $('form.rightRegistrationForm').validate({
        errorPlacement: function (error, element) {
            $(element).tooltipster('update', $(error).text());
            $(element).tooltipster('show');
        },
        success: function (label, element) {
            $(element).tooltipster('hide');
        }
    });

    $('form.signupFormGlobal').submit(function (event) {
        if (event && event.result)
            $.blockUI({ baseZ: 10000, message: '<div class="title">Подождите пожалуйста...<p>&nbsp;<p/>Мы обрабатываем Вашу регистрацию.</div>' });
    });

    $('form.rightRegistrationForm').submit(function (event) {
        if (event && event.result)
            $.blockUI({ baseZ: 10000, message: '<div class="title">Подождите пожалуйста...<p>&nbsp;<p/>Мы обрабатываем Вашу регистрацию.</div>' });
    });

    $('form#footer-form').validate({
        errorPlacement: function (error, element) {
            $(element).tooltipster('update', $(error).text());
            $(element).tooltipster('show');
        },
        success: function (label, element) {
            $(element).tooltipster('hide');
        }
    });

    $('form#footer-form').submit(function (event) {
        if (event && event.result)
            $.blockUI({ baseZ: 10000, message: '<div class="title">Подождите пожалуйста...<p>&nbsp;<p/>Мы обрабатываем Ваш вопрос.</div>' });
    });

    $('a[name=modal]').click(function (e) {


        if ($(this).attr('course-id') && $(this).attr('course-name')) {
            var name     = $(this).attr('course-name');
            var courseId = $(this).attr('course-id');
        }
        else {
            var name = $(this).parent().parent().parent().find('.service-name').text();
            var courseId = $(this).parent().find('.courseId').val();
        }

        if ($(this).attr('image-url'))
        {
            var dialog = $('div#dialog');
            var imageEl = $('img.feedback-scan');
            var progressElement = $('span.spinner-container');


            dialog.css('paddingTop', '200px');
            progressElement.show();
            imageEl.hide();

            $(imageEl).one('load', function () {
                progressElement.hide();
                dialog.css('paddingTop', '0px');
                imageEl.show();
            });

            //$('img.feedback-scan').attr('src', '/Themes/HelloWorld/Content/Images/loading.gif');
            $('img.feedback-scan').attr('src', $(this).attr('image-url'));
        }

        e.preventDefault();
        $('#boxes-order #course-id').val(courseId);
        $('#boxes-order #course-name').val(name);

        var id = $(this).attr('href');
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
        $('#mask').fadeIn(0);
        $('#mask').fadeTo(0);
        var winH = $('#page').height();
        var winW = $('#page').width();

        if ($(window).height() > 800) {
            $(id).css('top', 100 + 'px');
            $(id).css('left', '50%');
        }
        else
        {
            $(id).css('top', 10 + 'px');
            $(id).css('left', '50%');
        }


        $(id).fadeIn(0);
    });
    $('.window .close').click(function (e) {
        $('form.signupFormGlobal input[type="text"]').tooltipster('hide');
        $('form.signupFormGlobal input[type="tel"]').tooltipster('hide');
        $('form.signupFormGlobal input[type="email"]').tooltipster('hide');
        $('form.signupFormGlobal textarea').tooltipster('hide');
        e.preventDefault();
        $('#mask, .window').hide();
    });
    $('#mask').click(function () {
        $('form.signupFormGlobal input[type="text"]').tooltipster('hide');
        $('form.signupFormGlobal input[type="tel"]').tooltipster('hide');
        $('form.signupFormGlobal input[type="email"]').tooltipster('hide');
        $('form.signupFormGlobal textarea').tooltipster('hide');
        $(this).hide();
        $('.window').hide();
    });
});