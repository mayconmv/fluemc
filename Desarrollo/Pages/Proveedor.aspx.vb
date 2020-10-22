Imports System.Web.Script.Services
Imports System.Web.Services
Imports Comun.ProveedoresFletes
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Public Class Proveedor
    Inherits Page

    Private Shared _proveedorNegocio As IProveedorContracto

    Public Sub New()
        _proveedorNegocio = New ProveedorNegocio(ConfigurationManager.ConnectionStrings("ProveedoresFletesConexion").ConnectionString)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    'POST METHOD
    <WebMethod()>
    Public Shared Function NuevoProveedor(ByVal proveedor As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim proveedorObj = JsonConvert.DeserializeObject(Of ProveedorModelo)(proveedor, configuracionDelJson)
        Dim resultado = _proveedorNegocio.Crear(proveedorObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function ActualizarProveedor(ByVal proveedor As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim proveedorObj = JsonConvert.DeserializeObject(Of ProveedorModelo)(proveedor, configuracionDelJson)
        Dim resultado = _proveedorNegocio.Actualizar(proveedorObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function EliminarProveedor(ByVal proveedor As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim proveedorObj = JsonConvert.DeserializeObject(Of ProveedorModelo)(proveedor, configuracionDelJson)
        Dim resultado = _proveedorNegocio.Eliminar(proveedorObj)

        Return resultado
    End Function

    'GET METHOD
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerProveedores() As Object
        Dim resultado = _proveedorNegocio.Obtener()
        Return resultado
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerProveedor(ByVal id As Integer) As Object
        Dim resultado = _proveedorNegocio.ObtenerPorId(id)
        Return resultado
    End Function
End Class