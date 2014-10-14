var bingSearch = (function() {
    
    var pageSize = 20;
    var offset = pageSize;
    var executing = false;

    var bindEvents = function() {

        $(window).scroll(function () {

            if ($(window).scrollTop() == $(document).height() - $(window).height()) {

                execute();

            }

        });

        $('#ctl00_ctl00_CPHContent_header_txtdePesquisa').click(function() {

            $('ul.results').empty('');
            offset = pageSize;

            setTitle();
            execute();

        });

        return this;
    };

    function setTitle() {

        var query = window.location.href.split('?')[1];

        $('#title').text('Resultado para : ' + query);
        $('#ctl00_ctl00_CPHContent_header_txtdePesquisa').val(query);
    };

    var execute = function() {

        if (!executing) {

            executing = true;

            var query = window.location.href.split('?')[1];

            setTitle();

            getLoader().show();

            $.ajax({
                type: 'GET',
                url: '/services/bingservice.ashx',
                data: { query: 'site:www.uniceub.br ' + query, offset: offset },
                dataType: 'json',
                cache: false,
                success: function(response) {

                    if (response.status == 'success') {

                        var html = '';

                        var len = response.data.length;

                        $.each(response.data, function(index, result) {

                            html += '<li><a href=\"' + result.Url + '\" target=\"_blank\">' + result.Title + '</a><br />' + result.Description + '<br /><span>' + result.Url + '</span></li>';

                            if (index == len - 1) {

                                $('ul.results').append(html);

                                if (len < pageSize) {

                                    $('ul.result-navigation').hide();

                                } else {

                                    $('ul.result-navigation').show();
                                    
                                }

                                offset += pageSize;

                                getLoader().hide();

                                executing = false;
                            }

                        });

                    }
                }
            });

        }
    };

    function getLoader() {

        return $('#loader');

    };

    return {
        execute: execute,
        bindEvents: bindEvents
    };

})();

bingSearch.bindEvents().execute();
