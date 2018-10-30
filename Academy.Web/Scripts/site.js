$(function() {
   // $('.datePicker').datetimepicker();

    //$('.datePick').each(function () {
    //    var a = $(this).datepicker({
    //        dateFormat: "dd/ww/yy",
    //        defaultDate: new Date($(this).val())
    //    });
    //});

    $('#acceptApplicant').attr("disbled", true);
    $('#rejectApplicant').attr("disbled", true);









    ///////////////*****************New Style****************/////////////////////////////
    $("#foto").click(function() {
        $("#btnImagemPaciente").trigger('click');
    });

    $("#btnPrint").click(function() {
        if (imprimir()) {
            $(".no-print-required").each(function() {
                if ($(this).hasClass('no-print-required'))
                    $(this).removeClass('no-print-required');
            });
            window.print();
        } else {
            $("#msgValidaForm").removeClass("d-none");
            /* Após 4 segundo a mensagem desaparece com a classe invisible sendo novamente adicionada */
            setTimeout(function() {
                    $("#msgValidaForm").addClass("d-none");
                },
                4000);
        }
    });

    function imprimir() {
        var print = true;

        if ($("#paciente").val() === '')
            print = false;
        else if ($("#dtNascimento").val() === '')
            print = false;
        else if ($("#nrAtendimento").val() === '')
            print = false;
        else if ($("#medicoSolicitante").val() === '')
            print = false;
        else if ($("#crm").val() === '')
            print = false;
        else if ($("#diagnostico").val() === '')
            print = false;
        else if ($("#procedimento").val() === '')
            print = false;
        else if ($("#doenca_comprovada").val() === '')
            print = false;
        else if ($("#doenca_provavel").val() === '')
            print = false;
        else if ($("#medicamento").val() === '')
            print = false;
        else if ($("#posologia").val() === '')
            print = false;
        else if ($("#dias").val() === '')
            print = false;
        else if ($("#caracteristicas").val() === '')
            print = false;
        else if ($("#tratamentos_previos").val() === '')
            print = false;


        return print;
    }



    $("#doenca_comprovada").change(function () {
        $("#doenca_provavel").val($("#doenca_provavel option:first").val());
        $("#doenca_provavel").parent().parent().hide();

        if ($(this).prop('selectedIndex') === 0)
            $("#doenca_provavel").parent().parent().show();

    })

    $("#doenca_provavel").change(function () {
        $("#doenca_comprovada").val($("#doenca_comprovada option:first").val());
        $("#doenca_comprovada").parent().parent().hide();

        if ($(this).prop('selectedIndex') == 0)
            $("#doenca_comprovada").parent().parent().show();

    })

    autosize(document.getElementById("justificativa"));
    autosize(document.getElementById("caracteristicas"));
    autosize(document.getElementById("tratamentos_previos"));
});


