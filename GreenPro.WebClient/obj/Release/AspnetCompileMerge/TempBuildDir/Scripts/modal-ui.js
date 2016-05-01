$(function () {

    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');

            bindForm(this);
            $("#myModal").on('shown.bs.modal', function () {

                $("#CityId").prop("disabled", true);
                $("#StateId").change(function () {
                    $("#CityId").empty();
                    var sid = $("#StateId").val();
                    var urls = window.location.href + "/Citylist";
                    if (sid == "-1")
                        $("#CityId").prop("disabled", true);
                    else
                        $("#CityId").prop("disabled", false);
                    $.ajax({
                        type: 'POST',
                        url: urls,
                        dataType: 'json',
                        data: { id: sid },
                        success: function (selectListItemList) {
                            $.each(selectListItemList, function (i, state) {
                                $("#CityId").append('<option value="'
                                 + state.Value + '">'
                                 + state.Text + '</option>');
                            });
                        },
                        error: function (ex) {
                            alert('Failed to retrieve states.' + ex);
                        }
                    });
                    return false;
                })

            });
        });
        return false;
    });


});



function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#progress').hide();
                    location.reload();
                } else {
                    $('#progress').hide();
                    $('#myModalContent').html(result);

                    bindForm();
                }
            }
        });
        return false;
    });
}