var g = (function () {

    var _fontsize = 11;

    var redirect = function (url) {
        window.document.location.href = url;
    };

    var isEmpty = function (text) {

        return $.trim(text).length == 0;
    };

    var isNumber = function (o) {

        return typeof o === 'number' && isFinite(o);
    };

    var createLoader = function ($target) {

        var loader = $('<img src="http://www.espacoaluno.uniceub.br/content/images/loading.gif" alt="" />');
        $target.append(loader);
        return loader;

    };

    var getScript = function (path) {

        $.ajaxSetup({ async: false });
        //$.getScript('../../scripts/md5.js');
        $.getScript(path);
        $.ajaxSetup({ async: true });

    };

    var newGuid = function () {
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    };

    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };

    var isValidUrl = function (url) {
        //var reg1str = "^http:\/\/www.";
        var reg1str = "^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$";
        var reg1 = new RegExp(reg1str);

        if (reg1.test(url))
            return true;
        return false;
    };

    var isValidMail = function (url) {
        var reg1str = "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$";
        var reg1 = new RegExp(reg1str);

        if (reg1.test(url))
            return true;
        return false;
    };

    var validateName = function (nmPessoa) {

        if (nmPessoa == null)
            return "Não informado";

        nmPessoa = nmPessoa.toString().replace("'", "");

        var partes = nmPessoa.toString().split(' ');
        var naoUtilizar = ["da", "de", "do", "dos", "e", "a", "o", "i", "das", "um", "uma", "uns", "umas"];
        var useParte2 = true;

        if (partes.length == 1) {
            return partes[0];
        } else if (partes.length == 2) {
            for (i = 0; i < naoUtilizar.length; i++) {
                if (naoUtilizar[i].toLowerCase() == partes[1]) {
                    useParte2 = false;
                    break;
                }
            }

            if (useParte2)
                return partes[0] + ' ' + partes[1];
            else
                return partes[0];
        } else if (partes.length >= 3) {
            for (var j = 0; j < naoUtilizar.length; j++) {
                if (naoUtilizar[j].toLowerCase() == partes[1]) {
                    useParte2 = false;
                    break;
                }
            }

            if (useParte2)
                return partes[0] + ' ' + partes[1];
            else
                return partes[0] + ' ' + partes[2];
        }

        return '';
    };

    var decreaseAA = function (target) {

        if (_fontsize > 8) {
            _fontsize -= 1;
            $('#' + target).css({ 'font-size': _fontsize + 'px' });
        }
    };

    var increaseAA = function (target) {
        if (_fontsize < 24) {
            _fontsize += 1;
            $('#' + target).css({ 'font-size': _fontsize + 'px' });
        }
    };

    var focus = function (target) {

        var $target = $(target);

        if ($target.length) {
            var targetOffset = $target.offset().top;
            $('html,body').animate({ scrollTop: targetOffset }, 1000);
        }

    };

    var showInfo = function (text, autoHide) {

        text = '<div>' + text + '</div>';

        modal.show({
            width: '480px',
            caption: 'Informação',
            content: text,
            cancelButtonText: 'Fechar'
        });

        if (autoHide !== undefined && autoHide) {
            setTimeout(function () {
                modal.hide();
            }, 1500);
        }
    };

    var showError = function (text, autoHide) {

        if (text === undefined)
            text = '<div>Ocorreu um erro. Estamos trabalhando para resolver o mais rápido possível.</div>';
        else
            text = '<div>' + text + '</div>';

        modal.show({
            width: '480px',
            caption: 'Erro',
            content: $(text),
            cancelButtonText: 'Fechar'
        });

        if (autoHide !== undefined && autoHide) {
            setTimeout(function () {
                modal.hide();
            }, 1500);
        }
    };

    var isArray = function (object) {
        return (typeof (object.length) == "undefined") ? false : true;
    };

    var showSessionExpired = function () {

        modal.show({
            width: '420px',
            caption: 'Sessão Expirada',
            content: '<div>Você será redirecionado para página de login.</div>',
            hideAction: true,
            disabledClickMask: true,
            loaded: function () {

                setTimeout(function () {
                    modal.hide();
                    g.redirect('http://www.uniceub.br');
                }, 2000);

            }
        });

    };

    var decodeTag = function (text) {

        text = text.replace('&lt;br /&gt;', '<br />');
        text = text.replace('&lt;br&gt;', '<br />');
        text = text.replace('&lt;br &gt;', '<br />');
        text = text.replace('&lt;b&gt;', '<b>');
        text = text.replace('&lt;/b&gt;', '</b>');
        text = text.replace('&lt;p&gt;', '<p>');
        text = text.replace('&lt;/p&gt;', '</p>');
        text = text.replace('&lt;em&gt;', '<em>');
        text = text.replace('&lt;/em&gt;', '</em>');
        text = text.replace('&lt;i&gt;', '<i>');
        text = text.replace('&lt;/i&gt;', '</i>');
        text = text.replace('&lt;strong&gt;', '<strong>');
        text = text.replace('&lt;/strong&gt;', '</strong>');
        text = text.replace('&lt;u&gt;', '<u>');
        text = text.replace('&lt;/u&gt;', '</u>');
        text = text.replace('&lt;li&gt;', '<li>');
        text = text.replace('&lt;/li&gt;', '</li>');
        return g.recursiveDecodeTag(text);
    };

    var recursiveDecodeTag = function (text) {

        if (text.indexOf('&lt;br /&gt;') > -1 |
            text.indexOf('&lt;br&gt;') > -1 |
            text.indexOf('&lt;br &gt;') > -1 |
            text.indexOf('&lt;b&gt;') > -1 |
            text.indexOf('&lt;/b&gt;') > -1 |
            text.indexOf('&lt;p&gt;') > -1 |
            text.indexOf('&lt;/p&gt;') > -1 |
            text.indexOf('&lt;em&gt;') > -1 |
            text.indexOf('&lt;/em&gt;') > -1 |
            text.indexOf('&lt;i&gt;') > -1 |
            text.indexOf('&lt;/i&gt;') > -1 |
            text.indexOf('&lt;strong&gt;') > -1 |
            text.indexOf('&lt;/strong&gt;') > -1 |
            text.indexOf('&lt;u&gt;') > -1 |
            text.indexOf('&lt;/u&gt;') > -1 |
            text.indexOf('&lt;li&gt;') > -1 |
            text.indexOf('&lt;/li&gt;') > -1) {
            return g.decodeTag(text);
        } else {
            return text;
        }
    };

    var detectUrl = function (text, encoded) {

        if (encoded == undefined)
            encoded = false;

        if (text != undefined) {

            var pattern = /\b((?:https?:\/\/|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'".,<>?«»“”‘’]))/ig;
            var match = new RegExp(pattern);

            var urls = text.match(match);

            if (urls != null) {
                var result = [urls.length];

                //text = text.replace(mainPattern, "<a target='_blank' href=\"$&\">$&</a>");
                var url;
                var index;

                for (var i = 0; i < urls.length; i++) {

                    url = urls[i].replace('&lt;br', '');

                    if (url.indexOf('http://') == -1)
                        url = 'http://' + url;

                    index = (encoded ? url.indexOf('&quot') : url.indexOf('"'));

                    if (index != -1)
                        url = url.substr(0, index);

                    result[i] = { url: urls[i], corretUrl: url };
                }
            }
        }

        return result;
    };

    var findUrl = function (params) {

        if (!params.disableClickYoutube) {
            params.disableClickYoutube = false;
        }

        var addVideo = false;
        var urlVideo = '';
        var urlEncode = '';
        var text = undefined;
        var videoOptions = {};
        var dataHide = params.disableClickYoutube;
        var hider1 = new hider();

        if (params.text != undefined)
            text = params.text;

        if (params.videoOptions != undefined)
            videoOptions = params.videoOptions;

        if (params.videoOptions.dataHide != undefined)
            dataHide = params.videoOptions.dataHide;

        if (text != undefined) {

            var urls = g.detectUrl(text, true);

            if (urls != null) {

                for (var i = 0; i < urls.length; i++) {

                    if (g.isYoutTubeUrl(urls[i].corretUrl) & !addVideo) {
                        urlVideo = urls[i].url;
                        addVideo = true;
                    }

                    if (addVideo) {
                        text = text.replace(urls[i].url, '');

                        if ($.trim(text).length > 0)
                            text += '<br /><br />';
                    } else {
                        urlEncode = Encoder.urlEncode(urls[i].corretUrl);
                        text = text.replace(urls[i].url + ' /&gt;', '<a target="_blank" href="redirect.ashx?url=' + urlEncode + '">' + urls[i].corretUrl + '</a> ');
                        text = text.replace(urls[i].url, '<a target="_blank" href="redirect.ashx?url=' + urlEncode + '">' + urls[i].corretUrl + '</a>');
                    }

                }

                if (addVideo) {
                    if (dataHide) {
                        text = '<div class="ea-url-video" data-hide>' + text + g.youTubeClientScript(urlVideo, videoOptions) + '</div>';

                        hider1.init({
                            elem: $(text),
                            callback: function () {
                            }
                        });
                        text = hider1.GetWarapper();
                    } else
                        text = '<div class="ea-url-video">' + text + g.youTubeClientScript(urlVideo, videoOptions) + '</div>';
                } //text = youTubeClientScript(urlVideo);
            }

            /*Corrige bug caso o link tenha sido inserido e logo após ter feito uma quebra de linha*/
            if (text.indexOf('</a> /&gt;') > -1)
                text = text.replace('</a> /&gt;', '</a>');
        }

        return text;
    };

    var youTubeClientScript = function (url, options) {

        var id = url.match(/[\?&]v=(.*?)(?:&|$)/);
        var videoWidth = options.videoWidth == undefined ? 300 : options.videoWidth;
        var videoHeight = options.videoHeight == undefined ? 199 : options.videoHeight;
        var autoplay = options.autoplay == undefined ? 0 : options.autoplay == true ? 1 : 0;

        if (id != null && id.length >= 2)
            id = id[1];
        else if (id == null && url.toLowerCase().indexOf('embed') > -1) {

            var parts = url.split('/');
            id = parts[parts.length - 1];
        }

        if (id != null)
            return '<iframe id="' + g.newGuid() + '" rule="youtube" width="' + videoWidth + '" height="' + videoHeight + '" src="//www.youtube.com/embed/' + id + '?rel=0&wmode=transparent&autoplay=' + autoplay + '" frameborder="0" allowfullscreen></iframe>';
        else
            return '';

    };

    var isYoutTubeUrl = function (url) {

        return (url.indexOf('www.youtube.com') > -1);

    };

    var getUrl = function () {

        return window.location.protocol + '//' + window.location.host + window.location.pathname;

    };

    var resetUrl = function () {

        window.location.href = g.getUrl();

    };

    var disableButton = function ($button, text) {
        $button.attr('disabled', true);
        $button.val(text);
    };

    var enableButton = function ($button, text) {
        $button.attr('disabled', true);
        $button.val(text);
    };

    var convertStringToDate = function (dateString) {
        if (dateString == undefined | dateString.length != 10)
            throw 'Invalid date format';

        var day = dateString.substr(0, 2);
        var month = dateString.substr(3, 2);
        var year = dateString.substr(6, 4);

        return new Date(year, month, day);

    };

    var parseJSON = function (data) {

        try {
            if (typeof data !== 'object') {

                if (data.indexOf('isChromeWebToolbarDiv') > -1)
                    data = data.replace('<div id="isChromeWebToolbarDiv" style="display:none"></div>', '');

                //if ($.browser.chrome || $.browser.mozilla) {
                //    data = $(data)[0].innerHTML;
                //    data = $.parseJSON(data);
                //}
                //else {
                //    data = $.parseJSON(data);
                //}

                //            if ($.browser.chrome || $.browser.mozilla) {
                //                return $(data).text();
                //            }
                //        else if ($.browser.opera) {

                //        }
                //        else if ($.browser.safari) {

                //        }

                return $.parseJSON(data);
            }

            return data;
        }
        catch (e) {

            showError();

        }
    };

    var getMonthDescription = function (mes) {
        switch (mes) {
            case 1:
                return 'Janeiro';
            case 2:
                return 'Fevereiro';
            case 3:
                return 'Março';
            case 4:
                return 'Abril';
            case 5:
                return 'Maio';
            case 6:
                return 'Junho';
            case 7:
                return 'Julho';
            case 8:
                return 'Agosto';
            case 9:
                return 'Setembro';
            case 10:
                return 'Outubro';
            case 11:
                return 'Novembro';
            case 12:
                return 'Dezembro';
            default:
                return '';
        }
    };

    var mesAtual = function () {
        return (new Date()).getMonth() + 1;
    };

    var maxLength = function (text, length, concat) {

        if (text.length > length) {
            return text.substr(0, length) + (concat != undefined & $.trim(concat).length > 0 ? concat : '');
        }

        return text;

    };

    return {
        resetUrl: resetUrl,
        getUrl: getUrl,
        isYoutTubeUrl: isYoutTubeUrl,
        youTubeClientScript: youTubeClientScript,
        findUrl: findUrl,
        detectUrl: detectUrl,
        recursiveDecodeTag: recursiveDecodeTag,
        decodeTag: decodeTag,
        showSessionExpired: showSessionExpired,
        showError: showError,
        isArray: isArray,
        showInfo: showInfo,
        focus: focus,
        increaseAA: increaseAA,
        decreaseAA: decreaseAA,
        validateName: validateName,
        isValidMail: isValidMail,
        isValidUrl: isValidUrl,
        newGuid: newGuid,
        getScript: getScript,
        createLoader: createLoader,
        isNumber: isNumber,
        isEmpty: isEmpty,
        redirect: redirect,
        JSONSTATUS_SUCCESS: 'success',
        JSONSTATUS_ERROR: 'error',
        JSONSTATUS_SESSIONEXPIRED: 'sessionexpired',
        JSONSTATUS_BUSINESSRULE: 'businessrule',
        disableButton: disableButton,
        enableButton: enableButton,
        convertStringToDate: convertStringToDate,
        parseJSON: parseJSON,
        mesAtual: mesAtual,
        getMonthDescription: getMonthDescription,
        maxLength: maxLength
    };

})();

