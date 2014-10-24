Public Class frmExample
    Private LinkID As String = "TESTER"
    Private SecretKey As String = "7l3axqlMVMwOKcAGrY65p8TKNJ4VYnP3Q4M7Hg39Ito="

    Private faxService As FaxService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        faxService = New FaxService(LinkID, SecretKey)
        faxService.IsTest = True

    End Sub

    Private Sub getPopbillURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles getPopbillURL.Click
        Dim url As String = faxService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text)

        MsgBox(url)
    End Sub

    Private Sub btnJoinMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJoinMember.Click
        Dim joinInfo As JoinForm = New JoinForm

        joinInfo.LinkID = LinkID
        joinInfo.CorpNum = "1231212312" '사업자번호 "-" 제외
        joinInfo.CEOName = "대표자성명"
        joinInfo.CorpName = "상호"
        joinInfo.Addr = "주소"
        joinInfo.ZipCode = "500-100"
        joinInfo.BizType = "업태"
        joinInfo.BizClass = "업종"
        joinInfo.ID = "userid"  '6자 이상 20자 미만
        joinInfo.PWD = "pwd_must_be_long_enough" '6자 이상 20자 미만
        joinInfo.ContactName = "담당자명"
        joinInfo.ContactTEL = "02-999-9999"
        joinInfo.ContactHP = "010-1234-5678"
        joinInfo.ContactFAX = "02-999-9998"
        joinInfo.ContactEmail = "test@test.com"

        Try
            Dim response As Response = faxService.JoinMember(joinInfo)

            MsgBox(response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = faxService.GetBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = faxService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = faxService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox(response.code.ToString() + " | " + response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = faxService.GetUnitCost(txtCorpNum.Text)


            MsgBox(unitCost)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Function getReserveDT() As DateTime?
        If String.IsNullOrEmpty(txtReserveDT.Text) = False Then

            Return DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        End If

    End Function

    Private Sub btnCancelReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelReserve.Click
        Try
            Dim response As Response

            response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL.Click
        Try
            Dim url As String = faxService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX")

            MsgBox(url)

        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetFaxResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFaxResult.Click
        Try
            Dim ResultList As List(Of FaxResult) = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text)

            dataGridView1.DataSource = ResultList


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

  
    Private Sub btnSenFax_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_1.Click

        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", "111-2222-3333", "수신자 명칭", strFileName, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox(ex.code.ToString() + " | " + ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSenFax_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_2.Click
        If fileDialog.ShowDialog(Me) = DialogResult.OK Then

            Dim strFileName As String = fileDialog.FileName

            Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

            For i As Integer = 0 To 99
                Dim receiver As FaxReceiver = New FaxReceiver
                receiver.receiveNum = "111-2222-3333"
                receiver.receiveName = "수신자명칭_" + CStr(i)
                receivers.Add(receiver)
            Next i

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", receivers, strFileName, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox(ex.code.ToString() + " | " + ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSenFax_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_3.Click
        Dim filepaths As List(Of String) = New List(Of String)

        Do While fileDialog.ShowDialog(Me) = DialogResult.OK
            filepaths.Add(fileDialog.FileName)
        Loop

        If filepaths.Count > 0 Then
          
            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", "111-2222-3333", "수신자 명칭", filepaths, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox(ex.code.ToString() + " | " + ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSenFax_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenFax_4.Click
        Dim filepaths As List(Of String) = New List(Of String)

        Do While fileDialog.ShowDialog(Me) = DialogResult.OK
            filepaths.Add(fileDialog.FileName)
        Loop

        If filepaths.Count > 0 Then
            Dim receivers As List(Of FaxReceiver) = New List(Of FaxReceiver)

            For i As Integer = 0 To 99
                Dim receiver As FaxReceiver = New FaxReceiver
                receiver.receiveNum = "111-2222-3333"
                receiver.receiveName = "수신자명칭_" + CStr(i)
                receivers.Add(receiver)
            Next i

            Try
                Dim receiptNum As String = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", receivers, filepaths, getReserveDT(), txtUserId.Text)

                MsgBox("접수번호 : " + receiptNum)
                txtReceiptNum.Text = receiptNum
            Catch ex As PopbillException
                MsgBox(ex.code.ToString() + " | " + ex.Message)
            End Try

        End If
    End Sub
End Class
