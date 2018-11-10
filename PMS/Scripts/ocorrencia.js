$(function () {
    $("body").on('click', '.bt-inserir-foto', function (e) {
        e.preventDefault();
        $("#FotoFile").click();
    });

    $("body").on('change', '#FotoFile', function () {
        $("#upload-imagem").ajaxSubmit({
            success: function (json) {
                $(".imagem-upload").attr('src', json);
                $("#foto").val(json);
            }
        });
    });
})