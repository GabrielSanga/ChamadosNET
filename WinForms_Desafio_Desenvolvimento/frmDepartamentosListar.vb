Imports System.Data
Imports System.Windows.Forms

Public Class frmDepartamentosListar

#Region "Methods"

    Public Sub ListarDepartamentos()
        Try
            Dim dtDepartamentos As DataTable = Dados.ListarDepartamentos()
            Me.dgvDepartamentos.DataSource = dtDepartamentos
        Catch ex As Exception
            MessageBox.Show(Me, "Falha ao listar os departamentos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub OpenDepartamentos(Optional idDepartamento As Integer = 0)
        Dim frm As New frmDepartamentoEditar(idDepartamento)

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
        If dgvDepartamentos.SelectedRows.Count = 0 Then Exit Sub

        Dim drv As DataRowView = DirectCast(dgvDepartamentos.SelectedRows(0).DataBoundItem, DataRowView)

        Dim objDepartamento As New Departamento With {.ID = Util.nInt(drv("ID"))}

        If MessageBox.Show(Me, $"Confirma a exclusão do Departamento nº {objDepartamento.ID} ?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Try
            objDepartamento.Excluir()

            ListarDepartamentos()
        Catch ex As Exception
            MessageBox.Show(Me, "Falha ao excluir o departamento.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnAbrir.Click
        If Me.dgvDepartamentos.SelectedRows.Count = 0 Then Exit Sub

        Dim drv As DataRowView = DirectCast(dgvDepartamentos.SelectedRows(0).DataBoundItem, DataRowView)

        Dim idDepartamento As Integer = Util.nInt(drv("ID"))

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

        Dim idDepartamento As Integer = Util.nInt(dgvDepartamentos.Rows(e.RowIndex).Cells("ID").Value)

        If idDepartamento = 0 Then
            Exit Sub
        End If

        OpenDepartamentos(idDepartamento)
    End Sub

#End Region

End Class
