Imports Popbill
Imports Popbill.Taxinvoice
Imports System.ComponentModel

Public Class frmExample

    Private LinkID As String = "TESTER"
    Private SecretKey As String = "2eQzzRuuygccucjyhFLMGkQEz7MzmDB9wA++aZROr1I="

    Private taxinvoiceService As TaxinvoiceService

    Private Sub frmExample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        taxinvoiceService = New TaxinvoiceService(LinkID, SecretKey)
        taxinvoiceService.IsTest = True


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles getPopbillURL.Click
        Dim url As String = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text)

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
            Dim response As Response = taxinvoiceService.JoinMember(joinInfo)

            MsgBox(response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetBalance.Click
        Try
            Dim remainPoint As Double = taxinvoiceService.GetBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnGetPartnerBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPartnerBalance.Click
        Try
            Dim remainPoint As Double = taxinvoiceService.GetPartnerBalance(txtCorpNum.Text)


            MsgBox(remainPoint)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnCheckIsMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIsMember.Click
        Try
            Dim response As Response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID)

            MsgBox(response.code.ToString() + " | " + response.message)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnUnitCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnitCost.Click
        Try
            Dim unitCost As Single = taxinvoiceService.GetUnitCost(txtCorpNum.Text)


            MsgBox(unitCost)


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCertificateExpireDate.Click
        Try
            Dim expiration As DateTime = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text)


            MsgBox(expiration.ToString())


        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)

        End Try
    End Sub

    Private Sub btnCheckMgtKeyInUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMgtKeyInUse.Click
        Dim KeyType As MgtKeyType


        KeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim InUse As Boolean = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            MsgBox(IIf(InUse, "사용중", "미사용중"))

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetEmailPublicKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailPublicKey.Click

        Try
            Dim KeyList As List(Of EmailPublicKey) = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text)

            MsgBox(KeyList.Count.ToString())

        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "정발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "1231212312"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호"
        taxinvoice.invoicerMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "8888888888"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = ""
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.email = "test2@invoicee.com"
        addContact.contactName = "추가담당자명"

        taxinvoice.addContactList.Add(addContact)


        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, False)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click, btnDelete_Reverse.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim Memo As String = "발행예정 메모"

        '발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송ㄴ
        Dim EmailSubject As String = "발행예정 메일제목 테스트 dotent 3.5"

        Try
            Dim response As Response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSend.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행예정 취소시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetDetailInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDetailInfo.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim taxinvoice As Taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            '자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

            Dim tmp As String = ""

            tmp += "InvoicerCorpNum : " + taxinvoice.invoicerCorpNum + vbCrLf
            tmp += "InvoicerCorpName : " + taxinvoice.invoicerCorpName + vbCrLf
            tmp += "InvoiceeCorpNum : " + taxinvoice.invoiceeCorpNum + vbCrLf
            tmp += "InvoiceeCorpName : " + taxinvoice.invoiceeCorpName + vbCrLf

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfo.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim taxinvoiceInfo As TaxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text)

            Dim tmp As String = ""

            tmp += "itemKey : " + taxinvoiceInfo.itemKey + vbCrLf
            tmp += "taxType : " + taxinvoiceInfo.taxType + vbCrLf
            tmp += "writeDate : " + taxinvoiceInfo.writeDate + vbCrLf
            tmp += "regDT : " + taxinvoiceInfo.regDT + vbCrLf

            tmp += "invoicerCorpName : " + taxinvoiceInfo.invoicerCorpName + vbCrLf
            tmp += "invoicerCorpNum : " + taxinvoiceInfo.invoicerCorpNum + vbCrLf
            tmp += "invoicerMgtKey : " + taxinvoiceInfo.invoicerMgtKey + vbCrLf
            tmp += "invoiceeCorpName : " + taxinvoiceInfo.invoiceeCorpName + vbCrLf
            tmp += "invoiceeCorpNum : " + taxinvoiceInfo.invoiceeCorpNum + vbCrLf
            tmp += "invoiceeMgtKey : " + taxinvoiceInfo.invoiceeMgtKey + vbCrLf
            tmp += "trusteeCorpName : " + taxinvoiceInfo.trusteeCorpName + vbCrLf
            tmp += "trusteeCorpNum : " + taxinvoiceInfo.trusteeCorpNum + vbCrLf
            tmp += "trusteeMgtKey : " + taxinvoiceInfo.trusteeMgtKey + vbCrLf

            tmp += "supplyCostTotal : " + taxinvoiceInfo.supplyCostTotal + vbCrLf
            tmp += "taxTotal : " + taxinvoiceInfo.taxTotal + vbCrLf
            tmp += "purposeType : " + taxinvoiceInfo.purposeType + vbCrLf
            tmp += "modifyCode : " + taxinvoiceInfo.modifyCode.ToString + vbCrLf
            tmp += "issueType : " + taxinvoiceInfo.issueType + vbCrLf

            tmp += "issueDT : " + taxinvoiceInfo.issueDT + vbCrLf
            tmp += "preIssueDT : " + taxinvoiceInfo.preIssueDT + vbCrLf

            tmp += "stateCode : " + taxinvoiceInfo.stateCode.ToString + vbCrLf
            tmp += "stateDT : " + taxinvoiceInfo.stateDT + vbCrLf

            tmp += "openYN : " + taxinvoiceInfo.openYN.ToString + vbCrLf
            tmp += "openDT : " + taxinvoiceInfo.openDT + vbCrLf
            tmp += "ntsresult : " + taxinvoiceInfo.ntsresult + vbCrLf
            tmp += "ntsconfirmNum : " + taxinvoiceInfo.ntsconfirmNum + vbCrLf
            tmp += "ntssendDT : " + taxinvoiceInfo.ntssendDT + vbCrLf
            tmp += "ntsresultDT : " + taxinvoiceInfo.ntsresultDT + vbCrLf
            tmp += "ntssendErrCode : " + taxinvoiceInfo.ntssendErrCode + vbCrLf
            tmp += "stateMemo : " + taxinvoiceInfo.stateMemo

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_TBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_TBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetURL_SBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_SBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_PBOX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_PBOX.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetURL_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetURL_WRITE.Click
        Try
            Dim url As String = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE")

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetLogs.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim logList As List(Of TaxinvoiceLog) = taxinvoiceService.GetLogs(txtCorpNum.Text, KeyType, txtMgtKey.Text)


            Dim tmp As String = ""


            For Each log As TaxinvoiceLog In logList
                tmp += log.docLogType.ToString + " | " + log.log + " | " + log.procType + " | " + log.procCorpName + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + vbCrLf
            Next

            MsgBox(tmp)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetInfos.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        ''최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim taxinvoiceInfoList As List(Of TaxinvoiceInfo) = taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList)

            ''TOGO Describe it.

            MsgBox(taxinvoiceInfoList.Count.ToString())

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try


    End Sub

    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        Try
            Dim response As Response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, "test@test.com", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "111-2222-4444", "발신문자 내용...", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendFAX.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "000-2222-4444", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnGetPopUpURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPopUpURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnEPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetEmailURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetEmailURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim url As String = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnGetMassPrintURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMassPrintURL.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim MgtKeyList As List(Of String) = New List(Of String)

        ''최대 1000건.
        MgtKeyList.Add("1234")
        MgtKeyList.Add("12345")

        Try
            Dim url As String = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text)

            MsgBox(url)
        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub

    Private Sub btnSendToNTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendToNTS.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssue.Click, btnIssue_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행시 메모", False, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIssue.Click, btnCancelIssue_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, "승인시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnDeny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeny.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, "거부시 메모.", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequest.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnCancelRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelRequest.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 취소시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRefuse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefuse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 거부시 메모", txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "정발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "1231212312"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호 수정"
        taxinvoice.invoicerMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "8888888888"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = ""
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        taxinvoice.addContactList = New List(Of TaxinvoiceAddContact)

        Dim addContact As TaxinvoiceAddContact = New TaxinvoiceAddContact

        addContact.email = "test2@invoicee.com"
        addContact.contactName = "추가담당자명"

        taxinvoice.addContactList.Add(addContact)


        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnRegister_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister_Reverse.Click
        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "역발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "8888888888"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호"
        taxinvoice.invoicerMgtKey = ""                  '공급자 발행까지 API로 발행하고자 할경우 정발행과 동일한 형태로 추가 기재.
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "1231212312"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        Try
            Dim response As Response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Reverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate_Reverse.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Dim taxinvoice As Taxinvoice = New Taxinvoice

        taxinvoice.writeDate = "20140923"               '필수, 기재상 작성일자
        taxinvoice.chargeDirection = "정과금"           '필수, {정과금, 역과금}
        taxinvoice.issueType = "역발행"                 '필수, {정발행, 역발행, 위수탁}
        taxinvoice.purposeType = "영수"                 '필수, {영수, 청구}
        taxinvoice.issueTiming = "직접발행"             '필수, {직접발행, 승인시자동발행}
        taxinvoice.taxType = "과세"                     '필수, {과세, 영세, 면세}


        taxinvoice.invoicerCorpNum = "8888888888"
        taxinvoice.invoicerTaxRegID = ""                '종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
        taxinvoice.invoicerCorpName = "공급자 상호 수정"
        taxinvoice.invoicerMgtKey = ""                  '공급자 발행까지 API로 발행하고자 할경우 정발행과 동일한 형태로 추가 기재.
        taxinvoice.invoicerCEOName = "공급자 대표자 성명"
        taxinvoice.invoicerAddr = "공급자 주소"
        taxinvoice.invoicerBizClass = "공급자 업종"
        taxinvoice.invoicerBizType = "공급자 업태,업태2"
        taxinvoice.invoicerContactName = "공급자 담당자명"
        taxinvoice.invoicerEmail = "test@test.com"
        taxinvoice.invoicerTEL = "070-7070-0707"
        taxinvoice.invoicerHP = "010-000-2222"
        taxinvoice.invoicerSMSSendYN = True             '발행시 문자발송기능 사용시 활용

        taxinvoice.invoiceeType = "사업자"
        taxinvoice.invoiceeCorpNum = "1231212312"
        taxinvoice.invoiceeCorpName = "공급받는자 상호"
        taxinvoice.invoiceeMgtKey = txtMgtKey.Text      '문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
        taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명"
        taxinvoice.invoiceeAddr = "공급받는자 주소"
        taxinvoice.invoiceeBizClass = "공급받는자 업종"
        taxinvoice.invoiceeBizType = "공급받는자 업태"
        taxinvoice.invoiceeContactName1 = "공급받는자 담당자명"
        taxinvoice.invoiceeEmail1 = "test@invoicee.com"

        taxinvoice.supplyCostTotal = "100000"           '필수 공급가액 합계"
        taxinvoice.taxTotal = "10000"                   '필수 세액 합계
        taxinvoice.totalAmount = "110000"               '필수 합계금액.  공급가액 + 세액

        taxinvoice.modifyCode = Nothing                  '수정세금계산서 작성시 1~6까지 선택기재.
        taxinvoice.originalTaxinvoiceKey = ""           '수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
        taxinvoice.serialNum = "123"
        taxinvoice.cash = ""                            '현금
        taxinvoice.chkBill = ""                         '수표
        taxinvoice.note = ""                            '어음
        taxinvoice.credit = ""                          '외상미수금
        taxinvoice.remark1 = "비고1"
        taxinvoice.remark2 = "비고2"
        taxinvoice.remark3 = "비고3"
        taxinvoice.kwon = 1
        taxinvoice.ho = 1

        taxinvoice.businessLicenseYN = False            '사업자등록증 이미지 첨부시 설정.
        taxinvoice.bankBookYN = False                   '통장사본 이미지 첨부시 설정.
        taxinvoice.faxreceiveNum = ""                   '발행시 Fax발송기능 사용시 수신번호 기재.
        taxinvoice.faxsendYN = False                    '발행시 Fax발송시 설정.

        taxinvoice.detailList = New List(Of TaxinvoiceDetail)

        Dim detail As TaxinvoiceDetail = New TaxinvoiceDetail

        detail.serialNum = 1                            '일련번호
        detail.purchaseDT = "20140319"                  '거래일자
        detail.itemName = "품목명"
        detail.spec = "규격"
        detail.qty = "1"                                '수량
        detail.unitCost = "100000"                      '단가
        detail.supplyCost = "100000"                    '공급가액
        detail.tax = "10000"                            '세액
        detail.remark = "품목비고"

        taxinvoice.detailList.Add(detail)

        detail = New TaxinvoiceDetail

        detail.serialNum = 2
        detail.itemName = "품목명"

        taxinvoice.detailList.Add(detail)

        Try
            Dim response As Response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text)

            MsgBox(response.message)
        Catch ex As PopbillException
            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)


        If fileDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim strFileName As String = fileDialog.FileName


            Try
                Dim response As Response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text)

                MsgBox(response.message)
            Catch ex As PopbillException
                MsgBox(ex.code.ToString() + " | " + ex.Message)
            End Try
         
        End If

    End Sub

    Private Sub gtnGetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gtnGetFiles.Click

        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim fileList As List(Of AttachedFile) = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text)

       
            Dim tmp As String = "일련번호 | 표시명 | 파일아이디 | 등록일자" + vbCrLf

            For Each file As AttachedFile In fileList
                tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + vbCrLf

            Next
            MsgBox(tmp)


        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim KeyType As MgtKeyType = [Enum].Parse(GetType(MgtKeyType), cboMgtKeyType.Text)

        Try
            Dim response As Response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text)

            MsgBox(response.message)

        Catch ex As PopbillException

            MsgBox(ex.code.ToString() + " | " + ex.Message)
        End Try

    End Sub
End Class
