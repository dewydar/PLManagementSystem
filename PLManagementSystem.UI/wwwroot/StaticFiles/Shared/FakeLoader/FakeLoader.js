! function (d) {
    d.fakeLoader = function (i) {
        var s = d.extend(d, i),
            e = d("body").find("." + s.targetClass);
        e.each(function () {
            switch (s.spinner) {
                case "spinner1":
                    e.html('<div class="fl fl-spinner spinner1"><div class="double-bounce1"></div><div class="double-bounce2"></div></div>');
                    break;
                case "spinner2":
                    e.html('<div class="fl fl-spinner spinner2"><div class="spinner-container container1"><div class="circle1"></div><div class="circle2"></div><div class="circle3"></div><div class="circle4"></div></div><div class="spinner-container container2"><div class="circle1"></div><div class="circle2"></div><div class="circle3"></div><div class="circle4"></div></div><div class="spinner-container container3"><div class="circle1"></div><div class="circle2"></div><div class="circle3"></div><div class="circle4"></div></div></div>');
                    break;
                case "spinner3":
                    e.html('<div class="fl fl-spinner spinner3"><div class="dot1"></div><div class="dot2"></div></div>');
                    break;
                case "spinner4":
                    e.html('<div class="fl fl-spinner spinner4"></div>');
                    break;
                case "spinner5":
                    e.html('<div class="fl fl-spinner spinner5"><div class="cube1"></div><div class="cube2"></div></div>');
                    break;
                case "spinner6":
                    e.html('<div class="fl fl-spinner spinner6"><div class="rect1"></div><div class="rect2"></div><div class="rect3"></div><div class="rect4"></div><div class="rect5"></div></div>');
                    break;
                case "spinner7":
                    e.html('<div class="fl fl-spinner spinner7"><div class="circ1"></div><div class="circ2"></div><div class="circ3"></div><div class="circ4"></div></div>');
                    break;
                default:
                    e.html(c)
            }
        }), e.css({
            backgroundColor: s.bgColor,
            opacity: s.opacity,
            display: s.display
        })
    }
}(jQuery);



async function LoaderIndicator(display) {
    if (display == 'none') {
        console.log(false);

    } else {
        console.log(true);

    }
    $.fakeLoader({
        targetClass: "fakeLoader",
        bgColor: "#007BFF",
        spinner: "spinner6",
        opacity: 0.5,
        display: display
    });
}