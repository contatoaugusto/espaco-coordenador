var timeline = (function () {

    var _pageSize = 10;
    var _currentYear;
    var _currentMonth;
    var _selectedYear;
    var _selectedMonth;
    var _pagination = [];
    var _contentLoader;
    var _contentLoaderMore;
    var _idUsuarioSession;
    var _initialized = false;
    var _isEA = true;

    var init = function (idUsuarioSession, currentYear, currentMonth) {

        _idUsuarioSession = idUsuarioSession;
        _currentYear = _selectedYear = currentYear;
        _currentMonth = _selectedMonth = currentMonth;

        createContentsMonth();

        //Verifica se a requisição é EA ou EP
        _isEA = (window.location.toString().indexOf('.aspx') > -1);

        setCursorYear(currentYear);
        setCursorMonth(currentMonth);

        bindEvent();

        showLoaderMonth(currentYear, false);

        load(currentYear, currentMonth);
    };

    function initPagination(year, month) {

        var pag = _pagination[year.toString() + month.toString()];

        if (pag == undefined)
            pag = _pagination[year.toString() + month.toString()] = { 'pageIndex': 0, 'total': 0, 'totalLoaded': 0 };

        return pag;

    }

    function showContentMonth(year, month) {

        var $year = $('.timeline-year-content[arg="' + year + '"]');

        $('.timeline-month[arg="' + month + '/' + year + '"]', $year).show();
        $('.timeline-month-content[arg="' + month + '/' + year + '"]', $year).show();

    };

    function hideContentsMonth() {

        var $years = $('.timeline-year');

        $.each($years, function () {

            var year = $(this).attr('arg');

            var $year;

            for (var month = 12; month >= 1; month--) {

                if (_selectedYear != year || _selectedYear == year & _selectedMonth != month) {

                    $year = $('.timeline-year-content[arg="' + year + '"]');

                    $('.timeline-month[arg="' + month + '/' + year + '"]', $year).hide();
                    $('.timeline-month-content[arg="' + month + '/' + year + '"]', $year).hide();
                }

            }

        });

    };

    function createContentsMonth() {

        var $years = $('.timeline-year');
        var year;

        $.each($years, function (index, intem) {

            year = $(this).attr('arg');

            for (var month = 12; month >= 1; month--)
                createMonthContent(year, month);
        });

    };

    var load = function (year, month, hideMore) {

        if (hideMore == undefined)
            hideMore = false;

        var showPreviousMonth = false;
        var pag = initPagination(year, month);

        var $more = createMore(month, year);

        Api.get({
            url: "coorporativo/timeline",
            data: {
                idOperacao: 1,
                idUsuario: _idUsuarioSession,
                aaAtual: year,
                mmAtual: month,
                nuPagina: pag.pageIndex,
                nuRegistros: _pageSize
            },
            success: function (response) {

                if (!_initialized) {
                    hideContentsMonth();
                    _initialized = true;
                }

                if (response.status == Api.status.SUCCESS) {

                    if (response.data.length > 0) {

                        pag.pageIndex++;

                        if (pag.total == 0 & response.data.length > 0) {
                            $more.show();
                            pag.total = response.total;
                        }

                        pag.totalLoaded += response.data.length;

                        if (pag.totalLoaded >= pag.total) {
                            showPreviousMonth = true;
                            $more.hide();
                        }

                        var order = 'L';

                        createMonthContent(year, month);

                        $(response.data).each(function (index, item) {

                            if (order == 'L') {
                                getItemColumnLeft(item);
                                order = 'R';
                            }
                            else {
                                getItemColumnRight(item);
                                order = 'L';
                            }

                            if (index >= response.data.length - 1) {

                                if (hideMore) {
                                    hideLoaderMore(month, year);
                                } else {
                                    hideLoaderMonth(year);
                                }

                                resizeContent(month, year);
                            }

                        });

                    }
                    else {

                        if (pag.pageIndex == 0) {
                            $('#timeline-loader').hide();
                        }

                        showPreviousMonth = true;

                        if (hideMore)
                            hideLoaderMore(month, year);
                        else
                            hideLoaderMonth(year);

                        //Criar o alerta somente se não existir ocorrencias no mes
                        if (!verifyEventsForMonth(year, month))
                            createAlertEmpty(month, year);
                    }

                    //Verifica se existe um mês a ser mostrado
                    if (month > 1 & month < 12) {
                        if (showPreviousMonth)
                            showContentMonth(year, month - 1);
                    }
                }

            }
        });

    };

    function verifyEventsForMonth(year, month) {

        var $contentleft = $('.timeline-year-content[arg=' + year + '] .timeline-month-content[arg="' + month + '/' + year + '"] .timeline-month-column-left');

        return $contentleft.children().length > 0;

    }

    function resizeContent(month, year) {

        var $contentleft = $('.timeline-year-content[arg=' + year + '] .timeline-month-content[arg="' + month + '/' + year + '"] .timeline-month-column-left');
        var $contentright = $('.timeline-year-content[arg=' + year + '] .timeline-month-content[arg="' + month + '/' + year + '"] .timeline-month-column-right');

        var height = 0;
        var heightleft = $contentleft.height();
        var heightright = $contentright.height();

        if (heightleft > heightright)
            height = heightleft;
        else if (heightright > heightleft)
            height = heightright;

        $contentleft.height(height);

        $contentright.height(height);

    }

    function createMore(month, year) {

        var $item = $('.timeline-month-more[arg="' + month + '/' + year + '"]');

        if (!$item.length) {

            var html = '<div class="cb"></div>';
            html += '<div class="timeline-month-more" arg="' + month + '/' + year + '"><a>Mais</a></div>';
            html += '<div class="timeline-month-column-left"></div>';
            html += '<div class="timeline-month-column-right"></div>';

            $item = $(html);

            var $monthContent = $('.timeline-year-content[arg=' + year + '] .timeline-month-content[arg="' + month + '/' + year + '"]');

            $item.insertAfter($monthContent);

            $item.bind('click', function () {

                showLoaderMore(month, year);

                load(year, month, true);

            });

        }

        return $item;
    }

    function createAlertEmpty(month, year) {

        var $item = $('.timeline-month-empty[arg="' + month + '/' + year + '"]');

        if (!$item.length) {

            var html = '<div class="cb"></div>';
            html += '<div class="timeline-month-empty" arg="' + month + '/' + year + '">Nenhuma ocorrência encontrada</div>';
            $item = $(html);

            var $monthContent = $('.timeline-year-content[arg=' + year + '] .timeline-month-content[arg="' + _selectedMonth + '/' + year + '"]');

            $item.insertAfter($monthContent);

        }
    }

    function hideLoaderMore(month, year) {

        var $more = $('.timeline-month-more[arg="' + month + '/' + year + '"]');

        _contentLoader = $more.children().detach();

        $more.html('');
        $more.append(_contentLoaderMore);

    }

    function showLoaderMore(month, year) {

        var $more = $('.timeline-month-more[arg="' + month + '/' + year + '"]');

        _contentLoaderMore = $more.children().detach();

        $more.html('');
        $more.append(_contentLoader);
        _contentLoader.show();

    }

    function hideLoaderMonth(year) {

        var $yearContent = $('.timeline-year-content[arg=' + year + ']');
        var $month = $('.timeline-month[arg="' + _selectedMonth + '/' + year + '"]', $yearContent);

        var $img = $month.find('img').detach();

        $month.html('');
        $month.append(_contentLoader);

        _contentLoader = $img;
    }

    function showLoaderMonth(year, setFocus) {

        if (setFocus == undefined)
            setFocus = true;

        var $img = $('#timeline-loader');

        if (!$img.length) {
            var pathLoader = '/content/images/loading.gif';
            $img = $('<img id="timeline-loader" class="dn" src="' + pathLoader + '" alt="Carregando" />');


            $('#timeline').prepend('<center />');
            $('#timeline').find('center').append($img);
        }

        var $yearContent = $('.timeline-year-content[arg=' + year + ']');

        var $monthHeader = $('.timeline-month[arg="' + _selectedMonth + '/' + year + '"]', $yearContent);
        var $monthContent = $('.timeline-month-content[arg="' + _selectedMonth + '/' + year + '"]', $yearContent);

        $monthHeader.show();
        $monthContent.show();

        _contentLoader = $monthHeader.children().detach();

        $monthHeader.html('');
        $monthHeader.append($img);
        $img.show();

        if (setFocus)
            $monthHeader.animatefocus();

    }

    function getItemColumn(item) {

        var cssClass = '';

        if (item.coTipo == 'T')
            cssClass = ' timeline-item-twitter';

        var html = '<div class="timeline-item' + cssClass + '">';

        html += '<img class="tn-set" alt="" />';

        html += '<div class="timeline-item-header">';

        if (item.coTipo != 'T')
            html += '<img src="http://servicos.uniceub.br/api/coorporativo/foto/?idUsuario=' + item.idUsuario + '&idOperacao=1" alt="' + item.nmPessoa + '" />';
        else
            html += '<img src="/Content/Images/ico-twitter.png" alt="@UniCEUB_Oficial" />';

        var nmPessoa = '';

        if (item.coTipo == 'M' | item.coTipo == 'A' | item.coTipo == 'C' | item.coTipo == 'N' | item.coTipo == 'P') {

            if (item.idUsuario == -1)
                nmPessoa = 'UniCEUB';
            else
                nmPessoa = g.validateName(item.nmPessoa);

            html += '<div class="timeline-item-header-name">' + nmPessoa + '</div>';
            html += '<br />';
        }

        var action = '';

        switch (item.coTipo) {
            case 'M':
                if (!_isEA) {

                    var url = '/comunicacao/mensagem';

                    if (_idUsuarioSession == item.idUsuario) {
                        url = '/comunicacao/mensagens/enviadas';
                    }

                    url = "javascript:g.redirect('" + url + "/" + item.idTimeline + "')";
                } else {
                    url = '#';
                }

                action = '<a id="mensagem-' + item.idTimeline + '" href="' + url + '" class="link">Ler mensagem</a>';
                getUrlMensagem(item.idTimeline, item.idUsuario);
                break;

            case 'A':
                action = '<img alt="Baixar Arquivo" src="/Content/Images/link.png"/> <a id="arquivo-' + item.idTimeline + '" class="link">Baixar arquivo</a>';
                getUrlArquivo(item.idTimeline);
                break;

            case 'N':
                action = "<a href=\"javascript:timeline.showNoticia('" + item.idTimeline + "');\" class=\"link\">Ler notícia</a>";
                break;

            case 'C':
                action = '<a id="evento-' + item.idTimeline + '" class="link">Ir para o evento</a>';
                getUrlCalendario(item.idTimeline);
                break;

            case 'P':

                item.deTitulo = 'Mural - ' + item.deTitulo;
                action = '';
                break;
        }

        html += '<div class="timeline-item-header-discipline">' + item.deTitulo + '</div>';
        html += '</div>';
        html += '<div class="timeline-item-text">' + stripHtml({ html: item.deTimeline }) + '</div>';

        if (action.length > 0) {

            html += '<div class="timeline-item-action">  ' + action + '</div>';

        }

        //Desabilitado devido a url que é necessario com os dados da notícia
        if (item.coTipo == 'N') {
            html += '<div class="timeline-item-social">';
            html += getSocialShortCuts(item, true);
            html += '</div>';
        }

        html += '<div class="timeline-item-elapsetime">Em ' + item.dtTimeline.replace(' ', ' às ') + '</div>';

        html += '</div>';

        var $item = $(html);

        animateSocialShortCut($('.timeline-item-social-facebook', $item));
        animateSocialShortCut($('.timeline-item-social-twitter', $item));

        return $item;

    }

    var showNoticia = function (idPublicacao) {

        var dataPost = { 'idPublicacao': idPublicacao };

        $.ajax({
            type: 'POST',
            url: 'timelinenoticia.ashx',
            data: dataPost,
            dataType: 'json',
            success: function (data) {

                if (data.status == 'success') {

                    var $html = $('<div id="timeline-noticia"></div>');

                    $html.empty();
                    $html.append(getHtmlNoticia(data.response));

                    modal.show({
                        'width': '550px',
                        'caption': data.response.deTitulo,
                        'content': $html,
                        'enableScroll': false,
                        'cancelButtonText': 'Fechar'
                    });
                } else if (data.status == 'success') {
                    g.showSessionExpired();
                }

            }
        });

    };

    function getHtmlNoticia(item) {
        var html = '<div class="timeline-noticia-text">';

        //Redes Sociais
        html += '<div class="timeline-noticia-social">';
        html += getSocialShortCuts(item);
        html += '</div>';

        if ($.trim(item.nmAuto).length > 0) {
            html += 'Por ' + item.nmAutor;
            html += '<br />';
        }

        if ($.trim(item.deFonte).length > 0) {
            html += 'Fonte: ' + item.dtPublicacao.split(' ')[0] + ' - ' + item.deFonte;
        }
        else
            html += 'Fonte: ' + item.dtPublicacao.split(' ')[0];

        html += '<br /><br />';

        html += Encoder.htmlDecode(Encoder.htmlDecode(item.deConteudo));

        if ($.trim(item.edURL).length > 0) {
            html += '<br /><br />';
            html += '<a href="' + item.edURL + '" target="_blank">Veja Mais</a>';
        }

        html += '<br /><br />';

        html += '</div>';

        html += '<div class="timeline-noticia-images"></div>';

        var $html = $(html);

        getHtmlImagemNoticia($html, item.idDetalhamentoPublicacao);

        return $html;
    }

    function getSocialShortCuts(item, mini) {


        var deTitulo = Encoder.urlEncode(item.deTitulo);

        var edUrlNoticia = Encoder.urlEncode('https://www.espacoaluno.uniceub.br/informacoes/informativos/noticias/noticiapublica.aspx?' + item.idTimeline);

        var edUrlFacebook = 'http://www.facebook.com/share.php?t=' + deTitulo + '&u=' + edUrlNoticia;
        var edUrlTwitter = 'http://twitter.com/share/?url=' + edUrlNoticia + '&text=' + deTitulo;

        var html = '';

        //Facebook
        html += '<a title="Compartilhar no Facebook" class="ultimo" href="' + edUrlFacebook + '" target="_blank">';
        html += '<img class="timeline-item-social-facebook" alt="Compartilhar no Facebook" src="/Content/Images/ico-facebook' + (mini === true ? '-min' : '') + '.gif" complete="complete" />';
        html += '</a>';

        //Twitter
        html += '<a title="Compartilhar no Twitter" class="ultimo" href="' + edUrlTwitter + '" target="_blank">';
        html += '<img class="timeline-item-social-twitter" alt="Compartilhar no Twitter" src="/Content/Images/ico-twitter' + (mini === true ? '-min' : '') + '.gif" complete="complete" />';
        html += '</a>';

        return html;

    }

    function animateSocialShortCut($img) {

        $img.bind('mouseover', function () {

            var src = $(this).attr('src');
            $(this).attr('src', src.replace('-min', ''));

        }).bind('mouseout', function () {

            var src = $(this).attr('src');
            $(this).attr('src', src.replace('.gif', '-min.gif'));

        });

    }

    function getUrlMensagem(idTimeline, idUsuario) {

        if (_isEA) {

            var url = '/comunicacao/mensagens/mensagem.aspx';

            if (_idUsuarioSession == idUsuario)
                url = '/comunicacao/mensagens/mensagemenviada.aspx';

            $.ajax({
                type: 'POST',
                url: '/rede/encrypturlmensagem.ashx?' + idTimeline,
                dataType: "text",
                data: { idTimeline: idTimeline },
                success: function (data) {

                    if (data != null) {
                        var $link = $('#mensagem-' + idTimeline);
                        $link.attr('href', "javascript:g.redirect('" + url + data.toString() + "');");
                    }
                },
                error: function () {

                    g.showError();

                }
            });
        }

    }

    function getUrlArquivo(idTimeline) {

        $.ajax({
            type: 'POST',
            url: '/rede/encrypturlarquivo.ashx?' + idTimeline,
            dataType: "text",
            data: { idTimeline: idTimeline },
            success: function (data) {

                if (data != null) {

                    var $link = $('#arquivo-' + idTimeline);
                    $link.attr('href', "javascript:g.redirect('downloadarquivo.ashx" + data.toString() + "');");
                }
            }
        });

    }

    function getHtmlImagemNoticia($html, idDetalhamentoPublicacao) {

        var $images = $($html[1]);

        var dataPost = { 'idDetalhementoPublicacao': idDetalhamentoPublicacao };

        $.ajax({
            type: 'POST',
            url: 'timelineimagensnoticia.ashx',
            data: dataPost,
            dataType: 'json',
            success: function (data) {

                if (data.status == 'success') {

                    var width;

                    $.each(data.response, function (index, item) {

                        width = item.nuWidth;

                        if (item.nuWidth > 350)
                            width = '350';

                        $images.append($('<img width="' + width + 'px" src="/ImageNotice.ashx?' + item.idImagemPublicacao + '" alt="' + item.deLegenda + '" />'));
                        $images.append('<br />');
                        $images.append('<span class="timeline-noticia-legenda">' + item.deLegenda + '</span>');
                        $images.append('<br /><br />');

                    });

                }
                else if (data.status == 'sessionexpired') {
                    g.showSessionExpired();
                }

            }
        });

    }

    function getUrlCalendario(idTimeline) {

        $.ajax({
            type: 'POST',
            url: '/rede/encrypturlcalendario.ashx?' + idTimeline,
            dataType: "text",
            data: { idTimeline: idTimeline },
            success: function (data) {

                if (data != null) {

                    var $link = $('#evento-' + idTimeline);
                    $link.attr('href', "javascript:g.redirect('/informacoes/informativos/calendario/evento.aspx" + data.toString() + "');");
                }
            }
        });

    }

    function getItemColumnLeft(item) {

        var pathImage = 'content/images/tn-set-left.png';

        var $item = getItemColumn(item);

        var $img = $item.find('.tn-set');

        $img.attr('src', pathImage);
        $img.addClass('timeline-item-set-left');

        var $content = $('.timeline-year-content[arg=' + item.aaAtual + ']').find('.timeline-month-content[arg="' + item.mmAtual + '/' + item.aaAtual + '"]');

        $('.timeline-month-column-left', $content).css('height', 'auto');

        $('.timeline-month-column-left', $content).append($item);

        return $item;
    }

    function getItemColumnRight(item) {

        var pathImage = 'content/images/tn-set-right.png';

        var $item = getItemColumn(item);

        var $img = $item.find('.tn-set');

        $img.attr('src', pathImage);
        $img.addClass('timeline-item-set-right');

        var $content = $('.timeline-year-content[arg=' + item.aaAtual + ']').find('.timeline-month-content[arg="' + item.mmAtual + '/' + item.aaAtual + '"]');

        $('.timeline-month-column-right', $content).css('height', 'auto');

        $('.timeline-month-column-right', $content).append($item);

        return $item;

    }

    function bindEvent() {

        $('#timeline-title').bind('click', function () {

            setCursorMonth();
            setCursorYear();

            showLoaderMonth(_currentYear);

            load(_currentYear, _currentMonth);

        });

        $('#timeline-nav-year a').bind('click', function () {

            _selectedYear = $(this).text();


            _selectedMonth = 12;
            setCursorMonth();
            setCursorYear();

            showLoaderMonth(_selectedYear);

            load(_selectedYear, _selectedMonth);

        });

        $('#timeline-nav-month a').bind('click', function () {

            _selectedMonth = $(this).attr('arg');

            setCursorMonth();

            showLoaderMonth(_selectedYear);

            $('.timeline-year[arg=' + _selectedYear + ']').animatefocus();

            load(_selectedYear, _selectedMonth);

        });

        $('.timeline-year a').bind('click', function () {

            var year = $(this).attr('arg');

            for (var month = 12; month >= 1; month--) {

                var $year = $('.timeline-year-content[arg="' + year + '"]');

                $('.timeline-month[arg="' + month + '/' + year + '"]', $year).show();
                $('.timeline-month-content[arg="' + month + '/' + year + '"]', $year).show();
            }

            $(this).parent().animatefocus();
        });

        $('.timeline-year a').bind('dblclick', function () {

            var year = $(this).attr('arg');
            var $year;

            for (var month = 12; month >= 1; month--) {

                $year = $('.timeline-year-content[arg="' + year + '"]');

                $('.timeline-month[arg="' + month + '/' + year + '"]', $year).hide();
                $('.timeline-month-content[arg="' + month + '/' + year + '"]', $year).hide();
            }

        });

        $('.timeline-month a').bind('click', function () {

            var arg = $(this).parent().attr('arg');
            var month = arg.split('/')[0];
            var year = arg.split('/')[1];

            _selectedYear = year;
            _selectedMonth = month;

            setCursorMonth();
            setCursorYear();

            showLoaderMonth(_selectedYear);

            load(_selectedYear, _selectedMonth);
        });

    };

    function setCursorYear() {

        var $img = getCursorYear();

        var $year = $('#timeline-nav-year a[arg=' + _selectedYear + ']');

        $year.append($img);
    };

    function setCursorMonth() {

        var $img = getCursorMonth();

        var $month = $('#timeline-nav-month a[arg=' + _selectedMonth + ']');

        $month.append($img);
    };

    function getCursorYear() {

        var $img = $('#timeline-nav-year img');

        $img.show();

        return $img.detach();

    }

    function getCursorMonth() {

        var $img = $('#timeline-nav-month img');

        $img.show();

        return $img.detach();

    }

    function stripHtml(params) {

        var videoOptions = {}
        var html;

        if (params.videoOptions != undefined)
            videoOptions = params.videoOptions;

        videoOptions.videoWidth = 282;
        videoOptions.videoHeight = 200;

        html = g.findUrl({ text: params.html, videoOptions: videoOptions });
        html = g.decodeTag(html);
        return html;
    }

    function createMonthContent(year, month) {

        var $yearContent = $('.timeline-year-content[arg=' + year + ']');
        var $item = $('.timeline-month[arg="' + month + '/' + year + '"]', $yearContent);

        if (!$item.length) {
            var html = '<div class="cb"></div>';
            html += '<div class="timeline-month" arg="' + month + '/' + year + '"><a>' + getMonthDesc(month) + '</a><br /><span>' + year + '</span></div>';
            html += '<div class="timeline-month-content" arg="' + month + '/' + year + '">';
            html += '    <div class="timeline-month-column-left"></div>';
            html += '    <div class="timeline-month-column-right"></div>';
            html += '</div>';
            html += '<div class="cb"></div>';

            $item = $(html);

            $yearContent.append($item);
        }

        return $item;

    };

    function getMonthDesc(month) {

        switch (month.toString()) {
            case '1': return 'Janeiro';
            case '2': return 'Fevereiro';
            case '3': return 'Março';
            case '4': return 'Abril';
            case '5': return 'Maio';
            case '6': return 'Junho';
            case '7': return 'Julho';
            case '8': return 'Agosto';
            case '9': return 'Setembro';
            case '10': return 'Outubro';
            case '11': return 'Novembro';
            case '12': return 'Dezembro';
        }

        return '';
    };

    return {
        init: init,
        showNoticia: showNoticia,
        load: load
    };
})();