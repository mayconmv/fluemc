Imports System.Web.Script.Services
Imports System.Web.Services
Imports Comun.ProveedoresFletes
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Public Class Contacto
    Inherits Page

    Private Shared _contactoNegocio As IContactoContracto

    Public Sub New()
        _contactoNegocio = New ContactoNegocio(ConfigurationManager.ConnectionStrings("ProveedoresFletesConexion").ConnectionString)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    'POST METHOD
    <WebMethod()>
    Public Shared Function NuevoContacto(ByVal contacto As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim contactoObj = JsonConvert.DeserializeObject(Of ContactoModelo)(contacto, configuracionDelJson)
        Dim resultado = _contactoNegocio.Crear(contactoObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function ActualizarContacto(ByVal contacto As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim contactoObj = JsonConvert.DeserializeObject(Of ContactoModelo)(contacto, configuracionDelJson)
        Dim resultado = _contactoNegocio.Actualizar(contactoObj)

        Return resultado
    End Function

    <WebMethod()>
    Public Shared Function EliminarContacto(ByVal contacto As Object) As String
        Dim configuracionDelJson = New JsonSerializerSettings()
        configuracionDelJson.MissingMemberHandling = MissingMemberHandling.Ignore

        Dim contactoObj = JsonConvert.DeserializeObject(Of ContactoModelo)(contacto, configuracionDelJson)
        Dim resultado = _contactoNegocio.Eliminar(contactoObj)

        Return resultado
    End Function

    'GET METHOD
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerContactos() As Object
        Dim resultado = _contactoNegocio.Obtener()
        Return resultado
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ObtenerContacto(ByVal id As Integer) As Object
        Dim resultado = _contactoNegocio.ObtenerPorId(id)
        Return resultado
    End Function
End Class