var modalTurma = (function () {

    var _parent;
    var _caption = 'Minhas Turmas';
    var _urlTurmas = '../../jsonturmas.ashx';
    var _urlAlunos = '../../jsonalunosbyturma.ashx';
    var _colspan = 3;
    var _confirmButtonText = 'Confirmar';
    var _cancelButtonText = 'Cancelar';
    var _confirmButtonClick;
    var _titleSearch = 'Selecione'; //referente a propriedade ALT do IMG de mostrar o modal
    var _checkUsers = '';
    var _usuarios = [];
    var _checkAllUsers = false;
    var _enable = true;
    var _selectedUser;
    var _unSelectedUser;
    var _hideCheckbox = false;
    var _typeID = 'idturma'; /*Usado para determinar se o agrupamento é montado por turmas ou Grupos*/
    var _markCheckbox = false; /*Usado para marcar o checkbox do grupo quando todos os alunos de um grupo são selecionados*/

    var init = function (options) {

        _checkAllUsers = false;

        if (options.caption != undefined) {
            _caption = options.caption;
        }
        if (options.enable != undefined) {
            _enable = options.enable;
        }
        if (options.urlTurmas != undefined) {
            _urlTurmas = options.urlTurmas;
        }

        if (options.urlAlunos != undefined) {
            _urlAlunos = options.urlAlunos;
        }

        if (options.titleSearch != undefined) {
            _titleSearch = options.titleSearch;
        }

        if (options.confirmButtonText != undefined) {
            _confirmButtonText = options.confirmButtonText;
        }

        if (options.confirmButtonClick != undefined) {
            _confirmButtonClick = options.confirmButtonClick;
        }

        if (options.cancelButtonText != undefined) {
            _cancelButtonText = options.cancelButtonText;
        }

        if (options.checkUsers != undefined) {
            _checkUsers = options.checkUsers;
        }

        if (options.hideCheckbox != undefined) {
            _hideCheckbox = options.hideCheckbox;
        }

        if (options.parent != undefined) {
            _parent = options.parent;
            initControls();
        }

        if (options.show == undefined) {
            options.show = false;
        }

        if (options.typeID != undefined) {
            _typeID = options.typeID.toLowerCase();
        }

        _selectedUser = options.selectedUser;
        _unSelectedUser = options.unSelectedUser;

        if (_enable)
            initControls();

        bindEvents();

        if (options.show) {
            show();
        }

        if (options.hideButtonConfirm != undefined) {
            hideButtonConfirm(options.hideButtonConfirm);
        }
    };

    function initControls() {
        //limpa todo os filhos do objeto a ser inserido a imagem
        _parent.empty();
        if (_enable) {
            var $imgSearch = getImageSearchContent();
            _parent.append($imgSearch);
        }
    };

    function confirmButtonClickHandler() {
        var usuarios = getSelectedUsers();
        if (_parent != undefined) {
            for (var i = 0; i < usuarios.length; i++) {

                if (!existsInSearch(usuarios[i].idUsuario))
                    addUsuario(usuarios[i]);

            }

            removeUsuarios(library.filter(
                _usuarios, [],
                function (u) {
                    var adicionar = true;
                    for (var i = 0; i < usuarios.length; i++) {
                        if (u.idUsuario == usuarios[i].idUsuario) {
                            adicionar = false;
                            break;
                        }
                    }
                    return adicionar;
                }));
        }
        _confirmButtonClick(usuarios);
    }

    function bindEvents() {

        $('#modalturma-search, #from-list-search, img[data-rule="share"]').bind('click', function () {

            show();

        });
    };

    function show() {

        var $content = getModalContent();

        modal.show({
            caption: _caption,
            content: $content,
            confirmButtonText: _confirmButtonText,
            cancelButtonText: _cancelButtonText,
            confirmButtonClick: confirmButtonClickHandler,
            cancelButtonClick: function () {

                modal.hide();

            },
            width: '800px',
            enableScroll: false
        });

    };

    var hide = function () {
        modal.hide();
    };

    function getSelectedUsers() {

        var idUsuario;
        var nmPessoa;
        var $users = $('#modalturma-gridlist tr[data-rule=panel] td div.from-user-selected');
        var selectedUsers = [];
        var exists = false;
        var index1 = 0;
        var $user;
        var idReferencia = 0;

        for (var j = 0; j < $users.length; j++) {

            exists = false;
            $user = $($users[j]);

            if ($($users[j]).parent().parent().prev().find('input')[0].checked) {
                idReferencia = $user.parent().parent().data().args;
            }
            else {
                idReferencia = 0;
            }

            idUsuario = $user.data().args;
            nmPessoa = $user.find('img').prop('alt');

            for (i = 0; i < selectedUsers.length; i++) {

                if (selectedUsers[i].idUsuario == idUsuario) {
                    exists = true;
                    break;
                }

            }

            if (!exists) {

                if (_typeID === 'idturma') {
                    selectedUsers[index1] = { 'idUsuario': idUsuario, 'nmPessoa': nmPessoa, 'idTurma': idReferencia };
                }
                else {
                    selectedUsers[index1] = { 'idUsuario': idUsuario, 'nmPessoa': nmPessoa, 'idGrupo': idReferencia };
                }
                index1++;
            }

        }

        return selectedUsers;

    };

    function getImageSearchContent() {
        return $('<img id="modalturma-search" src="/content/images/add-user.png" class="cp" data-rule="tooltip" style="border:0;" title="' + _titleSearch + '" alt="' + _titleSearch + '" />');
    };

    function getModalContent() {

        var html = '<table id="modalturma-gridlist" class="gridlist">';

        html += '<tr class="gridviewrow">';

        html += '<td colspan="3" class="gridviewcolumnrow">';

        html += '<center></center>';

        html += '</td>';

        html += '</tr>';

        html += '</table>';

        var $gridlist = $(html);

        loadTurmas($gridlist);

        return $gridlist;

    };

    function loadTurmas($gridlist) {

        var $loader = g.createLoader($gridlist.find('tr td[colspan=' + _colspan + '] center'));

        $.ajax({
            type: 'GET',
            url: _urlTurmas,
            dataType: 'json',
            data: { 'pageIndex': '1', 'pageSize': '200' },
            cache: false,
            success: function (data) {

                if (data.status == g.JSONSTATUS_SUCCESS) {

                    var length = data.response.length;

                    $.each(data.response, function (index, turma) {

                        addTurma($gridlist, turma);

                        if (index == length - 1)
                            $loader.parent().parent().parent().remove();
                    });

                    return;

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

    };

    function addTurma($gridlist, item) {


        var dataArgs = item.idTurma;

        if (_typeID === 'idgrupo') {
            dataArgs = item.idGrupo;
        }

        var html = '<tr data-args="' + dataArgs + '" class="gridviewrow" data-rule="turma">';

        if (!_hideCheckbox) {

            html += '<td class="gridviewcolumnrow tac" style="width:0px">';

            html += '<input type="checkbox" id="ckb' + dataArgs + '" data-rule="checkall" />';

            html += '</td>';

            html += '<td class="gridviewcolumnrow" style="width:100%">';
        }
        else {

            html += '<td class="gridviewcolumnrow" style="width:100%" colspan="2">';

        }

        if (_typeID === 'idturma') {
            html += '<label class="cp">' + item.nuPeriodoLetivo + 'º ' + item.deRegimeLetivo + '-' + item.aaPeriodoLetivo + ' / ' + item.coTurma + ' / ' + item.nmCompleto + ' / ' + item.nmTurno + '</label>';
        }
        else {
            html += '<label class="cp">' + item.nmGrupo + ' / ' + item.deGrupo + ' / ' + item.coTurma + ' / ' + item.nmTurno + '</label>';
        }

        html += '</td>';

        html += '<td class="gridviewcolumnrow tac" style="width:0px">';

        html += '<img src="/content/images/down.png" data-rule="set" class="cp" alt="Visualizar alunos da turma" />';

        html += '</td>';

        html += '</tr>';

        html += '<tr data-rule="panel" data-args="' + dataArgs + '" style="background:#f1f1f1" class="gridviewrow dn"><td colspan="' + _colspan + '" class="gridviewcolumnrow" style="width:100%"><center></center></td></tr>';

        var $item = $(html);

        bindTurmaEvent($item, item);

        $gridlist.append($item);

    }

    function bindTurmaEvent($item, item) {

        $('[type=checkbox][data-rule=checkall], label, img[data-rule=set]', $item).bind('click', function (e) {

            var checked = false;
            var nodeName = this.nodeName.toLowerCase();
            var isVisible = false;
            var isLoaded = false;

            if (nodeName === 'input') {
                checked = this.checked;
                _checkAllUsers = checked;

            }

            //Mostra/oculta os alunos da turma
            var $panel = $item.last();
            $panel.toggle();

            isLoaded = $panel.children(0).children().length > 1;
            isVisible = $panel.is(':visible');
            if (nodeName === 'input' & !isVisible) {
                $panel.show();
                isVisible = true;
            }
            var $label = $item.first().find('td label');

            $item.first().find('td img[data-rule=set]').attr('src', '/content/images/' + (isVisible ? 'up.png' : 'down.png'));

            //Verifica se já foi carregado os alunos
            if (!isLoaded) {

                var idReferencia = item.idTurma;

                if (_typeID === 'idgrupo') {
                    idReferencia = item.idGrupo;
                }
                loadAlunos($panel, idReferencia);

            }
            else {

                if (nodeName === 'input') {

                    var $users = $panel.last().find('div.from-user');
                    $users.removeClass('from-user-selected');
                    if (checked) {
                        $users.addClass('from-user-selected');
                    }

                }
            }

        });

    };

    function loadAlunos($panel, idReferencia, callback) {

        var $center = $panel.find('center');
        var $loader = g.createLoader($center);

        var dataPost = { 'idTurma': idReferencia };

        if (_typeID === 'idgrupo') {
            dataPost = { 'idGrupo': idReferencia };
        }

        $.ajax({
            type: 'POST',
            url: _urlAlunos,
            data: dataPost,
            dataType: 'json',
            cache: true,
            success: function (data) {

                if (data.status == g.JSONSTATUS_SUCCESS) {

                    if (data.response.length > 0) {

                        //var nuTotal = $('#from-list img').slice(2).length;
                        var length = data.response.length;

                        _markCheckbox = true;

                        $.each(data.response, function (index, aluno) {

                            addAlunoTurma($panel, aluno);

                            if (index == length - 1) {
                                $center.remove();

                                if (_markCheckbox)
                                    $panel.prev().find('input').attr('checked', 'checked');
                            }
                        });
                    }

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
    };

    function addAlunoTurma($panel, item) {

        var $item = getAlunoTurma(item);

        $item.on('click', function () {

            if ($(this).hasClass('from-user-selected')) {
                if (_selectedUser != undefined) {
                    _selectedUser(item);
                }
            }
            else {
                if (_unSelectedUser != undefined) {
                    _unSelectedUser(item);
                }
            }

        });

        $panel.children(0).append($item);

    };

    function getAlunoTurma(aluno) {

        var exists = false;

        if (_parent != undefined) {
            exists = existsInSearch(aluno.idUsuario);
        }
        else {
            exists = existsInCheckUsers(aluno.idUsuario);
        }

        if (!exists)
            _markCheckbox = false;

        var html = '<div class="from-user' + (exists || _checkAllUsers ? ' from-user-selected' : '') + '" data-selected="' + (exists ? 'true' : 'false') + '" data-args="' + aluno.idUsuario + '">';

        html += '<img src="http://www.espacoaluno.uniceub.br/includes/avatar.ashx?' + aluno.idUsuario + '" class="foto large" alt="' + aluno.nmPessoa + '" />';

        html += '</br>';

        html += '<label>';

        html += g.validateName(aluno.nmPessoa);

        html += '</label>';

        html += '</div>';

        var $item = $(html);

        $item.bind('click', function () {

            var selected = $(this).hasClass('from-user-selected');
            var self = $(this);
            var idTurma = self.parent().parent().data().args;

            if (!selected) {

                self.addClass('from-user-selected');
                var alunos = $('tr[data-args="' + idTurma + '"][data-rule="panel"]').find(".from-user")
                var nuTotalAlunos = alunos.filter(".from-user").length;
                var nuTotalAlunosChecked = alunos.filter(".from-user-selected").length;

                if (nuTotalAlunos == nuTotalAlunosChecked)
                    $("#ckb" + idTurma).attr('checked', 'checked');
            }
            else {

                self.removeClass('from-user-selected');
                $("#ckb" + idTurma).removeAttr("checked");

            }

        });

        return $item;
    };

    function getUsuario(usuario) {

        var html = '<img src="/content/avatar.ashx?' + usuario.idUsuario + '" class="medium" data-args="' + usuario.idUsuario + '" alt="' + usuario.nmPessoa + '" />';

        var $item = $(html);

        bindTooltip($item, _usuarios[0].idUsuario != usuario.idUsuario);

        return $item;
    };

    var addUsuario = function (usuario) {
        _usuarios[_usuarios.length] = usuario;
        _parent.append(getUsuario(usuario));

    };

    function addUsuarios(usuarios) {

        if (usuarios != undefined) {
            if (usuarios.length == 0)
                return;
            else {
                addUsuario(library.hd(usuarios));
                return addUsuarios(library.tl(usuarios));
            }
        }
    };

    function removeUsuarios(usuarios) {
        if (usuarios != undefined) {
            if (usuarios.length == 0)
                return;
            else {
                removeUsuario(library.hd(usuarios));
                return removeUsuarios(library.tl(usuarios));
            }
        }

    };

    var removeUsuario = function (usuario) {
        _usuarios = library.filter(_usuarios, [], function (u) { return usuario.idUsuario != u.idUsuario });
        $('img').remove('img[data-args="' + usuario.idUsuario + '"]');
    };

    function bindTooltip($img, bindRemover) {

        $img.poshytip({
            className: 'tip-info-user',
            showTimeout: 1,
            alignTo: 'target',
            alignX: 'center',
            offsetY: 5,
            allowTipHover: true,
            fade: false,
            slide: false,
            content: function () {

                var $img = $(this);
                var idUsuario = $img.data().args;
                var style = 'style="border:1px solid #eaeaea;"';

                var html = '<img src="' + this.src + '" width="50px" height="55px" complete="complete" style="' + style + '" class="foto" />';
                html += '<br />';

                html += '<label class="p4 mb4">' + g.validateName(this.alt) + '</label>';

                html += '<br />';
                if (bindRemover)
                    html += '<label class="button"><button type="button">Remover</button></label>';

                var $html = $(html);

                $html.find('button').bind('click', function () {

                    $img.remove();
                    $(this).parent().parent().hide();

                });

                return $html;
            }
        });

    };

    //Verifica se o usuário já está na lista de destinatários
    function existsInSearch(idUsuario) {

        //if (_checkUsers != undefined) {

        var $imgs = _parent.find('img').slice(1);

        var length = $imgs.length;

        for (var i = 0; i < length; i++) {
            if (idUsuario == $($imgs[i]).data().args)
                return true;
        }
        //}

        return false;
    };

    function existsInCheckUsers(idUsuario) {

        if (_checkUsers != undefined) {

            return _checkUsers.toString().indexOf(idUsuario.toString()) > -1;
        }

        return false;
    };

    var getSelectedUsersString = function (separator) {

        if (separator == undefined)
            separator = ',';

        var usuarios = getSelectedUsers();

        var selectedUsers = '';

        var length = usuarios.length;

        for (var i = 0; i < length; i++) {

            selectedUsers += usuarios[i].idUsuario + separator;
        }

        if ($.trim(selectedUsers).length > 0) {
            selectedUsers = selectedUsers.substr(0, selectedUsers.length - 1);
        }

        return selectedUsers;
    };

    var getJSONSelectedUsers = function () {

        var $imgs = $('#modalturma-search').parent().find('img').slice(1);
        var selectedUsers = [];
        var $img;

        var length = $imgs.length;

        for (var j = 0; j < length; j++) {

            $img = $($imgs[j]);
            selectedUsers[j] = { idUsuario: $img.data().args, nmPessoa: $img.prop('alt'), 'idTurma': 0 };

        }

        return selectedUsers;

    };

    var getIMGSelectedUsers = function () {

        return $('#modalturma-search').parent().find('img').slice(1);

    };

    var hideButtonConfirm = function (hide) {

        if (hide) {
            modal.getConfirmButton().hide();
        }

    };

    return {
        init: init,
        addUsuario: addUsuario,
        getJSONSelectedUsers: getJSONSelectedUsers,
        getIMGSelectedUsers: getIMGSelectedUsers,
        getSelectedUsersString: getSelectedUsersString,
        getSelectedUsers: getSelectedUsers,
        hide: hide,
        hideButtonConfirm: hideButtonConfirm
    };

})();