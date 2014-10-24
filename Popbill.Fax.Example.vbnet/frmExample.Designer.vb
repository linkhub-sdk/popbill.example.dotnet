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
        Me.btnCancelReserve = New System.Windows.Forms.Button
        Me.btnGetURL = New System.Windows.Forms.Button
        Me.dataGridView1 = New System.Windows.Forms.DataGridView
        Me.txtReserveDT = New System.Windows.Forms.TextBox
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.txtReceiptNum = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.btnGetFaxResult = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.cboPopbillTOGO = New System.Windows.Forms.ComboBox
        Me.btnGetPartnerBalance = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnUnitCost = New System.Windows.Forms.Button
        Me.btnGetBalance = New System.Windows.Forms.Button
        Me.getPopbillURL = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnCheckIsMember = New System.Windows.Forms.Button
        Me.btnJoinMember = New System.Windows.Forms.Button
        Me.groupBox4 = New System.Windows.Forms.GroupBox
        Me.btnSenFax_4 = New System.Windows.Forms.Button
        Me.btnSenFax_3 = New System.Windows.Forms.Button
        Me.btnSenFax_2 = New System.Windows.Forms.Button
        Me.btnSenFax_1 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCorpNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancelReserve
        '
        Me.btnCancelReserve.Location = New System.Drawing.Point(429, 102)
        Me.btnCancelReserve.Name = "btnCancelReserve"
        Me.btnCancelReserve.Size = New System.Drawing.Size(121, 24)
        Me.btnCancelReserve.TabIndex = 22
        Me.btnCancelReserve.Text = "예약 전송 취소"
        Me.btnCancelReserve.UseVisualStyleBackColor = True
        '
        'btnGetURL
        '
        Me.btnGetURL.Location = New System.Drawing.Point(429, 11)
        Me.btnGetURL.Name = "btnGetURL"
        Me.btnGetURL.Size = New System.Drawing.Size(121, 24)
        Me.btnGetURL.TabIndex = 20
        Me.btnGetURL.Text = "전송내역조회 팝업"
        Me.btnGetURL.UseVisualStyleBackColor = True
        '
        'dataGridView1
        '
        Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView1.Location = New System.Drawing.Point(14, 132)
        Me.dataGridView1.Name = "dataGridView1"
        Me.dataGridView1.RowTemplate.Height = 21
        Me.dataGridView1.Size = New System.Drawing.Size(539, 203)
        Me.dataGridView1.TabIndex = 23
        '
        'txtReserveDT
        '
        Me.txtReserveDT.Location = New System.Drawing.Point(196, 11)
        Me.txtReserveDT.Name = "txtReserveDT"
        Me.txtReserveDT.Size = New System.Drawing.Size(168, 21)
        Me.txtReserveDT.TabIndex = 14
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(326, 4)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(143, 21)
        Me.txtUserId.TabIndex = 21
        Me.txtUserId.Text = "userid"
        '
        'txtReceiptNum
        '
        Me.txtReceiptNum.Location = New System.Drawing.Point(73, 105)
        Me.txtReceiptNum.Name = "txtReceiptNum"
        Me.txtReceiptNum.Size = New System.Drawing.Size(143, 21)
        Me.txtReceiptNum.TabIndex = 17
        Me.txtReceiptNum.Text = "014102315000000005"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(12, 109)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(65, 12)
        Me.label4.TabIndex = 16
        Me.label4.Text = "접수번호 : "
        '
        'btnGetFaxResult
        '
        Me.btnGetFaxResult.Location = New System.Drawing.Point(302, 102)
        Me.btnGetFaxResult.Name = "btnGetFaxResult"
        Me.btnGetFaxResult.Size = New System.Drawing.Size(121, 24)
        Me.btnGetFaxResult.TabIndex = 21
        Me.btnGetFaxResult.Text = "전송상태확인"
        Me.btnGetFaxResult.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(10, 17)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(191, 12)
        Me.label3.TabIndex = 13
        Me.label3.Text = "예약시간(yyyyMMddHHmmss) : "
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
        'btnGetPartnerBalance
        '
        Me.btnGetPartnerBalance.Location = New System.Drawing.Point(143, 48)
        Me.btnGetPartnerBalance.Name = "btnGetPartnerBalance"
        Me.btnGetPartnerBalance.Size = New System.Drawing.Size(118, 26)
        Me.btnGetPartnerBalance.TabIndex = 3
        Me.btnGetPartnerBalance.Text = "파트너포인트 확인"
        Me.btnGetPartnerBalance.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGetPartnerBalance)
        Me.GroupBox3.Controls.Add(Me.btnUnitCost)
        Me.GroupBox3.Controls.Add(Me.btnGetBalance)
        Me.GroupBox3.Location = New System.Drawing.Point(145, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(272, 83)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "포인트 관련"
        '
        'btnUnitCost
        '
        Me.btnUnitCost.Location = New System.Drawing.Point(15, 16)
        Me.btnUnitCost.Name = "btnUnitCost"
        Me.btnUnitCost.Size = New System.Drawing.Size(118, 26)
        Me.btnUnitCost.TabIndex = 3
        Me.btnUnitCost.Text = "단가 확인"
        Me.btnUnitCost.UseVisualStyleBackColor = True
        '
        'btnGetBalance
        '
        Me.btnGetBalance.Location = New System.Drawing.Point(143, 16)
        Me.btnGetBalance.Name = "btnGetBalance"
        Me.btnGetBalance.Size = New System.Drawing.Size(118, 26)
        Me.btnGetBalance.TabIndex = 2
        Me.btnGetBalance.Text = "잔여포인트 확인"
        Me.btnGetBalance.UseVisualStyleBackColor = True
        '
        'getPopbillURL
        '
        Me.getPopbillURL.Location = New System.Drawing.Point(6, 48)
        Me.getPopbillURL.Name = "getPopbillURL"
        Me.getPopbillURL.Size = New System.Drawing.Size(118, 26)
        Me.getPopbillURL.TabIndex = 0
        Me.getPopbillURL.Text = "팝빌 URL 확인"
        Me.getPopbillURL.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cboPopbillTOGO)
        Me.GroupBox5.Controls.Add(Me.getPopbillURL)
        Me.GroupBox5.Location = New System.Drawing.Point(423, 17)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 83)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "기타"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(566, 106)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "팝빌 기본 API"
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
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.btnSenFax_4)
        Me.groupBox4.Controls.Add(Me.btnSenFax_3)
        Me.groupBox4.Controls.Add(Me.btnSenFax_2)
        Me.groupBox4.Controls.Add(Me.btnSenFax_1)
        Me.groupBox4.Controls.Add(Me.dataGridView1)
        Me.groupBox4.Controls.Add(Me.btnCancelReserve)
        Me.groupBox4.Controls.Add(Me.btnGetFaxResult)
        Me.groupBox4.Controls.Add(Me.btnGetURL)
        Me.groupBox4.Controls.Add(Me.txtReceiptNum)
        Me.groupBox4.Controls.Add(Me.label4)
        Me.groupBox4.Controls.Add(Me.txtReserveDT)
        Me.groupBox4.Controls.Add(Me.label3)
        Me.groupBox4.Location = New System.Drawing.Point(12, 143)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(565, 347)
        Me.groupBox4.TabIndex = 23
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "메시지 관련 기능"
        '
        'btnSenFax_4
        '
        Me.btnSenFax_4.Location = New System.Drawing.Point(372, 47)
        Me.btnSenFax_4.Name = "btnSenFax_4"
        Me.btnSenFax_4.Size = New System.Drawing.Size(113, 42)
        Me.btnSenFax_4.TabIndex = 31
        Me.btnSenFax_4.Text = "다수파일 동보전송"
        Me.btnSenFax_4.UseVisualStyleBackColor = True
        '
        'btnSenFax_3
        '
        Me.btnSenFax_3.Location = New System.Drawing.Point(268, 47)
        Me.btnSenFax_3.Name = "btnSenFax_3"
        Me.btnSenFax_3.Size = New System.Drawing.Size(98, 42)
        Me.btnSenFax_3.TabIndex = 30
        Me.btnSenFax_3.Text = "다수 파일 전송"
        Me.btnSenFax_3.UseVisualStyleBackColor = True
        '
        'btnSenFax_2
        '
        Me.btnSenFax_2.Location = New System.Drawing.Point(174, 47)
        Me.btnSenFax_2.Name = "btnSenFax_2"
        Me.btnSenFax_2.Size = New System.Drawing.Size(88, 42)
        Me.btnSenFax_2.TabIndex = 29
        Me.btnSenFax_2.Text = "동보 전송"
        Me.btnSenFax_2.UseVisualStyleBackColor = True
        '
        'btnSenFax_1
        '
        Me.btnSenFax_1.Location = New System.Drawing.Point(80, 47)
        Me.btnSenFax_1.Name = "btnSenFax_1"
        Me.btnSenFax_1.Size = New System.Drawing.Size(88, 42)
        Me.btnSenFax_1.TabIndex = 28
        Me.btnSenFax_1.Text = "전송"
        Me.btnSenFax_1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(247, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 12)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "팝빌아이디 :"
        '
        'txtCorpNum
        '
        Me.txtCorpNum.Location = New System.Drawing.Point(85, 4)
        Me.txtCorpNum.Name = "txtCorpNum"
        Me.txtCorpNum.Size = New System.Drawing.Size(143, 21)
        Me.txtCorpNum.TabIndex = 19
        Me.txtCorpNum.Text = "1231212312"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "사업자번호 : "
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "OpenFileDialog1"
        '
        'frmExample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 502)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCorpNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmExample"
        Me.Text = "팝빌 팩스 SDK VB.NET Example"
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnCancelReserve As System.Windows.Forms.Button
    Private WithEvents btnGetURL As System.Windows.Forms.Button
    Private WithEvents dataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtReserveDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiptNum As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents btnGetFaxResult As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents cboPopbillTOGO As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetPartnerBalance As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUnitCost As System.Windows.Forms.Button
    Friend WithEvents btnGetBalance As System.Windows.Forms.Button
    Friend WithEvents getPopbillURL As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCheckIsMember As System.Windows.Forms.Button
    Friend WithEvents btnJoinMember As System.Windows.Forms.Button
    Private WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCorpNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents btnSenFax_4 As System.Windows.Forms.Button
    Private WithEvents btnSenFax_3 As System.Windows.Forms.Button
    Private WithEvents btnSenFax_2 As System.Windows.Forms.Button
    Private WithEvents btnSenFax_1 As System.Windows.Forms.Button
    Friend WithEvents fileDialog As System.Windows.Forms.OpenFileDialog

End Class
