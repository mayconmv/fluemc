Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports Datos.ProveedoresFletes
Imports Microsoft.VisualBasic.CompilerServices

Public Class ContactoNegocio
    Implements IContactoContracto

    Private _contactoRepositorio As IContactoRepositorio
    Private _proveedorRepositorio As IProveedorRepositorio

    Public Sub New(rutaDelConexion As String)
        _contactoRepositorio = New ContactoRepositorio(rutaDelConexion)
        _proveedorRepositorio = New ProveedorRepositorio(rutaDelConexion)
    End Sub

    Public Function Obtener() As Object Implements ICRUDBaseContracto(Of ContactoModelo).Obtener
        Try
            Dim resultado = _contactoRepositorio _
                .Obtener(My.Resources.ObtenerContactos) _
                .Select(Function(c) c.AlContactoModelo()) _
                .ToList()
            resultado.ForEach(Sub(c) c.Proveedor = _proveedorRepositorio _
                    .Obtener(My.Resources.ObtenerProveedor.Replace("{Id}", c.ProveedorId)) _
                    .FirstOrDefault()? _
                    .AlProveedorModelo())

            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorId(id As Long) As Object Implements ICRUDBaseContracto(Of ContactoModelo).ObtenerPorId
        Try
            Dim resultado = _contactoRepositorio _
                .Obtener(My.Resources.ObtenerContacto.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlContactoModelo()

            If Not resultado Is Nothing Then
                resultado.Proveedor = _proveedorRepositorio _
                .Obtener(My.Resources.ObtenerProveedor.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlProveedorModelo()
            End If

            Return resultado
        Catch ex As NullReferenceException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorNombre(nombre As String) As Object Implements ICRUDBaseContracto(Of ContactoModelo).ObtenerPorNombre
        Throw New NotImplementedException()
    End Function

    Public Function Crear(obj As ContactoModelo) As Object Implements ICRUDBaseContracto(Of ContactoModelo).Crear
        Try
            Dim resultado = _contactoRepositorio _
                .Actualizar(obj.AlContactoSQL(My.Resources.CrearContacto))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Actualizar(obj As ContactoModelo) As Object Implements ICRUDBaseContracto(Of ContactoModelo).Actualizar
        Try
            Dim resultado = _contactoRepositorio _
                .Actualizar(obj.AlContactoSQL(My.Resources.ActualizarContacto))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Eliminar(obj As ContactoModelo) As Object Implements ICRUDBaseContracto(Of ContactoModelo).Eliminar
        Try
            Dim resultado = _contactoRepositorio _
                .Eliminar(obj.AlContactoSQL(My.Resources.EliminarContacto))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

Public Module ContactoExtensiones
    <Extension()>
    Public Function AlContactoModelo(ByVal contacto As ContactoEntidade) As ContactoModelo
        Return New ContactoModelo With {
            .Id = contacto.Id,
            .Nombre = contacto.Nombre,
            .Departamento = contacto.Departamento,
            .EMail = contacto.EMail,
            .Telefono = contacto.Telefono,
            .ProveedorId = contacto.ProveedorId
        }
    End Function

    <Extension()>
    Public Function AlContactoSQL(ByVal contacto As ContactoModelo, ByVal SQL As String) As String
        For Each prop In contacto.GetType().GetProperties()
            Dim valor As Object = prop.GetValue(contacto)

            Select Case valor?.GetType()
                Case GetType(Boolean)
                    valor = Convert.ToByte(valor)
                Case GetType(ProveedorModelo)
                    valor = Nothing
                Case GetType(Double)
                    valor = valor.ToString().Replace(",", ".")
                Case Else
                    valor = prop.GetValue(contacto)
            End Select

            SQL = SQL _
                .Replace("{" + prop.Name + "}", valor)
        Next
        Return SQL
    End Function
End Module
