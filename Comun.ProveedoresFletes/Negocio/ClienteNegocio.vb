Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports Datos.ProveedoresFletes
Imports Microsoft.VisualBasic.CompilerServices

Public Class ClienteNegocio
    Implements IClienteContracto

    Private _clienteRepositorio As IClienteRepositorio
    Private _proveedorRepositorio As IProveedorRepositorio

    Public Sub New(rutaDelConexion As String)
        _clienteRepositorio = New ClienteRepositorio(rutaDelConexion)
        _proveedorRepositorio = New ProveedorRepositorio(rutaDelConexion)
    End Sub

    Public Function Obtener() As Object Implements ICRUDBaseContracto(Of ClienteModelo).Obtener
        Try
            Dim resultado = _clienteRepositorio _
                .Obtener(My.Resources.ObtenerClientes) _
                .Select(Function(c) c.AlClienteModelo()) _
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

    Public Function ObtenerPorId(id As Long) As Object Implements ICRUDBaseContracto(Of ClienteModelo).ObtenerPorId
        Try
            Dim resultado = _clienteRepositorio _
                .Obtener(My.Resources.ObtenerCliente.Replace("{Id}", id)) _
                .FirstOrDefault()? _
                .AlClienteModelo()

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

    Public Function ObtenerPorNombre(nombre As String) As Object Implements ICRUDBaseContracto(Of ClienteModelo).ObtenerPorNombre
        Throw New NotImplementedException()
    End Function

    Public Function Crear(obj As ClienteModelo) As Object Implements ICRUDBaseContracto(Of ClienteModelo).Crear
        Try
            Dim resultado = _clienteRepositorio _
                .Actualizar(obj.AlClienteSQL(My.Resources.CrearClienteProcedimento))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Actualizar(obj As ClienteModelo) As Object Implements ICRUDBaseContracto(Of ClienteModelo).Actualizar
        Try
            Dim resultado = _clienteRepositorio _
                .Actualizar(obj.AlClienteSQL(My.Resources.ActualizarCliente))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Eliminar(obj As ClienteModelo) As Object Implements ICRUDBaseContracto(Of ClienteModelo).Eliminar
        Try
            Dim resultado = _clienteRepositorio _
                .Eliminar(obj.AlClienteSQL(My.Resources.EliminarCliente))
            Return resultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

Public Module ClienteExtensiones
    <Extension()>
    Public Function AlClienteModelo(ByVal cliente As ClienteEntidade) As ClienteModelo
        Return New ClienteModelo With {
            .Id = cliente.Id,
            .Absoluto = cliente.Absoluto,
            .DiasDePago = cliente.DiasDePago,
            .Flete = cliente.Flete,
            .Nombre = cliente.Nombre,
            .Porcentaje = cliente.Porcentaje,
            .Temperatura = cliente.Temperatura,
            .Tipo = cliente.Tipo,
            .ProveedorId = cliente.ProveedorId
        }
    End Function

    <Extension()>
    Public Function AlClienteSQL(ByVal cliente As ClienteModelo, ByVal SQL As String) As String
        For Each prop In cliente.GetType().GetProperties()
            Dim valor As Object = prop.GetValue(cliente)

            Select Case valor?.GetType()
                Case GetType(Boolean)
                    valor = Convert.ToByte(valor)
                Case GetType(ProveedorModelo)
                    valor = Nothing
                Case GetType(Double)
                    valor = valor.ToString().Replace(",", ".")
                Case Else
                    valor = prop.GetValue(cliente)
            End Select

            SQL = SQL _
                .Replace("{" + prop.Name + "}", valor)
        Next
        Return SQL
    End Function
End Module
