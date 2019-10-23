namespace Popbill.HomeTax.Taxinvoice.Example.csharp
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
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetPartnerBalance1 = new System.Windows.Forms.Button();
            this.btnGetPartnerURL_CHRG = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeURL = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.btnGetAccessURL = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnDeleteDeptUser = new System.Windows.Forms.Button();
            this.btnCheckLoginDeptUser = new System.Windows.Forms.Button();
            this.btnCheckDeptUser = new System.Windows.Forms.Button();
            this.btnRegistDeptUser = new System.Windows.Forms.Button();
            this.btnCheckCertValidation = new System.Windows.Forms.Button();
            this.btnGetCertificateExpireDate = new System.Windows.Forms.Button();
            this.btnGetCertificatePopUpURL = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGetFlatRatePopUpURL = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnGetPopUpURL = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGetXML = new System.Windows.Forms.Button();
            this.btnGetTaxinvocie = new System.Windows.Forms.Button();
            this.txtNTSconfirmNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtJobID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnRequestJob = new System.Windows.Forms.Button();
            this.btnGetJobState = new System.Windows.Forms.Button();
            this.btnListActiveJob = new System.Windows.Forms.Button();
            this.btnGetPrintURL = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(367, 15);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(143, 21);
            this.txtUserId.TabIndex = 15;
            this.txtUserId.Text = "testkorea";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.groupBox13);
            this.GroupBox1.Controls.Add(this.groupBox12);
            this.GroupBox1.Controls.Add(this.groupBox4);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Controls.Add(this.GroupBox6);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Location = new System.Drawing.Point(12, 42);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(999, 145);
            this.GroupBox1.TabIndex = 16;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnGetPartnerBalance1);
            this.groupBox13.Controls.Add(this.btnGetPartnerURL_CHRG);
            this.groupBox13.Location = new System.Drawing.Point(431, 15);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(133, 118);
            this.groupBox13.TabIndex = 6;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "파트너과금 포인트";
            // 
            // btnGetPartnerBalance1
            // 
            this.btnGetPartnerBalance1.Location = new System.Drawing.Point(8, 19);
            this.btnGetPartnerBalance1.Name = "btnGetPartnerBalance1";
            this.btnGetPartnerBalance1.Size = new System.Drawing.Size(119, 29);
            this.btnGetPartnerBalance1.TabIndex = 4;
            this.btnGetPartnerBalance1.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance1.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance1.Click += new System.EventHandler(this.btnGetPartnerBalance1_Click);
            // 
            // btnGetPartnerURL_CHRG
            // 
            this.btnGetPartnerURL_CHRG.Location = new System.Drawing.Point(8, 52);
            this.btnGetPartnerURL_CHRG.Name = "btnGetPartnerURL_CHRG";
            this.btnGetPartnerURL_CHRG.Size = new System.Drawing.Size(119, 29);
            this.btnGetPartnerURL_CHRG.TabIndex = 0;
            this.btnGetPartnerURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPartnerURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPartnerURL_CHRG.Click += new System.EventHandler(this.btnGetPartnerURL_CHRG_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnGetChargeURL);
            this.groupBox12.Controls.Add(this.btnGetBalance);
            this.groupBox12.Location = new System.Drawing.Point(291, 16);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(133, 118);
            this.groupBox12.TabIndex = 5;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "연동과금 포인트";
            // 
            // btnGetChargeURL
            // 
            this.btnGetChargeURL.Location = new System.Drawing.Point(8, 54);
            this.btnGetChargeURL.Name = "btnGetChargeURL";
            this.btnGetChargeURL.Size = new System.Drawing.Size(119, 29);
            this.btnGetChargeURL.TabIndex = 1;
            this.btnGetChargeURL.Text = "포인트 충전 URL";
            this.btnGetChargeURL.UseVisualStyleBackColor = true;
            this.btnGetChargeURL.Click += new System.EventHandler(this.btnGetChargeURL_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(8, 19);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(119, 29);
            this.btnGetBalance.TabIndex = 2;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox4.Controls.Add(this.btnGetCorpInfo);
            this.groupBox4.Location = new System.Drawing.Point(851, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(133, 118);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "회사정보 관련";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(7, 53);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(119, 29);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(7, 20);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(119, 29);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnGetChargeInfo);
            this.GroupBox3.Location = new System.Drawing.Point(151, 15);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(133, 118);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(6, 20);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(119, 29);
            this.btnGetChargeInfo.TabIndex = 5;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnCheckID);
            this.GroupBox2.Controls.Add(this.btnCheckIsMember);
            this.GroupBox2.Controls.Add(this.btnJoinMember);
            this.GroupBox2.Location = new System.Drawing.Point(11, 15);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(133, 118);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(6, 52);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(119, 29);
            this.btnCheckID.TabIndex = 3;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(6, 19);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(119, 29);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(6, 84);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(119, 29);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.btnUpdateContact);
            this.GroupBox6.Controls.Add(this.btnListContact);
            this.GroupBox6.Controls.Add(this.btnRegistContact);
            this.GroupBox6.Location = new System.Drawing.Point(711, 14);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(133, 118);
            this.GroupBox6.TabIndex = 3;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "담당자 관련";
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(6, 83);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(119, 29);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(6, 51);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(119, 29);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(6, 21);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(119, 29);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.btnGetAccessURL);
            this.GroupBox5.Location = new System.Drawing.Point(571, 15);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(133, 118);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
            // 
            // btnGetAccessURL
            // 
            this.btnGetAccessURL.Location = new System.Drawing.Point(6, 19);
            this.btnGetAccessURL.Name = "btnGetAccessURL";
            this.btnGetAccessURL.Size = new System.Drawing.Size(119, 29);
            this.btnGetAccessURL.TabIndex = 0;
            this.btnGetAccessURL.Text = "팝빌 로그인 URL";
            this.btnGetAccessURL.UseVisualStyleBackColor = true;
            this.btnGetAccessURL.Click += new System.EventHandler(this.btnGetAccessURL_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(292, 18);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 12);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "팝빌아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(136, 15);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(143, 21);
            this.txtCorpNum.TabIndex = 13;
            this.txtCorpNum.Text = "1234567890";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 12);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "팝빌회원 사업자번호 : ";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox14);
            this.groupBox7.Controls.Add(this.groupBox11);
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.listBox1);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.txtJobID);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(12, 200);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(999, 455);
            this.groupBox7.TabIndex = 17;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "홈택스 전자(세금)계산서 연계 관련 API";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btnDeleteDeptUser);
            this.groupBox14.Controls.Add(this.btnCheckLoginDeptUser);
            this.groupBox14.Controls.Add(this.btnCheckDeptUser);
            this.groupBox14.Controls.Add(this.btnRegistDeptUser);
            this.groupBox14.Controls.Add(this.btnCheckCertValidation);
            this.groupBox14.Controls.Add(this.btnGetCertificateExpireDate);
            this.groupBox14.Controls.Add(this.btnGetCertificatePopUpURL);
            this.groupBox14.Location = new System.Drawing.Point(808, 21);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(177, 264);
            this.groupBox14.TabIndex = 8;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "홈택스 인증관련 기능";
            // 
            // btnDeleteDeptUser
            // 
            this.btnDeleteDeptUser.Location = new System.Drawing.Point(6, 222);
            this.btnDeleteDeptUser.Name = "btnDeleteDeptUser";
            this.btnDeleteDeptUser.Size = new System.Drawing.Size(163, 29);
            this.btnDeleteDeptUser.TabIndex = 10;
            this.btnDeleteDeptUser.Text = "부서사용자 등록정보 삭제";
            this.btnDeleteDeptUser.UseVisualStyleBackColor = true;
            this.btnDeleteDeptUser.Click += new System.EventHandler(this.btnDeleteDeptUser_Click);
            // 
            // btnCheckLoginDeptUser
            // 
            this.btnCheckLoginDeptUser.Location = new System.Drawing.Point(6, 188);
            this.btnCheckLoginDeptUser.Name = "btnCheckLoginDeptUser";
            this.btnCheckLoginDeptUser.Size = new System.Drawing.Size(163, 29);
            this.btnCheckLoginDeptUser.TabIndex = 9;
            this.btnCheckLoginDeptUser.Text = "부서사용자 로그인 테스트";
            this.btnCheckLoginDeptUser.UseVisualStyleBackColor = true;
            this.btnCheckLoginDeptUser.Click += new System.EventHandler(this.btnCheckLoginDeptUser_Click);
            // 
            // btnCheckDeptUser
            // 
            this.btnCheckDeptUser.Location = new System.Drawing.Point(6, 154);
            this.btnCheckDeptUser.Name = "btnCheckDeptUser";
            this.btnCheckDeptUser.Size = new System.Drawing.Size(163, 29);
            this.btnCheckDeptUser.TabIndex = 8;
            this.btnCheckDeptUser.Text = "부서사용자 등록정보 확인";
            this.btnCheckDeptUser.UseVisualStyleBackColor = true;
            this.btnCheckDeptUser.Click += new System.EventHandler(this.btnCheckDeptUser_Click);
            // 
            // btnRegistDeptUser
            // 
            this.btnRegistDeptUser.Location = new System.Drawing.Point(6, 120);
            this.btnRegistDeptUser.Name = "btnRegistDeptUser";
            this.btnRegistDeptUser.Size = new System.Drawing.Size(163, 29);
            this.btnRegistDeptUser.TabIndex = 7;
            this.btnRegistDeptUser.Text = "부서사용자 계정등록";
            this.btnRegistDeptUser.UseVisualStyleBackColor = true;
            this.btnRegistDeptUser.Click += new System.EventHandler(this.btnRegistDeptUser_Click);
            // 
            // btnCheckCertValidation
            // 
            this.btnCheckCertValidation.Location = new System.Drawing.Point(6, 86);
            this.btnCheckCertValidation.Name = "btnCheckCertValidation";
            this.btnCheckCertValidation.Size = new System.Drawing.Size(163, 29);
            this.btnCheckCertValidation.TabIndex = 6;
            this.btnCheckCertValidation.Text = "공인인증서 로그인 테스트";
            this.btnCheckCertValidation.UseVisualStyleBackColor = true;
            this.btnCheckCertValidation.Click += new System.EventHandler(this.btnCheckCertValidation_Click);
            // 
            // btnGetCertificateExpireDate
            // 
            this.btnGetCertificateExpireDate.Location = new System.Drawing.Point(6, 54);
            this.btnGetCertificateExpireDate.Name = "btnGetCertificateExpireDate";
            this.btnGetCertificateExpireDate.Size = new System.Drawing.Size(163, 29);
            this.btnGetCertificateExpireDate.TabIndex = 5;
            this.btnGetCertificateExpireDate.Text = "공인인증서 만료일자 확인";
            this.btnGetCertificateExpireDate.UseVisualStyleBackColor = true;
            this.btnGetCertificateExpireDate.Click += new System.EventHandler(this.btnGetCertificateExpireDate_Click);
            // 
            // btnGetCertificatePopUpURL
            // 
            this.btnGetCertificatePopUpURL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetCertificatePopUpURL.Location = new System.Drawing.Point(6, 21);
            this.btnGetCertificatePopUpURL.Name = "btnGetCertificatePopUpURL";
            this.btnGetCertificatePopUpURL.Size = new System.Drawing.Size(163, 29);
            this.btnGetCertificatePopUpURL.TabIndex = 4;
            this.btnGetCertificatePopUpURL.Text = "홈택스연동 인증관리 URL";
            this.btnGetCertificatePopUpURL.UseVisualStyleBackColor = true;
            this.btnGetCertificatePopUpURL.Click += new System.EventHandler(this.btnGetCertificatePopUpURL_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.button1);
            this.groupBox11.Controls.Add(this.btnGetFlatRatePopUpURL);
            this.groupBox11.Location = new System.Drawing.Point(617, 21);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(177, 196);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "부가기능";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "정액제 서비스 상태 확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGetFlatRatePopUpURL
            // 
            this.btnGetFlatRatePopUpURL.Location = new System.Drawing.Point(8, 23);
            this.btnGetFlatRatePopUpURL.Name = "btnGetFlatRatePopUpURL";
            this.btnGetFlatRatePopUpURL.Size = new System.Drawing.Size(163, 29);
            this.btnGetFlatRatePopUpURL.TabIndex = 0;
            this.btnGetFlatRatePopUpURL.Text = "정액제 서비스 신청 URL";
            this.btnGetFlatRatePopUpURL.UseVisualStyleBackColor = true;
            this.btnGetFlatRatePopUpURL.Click += new System.EventHandler(this.btnGetFlatRatePopUpURL_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnGetPrintURL);
            this.groupBox10.Controls.Add(this.btnGetPopUpURL);
            this.groupBox10.Controls.Add(this.label6);
            this.groupBox10.Controls.Add(this.btnGetXML);
            this.groupBox10.Controls.Add(this.btnGetTaxinvocie);
            this.groupBox10.Controls.Add(this.txtNTSconfirmNum);
            this.groupBox10.Controls.Add(this.label5);
            this.groupBox10.Location = new System.Drawing.Point(338, 21);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(265, 196);
            this.groupBox10.TabIndex = 6;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "전자(세금)계산서 상세정보 조회";
            // 
            // btnGetPopUpURL
            // 
            this.btnGetPopUpURL.Location = new System.Drawing.Point(112, 115);
            this.btnGetPopUpURL.Name = "btnGetPopUpURL";
            this.btnGetPopUpURL.Size = new System.Drawing.Size(141, 29);
            this.btnGetPopUpURL.TabIndex = 5;
            this.btnGetPopUpURL.Text = "세금계산서 보기 팝업";
            this.btnGetPopUpURL.UseVisualStyleBackColor = true;
            this.btnGetPopUpURL.Click += new System.EventHandler(this.btnGetPopUpURL_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 28);
            this.label6.TabIndex = 4;
            this.label6.Text = "(\'수집 결과 조회\' 후 반환된 국세청 승인번호를 입력하세요.)";
            // 
            // btnGetXML
            // 
            this.btnGetXML.Location = new System.Drawing.Point(111, 80);
            this.btnGetXML.Name = "btnGetXML";
            this.btnGetXML.Size = new System.Drawing.Size(142, 29);
            this.btnGetXML.TabIndex = 3;
            this.btnGetXML.Text = "상세정보 조회 - XML";
            this.btnGetXML.UseVisualStyleBackColor = true;
            this.btnGetXML.Click += new System.EventHandler(this.btnGetXML_Click);
            // 
            // btnGetTaxinvocie
            // 
            this.btnGetTaxinvocie.Location = new System.Drawing.Point(8, 80);
            this.btnGetTaxinvocie.Name = "btnGetTaxinvocie";
            this.btnGetTaxinvocie.Size = new System.Drawing.Size(92, 29);
            this.btnGetTaxinvocie.TabIndex = 2;
            this.btnGetTaxinvocie.Text = "상세정보 조회";
            this.btnGetTaxinvocie.UseVisualStyleBackColor = true;
            this.btnGetTaxinvocie.Click += new System.EventHandler(this.btnGetTaxinvocie_Click);
            // 
            // txtNTSconfirmNum
            // 
            this.txtNTSconfirmNum.Location = new System.Drawing.Point(102, 18);
            this.txtNTSconfirmNum.Name = "txtNTSconfirmNum";
            this.txtNTSconfirmNum.Size = new System.Drawing.Size(157, 21);
            this.txtNTSconfirmNum.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "국세청승인번호 : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "(작업아이디는 \'수집 요청\' 호출시 생성됩니다.)";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(13, 255);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(783, 172);
            this.listBox1.TabIndex = 4;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSummary);
            this.groupBox9.Controls.Add(this.btnSearch);
            this.groupBox9.Location = new System.Drawing.Point(164, 21);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(160, 196);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "매출/매입 수집결과 조회";
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(6, 55);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(148, 29);
            this.btnSummary.TabIndex = 1;
            this.btnSummary.Text = "수집 결과 요약정보 조회";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(148, 29);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "수집 결과 조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtJobID
            // 
            this.txtJobID.Location = new System.Drawing.Point(132, 223);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(134, 21);
            this.txtJobID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "작업아이디 (jobID) :";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnRequestJob);
            this.groupBox8.Controls.Add(this.btnGetJobState);
            this.groupBox8.Controls.Add(this.btnListActiveJob);
            this.groupBox8.Location = new System.Drawing.Point(11, 21);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(139, 196);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "매출/매입 내역 수집";
            // 
            // btnRequestJob
            // 
            this.btnRequestJob.Location = new System.Drawing.Point(7, 20);
            this.btnRequestJob.Name = "btnRequestJob";
            this.btnRequestJob.Size = new System.Drawing.Size(125, 29);
            this.btnRequestJob.TabIndex = 2;
            this.btnRequestJob.Text = "수집 요청";
            this.btnRequestJob.UseVisualStyleBackColor = true;
            this.btnRequestJob.Click += new System.EventHandler(this.btnRequestJob_Click);
            // 
            // btnGetJobState
            // 
            this.btnGetJobState.Location = new System.Drawing.Point(7, 55);
            this.btnGetJobState.Name = "btnGetJobState";
            this.btnGetJobState.Size = new System.Drawing.Size(125, 29);
            this.btnGetJobState.TabIndex = 1;
            this.btnGetJobState.Text = "수집 상태 확인";
            this.btnGetJobState.UseVisualStyleBackColor = true;
            this.btnGetJobState.Click += new System.EventHandler(this.btnGetJobState_Click);
            // 
            // btnListActiveJob
            // 
            this.btnListActiveJob.Location = new System.Drawing.Point(7, 90);
            this.btnListActiveJob.Name = "btnListActiveJob";
            this.btnListActiveJob.Size = new System.Drawing.Size(125, 29);
            this.btnListActiveJob.TabIndex = 0;
            this.btnListActiveJob.Text = "수집 상태 목록 확인";
            this.btnListActiveJob.UseVisualStyleBackColor = true;
            this.btnListActiveJob.Click += new System.EventHandler(this.btnListActiveJob_Click);
            // 
            // btnGetPrintURL
            // 
            this.btnGetPrintURL.Location = new System.Drawing.Point(112, 148);
            this.btnGetPrintURL.Name = "btnGetPrintURL";
            this.btnGetPrintURL.Size = new System.Drawing.Size(141, 30);
            this.btnGetPrintURL.TabIndex = 6;
            this.btnGetPrintURL.Text = "세금계산서 인쇄 팝업";
            this.btnGetPrintURL.UseVisualStyleBackColor = true;
            this.btnGetPrintURL.Click += new System.EventHandler(this.btnGetPrintURL_Click);
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 677);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 홈택스 전자(세금)계산서 연동 API SDK Example";
            this.GroupBox1.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtUserId;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        private System.Windows.Forms.Button btnGetCorpInfo;
        internal System.Windows.Forms.GroupBox GroupBox6;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnRegistContact;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Button btnGetChargeURL;
        internal System.Windows.Forms.Button btnGetAccessURL;
        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.Button btnGetPartnerBalance1;
        internal System.Windows.Forms.Button btnGetBalance;
        internal System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.Button btnCheckID;
        internal System.Windows.Forms.Button btnCheckIsMember;
        internal System.Windows.Forms.Button btnJoinMember;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCorpNum;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnGetChargeInfo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnListActiveJob;
        private System.Windows.Forms.Button btnGetJobState;
        private System.Windows.Forms.Button btnRequestJob;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNTSconfirmNum;
        private System.Windows.Forms.Button btnGetTaxinvocie;
        private System.Windows.Forms.Button btnGetXML;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnGetFlatRatePopUpURL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btnGetPartnerURL_CHRG;
        private System.Windows.Forms.Button btnGetPopUpURL;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button btnDeleteDeptUser;
        private System.Windows.Forms.Button btnCheckLoginDeptUser;
        private System.Windows.Forms.Button btnCheckDeptUser;
        private System.Windows.Forms.Button btnRegistDeptUser;
        private System.Windows.Forms.Button btnCheckCertValidation;
        private System.Windows.Forms.Button btnGetCertificateExpireDate;
        private System.Windows.Forms.Button btnGetCertificatePopUpURL;
        private System.Windows.Forms.Button btnGetPrintURL;
    }
}

