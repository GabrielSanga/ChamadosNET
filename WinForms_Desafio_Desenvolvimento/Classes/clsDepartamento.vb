Public Class Departamento

    Private _ID As Integer
    Private _Descricao As String

    Public Property ID As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Public Property Descricao As String
        Get
            Return _Descricao
        End Get
        Set(value As String)
            _Descricao = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, descricao As String)
        _ID = id
        _Descricao = descricao
    End Sub

    Public Sub Gravar()
        Dim mensagem As String = ""

        If Not Valida(mensagem) Then
            Throw New Exception(mensagem)
        End If

        Dados.GravarDepartamento(Me)
    End Sub

    Public Sub Excluir()
        Dados.ExcluirDepartamento(_ID)
    End Sub

    Public Function Valida(ByRef mensagem As String) As Boolean
        If _Descricao.Length = 0 Then
            mensagem = "Descrição é campo de preenchimento obrigatório." : Return False
        End If

        Return True
    End Function

End Class
