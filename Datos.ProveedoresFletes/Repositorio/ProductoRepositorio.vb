Public Class ProductoRepositorio
    Inherits CRUDBaseRepositorio(Of ProductoEntidade)
    Implements IProductoRepositorio

    Public Sub New(rutaDelConexion As String)
        MyBase.New(rutaDelConexion)
    End Sub
End Class
