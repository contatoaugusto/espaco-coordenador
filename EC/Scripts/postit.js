var postit = (function () {

    var _idUsuario;
    var _imagesPath = '/content/images/';
    var _ajaxUpload;
    var extExpr = /^(doc|pdf|xls|ppt|mdb|vsd|zip|accdb|xlsx|docx|txt|jpg|jpeg|gif|png|bmp|htm|html|dwg|pptx)$/;

    var init = function (idUsuario) {

        _idUsuario = idUsuario;
        $("#postit-list").isotope(
            {
                animationEngine: 'jquery',
                itemClass: 'postit-item',
                layoutMode: 'masonry',
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            }
        );
        load();
    },
    load = function () {

        $.ajax({
            type: 'GET',
            url: '/mural/getpostitlist.ashx',
            dataType: 'json',
            cache: false,
            success: function (data) {

                if (data.status == g.JSONSTATUS_SUCCESS) {

                    if (data.response != null) {

                        var length = 0;

                        //Completa os postits
                        //length += multiplyOffset(length, 4);
                        length = data.response.length + 1;

                        for (var i = 1; i <= length; i++) {
                            add(addItem(i));
                        }

                        if (data.response.length > 0) {

                            $.each(data.response, function (index, item) {
                                setToPostit(index + 1, item);
                            });

                        }
                    }
                    else {
                        add(addItem(0));
                    }
                }
                else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                    g.showSessionExpired();
                }
                else if (data.status == 'error') {
                    g.showError();
                }

            },
            error: function () {
                g.showError();
            }
        });

    };

    function loadItems($postit) {
        var $loader = $postit.find('img[data-rule=loader]');
        $loader.show();

        var dataPost = { 'idPostit': $postit.data().args };

        $.ajax({
            type: 'POST',
            url: '/mural/getpostititemlist.ashx',
            dataType: 'json',
            data: dataPost,
            cache: false,
            success: function (data) {

                if (data.status == g.JSONSTATUS_SUCCESS) {

                    if (data.response != null) {

                        if (g.isArray(data.response)) {
                            var length = data.response.length;
                            $.each(data.response, function (index, item) {
                                addItemToList($postit, item);
                                if (index == length - 1)
                                    loadTags();
                            });

                        }
                        else {
                            addItemToList($postit, data.response);

                            loadTags();
                        }
                    }
                }
                else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                    g.showSessionExpired();
                }
                else {
                    g.showError();
                }

                $loader.hide();
            }
        });

    };

    function loadTags() {

        tags.load({
            target: '.postit-tag-filters',
            filter: function (a) {

                var selector = '.postit-item-virtual, #postit-list .postit-item:has(.postit-itemlist[data-tags*="' + a.title + '"])';
                $('#postit-list').isotope({ filter: selector, animationEngine: 'jquery' });

            }

        });

    };

    function addItemToList($postit, postit, created) {
        var html = '<div class="postit-itemlist" data-args="' + postit.idPostitItem + '" data-tags="' + (postit.deTag == undefined ? '' : postit.deTag) + '">';

        var deItem = stripHtml({ html: postit.deItem });
        var deItemDecoded = Encoder.htmlDecode(deItem);

        if (_idUsuario != postit.idUsuario) {
            html += '<div class="postit-itemlist-content-share">';
            html += '<img class="postit-itemlist-photo foto medium" src="http://servicos.uniceub.br/api/coorporativo/foto/?idUsuario=' + postit.idUsuario + '&idOperacao=1" alt="' + postit.nmPessoa + '" />';
            html += '<div class="postit-itemlist-name">' + g.validateName(postit.nmPessoa) + '</div>';

            if (g.isYoutTubeUrl(deItem))
                html += '<div class="postit-itemlist-desc" style="text-align:center">' + deItemDecoded + '</div>';
            else
                html += '<div class="postit-itemlist-desc">' + deItemDecoded + '</div>';

            html += '<div class="postit-itemlist-date">Há ' + postit.dtCriacao + '</div>';
            html += '</div>';
        }
        else {

            html += '<div class="postit-itemlist-content">';
            html += '<div class="postit-itemlist-desc">';

            if (postit.idArquivo > 0) {

                html += '<div class="file">';

                if ($.trim(postit.nmArquivo) != $.trim(deItem)) {
                    html += Encoder.htmlDecode(postit.nmArquivo) + '<br />';
                }

                html += deItemDecoded;
                html += '<img src="' + _imagesPath + 'icon' + postit.deExtensaoPadrao + '.gif" alt="Arquivo" />';
                html += '</div>';
            }
            else {
                html += deItemDecoded;
            }
            html += '</div>';
            html += '<div class="postit-itemlist-content-date">Há ' + postit.dtCriacao + '</div>';
            html += '</div>';

        }

        //Verifica se o usuário é dono e se não é restrito para dele
        if (_idUsuario == postit.idUsuario) {
            html += '<div class="delete-item"></div>';
        }
        html += '</div>';

        var $item = $(html);

        if (created == undefined || !created) {
            $postit.find('div[data-rule=item-list]').append($item);
        } else {
            // Verifica se existe algum item cadastrado
            var $first = $postit.find('div[data-rule=item-list] div.postit-itemlist:eq(0)');

            if ($first.length) {
                $item.insertBefore($first);
            } else {
                $postit.find('div[data-rule=item-list]').append($item);
            }
        }
        tags.reLoad();
        bindPostitItemEvents($item);
    };

    function bindPostitItemEvents($item) {

        $item.bind('mouseover', function () {

            $(this).addClass('');
            $(this).removeClass('');

        }).bind('mouseout', function () {

            $(this).addClass('');
            $(this).removeClass('');

        }).bind('click', function (e) {

            e.preventDefault();

            var html = '<div class="postit-modal-content">';
            html += '<center><img data-rule="postit-modal-content-loader" src="' + _imagesPath + 'loading.gif" class="loader" alt="Carregando..." /></center>';

            html += '<div class="postit-modal-content-user dn"></div>';
            html += '<div class="postit-modal-content-name dn"></div>';
            html += '<div class="postit-modal-content-desc"></div>';
            html += '<div class="postit-modal-content-date"></div>';

            //html += '<div class="postit-modal-content-denounce"><a data-rule="denounce" href="javacript:void();">Denunciar</a></div>';

            html += '</div>';

            var $html = $(html);

            modal.show({
                'width': '674px',
                'caption': 'Descrição do item',
                'content': $html,
                'cancelButtonClick': function () {

                    modal.hide();

                }
            });

            var dataPost = { 'idPostitItem': $item.data().args };

            $.ajax({
                type: 'POST',
                url: '/mural/getpostititem.ashx',
                dataType: 'json',
                data: dataPost,
                cache: false,
                success: function (data) {

                    var $loader = $html.find('img[data-rule=postit-modal-content-loader]');

                    if (data.status == g.JSONSTATUS_SUCCESS) {

                        if (data.response != null) {

                            var postItem = data.response;

                            if (postItem.idUsuario != _idUsuario) {

                                $html.find('.postit-modal-content-user').append('<img src="http://servicos.uniceub.br/api/coorporativo/foto/?idUsuario=' + postItem.idUsuario + '&idOperacao=1" class="foto large" alt="' + g.validateName(postItem.nmPessoa) + '" />');
                                $html.find('.postit-modal-content-user').show();

                                $html.find('.postit-modal-content-name').html(g.validateName(postItem.nmPessoa));
                                $html.find('.postit-modal-content-name').show();

                                $html.find('.postit-modal-content-desc').css('margin-left', '60px');
                                $html.find('.postit-modal-content-date').css('margin-left', '60px');
                            }

                            var deItem = stripHtml({ html: postItem.deItem, videoOptions: { autoplay: true, dataHide: false, videoWidth: 640, videoHeight: 360 } })

                            if (postItem.idArquivo > 0) {


                                var isImage = (extExpr.test(postItem.deExtensaoPadrao.replace('.', '')));

                                var width = postItem.nuArquivoWidth;
                                var height = postItem.nuArquivoHeight;
                                var style = '';

                                if (width > 600) {

                                    origwidth = parseFloat(width);
                                    origheight = parseFloat(height);

                                    var ratio = origwidth / origheight;

                                    newwidth = origwidth;
                                    newheight = 600 / ratio;

                                    roundto = parseInt(10);

                                    if (roundto > 0) {
                                        newheight = newheight.toFixed(roundto);
                                    } else {
                                        newheight = Math.round(newheight);
                                    }

                                    width = 600;
                                    height = newheight;
                                }

                                if (isImage) {
                                    deItem += '<center><img width="' + width + 'px" height="' + height + 'px" src="' + postItem.edUrlDownload + '" alt="' + postItem.nmArquivo + '" /></center>'
                                    style = ' style="left:0px;"';
                                }

                                deItem += '<div class="file">';

                                if (!isImage) {
                                    deItem += '<img src="' + _imagesPath + 'icon' + postItem.deExtensaoPadrao + '.gif" alt="' + postItem.deTipoArquivo + '" />';
                                }

                                deItem += '<a href="' + postItem.edUrlDownload + '" target="_blank"' + style + '>Baixar arquivo</a>';
                                deItem += '</div>';
                            }

                            deItem += '<br />';
                            deItem += '<div style="font-size:90%;color:#ccc;" class="postititem-tags">Tags: ';

                            deItem += postItem.deTag;
                            if (postItem.deTag == undefined || postItem.deTag == null || postItem.deTag.length == 0)
                                deItem += 'Não há tag para este postit.';

                            deItem += '</div>';

                            var $content = $('<div>' + deItem.replace('\n', '<br /><br />') + '</div>');

                            var $youtube = $content.find('iframe[data-rule=youtube]');

                            if ($youtube.length > 0) {

                                $youtube.attr('width', '640px');
                                $youtube.attr('height', '460px');

                            }

                            $html.find('.postit-modal-content-desc').html($content);
                            $html.find('.postit-modal-content-date').html('<label>Há ' + postItem.dtCriacao + '</label> <label class="bullet">·</label> ');
                        }
                    }
                    else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                        g.showSessionExpired();
                    }
                    else {
                        g.showError();
                    }

                    $loader.hide();
                }
            });

        });

        var _viewItemClickHandler;

        $item.find('.delete-item').bind('mouseover', function () {

            $(this).addClass('delete-item-on');
            $(this).removeClass('delete-item');

        }).bind('mouseout', function () {

            $(this).addClass('delete-item');
            $(this).removeClass('delete-item-on');

        }).bind('click', function (e) {

            e.preventDefault();

            var $parent = $(this).parent();

            $parent.unbind('click');

            var deItem = $item.find('.postit-itemlist-desc').html();

            var content = '<div>Tem certeza que deseja excluir o item abaixo?<br /><br />' + deItem + '</div>';

            modal.show({
                'width': '500px',
                'caption': 'Confirmação de Exclusão',
                'content': content,
                'confirmButtonText': 'Confirmo',
                'confirmButtonClick': function () {

                    var dataPost = { 'idPostitItem': $item.data().args };

                    disableConfirmButtonModal();

                    $.ajax({

                        type: 'POST',
                        url: '/mural/deletepostititem.ashx',
                        dataType: 'json',
                        data: dataPost,
                        cache: false,
                        success: function (data) {

                            if (data.status == g.JSONSTATUS_SUCCESS) {

                                $item.remove();
                                tags.reLoad();
                                enableConfirmButtonModal();
                                modal.hide();

                            }
                            else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                                g.showSessionExpired();
                            }
                            else {
                                g.showError();
                            }
                        },
                        error: function () {
                            g.showError();
                        }

                    });

                },
                'cancelButtonClick': function () {

                    enableConfirmButtonModal();
                    modal.hide();
                    $parent.bind('click', _viewItemClickHandler);
                }
            });

        });
    };

    function disableConfirmButtonModal() {

        modal.getConfirmButton().attr('disabled', 'disabled');

    };

    function enableConfirmButtonModal() {

        modal.getConfirmButton().removeAttr('disabled');

    }

    function setToPostit(index, postit) {

        var $postit = $('.postit-item[index=' + index + ']');

        $postit.attr('data-args', postit.idPostit);

        addPostit($postit, postit);

        loadItems($postit);

    };

    function bindPostitEvents($postit, postit) {

        var $imgAddItem = $postit.find('.postit-item-action img[data-rule=additem]');
        bindChangeActionImage($imgAddItem);
        //Adicionar um novo item
        $imgAddItem.bind('click', function () {

            var html = '<div class="postit-modal-content">';
            html += '<label>Você pode compartilhar um texto, link de site, um vídeo do <a target="_blank" href="http://www.youtube.com.br">Youtube</a>, uma imagem ou arquivos nos formatos: DOC, PDF, XLS, PPT, MDB, VSD, ZIP, ACCDB, XLSX, DOCX, TXT, JPG, JPEG, GIF, PNG, BMP, HTM, HTML, DWG e PPTX.</label><br />';
            html += '<div class="postit-modal-content-action">';
            html += '<img data-rule="text" src="' + _imagesPath + 'postit-modal-text-on.png" class="postit-modal-action-text modal-action-img-selected cp" alt="Texto" />';
            html += '<img data-rule="file" src="' + _imagesPath + 'postit-modal-file.png" class="postit-modal-action-file cp" alt="Arquivo" />';
            html += '</div>';
            html += '<div class="postit-modal-content-text">';
            //html += '<span class="total-characters">0 caracteres</span>';
            html += '<textarea id="txtdeItem" rows="6"></textarea>';
            html += '<span id="vamdeItem" class="validate-field dn">O que você gostaria de descrever para o item?</span>';

            html += '<br />';
            html += '<br />';
            html += 'Tags: <span class="total-characters">0 caracteres</span>';
            html += '<br />';
            html += '<textarea id="txtdeTag" rows="2" maxlength="300"></textarea>';
            html += '<span id="vamdeTag" class="validate-field dn">Você ultrapassou o limite de 300 caracteres.</span>';

            html += '<span id="vamedurlarquivo" class="validate-field dn">Suporte somente para os arquivos com a extensão: doc, pdf, xls, ppt, mdb, vsd, zip, accdb, xlsx, docx, txt, jpg, jpeg, gif, png, bmp, htm, html, dwg e pptx.</span>';
            html += '</div>';
            html += '</div>';

            var $html = $(html);

            $html.find('#txtdeItem').bind('keyup', function () {

                $(this).prev().prev().html($(this).val().length.toString() + ' caracteres');


            });

            $html.find('#txtdeTag').bind('keyup', function () {

                $(this).prev().prev().html($(this).val().length.toString() + ' caracteres');

            });

            modal.show({
                width: '600px',
                caption: 'Incluir Item',
                content: $html,
                confirmButtonText: 'Incluir',
                disableMaskClick:true,
                loaded: function () {

                    $html.find('#txtdeItem').select();

                    bindModalContentPostitItemEvents($html, $postit);

                },
                'confirmButtonClick': function () {

                    $('#vamdeItem').hide();

                    var $selected = $html.find('img.modal-action-img-selected');

                    var deItem = $html.find('#txtdeItem').val();
                    var deTag = $html.find('#txtdeTag').val();
                    var dataPost;

                    if ($selected.data().rule == 'text') {

                        if ($.trim(deItem).length == 0) {

                            $('#vamdeItem').show();
                            $html.find('#txtdeItem').focus();
                            return;
                        }
                        else if ($.trim(deTag).length > 300) {

                            $('#vamdeTag').show();
                            $html.find('#txtdeTag').select();
                            return;
                        }

                        disableConfirmButtonModal();

                        dataPost = { idPostit: $postit.data().args, deItem: deItem, 'deTag': deTag };

                        $.ajax({
                            type: 'POST',
                            url: '/mural/addpostititem.ashx',
                            dataType: 'json',
                            data: dataPost,
                            cache: false,
                            success: function (data) {

                                if (data.status == g.JSONSTATUS_SUCCESS) {

                                    addItemToList($postit, data.response, true);
                                    enableConfirmButtonModal();
                                    modal.hide();

                                } else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                                    g.showSessionExpired();
                                }
                                else {
                                    g.showError();
                                }
                            }
                        });

                    }
                    else {


                    }

                },
                'cancelButtonClick': function () {

                    if (_ajaxUpload != undefined)
                        _ajaxUpload.destroy();

                    modal.hide();

                }
            });

        });
        bindTooltip($imgAddItem, 'Inclua textos, links, vídeos do <a class="link" target="_blank" href="http://www.youtube.com">Youtube</a><br /> e arquivos');

        if (_idUsuario != postit.idUsuario & postit.icRestrito) {
            var $imgLock = $postit.find('.postit-item-action img[data-rule=lock]');
            bindChangeActionImage($imgLock);
        }

        if (_idUsuario == postit.idUsuario) {

            var $imgColor = $postit.find('.postit-item-action img[data-rule=color]');
            var $imgEdit = $postit.find('.postit-item-action img[data-rule=edit]');
            var $imgExpires = $postit.find('.postit-item-action img[data-rule=expires]');
            var $imgShare = $postit.find('.postit-item-action img[data-rule=share]');
            var $imgDelete = $postit.find('.postit-item-action img[data-rule=delete]');

            bindChangeActionImage($imgColor);
            bindChangeActionImage($imgEdit);
            bindChangeActionImage($imgExpires);
            bindChangeActionImage($imgShare);
            bindChangeActionImage($imgDelete);

            //Altera os dados do post-it
            $imgEdit.bind('click', function () {

                var idPostit = $postit.data().args;
                var deTitulo = $postit.attr('text');
                var icPublico = $postit.attr('public');
                var icRestrito = $postit.attr('restrict');

                var $content = $(getContentFormPostit());

                var $txtdeTitulo = $content.find('#txtdeTitulo');
                $txtdeTitulo.val(deTitulo);

                $content.find('#ckbicPublico')[0].checked = eval(icPublico);
                $content.find('#ckbicRestrito')[0].checked = eval(icRestrito);

                modal.show({

                    width: '500px',
                    caption: 'Edição do Item do Mural',
                    content: $content,
                    loaded: function () {

                        $txtdeTitulo.select();

                    },
                    confirmButtonText: 'Confirmo',
                    confirmButtonClick: function () {

                        disableConfirmButtonModal();

                        deTitulo = $content.find('#txtdeTitulo').val();
                        icPublico = $content.find('#ckbicPublico')[0].checked;
                        icRestrito = $content.find('#ckbicRestrito')[0].checked;

                        $('#vamdeTitulo').hide();

                        if (g.isEmpty(deTitulo)) {
                            $('#vamdeTitulo', $content).show();
                            enableConfirmButtonModal();
                            return;
                        }

                        var dataPost = { 'idPostit': idPostit, 'deTitulo': deTitulo, 'icPublico': icPublico, 'icRestrito': icRestrito };

                        $.ajax({
                            type: 'POST',
                            url: '/mural/updatepostit.ashx',
                            data: dataPost,
                            dataType: 'json',
                            success: function (data) {

                                enableConfirmButtonModal();

                                if (data.status == g.JSONSTATUS_SUCCESS) {

                                    $postit.attr('text', deTitulo);
                                    $postit.find('.postit-item-title').html(validateTituloLength(deTitulo));

                                    $postit.attr('public', icPublico);
                                    $postit.attr('restrict', icRestrito);

                                    modal.hide();

                                }
                                else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                                    g.showSessionExpired();
                                }
                                else {
                                    g.showError();
                                }

                            },
                            error: function () {
                                g.showError();

                            }
                        });

                    },
                    cancelButtonClick: function () {

                        enableConfirmButtonModal();
                        modal.hide();

                    }
                });

            });

            //Exclui o post-it
            $imgExpires.bind('click', function () {

                var $self = $(this);
                var dtExpiracao = $(this).data().args;
                var idPostit = $(this).parent().parent().data().args;
                var html = '<div class="postit-modal-content">A data de expiração é usada para compartilhar os itens do mural com os colegas/alunos. Uma vez que a data expire o item do mural não será mais mostrado no item do mural do colega/aluno. Utilize o calendário abaixo para informar a data de expiração do item do mural.<br /><br /><center><div id="dtExpiracao"></div></center></div>';

                var $content = $(html);

                modal.show({

                    'width': '500px',
                    'caption': 'Data de Expiração do Item do Mural',
                    'content': $content,
                    'confirmButtonText': 'Confirmo',
                    'confirmButtonClick': function () {

                        disableConfirmButtonModal();

                        var dataPost = { 'idPostit': idPostit, 'dtExpiracao': $('#dtExpiracao').datepicker({ dateFormat: 'dd-mm-yy' }).val() };

                        $.ajax({
                            type: 'POST',
                            url: '/mural/updatepostitexpires.ashx',
                            data: dataPost,
                            dataType: 'json',
                            success: function (data) {

                                enableConfirmButtonModal();
                                if (data.status == g.JSONSTATUS_SUCCESS) {

                                    $self.data('args', data.response.dtExpiracao);
                                    modal.hide();

                                }
                                else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                                    g.showSessionExpired();
                                }
                                else {
                                    g.showError();
                                }

                            }
                        });

                    },
                    'loaded': function () {

                        $content.find('#dtExpiracao').datepicker({
                            inline: true
                        });

                        $content.find('#dtExpiracao').datepicker("setDate", dtExpiracao);

                    }
                    ,
                    'cancelButtonClick': function () {

                        enableConfirmButtonModal();
                        modal.hide();

                    }
                });

            });

            //Compartilhar com colegas de sala de aula e com professores
            $imgShare.bind('click', function () {

                var $share = $(this);
                var idTurma = $postit.data().args;
                //var deTitulo = $postit.find('.postit-item-title').html();
                var html = 'Clique nos professores ou alunos com quem deseja compartilhar o item do mural';
                var sharedUsers = $share.data().args;

                modalTurma.init({
                    idUsuario: _idUsuario,
                    caption: html,
                    enableScroll: true,
                    urlTurmas: '/mural/getturmalist.ashx',
                    urlAlunos: '/mural/getalunolistbyturma.ashx',
                    confirmButtonText: 'Compartilhar',
                    enable: false,
                    checkUsers: sharedUsers,
                    show: true,
                    confirmButtonClick: function () {

                        var dataPost = { 'idTurma': idTurma, 'arUsuario': modalTurma.getSelectedUsersString() };

                        var $button = modal.getConfirmButton();

                        $button.attr('disabled', 'disabled');
                        $button.val('Compartilhando...');

                        $.ajax({
                            type: 'POST',
                            url: '/mural/sharepostit.ashx',
                            data: dataPost,
                            cache: false,
                            dataType: 'json',
                            success: function (data) {

                                if (data.status == g.JSONSTATUS_SUCCESS) {

                                    $share.data('args', data.response.arUsuario);

                                    if (arrayIsValid(data.response.arUsuario)) {
                                        $imgExpires.removeClass('dn');
                                    }
                                    else {
                                        $imgExpires.addClass('dn');
                                    }

                                    $button.removeAttr('disabled');
                                    $button.val('Compartilhar');
                                    modal.hide();

                                }
                                else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {

                                    g.showSessionExpired();

                                }
                                else {

                                    g.showError();

                                }
                            }
                        });



                    }
                });
            });

            //Exclui o post-it
            $imgDelete.bind('click', function () {

                var postit = $(this).parent().parent();
                var idPostit = postit.data().args;
                var content = postit.parent();

                modal.show({

                    width: '500px',
                    caption: 'Confirmação de Exclusão',
                    content: 'Tem certeza que deseja excluir o item do mural: <strong>' + $postit.find('.postit-item-title').html() + '</strong>?',
                    confirmButtonText: 'Confirmo',
                    confirmButtonClick: function () {

                        disableConfirmButtonModal();

                        var dataPost = { 'idPostit': idPostit };

                        $.ajax({
                            type: 'POST',
                            url: '/mural/deletepostit.ashx',
                            data: dataPost,
                            dataType: 'json',
                            success: function (data) {

                                enableConfirmButtonModal();

                                if (eval(data) != null) {

                                    content.isotope('remove', postit);
                                    $imgDelete.parent().parent().remove();
                                    tags.reLoad();
                                    modal.hide();

                                }
                                else {
                                    showModalError();
                                }
                            }
                        });

                    },
                });

            });

            //Carregando os tooltips
            bindTooltipColor($imgColor);
            bindTooltip($imgEdit, 'Altere as configurações do Item do Mural');
            bindTooltip($imgExpires, 'Altere a data de expiração do Item do Mural,<br />uma vez que esteja compartilhado');
            bindTooltipShare($imgShare);
            bindTooltip($imgDelete, 'Exclui o Item do Mural');

        }
        else {

            var $imgDeleteShare = $postit.find('.postit-item-action img[data-rule=delete-share]');
            bindChangeActionImage($imgDeleteShare);
            //Exclui o post-it compartilhado
            $imgDeleteShare.bind('click', function () {

                var postit = $(this).parent().parent();
                var idPostit = postit.data().args;
                var content = postit.parent();

                var deTitulo = $postit.attr('text');

                modal.show({

                    'width': '500px',
                    'caption': 'Confirmação de Exclusão',
                    'content': 'Tem certeza que deseja excluir o item do mural compartilhado: <b>' + deTitulo + '</b>?',
                    'confirmButtonText': 'Confirmo',
                    'confirmButtonClick': function () {

                        disableConfirmButtonModal();

                        var dataPost = { 'idPostit': idPostit };

                        $.ajax({
                            type: 'POST',
                            url: '/mural/deletesharepostit.ashx',
                            data: dataPost,
                            dataType: 'json',
                            success: function (data) {

                                enableConfirmButtonModal();

                                if (eval(data) != null) {

                                    content.isotope('remove', postit);
                                    $imgDeleteShare.parent().parent().remove();
                                    tags.reLoad();
                                    modal.hide();

                                }
                                else {
                                    showModalError();
                                }
                            }
                        });

                    },
                    'cancelButtonClick': function () {

                        modal.hide();

                    }
                });

            });
        }
        //var $aDenounce = $postit.find('.postit-item-action a[data-rule=denounce]');
        //$('div[data-rule="item-list"]').scrollbar();
    };

    function bindChangeActionImage($img) {

        var _src = $img.attr('src');

        $img.bind('mouseover', function() {

            this.src = _src.replace('.png', '-on.png');

        }).bind('mouseout', function() {

            this.src = _src.replace('-on.png', '.png');

        });

    };

    function arrayIsValid(arUsuario) {

        if (arUsuario == undefined || $.trim(arUsuario).length == 0)
            return false;
        arUsuario = arUsuario.split(',');

        return ((arUsuario.length == 1 && arUsuario[0] != '') || arUsuario.length > 1);
    };

    function bindTooltip($img, text) {

        $img.poshytip({
            className: 'tip-info',
            showTimeout: 1,
            alignTo: 'target',
            alignX: 'center',
            offsetY: 5,
            allowTipHover: true,
            fade: false,
            slide: false,
            content: function () {
                return text;
            }
        });

    };

    function bindTooltipColor($img) {

        var $postit = $img.parent().parent();
        var idPostit = $postit.data().args;

        $img.poshytip({
            className: 'tip-info',
            showTimeout: 1,
            alignTo: 'target',
            alignX: 'center',
            offsetY: 5,
            allowTipHover: true,
            fade: false,
            slide: false,
            content: function () {
                
                var html = '<div class="postit-color">';

                html += '<div class="postit-color1"></div>';
                html += '<div class="postit-color2"></div>';
                html += '<div class="postit-color3"></div>';
                html += '<div class="postit-color4"></div>';
                html += '<div class="postit-color5"></div>';

                html += '</div>';

                var $html = $(html);

                $html.find('div').on('click', function () {

                    var $this = $(this);
                    var deCor = $this.prop('class');
                    var css;

                    $.ajax({
                        type: 'POST',
                        url: '/mural/changecolor.ashx',
                        dataType: 'json',
                        data: { idPostit: idPostit, deCor: deCor },
                        cache: false,
                        success: function (data) {

                            if (data.status == g.JSONSTATUS_SUCCESS) {

                                var $postitTitle = $postit.find('.postit-item-title');

                                for (var i = 1; i <= 5; i++) {
                                    css = 'postit-color' + i;
                                    if ($postitTitle.hasClass(css)) {
                                        $postitTitle.removeClass(css);
                                        $postit.removeClass('filter-' + css);
                                    }
                                }

                                $postitTitle.addClass(deCor);
                                $postit.addClass('filter-' + deCor);

                            } else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                                g.showSessionExpired();
                            }
                            else {
                                g.showError();
                            }

                        }
                    });

                });

                return $html;
            }
        });

    };

    function bindTooltipShare($img) {

        $img.poshytip({
            className: 'tip-info',
            showTimeout: 1,
            alignTo: 'target',
            alignX: 'center',
            offsetY: 5,
            allowTipHover: true,
            fade: false,
            slide: false,
            content: function () {

                var $img = $(this);
                var arUsuario = $img.data().args.toString();
                var html = '';

                if (arrayIsValid(arUsuario)) {

                    arUsuario = arUsuario.split(',');

                    var style = 'width:auto';

                    if (arUsuario.length >= 11)
                        style = 'width:360px';

                    html += '<div class="tooltip-user-list" style="' + style + '">';
                    html += 'Compartilhado para ' + arUsuario.length + ' usuários<br />';

                    $.each(arUsuario, function(index, item) {

                        html += '<img src="http://servicos.uniceub.br/api/coorporativo/foto/?idUsuario=' + $.trim(item) + '&idOperacao=1" class="foto medium" />';

                    });

                    html += '</div>';
                }
                else
                    html = 'Compartilhar Item do Mural';

                return html;
            }
        });

    };

    function bindModalContentPostitItemEvents($html, $postit) {

        var $button = modal.getConfirmButton();

        $html.find('img.postit-modal-action-text').bind('click', function () {

            //Desabilita o ajaxUpload
            if (_ajaxUpload != undefined)
                _ajaxUpload.destroy();

            $(this).next().attr('src', _imagesPath + 'postit-modal-file.png');
            $(this).attr('src', _imagesPath + 'postit-modal-text-on.png');

            $(this).next().removeClass('modal-action-img-selected');
            $(this).addClass('modal-action-img-selected');

            $button.html('Incluir');
        });

        $html.find('img.postit-modal-action-file').bind('click', function () {

            $('#vamdeItem').hide();

            $(this).prev().attr('src', _imagesPath + 'postit-modal-text.png');
            $(this).attr('src', _imagesPath + 'postit-modal-file-on.png');

            $(this).prev().removeClass('modal-action-img-selected');
            $(this).addClass('modal-action-img-selected');

            $button.html('Selecionar Arquivo');

            var deItem = $html.find('#txtdeItem').val();
            var deTag = $html.find('#txtdeTag').val();
            var idPostit = $postit.data().args;

            var dataPost = { 'idPostit': idPostit, 'deItem': deItem, 'deTag': deTag };

            _ajaxUpload = new AjaxUpload($button, {
                action: '/mural/uploadfile.ashx',
                name: 'uploadfile',
                data: dataPost,
                responseType: 'json',
                onSubmit: function (file, ext) {

                    $('#vamedurlarquivo').hide();

                    if (!(ext && extExpr.test(ext))) {
                        $('#vamedurlarquivo').show();
                        return false;
                    }

                    dataPost.deItem = $html.find('#txtdeItem').val();
                    dataPost.deTag = $html.find('#txtdeTag').val();

                    disableConfirmButtonModal();

                },
                onComplete: function (file, data) {

                    try {
                        data = g.parseJSON(data);
                    }
                    catch (e) {

                        if ($.browser.msie == true && parseInt($.browser.version) <= 7) {
                            g.showError('Este erro ocorreu devido a versão do Internet Explorer 7.0, ou inferior. Favor verificar a versão do seu navegador ou clique <a target="_blank" href="http://windows.microsoft.com/pt-BR/internet-explorer/downloads/ie">aqui</a> para atualizá-lo para uma versão mais recente.');
                            return;
                        }
                        else if ($.browser.mozilla == true && parseInt($.browser.version) <= 9) {
                            g.showError('Este erro ocorreu devido a versão do Firefox Mozilla 9.0, ou inferior.0. Favor verificar a versão do seu navegador ou clique <a target="_blank" href="http://br.mozdev.org/">aqui</a> para atualizá-lo para uma versão mais recente.');
                            return;
                        }
                        else {
                            g.showError();
                            return;
                        }

                    }

                    _ajaxUpload.destroy();
                    enableConfirmButtonModal();

                    if (data.status == g.JSONSTATUS_SUCCESS) {

                        addItemToList($postit, data.response, true);

                        loadTags();

                        modal.hide();
                    }
                    else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                        g.showSessionExpired();
                    }
                    else {
                        g.showError();
                    }
                }
            });

            $('#txtdeItem', $html).focus();

        });
    };

    function bindVirtualEvents($postit) {

        $postit.bind('mouseover', function () {

            $(this).addClass('postit-item-virtual-hover');
            $(this).removeClass('postit-item-virtual');

        }).bind('mouseout', function () {

            $(this).addClass('postit-item-virtual');
            $(this).removeClass('postit-item-virtual-hover');

        }).bind('click', function () {

            var $item = $(this);

            var $html = $(getContentFormPostit());

            var $txtdeTitulo = $html.find('#txtdeTitulo');


            modal.show({
                width: '500px',
                caption: 'Incluir Item do Mural',
                content: $html,
                confirmButtonText: 'Incluir',
                loaded: function () {

                    $txtdeTitulo.focus();

                },
                confirmButtonClick: function (e) {

                    $('#vamdeTitulo').hide();

                    e.preventDefault();

                    var index = $item.attr('index');
                    var deTitulo = $.trim($txtdeTitulo.val());
                    var icPublico = $html.find('#ckbicPublico')[0].checked;
                    var icRestrito = $html.find('#ckbicRestrito')[0].checked;
                    var nuOrdem = parseInt($postit.attr('index')) + 1;

                    if (g.isEmpty(deTitulo)) {
                        $('#vamdeTitulo').show();
                        return;
                    }

                    var dataPost = { 'deTitulo': deTitulo, 'nuOrdem': nuOrdem, 'icPublico': icPublico, 'icRestrito': icRestrito };

                    disableConfirmButtonModal();

                    $.ajax({
                        type: 'POST',
                        url: '/mural/addpostit.ashx',
                        dataType: 'json',
                        data: dataPost,
                        cache: false,
                        success: function (data) {

                            enableConfirmButtonModal();

                            if (data.status == g.JSONSTATUS_SUCCESS) {

                                addPostit($item, data.response);

                                add(addItem(index));

                                clearControlsPostit($html);
                                modal.hide();


                            } else if (data.status == g.JSONSTATUS_SESSIONEXPIRED) {
                                g.showSessionExpired();
                            }
                            else {
                                g.showError();
                            }

                        }
                    });

                },
                cancelButtonClick: function () {

                    modal.hide();

                }
            });

        });

    };

    function showModalError() {

        modal.show({
            caption: 'Erro',
            content: 'Ocorreu um erro. Estamos trabalhando para solucionar o mais rápido possível',
            'cancelButtonText': 'Ok',
            'cancelButtonClick': function () {
                modal.hide();
            }
        });

    };

    function clearControlsPostit($html) {

        $html.find('#txtdeTitulo').val('');

    };

    function addPostit($postit, postit) {

        //var icShareForUsers = arrayIsValid(postit.arUsuario);

        clearEventVirtualItem($postit);
        $postit.removeClass('postit-item-virtual-hover');
        $postit.removeClass('postit-item-virtual');
        //$postit.find('img[data-rule=shadow]').addClass('postit-shadow');

        $postit.data('args', postit.idPostit);
        $postit.attr('public', postit.icPublico);
        $postit.attr('restrict', postit.icRestrito);
        $postit.attr('text', postit.deTitulo);
        $postit.addClass('filter-' + (g.isEmpty(postit.deCor) ? 'postit-color1' : postit.deCor));

        var icShared = false;

        if (_idUsuario != postit.idUsuario) {

            //$postit.removeClass('postit-item');
            //$postit.removeClass('postit-item-share');

            var $user = $('<img class="postit-share-user foto large" src="http://servicos.uniceub.br/api/coorporativo/foto/?idUsuario=' + postit.idUsuario + '&idOperacao=1" alt="' + postit.nmPessoa + '" />');

            $postit.append($user);

            icShared = true;
        }

        var content = '<div class="' + (icShared ? 'postit-item-title-share' : 'postit-item-title') + ' ' + (!g.isEmpty(postit.deCor) ? postit.deCor : 'postit-color1') + '">';

        content += validateTituloLength(postit.deTitulo);

        if (icShared) {
            content += '<br />';
            content += '<span>' + g.validateName(postit.nmPessoa) + '</span>';
            content += '<br />';
            content += '<span class="expires">Expira em ' + postit.dtExpiracao + '</span>';
        }

        content += '</div>';
        content += '<div class="' + (icShared ? 'postit-item-list-share' : 'postit-item-list') + ' scrollbar simple" data-rule="item-list" data-args="' + postit.idPostit + '"></div>';
        content += '<div class="postit-item-action">';

        //content += '<a href="javascript:void();" data-rule="denounce"' + (icShared || icShareForUsers ? '' : 'class="dn"') + '>Denunciar</a>';

        if (_idUsuario != postit.idUsuario & postit.icRestrito) {
            content += '<img data-rule="locked" class="cp" src="' + _imagesPath + 'ico-postit-lock.png" alt="Postit restrito" />';
        }

        content += '<img data-rule="additem" class="cp" src="' + _imagesPath + 'ico-postit-additem.png" alt="Incluir item" />';

        if (_idUsuario == postit.idUsuario) {

            content += '<img data-rule="color" class="cp" src="' + _imagesPath + 'ico-postit-color.png" title="" alt="Cor" />';

            content += '<img data-rule="edit" class="cp" src="' + _imagesPath + 'ico-postit-edit.png" title="" alt="Editar" />';

            content += '<img data-rule="expires" class="cp' + (arrayIsValid(postit.arUsuario) ? '' : ' dn') + '" src="' + _imagesPath + 'ico-postit-expires.png" title="" data-args="' + postit.dtExpiracao + '" alt="Data de expiração" />';

            content += '<img data-rule="share" class="cp" src="' + _imagesPath + 'ico-postit-share.png" title="" alt="Compartilhar" data-args="' + postit.arUsuario + '" />';

            content += '<img data-rule="delete" class="cp" src="' + _imagesPath + 'ico-postit-delete.png" title="" alt="Excluir" />';
        }
        else {
            content += '<img data-rule="delete-share" class="cp" src="' + _imagesPath + 'ico-postit-delete.png" title="" alt="Excluir compartilhamento" />';
        }

        content += '</div>';

        var $content = $(content);

        $postit.append($content);

        var $shadow = $('<img data-rule="shadow" class="postit-shadow" src="' + _imagesPath + 'postit-shadow.png" alt="" />');
        $postit.append($shadow);

        bindPostitEvents($postit, postit);
    };

    function validateTituloLength(deTitulo) {

        return (deTitulo.length > 35 ? deTitulo.substr(0, 34) + '...' : deTitulo);

    };

    function clearEventVirtualItem($postit) {

        $postit.unbind('mouseover');
        $postit.unbind('mouseout');
        $postit.unbind('click');

    };

    function getContentFormPostit() {

        var html = '<div class="postit-modal-content">';

        html += 'Informe um título para o item do mural<br />';
        html += '<input id="txtdeTitulo" type="text" name="txtdeTitulo" maxlength="50" class="textbox">';
        html += '<span id="vamdeTitulo" class="validate-field dn">Qual o título do item do mural?</span>';
        html += '<br />';
        html += '<br />';
        html += '<input id="ckbicPublico" type="checkbox" name="ckbicPublico"><label for="ckbicPublico">Público</label>';
        html += '<br />';
        html += '<label class="postit-modal-content-tip" for="ckbicPublico">Ao marcar como público, o item do mural poderá ser visualizado por todos os colegas/alunos. Se o item do mural for compartilhado com algum colega/aluno, este poderá incluir itens no seu item do mural.</label>';
        html += '<br />';
        html += '<br />';
        html += '<input id="ckbicRestrito" type="checkbox" name="ckbicRestrito"><label for="ckbicRestrito">Restrito</label>';
        html += '<br />';
        html += '<label class="postit-modal-content-tip" for="ckbicRestrito">Ao marcar como restrito, os itens do item do mural poderão ser visualizados apenas pelo seu dono e/ou por colegas/aluno com os quais foram compartilhados. Porém colegas não poderão visualizar os itens de outros colegas.</label>';

        html += '</div>';

        return html;

    };

    function addItem(index) {

        var html = '';

        html += '<div class="postit-item postit-item-virtual" data-rule="item" index="' + index + '">';
        html += '<img data-rule="loader" src="' + _imagesPath + 'loading.gif" class="loader dn" alt="Carregando..." />';
        html += '</div>';

        var $postit = $(html);

        bindVirtualEvents($postit);

        return $postit;

    };

    function add($postit) {

        $('#postit-list').append($postit).isotope('insert', $postit);

    };

    function stripHtml(params) {

        var videoOptions = {};
        var html = undefined;

        if (params.html != undefined)
            html = params.html;

        if (params.videoOptions != undefined)
            videoOptions = params.videoOptions;

        //return html.replace(/(<([^>]+)>)/ig, "");
        html = Encoder.htmlDecode(html);
        html = Encoder.htmlEncode(html);
        html = g.findUrl({ text: html, videoOptions: videoOptions });
        html = g.decodeTag(html);

        return html;
    }

    return {
        init: init,
        load: load
    };
})();

