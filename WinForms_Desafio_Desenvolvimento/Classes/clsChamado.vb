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
        Dim mensagem As String = ""

        If Not Valida(mensagem) Then
            Throw New Exception(mensagem)
        End If

        Dados.GravarChamado(Me)
    End Sub

    Public Sub Excluir()
        Dados.ExcluirChamado(_ID)
    End Sub

    Public Function Valida(ByRef mensagem As String) As Boolean
        If _Assunto.Length = 0 Then
            mensagem = "Assunto é campo de preenchimento obrigatório." : Return False
        End If

        If _Solicitante.Length = 0 Then
            mensagem = "Solicitante é campo de preenchimento obrigatório." : Return False
        End If

        If _Departamento = 0 Then
            mensagem = "Departamento é campo de preenchimento obrigatório." : Return False
        End If

        If _DataAbertura = DateTime.MinValue Then
            mensagem = "Data Abertura é campo de preenchimento obrigatório." : Return False
        End If

        'Caso inclusão e data abertura retroativa
        If ID = 0 AndAlso DataAbertura.Date < DateTime.Now.Date Then
            mensagem = "Não é permitido criar chamado com data retroativa." : Return False
        End If

        Return True
    End Function

End Class
