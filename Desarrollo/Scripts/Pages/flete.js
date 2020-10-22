let tabelaDelFletes;
let tabelaProductosDelFletes;
let modalDelFletes;
let fleteActual = [];

$(() => {
    let idFlete = 0;
    alGenerarTabelaDelFletes();

    $('#btnAnadirNuevo').click(function () {
        $('#fleteModal').data({
            'accion': 'crear',
            'id-flete': '0'
        });
        $('#fleteModal').modal();
    });

    $('#btnGuardarFlete').click(function () {
        let flete = {
            Id: idFlete,
            Negocio: $('#txtNegocio').val(),
            CostoTotal: $('#txtCostoTotal').val(),
            Productos: fleteActual.Productos,
            ProveedorId: $('#drpProveedor :selected').val()
        }

        if (!validateForm(flete))
            return;

        crearActualizarFlete(flete);
    });

    $('#btnAnadirProducto').click(function (e) {
        e.preventDefault();

        let productoFlete = {
            Producto: {
                Id: $('#drpProducto :selected').val(),
                Descripcion: $('#drpProducto :selected').data('descripcion')
            },
            Costo: $('#txtCosto').val()
        };

        anadirProductoDelFrete(productoFlete);
    });

    $('#fleteModal').on('show.bs.modal', function () {
        inicializarCasillasDeVerificacion();
        preencherProductos();
    }).on('shown.bs.modal', function () {
        let action = $(this).data('accion');
        idFlete = $(this).data('id-flete');

        if (action == 'crear') {
            preencherProveedores();
            generarTabelaDelProductosDelFletes({});
            $('#lblTituloModal').text('Añadir nuevo flete');
            formaClara();
        } else {
            $('#lblTituloModal').text('Actualizar flete');
            mostrarDatosEnModal(idFlete);
        }
    }).on('hide.bs.modal', function () {

    });

    $('#tabelaDelFletes').on('click', '[data-editar]', function (e) {
        e.preventDefault();

        let idFlete = $(this).data('editar');

        $('#fleteModal').data({
            'id-flete': idFlete,
            'accion': 'editar'
        });
        $('#fleteModal').modal();
    }).on('click', '.eliminar-flete', function (e) {
        e.preventDefault();

        let idFlete = $(this)[0].classList[2];
        let filaActual = $($(this).parents('td')).parents('tr')

        obtenerFletes(function (dados) {
            if (dados != undefined)
                eliminarFlete(function (resultado) {
                    if (resultado) {
                        $(filaActual).fadeOut(600, function () {
                            tabelaDelFletes
                                .row(filaActual)
                                .remove()
                                .draw();
                        });
                    }
                }, dados);
        }, idFlete);
    });

    $('#tabelaProductosDelFletes').on('click', '[data-eliminar]', function (e) {
        e.preventDefault();

        let idProducto = $(this).data('eliminar');

        let filaActual = $($(this).parents('td')).parents('tr');

        fleteActual.Productos = fleteActual.Productos.filter(p => p.Id !== idProducto);

        $(filaActual).fadeOut(600, function () {
            tabelaProductosDelFletes
                .row(filaActual)
                .remove()
                .draw();
        });
    })
});

function validateForm(obj) {
    if (obj.Negocio == '')
        return notificacionGenerica('¡El campo de <b>negócio</b> debe ser informado!', 'warning', 1500);

    if (obj.ProveedorId == '')
        return notificacionGenerica('¡El campo de <b>proveedor</b> debe ser informado!', 'warning', 1500);

    return true;
}

