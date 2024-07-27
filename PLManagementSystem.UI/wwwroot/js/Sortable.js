var SortTable = function (tableId, sortBackEndController, sortBackEndAction, titleNUmber) {
    var oldValue;
    $("#" + tableId).sortable({
        items: 'tbody>tr',
        dropOnEmpty: false,
        start: function (G, ui) {
            oldValue = ui.item[0].innerHTML
            ui.item.addClass("select");
            if (titleNUmber != undefined && titleNUmber != null && titleNUmber != '') {
                ui.item[0].innerHTML = '<div class="col-md-3"  style="width: 50 %; background-color:rgba(192,192,192,0.5);  width: auto !important;"><h4>' + ui.item[0].children[titleNUmber].textContent + '</h4></div>';
            }
        },
        update: function (event, ui) {
            var sortables = [];
            var sortBackEndURL = "/" + sortBackEndController + "/" + sortBackEndAction;
            ui.item[0].innerHTML = oldValue;
            $('#sortable_table_tbody').find(".singleInline").each(function () {
                var sortableId = $(this).attr("data-Id");
                sortables.push(sortableId);
            });
            $.ajax({
                url: sortBackEndURL,
                method: "POST",
                data: {
                    sortables: sortables
                }
            }).done(function () {
                LoadDataGrid();
            })
        },
        stop: function (G, ui) {
            ui.item.removeClass("select");
            ui.item[0].innerHTML = oldValue;
        }
    });
}
var oldHtmlValue;
function SetTableSort(tableid, titleNUmber, indexOfOrderNoForlastRow) {
    var SortColumn = $('.ColumnDropdown').val();
    var SortDir = $('.DirectionDropdown').val();
    if (SortColumn == "OrderNo" && SortDir == "asc") {
        $('.SortColume').css('cursor', 'grab');
        $("#" + tableid).sortable({
            items: 'tbody>tr:not(.ui-state-disabled)',
            cancel: ".ui-state-disabled",
            start: function (G, ui) {
                oldHtmlValue = ui.item[0].innerHTML
                ui.item[0].innerHTML = '<div class="col-md-3"  style="width: 50 %; background-color:rgba(192,192,192,0.5);  width: auto !important;"><h4>' + ui.item.attr("data-Name") + '</h4></div>';
            },
            update: function (G, ui) {
                ui.item.removeClass("select");

                var divs = document.getElementsByTagName("tbody")[0].children;
                var selectionDiv = ui.item[0];

                for (var i = 0; i < divs.length; i++) {
                    if (divs[i] == selectionDiv) {
                        var currentOrder = (i + 1) + ((parseInt(ui.item.attr("data-pageindex") - 1) * 10));
                        updateOrder(parseInt(ui.item.attr("data-id")), currentOrder, parseInt(ui.item.attr("data-pageindex")));
                    }
                }

                ui.item[0].innerHTML = oldHtmlValue;

            }
            ,
            stop: function (G, ui) {
                ui.item.removeClass("select");
                ui.item[0].innerHTML = oldHtmlValue;

            }
        }).disableSelection();

    }

}
var fixHelperModified = function (e, tr) {
    var $originals = tr.children();
    var $helper = tr.clone();
    $helper.children().each(function (index) {
        $(this).width($originals.eq(index).width())
    });
    return $helper;
};