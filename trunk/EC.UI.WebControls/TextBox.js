/*
Extensões jQuery
Criado por Stiven Fabiano da Câmara
13/08/2010 as 11:30hs
*/
alert("RRRRRRRRRRRRRRRRRRRRRRRRR");
(function ($) {

    function changeStringToNumber(value) {

        var numbers = '0123456789';
        var char = '';
        var aux = '';

        for (i = 0; i < value.length; i++) {

            char = value.substr(i, 1);

            if (numbers.indexOf(char) > -1) {
                aux += char;
            }
        }

        return aux;
    }

    $.fn.maskPhone = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 10);

            sender.keypress(function (e) {

                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);

            });

            sender.keyup(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;

                if (length > 5) {
                    value = format(value, length);
                }

                $(this).val(value);

            });

            sender.blur(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;

                if (length == 8) {
                    sender.val(format(value, 8));
                }
                else if (length == 9) {
                    sender.val(format(value, 9));
                }
                else {
                    sender.val('');
                }
            });

            sender.change(function (e) {
                //TODO
            });

        });

        function format(value, length) {
            if (length <= 8) {
                return value.substr(0, 4) + '-' + value.substr(4, length - 4);
            } else {
                return value.substr(0, 5) + '-' + value.substr(5, length - 4);
            }
        };
    };

    $.fn.onlyNumbers = function (options) {

        var defaults = {
            length: 10,
            showalert: false
        };

        var options = $.extend(defaults, options);

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', options.length);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keydown(function (e) {
                if (options.showalert)
                    alert(e.which);
            });


            sender.keyup(function (e) {
                sender.val(changeStringToNumber(sender.val()));
            });

            sender.change(function (e) {
                //TODO
            });
        });
    };

    $.fn.maskHour = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 5);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;
                var hour;
                var min;

                if (length == 2) {

                    hour = value;
                    if (hour > 23)
                        value = format("00");

                }
                else if (length == 4) {

                    hour = value.substr(0, 2);
                    min = value.substr(2, 2);

                    if (hour > 23)
                        value = '00' + min;

                    if (min > 59)
                        value = hour + "00"

                }

                if (length > 2)
                    value = format(value);

                sender.val(value);

            });

            sender.blur(function () {

                var value = validate(sender.val());
                sender.val(format(value));

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function validate(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length == 1)
                value += "000";
            else if (length == 2)
                value += "00";
            else if (length == 3)
                value += "0";

            var hour = value.substr(0, 2);
            var min = value.substr(2, 2);

            if (hour > 23)
                value = "00" + min;

            if (min > 59)
                value = hour + "00"

            return value;
        }

        function format(value) {

            var length = value.length;

            if (length == 0)
                return '';

            return value.substr(0, 2) + ':' + value.substr(2, 2);

        }
    };

    $.fn.maskMonthYear = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 7);

            sender.keypress(function (e) {
                //Se digitou 6 digitos e não digitou a barra, não permite digitar mais. Exemplo 082013 
                if (sender.val().length == 6 && sender.val().indexOf('/') == -1) return false;

                return ((e.which == 47 && sender.val().length == 2) ||
                        (e.which > 47 && e.which < 58) ||
                         e.which == 8 || e.which == 0 || e.which == 13
                       );
            });

            sender.keyup(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;
                var _month;
                var _year;

                if (length == 2) {

                    _month = value;
                    if (parseInt(_month, 10) < 1 || parseInt(_month, 10) > 12) {
                        _month = "00";
                    }

                    value = _month;
                }
                else if (length == 6) {

                    //_month = value.substr(0, 2);
                    //if (parseInt(_month, 10) < 1 || parseInt(_month, 10) > 12) {
                    //    _month = "00";
                    //}

                    //_year = value.substr(2, 4);
                    //if (parseInt(_year, 10) < 1900) {
                    //    _year = "0000";
                    //}

                    //value = validate(_month + _year);
                    value = validate(value);
                }

                if (length > 2)
                    value = format(value);

                //sender.val(value);
            });

            sender.blur(function () {

                var value = validate(sender.val());

                //alert(value.substr(0, 2));
                //alert(value.substr(2, 4));
                //alert(value);


                sender.val(format(value));

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function validate(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length == 0)
                value += "000000";
            else if (length == 1)
                value += "00000";
            else if (length == 2)
                value += "0000";
            else if (length == 3)
                value += "000";
            else if (length == 4)
                value += "00";
            else if (length == 5)
                value += "0";

            _month = value.substr(0, 2);
            if (parseInt(_month, 10) < 1 || parseInt(_month, 10) > 12) {
                _month = "00";
            }

            _year = value.substr(2, 4);
            if (parseInt(_year, 10) < 1900) {
                _year = "0000";
            }

            value = _month + _year;

            return value;
        }

        function format(value) {

            if (value.length == 0) return '';

            if (value == "000000") return '';

            return value.substr(0, 2) + '/' + value.substr(2, 4);
        }
    };

    $.fn.maskCEP = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 10);

            sender.keypress(function (e) {

                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);

            });

            sender.keyup(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;

                if (length > 2 & length <= 5) {
                    value = value.substr(0, 2) + '.' + value.substr(2, length - 2);
                }
                else if (length > 5) {
                    value = format(value, length);
                }

                sender.val(value);

            });

            sender.blur(function (e) {

                var value = changeStringToNumber(sender.val());

                if (value.length > 0) {
                    if (value.length != 8) {
                        alert('CEP inválido!');
                        sender.focus();
                        sender.select();
                        return false;
                    }
                    else
                        value = format(value, 8);

                    sender.val(value);
                }

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function format(value, length) {
            return value.substr(0, 2) + '.' + value.substr(2, 3) + '-' + value.substr(5, length - 5);
        }

    };

    $.fn.maskCPF = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 14);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = sender.val();

                sender.val(format(value));

            });

            sender.blur(function (e) {

                if (!validate(sender.val())) {
                    alert("CPF inválido!");
                    sender.focus();
                    sender.select();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function format(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length >= 4 & length < 7)
                value = value.substr(0, 3) + '.' + value.substr(3, length - 3);
            else if (length >= 7 & length < 10)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, length - 6);
            else if (length >= 10 & length < 11)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 3) + '-' + value.substr(9, length - 9);
            else if (length == 11)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 3) + '-' + value.substr(9, 2);

            return value;

        }

        function validate(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (value.length == 0)
                return true;

            if (value.length > 0 & value.length != 11)
                return false;

            if (value == '00000000000' |
						value == '11111111111' |
						value == '22222222222' |
						value == '33333333333' |
						value == '44444444444' |
						value == '55555555555' |
						value == '66666666666' |
						value == '77777777777' |
						value == '88888888888' |
						value == '99999999999' |
						value == '11122233355') {
                return false;
            }

            var cpf1 = value.substr(0, 9);
            var cpf2 = value.substr(9, 2);
            var d1 = 0;

            for (i = 0; i < 9; i++)
                d1 += cpf1.charAt(i) * (10 - i);

            d1 = 11 - (d1 % 11);

            if (d1 > 9) d1 = 0;

            if (cpf2.charAt(0) != d1)
                return false;

            d1 *= 2;
            for (i = 0; i < 9; i++)
                d1 += cpf1.charAt(i) * (11 - i);
            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;

            if (cpf2.charAt(1) != d1)
                return false;

            return true;
        }
    };

    $.fn.maskCNPJ = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 18);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = sender.val();

                sender.val(format(value));

            });

            sender.blur(function (e) {

                if (!validate(sender.val())) {
                    alert("CNPJ inválido!");
                    sender.focus();
                    sender.select();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function format(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length >= 2 & length < 6)
                value = value.substr(0, 2) + '.' + value.substr(2, length - 2);
            else if (length >= 6 & length < 8)
                value = value.substr(0, 2) + '.' + value.substr(2, 3) + '.' + value.substr(5, length - 5);
            else if (length >= 8 & length < 12)
                value = value.substr(0, 2) + '.' + value.substr(2, 3) + '.' + value.substr(5, 3) + '/' + value.substr(8, length - 8);
            else if (length >= 12 & length < 14)
                value = value.substr(0, 2) + '.' + value.substr(2, 3) + '.' + value.substr(5, 3) + '/' + value.substr(8, 4) + '-' + value.substr(12, length - 12);
            else if (length == 14)
                value = value.substr(0, 2) + '.' + value.substr(2, 3) + '.' + value.substr(5, 3) + '/' + value.substr(8, 4) + '-' + value.substr(12, 2);

            return value;

        }

        function validate(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (value.length == 0)
                return true;

            if (value.length > 0 & value.length != 14)
                return false;

            var l, inx, dig;
            var s1, s2, i, d1, d2, v, m1, m2;

            inx = value.substr(12, 2);
            cnpj = value.substr(0, 12);
            s1 = 0;
            s2 = 0;
            m2 = 2;

            for (i = 11; i >= 0; i--) {
                l = cnpj.substr(i, 1);
                v = parseInt(l);
                m1 = m2;
                m2 = m2 < 9 ? m2 + 1 : 2;
                s1 += v * m1;
                s2 += v * m2;
            }

            s1 %= 11;
            d1 = s1 < 2 ? 0 : 11 - s1;
            s2 = (s2 + 2 * d1) % 11;
            d2 = s2 < 2 ? 0 : 11 - s2;
            dig = d1.toString() + d2.toString();

            if (inx != dig)
                return false;

            return true;
        }
    };

    $.fn.alfaNumeric = function (options) {

        var defaults = {
            length: 100
        };

        var options = $.extend(defaults, options);

        var alfanumeric = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', options.length);

            sender.keypress(function (e) {

                return validateChar(String.fromCharCode(e.which));

            });

            sender.keyup(function (e) {

                sender.val(changeStringToAlfaNumeric(sender.val()));

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function validateChar(char) {

            return alfanumeric.indexOf(char) > -1;

        }

        function changeStringToAlfaNumeric(value) {

            var char = '';
            var aux = '';

            for (i = 0; i < value.length; i++) {

                char = value.substr(i, 1);

                if (alfanumeric.indexOf(char) > -1) {
                    aux += char;
                }
            }

            return aux;
        }
    };

    $.fn.maskDate = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 10);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;

                if (length > 8)
                    value = value.substr(0, 8);

                if (length > 2 & length <= 4) {
                    value = value.substr(0, 2) + '/' + value.substr(2, length - 2);
                }
                else if (length > 4) {
                    value = value.substr(0, 2) + '/' + value.substr(2, 2) + '/' + value.substr(4, length - 4);
                }
                else if (length == 8) {
                    value = format(value);
                }

                sender.val(value);

            });

            sender.blur(function (e) {

                var value = changeStringToNumber(sender.val());

                if (!validate(value)) {
                    //sender.val('');

                    setTimeout(function () { sender.select(); }, 50);

                    sender.focus();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });
            try {
                var dtIn;
                var dtFn;
                try {
                    dtIn = sender.attr("dayIn").split('/');
                } catch (e) {
                    dtIn = null;
                }
                try {
                    dtFn = sender.attr("dayFn").split('/');
                } catch (e) {
                    dtFn = null;
                }
                if (dtIn != null && dtFn != null) {
                    sender.datepicker({ minDate: new Date(dtIn[2], dtIn[1] - 1, dtIn[0]), maxDate: new Date(dtFn[2], dtFn[1] - 1, dtFn[0]) });
                } else if (dtIn != null && dtFn == null) {
                    sender.datepicker({ minDate: new Date(dtIn[2], dtIn[1] - 1, dtIn[0]) });
                } else if (dtIn == null && dtFn != null) {
                    sender.datepicker({ maxDate: new Date(dtFn[2], dtFn[1] - 1, dtFn[0]) });
                } else {
                    sender.datepicker();
                }

            } catch (e) {
                sender.datepicker();
            }

            //            sender.datepicker({
            //                beforeShow: function (input, inst) { 
            //                
            //                    if(input.attr('disabled') == 'disabled')
            //                        inst.hide();
            //                
            //                } 
            //            });

        });

        function format(value) {

            var dia = value.substr(0, 2);
            var mes = value.substr(2, 2);
            var ano = value.substr(4, 4);

            return dia + '/' + mes + '/' + ano
        }

        function validate(value) {

            if (value.length == 0)
                return true;

            if (value.length != 8)
                return false;

            var dia = value.substr(0, 2);
            var mes = value.substr(2, 2);
            var ano = value.substr(4, 4);


            if (dia > 31 || mes > 12 || ano > 2100 || (ano < 1800 && ano > 99) || ano.length == 3 || ano.length == 1 || mes < 1 || dia < 1)
                return false;

            if (dia.length == 1)
                dia = "0" + dia;

            if (mes.length == 1)
                mes = "0" + mes;

            if (ano.length == 2) {
                if (ano < 15)
                    ano = "20" + ano;
                else
                    ano = "19" + ano;

            }

            var arrMes = new Array();
            arrMes[1] = arrMes[3] = arrMes[5] = arrMes[7] = arrMes[8] = arrMes[10] = arrMes[12] = 31;
            arrMes[4] = arrMes[6] = arrMes[9] = arrMes[11] = 30;
            arrMes[2] = ano % 4 == 0 ? 29 : 28;

            if (dia > arrMes[parseInt(mes)]) {
                return false;
            }

            return true;

        }

    };

    $.fn.validMail = function (options) {

        var defaults = {
            length: 70
        };

        var options = $.extend(defaults, options);

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', options.length);

            sender.blur(function () {

                if (!validate(sender.val())) {
                    alert('E-mail inválido!');
                    sender.select();
                    sender.focus();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });

            function validate(value) {

                if (value.length > 0) {

                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                    if (filter.test(value))
                        return true;
                    else
                        return false;
                }

                return true;
            }

        });
    };

    $.fn.maskNumeroContrato = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 22);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = sender.val();

                sender.val(format(value));

            });

            sender.blur(function (e) {

                var value = changeStringToNumber(sender.val());

                if (!validate(value)) {
                    //sender.val('');

                    setTimeout(function () { sender.select(); }, 50);

                    sender.focus();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function format(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length >= 3 & length < 7)
                value = value.substr(0, 2) + '.' + value.substr(2, length - 2);
            else if (length >= 7 & length < 10)
                value = value.substr(0, 2) + '.' + value.substr(2, 4) + '.' + value.substr(6, length - 6);
            else if (length >= 10 & length < 17)
                value = value.substr(0, 2) + '.' + value.substr(2, 4) + '.' + value.substr(6, 3) + '.' + value.substr(9, length - 9);
            else if (length >= 17 & length < 19)
                value = value.substr(0, 2) + '.' + value.substr(2, 4) + '.' + value.substr(6, 3) + '.' + value.substr(9, 7) + '-' + value.substr(16, length - 16);
            else if (length == 18)
                value = value.substr(0, 2) + '.' + value.substr(2, 4) + '.' + value.substr(6, 3) + '.' + value.substr(9, 7) + '-' + value.substr(16, 2);

            if (value == "00.0000.000.0000000-00")
                return "";
            return value;
        }
    };

    $.fn.maskNumeroContratoBB = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 11);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = sender.val();

                sender.val(format(value));

            });

            sender.blur(function (e) {

                var value = changeStringToNumber(sender.val());

                if (!validate(value)) {
                    //sender.val('');

                    setTimeout(function () { sender.select(); }, 50);

                    sender.focus();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function format(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length >= 4 & length < 7)
                value = value.substr(0, 3) + '.' + value.substr(3, length - 3);
            else if (length >= 7 & length < 10)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, length - 6);
            else if (length >= 10 & length < 11)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 3) + '-' + value.substr(9, length - 9);

            if (value == "000.000.000")
                return "";
            return value;
        }
    };



    $.fn.maskPis = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 14);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = sender.val();

                sender.val(format(value));

            });

            sender.blur(function (e) {

                if (!validate(sender.val())) {
                    alert("PIS inválido!");
                    sender.focus();
                    sender.select();
                    return false;
                }

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function format(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length >= 4 & length < 7)
                value = value.substr(0, 3) + '.' + value.substr(3, length - 3);
            else if (length >= 7 & length < 10)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, length - 6);
            else if (length >= 10 & length < 11)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 3) + '-' + value.substr(9, length - 9);
            else if (length == 11)
                value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 4) + '-' + value.substr(10, 1);

            return value;

        }

        //function validate(value) {
        function validate(value) {

            value = changeStringToNumber(value);
            //var length = value.length;
            var fixo = "3298765432";
            var total = 0;
            var resto = 0;
            var numPIS = 0;
            var strResto = "";

            numPIS = value;

            if (value.length == 0) { return true; }

            if (numPIS == "" || numPIS == null) {
                return false;
            }

            for (i = 0; i <= 9; i++) {
                var resultado = (numPIS.slice(i, i + 1)) * (fixo.slice(i, i + 1));
                total = total + resultado;
            }

            resto = (total % 11);

            if (resto != 0) {
                resto = 11 - resto;
            }

            if (resto == 10 || resto == 11) {
                strResto = resto + "";
                resto = strResto.slice(1, 2);
            }

            if (resto != (numPIS.slice(10, 11))) {
                return false;
            }

            return true;
        }

    };


    $.fn.maskTime = function () {

        return this.each(function () {

            var sender = $(this);

            sender.attr('maxlength', 5);

            sender.keypress(function (e) {
                return (e.which > 47 && e.which < 58 || e.which == 8 || e.which == 0 || e.which == 13);
            });

            sender.keyup(function (e) {

                var value = changeStringToNumber(sender.val());

                var length = value.length;
                var hour;
                var min;

                if (length == 2) {

                    hour = value;

                }
                else if (length == 4) {

                    hour = value.substr(0, 2);
                    min = value.substr(2, 2);

                    if (min > 59)
                        value = hour + "00"

                }

                if (length > 2)
                    value = format(value);

                sender.val(value);

            });

            sender.blur(function () {

                var value = validate(sender.val());
                sender.val(format(value));

            });

            sender.change(function (e) {
                //TODO
            });
        });

        function validate(value) {

            var value = changeStringToNumber(value);
            var length = value.length;

            if (length == 1)
                value += "000";
            else if (length == 2)
                value += "00";
            else if (length == 3)
                value += "0";

            var hour = value.substr(0, 2);
            var min = value.substr(2, 2);

            if (min > 59)
                value = hour + "00"

            return value;
        }

        function format(value) {

            var length = value.length;

            if (length == 0)
                return '';

            return value.substr(0, 2) + ':' + value.substr(2, 2);

        }
    };







    //$.fn.maskPis = function () {

    //    return this.each(function () {

    //        var sender = $(this);

    //        sender.attr('maxlength', 14);

    //        sender.keyup(function (e) {

    //            var value = sender.val();

    //            sender.val(format(value));

    //        });

    //        sender.blur(function (e) {

    //            if (!validate(sender.val())) {
    //                alert("Pis inválido!");
    //                sender.focus();
    //                sender.select();
    //                return false;
    //            }

    //        });

    //        sender.change(function (e) {
    //            //TODO
    //        });
    //    });

    //    function format(value) {

    //        var value = changeStringToNumber(value);
    //        var length = value.length;

    //        if (length >= 4 & length < 7)
    //            value = value.substr(0, 3) + '.' + value.substr(3, length - 3);
    //        else if (length >= 7 & length < 10)
    //            value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, length - 6);
    //        else if (length >= 10 & length < 11)
    //            value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 3) + '-' + value.substr(10, length - 10);
    //        if (length == 11)
    //            value = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 4) + '-' + value.substr(10, 1);

    //        return value;

    //    }












})(jQuery);

function changeInnerLabelBlur(control, label) {
    if (trim(control.value) == '')
        control.value = label;
}

function changeInnerLabelFocus(control, label) {
    if (trim(control.value) == label)
        control.value = '';
}

function setFocus(control) {
    document.getElementById('" + control + "').focus();
    window.onload = SetFocus;
}

function trim(s) {
    var m = s.match(/^\s*(\S+(\s+\S+)*)\s*$/);
    return (m == null) ? "" : m[1];
}

