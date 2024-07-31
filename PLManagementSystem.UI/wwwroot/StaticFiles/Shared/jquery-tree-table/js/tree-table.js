
function flatListToTree(items) {
    const getChild = (item, level, allLevel) => {
        return items.filter(v => v.parentId === item.id)
            .map(v => {
                const temp = {
                    ...v,
                    level,
                    children: getChild(v, level + 1, level === 0 ? v.id : `${allLevel}_${v.id}`),
                    partLevel: level === 0 ? v.id : `${v.parentId}_${v.id}`,
                    ...(level === 0 ? {
                        allLevel: v.id
                    } : {
                        allLevel: [allLevel, v.id].join('_')
                    }),
                };
                return [temp].concat(...temp.children);
            }
            );
    };

    return [].concat(...getChild({ id: undefined }, 0, undefined))
};

$(document.body).delegate('.expand', 'click', function () {
    var level = $(this).attr('data-level');
    var partLevel = $(this).attr('data-part-level');
    var allLevel = $(this).attr('data-all-level');
    var isOpen = $(this).attr('data-is-open');
    var trsDiv = $('.tree-table').find('tbody tr');
    var trsArray = $(trsDiv);
    if (isOpen === '1') {
        for (var i = 0; i < trsArray.length; i++) {
            var tempTr = $(trsArray[i]);
            var trLevel = tempTr.attr('data-level');
            var trPartLevel = tempTr.attr('data-part-level');
            var trAllLevel = tempTr.attr('data-all-level');
            var contain = trAllLevel.split('_')[Number(level)]; // 通过循环出来的tr的all_level获取选中等级的id
            var curr = partLevel.split('_'); // 通过获取选中的part_level的最后一个元素获取选中等级的id
            // 判断是否相等，
            if (contain && contain === curr[curr.length - 1] && partLevel !== trPartLevel) {
                tempTr.removeClass('tree-table-show');
                tempTr.addClass('hidden');
            }
        }
        $(this).text('+');
        $(this).attr('data-is-open', '0');
    } else {
        for (var i = 0; i < trsArray.length; i++) {
            var tempTr = $(trsArray[i]);
            var trLevel = tempTr.attr('data-level');
            var trPartLevel = tempTr.attr('data-part-level');
            var trAllLevel = tempTr.attr('data-all-level');
            var contain = trAllLevel.split('_')[Number(level)]; // 通过循环出来的tr的all_level获取选中等级的id
            var curr = partLevel.split('_'); // 通过获取选中的part_level的最后一个元素获取选中等级的id
            // 判断是否相等，
            if (contain && contain === curr[curr.length - 1] && Number(trLevel) > (Number(level))) {
                var span = $(tempTr.children()[0].children[Number(trLevel)]);
                var isOpen = $(span).attr('data-is-open');
                var childrenCount = $(span).attr('data-count');
                tempTr.removeClass('hidden');
                tempTr.addClass('tree-table-show');
                // pLevel != -1 并且有子级的情况下，判断pLevel的开关状态，关闭则不展开其下级元素
                if (isOpen && isOpen === '0' && Number(childrenCount) > 0) { // 下级折叠状态
                    i = i + Number(childrenCount);
                } else {
                    if (isOpen === '1') {
                        $(span).attr('data-is-open', '1');
                        $(span).text('-');
                        tempTr.removeClass('hidden');
                        tempTr.addClass('tree-table-show');
                    }
                }
            }
        }
        $(this).text('-');
        $(this).attr('data-is-open', '1');
    }
});

function countChildren(node) {
    var sum = 0,
        children = node && node.length ? node : node.children,
        i = children && children.length;

    if (!i) {
        sum = 0;
    } else {
        while (--i >= 0) {
            if (node && node.length) {
                sum++;
                countChildren(children[i]);
            } else {
                sum += countChildren(children[i]);
            }
        }
    }
    return sum;
}

function createRows() {
    $('#table-tree').html('');
    var fragments = document.createDocumentFragment();
    var opts = flatListToTree(items);
    for (var i = 0; i < opts.length; i++) {
        var item = opts[i];
        var trEle = document.createElement('tr');
        $(trEle).attr('data-part-level', item.partLevel);
        $(trEle).attr('data-all-level', item.allLevel);
        $(trEle).attr('data-level', item.level);
        $(trEle).attr('data-count', countChildren(item));
        var tdEle1 = document.createElement('td');
        for (var j = 0; j <= item.level; j++) {
            var spanEle = document.createElement('span');
            $(spanEle).addClass('tree-table-space-block');
            $(spanEle).attr('data-part-level', item.partLevel);
            $(spanEle).attr('data-all-level', item.allLevel);
            $(spanEle).attr('data-level', item.level);
            var iEle = document.createElement('i');
            if (j === item.level) {
                if (item.children && item.children.length > 0) {
                    $(spanEle).addClass('btn-toggle expand');
                    $(spanEle).attr('data-is-open', '1');
                    $(spanEle).attr('data-count', countChildren(item));
                    $(spanEle).text('-');
                } else {
                    $(spanEle).addClass('last-block');
                    $(spanEle).append(iEle);
                }
            } else {
                $(spanEle).append(iEle);
            }
            $(tdEle1).append(spanEle);
        }
        var spanEle2 = document.createElement('span');
        $(spanEle2).addClass('tree-table-td-content');
        $(spanEle2).text(item.id);
        $(tdEle1).append(spanEle2);
        $(trEle).append(tdEle1);
        for (const [key, value] of Object.entries(item)) {
            if (key != 'id' && key != 'level' && key != 'children' && key != 'parentId' && key != 'allLevel' && key != 'partLevel') {
                var tdEle = document.createElement('td');
                $(tdEle).css('width', '200px');
                $(tdEle).css('text-align', 'center');
                var spanTd = document.createElement('span');
                $(spanTd).addClass('tree-table-td-content');
                $(spanTd).append(value);
                $(tdEle).append(spanTd);
                $(trEle).append(tdEle);

            }
        }
        $(fragments).append(trEle);
    }
    $('#table-tree').append(fragments);
}