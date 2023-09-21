$(document).ready(function (e) {
    let paginationForm = $('.pagination form');
    let pageNumInput = $('#PageNumber', paginationForm);
    let lastPageInput = $('#TotalPages', paginationForm);
    let categoryIdInput = $('#CategoryId', paginationForm)
    $('input', paginationForm).change(function (e) {
        paginationForm.submit();
    })
    $('.btn.first', paginationForm).click(function (e) {
        pageNumInput.val(1);
        pageNumInput.change();
    })
    $('.btn.prev', paginationForm).click(function (e) {
        num = pageNumInput.val();
        if (num > 1) {
            pageNumInput.val(--num);
            pageNumInput.change();
        }
    })
    $('.btn.next', paginationForm).click(function (e) {
        num = pageNumInput.val();
        if (num < lastPageInput.val()) {
            pageNumInput.val(++num);
            pageNumInput.change();
        }
    })
    $('.btn.last', paginationForm).click(function (e) {
        pageNumInput.val(lastPageInput.val());
        pageNumInput.change();
    })
    $('.select.category', paginationForm).change(function (e) {
        categoryIdInput.val($(e.target).val());
        categoryIdInput.change();
    })
})

document.addEventListener('DOMContentLoaded', function () {

    const fileInput = document.getElementById('filein');
    const filetext = document.getElementById('filetxt');
    fileInput.onchange = () => {
        filetxt.value = fileInput.files[0].name;
    }
    filetext.onclick = () => {
        fileInput.click();
    }
});
