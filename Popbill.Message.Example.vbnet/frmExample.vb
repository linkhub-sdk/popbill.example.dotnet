Public Class frmExample
    Private LinkID As String = "TESTER"
    Private SecretKey As String = "UFWR16wurqHma8yrEsNzs+t83+A2DwWZ+PsFSnj36Hk="

    Private messageService As MessageService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        messageService = New MessageService(LinkID, SecretKey)
        messageService.IsTest = True

    End Sub

    Private Sub getPopbillURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles getPopbillURL.Click
        Dim url As String = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text)

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
            Dim response As Response = messageService.JoinMember(joinInfo)

            MsgBox(response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = messageService.GetBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = messageService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = messageService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox(response.code.ToString() + " | " + response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, MessageType.SMS)


            MsgBox(unitCost)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnUnitCost_LMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost_LMS.Click
        Try
            Dim unitCost As Single = messageService.GetUnitCost(txtCorpNum.Text, MessageType.LMS)


            MsgBox(unitCost)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendSMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_one.Click
        Try
            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, "07075106766", "11122223333", "수신자명칭", "단문 문자 메시지 내용. 90Byte", getReserveDT(), txtUserId.Text)

            MsgBox("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

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

            response = messageService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL.Click
        Try
            Dim url As String = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX")

            MsgBox(url)

        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetMessageResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMessageResult.Click
        Try
            Dim ResultList As List(Of MessageResult) = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text)

            dataGridView1.DataSource = ResultList


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btn_SendSMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendSMS_hund.Click
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99

            Dim msg As Message = New Message

            msg.sendNum = "07075106766"
            msg.receiveNum = "11122223333"
            msg.receiveName = "수신자명칭_" + CStr(i)
            msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            messages.Add(msg)
        Next

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum

        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendSMS_Same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS_Same.Click
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            msg.receiveNum = "11122223333"
            msg.receiveName = "수신자명칭_" + CStr(i)

            messages.Add(msg)
        Next

        Try

            Dim receiptNum As String = messageService.SendSMS(txtCorpNum.Text, "07075106766", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendLMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_one.Click
        Try
            Dim receiptNum As String = messageService.SendLMS( _
                                    txtCorpNum.Text, _
                                    "07075106766", _
                                    "11122223333", _
                                    "수신자명칭", _
                                    "장문문자 메시지 제목", _
                                    "장문 문자 메시지 내용. 2000Byte", _
                                    getReserveDT(), _
                                    txtUserId.Text)

            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendLMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_hund.Click
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            msg.sendNum = "07075106766"
            msg.receiveNum = "11122223333"
            msg.receiveName = "수신자명칭_" + CStr(i)
            msg.subject = "장문 문자메시지 제목"
            msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            messages.Add(msg)
        Next

        Try
            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendLMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendLMS_same.Click
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            msg.receiveNum = "11122223333"
            msg.receiveName = "수신자명칭_" + CStr(i)

            messages.Add(msg)
        Next
        Try

            Dim receiptNum As String = messageService.SendLMS(txtCorpNum.Text, "07075106766", "동보 메시지 제목", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendXMS_one_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_one.Click
        Try
            Dim receiptNum As String = messageService.SendXMS( _
                                    txtCorpNum.Text, _
                                    "07075106766", _
                                    "11122223333", _
                                    "수신자명칭", _
                                    "장문문자 메시지 제목", _
                                    "문자 메시지 내용. 메시지의 길이에 따라 90Byte를 기준으로 SMS/LMS를 선택전송", _
                                    getReserveDT(), _
                                    txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendXMS_hund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_hund.Click
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            msg.sendNum = "07075106766"
            msg.receiveNum = "11122223333"
            msg.receiveName = "수신자명칭_" + CStr(i)
            msg.subject = "문자메시지 제목"
            msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + CStr(i)

            messages.Add(msg)
        Next
        Try

            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnSendXMS_same_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendXMS_same.Click
        Dim messages As List(Of Message) = New List(Of Message)

        For i As Integer = 0 To 99
            Dim msg As Message = New Message

            msg.receiveNum = "11122223333"
            msg.receiveName = "수신자명칭_" + CStr(i)

            messages.Add(msg)
        Next
        Try

            Dim receiptNum As String = messageService.SendXMS(txtCorpNum.Text, "07075106766", "동보 메시지 제목", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text)
            MessageBox.Show("접수번호 : " + receiptNum)
            txtReceiptNum.Text = receiptNum
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub
End Class
