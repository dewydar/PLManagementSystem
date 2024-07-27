class StringConverter {
    static toBolean(str) {
        const truePattern = /^(true|1)$/i;
        return truePattern.test(str.trim());
    }
}
class AppStorge {
    static SetTheme(isDark) {
        return localStorage.setItem('isDark', isDark);
    }
    static GetTheme() {
        return StringConverter.toBolean(localStorage.getItem('isDark'));
    }
}


var ActiveLayoutLink = function () {
    var currentPath = window.location.pathname.substring(1);
    var currentPathSplit = currentPath.split('/').filter(function (v) { return v !== '' });
    var controllerName = currentPathSplit[0];
    var itemActivemain = [];
    $('.nav-link').each(function () {
        var sideLink = $(this).attr('href');
        var sideLinkSplit = sideLink.split('/').filter(function (v) { return v !== '' });
        var currentlinkController = sideLinkSplit[0];
        if (sideLink === "/" + currentPath) {
            $(this).addClass('active');
            this.parentElement.parentElement.parentElement.classList.add('menu-is-opening', 'menu-open');
        } 
        else if (currentlinkController == controllerName) {
            itemActivemain.push(this);
        }
    })
    if ($('.nav-link').hasClass('active') ==false) {
        itemActivemain.filter((u) => {
            var href = $(u).attr('href');
            var sideLinkSplit = href.split('/').filter(function (v) { return v !== '' });
            var currentlinkAction = sideLinkSplit[1];
            if (currentlinkAction == undefined) {
                $(u).addClass('active');
                u.parentElement.parentElement.parentElement.classList.add('menu-is-opening', 'menu-open');
            }
        });
    }
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}