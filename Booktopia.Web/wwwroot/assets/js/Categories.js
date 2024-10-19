//// "use strict";
//$(document).ready(function () {

//    $('.js-toggle-status').on('click', function () {
//        var btn = $(this);

//        bootbox.confirm({
//            message: 'Are you sure you need to toggle this item status?',
//            buttons: {
//                confirm: {
//                    label: 'Yes',
//                    className: 'btn-danger'
//                },
//                cancel: {
//                    label: 'No',
//                    className: 'btn-secondary'
//                }
//            },
//            callback: function (result) {
//                if (result) {
//                    $.post({
//                        url: 'Categories/ToggleStatus/' + btn.data('id'),
//                        data: {
//                            '__RequestVerificationToken': $('input[name = "__RequestVerificationToken"]').val()
//                        },
//                        success: function (LastUpdatedOn) {
//                            var row = btn.parents('tr');
//                            var status = row.find('.js-status');
//                            var newStatus = status.text().trim() === 'Deleted' ? 'Available' : 'Deleted';
//                            status.text(newStatus).toggleClass('badge-light-success badge-light-danger');
//                            row.find('.js-updated-on').html(LastUpdatedOn);
//                            row.addClass('animate__animated animate__flash');
//                            showSuccessMessage();
//                        },
//                        error: function () {
//                            showErrorMessage();
//                        }
//                    });
//                }
//            }
//        });


//    });
//});