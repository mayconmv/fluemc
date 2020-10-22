Public Class ProveedorRepositorio
    Inherits CRUDBaseRepositorio(Of ProveedorEntidade)
    Implements IProveedorRepositorio

    Public Sub New(rutaDelConexion As String)
        MyBase.New(rutaDelConexion)
    End Sub
End Class
