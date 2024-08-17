$(() => {
    LoadDays();
})
// load template details
var LoadMainInfo = function (id) {
    var endPoint = $('#loadMainInfoAction').val();
    $.ajax({
        url: endPoint,
        method: 'GET',
        data: {
            id: id
        }
    }).done(data => {
        $('#MainInfo').html('');
        $('#MainInfo').html(data);
    })
}

// edit main template
var edit = function (Id) {
    var endPoint = $('#editTemplateAction').val();
    var titleTXT = $('#editTemplateAction').attr('titleTXT');
    var errorMSG = $('#editTemplateAction').attr('errorMSG');
    $.ajax({
        method: "get",
        url: endPoint,
        data: {
            id: Id
        }
    }).done(data => {
        $('#modal-trigger .modal-title').text('');
        $('#modal-trigger .modal-body').html('');
        $('#modal-trigger .modal-title').text(titleTXT);
        $('#modal-trigger .modal-body').html(data);
        reinitializeSelect2();

        $('#modal-trigger').modal('show');
    }).fail(function () {
        toastr.error("`" + errorMSG + "`");
    })
}

// edit main template
var changePosition = function (Id, orderNo, maxOrder, PageIndex) {
    var endPoint = $('#changePositionAction').val();
    var titleTXT = $('#changePositionAction').attr('titleTXT');
    var errorMSG = $('#changePositionAction').attr('errorMSG');
    $.ajax({
        method: "get",
        url: endPoint,
        data: {
            id: Id,
            orderNo: orderNo,
            maxOrder: maxOrder,
            PageIndex: PageIndex
        }
    }).done(data => {
        $('#modal-trigger .modal-title').text('');
        $('#modal-trigger .modal-body').html('');
        $('#modal-trigger .modal-title').text(titleTXT);
        $('#modal-trigger .modal-body').html(data);
        reinitializeSelect2();

        $('#modal-trigger').modal('show');
    }).fail(function () {
        toastr.error("`" + errorMSG + "`");
    })
}


// form functions

var OnBegin = function () {
    $('form button[type="submit"]').attr('disabled', true).text('Wait Sending');
    reinitializeSelect2();

}
var OnFailure = function (response) {
    $('form button[type="submit"]').attr('disabled', false).text('Submit');
    toastr.error(response);
    reinitializeSelect2();

}

var OnSuccess = function (response) {
    $('form button[type="submit"]').attr('disabled', false).text('Submit');
    if (response.isSucceeded) {
        toastr.success(response.message);
        var formType = $('#formType').val();
        if (formType == "edit") {
            var id = $('#Id').val();
            LoadMainInfo(id);
        } else if (formType == "form-day") {
            LoadDays();
        }
        $('#modal-trigger .modal-title').text('');
        $('#modal-trigger .modal-body').html('');
        $('#modal-trigger').modal('hide');
    } else {
        toastr.error(response.message);

        $('#modal-trigger .ErrorDiv').html(response.message);

        $('#modal-trigger .ErrorDiv').show();
    }
}
var deleteTemplate = function (e) {
    var confirmButtonTextMSG = $('#deleteComponentAction').attr('confirmButtonTextMSG');
    var textDeleteMSG = $('#deleteComponentAction').attr('textDeleteMSG');
    var titleDeleteMSG = $('#deleteComponentAction').attr('titleDeleteMSG');
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
            document.getElementById("deleteForm").submit();
        } else {
            return false;
        }
    })
}
var LoadDays = function () {
    var endPoint = $('#daysEndPoint').val();
    $.ajax({
        method: "get",
        url: endPoint
    }).done(data => {
        $('#DaysDiv').html('');
        $('#DaysDiv').html(data);
    })
}