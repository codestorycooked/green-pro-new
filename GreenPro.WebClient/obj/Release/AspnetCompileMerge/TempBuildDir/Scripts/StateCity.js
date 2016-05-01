
$(document).ready(function () {
    $("#CityId").prop("readonly", true);
    $("#StateId").change(function () {
        $("#CityId").empty();
        var sid = $("#StateId").val();
        if (sid == "-1")
            $("#CityId").prop("readonly", true);
        else
            $("#CityId").prop("readonly", false);
        $.ajax({
            type: 'POST',
            url: '@Url.Action("Citylist")',
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
