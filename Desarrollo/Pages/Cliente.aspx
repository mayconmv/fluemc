<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Cliente.aspx.vb" Inherits="ProveedoresFletes.Cliente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Pages/cliente.css" rel="stylesheet" />

    <div class="col-md-12">
        <hr />
        <h1 class="pull-left">Clientes</h1>
        <input type="button" class="btn btn-lg btn-primary pull-right" id="btnAnadirNuevo" value="Añadir Nuevo" style="margin-top: 10px;"/>
        <div class="clearfix"></div>
        <hr />

        <table id="tabelaDelClientes" class="table table-hover display"></table>
    </div>

    <%--MODAL DE ACCIONES--%>
    <div class="modal fade" id="clienteModal" data-accion="crear" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <label id="lblTituloModal" style="font-size: 18pt; font-weight: bold;" class="modal-title">Añadir nuevo cliente</label>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtNombre">Nombre</label>
                            <input type="text" class="form-control" id="txtNombre" value="" placeholder="Nombre" style="min-width: 100%;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label for="txtDiasDePago">Dias de Pago</label>
                            <input type="number" class="form-control" id="txtDiasDePago" value="" placeholder="Dias de Pago" />
                        </div>
                        <div class="col-md-4">
                            <label for="txtPorcentaje">Porcentaje</label>
                            <input type="number" class="form-control" id="txtPorcentaje" value="" placeholder="Porcentaje" />
                        </div>
                        <div class="col-md-4">
                            <label for="txtAbsoluto">Absoluto</label>
                            <input type="text" class="form-control" id="txtAbsoluto" value="" placeholder="Absoluto" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label for="drpTipo">Tipo</label>
                            <select class="form-control" id="drpTipo" placeholder="Tipo">
                                <option>Referenciado</option>
                                <option>Spot</option>
                            </select>
                        </div>
                        <div class="col-md-4">
                            <label for="drpProveedor">Proveedor</label>
                            <select class="form-control" id="drpProveedor" placeholder="Proveedor">
                            </select>
                        </div>
                        <div class="col-md-2">
                            <label for="txtTemperatura">Temperatura</label><br />
                            <input type="checkbox" data-on-text="SI" data-off-text="NO" name="chkTemperatura" id="chkTemperatura" />
                        </div>
                        <div class="col-md-2">
                            <label for="txtFlete">Flete</label><br />
                            <input type="checkbox" data-on-text="SI" data-off-text="NO" name="chkFlete" id="chkFlete" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                    <button type="button" id="btnGuardarCliente" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/Pages/cliente.js"></script>
</asp:Content>
