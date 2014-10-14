var Api = (function() {

    var put = function(params) {

        params.type = 'PUT';

        ajax(params);

    };

    var post = function (params) {

        params.type = 'POST';

        ajax(params);

    };

    //var delete = function (params) {

    //    params.type = 'DELETE';

    //    ajax(params);

    //};

    var get = function(params) {

        params.type = 'GET';

        ajax(params);

    };

    var status = {
        SUCCESS: 0,
        BUSINESSRULE: 1,
        ERROR: 2,
        NOTFOUND: 3
    };

    var ajax = function(params) {

        jQuery.support.cors = true;

        $.ajax({
            //crossDomain: false,
            url: window.config.apiurl + '/api/' + params.url,
            type: params.type,
            data: params.data,
            cache: (params.cache ? params.cache : false),
            datatype: 'json',
            headers: {
                'Authentication': window.config.authentication,
                'Timestamp': window.config.timestamp,
                'Cache-Control': 'max-age=3600'
            },
            success: function(data) {

                if (data.status === 0) { //SUCCESS 

                    params.success(data);

                } else if (data.status === 1) { //BUSINESSRULE 

                    modal.show({
                        caption: 'Atenção',
                        content:data.error.description
                    });

                }

            },
            error: function(a, b, c) {

                var response = $.parseJSON(a.responseText);

                if (params.error && typeof params.error == 'function') {

                    params.error({
                        statusCode: a.status,
                        message: response.message,
                        status: response.status
                    });
                    return;
                }

            },
            statusCode: {
                //401: function() {
                //    window.location = '/erros/requisicaoinvalida.htm';
                //}
            }
        });

    };

    return {
        status: status,
        get: get,
        post: post,
        put: put
    };

})();
