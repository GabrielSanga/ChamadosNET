Imports System.Net

Public Class frmChamadosEditar

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Methods"

    Public Sub AbrirChamado(ByVal idChamado As Integer)
        Dim objChamado As Chamado = Dados.ObterChamado(idChamado)

        txtID.Text = objChamado.ID.ToString
        txtAssunto.Text = objChamado.Assunto
        txtSolicitante.Text = objChamado.Solicitante
        cmbDepartamento.SelectedValue = objChamado.Departamento
        dtpDataAbertura.Value = objChamado.DataAbertura
    End Sub

#End Region

#Region "Events"

    Public Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtDepartamentos As DataTable = Dados.ListarDepartamentos()
        cmbDepartamento.DataSource = dtDepartamentos
        cmbDepartamento.DisplayMember = "Descricao"
        cmbDepartamento.ValueMember = "ID"
    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Dim objChamado As New Chamado(Util.nInt(txtID.Text), txtAssunto.Text, txtSolicitante.Text, Util.nInt(cmbDepartamento.SelectedValue), dtpDataAbertura.Value)

        Dim sucesso As Boolean = Dados.GravarChamado(objChamado)

        If Not sucesso Then
            MessageBox.Show(Me, "Falha ao gravar o chamado", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DialogResult = DialogResult.Cancel
        Else
            DialogResult = DialogResult.OK
        End If

        Close()
    End Sub

#End Region

End Class