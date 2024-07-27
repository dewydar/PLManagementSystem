
showInPopup = (url, title) => {

    $.ajax({

        type: 'GET',

        url: url,

        success: function (res) {

            //if (res.hasOwnProperty('isValid') && res.isValid === false) {we2

            //    return;

            //}

            $('#form-modal .modal-body').html(res);

            $('#form-modal .modal-title').html(title);
            reinitializeSelect2();

            $('#form-modal').modal('show');

        }

    })

    return false;

}

showInPopupform = (form, title) => {

    try {

        $.ajax({

            type: 'GET',

            url: form.action,

            success: function (res) {

                //debugger;

                ////if (res.hasOwnProperty('isValid') && res.isValid === false) {

                ////    notyf.error({ message: res.errorMessage });

                ////    return;

                ////}

                //console.log(res);

                $('#form-modal .modal-body').html(res);

                $('#form-modal .modal-title').html(title);
                reinitializeSelect2();

                $('#form-modal').modal('show');

            }

        })

    } catch (ex) {

        console.log(ex)

    }

    //prevent default form submit event

    return false;

}

function HideModel() {

    $('#form-modal').modal('hide');
    reinitializeSelect2();

}

function ModalFormSubmit() {

    jQueryAjaxPost(document.getElementById('MyModalForm'));
    reinitializeSelect2();

}

jQueryAjaxGetUrl = (url) => {

    try {

        $.ajax({

            type: 'GET',

            url: url,

            contentType: false,

            processData: false,

            success: function (res) {


                if (res.isValid == true) {

                    $('#view-all').html(res.html)

                    SetTableSort("MyTable", 2, 4);

                    DataTableInit();

                    $('#form-modal .modal-body').html('');

                    $('#form-modal .modal-title').html('');

                    $('#form-modal').modal('hide');

                }

                else {

                    //$('#form-modal .modal-body').html(res.html);

                    console.log(res.message);

                    $('#form-modal .ErrorDiv').html(res.message);

                    $('#form-modal .ErrorDiv').show();

                }

                reinitializeSelect2();

            },

            error: function (err) {
                toastr.error(err);
                console.log(err)

            }

        })

    } catch (ex) {

        console.log(ex)

    }

    //to prevent default form submit event

    return false;

}


jQueryAjaxPost = form => {

    try {

        $.ajax({

            type: 'POST',

            url: form.action,

            data: new FormData(form),

            contentType: false,

            processData: false,

            success: function (res) {

                //debugger;

                if (res.isValid == true) {


                    //window.location.replace(res.newUrl);

                    $('#view-all').html(res.html)

                    $('#form-modal .modal-body').html('');

                    $('#form-modal .modal-title').html('');

                    $('#form-modal').modal('hide');

                    SetTableSort("MyTable", 2, 4);
                    if (res.message != undefined && res.message != null && res.message != '') {
                        toastr.success(res.message);
                    }

                    DataTableInit();

                }

                else {

                    //$('#form-modal .modal-body').html(res.html);

                    $('#form-modal .ErrorDiv').html(res.errorMessage);

                    $('#form-modal .ErrorDiv').show();

                }

                reinitializeSelect2();

            },

            error: function (err) {
                toastr.error(err);
                console.log(err)

            }

        })

        //to prevent default form submit event

        return false;

    } catch (ex) {

        console.log(ex)

    }

}

