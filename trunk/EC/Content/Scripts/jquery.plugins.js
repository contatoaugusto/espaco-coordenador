var userAgent = navigator.userAgent.toLowerCase();

// Figure out what browser is being used
$.browser = {
    version: (userAgent.match(/.+(?:rv|it|ra|ie|me)[\/: ]([\d.]+)/) || [])[1],
    chrome: /chrome/.test(userAgent),
    safari: /webkit/.test(userAgent) && !/chrome/.test(userAgent),
    opera: /opera/.test(userAgent),
    msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
    mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent)
};

$.fn.animatefocus = function (options) {

    // default configuration properties
    var defaults = {
        interval: 1000
    };

    var options = $.extend(defaults, options);
    var target = this;

    if (target.length) {
        var targetOffset = target.offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, options.interval);
    }
};

$.expr[':'].Contains = function (a, i, m) {
    return (a.textContent || a.innerText || "").toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
};

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

Date.prototype.diffDays = function (date) {

    var utcThis = Date.UTC(this.getFullYear(), this.getMonth(), this.getDate(), this.getHours(), this.getMinutes(), this.getSeconds(), this.getMilliseconds());
    var utcOther = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());

    return (utcThis - utcOther) / 86400000;
};
