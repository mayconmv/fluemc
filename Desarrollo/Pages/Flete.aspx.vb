Imports System.Web.Script.Services
Imports System.Web.Services
Imports Comun.ProveedoresFletes
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Public Class Flete
    Inherits Page

    Private Shared _fleteNegocio As IFleteContracto
    Private Shared _productoNegocio As IProductoContracto

    Public Sub New()
        _fleteNegocio = New FleteNegocio(ConfigurationManager.ConnectionStrings("ProveedoresFletesConexion").ConnectionString)
        _productoNegocio = New ProductoNegocio(ConfigurationManager.ConnectionStrings("ProveedoresFletesConexion").ConnectionString)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    'POST METHOD
    <WebMethod()>
    Public Shared Function NuevoFlete(ByVal flete As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim fleteObj = JsonConvert.DeserializeObject(Of FleteModelo)(flete, configuracionDelJson)
        Dim resultado = _fleteNegocio.Crear(fleteObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function ActualizarFlete(ByVal flete As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim fleteObj = JsonConvert.DeserializeObject(Of FleteModelo)(flete, configuracionDelJson)
        Dim resultado = _fleteNegocio.Actualizar(fleteObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function EliminarFlete(ByVal flete As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim fleteObj = JsonConvert.DeserializeObject(Of FleteModelo)(flete, configuracionDelJson)
        Dim resultado = _fleteNegocio.Eliminar(fleteObj)

        Return resultado
    End Function

    'GET METHOD
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerFletes() As Object
        Dim resultado = _fleteNegocio.Obtener()
        Return resultado
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerFlete(ByVal id As Integer) As Object
        Dim resultado = _fleteNegocio.ObtenerPorId(id)
        Return resultado
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerProductos() As Object
        Dim resultado = _productoNegocio.Obtener()
        Return resultado
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerProducto(ByVal id As Integer) As Object
        Dim resultado = _productoNegocio.ObtenerPorId(id)
        Return resultado
    End Function
End Class