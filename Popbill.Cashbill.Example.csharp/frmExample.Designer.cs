namespace Popbill.Cashbill.Example.csharp
{
    partial class frmExample
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetInfos = new System.Windows.Forms.Button();
            this.btnGetLogs = new System.Windows.Forms.Button();
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.GroupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetDetailInfo = new System.Windows.Forms.Button();
            this.btnGetEmailURL = new System.Windows.Forms.Button();
            this.btnGetMassPrintURL = new System.Windows.Forms.Button();
            this.btnSendFAX = new System.Windows.Forms.Button();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnCheckMgtKeyInUse = new System.Windows.Forms.Button();
            this.txtMgtKey = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.GroupBox11 = new System.Windows.Forms.GroupBox();
            this.btnAssignMgtKey = new System.Windows.Forms.Button();
            this.btnUpdateEmailConfig = new System.Windows.Forms.Button();
            this.btnListEmailConfig = new System.Windows.Forms.Button();
            this.btnUnitCost = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance1 = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.btnGetPopUpURL = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.GroupBox7 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnRevokeRegistIssue_part = new System.Windows.Forms.Button();
            this.btnCancelIssue02 = new System.Windows.Forms.Button();
            this.btnDelete02 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRevokRegistIssue = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnCancelIssueSub = new System.Windows.Forms.Button();
            this.btnRegistIssue = new System.Windows.Forms.Button();
            this.btnDeleteSub = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.GroupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetURL_WRITE = new System.Windows.Forms.Button();
            this.btnGetURL_SBOX = new System.Windows.Forms.Button();
            this.btnGetURL_TBOX = new System.Windows.Forms.Button();
            this.GroupBox12 = new System.Windows.Forms.GroupBox();
            this.btnGetViewURL = new System.Windows.Forms.Button();
            this.btnGetPDFURL = new System.Windows.Forms.Button();
            this.btnGetPrintURL = new System.Windows.Forms.Button();
            this.textURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.btnGetAccessURL = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.btnGetContactInfo = new System.Windows.Forms.Button();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnGetPartnerURL_CHRG = new System.Windows.Forms.Button();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.btnGetUseHistoryURL = new System.Windows.Forms.Button();
            this.btnGetPaymentURL = new System.Windows.Forms.Button();
            this.btnGetChargeURL = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox9.SuspendLayout();
            this.GroupBox11.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox7.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.GroupBox13.SuspendLayout();
            this.GroupBox12.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetInfos
            // 
            this.btnGetInfos.Location = new System.Drawing.Point(6, 53);
            this.btnGetInfos.Name = "btnGetInfos";
            this.btnGetInfos.Size = new System.Drawing.Size(104, 30);
            this.btnGetInfos.TabIndex = 9;
            this.btnGetInfos.Text = "문서정보(대량)";
            this.btnGetInfos.UseVisualStyleBackColor = true;
            this.btnGetInfos.Click += new System.EventHandler(this.btnGetInfos_Click);
            // 
            // btnGetLogs
            // 
            this.btnGetLogs.Location = new System.Drawing.Point(6, 118);
            this.btnGetLogs.Name = "btnGetLogs";
            this.btnGetLogs.Size = new System.Drawing.Size(104, 30);
            this.btnGetLogs.TabIndex = 8;
            this.btnGetLogs.Text = "문서이력";
            this.btnGetLogs.UseVisualStyleBackColor = true;
            this.btnGetLogs.Click += new System.EventHandler(this.btnGetLogs_Click);
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(6, 20);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(104, 30);
            this.btnGetInfo.TabIndex = 7;
            this.btnGetInfo.Text = "문서정보";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // GroupBox9
            // 
            this.GroupBox9.Controls.Add(this.btnSearch);
            this.GroupBox9.Controls.Add(this.btnGetInfos);
            this.GroupBox9.Controls.Add(this.btnGetLogs);
            this.GroupBox9.Controls.Add(this.btnGetInfo);
            this.GroupBox9.Controls.Add(this.btnGetDetailInfo);
            this.GroupBox9.Location = new System.Drawing.Point(29, 246);
            this.GroupBox9.Name = "GroupBox9";
            this.GroupBox9.Size = new System.Drawing.Size(116, 189);
            this.GroupBox9.TabIndex = 8;
            this.GroupBox9.TabStop = false;
            this.GroupBox9.Text = "문서 정보";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 150);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 30);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "문서목록조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGetDetailInfo
            // 
            this.btnGetDetailInfo.Location = new System.Drawing.Point(6, 86);
            this.btnGetDetailInfo.Name = "btnGetDetailInfo";
            this.btnGetDetailInfo.Size = new System.Drawing.Size(104, 30);
            this.btnGetDetailInfo.TabIndex = 6;
            this.btnGetDetailInfo.Text = "문서상세정보";
            this.btnGetDetailInfo.UseVisualStyleBackColor = true;
            this.btnGetDetailInfo.Click += new System.EventHandler(this.btnGetDetailInfo_Click);
            // 
            // btnGetEmailURL
            // 
            this.btnGetEmailURL.Location = new System.Drawing.Point(6, 149);
            this.btnGetEmailURL.Name = "btnGetEmailURL";
            this.btnGetEmailURL.Size = new System.Drawing.Size(180, 30);
            this.btnGetEmailURL.TabIndex = 13;
            this.btnGetEmailURL.Text = "이메일의 보기 버튼 URL";
            this.btnGetEmailURL.UseVisualStyleBackColor = true;
            this.btnGetEmailURL.Click += new System.EventHandler(this.btnGetEmailURL_Click);
            // 
            // btnGetMassPrintURL
            // 
            this.btnGetMassPrintURL.Location = new System.Drawing.Point(6, 117);
            this.btnGetMassPrintURL.Name = "btnGetMassPrintURL";
            this.btnGetMassPrintURL.Size = new System.Drawing.Size(180, 30);
            this.btnGetMassPrintURL.TabIndex = 12;
            this.btnGetMassPrintURL.Text = "대량인쇄 팝업 URL";
            this.btnGetMassPrintURL.UseVisualStyleBackColor = true;
            this.btnGetMassPrintURL.Click += new System.EventHandler(this.btnGetMassPrintURL_Click);
            // 
            // btnSendFAX
            // 
            this.btnSendFAX.Location = new System.Drawing.Point(7, 84);
            this.btnSendFAX.Name = "btnSendFAX";
            this.btnSendFAX.Size = new System.Drawing.Size(142, 30);
            this.btnSendFAX.TabIndex = 10;
            this.btnSendFAX.Text = "팩스 전송";
            this.btnSendFAX.UseVisualStyleBackColor = true;
            this.btnSendFAX.Click += new System.EventHandler(this.btnSendFAX_Click);
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(7, 52);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(142, 30);
            this.btnSendSMS.TabIndex = 9;
            this.btnSendSMS.Text = "문자 전송";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(7, 20);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(142, 30);
            this.btnSendEmail.TabIndex = 8;
            this.btnSendEmail.Text = "이메일 전송";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // btnCheckMgtKeyInUse
            // 
            this.btnCheckMgtKeyInUse.Location = new System.Drawing.Point(418, 20);
            this.btnCheckMgtKeyInUse.Name = "btnCheckMgtKeyInUse";
            this.btnCheckMgtKeyInUse.Size = new System.Drawing.Size(141, 28);
            this.btnCheckMgtKeyInUse.TabIndex = 5;
            this.btnCheckMgtKeyInUse.Text = "문서번호 사용여부 확인";
            this.btnCheckMgtKeyInUse.UseVisualStyleBackColor = true;
            this.btnCheckMgtKeyInUse.Click += new System.EventHandler(this.btnCheckMgtKeyInUse_Click);
            // 
            // txtMgtKey
            // 
            this.txtMgtKey.Location = new System.Drawing.Point(264, 24);
            this.txtMgtKey.Name = "txtMgtKey";
            this.txtMgtKey.Size = new System.Drawing.Size(143, 21);
            this.txtMgtKey.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(127, 30);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(118, 12);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "문서번호(MgtKey) : ";
            // 
            // GroupBox11
            // 
            this.GroupBox11.Controls.Add(this.btnAssignMgtKey);
            this.GroupBox11.Controls.Add(this.btnUpdateEmailConfig);
            this.GroupBox11.Controls.Add(this.btnListEmailConfig);
            this.GroupBox11.Controls.Add(this.btnSendFAX);
            this.GroupBox11.Controls.Add(this.btnSendSMS);
            this.GroupBox11.Controls.Add(this.btnSendEmail);
            this.GroupBox11.Location = new System.Drawing.Point(156, 246);
            this.GroupBox11.Name = "GroupBox11";
            this.GroupBox11.Size = new System.Drawing.Size(296, 189);
            this.GroupBox11.TabIndex = 10;
            this.GroupBox11.TabStop = false;
            this.GroupBox11.Text = "부가 기능";
            // 
            // btnAssignMgtKey
            // 
            this.btnAssignMgtKey.Location = new System.Drawing.Point(155, 20);
            this.btnAssignMgtKey.Name = "btnAssignMgtKey";
            this.btnAssignMgtKey.Size = new System.Drawing.Size(133, 30);
            this.btnAssignMgtKey.TabIndex = 11;
            this.btnAssignMgtKey.Text = "문서번호 할당";
            this.btnAssignMgtKey.UseVisualStyleBackColor = true;
            this.btnAssignMgtKey.Click += new System.EventHandler(this.btnAssignMgtKey_Click);
            // 
            // btnUpdateEmailConfig
            // 
            this.btnUpdateEmailConfig.Location = new System.Drawing.Point(6, 150);
            this.btnUpdateEmailConfig.Name = "btnUpdateEmailConfig";
            this.btnUpdateEmailConfig.Size = new System.Drawing.Size(142, 30);
            this.btnUpdateEmailConfig.TabIndex = 1;
            this.btnUpdateEmailConfig.Text = "알림메일 전송설정 수정";
            this.btnUpdateEmailConfig.UseVisualStyleBackColor = true;
            this.btnUpdateEmailConfig.Click += new System.EventHandler(this.btnUpdateEmailConfig_Click);
            // 
            // btnListEmailConfig
            // 
            this.btnListEmailConfig.Location = new System.Drawing.Point(6, 117);
            this.btnListEmailConfig.Name = "btnListEmailConfig";
            this.btnListEmailConfig.Size = new System.Drawing.Size(142, 30);
            this.btnListEmailConfig.TabIndex = 0;
            this.btnListEmailConfig.Text = "알림메일 전송목록 조회";
            this.btnListEmailConfig.UseVisualStyleBackColor = true;
            this.btnListEmailConfig.Click += new System.EventHandler(this.btnListEmailConfig_Click);
            // 
            // btnUnitCost
            // 
            this.btnUnitCost.Location = new System.Drawing.Point(6, 21);
            this.btnUnitCost.Name = "btnUnitCost";
            this.btnUnitCost.Size = new System.Drawing.Size(118, 28);
            this.btnUnitCost.TabIndex = 3;
            this.btnUnitCost.Text = "요금 단가 확인";
            this.btnUnitCost.UseVisualStyleBackColor = true;
            this.btnUnitCost.Click += new System.EventHandler(this.btnUnitCost_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnGetChargeInfo);
            this.GroupBox3.Controls.Add(this.btnUnitCost);
            this.GroupBox3.Location = new System.Drawing.Point(143, 15);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(131, 151);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(6, 51);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(118, 28);
            this.btnGetChargeInfo.TabIndex = 5;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // btnGetPartnerBalance1
            // 
            this.btnGetPartnerBalance1.Location = new System.Drawing.Point(8, 20);
            this.btnGetPartnerBalance1.Name = "btnGetPartnerBalance1";
            this.btnGetPartnerBalance1.Size = new System.Drawing.Size(118, 28);
            this.btnGetPartnerBalance1.TabIndex = 4;
            this.btnGetPartnerBalance1.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance1.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance1.Click += new System.EventHandler(this.btnGetPartnerBalance1_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(8, 20);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(139, 28);
            this.btnGetBalance.TabIndex = 2;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // btnGetPopUpURL
            // 
            this.btnGetPopUpURL.Location = new System.Drawing.Point(6, 20);
            this.btnGetPopUpURL.Name = "btnGetPopUpURL";
            this.btnGetPopUpURL.Size = new System.Drawing.Size(180, 30);
            this.btnGetPopUpURL.TabIndex = 9;
            this.btnGetPopUpURL.Text = "문서 팝업 URL";
            this.btnGetPopUpURL.UseVisualStyleBackColor = true;
            this.btnGetPopUpURL.Click += new System.EventHandler(this.btnGetPopUpURL_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnCheckID);
            this.GroupBox2.Controls.Add(this.btnCheckIsMember);
            this.GroupBox2.Controls.Add(this.btnJoinMember);
            this.GroupBox2.Location = new System.Drawing.Point(11, 15);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(126, 151);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(6, 51);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(112, 28);
            this.btnCheckID.TabIndex = 3;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(6, 19);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(112, 28);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(6, 82);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(112, 28);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // GroupBox7
            // 
            this.GroupBox7.Controls.Add(this.label5);
            this.GroupBox7.Controls.Add(this.groupBox14);
            this.GroupBox7.Controls.Add(this.groupBox10);
            this.GroupBox7.Controls.Add(this.GroupBox13);
            this.GroupBox7.Controls.Add(this.GroupBox12);
            this.GroupBox7.Controls.Add(this.GroupBox11);
            this.GroupBox7.Controls.Add(this.GroupBox9);
            this.GroupBox7.Controls.Add(this.btnCheckMgtKeyInUse);
            this.GroupBox7.Controls.Add(this.txtMgtKey);
            this.GroupBox7.Controls.Add(this.Label3);
            this.GroupBox7.Location = new System.Drawing.Point(12, 219);
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.Size = new System.Drawing.Size(993, 454);
            this.GroupBox7.TabIndex = 12;
            this.GroupBox7.TabStop = false;
            this.GroupBox7.Text = "현금영수증 관련 API";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(356, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(291, 28);
            this.label5.TabIndex = 20;
            this.label5.Text = "국세청에 전송이 완료된 현금영수증을 취소하는 경우에는 \'취소현금영수증\'을 발행해야 합니다.";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btnRevokeRegistIssue_part);
            this.groupBox14.Controls.Add(this.btnCancelIssue02);
            this.groupBox14.Controls.Add(this.btnDelete02);
            this.groupBox14.Controls.Add(this.panel2);
            this.groupBox14.Controls.Add(this.panel1);
            this.groupBox14.Controls.Add(this.btnRevokRegistIssue);
            this.groupBox14.Controls.Add(this.label11);
            this.groupBox14.Location = new System.Drawing.Point(353, 91);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(277, 140);
            this.groupBox14.TabIndex = 19;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "취소현금영수증 즉시발행 프로세스";
            // 
            // btnRevokeRegistIssue_part
            // 
            this.btnRevokeRegistIssue_part.BackColor = System.Drawing.Color.LightCoral;
            this.btnRevokeRegistIssue_part.Location = new System.Drawing.Point(164, 24);
            this.btnRevokeRegistIssue_part.Name = "btnRevokeRegistIssue_part";
            this.btnRevokeRegistIssue_part.Size = new System.Drawing.Size(69, 28);
            this.btnRevokeRegistIssue_part.TabIndex = 14;
            this.btnRevokeRegistIssue_part.Text = "부분취소";
            this.btnRevokeRegistIssue_part.UseVisualStyleBackColor = false;
            this.btnRevokeRegistIssue_part.Click += new System.EventHandler(this.btnRevokeRegistIssue_part_Click);
            // 
            // btnCancelIssue02
            // 
            this.btnCancelIssue02.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelIssue02.Location = new System.Drawing.Point(21, 105);
            this.btnCancelIssue02.Name = "btnCancelIssue02";
            this.btnCancelIssue02.Size = new System.Drawing.Size(65, 30);
            this.btnCancelIssue02.TabIndex = 11;
            this.btnCancelIssue02.Text = "발행취소";
            this.btnCancelIssue02.UseVisualStyleBackColor = false;
            this.btnCancelIssue02.Click += new System.EventHandler(this.btnCancelIssue02_Click);
            // 
            // btnDelete02
            // 
            this.btnDelete02.Location = new System.Drawing.Point(130, 106);
            this.btnDelete02.Name = "btnDelete02";
            this.btnDelete02.Size = new System.Drawing.Size(56, 29);
            this.btnDelete02.TabIndex = 10;
            this.btnDelete02.Text = "삭제";
            this.btnDelete02.UseVisualStyleBackColor = true;
            this.btnDelete02.Click += new System.EventHandler(this.btnDelete02_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(81, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(56, 1);
            this.panel2.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(52, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 59);
            this.panel1.TabIndex = 13;
            // 
            // btnRevokRegistIssue
            // 
            this.btnRevokRegistIssue.BackColor = System.Drawing.Color.LightCoral;
            this.btnRevokRegistIssue.Location = new System.Drawing.Point(80, 24);
            this.btnRevokRegistIssue.Name = "btnRevokRegistIssue";
            this.btnRevokRegistIssue.Size = new System.Drawing.Size(69, 28);
            this.btnRevokRegistIssue.TabIndex = 9;
            this.btnRevokRegistIssue.Text = "전체취소";
            this.btnRevokRegistIssue.UseVisualStyleBackColor = false;
            this.btnRevokRegistIssue.Click += new System.EventHandler(this.btnRevokRegistIssue_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Silver;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(9, 19);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label11.Size = new System.Drawing.Size(249, 37);
            this.label11.TabIndex = 6;
            this.label11.Text = "즉시발행";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnCancelIssueSub);
            this.groupBox10.Controls.Add(this.btnRegistIssue);
            this.groupBox10.Controls.Add(this.btnDeleteSub);
            this.groupBox10.Controls.Add(this.label4);
            this.groupBox10.Controls.Add(this.label8);
            this.groupBox10.Controls.Add(this.label10);
            this.groupBox10.Location = new System.Drawing.Point(92, 91);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(229, 143);
            this.groupBox10.TabIndex = 18;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "현금영수증 즉시발행 프로세스(권장)";
            // 
            // btnCancelIssueSub
            // 
            this.btnCancelIssueSub.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelIssueSub.Location = new System.Drawing.Point(22, 106);
            this.btnCancelIssueSub.Name = "btnCancelIssueSub";
            this.btnCancelIssueSub.Size = new System.Drawing.Size(65, 30);
            this.btnCancelIssueSub.TabIndex = 8;
            this.btnCancelIssueSub.Text = "발행취소";
            this.btnCancelIssueSub.UseVisualStyleBackColor = false;
            this.btnCancelIssueSub.Click += new System.EventHandler(this.btnCancelIssueSub_Click);
            // 
            // btnRegistIssue
            // 
            this.btnRegistIssue.BackColor = System.Drawing.Color.LightCoral;
            this.btnRegistIssue.Location = new System.Drawing.Point(92, 22);
            this.btnRegistIssue.Name = "btnRegistIssue";
            this.btnRegistIssue.Size = new System.Drawing.Size(69, 28);
            this.btnRegistIssue.TabIndex = 0;
            this.btnRegistIssue.Text = "즉시발행";
            this.btnRegistIssue.UseVisualStyleBackColor = false;
            this.btnRegistIssue.Click += new System.EventHandler(this.btnRegistIssue_Click);
            // 
            // btnDeleteSub
            // 
            this.btnDeleteSub.Location = new System.Drawing.Point(131, 107);
            this.btnDeleteSub.Name = "btnDeleteSub";
            this.btnDeleteSub.Size = new System.Drawing.Size(56, 29);
            this.btnDeleteSub.TabIndex = 1;
            this.btnDeleteSub.Text = "삭제";
            this.btnDeleteSub.UseVisualStyleBackColor = true;
            this.btnDeleteSub.Click += new System.EventHandler(this.btnDeleteSub_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(11, 17);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(194, 37);
            this.label4.TabIndex = 5;
            this.label4.Text = "즉시발행";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(43, 122);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label8.Size = new System.Drawing.Size(119, 1);
            this.label8.TabIndex = 14;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(53, 52);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label10.Size = new System.Drawing.Size(1, 70);
            this.label10.TabIndex = 15;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox13
            // 
            this.GroupBox13.Controls.Add(this.btnGetURL_WRITE);
            this.GroupBox13.Controls.Add(this.btnGetURL_SBOX);
            this.GroupBox13.Controls.Add(this.btnGetURL_TBOX);
            this.GroupBox13.Location = new System.Drawing.Point(860, 247);
            this.GroupBox13.Name = "GroupBox13";
            this.GroupBox13.Size = new System.Drawing.Size(115, 146);
            this.GroupBox13.TabIndex = 12;
            this.GroupBox13.TabStop = false;
            this.GroupBox13.Text = "기타 URL";
            // 
            // btnGetURL_WRITE
            // 
            this.btnGetURL_WRITE.Location = new System.Drawing.Point(6, 87);
            this.btnGetURL_WRITE.Name = "btnGetURL_WRITE";
            this.btnGetURL_WRITE.Size = new System.Drawing.Size(102, 30);
            this.btnGetURL_WRITE.TabIndex = 11;
            this.btnGetURL_WRITE.Text = "매출작성";
            this.btnGetURL_WRITE.UseVisualStyleBackColor = true;
            this.btnGetURL_WRITE.Click += new System.EventHandler(this.btnGetURL_WRITE_Click);
            // 
            // btnGetURL_SBOX
            // 
            this.btnGetURL_SBOX.Location = new System.Drawing.Point(6, 53);
            this.btnGetURL_SBOX.Name = "btnGetURL_SBOX";
            this.btnGetURL_SBOX.Size = new System.Drawing.Size(102, 30);
            this.btnGetURL_SBOX.TabIndex = 9;
            this.btnGetURL_SBOX.Text = "발행 문서함";
            this.btnGetURL_SBOX.UseVisualStyleBackColor = true;
            this.btnGetURL_SBOX.Click += new System.EventHandler(this.btnGetURL_SBOX_Click);
            // 
            // btnGetURL_TBOX
            // 
            this.btnGetURL_TBOX.Location = new System.Drawing.Point(6, 20);
            this.btnGetURL_TBOX.Name = "btnGetURL_TBOX";
            this.btnGetURL_TBOX.Size = new System.Drawing.Size(102, 30);
            this.btnGetURL_TBOX.TabIndex = 8;
            this.btnGetURL_TBOX.Text = "연동문서함";
            this.btnGetURL_TBOX.UseVisualStyleBackColor = true;
            this.btnGetURL_TBOX.Click += new System.EventHandler(this.btnGetURL_TBOX_Click);
            // 
            // GroupBox12
            // 
            this.GroupBox12.Controls.Add(this.btnGetViewURL);
            this.GroupBox12.Controls.Add(this.btnGetPDFURL);
            this.GroupBox12.Controls.Add(this.btnGetEmailURL);
            this.GroupBox12.Controls.Add(this.btnGetMassPrintURL);
            this.GroupBox12.Controls.Add(this.btnGetPrintURL);
            this.GroupBox12.Controls.Add(this.btnGetPopUpURL);
            this.GroupBox12.Location = new System.Drawing.Point(467, 247);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(379, 188);
            this.GroupBox12.TabIndex = 11;
            this.GroupBox12.TabStop = false;
            this.GroupBox12.Text = "문서관련 URL 기능";
            // 
            // btnGetViewURL
            // 
            this.btnGetViewURL.Location = new System.Drawing.Point(6, 53);
            this.btnGetViewURL.Name = "btnGetViewURL";
            this.btnGetViewURL.Size = new System.Drawing.Size(180, 30);
            this.btnGetViewURL.TabIndex = 15;
            this.btnGetViewURL.Text = "문서 팝업 URL (메뉴x)";
            this.btnGetViewURL.UseVisualStyleBackColor = true;
            this.btnGetViewURL.Click += new System.EventHandler(this.btnGetViewURL_Click);
            // 
            // btnGetPDFURL
            // 
            this.btnGetPDFURL.Location = new System.Drawing.Point(192, 20);
            this.btnGetPDFURL.Name = "btnGetPDFURL";
            this.btnGetPDFURL.Size = new System.Drawing.Size(180, 30);
            this.btnGetPDFURL.TabIndex = 14;
            this.btnGetPDFURL.Text = "PDF 다운로드 URL";
            this.btnGetPDFURL.UseVisualStyleBackColor = true;
            this.btnGetPDFURL.Click += new System.EventHandler(this.btnGetPDFURL_Click);
            // 
            // btnGetPrintURL
            // 
            this.btnGetPrintURL.Location = new System.Drawing.Point(6, 85);
            this.btnGetPrintURL.Name = "btnGetPrintURL";
            this.btnGetPrintURL.Size = new System.Drawing.Size(180, 30);
            this.btnGetPrintURL.TabIndex = 10;
            this.btnGetPrintURL.Text = "인쇄 팝업 URL";
            this.btnGetPrintURL.UseVisualStyleBackColor = true;
            this.btnGetPrintURL.Click += new System.EventHandler(this.btnGetPrintURL_Click);
            // 
            // textURL
            // 
            this.textURL.Location = new System.Drawing.Point(659, 12);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(333, 21);
            this.textURL.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(587, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "응답 URL :";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(372, 10);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(143, 21);
            this.txtUserId.TabIndex = 10;
            this.txtUserId.Text = "testkorea";
            // 
            // btnGetAccessURL
            // 
            this.btnGetAccessURL.Location = new System.Drawing.Point(6, 20);
            this.btnGetAccessURL.Name = "btnGetAccessURL";
            this.btnGetAccessURL.Size = new System.Drawing.Size(116, 28);
            this.btnGetAccessURL.TabIndex = 0;
            this.btnGetAccessURL.Text = "팝빌 로그인 URL";
            this.btnGetAccessURL.UseVisualStyleBackColor = true;
            this.btnGetAccessURL.Click += new System.EventHandler(this.btnGetAccessURL_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.groupBox4);
            this.GroupBox1.Controls.Add(this.GroupBox6);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Controls.Add(this.groupBox8);
            this.GroupBox1.Controls.Add(this.groupBox15);
            this.GroupBox1.Location = new System.Drawing.Point(13, 33);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(1010, 180);
            this.GroupBox1.TabIndex = 11;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox4.Controls.Add(this.btnGetCorpInfo);
            this.groupBox4.Location = new System.Drawing.Point(870, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(126, 151);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "회사정보 관련";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(7, 51);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(111, 28);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(7, 20);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(111, 28);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.btnGetContactInfo);
            this.GroupBox6.Controls.Add(this.btnUpdateContact);
            this.GroupBox6.Controls.Add(this.btnListContact);
            this.GroupBox6.Controls.Add(this.btnRegistContact);
            this.GroupBox6.Location = new System.Drawing.Point(731, 15);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(132, 151);
            this.GroupBox6.TabIndex = 3;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "담당자 관련";
            // 
            // btnGetContactInfo
            // 
            this.btnGetContactInfo.Location = new System.Drawing.Point(6, 51);
            this.btnGetContactInfo.Name = "btnGetContactInfo";
            this.btnGetContactInfo.Size = new System.Drawing.Size(117, 28);
            this.btnGetContactInfo.TabIndex = 3;
            this.btnGetContactInfo.Text = "담당자 정보 확인";
            this.btnGetContactInfo.UseVisualStyleBackColor = true;
            this.btnGetContactInfo.Click += new System.EventHandler(this.btnGetContactInfo_Click);
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(6, 113);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(117, 28);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(6, 82);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(117, 28);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(6, 20);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(117, 28);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.btnGetAccessURL);
            this.GroupBox5.Location = new System.Drawing.Point(593, 16);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(131, 150);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnGetPartnerURL_CHRG);
            this.groupBox8.Controls.Add(this.btnGetPartnerBalance1);
            this.groupBox8.Location = new System.Drawing.Point(451, 15);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(133, 151);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "파트너과금 포인트";
            // 
            // btnGetPartnerURL_CHRG
            // 
            this.btnGetPartnerURL_CHRG.Location = new System.Drawing.Point(8, 51);
            this.btnGetPartnerURL_CHRG.Name = "btnGetPartnerURL_CHRG";
            this.btnGetPartnerURL_CHRG.Size = new System.Drawing.Size(119, 28);
            this.btnGetPartnerURL_CHRG.TabIndex = 0;
            this.btnGetPartnerURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPartnerURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPartnerURL_CHRG.Click += new System.EventHandler(this.btnGetPartnerURL_CHRG_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.btnGetUseHistoryURL);
            this.groupBox15.Controls.Add(this.btnGetPaymentURL);
            this.groupBox15.Controls.Add(this.btnGetChargeURL);
            this.groupBox15.Controls.Add(this.btnGetBalance);
            this.groupBox15.Location = new System.Drawing.Point(285, 15);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(153, 150);
            this.groupBox15.TabIndex = 13;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "연동과금 포인트";
            // 
            // btnGetUseHistoryURL
            // 
            this.btnGetUseHistoryURL.Location = new System.Drawing.Point(8, 113);
            this.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL";
            this.btnGetUseHistoryURL.Size = new System.Drawing.Size(139, 28);
            this.btnGetUseHistoryURL.TabIndex = 4;
            this.btnGetUseHistoryURL.Text = "포인트 사용내역 URL";
            this.btnGetUseHistoryURL.UseVisualStyleBackColor = true;
            this.btnGetUseHistoryURL.Click += new System.EventHandler(this.btnGetUseHistoryURL_Click);
            // 
            // btnGetPaymentURL
            // 
            this.btnGetPaymentURL.Location = new System.Drawing.Point(8, 82);
            this.btnGetPaymentURL.Name = "btnGetPaymentURL";
            this.btnGetPaymentURL.Size = new System.Drawing.Size(139, 28);
            this.btnGetPaymentURL.TabIndex = 3;
            this.btnGetPaymentURL.Text = "포인트 결제내역 URL";
            this.btnGetPaymentURL.UseVisualStyleBackColor = true;
            this.btnGetPaymentURL.Click += new System.EventHandler(this.btnGetPaymentURL_Click);
            // 
            // btnGetChargeURL
            // 
            this.btnGetChargeURL.Location = new System.Drawing.Point(8, 51);
            this.btnGetChargeURL.Name = "btnGetChargeURL";
            this.btnGetChargeURL.Size = new System.Drawing.Size(139, 28);
            this.btnGetChargeURL.TabIndex = 1;
            this.btnGetChargeURL.Text = "포인트 충전 URL";
            this.btnGetChargeURL.UseVisualStyleBackColor = true;
            this.btnGetChargeURL.Click += new System.EventHandler(this.btnGetChargeURL_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(297, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 12);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "팝빌아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(141, 10);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(143, 21);
            this.txtCorpNum.TabIndex = 8;
            this.txtCorpNum.Text = "1234567890";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(16, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 12);
            this.Label1.TabIndex = 7;
            this.Label1.Text = "팝빌회원 사업자번호 : ";
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 685);
            this.Controls.Add(this.textURL);
            this.Controls.Add(this.GroupBox7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 현금영수증 SDK Example";
            this.GroupBox9.ResumeLayout(false);
            this.GroupBox11.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox7.ResumeLayout(false);
            this.GroupBox7.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.GroupBox13.ResumeLayout(false);
            this.GroupBox12.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnGetInfos;
        internal System.Windows.Forms.Button btnGetLogs;
        internal System.Windows.Forms.Button btnGetInfo;
        internal System.Windows.Forms.GroupBox GroupBox9;
        internal System.Windows.Forms.Button btnGetDetailInfo;
        internal System.Windows.Forms.Button btnGetEmailURL;
        internal System.Windows.Forms.Button btnGetMassPrintURL;
        internal System.Windows.Forms.Button btnSendFAX;
        internal System.Windows.Forms.Button btnSendSMS;
        internal System.Windows.Forms.Button btnSendEmail;
        internal System.Windows.Forms.Button btnCheckMgtKeyInUse;
        internal System.Windows.Forms.TextBox txtMgtKey;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.GroupBox GroupBox11;
        internal System.Windows.Forms.Button btnUnitCost;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button btnGetBalance;
        internal System.Windows.Forms.Button btnGetPopUpURL;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button btnCheckIsMember;
        internal System.Windows.Forms.Button btnJoinMember;
        internal System.Windows.Forms.GroupBox GroupBox7;
        internal System.Windows.Forms.GroupBox GroupBox13;
        internal System.Windows.Forms.Button btnGetURL_WRITE;
        internal System.Windows.Forms.Button btnGetURL_SBOX;
        internal System.Windows.Forms.Button btnGetURL_TBOX;
        internal System.Windows.Forms.GroupBox GroupBox12;
        internal System.Windows.Forms.Button btnGetPrintURL;
        internal System.Windows.Forms.TextBox txtUserId;
        internal System.Windows.Forms.Button btnGetAccessURL;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.GroupBox GroupBox6;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCorpNum;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnCheckID;
        private System.Windows.Forms.Button btnGetPartnerBalance1;
        internal System.Windows.Forms.Button btnGetChargeURL;
        private System.Windows.Forms.Button btnRegistContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        internal System.Windows.Forms.GroupBox groupBox10;
        internal System.Windows.Forms.Button btnCancelIssueSub;
        internal System.Windows.Forms.Button btnRegistIssue;
        internal System.Windows.Forms.Button btnDeleteSub;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetChargeInfo;
        private System.Windows.Forms.GroupBox groupBox14;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Button btnCancelIssue02;
        internal System.Windows.Forms.Button btnRevokRegistIssue;
        internal System.Windows.Forms.Button btnDelete02;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnGetPartnerURL_CHRG;
        private System.Windows.Forms.GroupBox groupBox15;
        internal System.Windows.Forms.Button btnRevokeRegistIssue_part;
        private System.Windows.Forms.Button btnUpdateEmailConfig;
        private System.Windows.Forms.Button btnListEmailConfig;
        private System.Windows.Forms.Button btnGetPDFURL;
        private System.Windows.Forms.Button btnAssignMgtKey;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox textURL;
        internal System.Windows.Forms.Button btnGetViewURL;
        internal System.Windows.Forms.Button btnGetUseHistoryURL;
        internal System.Windows.Forms.Button btnGetPaymentURL;
        private System.Windows.Forms.Button btnGetContactInfo;
    }
}

