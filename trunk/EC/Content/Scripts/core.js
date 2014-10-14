

var hider = function () {
    var _elem = _warapper = _layer = _callback = callAgain = undefined;

    this.init = function (params) {
        _elem = params.elem.css("float", "left");
        _warapper = _elem.wrap('<div id="wrapper" style="position: relative;margin: auto;overflow: hidden;"></div>').parent();
        _layer = _warapper.prepend('<div id="layer" style="width : 100%;height : 100%;float: left;position:absolute; z-index: 99999;background:white;"></div>').find("#layer");
        _layer.fadeTo("0", 0);
        _callback = params.callback;
        bindEvents();
    };
    this.GetWarapper = function () {
        return _warapper == undefined ? undefined : _warapper.prop('outerHTML');
    };

    function bindEvents() {
        _layer.click(function () {
            _callback({ elem: _elem, layer: _layer, wrapper: _warapper });
        });
    }
};

var library = {
    chamarQuandoVerdadeiro:
        function (params) {
            if (params.condicao()) {
                params.chamada();
            }
        },
    chamarACadaXSegundos: function (params, setTimeOut) {
        var timeOut;
        if (!library.isFunction(setTimeOut)) {
            setTimeout = function(time) {
                return time;
            };
        }

        function recursiveCall() {
            clearTimeout(timeOut);
            timeOut = setTimeOut(setTimeout(recursiveCall, 1));
            params.chamada(params.chamadaParams);
        }
        recursiveCall();
    },
    filter: function (array, acc, condition) {
        return library.loop(array, acc, { condition: condition });
    },
    hd: function (array) {
        return array[0] == undefined ? [] : array[0];
    },
    tl: function (array) {
        return array.slice(1);
    },
    addItem: function (array, item) {

        if (library.isArray(array))
            array[array.length] = item;
        else
            array += item;

        return array;
    },
    isInArray: function (array, obj, comparator) {
        var validator = library.validateArrayToRecursion(array);

        if (!validator.isValid)
            return [];

        if (!library.isFunction(comparator)) {
            comparator = function (left, rigth) {
                return left == rigth;
            };
        }
        function aux(array, obj) {
            if (array.length == 0)
                return false;
            else
                return comparator(library.hd(array), obj) ? true : aux(library.tl(array), obj);
        }
        return aux(validator.array, obj);

    },
    removeEquals: function (array, comparator, transformator) {
        var validator = library.validateArrayToRecursion(array);

        if (!validator.isValid)
            return [];

        if (!library.isFunction(transformator)) {
            transformator = function (obj) {
                return obj;
            };
        }

        var accumulator = [transformator(library.hd(array))];
        var tlList = library.tl(validator.array);

        return library.loop(tlList, accumulator, {
            concat: function (acc, hd) {
                if (!library.isInArray(acc, hd, comparator))
                    return library.addItem(acc, transformator(hd));
                else
                    return acc;
            }
        });
    },
    capitalizeWord: function (str) {
        return str.substring(0, 1).toUpperCase() + str.substring(1).toLowerCase();
    },
    isArray: function (obj) {
        return obj instanceof Array;
    },
    isString: function (obj) {
        return obj instanceof String;
    },
    isFunction: function (obj) {
        return obj instanceof Function;
    },
    isAllCaps: function (str) {
        return str == str.toUpperCase();
    },
    loopIncrement: function (array, acc, content, concat) {
        return library.loop(array, acc, { content: content, concat: concat });
    },
    toArray: function (obj) {
        if (obj == undefined)
            return [];
        else if (library.isFunction(obj.toArray))
            return obj.toArray();
        else if (library.isArray(obj))
            return obj;
        else
            return [obj];
    },
    validateArrayToRecursion: function (array) {
        return {
            isValid: (array != undefined && array.length > 0),
            array: library.toArray(array)
        };
    },
    loop: function (array, acc, params) {
        var validator = library.validateArrayToRecursion(array);

        if (!validator.isValid)
            return acc;

        array = validator.array;
        var content, concat, condition;

        if (!(params == undefined)) {
            content = params.content;
            concat = params.concat;
            condition = params.condition;
        }

        var arrayLength = validator.array.length;

        if (!library.isFunction(content))
            content = function (hd) { return hd; };

        if (library.isFunction(condition))
            concat = function (acc, hd) {
                if (condition(hd))
                    return library.addItem(acc, hd);
                else
                    return acc;
            };

        if (!library.isFunction(concat))
            concat = function (acc, hd) { return library.addItem(acc, hd); };

        for (var i = 0; i < arrayLength; i++)
            acc = concat(acc, content(array[i]));

        return acc;
    }
};

