Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports Datos.ProveedoresFletes
Imports Microsoft.VisualBasic.CompilerServices

Public Class ProveedorNegocio
    Implements IProveedorContracto

    Private _proveedorRepositorio As IProveedorRepositorio

    Public Sub New(rutaDelConexion As String)
        _proveedorRepositorio = New ProveedorRepositorio(rutaDelConexion)
    End Sub

    Public Function Obtener() As Object Implements ICRUDBaseContracto(Of ProveedorModelo).Obtener
        Try
            Dim resultado = _proveedorRepositorio _
                .Obtener(My.Resources.ObtenerProveedores) _
                .Select(Function(c) c.AlProveedorModelo())
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorId(id As Long) As Object Implements ICRUDBaseContracto(Of ProveedorModelo).ObtenerPorId
        Try
            Dim resultado = _proveedorRepositorio _
                .Obtener(My.Resources.ObtenerProveedor.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlProveedorModelo()
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorNombre(nombre As String) As Object Implements ICRUDBaseContracto(Of ProveedorModelo).ObtenerPorNombre
        Throw New NotImplementedException()
    End Function

    Public Function Crear(obj As ProveedorModelo) As Object Implements ICRUDBaseContracto(Of ProveedorModelo).Crear
        Try
            Dim resultado = _proveedorRepositorio _
                .Actualizar(obj.AlProveedorSQL(My.Resources.CrearProveedor))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Actualizar(obj As ProveedorModelo) As Object Implements ICRUDBaseContracto(Of ProveedorModelo).Actualizar
        Try
            Dim resultado = _proveedorRepositorio _
                .Actualizar(obj.AlProveedorSQL(My.Resources.ActualizarProveedor))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Eliminar(obj As ProveedorModelo) As Object Implements ICRUDBaseContracto(Of ProveedorModelo).Eliminar
        Try
            Dim resultado = _proveedorRepositorio _
                .Eliminar(obj.AlProveedorSQL(My.Resources.EliminarProveedor))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

Public Module ProveedorExtensiones
    <Extension()>
    Public Function AlProveedorModelo(ByVal proveedor As ProveedorEntidade) As ProveedorModelo
        Return New ProveedorModelo With {
            .Id = proveedor.Id,
            .Nombre = proveedor.Nombre,
            .Direccion = proveedor.Direccion,
            .RefProveedor = proveedor.RefProveedor
        }
    End Function

    <Extension()>
    Public Function AlProveedorSQL(ByVal proveedor As ProveedorModelo, ByVal SQL As String) As String
        For Each prop In proveedor.GetType().GetProperties()
            Dim valor As Object = prop.GetValue(proveedor)
            If (TypeOf prop.GetValue(proveedor) Is Boolean) Then
                valor = Convert.ToByte(valor)
            End If

            SQL = SQL _
                .Replace("{" + prop.Name + "}", valor)
        Next
        Return SQL
    End Function
End Module
