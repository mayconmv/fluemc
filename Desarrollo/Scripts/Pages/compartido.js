function notificacionGenerica(mensaje, tipo = 'danger', retraso = 0) {
    $.notify(
        {
            icon: 'class',
            message: mensaje ?? 'Ocorreu um erro ao processar seu pedido!'
        },
        {
            offset: 50,
            allow_dismiss: true,
            placement: {
                from: "top",
                align: "center"
            },
            z_index: 9999,
            type: tipo,
            delay: retraso,
            animate: {
                enter: 'animated fadeInDown',
                exit: 'animated fadeOutUp'
            }
        }
    );
}

function decodeUnicode(str) {
    return decodeURIComponent(atob(str).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
}

function obtenerProveedores(callback, idProveedor = 0) {
    let webMethod = idProveedor != 0 ?
        `ObtenerProveedor?id=${idProveedor}` :
        'ObtenerProveedores';

    $.ajax({
        url: `/Pages/Proveedor.aspx/${webMethod}`,
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