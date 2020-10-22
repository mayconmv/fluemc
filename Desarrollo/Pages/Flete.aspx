<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Flete.aspx.vb" Inherits="ProveedoresFletes.Flete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Pages/flete.css" rel="stylesheet" />

    <div class="col-md-12">
        <hr />
        <h1 class="pull-left">Fletes</h1>
        <input type="button" class="btn btn-lg btn-primary pull-right" id="btnAnadirNuevo" value="Añadir Nuevo" style="margin-top: 10px;" />
        <div class="clearfix"></div>
        <hr />

        <table id="tabelaDelFletes" class="table table-hover display"></table>
    </div>

    <%--MODAL DE ACCIONES--%>
    <div class="modal fade" id="fleteModal" data-accion="crear" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <label id="lblTituloModal" style="font-size: 18pt; font-weight: bold;" class="modal-title">Añadir nuevo flete</label>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <label for="txtNegocio">Negócio</label>
                            <input type="text" class="form-control" id="txtNegocio" value="" placeholder="Negócio" style="min-width: 100%;" />
                        </div>
                        <div class="col-md-4">
                            <label for="drpProveedor">Proveedor</label>
                            <select class="form-control" id="drpProveedor" placeholder="Proveedor">
                            </select>
                        </div>
                    </div>
                    <br />
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <strong>Productos de lo flete</strong>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-7">
                                    <label for="drpProducto">Producto</label>
                                    <select class="form-control" id="drpProducto" placeholder="Producto" style="min-width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtCosto">Costo</label>
                                    <input type="number" class="form-control" id="txtCosto" value="" placeholder="Costo" style="min-width: 100%;" />
                                </div>
                                <div class="col-md-2">
                                    <label>
                                        <br />
                                    </label>
                                    <button id="btnAnadirProducto" class="btn btn-info" style="min-width: 100%;">
                                        <i class="glyphicon glyphicon-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="margin-bottom: -25px;">
                                    <table id="tabelaProductosDelFletes" class="table table-hover display table-bordered"></table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                    <button type="button" id="btnGuardarFlete" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/Pages/flete.js"></script>
</asp:Content>
