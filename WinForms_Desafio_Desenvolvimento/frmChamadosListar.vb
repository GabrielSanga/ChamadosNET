Imports System.Data
Imports System.Windows.Forms

Public Class frmChamadosListar

#Region "Methods"

    Private Sub ListarChamados()
        Dim dtChamados As DataTable = Dados.ListarChamados()
        Me.dgvChamados.DataSource = dtChamados
    End Sub

    Private Sub OpenChamado(Optional idChamado As Integer = 0)
        Dim frm As New frmChamadosEditar()

        If idChamado > 0 Then
            frm.AbrirChamado(idChamado)
        End If

        Dim dlgResult As DialogResult = frm.ShowDialog()

        If dlgResult = DialogResult.OK Then
            ListarChamados()
        End If
    End Sub

#End Region

#Region "Events"
    Private Sub frmChamadosListar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarChamados()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click

        If Me.dgvChamados.SelectedRows.Count = 0 Then Exit Sub

        ' ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

        Dim dgvr As DataGridViewRow = Me.dgvChamados.SelectedRows(0)
        Dim drv As DataRowView = DirectCast(dgvr.DataBoundItem, DataRowView)

        Dim idChamado As Integer = CInt(drv("ID"))

        Dim dlgResult As DialogResult =
            MessageBox.Show(Me, $"Confirma a exclusão do Chamado nº {idChamado} ?",
                            Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dlgResult <> DialogResult.Yes Then Exit Sub

        ' ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

        Dim sucesso As Boolean = Dados.ExcluirChamado(idChamado)

        If sucesso Then ListarChamados()
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnAbrir.Click
        If Me.dgvChamados.SelectedRows.Count = 0 Then Exit Sub

        Dim dgvr As DataGridViewRow = Me.dgvChamados.SelectedRows(0)
        Dim drv As DataRowView = DirectCast(dgvr.DataBoundItem, DataRowView)

        Dim idChamado As Integer = CInt(drv("ID"))

        OpenChamado(idChamado)
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        OpenChamado()
    End Sub

    Private Sub btnRelatorio_Click(sender As Object, e As EventArgs) Handles btnRelatorio.Click

        Dim frm As New frmChamadosRelatorio()
        frm.ShowDialog()

    End Sub

    Private Sub dgvChamados_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChamados.CellDoubleClick
        If e.RowIndex = 0 Then
            Exit Sub
        End If

        Dim idChamado As Integer = 0

        Integer.TryParse(dgvChamados.Rows(e.RowIndex).Cells("ID").Value.ToString, idChamado)

        If idChamado = 0 Then
            Exit Sub
        End If

        OpenChamado(idChamado)
    End Sub

#End Region

End Class