var watcher = function () {
    var _elem;
    var _dimensionsWatchers = [];
    var _displayWatchers = [];
    // watcherDimensions {
    /* Params
    * heightTolerance   = the tolerance for height diference . Ex: if you want to call de resize method only when
    *                     the height of the element change 5px , then you pass 5 to heightTolerance.
    * widthTolerance    = same of heightTolerance
    * resize            = function to call when a change occurs
    */
    var watcherDimensions = function (params) {

        var timeOut;

        /* _dimensions
        * heigth   = current, the current height of the element
        *            previous, previous heigth of the element
        *            tolerance, the height change tolarance for this element
        * width    = same of height
        * resize   = function to call ehan change occurs
        */
        var _dimensions = {
            height: {
                current: function () {
                    return _elem.self.height();
                },
                previous: 0,
                tolerance: params.heightTolerance == undefined ? 0 : params.heightTolerance
            },

            width: {
                current: function () {
                    return _elem.self.width();
                },
                previous: 0,
                tolerance: params.widthTolerance == undefined ? 0 : params.widthTolerance
            },
            resize: params.resize
        };

        function onResize() {
            upDateSize();
            _dimensions.resize();
        }



        function upDateSize() {
            _dimensions.height.previous = _dimensions.height.current();
            _dimensions.width.previous = _dimensions.width.current();
        }



        function heightDiference() {
            var diference = _dimensions.height.previous - _dimensions.height.current();
            return diference >= 0 ? diference : (diference * -1);
        }

        function widthDiference() {
            var diference = _dimensions.width.previous - _dimensions.width.current();
            return diference >= 0 ? diference : (diference * -1);
        }

        this.initResize = function (params) {
            if (_dimensions.resize != undefined) {
                upDateSize();

                interval = library.chamarACadaXSegundos({
                    segundos: 1,
                    chamada: library.chamarQuandoVerdadeiro,
                    chamadaParams:
                    {
                        condicao: function () {
                            return heightDiference() > _dimensions.height.tolerance || widthDiference() > _dimensions.width.tolerance
                        },
                        chamada: onResize
                    }
                },
                    function (time) {
                        timeOut = time;
                        return time;
                    });
            }
            ;
        };
    };
    // }

    var watcherDisplay = function (params) {

        var timeOut;

        var display = {
            current: function() {
                return _elem.self.css('display');
            },
            isVisible: function() {
                this.current != 'none' && this.current != 'hidden';
            },
            previous: this.current,
            displayChange: params.displayChange,
            onlyFirstChange: params.onlyFirstChange
        };

        function onDisplayChange() {
            display.displayChange();
            if (display.onlyFirstChange) {
                clearTimeout(timeOut);
            }
            upDateDisplay();
        }

        function upDateDisplay() {
            display.previous = display.current();
        }

        this.initDisplayChange = function () {
            if (display.displayChange != undefined) {
                upDateDisplay();

                library.chamarACadaXSegundos({
                    segundos: 1,
                    chamada: library.chamarQuandoVerdadeiro,
                    chamadaParams: {
                        condicao: function () {
                            return display.previous != display.current();
                        },
                        chamada: onDisplayChange
                    }
                }, function (time) {
                    timeOut = time;
                    return timeOut;
                });
            }
        };

    };

    /* Params
    * selector          = element selector, string
    * elem              = jQuery element, object
    */
    this.init = function(params) {
        _elem = {
            self: params.elem
        };
        var watcherObject;
        if (params.watchDimension == true) {
            watcherObject = new watcherDimensions(params);
            watcherObject.initResize();
            library.addItem(_dimensionsWatchers, watcherObject);
        }
        if (params.watchDisplay == true) {
            watcherObject = new watcherDisplay(params);
            watcherObject.initDisplayChange();
            library.addItem(_displayWatchers, watcherObject);
        }
    };
};

