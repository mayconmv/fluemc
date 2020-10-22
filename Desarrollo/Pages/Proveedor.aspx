<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Proveedor.aspx.vb" Inherits="ProveedoresFletes.Proveedor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Pages/proveedor.css" rel="stylesheet" />

    <div class="col-md-12">
        <hr />
        <h1 class="pull-left">Proveedores</h1>
        <input type="button" class="btn btn-lg btn-primary pull-right" id="btnAnadirNuevo" value="Añadir Nuevo" style="margin-top: 10px;"/>
        <div class="clearfix"></div>
        <hr />

        <table id="tabelaDelProveedores" class="table table-hover display"></table>
    </div>

    <%--MODAL DE ACCIONES--%>
    <div class="modal fade" id="proveedorModal" data-accion="crear" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <label id="lblTituloModal" style="font-size: 18pt; font-weight: bold;" class="modal-title">Añadir nuevo proveedor</label>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtNombre">Nombre</label>
                            <input type="text" class="form-control" id="txtNombre" value="" placeholder="Nombre" style="min-width: 100%;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            <label for="txtDireccion">Dirección</label>
                            <input type="text" class="form-control" id="txtDireccion" value="" placeholder="Dirección" style="min-width: 100%;" />
                        </div>
                        <div class="col-md-3">
                            <label for="txtRefProveedor">Ref Proveedor</label>
                            <input type="number" class="form-control" id="txtRefProveedor" value="" placeholder="Ref Proveedor" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                    <button type="button" id="btnGuardarProveedor" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/Pages/proveedor.js"></script>
</asp:Content>
