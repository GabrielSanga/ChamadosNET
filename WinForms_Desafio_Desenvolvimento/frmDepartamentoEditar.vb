Public Class frmDepartamentoEditar

#Region "Variables"

    Public idDepartamento As Integer = 0

#End Region

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(idDepartamento As Integer)
        InitializeComponent()

        Me.idDepartamento = idDepartamento
    End Sub

#End Region

#Region "Events"

    Public Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.idDepartamento > 0 Then
            Dim objDepartamento As Departamento = Dados.ObterDepartamento(idDepartamento)

            txtID.Text = objDepartamento.ID.ToString()
            txtDescricao.Text = objDepartamento.Descricao
        End If
    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Dim objChamado As New Departamento(Util.nInt(txtID.Text), txtDescricao.Text)

        Try
            objChamado.Gravar()

            DialogResult = DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(Me, "Falha ao gravar o departamento.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DialogResult = DialogResult.Cancel
        Finally
            Close()
        End Try
    End Sub

#End Region

End Class