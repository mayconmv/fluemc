let tabelaDelProveedores;
let modalDelProveedores;

$(() => {
    let idProveedor = 0;
    alGenerarTabelaDelProveedores();

    $('#btnAnadirNuevo').click(function () {
        $('#proveedorModal').data({
            'accion': 'crear',
            'id-proveedor': '0'
        });
        $('#proveedorModal').modal();
    });

    $('#btnGuardarProveedor').click(function () {
        let proveedor = {
            Id: idProveedor,
            Nombre: $('#txtNombre').val(),
            Direccion: $('#txtDireccion').val(),
            RefProveedor: $('#proveedorModal #txtRefProveedor').val()
        }

        if (!validateForm(proveedor))
            return;

        crearActualizarProveedor(proveedor);
    });

    $('#proveedorModal').on('show.bs.modal', function () {
        inicializarCasillasDeVerificacion();
    }).on('shown.bs.modal', function () {
        let action = $(this).data('accion');
        idProveedor = $(this).data('id-proveedor');

        if (action == 'crear') {
            $('#lblTituloModal').text('Añadir nuevo proveedor');
            formaClara();
        } else {
            $('#lblTituloModal').text('Actualizar proveedor');
            mostrarDatosEnModal(idProveedor);
        }
    }).on('hide.bs.modal', function () {

    });
});

function validateForm(obj) {
    if (obj.Nombre == '')
        return notificacionGenerica('¡El campo de <b>nombre</b> debe ser informado!', 'warning', 1500);

    if (obj.Direccion == '')
        return notificacionGenerica('¡El campo de <b>direccion</b> debe ser informado!', 'warning', 1500);

    if (obj.RefProveedor == '')
        return notificacionGenerica('¡El campo de <b>ref proveedor</b> debe ser informado!', 'warning', 1500);

    return true;
}

function inicializarCasillasDeVerificacion() {
    $("input[type=checkbox]")
        .bootstrapSwitch('state', false);
}

function mostrarDatosEnModal(idProveedor) {
    obtenerProveedores(function (dados) {
        $('#proveedorModal #txtNombre').val(dados.Nombre);
        $('#proveedorModal #txtDireccion').val(dados.Direccion);
        $('#proveedorModal #txtRefProveedor').val(dados.RefProveedor);
    }, idProveedor);
}

function formaClara() {
    $('#proveedorModal .form-control').val('');
}

function alGenerarTabelaDelProveedores() {
    obtenerProveedores(function (dados) {
        if (dados != undefined)
            generarTabelaDelProveedores(dados);
    });
}

function generarTabelaDelProveedores(dados) {
    tabelaDelProveedores = $('#tabelaDelProveedores').DataTable({
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
                data: 'Direccion',
                title: 'Dirección'
            },
            {
                data: 'RefProveedor',
                title: 'Ref Proveedor',
                width: '125px'
            },
            {
                title: 'Opciones',
                width: '125px',
                render: function (a, b, c) {
                    let acciones = "<div class='btn-group'>" +
                        `<a href='#' class='btn btn-danger ${c.Id} eliminar-proveedor'><i class='glyphicon glyphicon-thumbs-up ${c.Id}'></i></a>` +
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

        let idProveedor = $(this).data('editar');

        $('#proveedorModal').data({
            'id-proveedor': idProveedor,
            'accion': 'editar'
        });
        $('#proveedorModal').modal();
    }).on('click', '.eliminar-proveedor', function (e) {
        e.preventDefault();

        let idProveedor = $(this)[0].classList[2];
        let filaActual = $($(this).parents('td')).parents('tr')

        obtenerProveedores(function (dados) {
            if (dados != undefined)
                eliminarProveedor(function (resultado) {
                    if (resultado) {
                        $(filaActual).fadeOut(600, function () {
                            tabelaDelProveedores
                                .row(filaActual)
                                .remove()
                                .draw();
                        });
                    }
                }, dados);
        }, idProveedor);
    });

    tabelaDelProveedores.on('draw', function () {
        $('[data-toggle="popover"]').popover()
    });
}

function eliminarProveedor(callback, proveedor) {
    $.ajax({
        url: `/Pages/Proveedor.aspx/EliminarProveedor`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{proveedor: '" + JSON.stringify(proveedor) + "'}",
        success: function (result) {
            if (result.d == 1) {
                notificacionGenerica(`El proveedor se ha eliminado correctamente.`, 'success', 3000);
                callback(true);
            }
            else {
                notificacionGenerica(`No se pudo eliminar el proveedor.`, 'danger', 3000);
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

function crearActualizarProveedor(proveedor) {
    $.ajax({
        url: `/Pages/Proveedor.aspx/${proveedor.Id != 0 ? "ActualizarProveedor" : "NuevoProveedor"}`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{proveedor: '" + JSON.stringify(proveedor) + "'}",
        success: function (result) {
            if (result.d == 1) {
                $('#proveedorModal').modal('hide');
                alGenerarTabelaDelProveedores();
                notificacionGenerica(`El proveedor se ha ${proveedor.Id != 0 ? "actualizado" : "añadido"} correctamente.`, 'success', 3000);
            }
            else
                notificacionGenerica(`No se pudo ${proveedor.Id != 0 ? "actualizar" : "añadir"} el proveedor.`, 'danger', 3000);
        },
        failure: function (failure) {
            alert(failure.d);
        },
        error: function (data) {
            notificacionGenerica(`No se pudo ${proveedor.Id != 0 ? "actualizar" : "añadir"} el proveedor.`, 'danger', 3000);
        }
    }).done(function () { });
}