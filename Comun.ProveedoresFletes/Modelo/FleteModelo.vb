Public Class FleteModelo
    Public Property Id As Long
    Public Property Negocio As String
    Public Property CostoTotal As Double
    Public Property Productos As New List(Of ProductoModelo)
    Public Property ProveedorId As Long
    Public Property Proveedor As ProveedorModelo
End Class
