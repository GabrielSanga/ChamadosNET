Imports System.Data
Imports System.Windows.Forms

Public Class frmDepartamentosListar

#Region "Methods"

    Public Sub ListarDepartamentos()
        Dim dtDepartamentos As DataTable = Dados.ListarDepartamentos()
        Me.dgvDepartamentos.DataSource = dtDepartamentos
    End Sub

    Public Sub OpenDepartamentos(Optional idDepartamento As Integer = 0)
        Dim frm As New frmDepartamentoEditar()

        If idDepartamento > 0 Then
            frm.AbrirDepartamento(idDepartamento)
        End If

        Dim dlgResult As DialogResult = frm.ShowDialog()

        If dlgResult = DialogResult.OK Then
            ListarDepartamentos()
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub frmDepartamentoListar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarDepartamentos()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Me.dgvDepartamentos.SelectedRows.Count = 0 Then Exit Sub

        ' ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

        Dim dgvr As DataGridViewRow = Me.dgvDepartamentos.SelectedRows(0)
        Dim drv As DataRowView = DirectCast(dgvr.DataBoundItem, DataRowView)

        Dim idDepartamento As Integer = CInt(drv("ID"))

        Dim dlgResult As DialogResult =
            MessageBox.Show(Me, $"Confirma a exclusão do Departamento nº {idDepartamento} ?",
                            Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dlgResult <> DialogResult.Yes Then Exit Sub

        ' ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

        Dim sucesso As Boolean = Dados.ExcluirDepartamento(idDepartamento)

        If sucesso Then ListarDepartamentos()
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnAbrir.Click
        If Me.dgvDepartamentos.SelectedRows.Count = 0 Then Exit Sub

        Dim dgvr As DataGridViewRow = Me.dgvDepartamentos.SelectedRows(0)
        Dim drv As DataRowView = DirectCast(dgvr.DataBoundItem, DataRowView)

        Dim idDepartamento As Integer = CInt(drv("ID"))

        OpenDepartamentos(idDepartamento)
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        OpenDepartamentos()
    End Sub

    Private Sub btnRelatorio_Click(sender As Object, e As EventArgs) Handles btnRelatorio.Click
        Dim frm As New frmDepartamentosRelatorio()
        frm.ShowDialog()
    End Sub

    Private Sub dgvDepartamentos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDepartamentos.CellDoubleClick
        If e.RowIndex = 0 Then
            Exit Sub
        End If

        Dim idDepartamento As Integer = 0

        Integer.TryParse(dgvDepartamentos.Rows(e.RowIndex).Cells("ID").Value.ToString, idDepartamento)

        If idDepartamento = 0 Then
            Exit Sub
        End If

        OpenDepartamentos(idDepartamento)
    End Sub

#End Region

End Class
