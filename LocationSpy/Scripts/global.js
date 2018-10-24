// Global functions

if (!('contains' in String.prototype)) {
    String.prototype.contains = function (str, startIndex) {
        return ''.indexOf.call(this, str, startIndex) !== -1;
    };
};

function renderLoading(elementId, flickeringRate, loadingSymbol, maxNumberOfLoadingSymbol) {
    flickeringRate = flickeringRate > 0 ? flickeringRate : 600;
    loadingSymbol = loadingSymbol ? loadingSymbol : ".";
    maxNumberOfLoadingSymbol = maxNumberOfLoadingSymbol > 0 ? maxNumberOfLoadingSymbol : 3;
    setInterval(function () {
        var element = document.getElementById(elementId);
        var count = element.innerHTML.length;
        if (count < maxNumberOfLoadingSymbol) {
            element.innerHTML += (loadingSymbol + "");
        } else {
            element.innerHTML = "";
        }
    }, flickeringRate);
}

function renderFlicker(flickeringRate) {
    flickeringRate = flickeringRate > 0 ? flickeringRate : 600;
    setInterval(function () {
        var elements = document.getElementsByClassName("flicker");
        Array.prototype.forEach.call(elements, function (slide, index) {
            if (elements.item(index).style.visibility === "hidden") {
                elements.item(index).style.visibility = "visible";
            } else {
                elements.item(index).style.visibility = "hidden";
            }
        });
    }, flickeringRate);
};

function MicrosoftJsonDateFormatStringToTimeInMillis(ticksString) {
    // "\/Date(1539953962642)\/";
    // IT CALLED Microsoft's built-in JSON Date format - it's not part of any standard
    // and the number in the string is NOT the .NET DateTime ticks
    // NO NEED TO convert, IT IS NOT Ticks !!!
    // See more at https://www.codeproject.com/tips/467697/converting-a-net-datetime-object-to-a-javascript-d
    // https://stackoverflow.com/questions/206384/how-do-i-format-a-microsoft-json-date
    var ticks = ticksString.replace(/[^0-9]/ig, "");
    return new Date(parseInt(ticks));
    //
    // Ticks means since 0001-01-01 00:00:00.000 to now (unit: 100 nanosecond, type long)
    // It's for .NET
    //
    // TimeInMillis means since 1970-01-01 00:00:00.000 to now (unit: 1 millisecond, type long)
    // It's for Java and JavaScript
    //
};

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
// 例子：
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18
Date.prototype.Format = function (fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1,                 //月份
        "d+": this.getDate(),                    //日
        "h+": this.getHours(),                   //小时
        "m+": this.getMinutes(),                 //分
        "s+": this.getSeconds(),                 //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds()             //毫秒
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function hexColorToRgb(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
};

function isUnique(data, target, name) {
    var i = 0,
        flag = true,
        c = target.length;
    for (; i < c; i++) {
        if (data[name] == target[i].value) {
            flag = false;
            break;
        }
    }
    return flag;
};

function fallbackCopyTextToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;
    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Fallback: Copying text command was ' + msg);
    } catch (err) {
        console.error('Fallback: Oops, unable to copy', err);
    }

    document.body.removeChild(textArea);
};

function copyToClipboard(text) {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(text);
        return;
    }
    navigator.clipboard.writeText(text).then(function () {
        console.log('Async: Copying to clipboard was successful!');
    }, function (err) {
        console.error('Async: Could not copy text: ', err);
    });
};

function copyInputToClipboard(targetInputId) {
    var element = document.getElementById(targetInputId);
    element.focus();
    element.select();
    document.execCommand('copy');
};