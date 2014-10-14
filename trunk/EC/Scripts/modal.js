var modal = (function () {

    var _mask;
    var _dialog;
    var _confirmButtonText = '';
    var _cancelButtonText = '';
    var _enableScroll = true;
    var _top;

    /*
    É necessário adicionar uma DIV depois da tag <BODY><div id="page"> <!-- HTML --></div></BODY>

    caption
    content
    confirmButtonText
    confirmButtonClick
    cancelButtonText
    cancelButtonClick
    enableScroll
    loaded
    width
    height
    disableMaskClick    -> disable on click in mask
    maskClick

    O Css da DIV #page deve ter as configurações abaixo e estar entre as <BODY></BODY>:

    div#page {
    position:fixed;
    width: 100%;
    height: 100%;
    top:0;
    left:0;
    bottom: 0;
    right: 0;
    margin: 0 auto;
    padding: 0;
    }

    */

    var show = function(params) {

        if (params.enableScroll == undefined)
            params.enableScroll = true;

        if (params.caption == undefined)
            params.caption = 'Caption';

        if (params.content == undefined)
            params.content = 'Content';

        if (params.hideAction == undefined)
            params.hideAction = false;

        if (params.disableMaskClick == undefined)
            params.disableMaskClick = false;

        _mask = getMask();
        _dialog = getDialog();

        $('body').append(_mask);
        $('body').append(_dialog);

        setParams(params);

        if (params.enableScroll === true) {
            _enableScroll = params.enableScroll;
            scroll();
            if ($(window).height() < $(document).height())
                $(window).scroll();
        } else
            $('div#page').css('position', 'fixed');

        if (params.width != undefined)
            setWidth(params.width);

        if (params.height != undefined)
            setHeight(params.height);

        _mask.show();
        _dialog.show();

        _dialog.watch({ resize: modal.resize, widthTolerance: 5, watchDimension: true });

        //Observavel.init({ elem: $("#dialog"), resize: modal.resize, widthTolerance: 5 });

        if (params.disableMaskClick === false) {
            _mask.click(function () {
                
                if (params.maskClick != undefined) {
                    params.maskClick();
                }

                modal.hide();
            });
        }

        $(window).resize(function() {

            modal.resize();

        });

        if (params.loaded != undefined)
            params.loaded();

        modal.resize();
    };

    function getDialog() {

        if (_dialog == undefined) {
            _dialog = $(document).find('#dialog');

            if (_dialog.length) {
                destroy();
            }
            
             _dialog = createDialog();
        }

        if (!$('.dialog-action .button', _dialog).length)
            _dialog = createDialog();

        return _dialog;

    };

    var destroy = function () {

        if (_dialog != undefined) {
            $(document).find('#dialog').remove();
            _dialog = null;
        }

    };

    function getMask() {

        if (_mask == undefined) {
            _mask = $(document).find('#mask');

            if (!_mask.length)
                _mask = $('<div id="mask" />');
        }

        return _mask;
    }

    function setParams(params) {

        var $title = $('.dialog-title', _dialog);
        $title.html(params.caption);

        var $content = $('.dialog-content', _dialog);

        $content.html(params.content);

        var $confirmButton = $('#confirm_button_modal', _dialog);

        $confirmButton.unbind('click');

        if (params.confirmButtonText == undefined)
            params.confirmButtonText = "Ok";

        $confirmButton.text(params.confirmButtonText);

        if (params.confirmButtonClick != undefined) {

            $confirmButton.removeAttr('disabled');
            $confirmButton.show();
            $confirmButton.click(params.confirmButtonClick);
        } else {
            $confirmButton.hide();
        }

        var $cancelButton = $('#cancel_button_modal', _dialog);
        $cancelButton.unbind('click');

        if (params.cancelButtonText == undefined)
            params.cancelButtonText = "Cancelar";

        $cancelButton.text(params.cancelButtonText);

        if (params.cancelButtonClick != undefined) {

            $cancelButton.show();
            $cancelButton.click(params.cancelButtonClick);
        }
        else {
            $cancelButton.click(function () {
                modal.hide();
            });
        }

        if (params.hideAction === true) {
            $('.dialog-action', _dialog).hide();
        }
        else if (params.hideAction === false) {
            $('.dialog-action', _dialog).show();
        }
    }

    function createDialog() {

        var $dialog = $('<div id="dialog" class="window"></div>');

        var $title = $('<h2 class="dialog-title"></h2>');
        var $content = $('<div class="dialog-content"></div>');
        var $action = $('<div class="dialog-action"></div>');

        var $cancelButton = $('<label class="button"><button id="cancel_button_modal" type="button" /></label>');
        var $okButton = $('<label class="button"><button id="confirm_button_modal" class="red" type="button" /></label>');

        $action.append($okButton);
        $action.append($cancelButton);
        $dialog.append($title);
        $dialog.append($content);
        $dialog.append($action);


        return $dialog;

    };

    function scroll() {
        var timeout = 0;
        $(window).scroll(function () {

            if (_enableScroll) {

                var $dialog = $(_dialog);

                var scrollTop = $(document).scrollTop();

            }
            var $dialog = $(_dialog);

            var scrollTop = $(document).scrollTop();
            if ($(_dialog).height() < $(window).height()) {
                if (timeout > 0)
                    clearTimeout(timeout);
                timeout = setTimeout(function () {
                    $dialog.animate({
                        'top': (40 + scrollTop)
                    }, 200);
                }, 300);
            }
        });

    };

    var setWidth = function (width) {
        _dialog.css('width', width);

    };

    var setHeight = function (height) {
        _dialog.css('height', height);

    };

    var resize = function () {
        if (_dialog != null) {
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            var scrollTop = $(document).scrollTop();

            _mask.css({
                'width': maskWidth,
                'height': maskHeight
            });
            _dialog.css("top", calcTopH());
            _dialog.css("left", calcTopW());

            if (!_enableScroll) {
                _dialog.css('position', 'relative');
                _dialog.css('margin-bottom', '80px');
            }
            else {
                _dialog.css('position', 'absolute');
            }
        }
        if (!_enableScroll === true) {
            _dialog.css('padding-bottom', '40px');
        }

    };

    function calcTopH() {
        if (_top != undefined) {
            return _top;
        } else {
            var winH = $(window).height();

            if (winH < _dialog.height())
                return 20;
            else
                return (winH / 2 - (_dialog.height() / 2));
        }
    }
    function calcTopW() {
        var winW = $(window).width();
        if (winW < _dialog.width())
            return 0;
        else
            return (winW / 2 - (_dialog.width() / 2));

    }
    var hide = function () {

        $('div#page').css('position', 'relative');

        _mask.hide();
        if (_dialog !== undefined && _dialog !== null) {
            _dialog.hide();
            _dialog.find('.dialog-content').empty();
            _dialog.find('.dialog-action button#confirm_button_modal').unbind();
        }



        destroy();

    };

    var showButtons = function () {

        $('.dialog-action button').show();

    };

    var hideButtons = function () {

        $('.dialog-action button').hide();
    };

    var getConfirmButton = function () {

        return _dialog.find('.dialog-action #confirm_button_modal');

    };
    var getCancelButton = function () {

        return _dialog.find('.dialog-action #cancel_button_modal');

    }
    return {
        getConfirmButton: getConfirmButton,
        getCancelButton: getCancelButton,
        hideButtons: hideButtons,
        showButtons: showButtons,
        hide: hide,
        resize: resize,
        setHeight: setHeight,
        setWidth: setWidth,
        destroy: destroy,
        show: show
    };

})();
