Public Class Chamado

    Private _ID As Integer
    Private _Assunto As String
    Private _Solicitante As String
    Private _Departamento As Integer
    Private _DataAbertura As DateTime

    Public Property ID As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Public Property Assunto As String
        Get
            Return _Assunto
        End Get
        Set(value As String)
            _Assunto = value
        End Set
    End Property

    Public Property Solicitante As String
        Get
            Return _Solicitante
        End Get
        Set(value As String)
            _Solicitante = value
        End Set
    End Property

    Public Property Departamento As Integer
        Get
            Return _Departamento
        End Get
        Set(value As Integer)
            _Departamento = value
        End Set
    End Property

    Public Property DataAbertura As DateTime
        Get
            Return _DataAbertura
        End Get
        Set(value As DateTime)
            _DataAbertura = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, assunto As String, solicitante As String, departamento As Integer, dataAbertura As DateTime)
        _ID = id
        _Assunto = assunto
        _Solicitante = solicitante
        _Departamento = departamento
        _DataAbertura = dataAbertura
    End Sub

    Public Sub Gravar()
        Dados.GravarChamado(Me)
    End Sub

    Public Sub Excluir()
        Dados.ExcluirChamado(_ID)
    End Sub

End Class
