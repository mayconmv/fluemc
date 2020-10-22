Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports Datos.ProveedoresFletes
Imports Microsoft.VisualBasic.CompilerServices

Public Class FleteNegocio
    Implements IFleteContracto

    Private _fleteRepositorio As IFleteRepositorio
    Private _proveedorRepositorio As IProveedorRepositorio
    Private _productoRepositorio As IProductoRepositorio

    Public Sub New(rutaDelConexion As String)
        _fleteRepositorio = New FleteRepositorio(rutaDelConexion)
        _proveedorRepositorio = New ProveedorRepositorio(rutaDelConexion)
        _productoRepositorio = New ProductoRepositorio(rutaDelConexion)
    End Sub

    Public Function Obtener() As Object Implements ICRUDBaseContracto(Of FleteModelo).Obtener
        Try
            Dim resultado = _fleteRepositorio _
                .Obtener(My.Resources.ObtenerFletes) _
                .Select(Function(c) c.AlFleteModelo()) _
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

    Public Function ObtenerPorId(id As Long) As Object Implements ICRUDBaseContracto(Of FleteModelo).ObtenerPorId
        Try
            Dim resultado = _fleteRepositorio _
                .Obtener(My.Resources.ObtenerFlete.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlFleteModelo()

            If Not resultado Is Nothing Then
                resultado.Proveedor = _proveedorRepositorio _
                .Obtener(My.Resources.ObtenerProveedor.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlProveedorModelo()
            End If

            Dim productosDelFlete = _productoRepositorio _
                .Obtener(My.Resources.ObtenerProductosDelFlete.Replace("{FleteId}", id)) _
                .Select(Function(x) x.AlProductoModelo()) _
                .ToList()

            resultado.Productos = productosDelFlete

            Return resultado
        Catch ex As NullReferenceException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerPorNombre(nombre As String) As Object Implements ICRUDBaseContracto(Of FleteModelo).ObtenerPorNombre
        Throw New NotImplementedException()
    End Function

    Public Function Crear(obj As FleteModelo) As Object Implements ICRUDBaseContracto(Of FleteModelo).Crear
        Try
            Dim fleteId As Long? = obj.Id

            obj.CostoTotal = obj.Productos.Sum(Function(f) f.CostoDelFlete)

            Dim resultado = _fleteRepositorio _
                .Crear(obj.AlFleteSQL(My.Resources.CrearFlete))

            If resultado = 1 Then
                If fleteId = 0 Then
                    fleteId = _fleteRepositorio _
                                  .Obtener(My.Resources.ObtenerUltimoFleteIncluido) _
                                  .FirstOrDefault()?.Id
                End If

                For Each producto In obj.Productos
                    Dim inclusionDeProductos = _fleteRepositorio _
                        .Crear(producto.AlFleteProductoSQL(My.Resources.AnadirProductosDelFlete, fleteId))
                Next
            End If

            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Actualizar(obj As FleteModelo) As Object Implements ICRUDBaseContracto(Of FleteModelo).Actualizar
        Try
            obj.CostoTotal = obj.Productos.Sum(Function(f) f.CostoDelFlete)

            Dim resultado = _fleteRepositorio _
                .Actualizar(obj.AlFleteSQL(My.Resources.ActualizarFlete))

            Dim eliminacionDeProductos = _fleteRepositorio _
                .Eliminar(My.Resources.EliminarProductosDelFlete.Replace("{FleteId}", obj.Id))

            For Each producto In obj.Productos
                Dim inclusionDeProductos = _fleteRepositorio _
                    .Crear(producto.AlFleteProductoSQL(My.Resources.AnadirProductosDelFlete, obj.Id))
            Next

            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Eliminar(obj As FleteModelo) As Object Implements ICRUDBaseContracto(Of FleteModelo).Eliminar
        Try
            Dim eliminacionDeProductos = _fleteRepositorio _
                .Eliminar(My.Resources.EliminarProductosDelFlete.Replace("{FleteId}", obj.Id))

            Dim resultado = _fleteRepositorio _
                .Eliminar(obj.AlFleteSQL(My.Resources.EliminarFlete))

            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

Public Module FleteExtensiones
    <Extension()>
    Public Function AlFleteModelo(ByVal flete As FleteEntidade) As FleteModelo
        Return New FleteModelo With {
            .Id = flete.Id,
            .Negocio = flete.Negocio,
            .CostoTotal = flete.CostoTotal,
            .ProveedorId = flete.ProveedorId
        }
    End Function

    <Extension()>
    Public Function AlFleteSQL(ByVal flete As FleteModelo, ByVal SQL As String) As String
        For Each prop In flete.GetType().GetProperties()
            Dim valor As Object = prop.GetValue(flete)

            Select Case valor?.GetType()
                Case GetType(Boolean)
                    valor = Convert.ToByte(valor)
                Case GetType(ProveedorModelo)
                    valor = Nothing
                Case GetType(List(Of ProductoModelo))
                    valor = Nothing
                Case GetType(Double)
                    valor = valor.ToString().Replace(",", ".")
                Case Else
                    valor = prop.GetValue(flete)
            End Select

            SQL = SQL _
                .Replace("{" + prop.Name + "}", valor)
        Next
        Return SQL
    End Function

    <Extension()>
    Public Function AlFleteProductoSQL(ByVal producto As ProductoModelo, ByVal SQL As String, ByVal fleteId As Long?) As String
        Return SQL _
            .Replace("{FleteId}", IIf(IsNothing(fleteId), 0, fleteId)) _
            .Replace("{ProductoId}", producto.Id) _
            .Replace("{Costo}", producto.CostoDelFlete.ToString().Replace(",", "."))
    End Function
End Module