var tags = (function () {

    var _$target;
    var _tags = [];
    var _filter = [];

    var load = function (params) {

        if (params.target == undefined)
            throw 'Not defined target';
        if (params.filter == undefined)
            throw 'Not defined filter';

        if (typeof params.target === 'string')
            _$target = $(params.target);
        else if (typeof params.target === 'object')
            _$target = params.target;
        if (typeof params.filter === 'function')
            _filter = params.filter;
        else if (typeof params.filter === 'object')
            _filter = params.filter;

        reLoad();
    };

    var reLoad = function () {
        var aux = '';
        var $taglist = $('#postit-list .postit-itemlist');
        //var length = $taglist.length;

        var aux = library.loopIncrement($taglist, '', function (elem) {
            return ' ' + $(elem).data().tags;
        })

        _tags = library.removeEquals(removeBlackTags(aux),
            compareTags);
        if (_$target == undefined)
            return;
        else if (_tags == undefined || _tags.length == 0) {
            renderEmpty();
            return;
        }
        render();
    }

    function render() {

        var html = library.loopIncrement(_tags, '', function (hd) { return '<a href="#" class="filter" title="' + hd + '">#' + hd + '</a>' });
        var $html = $(html);
        _$target.empty();
        bindEvents(_$target.append($html));
        $('#btnShowTags').show();

    };

    function renderEmpty() {
        _$target.empty();
        _$target.append('<a href="#" class="filter-empty" title="Não há tags cadastradas">Não há tags cadastradas</a>');
        $('#btnShowTags').hide();
    }

    function removeBlackTags(tagList) {
        return library.filter(tagList.split(' '), [],
            function (hd) {
                return hd != '' && hd.toString() != 'undefined';
            });
    }

    function compareTags(tag1, tag2) {
        return tag1.toLowerCase() == tag2.toLowerCase();
    }

    function bindEvents($html) {

        $html.find('a.filter').on('click', function () {

            _filter(this);

        });

    };

    return {
        load: load,
        tags: _tags,
        reLoad: reLoad
    };

})();