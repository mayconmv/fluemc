Public Class ContactoRepositorio
    Inherits CRUDBaseRepositorio(Of ContactoEntidade)
    Implements IContactoRepositorio

    Public Sub New(rutaDelConexion As String)
        MyBase.New(rutaDelConexion)
    End Sub
End Class
