// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQuery('document').ready(function () {
    jQuery('table').DataTable({
        aoColumnDefs: [
            { "aTargets": [0], "bSortable": true },
            { "aTargets": [2], "asSorting": ["asc"], "bSortable": true },
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/French.json"
        }
    });
});
