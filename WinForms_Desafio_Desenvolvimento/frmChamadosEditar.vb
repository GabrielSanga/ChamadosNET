Imports System.Net

Public Class frmChamadosEditar

#Region "Variables"

    Public idChamado As Integer = 0

#End Region

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(idChamado As Integer)
        InitializeComponent()

        Me.idChamado = idChamado
    End Sub

#End Region

#Region "Events"

    Public Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtDepartamentos As DataTable = Dados.ListarDepartamentos()
        cmbDepartamento.DataSource = dtDepartamentos
        cmbDepartamento.DisplayMember = "Descricao"
        cmbDepartamento.ValueMember = "ID"

        If idChamado > 0 Then
            Dim objChamado As Chamado = Dados.ObterChamado(idChamado)

            txtID.Text = objChamado.ID.ToString
            txtAssunto.Text = objChamado.Assunto
            txtSolicitante.Text = objChamado.Solicitante
            cmbDepartamento.SelectedValue = objChamado.Departamento
            dtpDataAbertura.Value = objChamado.DataAbertura
        End If
    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            Dim objChamado As New Chamado(Util.nInt(txtID.Text), txtAssunto.Text, txtSolicitante.Text, Util.nInt(cmbDepartamento.SelectedValue), dtpDataAbertura.Value)

            objChamado.Gravar()

            DialogResult = DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(Me, "Falha ao gravar o chamado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DialogResult = DialogResult.Cancel
        Finally
            Close()
        End Try
    End Sub

#End Region

End Class