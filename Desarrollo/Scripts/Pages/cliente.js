let tabelaDelClientes;
let modalDelClientes;

$(() => {
    let idCliente = 0;
    alGenerarTabelaDelClientes();

    $('#btnAnadirNuevo').click(function () {
        $('#clienteModal').data({
            'accion': 'crear',
            'id-cliente' : '0'
        });
        $('#clienteModal').modal();
    });

    $('#btnGuardarCliente').click(function () {
        let cliente = {
            Id: idCliente,
            Nombre: $('#txtNombre').val(),
            DiasDePago: $('#txtDiasDePago').val(),
            Temperatura: $('#clienteModal #chkTemperatura').bootstrapSwitch('state'),
            Flete: $('#clienteModal #chkFlete').bootstrapSwitch('state'),
            Tipo: $('#drpTipo :selected').val(),
            Porcentaje: $('#txtPorcentaje').val(),
            Absoluto: $('#txtAbsoluto').val(),
            ProveedorId: $('#drpProveedor :selected').val()
        }

        if (!validateForm(cliente))
            return;

        crearActualizarCliente(cliente);
    });

    $('#clienteModal').on('show.bs.modal', function () {
        inicializarCasillasDeVerificacion();
    }).on('shown.bs.modal', function () {
        let action = $(this).data('accion');
        idCliente = $(this).data('id-cliente');

        if (action == 'crear') {
            preencherProveedores();
            $('#lblTituloModal').text('Añadir nuevo cliente');
            formaClara();
        } else {
            $('#lblTituloModal').text('Actualizar cliente');
            mostrarDatosEnModal(idCliente);
        }
    }).on('hide.bs.modal', function () {
        
    });
});

function validateForm(obj) {
    if (obj.Nombre == '')
        return notificacionGenerica('¡El campo de <b>nombre</b> debe ser informado!', 'warning', 1500);

    if (obj.DiasDePago == '')
        return notificacionGenerica('¡El campo de <b>dias de pago</b> debe ser informado!', 'warning', 1500);

    if (obj.Porcentaje == '')
        return notificacionGenerica('¡El campo de <b>porcentaje</b> debe ser informado!', 'warning', 1500);

    if (obj.Absoluto == '')
        return notificacionGenerica('¡El campo de <b>absoluto</b> debe ser informado!', 'warning', 1500);

    if (obj.Tipo == '')
        return notificacionGenerica('¡El campo de <b>tipo</b> debe ser informado!', 'warning', 1500);

    if (obj.ProveedorId == '')
        return notificacionGenerica('¡El campo de <b>proveedor</b> debe ser informado!', 'warning', 1500);

    return true;
}

function preencherProveedores(idProveedor = 0) {
    obtenerProveedores(function (dados) {
        let objetoProveedores = $('#drpProveedor');
        objetoProveedores.empty();

        $(dados).each(function (a, b) {
            objetoProveedores.append(`<option value="${b.Id}" ${b.Id == idProveedor ? "selected": ""}>${b.Nombre}</option>`);
        });
    });
}

function inicializarCasillasDeVerificacion() {
    $("input[type=checkbox]")
        .bootstrapSwitch('state', false);
}

function mostrarDatosEnModal(idCliente) {
    obtenerClientes(function (dados) {
        $('#clienteModal #txtNombre').val(dados.Nombre);
        $('#clienteModal #txtDiasDePago').val(dados.DiasDePago);
        $('#clienteModal #drpTipo').val(dados.Tipo).trigger('change');
        $('#clienteModal #txtPorcentaje').val(dados.Porcentaje);
        $('#clienteModal #txtAbsoluto').val(dados.Absoluto);
        $('#clienteModal #chkTemperatura').bootstrapSwitch('state', dados.Temperatura);
        $('#clienteModal #chkFlete').bootstrapSwitch('state', dados.Flete);

        preencherProveedores(dados.ProveedorId);
    }, idCliente);
}

function formaClara() {
    $('#clienteModal .form-control').val('');
}

function alGenerarTabelaDelClientes() {
    obtenerClientes(function (dados) {
        if (dados != undefined)
            generarTabelaDelClientes(dados);
    });
}

function generarTabelaDelClientes(dados) {
    tabelaDelClientes = $('#tabelaDelClientes').DataTable({
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
                data: 'DiasDePago',
                title: 'Dias de Pago'
            },
            {
                data: 'Proveedor.Nombre',
                title: 'Proveedor'
            },
            {
                title: 'Opciones',
                width: '125px',
                render: function (a, b, c) {
                    let acciones = "<div class='btn-group'>" +
                        `<a href='#' class='btn btn-danger ${c.Id} eliminar-cliente'><i class='glyphicon glyphicon-thumbs-up ${c.Id}'></i></a>` +
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

        let idCliente = $(this).data('editar');

        $('#clienteModal').data({
            'id-cliente': idCliente,
            'accion' : 'editar'
        });
        $('#clienteModal').modal();
    }).on('click', '.eliminar-cliente', function (e) {
        e.preventDefault();

        let idCliente = $(this)[0].classList[2];
        let filaActual = $($(this).parents('td')).parents('tr')

        obtenerClientes(function (dados) {
            if (dados != undefined)
                eliminarCliente(function (resultado) {
                    if (resultado) {
                        $(filaActual).fadeOut(600, function () {
                            tabelaDelClientes
                                .row(filaActual)
                                .remove()
                                .draw();
                        });
                    }
                }, dados);
        }, idCliente);
    });

    tabelaDelClientes.on('draw', function () {
        $('[data-toggle="popover"]').popover()
    });
}

function eliminarCliente(callback, cliente) {
    $.ajax({
        url: `/Pages/Cliente.aspx/EliminarCliente`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{cliente: '" + JSON.stringify(cliente) + "'}",
        success: function (result) {
            if (result.d == 1) {
                notificacionGenerica(`El cliente se ha eliminado correctamente.`, 'success', 3000);
                callback(true);
            }
            else {
                notificacionGenerica(`No se pudo eliminar el cliente.`, 'danger', 3000);
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

function crearActualizarCliente(cliente) {
    $.ajax({
        url: `/Pages/Cliente.aspx/${cliente.Id != 0 ? "ActualizarCliente" : "NuevoCliente"}`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{cliente: '" + JSON.stringify(cliente) + "'}",
        success: function (result) {
            if (result.d == 1) {
                $('#clienteModal').modal('hide');
                alGenerarTabelaDelClientes();
                notificacionGenerica(`El cliente se ha ${cliente.Id != 0 ? "actualizado" : "añadido"} correctamente.`, 'success', 3000);
            }
            else
                notificacionGenerica(`No se pudo ${cliente.Id != 0 ? "actualizar" : "añadir"} el cliente.`, 'danger', 3000);
        },
        failure: function (failure) {
            alert(failure.d);
        },
        error: function (data) {
            notificacionGenerica(`No se pudo ${cliente.Id != 0 ? "actualizar" : "añadir"} el cliente.`, 'danger', 3000);
        }
    }).done(function () { });
}

function obtenerClientes(callback, idCliente = 0) {
    let webMethod = idCliente != 0 ?
        `ObtenerCliente?id=${idCliente}` :
        'ObtenerClientes';

    $.ajax({
        url: `/Pages/Cliente.aspx/${webMethod}`,
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