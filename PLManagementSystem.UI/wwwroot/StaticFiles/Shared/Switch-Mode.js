function SwithMode() {
    var isDark = AppStorge.GetTheme();
    document.getElementById('checkbox').checked = isDark;
    var bdy = document.getElementById('bdy');
    var nv = document.getElementById('nv');
    var nv2 = document.getElementById('nv2');
    var minSideBar = document.getElementById('main-sidebar');

    if (isDark) {
        bdy.classList.add("dark-mode");
        nv.classList.add("navbar-dark");
        nv.classList.remove("navbar-light");
        minSideBar.classList.remove("sidebar-light-primary");
        minSideBar.classList.add("sidebar-dark-primary");
        if (nv2 != null) {
            nv2.classList.add("navbar-dark");
            nv2.classList.remove("navbar-light");
        }
    } else {
        if (nv2 != null) {
            nv2.classList.remove("navbar-dark");
            nv2.classList.add("navbar-light");
        }
        bdy.classList.remove("dark-mode");
        nv.classList.remove("navbar-dark");
        nv.classList.add("navbar-light");
        minSideBar.classList.remove("sidebar-dark-primary");
        minSideBar.classList.add("sidebar-light-primary");
    }


}
class AppStorge {
    static SetTheme(isDark) {
        return localStorage.setItem('isDark', isDark);
    }
    static GetTheme() {
        try {

            return StringConverter.toBolean(localStorage.getItem('isDark'));
        } catch (e) {
            return false;
        }
    }
}

function onModeSwitch() {
    AppStorge.SetTheme(document.getElementById('checkbox').checked);
    SwithMode();
}
class StringConverter {
    static toBolean(str) {
        const truePattern = /^(true|1)$/i;
        return truePattern.test(str.trim());
    }
}