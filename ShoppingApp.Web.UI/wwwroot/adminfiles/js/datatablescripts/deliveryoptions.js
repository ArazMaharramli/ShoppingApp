"use strict";
// Class definition

var KTDefaultDatatableDemo = function () {
    // Private functions

    // basic demo
    var demo = function () {

        var options = {
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/admin/deliveryoption/getpageddeliveryoptions/'
                    },
                },
                pageSize: 10, // display 20 records per page
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
            },
            layout: {
                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
                height: 550, // datatable's body's fixed height
                footer: false, // display/hide footer
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },

            // columns definition
            columns: [
                {
                    field: 'globalId',
                    title: '#',
                    sortable: false,
                    width: 30,
                    type: 'number',
                    selector: { class: 'kt-checkbox--solid' },
                    textAlign: 'center',
                },
                {
                    field: 'uniqueName',
                    title: 'Name',
                }, {
                    field: 'addedDate',
                    title: 'Added Date',
                    type: 'date',
                    format: 'MM/DD/YYYY',
                }, {
                    field: 'status',
                    title: 'Status',
                    template: function (row) {
                        var status = {
                            "Active": { 'title': 'Active', 'class': 'label-light-success' },
                            "Deleted": { 'title': 'Deleted', 'class': 'label-light-danger' },
                            "Hidden": { 'title': 'Hidden', 'class': 'label-light-info' },
                        };
                        return '<span class="label ' + status[row.status].class + ' label-inline font-weight-bold label-lg">' + status[row.status].title + '</span>';
                    },
                }, {
                    field: 'Actions',
                    title: 'Actions',
                    sortable: false,
                    width: 125,
                    overflow: 'visible',
                    autoHide: false,
                    template: function (row) {
                        return '\<a href="/admin/deliveryoption/edit/' + row.globalId + '"class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a id="'+ row.globalId + '" class="btn btn-sm btn-clean btn-icon deletebtn" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\
						';
                    },
                }],

        };

        var datatable = $('#kt_datatable').KTDatatable(options);




        // both methods are supported
        // datatable.methodName(args); or $(datatable).KTDatatable(methodName, args);

        $('#kt_datatable_destroy').on('click', function () {
            // datatable.destroy();
            $('#kt_datatable').KTDatatable('destroy');
        });

        $('#kt_datatable_init').on('click', function () {
            datatable = $('#kt_datatable').KTDatatable(options);
        });

        $('#kt_datatable_reload').on('click', function () {
            // datatable.reload();
            $('#kt_datatable').KTDatatable('reload');
        });

        $('#kt_datatable_sort_asc').on('click', function () {
            datatable.sort('Status', 'asc');
        });

        $('#kt_datatable_sort_desc').on('click', function () {
            datatable.sort('Status', 'desc');
        });

        // get checked record and get value by column name

        $('#setStatusActive').on('click', function () {

            if (datatable.nodes().length > 0) {
                // get column by field name and get the column nodes

                var ids = datatable.rows('.datatable-row-active').
                    nodes().
                    find('.checkbox > [type="checkbox"]').
                    map(function (i, chk) {
                        return $(chk).val();
                    }).get();

                let data = {
                    globalIds: ids,
                    status: "Active"
                };
                console.log(data);
                $.ajax({
                    url: "admin/deliveryoption/UpdateStatusRange/",
                    method: "Post",
                    data: data,
                    success: function (response) {
                        Swal.fire({
                            title: "Updated",
                            text: response.Message,
                            icon: "success",
                            confirmButtonText: "OK"
                        });

                        $('#kt_datatable').KTDatatable('reload');
                        $('#kt_datatable_group_action_form').collapse('hide');
                    },
                    error: function (request, status, error) {
                        Swal.fire({
                            title: "Something went wrong!",
                            text: response.Message,
                            icon: "error",
                            confirmButtonText: "OK"
                        });
                    },
                });
            }
        });

        $('#setStatusHide').on('click', function () {

            if (datatable.nodes().length > 0) {
                // get column by field name and get the column nodes

                var ids = datatable.rows('.datatable-row-active').
                    nodes().
                    find('.checkbox > [type="checkbox"]').
                    map(function (i, chk) {
                        return $(chk).val();
                    }).get();

                let data = {
                    globalIds: ids,
                    status: "Hidden"
                };
                console.log(data);
                $.ajax({
                    url: "admin/deliveryoption/UpdateStatusRange/",
                    method: "Post",
                    data: data,
                    success: function (response) {
                        Swal.fire({
                            title: "Updated",
                            text: response.Message,
                            icon: "success",
                            confirmButtonText: "OK"
                        });

                        $('#kt_datatable').KTDatatable('reload');
                        $('#kt_datatable_group_action_form').collapse('hide');
                    },
                    error: function (request, status, error) {
                        Swal.fire({
                            title: "Something went wrong!",
                            text: response.Message,
                            icon: "error",
                            confirmButtonText: "OK"
                        });
                    },
                });
            }
        });

        $('#deleteSelectedBtn').on('click', function () {

            if (datatable.nodes().length > 0) {
                // get column by field name and get the column nodes

                var ids = datatable.rows('.datatable-row-active').
                    nodes().
                    find('.checkbox > [type="checkbox"]').
                    map(function (i, chk) {
                        return $(chk).val();
                    }).get();

                let data = {
                    globalIds: ids
                };
                console.log(data);
                $.ajax({
                    url: "admin/deliveryoption/deleterange/",
                    method: "Post",
                    data: data,
                    success: function (response) {
                        Swal.fire({
                            title: "Deleted",
                            text: response.Message,
                            icon: "success",
                            confirmButtonText: "OK"
                        });

                        $('#kt_datatable').KTDatatable('reload');
                        $('#kt_datatable_group_action_form').collapse('hide');
                    },
                    error: function (request, status, error) {
                        Swal.fire({
                            title: "Something went wrong!",
                            text: response.Message,
                            icon: "error",
                            confirmButtonText: "OK"
                        });
                    },
                });

            }
        });

        datatable.on('click', '.deletebtn', function (e) {
            var id = this.id;
            console.log(this);

            let data = {
                globalId: id
            };
            console.log(data);
            $.ajax({
                url: "admin/deliveryoption/delete/",
                method: "Post",
                data: data,
                success: function (response) {
                    Swal.fire({
                        title: "Deleted",
                        text: response.Message,
                        icon: "success",
                        confirmButtonText: "OK"
                    });

                    $('#kt_datatable').KTDatatable('reload');
                    $('#kt_datatable_group_action_form').collapse('hide');
                },
                error: function (request, status, error) {
                    Swal.fire({
                        title: "Something went wrong!",
                        text: response.Message,
                        icon: "error",
                        confirmButtonText: "OK"
                    });
                },
            });

        });

        datatable.on(
            'datatable-on-check datatable-on-uncheck',
            function (e) {
                var checkedNodes = datatable.rows('.datatable-row-active').nodes();
                var count = checkedNodes.length;
                $('#kt_datatable_selected_records').html(count);
                if (count > 0) {
                    $('#kt_datatable_group_action_form').collapse('show');
                } else {
                    $('#kt_datatable_group_action_form').collapse('hide');
                }
            });

        $('#kt_datatable_remove_row').on('click', function () {
            datatable.rows('.datatable-row-active').remove();
        });

        $('#kt_datatable_search_status').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Status');
        });

        $('#kt_datatable_search_type').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Type');
        });

        $('#kt_datatable_search_status, #kt_datatable_search_type').selectpicker();

    };

    return {
        // public functions
        init: function () {
            demo();
        },
    };
}();

jQuery(document).ready(function () {
    KTDefaultDatatableDemo.init();
});
