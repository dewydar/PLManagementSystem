
// Toast
var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000
});
(function () {
    var danger = document.getElementsByClassName("alert alert-danger");
    var success = document.getElementsByClassName("alert alert-success");

    setTimeout(function () {
        if (danger.length > 0) { danger[0].style.display = "none"; }
        if (success.length > 0) { success[0].style.display = "none"; }
    }, 5000);
})();
// end