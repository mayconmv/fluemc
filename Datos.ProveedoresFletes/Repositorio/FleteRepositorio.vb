Public Class FleteRepositorio
    Inherits CRUDBaseRepositorio(Of FleteEntidade)
    Implements IFleteRepositorio

    Public Sub New(rutaDelConexion As String)
        MyBase.New(rutaDelConexion)
    End Sub
End Class
