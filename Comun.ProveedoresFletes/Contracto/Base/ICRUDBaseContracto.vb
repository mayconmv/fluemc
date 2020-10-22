Public Interface ICRUDBaseContracto(Of TEntity)
    Function Obtener() As Object
    Function ObtenerPorId(id As Long) As Object
    Function ObtenerPorNombre(nombre As String) As Object
    Function Crear(obj As TEntity) As Object
    Function Actualizar(obj As TEntity) As Object
    Function Eliminar(obj As TEntity) As Object
End Interface