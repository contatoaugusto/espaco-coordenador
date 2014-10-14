var fontsize = 11;

$(document).ready(function () {

    if (!$.browser.mozilla) {
        $.preloadCssImages();
    }

    /*  Bug menu z-Index IE7 */
    /*
    if ($.browser.msie && $.browser.version == "7.0") {
        var zIndexNumber = 1000;
        $('div').each(function () {
            $(this).css('zIndex', zIndexNumber);
            zIndexNumber -= 10;
        });
    }
    */

    function addMega() {
        var li = $(this);
        var a = li.find('.out');

        a.removeClass('out');
        a.addClass('hover');

        li.addClass('hovering');
    }

    function removeMega() {
        var li = $(this);
        var a = li.find('.hover');

        a.removeClass('hover');
        a.addClass('out');

        li.removeClass('hovering');
    }

    var megaConfig = {
        interval: 100,
        sensitivity: 4,
        over: addMega,
        timeout: 100,
        out: removeMega
    };

    $("div#menu div#items li.mega").hoverIntent(megaConfig);

    var megaDadosPessoais = {
        interval: 100,
        sensitivity: 4,
        over: addMega,
        timeout: 100,
        out: removeMega
    };

    $("div#header div#detalhe div#menurapido ul li.mega").hoverIntent(megaDadosPessoais);

    $('table.gridview tr.gridviewpaging td table td').mouseover(function () {
        $(this).find('span').css('color', '#494848');
        $(this).find('a').css('color', '#494848');
    });

    $('table.gridview tr.gridviewpaging td table td').mouseout(function () {
        $(this).find('span').css('color', '#af1d22');
        $(this).find('a').css('color', '#545454');
    });

    $('.button-content').mouseover(function () {

        var path = $(this).find('.button-left').css('background-image');
        var key = '#IMAGE#';
        path = path.replace('button-left.png', key);

        $(this).find('.button-middle').css('color', '#ffffff');
        $(this).find('.button-left').css({ background: path.replace(key, 'button-left-hover.png') });
        $(this).find('.button-middle').css({ background: path.replace(key, 'button-middle-hover.png') });
        $(this).find('.button-right').css({ background: path.replace(key, 'button-right-hover.png') });
    });

    $('.button-content').mouseout(function () {

        var path = $(this).find('.button-left').css('background-image');
        var key = '#IMAGE#';
        path = path.replace('button-left-hover.png', key);

        $(this).find('.button-middle').css('color', '#494848');
        $(this).find('.button-left').css({ background: path.replace(key, 'button-left.png') });
        $(this).find('.button-middle').css({ background: path.replace(key, 'button-middle.png') });
        $(this).find('.button-right').css({ background: path.replace(key, 'button-right.png') });
    });


    $('img.foto').poshytip({
        className: 'tip-twitter',
        showTimeout: 1,
        alignTo: 'target',
        alignX: 'center',
        offsetY: 5,
        allowTipHover: true,
        fade: false,
        slide: false,
        content: function () {

            var src = $(this).attr('src');
            var style = 'style="border:1px solid #eaeaea;"';

            return '<img src="' + src + '" tooltip="" width="50px" height="55px" complete="complete" style="' + style + '" class="foto" />';

        }
    });

    $('.info').poshytip({
        className: 'tip-info',
        showTimeout: 1,
        alignTo: 'target',
        alignX: 'center',
        offsetY: 5,
        allowTipHover: true,
        fade: false,
        slide: false,
        content: function () {

            var src = $(this).attr('info');
            return src;

        }
    });

    $('.tiprede').poshytip({
        className: 'tip-rede',
        showTimeout: 1,
        alignTo: 'target',
        alignX: 'center',
        offsetY: 5,
        allowTipHover: true,
        fade: false,
        slide: false,
        content: function () {

            var src = $(this).attr('info');
            return src;

        }
    });

    $('#ctl00_ctl00_CPHContent_header_txtdePesquisa').watermark('Pesquisar no UniCEUB', { className: 'textbox-water' });

});

function decreaseAA(target) {

    if (fontsize > 8) {
        fontsize -= 1;
        $('#' + target).css({ 'font-size': fontsize + 'px' });
    }
}

function increaseAA(target) {
    if (fontsize < 24) {
        fontsize += 1;
        $('#' + target).css({ 'font-size': fontsize + 'px' });
    }
}

function redirect(url) {
    document.location.href = url;
}

function _print() {

    var title = $('title').html();
    _print(title);
}

function _print(title) {

    jQuery.each(jQuery.browser, function (i, val) {
        if (i == "chrome") {
            $('.print').printElement({ leaveOpen: true, printMode: 'popup', pageTitle: title });
            return false;
        }
        else if (i == "msie") {
            $('.print').printElement({ pageTitle: title });
            return false;
        }
    });    

    return false;
}

function resizeReport() {

    $('.print').find('table').each(function () {
        $(this).css('width', '100%');
    });
}

function searchInBing(url, sender) {

    redirect(url + '?' + $('#' + sender).val());
    return false;
}

function htmlEncode(html) {
    return $('<div/>').text(html).html();
}

function htmlDecode(text) {
    return $('<div/>').html(text).text(); 
}

function openPopup(url) {
    //    try {
    var height = 600;
    var width = 780;
    var name = 'POPUPEA';
    var toolbar = "height=" + height;
    toolbar += ",width=" + width;
    if (window.screen) {
        var ah = screen.availHeight - 30;
        var aw = screen.availWidth - 10;

        var xc = (aw - width) / 2;
        var yc = (ah - height) / 2;

        toolbar += ",left=" + xc + ",screenX=" + xc;
        toolbar += ",top=" + yc + ",screenY=" + yc;
    }
    toolbar += ",scrollbars=yes, statusbar=yes";
    toolbar += ",resizable=yes";

    window.open(url, name, toolbar);

    //        var flash = window.open(url, name, toolbar);
    //        flash.focus();
    //        return flash;
    //    }
    //    catch (e) {
    //        alert('Ocorreu um erro de script durante a tentativa de abrir uma pop-up. Verifique se o "Bloqueador de Pop-Ups" está ativado.');
    //    }
}

function back() {
    window.history.back();
}

function focus(selector) {

    var target = $(selector);

    if (target.length) {
        var targetOffset = target.offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
    }

}