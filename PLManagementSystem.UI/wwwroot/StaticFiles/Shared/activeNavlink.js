//script to set nav-link to be active according to current pathname(URL)
document.addEventListener('DOMContentLoaded', () => {
    var url = location.pathname
    document.querySelectorAll('.nav-link').forEach(link => {
        if (link.getAttribute('href').toLowerCase() === url.toLowerCase() || link.getAttribute('href').toLowerCase() === "/" + url.split("/")[1].toLowerCase()) {
            link.classList.add('active');
        } else {
            link.classList.remove('active');
        }
    });
})