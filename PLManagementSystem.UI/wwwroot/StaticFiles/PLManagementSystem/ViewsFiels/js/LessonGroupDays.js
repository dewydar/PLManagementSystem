var openForm = function (endPoint,title) {
    $.ajax({
        url: endPoint,
        method: 'GET'
    }).done(data => {
        $('#modal-trigger .modal-title').text('');
        $('#modal-trigger .modal-body').html('');
        $('#modal-trigger .modal-title').text(title);
        $('#modal-trigger .modal-body').html(data);
        reinitializeSelect2();
        $('#modal-trigger').modal('show');
    })
}
var deleteDay = function (e) {
    var confirmButtonTextMSG = $('#deleteAction').attr('confirmButtonTextMSG');
    var textDeleteMSG = $('#deleteAction').attr('textDeleteMSG');
    var titleDeleteMSG = $('#deleteAction').attr('titleDeleteMSG');
    e.preventDefault();
    var resultTitleMessage = '';
    resultTitleMessage = titleDeleteMSG;
    Swal.fire({
        title: titleDeleteMSG,
        text: textDeleteMSG,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmButtonTextMSG
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById("deleteDaysForm").submit();
        } else {
            return false;
        }
    })
}