var watchers = (function () {
    var watchers = [];

    var init = function(elements, params) {
        elements.each(function() {
            var watcherObject = new watcher();
            $.extend(params, { elem: $(this) });
            watcherObject.init(params);
            library.addItem(watchers, watcherObject);
        });
    };

    return { init: init };
})();

var scroller = function (scroller) {
    
    var _scroller, _buttons, _childHolder, _children, _intensity, _showMode;
    var _binded = false;

    function bindObjects() {
        _scroller = $(scroller);
        _children = _scroller.children().not('[data-scrollbutton]');
        _childHolder = $('<div class="holder"></div>').append(_children);
        _binded = true;
    }

    function setObjects() {
        if (setScroller()) {
            setChildren();
            setButtons();
        }
    }

    function setChildren() {

        function sumWidth(list, out) {
            if (list.length == 0)
                return out;
            else {
                var hd = $(library.hd(list));
                return sumWidth(library.tl(list), (out + hd.outerWidth() + parseInt(hd.css('margin-left')) + parseInt(hd.css('margin-right'))));
            }
        }

        if ($.browser.msie) {
            _childHolder.css('width', sumWidth(_children, 0) + 1);
        } else {
            _childHolder.css('width', sumWidth(_children, 0));
        }

        _children.css('float', 'left');
    }

    function setScroller() {

        var display = _scroller.css('display');
        if (display == 'none' || display == 'hidden') {
            var params = { watchDisplay: true, displayChange: function () { apply() } };
            _scroller.watch(params);
            return false;
        } else {
            if (_children.length == 0)
                return false;
            _scroller.attr('data-scroller', '');
            _scroller.append(_childHolder);
            _scroller.css('height', library.hd(_children).offsetHeight).css('overflow', 'hidden').css('position', 'relative');
            return true;
        }
    };

    function setButtons() {

        var buttons = $('[data-scrollbutton]').filter('[data-scroll=#' + _scroller.attr('id') + ']');

        _buttons = { next: buttons.filter('[data-scrollbutton="next"]'), previous: buttons.filter('[data-scrollbutton="previous"]') }

        var show = _scroller.data().show;
        show = (show == undefined ? 1 : (show == 0 ? 1 : show));

        setIntensity();

        _intensity = _intensity * show;
        _buttons.next.attr('data-args', '+=' + _intensity);
        _buttons.previous.attr('data-args', '-=' + _intensity);

        buttons.click(function (e) {
            e.stopPropagation();
            var data = $(this).data();
            $(data.scroll).animate({ scrollLeft: data.args }, 500);
            return false;
        });
    };

    function setIntensity() {
        if (_showMode == 'holder') {
            _intensity = _scroller.outerWidth() / 1.5;
        } else {
            _intensity = library.hd(_children).offsetWidth;
        }
    };

    function apply() {
        if (!_binded)
            bindObjects();
        setObjects();
    };

    this.applyScroller = function (params) {
        _showMode = params.showMode == undefined ? 'child' : params.showMode;
        apply();
    };

};

var scrollers = (function () {

    var scrollers = [];

    var init = function (elements, params) {
        elements.each(function () {
            var scrollerObject = new scroller($(this));
            scrollerObject.applyScroller(params);
            library.addItem(scrollers, scrollerObject);
        });
    };

    return {
        init: init,
        scrollers: scrollers
    };

})();


(function ($) {

    $.fn.hasAttr = function (attr) {
        return this.attr(attr) != undefined;
    };

    $.fn.scroller = function (params) {
        scrollers.init(this, params);
        return $(scrollers.scrollers);
    };

    $.fn.watch = function (params) {
        watchers.init(this, params);
    };
    
})(jQuery);
