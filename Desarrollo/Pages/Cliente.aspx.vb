Imports System.Web.Script.Services
Imports System.Web.Services
Imports Comun.ProveedoresFletes
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Public Class Cliente
    Inherits Page

    Private Shared _clienteNegocio As IClienteContracto

    Public Sub New()
        _clienteNegocio = New ClienteNegocio(ConfigurationManager.ConnectionStrings("ProveedoresFletesConexion").ConnectionString)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    'POST METHOD
    <WebMethod()>
    Public Shared Function NuevoCliente(ByVal cliente As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim clienteObj = JsonConvert.DeserializeObject(Of ClienteModelo)(cliente, configuracionDelJson)
        Dim resultado = _clienteNegocio.Crear(clienteObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function ActualizarCliente(ByVal cliente As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim clienteObj = JsonConvert.DeserializeObject(Of ClienteModelo)(cliente, configuracionDelJson)
        Dim resultado = _clienteNegocio.Actualizar(clienteObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function EliminarCliente(ByVal cliente As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim clienteObj = JsonConvert.DeserializeObject(Of ClienteModelo)(cliente, configuracionDelJson)
        Dim resultado = _clienteNegocio.Eliminar(clienteObj)

        Return resultado
    End Function

    'GET METHOD
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerClientes() As Object
        Dim resultado = _clienteNegocio.Obtener()
        Return resultado
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerCliente(ByVal id As Integer) As Object
        Dim resultado = _clienteNegocio.ObtenerPorId(id)
        Return resultado
    End Function
End Class