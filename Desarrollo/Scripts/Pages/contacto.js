let tabelaDelContactos;
let modalDelContactos;

$(() => {
    let idContacto = 0;
    alGenerarTabelaDelContactos();

    $('#btnAnadirNuevo').click(function () {
        $('#contactoModal').data({
            'accion': 'crear',
            'id-contacto': '0'
        });
        $('#contactoModal').modal();
    });

    $('#btnGuardarContacto').click(function () {
        let contacto = {
            Id: idContacto,
            Nombre: $('#txtNombre').val(),
            Departamento: $('#txtDepartamento').val(),
            EMail: $('#contactoModal #txtEMail').val(),
            Telefono: $('#contactoModal #txtTelefono').val(),
            ProveedorId: $('#drpProveedor :selected').val()
        }

        if (!validateForm(contacto))
            return;

        crearActualizarContacto(contacto);
    });

    $('#contactoModal').on('show.bs.modal', function () {
        inicializarCasillasDeVerificacion();
    }).on('shown.bs.modal', function () {
        let action = $(this).data('accion');
        idContacto = $(this).data('id-contacto');

        if (action == 'crear') {
            preencherProveedores();
            $('#lblTituloModal').text('Añadir nuevo contacto');
            formaClara();
        } else {
            $('#lblTituloModal').text('Actualizar contacto');
            mostrarDatosEnModal(idContacto);
        }
    }).on('hide.bs.modal', function () {

    });
});

function validateForm(obj) {
    if (obj.Nombre == '')
        return notificacionGenerica('¡El campo de <b>nombre</b> debe ser informado!', 'warning', 1500);

    if (obj.Departamento == '')
        return notificacionGenerica('¡El campo de <b>departamento</b> debe ser informado!', 'warning', 1500);

    if (obj.Telefono == '')
        return notificacionGenerica('¡El campo de <b>telefono</b> debe ser informado!', 'warning', 1500);

    if (obj.ProveedorId == '')
        return notificacionGenerica('¡El campo de <b>proveedor</b> debe ser informado!', 'warning', 1500);

    if (obj.EMail == '')
        return notificacionGenerica('¡El campo de <b>e-mail</b> debe ser informado!', 'warning', 1500);

    return true;
}

function preencherProveedores(idProveedor = 0) {
    obtenerProveedores(function (dados) {
        let objetoProveedores = $('#drpProveedor');
        objetoProveedores.empty();

        $(dados).each(function (a, b) {
            objetoProveedores.append(`<option value="${b.Id}" ${b.Id == idProveedor ? "selected" : ""}>${b.Nombre}</option>`);
        });
    });
}

function inicializarCasillasDeVerificacion() {
    $("input[type=checkbox]")
        .bootstrapSwitch('state', false);
}

function mostrarDatosEnModal(idContacto) {
    obtenerContactos(function (dados) {
        $('#contactoModal #txtNombre').val(dados.Nombre);
        $('#contactoModal #txtDepartamento').val(dados.Departamento);
        $('#contactoModal #txtEMail').val(dados.EMail);
        $('#contactoModal #txtTelefono').val(dados.Telefono);

        preencherProveedores(dados.ProveedorId);
    }, idContacto);
}

function formaClara() {
    $('#contactoModal .form-control').val('');
}

function alGenerarTabelaDelContactos() {
    obtenerContactos(function (dados) {
        if (dados != undefined)
            generarTabelaDelContactos(dados);
    });
}