jQueryAjaxDelete = form => {

    var confirmButtonTextMSG = $('#deleteAction').attr('confirmButtonTextMSG');

    var textDeleteMSG = $('#deleteAction').attr('textDeleteMSG');

    var titleDeleteMSG = $('#deleteAction').attr('titleDeleteMSG');

    if (confirmButtonTextMSG == undefined || confirmButtonTextMSG == null || confirmButtonTextMSG == '') {

        confirmButtonTextMSG = "Confirm";

    }

    if (textDeleteMSG == undefined || textDeleteMSG == null || textDeleteMSG == '') {

        textDeleteMSG = "Once deleted, you cannot undo it again!";

    }

    if (titleDeleteMSG == undefined || titleDeleteMSG == null || titleDeleteMSG == '') {

        titleDeleteMSG = "Are you sure ?";

    }

    Swal.fire({

        title: titleDeleteMSG,

        text: textDeleteMSG,

        icon: "warning",

        showCancelButton: true,

        confirmButtonColor: "#3085d6",

        cancelButtonColor: "#d33",

        confirmButtonText: confirmButtonTextMSG

    }).then((result) => {

        if (result.isConfirmed) {

            try {

                $.ajax({

                    type: 'POST',

                    url: form.action,

                    data: new FormData(form),

                    contentType: false,

                    processData: false,

                    success: function (res) {
                        if (res.isValid) {

                            $('#view-all').html(res.html);

                            SetTableSort("MyTable", 2, 4);
                            if (res.message != undefined && res.message != null && res.message != '') {
                                toastr.success(res.message);
                            }
                            DataTableInit();
                            return true;

                        }


                            reinitializeSelect2();
                    },

                    error: function (err) {

                        console.log(err)

                        toastr.error(err);

                        return false;

                    }

                })

            } catch (ex) {

                toastr.error(ex);

                console.log(ex)

            }

        }

    });

    //prevent default form submit event

    return false;

}

function reinitializeSelect2() {

    // Remove existing Select2

    $('.select2').each(function () {

        if ($(this).data('select2')) { // Check if Select2 is initialized on this element

            $(this).select2('destroy'); // Destroy Select2 if initialized

        }

    });

    // Reinitialize Select2

    $('.select2').select2();

}

function jQueryAjaxPostDto(url, RecordUpdated) {

    try {
        

        $.ajax({

            type: 'POST',

            url: url,

            data: { RecordUpdated: RecordUpdated },

            success: function (res) {

                if (res.isValid == true) {


                    //window.location.replace(res.newUrl);

                    $('#view-all').html(res.html)

                    SetTableSort("MyTable", 2, 4);

                    DataTableInit();
                    if (res.message != undefined && res.message != null && res.message != '') {
                        toastr.success(res.message);
                    }
                    $('#form-modal .modal-body').html('');

                    $('#form-modal .modal-title').html('');

                    $('#form-modal').modal('hide');

                }

                else {

                    //$('#form-modal .modal-body').html(res.html);

                    $('#form-modal .ErrorDiv').html(res.errorMessage);

                    $('#form-modal .ErrorDiv').show();

                }

                reinitializeSelect2();

            },

            error: function (err) {

                console.log(err)
                toastr.error(err);

            }

        })

        //to prevent default form submit event

        return false;

    } catch (ex) {

        console.log(ex)

    }

}

function DataTableInit() {

    $("#MyTable").DataTable({

        responsive: true,

        searching: false,

        ordering: false,

        paging: false,

        info: false,

    });
    reinitializeSelect2();

}

var jQueryAjaxDeleteUrl = function (url) {
    var confirmButtonTextMSG = $('#deleteAction').attr('confirmButtonTextMSG');
    var textDeleteMSG = $('#deleteAction').attr('textDeleteMSG');
    var titleDeleteMSG = $('#deleteAction').attr('titleDeleteMSG');
    if (confirmButtonTextMSG == undefined || confirmButtonTextMSG == null || confirmButtonTextMSG == '') {
        confirmButtonTextMSG = "Confirm";
    }
    if (textDeleteMSG == undefined || textDeleteMSG == null || textDeleteMSG == '') {
        textDeleteMSG = "Once deleted, you cannot undo it again!";
    }
    if (titleDeleteMSG == undefined || titleDeleteMSG == null || titleDeleteMSG == '') {
        titleDeleteMSG = "Are you sure ?";
    }
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
            $.ajax({
                type: 'Delete',
                url: url,
                success: function (res) {
                    if (res.isValid) {

                        $('#view-all').html(res.html);

                        SetTableSort("MyTable", 2, 4);

                        DataTableInit();
                        if (res.message != undefined && res.message != null && res.message != '') {
                            toastr.success(res.message);
                        }
                        return true;

                    }
                    else {
                        toastr.error(res.message);
                    }
                    reinitializeSelect2();

                }
            });
        }
    })
}