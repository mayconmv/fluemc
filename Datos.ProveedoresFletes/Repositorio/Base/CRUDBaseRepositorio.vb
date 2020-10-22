Imports System.Linq.Expressions
Imports System.Data.SqlClient
Imports System.Reflection

Public Class CRUDBaseRepositorio(Of T)
    Implements ICRUDBaseRepositorio(Of T)

    Private _conexion As SqlConnection
    Private _comando As SqlCommand

    Public Sub New(rutaDelConexion As String)
        _conexion = New SqlConnection(rutaDelConexion)
    End Sub

    Private Sub AbrirConeccion(conexion As SqlConnection)
        conexion.Open()
    End Sub

    Private Sub CerrarConeccion()
        _comando.Dispose()
        _conexion.Close()
    End Sub

    Public Function Crear(obj As T) As T Implements ICRUDBaseRepositorio(Of T).Crear
        Throw New NotImplementedException()
    End Function

    Public Function Crear(sql As String) As Integer Implements ICRUDBaseRepositorio(Of T).Crear
        _comando = New SqlCommand(sql, _conexion)

        AbrirConeccion(_conexion)

        Dim resultado = _comando.ExecuteNonQuery()

        CerrarConeccion()

        Return resultado
    End Function

    Public Function CrearTodos(obj As IEnumerable(Of T)) As IEnumerable(Of T) Implements ICRUDBaseRepositorio(Of T).CrearTodos
        Throw New NotImplementedException()
    End Function

    Public Function CrearTodos(sql As String) As IEnumerable(Of Integer) Implements ICRUDBaseRepositorio(Of T).CrearTodos
        Throw New NotImplementedException()
    End Function

    Public Function Actualizar(obj As T) As T Implements ICRUDBaseRepositorio(Of T).Actualizar
        Throw New NotImplementedException()
    End Function

    Public Function Actualizar(sql As String) As Integer Implements ICRUDBaseRepositorio(Of T).Actualizar
        _comando = New SqlCommand(sql, _conexion)

        AbrirConeccion(_conexion)

        Dim resultado = _comando.ExecuteNonQuery()

        CerrarConeccion()

        Return resultado
    End Function

    Public Function ActualizarTodos(obj As IEnumerable(Of T)) As IEnumerable(Of T) Implements ICRUDBaseRepositorio(Of T).ActualizarTodos
        Throw New NotImplementedException()
    End Function

    Public Function ActualizarTodos(sql As String) As IEnumerable(Of Integer) Implements ICRUDBaseRepositorio(Of T).ActualizarTodos
        Throw New NotImplementedException()
    End Function

    Public Function Eliminar(obj As T) As T Implements ICRUDBaseRepositorio(Of T).Eliminar
        Throw New NotImplementedException()
    End Function

    Public Function Eliminar(sql As String) As Integer Implements ICRUDBaseRepositorio(Of T).Eliminar
        _comando = New SqlCommand(sql, _conexion)

        AbrirConeccion(_conexion)

        Dim resultado = _comando.ExecuteNonQuery()

        CerrarConeccion()

        Return resultado
    End Function

    Public Function Obtener(where As Expression(Of Func(Of T, Boolean)), Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of T) Implements ICRUDBaseRepositorio(Of T).Obtener
        Throw New NotImplementedException()
    End Function

    Public Function Obtener(sql As String) As IEnumerable(Of T) Implements ICRUDBaseRepositorio(Of T).Obtener
        _comando = New SqlCommand(sql, _conexion)

        AbrirConeccion(_conexion)

        Dim listaDelResultados As New List(Of T)
        Dim lector = _comando.ExecuteReader()

        Mapper(listaDelResultados, lector)

        CerrarConeccion()

        Return listaDelResultados
    End Function

    Public Function ObtenerPorId(id As Long) As T Implements ICRUDBaseRepositorio(Of T).ObtenerPorId
        Throw New NotImplementedException()
    End Function

    Public Function ObtenerMuchos(where As Expression(Of Func(Of T, Boolean)), Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of T) Implements ICRUDBaseRepositorio(Of T).ObtenerMuchos
        Throw New NotImplementedException()
    End Function

    Public Function ObtenerMuchos(sql As String) As IEnumerable(Of T) Implements ICRUDBaseRepositorio(Of T).ObtenerMuchos
        Throw New NotImplementedException()
    End Function

    Private Sub Mapper(ByRef listaDelResultados As List(Of T), ByVal lector As SqlDataReader)
        While lector.Read()
            Dim objetoResultado As T
            objetoResultado = Activator.CreateInstance(Of T)()

            For Each prop As PropertyInfo In objetoResultado.GetType().GetProperties()
                Dim lectorNombre = lector(prop.Name)
                If Not Equals(lectorNombre, DBNull.Value) Then
                    prop.SetValue(objetoResultado,
                                  lectorNombre,
                                  Nothing)
                End If
            Next

            listaDelResultados.Add(objetoResultado)
        End While
    End Sub
End Class
