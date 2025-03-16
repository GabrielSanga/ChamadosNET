Public Class Util

    ''' <summary>
    ''' Converte um objeto em inteiro
    ''' </summary>
    ''' <param name="pObject"></param>
    ''' <returns></returns>
    Public Shared Function nInt(pObject As Object) As Integer
        Try
            If pObject Is DBNull.Value OrElse pObject Is Nothing Then Return 0

            Return CInt(pObject)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Converte um objeto em string
    ''' </summary>
    ''' <param name="pObject"></param>
    ''' <returns></returns>
    Public Shared Function sStr(pObject As Object) As String
        If pObject Is DBNull.Value OrElse pObject Is Nothing Then Return ""

        Return CStr(pObject)
    End Function

End Class
