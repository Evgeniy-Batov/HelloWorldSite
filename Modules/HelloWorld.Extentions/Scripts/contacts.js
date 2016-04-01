$(function () {

    $('form#contact input').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'left',
        positionTracker: true
    });

    $('form#contact textarea').tooltipster({
        trigger: 'custom',
        onlyOne: false,
        position: 'left',
        positionTracker: true
    });
    
    $('form#contact').validate({
        errorPlacement: function (error, element) {
            $(element).tooltipster('update', $(error).text());
            $(element).tooltipster('show');
        },
        success: function (label, element) {
            $(element).tooltipster('hide');
        }
    });
     
});