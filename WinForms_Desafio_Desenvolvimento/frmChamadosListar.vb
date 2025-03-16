Imports System.Data
Imports System.Windows.Forms

Public Class frmChamadosListar

#Region "Methods"

    Private Sub ListarChamados()
        Try
            Dim dtChamados As DataTable = Dados.ListarChamados()
            dgvChamados.DataSource = dtChamados
        Catch ex As Exception
            MessageBox.Show(Me, "Falha ao listar os chamados", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenChamado(Optional idChamado As Integer = 0)
        Dim frm As New frmChamadosEditar(idChamado)

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
        If dgvChamados.SelectedRows.Count = 0 Then Exit Sub

        Dim drv As DataRowView = DirectCast(dgvChamados.SelectedRows(0).DataBoundItem, DataRowView)

        Dim objChamado As New Chamado With {.ID = Util.nInt(drv("ID"))}

        If MessageBox.Show(Me, $"Confirma a exclusão do Chamado nº {objChamado.ID} ?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Try
            objChamado.Excluir()

            ListarChamados()
        Catch ex As Exception
            MessageBox.Show(Me, "Falha ao excluir o chamado", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnAbrir.Click
        If dgvChamados.SelectedRows.Count = 0 Then Exit Sub

        Dim drv As DataRowView = DirectCast(dgvChamados.SelectedRows(0).DataBoundItem, DataRowView)

        Dim idChamado As Integer = Util.nInt(drv("ID"))

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

        Dim idChamado As Integer = Util.nInt(dgvChamados.Rows(e.RowIndex).Cells("ID").Value)

        If idChamado = 0 Then
            Exit Sub
        End If

        OpenChamado(idChamado)
    End Sub

#End Region

End Class