function generarTabelaDelContactos(dados) {
    tabelaDelContactos = $('#tabelaDelContactos').DataTable({
        responsive: true,
        destroy: true,
        data: dados,
        columns: [
            {
                data: 'Id',
                title: 'Id'
            },
            {
                data: 'Nombre',
                title: 'Nombre'
            },
            {
                data: 'Departamento',
                title: 'Departamento'
            },
            {
                data: 'EMail',
                title: 'EMail'
            },
            {
                title: 'Opciones',
                width: '125px',
                render: function (a, b, c) {
                    let acciones = "<div class='btn-group'>" +
                        `<a href='#' class='btn btn-danger ${c.Id} eliminar-contacto'><i class='glyphicon glyphicon-thumbs-up ${c.Id}'></i></a>` +
                        "<a href='#' class='btn btn-default'><i class='glyphicon glyphicon-thumbs-down'></i></a>" +
                        "</div>";

                    let resultado = '<div style="font-size: 14pt;">' +
                        `<a href="#" style="margin-right: 8px;" title="Editar"><i data-editar="${c.Id}" class="glyphicon glyphicon-edit"></i></a>` +
                        '<a href="#" style="margin-right: 8px;" title="¿Confirma lá exclusión?" role="button" data-toggle="popover" data-trigger="focus" ' +
                        `title="¿Confirma lá exclusión?" data-html="true" data-content="${acciones}" data-eliminar='${c.Id}'><i class="glyphicon glyphicon-trash"></i></a>` +
                        '</div>';

                    return resultado;
                }
            }
        ],
        "language": {
            "url": "/Scripts/DataTable/Lang/Spanish.json"
        }
    }).on('click', '[data-editar]', function (e) {
        e.preventDefault();

        let idContacto = $(this).data('editar');

        $('#contactoModal').data({
            'id-contacto': idContacto,
            'accion': 'editar'
        });
        $('#contactoModal').modal();
    }).on('click', '.eliminar-contacto', function (e) {
        e.preventDefault();

        let idContacto = $(this)[0].classList[2];
        let filaActual = $($(this).parents('td')).parents('tr')

        obtenerContactos(function (dados) {
            if (dados != undefined)
                eliminarContacto(function (resultado) {
                    if (resultado) {
                        $(filaActual).fadeOut(600, function () {
                            tabelaDelContactos
                                .row(filaActual)
                                .remove()
                                .draw();
                        });
                    }
                }, dados);
        }, idContacto);
    });

    tabelaDelContactos.on('draw', function () {
        $('[data-toggle="popover"]').popover()
    });
}

function eliminarContacto(callback, contacto) {
    $.ajax({
        url: `/Pages/Contacto.aspx/EliminarContacto`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{contacto: '" + JSON.stringify(contacto) + "'}",
        success: function (result) {
            if (result.d == 1) {
                notificacionGenerica(`El contacto se ha eliminado correctamente.`, 'success', 3000);
                callback(true);
            }
            else {
                notificacionGenerica(`No se pudo eliminar el contacto.`, 'danger', 3000);
                callback(false);
            }
        },
        failure: function (failure) {
            alert(failure.d);
        },
        error: function (data) {

        }
    }).done(function () { });
}

function crearActualizarContacto(contacto) {
    $.ajax({
        url: `/Pages/Contacto.aspx/${contacto.Id != 0 ? "ActualizarContacto" : "NuevoContacto"}`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{contacto: '" + JSON.stringify(contacto) + "'}",
        success: function (result) {
            if (result.d == 1) {
                $('#contactoModal').modal('hide');
                alGenerarTabelaDelContactos();
                notificacionGenerica(`El contacto se ha ${contacto.Id != 0 ? "actualizado" : "añadido"} correctamente.`, 'success', 3000);
            }
            else
                notificacionGenerica(`No se pudo ${contacto.Id != 0 ? "actualizar" : "añadir"} el contacto.`, 'danger', 3000);
        },
        failure: function (failure) {
            alert(failure.d);
        },
        error: function (data) {
            notificacionGenerica(`No se pudo ${contacto.Id != 0 ? "actualizar" : "añadir"} el contacto.`, 'danger', 3000);
        }
    }).done(function () { });
}

function obtenerContactos(callback, idContacto = 0) {
    let webMethod = idContacto != 0 ?
        `ObtenerContacto?id=${idContacto}` :
        'ObtenerContactos';

    $.ajax({
        url: `/Pages/Contacto.aspx/${webMethod}`,
        type: 'GET',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function () { },
        failure: function () { },
        error: function () { }
    }).done(function (dados) {
        callback(dados.d);
    });
}