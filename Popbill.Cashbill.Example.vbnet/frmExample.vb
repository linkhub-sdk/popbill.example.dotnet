Imports Popbill
Imports Popbill.Cashbill
Imports System.ComponentModel

Public Class frmExample

    Private LinkID As String = "TESTER"
    Private SecretKey As String = "Oafp98tjXpqjzPZRBL9lB1RsXR9zodOxCoPue7PfsQc="

    Private cashbillService As CashbillService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cashbillService = New CashbillService(LinkID, SecretKey)
        cashbillService.IsTest = True


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles getPopbillURL.Click
        Dim url As String = cashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text)

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
        joinInfo.Bizclass = "업종"
        joinInfo.ID = "userid"  '6자 이상 20자 미만
        joinInfo.PWD = "pwd_must_be_long_enough" '6자 이상 20자 미만
        joinInfo.ContactName = "담당자명"
        joinInfo.ContactTEL = "02-999-9999"
        joinInfo.ContactHP = "010-1234-5678"
        joinInfo.ContactFAX = "02-999-9998"
        joinInfo.ContactEmail = "test@test.com"

        Try
            Dim response As Response = cashbillService.JoinMember(joinInfo)

            MsgBox(response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = cashbillService.GetBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = cashbillService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = cashbillService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox(response.code.ToString() + " | " + response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = cashbillService.GetUnitCost(txtCorpNum.Text)


            MsgBox(unitCost)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click
      
        Try
            Dim InUse As Boolean = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

   

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim cashbill As Cashbill = New Cashbill



        cashbill.mgtKey = txtMgtKey.Text        '발행자별 고유번호 할당, 1~24자리 영문,숫자조합으로 중복없이 구성.
        cashbill.tradeType = "승인거래"         '승인거래 or 취소거래
        cashbill.franchiseCorpNum = txtCorpNum.Text
        cashbill.franchiseCorpName = "발행자 상호"
        cashbill.franchiseCEOName = "발행자 대표자"
        cashbill.franchiseAddr = "발행자 주소"
        cashbill.franchiseTEL = "070-1234-1234"
        cashbill.identityNum = "01041680206"
        cashbill.customerName = "고객명"
        cashbill.itemName = "상품명"
        cashbill.orderNumber = "주문번호"
        cashbill.email = "test@test.com"
        cashbill.hp = "111-1234-1234"
        cashbill.fax = "777-444-3333"
        cashbill.serviceFee = "0"
        cashbill.supplyCost = "10000"
        cashbill.tax = "1000"
        cashbill.totalAmount = "11000"
        cashbill.tradeUsage = "소득공제용"      '소득공제용 or 지출증빙용
        cashbill.taxationType = "과세"          '과세 or 비과세

        cashbill.smssendYN = False


        Try
            Dim response As Response = cashbillService.Register(txtCorpNum.Text, cashbill, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Try
            Dim response As Response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub


    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click
   
        Try
            Dim cashbill As Cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text, txtMgtKey.Text)

            '자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

            Dim tmp As String = ""

            tmp += "franchiseCorpNum : " + cashbill.franchiseCorpNum + vbCrLf
            tmp += "franchiseCorpName : " + cashbill.franchiseCorpName + vbCrLf
            tmp += "identityNum : " + cashbill.identityNum + vbCrLf
            tmp += "customerName : " + cashbill.customerName + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click
     
        Try
            Dim cashbillInfo As CashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey : " + cashbillInfo.itemKey + vbCrLf
            tmp += "mgtKey : " + cashbillInfo.mgtKey + vbCrLf
            tmp += "tradeDate : " + cashbillInfo.tradeDate + vbCrLf
            tmp += "issueDT : " + cashbillInfo.issueDT + vbCrLf
            tmp += "customerName : " + cashbillInfo.customerName + vbCrLf
            tmp += "itemName : " + cashbillInfo.itemName + vbCrLf
            tmp += "identityNum : " + cashbillInfo.identityNum + vbCrLf
            tmp += "taxationType : " + cashbillInfo.taxationType + vbCrLf

            tmp += "totalAmount : " + cashbillInfo.totalAmount + vbCrLf
            tmp += "tradeUsage : " + cashbillInfo.tradeUsage + vbCrLf
            tmp += "tradeType : " + cashbillInfo.tradeType + vbCrLf
            tmp += "stateCode : " + CStr(cashbillInfo.stateCode) + vbCrLf
            tmp += "stateDT : " + cashbillInfo.stateDT + vbCrLf
            tmp += "printYN : " + CStr(cashbillInfo.printYN) + vbCrLf

            tmp += "confirmNum : " + cashbillInfo.confirmNum + vbCrLf
            tmp += "orgTradeDate : " + cashbillInfo.orgTradeDate + vbCrLf
            tmp += "orgConfirmNum : " + cashbillInfo.orgConfirmNum + vbCrLf

            tmp += "ntssendDT : " + cashbillInfo.ntssendDT + vbCrLf
            tmp += "ntsresult : " + cashbillInfo.ntsresult + vbCrLf
            tmp += "ntsresultDT : " + cashbillInfo.ntsresultDT + vbCrLf
            tmp += "ntsresultCode : " + cashbillInfo.ntsresultCode + vbCrLf
            tmp += "ntsresultMessage : " + cashbillInfo.ntsresultMessage + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click
        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click
        Try
            Dim url As String = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

     
        Try
            Dim logList As List(Of CashbillLog) = cashbillService.GetLogs(txtCorpNum.Text, txtMgtKey.Text)


            Dim tmp As String = ""


            For Each log As CashbillLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click
     
        Dim MgtKeyList As List(Of String) = New List(Of String)

        ''최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim cashbillInfoList As List(Of CashbillInfo) = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList)

            ''TOGO Describe it.

            MsgBox(cashbillInfoList.Count.ToString())

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try


    End Sub

    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
    

        Try
            Dim response As Response = cashbillService.SendEmail(txtCorpNum.Text, txtMgtKey.Text, "test@test.com", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
     
        Try
            Dim response As Response = cashbillService.SendSMS(txtCorpNum.Text, txtMgtKey.Text, "1111-2222", "111-2222-4444", "발신문자 내용...", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click
     
        Try
            Dim response As Response = cashbillService.SendFAX(txtCorpNum.Text, txtMgtKey.Text, "1111-2222", "000-2222-4444", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

     
        Try
            Dim url As String = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click


        Try
            Dim url As String = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEPrintURL.Click

    
        Try
            Dim url As String = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailURL.Click

      
        Try
            Dim url As String = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

     
        Dim MgtKeyList As List(Of String) = New List(Of String)

        ''최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim url As String = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

 

    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssue.Click
      
        Try
            Dim response As Response = cashbillService.Issue(txtCorpNum.Text, txtMgtKey.Text, "발행시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue.Click
     
        Try
            Dim response As Response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        Dim cashbill As Cashbill = New Cashbill

        cashbill.mgtKey = txtMgtKey.Text        '발행자별 고유번호 할당, 1~24자리 영문,숫자조합으로 중복없이 구성.
        cashbill.tradeType = "승인거래"         '승인거래 or 취소거래
        cashbill.franchiseCorpNum = txtCorpNum.Text
        cashbill.franchiseCorpName = "발행자 상호_수정"
        cashbill.franchiseCEOName = "발행자 대표자"
        cashbill.franchiseAddr = "발행자 주소"
        cashbill.franchiseTEL = "070-1234-1234"
        cashbill.identityNum = "01041680206"
        cashbill.customerName = "고객명"
        cashbill.itemName = "상품명"
        cashbill.orderNumber = "주문번호"
        cashbill.email = "test@test.com"
        cashbill.hp = "111-1234-1234"
        cashbill.fax = "777-444-3333"
        cashbill.serviceFee = "0"
        cashbill.supplyCost = "10000"
        cashbill.tax = "1000"
        cashbill.totalAmount = "11000"
        cashbill.tradeUsage = "소득공제용"     '소득공제용 or 지출증빙용
        cashbill.taxationType = "과세"          '과세 or 비과세

        cashbill.smssendYN = False


        Try
            Dim response As Response = cashbillService.Update(txtCorpNum.Text, txtMgtKey.Text, Cashbill, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

End Class
