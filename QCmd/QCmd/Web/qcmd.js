$(document).ready(function () {
    var availableTags = [
          "test",
          "super test",
          "ultra test",
          "test prime"
    ];
    $('#qcmdBox').autocomplete({
        source: availableTags,
        select: function (event, ui) {
            qcmd.Execute(ui.item.value);
        },
        selectFirst: true,
        autoSelect: true
    });

    $('#qcmdBox').keypress(function(e) {
        if (e.keyCode == 13)
            qcmd.Execute($('#qcmdBox').val());
    });
});


function focusQcmd() {
    $("#qcmdBox").focus();
}