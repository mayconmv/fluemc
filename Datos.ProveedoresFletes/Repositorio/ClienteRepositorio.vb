Public Class ClienteRepositorio
    Inherits CRUDBaseRepositorio(Of ClienteEntidade)
    Implements IClienteRepositorio

    Public Sub New(rutaDelConexion As String)
        MyBase.New(rutaDelConexion)
    End Sub
End Class