function anadirProductoDelFrete(productoFlete) {
    if (productoFlete.Producto.Id == '')
        return notificacionGenerica('¡Debes seleccionar el producto!', 'warning', 1500);

    if (productoFlete.Costo == '')
        return notificacionGenerica('¡Debes informar el costo del producto!', 'warning', 1500);

    let productoExiste = fleteActual.Productos.find(x => x.Id == productoFlete.Producto.Id);
    if (productoExiste)
        return notificacionGenerica('¡El producto seleccionado ya ha sido informado!', 'warning', 1500);

    let objeto = {
        Id: parseInt(productoFlete.Producto.Id),
        Descripcion: productoFlete.Producto.Descripcion,
        CostoDelFlete: parseFloat(productoFlete.Costo)
    };

    if (fleteActual.Productos == undefined)
        fleteActual.Productos = [];

    fleteActual.Productos.push(objeto);

    let fila = {
        'Descripcion': productoFlete.Producto.Descripcion,
        'CostoDelFlete': productoFlete.Costo
    };
    tabelaProductosDelFletes.row.add(fila).draw();

    $($('#tabelaProductosDelFletes').find('tr').last())
        .find('.glyphicon-trash')
        .attr('data-eliminar', productoFlete.Producto.Id);
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

function preencherProductos() {
    obtenerProductos(function (dados) {
        let objetoProductos = $('#drpProducto');
        objetoProductos.empty();
        objetoProductos.on('change', function (e) {
            $('#txtCosto').val(parseFloat($('#drpProducto :selected').data('costo')));
        })

        $(dados).each(function (a, b) {
            objetoProductos.append(`<option value="${b.Id}" data-costo="${b.CostoDelFlete}" data-descripcion="${b.Descripcion}">${b.Id} - ${b.Descripcion}</option>`);
        });

        objetoProductos.trigger('change');
    });
}

function inicializarCasillasDeVerificacion() {
    $("input[type=checkbox]")
        .bootstrapSwitch('state', false);
}

function mostrarDatosEnModal(idFlete) {
    obtenerFletes(function (dados) {
        $('#fleteModal #txtNegocio').val(dados.Negocio);

        preencherProveedores(dados.ProveedorId);

        generarTabelaDelProductosDelFletes(dados.Productos);

        fleteActual = dados;
    }, idFlete);
}

function formaClara() {
    $('#fleteModal .form-control').val('');
}

function generarTabelaDelProductosDelFletes(dados) {
    tabelaProductosDelFletes = $('#tabelaProductosDelFletes').DataTable({
        responsive: true,
        destroy: true,
        searching: false,
        ordering: false,
        paging: false,
        data: dados,
        'info': false,
        columns: [
            {
                data: 'Descripcion',
                title: 'Descripción'
            },
            {
                data: 'CostoDelFlete',
                title: 'Costo'
            },
            {
                width: '60px',
                render: function (a, b, c) {
                    let resultado = '<div style="font-size: 14pt; text-align: center;">' +
                        `<a href="#" style="margin-right: 8px;" title="Eliminar"><i data-eliminar="${c.Id}" class="glyphicon glyphicon-trash"></i></a>` +
                        '</div>';

                    return resultado;
                }
            }
        ],
        "language": {
            "url": "/Scripts/DataTable/Lang/Spanish.json"
        }
    });

    tabelaProductosDelFletes.on('draw', function () {
        $('[data-toggle="popover"]').popover()
    });
}

function alGenerarTabelaDelFletes() {
    obtenerFletes(function (dados) {
        if (dados != undefined)
            generarTabelaDelFletes(dados);
    });
}

function generarTabelaDelFletes(dados) {
    tabelaDelFletes = $('#tabelaDelFletes').DataTable({
        responsive: true,
        destroy: true,
        data: dados,
        columns: [
            {
                data: 'Id',
                title: 'Id'
            },
            {
                data: 'Negocio',
                title: 'Negócio'
            },
            {
                data: 'Proveedor.Nombre',
                title: 'Proveedor'
            },
            {
                data: 'CostoTotal',
                title: '$ Total'
            },
            {
                title: 'Opciones',
                width: '125px',
                render: function (a, b, c) {
                    let acciones = "<div class='btn-group'>" +
                        `<a href='#' class='btn btn-danger ${c.Id} eliminar-flete'><i class='glyphicon glyphicon-thumbs-up ${c.Id}'></i></a>` +
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
    });

    tabelaDelFletes.on('draw', function () {
        $('[data-toggle="popover"]').popover()
    });
}

function eliminarFlete(callback, flete) {
    $.ajax({
        url: `/Pages/Flete.aspx/EliminarFlete`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{flete: '" + JSON.stringify(flete) + "'}",
        success: function (result) {
            if (result.d == 1) {
                notificacionGenerica(`El flete se ha eliminado correctamente.`, 'success', 3000);
                callback(true);
            }
            else {
                notificacionGenerica(`No se pudo eliminar el flete.`, 'danger', 3000);
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

function crearActualizarFlete(flete) {
    $.ajax({
        url: `/Pages/Flete.aspx/${flete.Id != 0 ? "ActualizarFlete" : "NuevoFlete"}`,
        type: 'POST',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{flete: '" + JSON.stringify(flete) + "'}",
        success: function (result) {
            if (result.d == 1) {
                $('#fleteModal').modal('hide');
                alGenerarTabelaDelFletes();
                notificacionGenerica(`El flete se ha ${flete.Id != 0 ? "actualizado" : "añadido"} correctamente.`, 'success', 3000);
            }
            else
                notificacionGenerica(`No se pudo ${flete.Id != 0 ? "actualizar" : "añadir"} el flete.`, 'danger', 3000);
        },
        failure: function (failure) {
            alert(failure.d);
        },
        error: function (data) {
            notificacionGenerica(`No se pudo ${flete.Id != 0 ? "actualizar" : "añadir"} el flete.`, 'danger', 3000);
        }
    }).done(function () { });
}

function obtenerFletes(callback, idFlete = 0) {
    let webMethod = idFlete != 0 ?
        `ObtenerFlete?id=${idFlete}` :
        'ObtenerFletes';

    $.ajax({
        url: `/Pages/Flete.aspx/${webMethod}`,
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

function obtenerProductos(callback, idProducto = 0) {
    let webMethod = idProducto != 0 ?
        `ObtenerProducto?id=${idProducto}` :
        'ObtenerProductos';

    $.ajax({
        url: `/Pages/Flete.aspx/${webMethod}`,
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