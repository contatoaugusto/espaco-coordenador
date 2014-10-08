var _abe = document.createElement("div");

function pageLoad() {
    var m = Sys.WebForms.PageRequestManager.getInstance();

    _timerLoad();

    m.add_beginRequest(OnBeginRequest);
    m.add_endRequest(OnEndRequest);

    document.body.appendChild(_abe);
}

function OnBeginRequest(sender, args) {
    _enableUI(false);
}

function OnEndRequest(sender, args) {
    _enableUI(true);
}

function _enableUI(state) {

    var o = $get('ajax-updating');

    if (!state) {

        _alignUI();

        _abe.style.display = '';
        _abe.style.position = 'fixed';
        _abe.style.left = '0px';
        _abe.style.top = '0px';

        var clientBounds = this._getClientBounds();
        var clientWidth = clientBounds.width;
        var clientHeight = clientBounds.height;
        _abe.style.width = Math.max(Math.max(document.documentElement.scrollWidth, document.body.scrollWidth), clientWidth) + 'px';
        _abe.style.height = Math.max(Math.max(document.documentElement.scrollHeight, document.body.scrollHeight), clientHeight) + 'px';
        _abe.style.zIndex = 16777270;
        _abe.className = "ajax-modalbackground";

        o.style.display = '';

        _timerReset();
        _timerStartStop();

    }
    else {
        _abe.style.display = 'none';
        o.style.display = 'none';
    }

    o.style.zIndex = 16777271;
}

function _alignUI() {

    var winH = $(window).height();
    var winW = $(window).width();
    var scrollTop = $(document).scrollTop();

    var $a = $('#ajax-updating');

    $a.css('top', 40 + scrollTop + 'px');
    $a.css('left', winW / 2 - $a.width() / 2);

};

function _getClientBounds() {
    var cw;
    var ch;
    switch (Sys.Browser.agent) {
        case Sys.Browser.InternetExplorer:
            cw = document.documentElement.clientWidth;
            ch = document.documentElement.clientHeight;
            break;
        case Sys.Browser.Safari:
            cw = window.innerWidth;
            ch = window.innerHeight;
            break;
        case Sys.Browser.Opera:
            cw = Math.min(window.innerWidth, document.body.clientWidth);
            ch = Math.min(window.innerHeight, document.body.clientHeight);
            break;
        default:  // Sys.Browser.Firefox, etc.
            cw = Math.min(window.innerWidth, document.documentElement.clientWidth);
            ch = Math.min(window.innerHeight, document.documentElement.clientHeight);
            break;
    }
    return new Sys.UI.Bounds(0, 0, cw, ch);
}


var t = [0, 0, 0, 0, 0, 0, 0, 1];

function _timerStartStop() {
    t[t[2]] = (new Date()).valueOf();
    t[2] = 1 - t[2];
    if (0 == t[2]) {
        clearInterval(t[4]);
        t[3] += t[1] - t[0];
        t[4] = t[1] = t[0] = 0;
        _timerDisplay();
    }
    else
        t[4] = setInterval(_timerDisplay, 43);
}

function _timerReset() {
    if (t[2])
        _timerStartStop();
    t[4] = t[3] = t[2] = t[1] = t[0] = 0;
    _timerDisplay();
    t[7] = 1;
}

function _timerDisplay() {
    
    if (t[2]) t[1] = (new Date()).valueOf();
    if( t[6] != undefined )
        t[6].innerHTML = _timerFormat(t[3] + t[1] - t[0]);
}

function _timerFormat(ms) {
    var d = new Date(ms + t[5]).toString()
		.replace(/.*([0-9][0-9]:[0-9][0-9]:[0-9][0-9]).*/, '$1');
    var x = String(ms % 1000);
    while (x.length < 3) x = '0' + x;
    d += '.' + x;
    return d;
}

function _timerLoad() {

    var o = $get('ajax-updating-timer');
    o.innerHTML = '';

    t[5] = new Date(1970, 1, 1, 0, 0, 0, 0).valueOf();
    t[6] = o;
    _timerDisplay();
}
