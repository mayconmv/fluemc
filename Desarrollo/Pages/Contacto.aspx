<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Contacto.aspx.vb" Inherits="ProveedoresFletes.Contacto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Pages/contacto.css" rel="stylesheet" />

    <div class="col-md-12">
        <hr />
        <h1 class="pull-left">Contactos</h1>
        <input type="button" class="btn btn-lg btn-primary pull-right" id="btnAnadirNuevo" value="Añadir Nuevo" style="margin-top: 10px;" />
        <div class="clearfix"></div>
        <hr />

        <table id="tabelaDelContactos" class="table table-hover display"></table>
    </div>

    <%--MODAL DE ACCIONES--%>
    <div class="modal fade" id="contactoModal" data-accion="crear" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <label id="lblTituloModal" style="font-size: 18pt; font-weight: bold;" class="modal-title">Añadir nuevo contacto</label>
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
                            <label for="txtDepartamento">Departamento</label>
                            <input type="text" class="form-control" id="txtDepartamento" value="" placeholder="Departamento" style="min-width: 100%;" />
                        </div>
                        <div class="col-md-4">
                            <label for="txtTelefono">Teléfono</label>
                            <input type="text" class="form-control" id="txtTelefono" value="" placeholder="Teléfono" style="min-width: 100%;" />
                        </div>
                        <div class="col-md-4">
                            <label for="drpProveedor">Proveedor</label>
                            <select class="form-control" id="drpProveedor" placeholder="Proveedor" style="min-width: 100%;">
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtEMail">E-Mail</label>
                            <input type="email" class="form-control" id="txtEMail" value="" placeholder="E-Mail" style="min-width: 100%;" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                    <button type="button" id="btnGuardarContacto" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/Pages/contacto.js"></script>
</asp:Content>
