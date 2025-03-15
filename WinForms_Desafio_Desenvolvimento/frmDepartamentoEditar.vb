Public Class frmDepartamentoEditar

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub AbrirDepartamento(idDepartamento As Integer)
        Dim drChamado As DataRow = Dados.ObeterDepartamento(idDepartamento)

        Me.txtID.Text = CInt(drChamado("ID")).ToString()
        Me.txtDescricao.Text = drChamado("Descricao").ToString
    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Dim ID As Integer = 0

        Integer.TryParse(Me.txtID.Text, ID)

        Dim descricao As String = Me.txtDescricao.Text

        Dim sucesso As Boolean = Dados.GravarDepartamento(ID, descricao)

        If Not sucesso Then
            MessageBox.Show(Me, "Falha ao gravar o departamento", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DialogResult = DialogResult.Cancel
        Else
            Me.DialogResult = DialogResult.OK
        End If

        Me.Close()
    End Sub

End Class