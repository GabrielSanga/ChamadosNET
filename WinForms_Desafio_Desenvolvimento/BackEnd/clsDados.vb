Imports System.Data
Imports System.Data.SQLite

Public Class Dados

    Private Const CONNECTION_STRING As String = "Data Source=Dados\DesafioDB.db;Version=3;"

    ' ------------------------------ CHAMADO ------------------------------

    Public Shared Function ListarChamados() As DataTable
        Dim dtChamados As New DataTable()

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                dbCommand.CommandText = "SELECT chamados.ID, " +
                                        "       Assunto, " +
                                        "       Solicitante, " +
                                        "       departamentos.Descricao AS Departamento, " +
                                        "       DataAbertura " +
                                        "FROM chamados " +
                                        "INNER JOIN departamentos " +
                                        "   ON chamados.Departamento = departamentos.ID "

                Using dbDataAdapter As New SQLiteDataAdapter(dbCommand)

                    dbDataAdapter.Fill(dtChamados)

                End Using

            End Using

        End Using

        Return dtChamados
    End Function

    Public Shared Function ObterChamado(idChamado As Integer) As Chamado
        Dim objChamado As New Chamado

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                dbCommand.CommandText = $"SELECT * FROM chamados WHERE ID = {idChamado}"

                Using dbDataAdapter As New SQLiteDataAdapter(dbCommand)
                    Dim dtChamados As New DataTable()
                    dbDataAdapter.Fill(dtChamados)

                    If dtChamados.Rows.Count > 0 Then
                        Dim ID As Integer = Util.nInt(dtChamados.Rows(0).Item("ID"))
                        Dim assunto As String = Util.sStr(dtChamados.Rows(0).Item("Assunto"))
                        Dim solicitante As String = Util.sStr(dtChamados.Rows(0).Item("Solicitante"))
                        Dim departamento As Integer = Util.nInt(dtChamados.Rows(0).Item("Departamento"))
                        Dim dataAbertura As DateTime = DateTime.Parse(CStr(dtChamados.Rows(0).Item("DataAbertura")))

                        objChamado = New Chamado(ID, assunto, solicitante, departamento, dataAbertura)
                    End If
                End Using

            End Using

        End Using

        Return objChamado
    End Function

    Public Shared Function GravarChamado(objChamado As Chamado) As Boolean
        Dim regsAfetados As Integer = -1

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                If objChamado.ID = 0 Then
                    dbCommand.CommandText = "INSERT INTO chamados (Assunto,Solicitante,Departamento,DataAbertura)" +
                                            "VALUES (@Assunto,@Solicitante,@Departamento,@DataAbertura)"
                Else
                    dbCommand.CommandText = "UPDATE chamados " +
                                            "SET Assunto=@Assunto, " +
                                            "    Solicitante=@Solicitante, " +
                                            "    Departamento=@Departamento, " +
                                            "    DataAbertura=@DataAbertura " +
                                            "WHERE ID=@ID "
                End If

                dbCommand.Parameters.AddWithValue("@Assunto", objChamado.Assunto)
                dbCommand.Parameters.AddWithValue("@Solicitante", objChamado.Solicitante)
                dbCommand.Parameters.AddWithValue("@Departamento", objChamado.Departamento)
                dbCommand.Parameters.AddWithValue("@DataAbertura", objChamado.DataAbertura.ToShortDateString())
                dbCommand.Parameters.AddWithValue("@ID", objChamado.ID)

                dbConnection.Open()
                regsAfetados = dbCommand.ExecuteNonQuery()
                dbConnection.Close()

            End Using

        End Using

        Return (regsAfetados > 0)
    End Function

    Public Shared Function ExcluirChamado(idChamado As Integer) As Boolean
        Dim regsAfetados As Integer = -1

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                dbCommand.CommandText = $"DELETE FROM chamados WHERE ID = {idChamado}"

                dbConnection.Open()
                regsAfetados = dbCommand.ExecuteNonQuery()
                dbConnection.Close()

            End Using

        End Using

        Return (regsAfetados > 0)
    End Function

    ' ------------------------------ DEPARTAMENTO ------------------------------

    Public Shared Function ListarDepartamentos() As DataTable
        Dim dtDepartamentos As New DataTable()

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                dbCommand.CommandText = "SELECT * FROM departamentos "

                Using dbDataAdapter As New SQLiteDataAdapter(dbCommand)

                    dbDataAdapter.Fill(dtDepartamentos)

                End Using

            End Using

        End Using

        Return dtDepartamentos
    End Function

    Public Shared Function ObterDepartamento(idDepartamento As Integer) As Departamento
        Dim objDepartamento As New Departamento

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                dbCommand.CommandText = $"SELECT * FROM departamentos WHERE ID = {idDepartamento}"

                Using dbDataAdapter As New SQLiteDataAdapter(dbCommand)

                    Dim dtDepartamento As New DataTable()
                    dbDataAdapter.Fill(dtDepartamento)

                    If dtDepartamento.Rows.Count > 0 Then
                        Dim ID As Integer = Util.nInt(dtDepartamento.Rows(0).Item("ID"))
                        Dim descricao As String = Util.sStr(dtDepartamento.Rows(0).Item("Descricao"))

                        objDepartamento = New Departamento(ID, descricao)
                    End If
                End Using

            End Using

        End Using

        Return objDepartamento
    End Function

    Public Shared Function GravarDepartamento(objDepartamento As Departamento) As Boolean
        Dim regsAfetados As Integer = -1

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                If objDepartamento.ID = 0 Then

                    dbCommand.CommandText = "INSERT INTO departamentos (Descricao)" +
                                            "VALUES (@Descricao)"

                Else

                    dbCommand.CommandText = "UPDATE departamentos " +
                                            "SET Descricao=@Descricao " +
                                            "WHERE ID=@ID "

                End If

                dbCommand.Parameters.AddWithValue("@Descricao", objDepartamento.Descricao)
                dbCommand.Parameters.AddWithValue("@ID", objDepartamento.ID)

                dbConnection.Open()
                regsAfetados = dbCommand.ExecuteNonQuery()
                dbConnection.Close()

            End Using

        End Using

        Return (regsAfetados > 0)
    End Function

    Public Shared Function ExcluirDepartamento(idDepartamento As Integer) As Boolean
        Dim regsAfetados As Integer = -1

        Using dbConnection As New SQLiteConnection(CONNECTION_STRING)

            Using dbCommand As SQLiteCommand = dbConnection.CreateCommand()

                dbCommand.CommandText = $"DELETE FROM departamentos WHERE ID = {idDepartamento}"

                dbConnection.Open()
                regsAfetados = dbCommand.ExecuteNonQuery()
                dbConnection.Close()

            End Using

        End Using

        Return (regsAfetados > 0)
    End Function

End Class
