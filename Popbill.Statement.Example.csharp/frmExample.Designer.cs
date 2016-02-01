namespace Popbill.Statement.Example.csharp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.btnGetFiles = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.btnCheckMgtKeyInUse = new System.Windows.Forms.Button();
            this.btnGetDetailInfo = new System.Windows.Forms.Button();
            this.btnGetLogs = new System.Windows.Forms.Button();
            this.btnGetInfos = new System.Windows.Forms.Button();
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.btnSendFAX = new System.Windows.Forms.Button();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnGetMassPrintURL = new System.Windows.Forms.Button();
            this.btnGetEPrintURL = new System.Windows.Forms.Button();
            this.btnGetPrintURL = new System.Windows.Forms.Button();
            this.btnGetPopUpURL = new System.Windows.Forms.Button();
            this.btnGetURL_SBOX = new System.Windows.Forms.Button();
            this.btnGetURL_TBOX = new System.Windows.Forms.Button();
            this.btnGetMailURL = new System.Windows.Forms.Button();
            this.cboItemCode = new System.Windows.Forms.ComboBox();
            this.txtFormCode = new System.Windows.Forms.TextBox();
            this.txtMgtKey = new System.Windows.Forms.TextBox();
            this.txtFileID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetPartnerBalance = new System.Windows.Forms.Button();
            this.btnGetUnitCost = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetPopbillURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetPopbillURL_LOGIN = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.UpdateContact = new System.Windows.Forms.Button();
            this.ListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "팝빌회원 사업자번호 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "팝빌회원 아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(143, 16);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(100, 21);
            this.txtCorpNum.TabIndex = 2;
            this.txtCorpNum.Text = "1234567890";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(347, 16);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(100, 21);
            this.txtUserID.TabIndex = 3;
            this.txtUserID.Text = "testkorea";
            // 
            // btnIssue
            // 
            this.btnIssue.Location = new System.Drawing.Point(54, 86);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(68, 26);
            this.btnIssue.TabIndex = 13;
            this.btnIssue.Text = "발행";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(54, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 26);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "발행취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(155, 121);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 26);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.Location = new System.Drawing.Point(10, 19);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(91, 26);
            this.btnAttachFile.TabIndex = 16;
            this.btnAttachFile.Text = "파일 첨부";
            this.btnAttachFile.UseVisualStyleBackColor = true;
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);
            // 
            // btnGetFiles
            // 
            this.btnGetFiles.Location = new System.Drawing.Point(107, 19);
            this.btnGetFiles.Name = "btnGetFiles";
            this.btnGetFiles.Size = new System.Drawing.Size(91, 26);
            this.btnGetFiles.TabIndex = 17;
            this.btnGetFiles.Text = "첨부파일 목록";
            this.btnGetFiles.UseVisualStyleBackColor = true;
            this.btnGetFiles.Click += new System.EventHandler(this.btnGetFiles_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Location = new System.Drawing.Point(216, 49);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(91, 26);
            this.btnDeleteFile.TabIndex = 18;
            this.btnDeleteFile.Text = "파일 삭제";
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnCheckMgtKeyInUse
            // 
            this.btnCheckMgtKeyInUse.Location = new System.Drawing.Point(168, 125);
            this.btnCheckMgtKeyInUse.Name = "btnCheckMgtKeyInUse";
            this.btnCheckMgtKeyInUse.Size = new System.Drawing.Size(151, 26);
            this.btnCheckMgtKeyInUse.TabIndex = 19;
            this.btnCheckMgtKeyInUse.Text = "관리번호 사용여부 확인";
            this.btnCheckMgtKeyInUse.UseVisualStyleBackColor = true;
            this.btnCheckMgtKeyInUse.Click += new System.EventHandler(this.btnCheckMgtKeyInUse_Click);
            // 
            // btnGetDetailInfo
            // 
            this.btnGetDetailInfo.Location = new System.Drawing.Point(9, 113);
            this.btnGetDetailInfo.Name = "btnGetDetailInfo";
            this.btnGetDetailInfo.Size = new System.Drawing.Size(102, 26);
            this.btnGetDetailInfo.TabIndex = 20;
            this.btnGetDetailInfo.Text = "문서 상세정보";
            this.btnGetDetailInfo.UseVisualStyleBackColor = true;
            this.btnGetDetailInfo.Click += new System.EventHandler(this.btnGetDetailInfo_Click);
            // 
            // btnGetLogs
            // 
            this.btnGetLogs.Location = new System.Drawing.Point(9, 81);
            this.btnGetLogs.Name = "btnGetLogs";
            this.btnGetLogs.Size = new System.Drawing.Size(102, 26);
            this.btnGetLogs.TabIndex = 21;
            this.btnGetLogs.Text = "문서 이력";
            this.btnGetLogs.UseVisualStyleBackColor = true;
            this.btnGetLogs.Click += new System.EventHandler(this.btnGetLogs_Click);
            // 
            // btnGetInfos
            // 
            this.btnGetInfos.Location = new System.Drawing.Point(9, 49);
            this.btnGetInfos.Name = "btnGetInfos";
            this.btnGetInfos.Size = new System.Drawing.Size(102, 26);
            this.btnGetInfos.TabIndex = 22;
            this.btnGetInfos.Text = "문서 정보(대량)";
            this.btnGetInfos.UseVisualStyleBackColor = true;
            this.btnGetInfos.Click += new System.EventHandler(this.btnGetInfos_Click);
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(9, 17);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(102, 26);
            this.btnGetInfo.TabIndex = 23;
            this.btnGetInfo.Text = "문서 정보";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // btnSendFAX
            // 
            this.btnSendFAX.Location = new System.Drawing.Point(10, 81);
            this.btnSendFAX.Name = "btnSendFAX";
            this.btnSendFAX.Size = new System.Drawing.Size(91, 26);
            this.btnSendFAX.TabIndex = 24;
            this.btnSendFAX.Text = "팩스 전송";
            this.btnSendFAX.UseVisualStyleBackColor = true;
            this.btnSendFAX.Click += new System.EventHandler(this.btnSendFAX_Click);
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(10, 49);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(91, 26);
            this.btnSendSMS.TabIndex = 25;
            this.btnSendSMS.Text = "문자 전송";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(10, 17);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(91, 26);
            this.btnSendEmail.TabIndex = 26;
            this.btnSendEmail.Text = "이메일 전송";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // btnGetMassPrintURL
            // 
            this.btnGetMassPrintURL.Location = new System.Drawing.Point(11, 113);
            this.btnGetMassPrintURL.Name = "btnGetMassPrintURL";
            this.btnGetMassPrintURL.Size = new System.Drawing.Size(185, 26);
            this.btnGetMassPrintURL.TabIndex = 27;
            this.btnGetMassPrintURL.Text = "다량 인쇄 팝업 URL";
            this.btnGetMassPrintURL.UseVisualStyleBackColor = true;
            this.btnGetMassPrintURL.Click += new System.EventHandler(this.btnGetMassPrintURL_Click);
            // 
            // btnGetEPrintURL
            // 
            this.btnGetEPrintURL.Location = new System.Drawing.Point(11, 81);
            this.btnGetEPrintURL.Name = "btnGetEPrintURL";
            this.btnGetEPrintURL.Size = new System.Drawing.Size(185, 26);
            this.btnGetEPrintURL.TabIndex = 28;
            this.btnGetEPrintURL.Text = "수신자 인쇄 팝업 URL";
            this.btnGetEPrintURL.UseVisualStyleBackColor = true;
            this.btnGetEPrintURL.Click += new System.EventHandler(this.btnGetEPrintURL_Click);
            // 
            // btnGetPrintURL
            // 
            this.btnGetPrintURL.Location = new System.Drawing.Point(11, 49);
            this.btnGetPrintURL.Name = "btnGetPrintURL";
            this.btnGetPrintURL.Size = new System.Drawing.Size(185, 26);
            this.btnGetPrintURL.TabIndex = 29;
            this.btnGetPrintURL.Text = "인쇄 팝업 URL";
            this.btnGetPrintURL.UseVisualStyleBackColor = true;
            this.btnGetPrintURL.Click += new System.EventHandler(this.btnGetPrintURL_Click);
            // 
            // btnGetPopUpURL
            // 
            this.btnGetPopUpURL.Location = new System.Drawing.Point(11, 17);
            this.btnGetPopUpURL.Name = "btnGetPopUpURL";
            this.btnGetPopUpURL.Size = new System.Drawing.Size(185, 26);
            this.btnGetPopUpURL.TabIndex = 30;
            this.btnGetPopUpURL.Text = "문서 내용 보기 팝업 URL";
            this.btnGetPopUpURL.UseVisualStyleBackColor = true;
            this.btnGetPopUpURL.Click += new System.EventHandler(this.btnGetPopUpURL_Click);
            // 
            // btnGetURL_SBOX
            // 
            this.btnGetURL_SBOX.Location = new System.Drawing.Point(11, 47);
            this.btnGetURL_SBOX.Name = "btnGetURL_SBOX";
            this.btnGetURL_SBOX.Size = new System.Drawing.Size(91, 26);
            this.btnGetURL_SBOX.TabIndex = 31;
            this.btnGetURL_SBOX.Text = "발행 문서함";
            this.btnGetURL_SBOX.UseVisualStyleBackColor = true;
            this.btnGetURL_SBOX.Click += new System.EventHandler(this.btnGetURL_SBOX_Click);
            // 
            // btnGetURL_TBOX
            // 
            this.btnGetURL_TBOX.Location = new System.Drawing.Point(11, 15);
            this.btnGetURL_TBOX.Name = "btnGetURL_TBOX";
            this.btnGetURL_TBOX.Size = new System.Drawing.Size(91, 26);
            this.btnGetURL_TBOX.TabIndex = 32;
            this.btnGetURL_TBOX.Text = "임시 문서함";
            this.btnGetURL_TBOX.UseVisualStyleBackColor = true;
            this.btnGetURL_TBOX.Click += new System.EventHandler(this.btnGetURL_TBOX_Click);
            // 
            // btnGetMailURL
            // 
            this.btnGetMailURL.Location = new System.Drawing.Point(11, 145);
            this.btnGetMailURL.Name = "btnGetMailURL";
            this.btnGetMailURL.Size = new System.Drawing.Size(185, 26);
            this.btnGetMailURL.TabIndex = 33;
            this.btnGetMailURL.Text = "이메일(공급받는자) 링크 URL";
            this.btnGetMailURL.UseVisualStyleBackColor = true;
            this.btnGetMailURL.Click += new System.EventHandler(this.btnGetMailURL_Click);
            // 
            // cboItemCode
            // 
            this.cboItemCode.FormattingEnabled = true;
            this.cboItemCode.Items.AddRange(new object[] {
            "거래명세서",
            "청구서",
            "견적서",
            "발주서",
            "입금표",
            "영수증"});
            this.cboItemCode.Location = new System.Drawing.Point(168, 45);
            this.cboItemCode.Name = "cboItemCode";
            this.cboItemCode.Size = new System.Drawing.Size(149, 20);
            this.cboItemCode.TabIndex = 34;
            this.cboItemCode.Text = "거래명세서";
            // 
            // txtFormCode
            // 
            this.txtFormCode.Location = new System.Drawing.Point(168, 71);
            this.txtFormCode.Name = "txtFormCode";
            this.txtFormCode.Size = new System.Drawing.Size(148, 21);
            this.txtFormCode.TabIndex = 35;
            // 
            // txtMgtKey
            // 
            this.txtMgtKey.Location = new System.Drawing.Point(168, 98);
            this.txtMgtKey.Name = "txtMgtKey";
            this.txtMgtKey.Size = new System.Drawing.Size(148, 21);
            this.txtMgtKey.TabIndex = 36;
            this.txtMgtKey.Text = "20150311-03";
            // 
            // txtFileID
            // 
            this.txtFileID.Location = new System.Drawing.Point(10, 54);
            this.txtFileID.Name = "txtFileID";
            this.txtFileID.Size = new System.Drawing.Size(188, 21);
            this.txtFileID.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "명세서 종류 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "맞춤 양식코드(FormCode) :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "문서관리번호(MgtKey) :";
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "fileDialog";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(75, 10);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(59, 26);
            this.btnRegister.TabIndex = 11;
            this.btnRegister.Text = "등록";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(152, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(59, 26);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnRegister);
            this.panel1.Location = new System.Drawing.Point(7, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 47);
            this.panel1.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "임시저장";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel1);
            this.groupBox5.Controls.Add(this.btnDelete);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnIssue);
            this.groupBox5.Location = new System.Drawing.Point(333, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(234, 162);
            this.groupBox5.TabIndex = 46;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "전자명세서 발행 프로세스";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtFileID);
            this.groupBox6.Controls.Add(this.btnDeleteFile);
            this.groupBox6.Controls.Add(this.btnGetFiles);
            this.groupBox6.Controls.Add(this.btnAttachFile);
            this.groupBox6.Location = new System.Drawing.Point(234, 186);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(333, 84);
            this.groupBox6.TabIndex = 47;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "첨부파일";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnGetInfo);
            this.groupBox7.Controls.Add(this.btnGetInfos);
            this.groupBox7.Controls.Add(this.btnGetLogs);
            this.groupBox7.Controls.Add(this.btnGetDetailInfo);
            this.groupBox7.Location = new System.Drawing.Point(19, 299);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(122, 150);
            this.groupBox7.TabIndex = 48;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "문서 정보";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnSendEmail);
            this.groupBox8.Controls.Add(this.btnSendSMS);
            this.groupBox8.Controls.Add(this.btnSendFAX);
            this.groupBox8.Location = new System.Drawing.Point(149, 299);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(112, 118);
            this.groupBox8.TabIndex = 49;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "부가 서비스";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnGetMailURL);
            this.groupBox9.Controls.Add(this.btnGetPopUpURL);
            this.groupBox9.Controls.Add(this.btnGetPrintURL);
            this.groupBox9.Controls.Add(this.btnGetEPrintURL);
            this.groupBox9.Controls.Add(this.btnGetMassPrintURL);
            this.groupBox9.Location = new System.Drawing.Point(270, 298);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(206, 182);
            this.groupBox9.TabIndex = 50;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "인쇄 URL";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnGetURL_TBOX);
            this.groupBox10.Controls.Add(this.btnGetURL_SBOX);
            this.groupBox10.Location = new System.Drawing.Point(487, 297);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(114, 84);
            this.groupBox10.TabIndex = 51;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "기타 URL";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckID);
            this.groupBox1.Controls.Add(this.btnJoinMember);
            this.groupBox1.Controls.Add(this.btnCheckIsMember);
            this.groupBox1.Location = new System.Drawing.Point(9, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 119);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "회원정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(7, 49);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(102, 29);
            this.btnCheckID.TabIndex = 6;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(7, 81);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(103, 30);
            this.btnJoinMember.TabIndex = 5;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(7, 17);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(102, 30);
            this.btnCheckIsMember.TabIndex = 4;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetPartnerBalance);
            this.groupBox2.Controls.Add(this.btnGetUnitCost);
            this.groupBox2.Controls.Add(this.btnGetBalance);
            this.groupBox2.Location = new System.Drawing.Point(130, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 119);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "포인트 관련";
            // 
            // btnGetPartnerBalance
            // 
            this.btnGetPartnerBalance.Location = new System.Drawing.Point(7, 49);
            this.btnGetPartnerBalance.Name = "btnGetPartnerBalance";
            this.btnGetPartnerBalance.Size = new System.Drawing.Size(118, 31);
            this.btnGetPartnerBalance.TabIndex = 8;
            this.btnGetPartnerBalance.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance.Click += new System.EventHandler(this.btnGetPartnerPoint_Click);
            // 
            // btnGetUnitCost
            // 
            this.btnGetUnitCost.Location = new System.Drawing.Point(7, 83);
            this.btnGetUnitCost.Name = "btnGetUnitCost";
            this.btnGetUnitCost.Size = new System.Drawing.Size(118, 29);
            this.btnGetUnitCost.TabIndex = 7;
            this.btnGetUnitCost.Text = "요금 단가 확인";
            this.btnGetUnitCost.UseVisualStyleBackColor = true;
            this.btnGetUnitCost.Click += new System.EventHandler(this.btnGetUnitCost_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(8, 18);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(117, 29);
            this.btnGetBalance.TabIndex = 6;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGetPopbillURL_CHRG);
            this.groupBox3.Controls.Add(this.btnGetPopbillURL_LOGIN);
            this.groupBox3.Location = new System.Drawing.Point(270, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 121);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "팝빌 URL 관련";
            // 
            // btnGetPopbillURL_CHRG
            // 
            this.btnGetPopbillURL_CHRG.Location = new System.Drawing.Point(9, 52);
            this.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG";
            this.btnGetPopbillURL_CHRG.Size = new System.Drawing.Size(123, 32);
            this.btnGetPopbillURL_CHRG.TabIndex = 9;
            this.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_CHRG.Click += new System.EventHandler(this.btnGetPopbillURL_CHRG_Click);
            // 
            // btnGetPopbillURL_LOGIN
            // 
            this.btnGetPopbillURL_LOGIN.Location = new System.Drawing.Point(9, 19);
            this.btnGetPopbillURL_LOGIN.Name = "btnGetPopbillURL_LOGIN";
            this.btnGetPopbillURL_LOGIN.Size = new System.Drawing.Size(122, 32);
            this.btnGetPopbillURL_LOGIN.TabIndex = 8;
            this.btnGetPopbillURL_LOGIN.Text = "팝빌 로그인 URL";
            this.btnGetPopbillURL_LOGIN.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_LOGIN.Click += new System.EventHandler(this.btnGetPopbillURL_LOGIN_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.UpdateContact);
            this.groupBox4.Controls.Add(this.ListContact);
            this.groupBox4.Controls.Add(this.btnRegistContact);
            this.groupBox4.Location = new System.Drawing.Point(415, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(135, 120);
            this.groupBox4.TabIndex = 44;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "담당자 관련";
            // 
            // UpdateContact
            // 
            this.UpdateContact.Location = new System.Drawing.Point(8, 83);
            this.UpdateContact.Name = "UpdateContact";
            this.UpdateContact.Size = new System.Drawing.Size(120, 31);
            this.UpdateContact.TabIndex = 2;
            this.UpdateContact.Text = "담당자 정보 수정";
            this.UpdateContact.UseVisualStyleBackColor = true;
            this.UpdateContact.Click += new System.EventHandler(this.UpdateContact_Click);
            // 
            // ListContact
            // 
            this.ListContact.Location = new System.Drawing.Point(8, 50);
            this.ListContact.Name = "ListContact";
            this.ListContact.Size = new System.Drawing.Size(119, 29);
            this.ListContact.TabIndex = 1;
            this.ListContact.Text = "담당자 목록 조회";
            this.ListContact.UseVisualStyleBackColor = true;
            this.ListContact.Click += new System.EventHandler(this.ListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(8, 18);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(118, 29);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.groupBox13);
            this.groupBox11.Controls.Add(this.groupBox4);
            this.groupBox11.Controls.Add(this.groupBox3);
            this.groupBox11.Controls.Add(this.groupBox2);
            this.groupBox11.Controls.Add(this.groupBox1);
            this.groupBox11.Location = new System.Drawing.Point(15, 42);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(702, 149);
            this.groupBox11.TabIndex = 52;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "팝빌 기본 API";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.groupBox10);
            this.groupBox12.Controls.Add(this.groupBox9);
            this.groupBox12.Controls.Add(this.groupBox8);
            this.groupBox12.Controls.Add(this.groupBox7);
            this.groupBox12.Controls.Add(this.groupBox6);
            this.groupBox12.Controls.Add(this.groupBox5);
            this.groupBox12.Controls.Add(this.label5);
            this.groupBox12.Controls.Add(this.label4);
            this.groupBox12.Controls.Add(this.label3);
            this.groupBox12.Controls.Add(this.txtMgtKey);
            this.groupBox12.Controls.Add(this.txtFormCode);
            this.groupBox12.Controls.Add(this.cboItemCode);
            this.groupBox12.Controls.Add(this.btnCheckMgtKeyInUse);
            this.groupBox12.Location = new System.Drawing.Point(15, 202);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(613, 487);
            this.groupBox12.TabIndex = 53;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "전자명세서 관련 API";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox13.Controls.Add(this.btnGetCorpInfo);
            this.groupBox13.Location = new System.Drawing.Point(558, 14);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(130, 117);
            this.groupBox13.TabIndex = 45;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "회사정보 관련";
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(8, 17);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(115, 30);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(8, 49);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(115, 30);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 688);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 전자명세서 C# SDK Example";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCorpNum;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.Button btnGetFiles;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.Button btnCheckMgtKeyInUse;
        private System.Windows.Forms.Button btnGetDetailInfo;
        private System.Windows.Forms.Button btnGetLogs;
        private System.Windows.Forms.Button btnGetInfos;
        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.Button btnSendFAX;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button btnGetMassPrintURL;
        private System.Windows.Forms.Button btnGetEPrintURL;
        private System.Windows.Forms.Button btnGetPrintURL;
        private System.Windows.Forms.Button btnGetPopUpURL;
        private System.Windows.Forms.Button btnGetURL_SBOX;
        private System.Windows.Forms.Button btnGetURL_TBOX;
        private System.Windows.Forms.Button btnGetMailURL;
        private System.Windows.Forms.ComboBox cboItemCode;
        private System.Windows.Forms.TextBox txtFormCode;
        private System.Windows.Forms.TextBox txtMgtKey;
        private System.Windows.Forms.TextBox txtFileID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnJoinMember;
        private System.Windows.Forms.Button btnCheckIsMember;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetUnitCost;
        private System.Windows.Forms.Button btnGetBalance;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGetPopbillURL_CHRG;
        private System.Windows.Forms.Button btnGetPopbillURL_LOGIN;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnCheckID;
        private System.Windows.Forms.Button btnGetPartnerBalance;
        private System.Windows.Forms.Button btnRegistContact;
        private System.Windows.Forms.Button ListContact;
        private System.Windows.Forms.Button UpdateContact;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
    }
}

