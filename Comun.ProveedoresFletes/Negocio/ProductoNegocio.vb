Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports Datos.ProveedoresFletes
Imports Microsoft.VisualBasic.CompilerServices

Public Class ProductoNegocio
    Implements IProductoContracto

    Private _productoRepositorio As IProductoRepositorio
    Private _proveedorRepositorio As IProveedorRepositorio

    Public Sub New(rutaDelConexion As String)
        _productoRepositorio = New ProductoRepositorio(rutaDelConexion)
        _proveedorRepositorio = New ProveedorRepositorio(rutaDelConexion)
    End Sub

    Public Function Obtener() As Object Implements ICRUDBaseContracto(Of ProductoModelo).Obtener
        Try
            Dim resultado = _productoRepositorio _
                .Obtener(My.Resources.ObtenerProductos) _
                .Select(Function(c) c.AlProductoModelo()) _
                .ToList()

            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorId(id As Long) As Object Implements ICRUDBaseContracto(Of ProductoModelo).ObtenerPorId
        Try
            Dim resultado = _productoRepositorio _
                .Obtener(My.Resources.ObtenerProducto.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlProductoModelo()

            Return resultado
        Catch ex As NullReferenceException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorNombre(nombre As String) As Object Implements ICRUDBaseContracto(Of ProductoModelo).ObtenerPorNombre
        Throw New NotImplementedException()
    End Function

    Public Function Crear(obj As ProductoModelo) As Object Implements ICRUDBaseContracto(Of ProductoModelo).Crear
        Throw New NotImplementedException()
    End Function

    Public Function Actualizar(obj As ProductoModelo) As Object Implements ICRUDBaseContracto(Of ProductoModelo).Actualizar
        Throw New NotImplementedException()
    End Function

    Public Function Eliminar(obj As ProductoModelo) As Object Implements ICRUDBaseContracto(Of ProductoModelo).Eliminar
        Throw New NotImplementedException()
    End Function
End Class

Public Module ProductoExtensiones
    <Extension()>
    Public Function AlProductoModelo(ByVal producto As ProductoEntidade) As ProductoModelo
        Return New ProductoModelo With {
            .Id = producto.Id,
            .Descripcion = producto.Descripcion,
            .CostoDelFlete = producto.CostoDelFlete
        }
    End Function

    <Extension()>
    Public Function AlProductoSQL(ByVal producto As ProductoModelo, ByVal SQL As String) As String
        For Each prop In producto.GetType().GetProperties()
            Dim valor As Object = prop.GetValue(producto)

            Select Case valor?.GetType()
                Case GetType(Boolean)
                    valor = Convert.ToByte(valor)
                Case GetType(Double)
                    valor = valor.ToString().Replace(",", ".")
                Case Else
                    valor = prop.GetValue(producto)
            End Select

            SQL = SQL _
                .Replace("{" + prop.Name + "}", valor)
        Next
        Return SQL
    End Function
End Module
