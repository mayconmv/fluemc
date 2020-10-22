Imports System.Linq.Expressions

Public Interface ICRUDBaseRepositorio(Of T)
    Function Crear(obj As T) As T
    Function Crear(sql As String) As Integer
    Function CrearTodos(obj As IEnumerable(Of T)) As IEnumerable(Of T)
    Function CrearTodos(sql As String) As IEnumerable(Of Integer)
    Function Actualizar(obj As T) As T
    Function Actualizar(sql As String) As Integer
    Function ActualizarTodos(obj As IEnumerable(Of T)) As IEnumerable(Of T)
    Function ActualizarTodos(sql As String) As IEnumerable(Of Integer)
    Function Eliminar(obj As T) As T
    Function Eliminar(sql As String) As Integer
    Function Obtener(where As Expression(Of Func(Of T, Boolean)), Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of T)
    Function Obtener(sql As String) As IEnumerable(Of T)
    Function ObtenerPorId(id As Long) As T
    Function ObtenerMuchos(where As Expression(Of Func(Of T, Boolean)), Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of T)
    Function ObtenerMuchos(sql As String) As IEnumerable(Of T)
End Interface
