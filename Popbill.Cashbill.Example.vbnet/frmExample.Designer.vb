<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExample
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.getPopbillURL = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cboPopbillTOGO = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnUnitCost = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.btnGetURL_WRITE = New System.Windows.Forms.Button
        Me.btnGetURL_PBOX = New System.Windows.Forms.Button
        Me.btnGetURL_TBOX = New System.Windows.Forms.Button
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.btnGetEmailURL = New System.Windows.Forms.Button
        Me.btnGetMassPrintURL = New System.Windows.Forms.Button
        Me.btnEPrintURL = New System.Windows.Forms.Button
        Me.btnGetPrintURL = New System.Windows.Forms.Button
        Me.btnGetPopUpURL = New System.Windows.Forms.Button
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.btnSendFAX = New System.Windows.Forms.Button
        Me.btnSendSMS = New System.Windows.Forms.Button
        Me.btnSendEmail = New System.Windows.Forms.Button
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.btnGetInfos = New System.Windows.Forms.Button
        Me.btnGetLogs = New System.Windows.Forms.Button
        Me.btnGetInfo = New System.Windows.Forms.Button
        Me.btnGetDetailInfo = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.btnCancelIssue = New System.Windows.Forms.Button
        Me.btnIssue = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.btnRegister = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnCheckMgtKeyInUse = New System.Windows.Forms.Button
        Me.txtMgtKey = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'getPopbillURL
        '
        Me.getPopbillURL.Location = New System.Drawing.Point(6, 51)
        Me.getPopbillURL.Name = "getPopbillURL"
        Me.getPopbillURL.Size = New System.Drawing.Size(118, 26)
        Me.getPopbillURL.TabIndex = 0
        Me.getPopbillURL.Text = "팝빌 URL 확인"
        Me.getPopbillURL.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "사업자번호 : "
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(85, 4)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 2
        Me.txtCorpNum.Text = "1231212312"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(247, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "팝빌아이디 :"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(326, 4)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 4
        Me.txtUserId.Text = "userid"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(568, 106)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnGetPartnerBalance)
        Me.GroupBox6.Location = New System.Drawing.Point(421, 17)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(131, 83)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "파트너 관련"
        '
        'btnGetPartnerBalance
        '
        Me.btnGetPartnerBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetPartnerBalance.Name = "btnGetPartnerBalance"
        Me.btnGetPartnerBalance.Size = New System.Drawing.Size(118, 26)
        Me.btnGetPartnerBalance.TabIndex = 3
        Me.btnGetPartnerBalance.Text = "파트너포인트 확인"
        Me.btnGetPartnerBalance.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cboPopbillTOGO)
        Me.GroupBox5.Controls.Add(Me.getPopbillURL)
        Me.GroupBox5.Location = New System.Drawing.Point(282, 17)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 83)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "기타"
        '
        'cboPopbillTOGO
        '
        Me.cboPopbillTOGO.FormattingEnabled = True
        Me.cboPopbillTOGO.Items.AddRange(New Object() {"LOGIN", "CHRG", "CERT"})
        Me.cboPopbillTOGO.Location = New System.Drawing.Point(6, 20)
        Me.cboPopbillTOGO.Name = "cboPopbillTOGO"
        Me.cboPopbillTOGO.Size = New System.Drawing.Size(118, 20)
        Me.cboPopbillTOGO.TabIndex = 1
        Me.cboPopbillTOGO.Text = "LOGIN"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnUnitCost)
        Me.GroupBox3.Controls.Add(Me.btnGetBalance)
        Me.GroupBox3.Location = New System.Drawing.Point(145, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(131, 83)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnUnitCost
        '
        Me.btnUnitCost.Location = New System.Drawing.Point(6, 51)
        Me.btnUnitCost.Name = "btnUnitCost"
        Me.btnUnitCost.Size = New System.Drawing.Size(118, 26)
        Me.btnUnitCost.TabIndex = 3
        Me.btnUnitCost.Text = "요금 단가 확인"
        Me.btnUnitCost.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(6, 19)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(118, 26)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCheckIsMember)
        Me.GroupBox2.Controls.Add(Me.btnJoinMember)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(131, 83)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "회원 정보"
        '
        'btnCheckIsMember
        '
        Me.btnCheckIsMember.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckIsMember.Name = "btnCheckIsMember"
        Me.btnCheckIsMember.Size = New System.Drawing.Size(118, 26)
        Me.btnCheckIsMember.TabIndex = 2
        Me.btnCheckIsMember.Text = "가입여부 확인"
        Me.btnCheckIsMember.UseVisualStyleBackColor = True
        '
        'btnJoinMember
        '
        Me.btnJoinMember.Location = New System.Drawing.Point(6, 51)
        Me.btnJoinMember.Name = "btnJoinMember"
        Me.btnJoinMember.Size = New System.Drawing.Size(118, 26)
        Me.btnJoinMember.TabIndex = 1
        Me.btnJoinMember.Text = "회원 가입"
        Me.btnJoinMember.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.GroupBox13)
        Me.GroupBox7.Controls.Add(Me.GroupBox12)
        Me.GroupBox7.Controls.Add(Me.GroupBox11)
        Me.GroupBox7.Controls.Add(Me.GroupBox9)
        Me.GroupBox7.Controls.Add(Me.GroupBox8)
        Me.GroupBox7.Controls.Add(Me.btnCheckMgtKeyInUse)
        Me.GroupBox7.Controls.Add(Me.txtMgtKey)
        Me.GroupBox7.Controls.Add(Me.Label3)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 151)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(568, 420)
        Me.GroupBox7.TabIndex = 6
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "현금영수증 관련 API"
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.btnGetURL_WRITE)
        Me.GroupBox13.Controls.Add(Me.btnGetURL_PBOX)
        Me.GroupBox13.Controls.Add(Me.btnGetURL_TBOX)
        Me.GroupBox13.Location = New System.Drawing.Point(445, 223)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(115, 154)
        Me.GroupBox13.TabIndex = 12
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "기타 URL"
        '
        'btnGetURL_WRITE
        '
        Me.btnGetURL_WRITE.Location = New System.Drawing.Point(7, 84)
        Me.btnGetURL_WRITE.Name = "btnGetURL_WRITE"
        Me.btnGetURL_WRITE.Size = New System.Drawing.Size(102, 26)
        Me.btnGetURL_WRITE.TabIndex = 11
        Me.btnGetURL_WRITE.Text = "매출작성"
        Me.btnGetURL_WRITE.UseVisualStyleBackColor = True
        '
        'btnGetURL_PBOX
        '
        Me.btnGetURL_PBOX.Location = New System.Drawing.Point(6, 52)
        Me.btnGetURL_PBOX.Name = "btnGetURL_PBOX"
        Me.btnGetURL_PBOX.Size = New System.Drawing.Size(102, 26)
        Me.btnGetURL_PBOX.TabIndex = 9
        Me.btnGetURL_PBOX.Text = "발행보관함"
        Me.btnGetURL_PBOX.UseVisualStyleBackColor = True
        '
        'btnGetURL_TBOX
        '
        Me.btnGetURL_TBOX.Location = New System.Drawing.Point(6, 20)
        Me.btnGetURL_TBOX.Name = "btnGetURL_TBOX"
        Me.btnGetURL_TBOX.Size = New System.Drawing.Size(102, 26)
        Me.btnGetURL_TBOX.TabIndex = 8
        Me.btnGetURL_TBOX.Text = "연동문서함"
        Me.btnGetURL_TBOX.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.btnGetEmailURL)
        Me.GroupBox12.Controls.Add(Me.btnGetMassPrintURL)
        Me.GroupBox12.Controls.Add(Me.btnEPrintURL)
        Me.GroupBox12.Controls.Add(Me.btnGetPrintURL)
        Me.GroupBox12.Controls.Add(Me.btnGetPopUpURL)
        Me.GroupBox12.Location = New System.Drawing.Point(247, 225)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(192, 182)
        Me.GroupBox12.TabIndex = 11
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "문서관련 URL 기능"
        '
        'btnGetEmailURL
        '
        Me.btnGetEmailURL.Location = New System.Drawing.Point(6, 148)
        Me.btnGetEmailURL.Name = "btnGetEmailURL"
        Me.btnGetEmailURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetEmailURL.TabIndex = 13
        Me.btnGetEmailURL.Text = "이메일의 보기 버튼 URL"
        Me.btnGetEmailURL.UseVisualStyleBackColor = True
        '
        'btnGetMassPrintURL
        '
        Me.btnGetMassPrintURL.Location = New System.Drawing.Point(6, 116)
        Me.btnGetMassPrintURL.Name = "btnGetMassPrintURL"
        Me.btnGetMassPrintURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetMassPrintURL.TabIndex = 12
        Me.btnGetMassPrintURL.Text = "대량인쇄 팝업 URL"
        Me.btnGetMassPrintURL.UseVisualStyleBackColor = True
        '
        'btnEPrintURL
        '
        Me.btnEPrintURL.Location = New System.Drawing.Point(6, 84)
        Me.btnEPrintURL.Name = "btnEPrintURL"
        Me.btnEPrintURL.Size = New System.Drawing.Size(180, 26)
        Me.btnEPrintURL.TabIndex = 11
        Me.btnEPrintURL.Text = "공급받는자 인쇄 팝업 URL"
        Me.btnEPrintURL.UseVisualStyleBackColor = True
        '
        'btnGetPrintURL
        '
        Me.btnGetPrintURL.Location = New System.Drawing.Point(6, 52)
        Me.btnGetPrintURL.Name = "btnGetPrintURL"
        Me.btnGetPrintURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetPrintURL.TabIndex = 10
        Me.btnGetPrintURL.Text = "인쇄 팝업 URL"
        Me.btnGetPrintURL.UseVisualStyleBackColor = True
        '
        'btnGetPopUpURL
        '
        Me.btnGetPopUpURL.Location = New System.Drawing.Point(6, 20)
        Me.btnGetPopUpURL.Name = "btnGetPopUpURL"
        Me.btnGetPopUpURL.Size = New System.Drawing.Size(180, 26)
        Me.btnGetPopUpURL.TabIndex = 9
        Me.btnGetPopUpURL.Text = "문서 내용보기 팝업 URL"
        Me.btnGetPopUpURL.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.btnSendFAX)
        Me.GroupBox11.Controls.Add(Me.btnSendSMS)
        Me.GroupBox11.Controls.Add(Me.btnSendEmail)
        Me.GroupBox11.Location = New System.Drawing.Point(134, 223)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(107, 154)
        Me.GroupBox11.TabIndex = 10
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "부가서비스"
        '
        'btnSendFAX
        '
        Me.btnSendFAX.Location = New System.Drawing.Point(7, 84)
        Me.btnSendFAX.Name = "btnSendFAX"
        Me.btnSendFAX.Size = New System.Drawing.Size(92, 26)
        Me.btnSendFAX.TabIndex = 10
        Me.btnSendFAX.Text = "팩스 전송"
        Me.btnSendFAX.UseVisualStyleBackColor = True
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Location = New System.Drawing.Point(7, 52)
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(92, 26)
        Me.btnSendSMS.TabIndex = 9
        Me.btnSendSMS.Text = "문자 전송"
        Me.btnSendSMS.UseVisualStyleBackColor = True
        '
        'btnSendEmail
        '
        Me.btnSendEmail.Location = New System.Drawing.Point(7, 20)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(92, 26)
        Me.btnSendEmail.TabIndex = 8
        Me.btnSendEmail.Text = "이메일 전송"
        Me.btnSendEmail.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnGetInfos)
        Me.GroupBox9.Controls.Add(Me.btnGetLogs)
        Me.GroupBox9.Controls.Add(Me.btnGetInfo)
        Me.GroupBox9.Controls.Add(Me.btnGetDetailInfo)
        Me.GroupBox9.Location = New System.Drawing.Point(12, 223)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(116, 154)
        Me.GroupBox9.TabIndex = 8
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "문서 정보"
        '
        'btnGetInfos
        '
        Me.btnGetInfos.Location = New System.Drawing.Point(6, 116)
        Me.btnGetInfos.Name = "btnGetInfos"
        Me.btnGetInfos.Size = New System.Drawing.Size(104, 26)
        Me.btnGetInfos.TabIndex = 9
        Me.btnGetInfos.Text = "문서정보(대량)"
        Me.btnGetInfos.UseVisualStyleBackColor = True
        '
        'btnGetLogs
        '
        Me.btnGetLogs.Location = New System.Drawing.Point(6, 84)
        Me.btnGetLogs.Name = "btnGetLogs"
        Me.btnGetLogs.Size = New System.Drawing.Size(104, 26)
        Me.btnGetLogs.TabIndex = 8
        Me.btnGetLogs.Text = "문서이력"
        Me.btnGetLogs.UseVisualStyleBackColor = True
        '
        'btnGetInfo
        '
        Me.btnGetInfo.Location = New System.Drawing.Point(6, 20)
        Me.btnGetInfo.Name = "btnGetInfo"
        Me.btnGetInfo.Size = New System.Drawing.Size(104, 26)
        Me.btnGetInfo.TabIndex = 7
        Me.btnGetInfo.Text = "문서정보"
        Me.btnGetInfo.UseVisualStyleBackColor = True
        '
        'btnGetDetailInfo
        '
        Me.btnGetDetailInfo.Location = New System.Drawing.Point(6, 52)
        Me.btnGetDetailInfo.Name = "btnGetDetailInfo"
        Me.btnGetDetailInfo.Size = New System.Drawing.Size(104, 26)
        Me.btnGetDetailInfo.TabIndex = 6
        Me.btnGetDetailInfo.Text = "문서상세정보"
        Me.btnGetDetailInfo.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.btnCancelIssue)
        Me.GroupBox8.Controls.Add(Me.btnIssue)
        Me.GroupBox8.Controls.Add(Me.Button7)
        Me.GroupBox8.Controls.Add(Me.btnRegister)
        Me.GroupBox8.Controls.Add(Me.btnDelete)
        Me.GroupBox8.Controls.Add(Me.Label5)
        Me.GroupBox8.Controls.Add(Me.Label6)
        Me.GroupBox8.Controls.Add(Me.Label7)
        Me.GroupBox8.Controls.Add(Me.Label9)
        Me.GroupBox8.Location = New System.Drawing.Point(160, 53)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(220, 161)
        Me.GroupBox8.TabIndex = 7
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "현금영수증 발행 프로세스"
        '
        'btnCancelIssue
        '
        Me.btnCancelIssue.BackColor = System.Drawing.Color.LightCoral
        Me.btnCancelIssue.Location = New System.Drawing.Point(11, 119)
        Me.btnCancelIssue.Name = "btnCancelIssue"
        Me.btnCancelIssue.Size = New System.Drawing.Size(65, 25)
        Me.btnCancelIssue.TabIndex = 8
        Me.btnCancelIssue.Text = "발행취소"
        Me.btnCancelIssue.UseVisualStyleBackColor = False
        '
        'btnIssue
        '
        Me.btnIssue.BackColor = System.Drawing.Color.LightCoral
        Me.btnIssue.Location = New System.Drawing.Point(11, 70)
        Me.btnIssue.Name = "btnIssue"
        Me.btnIssue.Size = New System.Drawing.Size(65, 32)
        Me.btnIssue.TabIndex = 7
        Me.btnIssue.Text = "발행"
        Me.btnIssue.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.LightCoral
        Me.Button7.Location = New System.Drawing.Point(142, 23)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(50, 25)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "수정"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.LightCoral
        Me.btnRegister.Location = New System.Drawing.Point(86, 23)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(50, 25)
        Me.btnRegister.TabIndex = 0
        Me.btnRegister.Text = "등록"
        Me.btnRegister.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(137, 119)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(50, 25)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "삭제"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Silver
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label5.Size = New System.Drawing.Size(197, 37)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "임시저장"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(42, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label6.Size = New System.Drawing.Size(126, 1)
        Me.Label6.TabIndex = 14
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(42, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(1, 84)
        Me.Label7.TabIndex = 15
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.Location = New System.Drawing.Point(164, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Label9.Size = New System.Drawing.Size(1, 104)
        Me.Label9.TabIndex = 17
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCheckMgtKeyInUse
        '
        Me.btnCheckMgtKeyInUse.Location = New System.Drawing.Point(298, 11)
        Me.btnCheckMgtKeyInUse.Name = "btnCheckMgtKeyInUse"
        Me.btnCheckMgtKeyInUse.Size = New System.Drawing.Size(141, 26)
        Me.btnCheckMgtKeyInUse.TabIndex = 5
        Me.btnCheckMgtKeyInUse.Text = "관리번호 사용여부 확인"
        Me.btnCheckMgtKeyInUse.UseVisualStyleBackColor = True
        '
        'txtMgtKey
        '
        Me.txtMgtKey.Location = New System.Drawing.Point(151, 14)
        Me.txtMgtKey.Name = "txtMgtKey"
        Me.txtMgtKey.Size = New System.Drawing.Size(143, 21)
        Me.txtMgtKey.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(142, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "문서관리번호(MgtKey) : "
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "OpenFileDialog1"
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 578)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmExample"
        Me.Text = "팝빌 현금영수증 SDK VB.NET Example"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents getPopbillURL As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Friend WithEvents btnGetBalance As System.Windows.Forms.Button
    Friend WithEvents btnGetPartnerBalance As System.Windows.Forms.Button
    Friend WithEvents btnUnitCost As System.Windows.Forms.Button
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents cboPopbillTOGO As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMgtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCheckMgtKeyInUse As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetDetailInfo As System.Windows.Forms.Button
    Friend WithEvents btnGetInfo As System.Windows.Forms.Button
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetURL_WRITE As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_PBOX As System.Windows.Forms.Button
    Friend WithEvents btnGetURL_TBOX As System.Windows.Forms.Button
    Friend WithEvents btnGetInfos As System.Windows.Forms.Button
    Friend WithEvents btnGetLogs As System.Windows.Forms.Button
    Friend WithEvents btnSendFAX As System.Windows.Forms.Button
    Friend WithEvents btnSendSMS As System.Windows.Forms.Button
    Friend WithEvents btnSendEmail As System.Windows.Forms.Button
    Friend WithEvents btnGetEmailURL As System.Windows.Forms.Button
    Friend WithEvents btnGetMassPrintURL As System.Windows.Forms.Button
    Friend WithEvents btnEPrintURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPrintURL As System.Windows.Forms.Button
    Friend WithEvents btnGetPopUpURL As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnCancelIssue As System.Windows.Forms.Button
    Friend WithEvents btnIssue As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents fileDialog As System.Windows.Forms.OpenFileDialog

End Class
