$(() => {
    reinitializeSelect2();
    var filterAny = $('#filterAny').val();
    if (filterAny == false) {
        SetTableSort("MyTable", 2, 4);
    }
})
function SortTable() {

    var SortColumn = $('.ColumnDropdown').val();
    var SortDir = $('.DirectionDropdown').val();

    var LinkUrl = $('#GetListViewAsJsonUrl').val();

    var count = $('#FilterQueryStringDictionaryCount').val();

    if (count > 0) {
        jQueryAjaxGetUrl(LinkUrl + '&SortColumn=' + SortColumn + '&SortDirection=' + SortDir);

    }
    else {
        jQueryAjaxGetUrl(LinkUrl + '?SortColumn=' + SortColumn + '&SortDirection=' + SortDir);
    }
}
function updateOrder(id, orderno, PageIndex) {
    var Id = id;
    var OrderNo = orderno;
    var RecordUpdated = {};
    RecordUpdated.Id = Id;
    RecordUpdated.OrderNo = OrderNo;
    RecordUpdated.MaxOrderNo = $('#TotalItemCount').val();
    var ChangePostionUrl = $('#ChangePostionUrl').val();
    jQueryAjaxPostDto(ChangePostionUrl + "?id=" + id + "&PageIndex=" + PageIndex, RecordUpdated);
}
