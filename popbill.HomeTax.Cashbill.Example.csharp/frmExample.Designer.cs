namespace Popbill.HomeTax.Cashbill.Example.csharp
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnGetCertificateExpireDate = new System.Windows.Forms.Button();
            this.btnGetCertificatePopUpURL = new System.Windows.Forms.Button();
            this.btnGetFlatRateState = new System.Windows.Forms.Button();
            this.btnGetFlatRatePopUpURL = new System.Windows.Forms.Button();
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
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.btnGetPopbillURL_CHRG = new System.Windows.Forms.Button();
            this.getPopbillURL_LOGIN = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance1 = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox11);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.listBox1);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.txtJobID);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(12, 189);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(693, 456);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "홈택스 전자(세금)계산서 연계 관련 API";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnGetCertificateExpireDate);
            this.groupBox11.Controls.Add(this.btnGetCertificatePopUpURL);
            this.groupBox11.Controls.Add(this.btnGetFlatRateState);
            this.groupBox11.Controls.Add(this.btnGetFlatRatePopUpURL);
            this.groupBox11.Location = new System.Drawing.Point(332, 19);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(177, 159);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "부가기능";
            // 
            // btnGetCertificateExpireDate
            // 
            this.btnGetCertificateExpireDate.Location = new System.Drawing.Point(8, 122);
            this.btnGetCertificateExpireDate.Name = "btnGetCertificateExpireDate";
            this.btnGetCertificateExpireDate.Size = new System.Drawing.Size(163, 28);
            this.btnGetCertificateExpireDate.TabIndex = 3;
            this.btnGetCertificateExpireDate.Text = "공인인증서 만료일자 확인";
            this.btnGetCertificateExpireDate.UseVisualStyleBackColor = true;
            this.btnGetCertificateExpireDate.Click += new System.EventHandler(this.btnGetCertificateExpireDate_Click);
            // 
            // btnGetCertificatePopUpURL
            // 
            this.btnGetCertificatePopUpURL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetCertificatePopUpURL.Location = new System.Drawing.Point(8, 89);
            this.btnGetCertificatePopUpURL.Name = "btnGetCertificatePopUpURL";
            this.btnGetCertificatePopUpURL.Size = new System.Drawing.Size(163, 28);
            this.btnGetCertificatePopUpURL.TabIndex = 2;
            this.btnGetCertificatePopUpURL.Text = "공인인증서 등록 URL";
            this.btnGetCertificatePopUpURL.UseVisualStyleBackColor = true;
            this.btnGetCertificatePopUpURL.Click += new System.EventHandler(this.btnGetCertificatePopUpURL_Click);
            // 
            // btnGetFlatRateState
            // 
            this.btnGetFlatRateState.Location = new System.Drawing.Point(8, 56);
            this.btnGetFlatRateState.Name = "btnGetFlatRateState";
            this.btnGetFlatRateState.Size = new System.Drawing.Size(163, 28);
            this.btnGetFlatRateState.TabIndex = 1;
            this.btnGetFlatRateState.Text = "정액제 서비스 상태 확인";
            this.btnGetFlatRateState.UseVisualStyleBackColor = true;
            this.btnGetFlatRateState.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGetFlatRatePopUpURL
            // 
            this.btnGetFlatRatePopUpURL.Location = new System.Drawing.Point(8, 23);
            this.btnGetFlatRatePopUpURL.Name = "btnGetFlatRatePopUpURL";
            this.btnGetFlatRatePopUpURL.Size = new System.Drawing.Size(163, 28);
            this.btnGetFlatRatePopUpURL.TabIndex = 0;
            this.btnGetFlatRatePopUpURL.Text = "정액제 서비스 신청 URL";
            this.btnGetFlatRatePopUpURL.UseVisualStyleBackColor = true;
            this.btnGetFlatRatePopUpURL.Click += new System.EventHandler(this.btnGetFlatRatePopUpURL_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "(작업아이디는 \'수집 요청\' 호출시 생성됩니다.)";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(21, 214);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(631, 256);
            this.listBox1.TabIndex = 4;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSummary);
            this.groupBox9.Controls.Add(this.btnSearch);
            this.groupBox9.Location = new System.Drawing.Point(160, 21);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(160, 131);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "매출/매입 수집결과 조회";
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(6, 55);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(148, 32);
            this.btnSummary.TabIndex = 1;
            this.btnSummary.Text = "수집 결과 요약정보 조회";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(148, 31);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "수집 결과 조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtJobID
            // 
            this.txtJobID.Location = new System.Drawing.Point(140, 189);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(134, 21);
            this.txtJobID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 194);
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
            this.groupBox8.Size = new System.Drawing.Size(139, 130);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "매출/매입 내역 수집";
            // 
            // btnRequestJob
            // 
            this.btnRequestJob.Location = new System.Drawing.Point(7, 20);
            this.btnRequestJob.Name = "btnRequestJob";
            this.btnRequestJob.Size = new System.Drawing.Size(125, 31);
            this.btnRequestJob.TabIndex = 2;
            this.btnRequestJob.Text = "수집 요청";
            this.btnRequestJob.UseVisualStyleBackColor = true;
            this.btnRequestJob.Click += new System.EventHandler(this.btnRequestJob_Click);
            // 
            // btnGetJobState
            // 
            this.btnGetJobState.Location = new System.Drawing.Point(7, 55);
            this.btnGetJobState.Name = "btnGetJobState";
            this.btnGetJobState.Size = new System.Drawing.Size(125, 31);
            this.btnGetJobState.TabIndex = 1;
            this.btnGetJobState.Text = "수집 상태 확인";
            this.btnGetJobState.UseVisualStyleBackColor = true;
            this.btnGetJobState.Click += new System.EventHandler(this.btnGetJobState_Click);
            // 
            // btnListActiveJob
            // 
            this.btnListActiveJob.Location = new System.Drawing.Point(7, 90);
            this.btnListActiveJob.Name = "btnListActiveJob";
            this.btnListActiveJob.Size = new System.Drawing.Size(125, 31);
            this.btnListActiveJob.TabIndex = 0;
            this.btnListActiveJob.Text = "수집 상태 목록 확인";
            this.btnListActiveJob.UseVisualStyleBackColor = true;
            this.btnListActiveJob.Click += new System.EventHandler(this.btnListActiveJob_Click);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(370, 6);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(143, 21);
            this.txtUserId.TabIndex = 21;
            this.txtUserId.Text = "testkorea";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.groupBox4);
            this.GroupBox1.Controls.Add(this.GroupBox6);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Location = new System.Drawing.Point(12, 31);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(693, 145);
            this.GroupBox1.TabIndex = 22;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox4.Controls.Add(this.btnGetCorpInfo);
            this.groupBox4.Location = new System.Drawing.Point(557, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(126, 121);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "회사정보 관련";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(7, 53);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(111, 30);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(7, 20);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(111, 30);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.btnUpdateContact);
            this.GroupBox6.Controls.Add(this.btnListContact);
            this.GroupBox6.Controls.Add(this.btnRegistContact);
            this.GroupBox6.Location = new System.Drawing.Point(418, 14);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(132, 121);
            this.GroupBox6.TabIndex = 3;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "담당자 관련";
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(6, 83);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(117, 30);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(6, 51);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(117, 30);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(6, 21);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(117, 28);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.btnGetPopbillURL_CHRG);
            this.GroupBox5.Controls.Add(this.getPopbillURL_LOGIN);
            this.GroupBox5.Location = new System.Drawing.Point(280, 15);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(131, 122);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
            // 
            // btnGetPopbillURL_CHRG
            // 
            this.btnGetPopbillURL_CHRG.Location = new System.Drawing.Point(6, 52);
            this.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG";
            this.btnGetPopbillURL_CHRG.Size = new System.Drawing.Size(116, 29);
            this.btnGetPopbillURL_CHRG.TabIndex = 1;
            this.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_CHRG.Click += new System.EventHandler(this.btnGetPopbillURL_CHRG_Click);
            // 
            // getPopbillURL_LOGIN
            // 
            this.getPopbillURL_LOGIN.Location = new System.Drawing.Point(6, 19);
            this.getPopbillURL_LOGIN.Name = "getPopbillURL_LOGIN";
            this.getPopbillURL_LOGIN.Size = new System.Drawing.Size(116, 29);
            this.getPopbillURL_LOGIN.TabIndex = 0;
            this.getPopbillURL_LOGIN.Text = "팝빌 로그인 URL";
            this.getPopbillURL_LOGIN.UseVisualStyleBackColor = true;
            this.getPopbillURL_LOGIN.Click += new System.EventHandler(this.getPopbillURL_LOGIN_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnGetChargeInfo);
            this.GroupBox3.Controls.Add(this.btnGetPartnerBalance1);
            this.GroupBox3.Controls.Add(this.btnGetBalance);
            this.GroupBox3.Location = new System.Drawing.Point(143, 15);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(131, 123);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(6, 86);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(119, 30);
            this.btnGetChargeInfo.TabIndex = 5;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // btnGetPartnerBalance1
            // 
            this.btnGetPartnerBalance1.Location = new System.Drawing.Point(6, 53);
            this.btnGetPartnerBalance1.Name = "btnGetPartnerBalance1";
            this.btnGetPartnerBalance1.Size = new System.Drawing.Size(119, 30);
            this.btnGetPartnerBalance1.TabIndex = 4;
            this.btnGetPartnerBalance1.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance1.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance1.Click += new System.EventHandler(this.btnGetPartnerBalance1_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(6, 19);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(118, 30);
            this.btnGetBalance.TabIndex = 2;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnCheckID);
            this.GroupBox2.Controls.Add(this.btnCheckIsMember);
            this.GroupBox2.Controls.Add(this.btnJoinMember);
            this.GroupBox2.Location = new System.Drawing.Point(11, 15);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(126, 123);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(6, 52);
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
            this.btnCheckIsMember.Size = new System.Drawing.Size(112, 30);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(6, 84);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(112, 31);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(294, 11);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 12);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "팝빌아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(140, 7);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(143, 21);
            this.txtCorpNum.TabIndex = 19;
            this.txtCorpNum.Text = "1234567890";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 12);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "팝빌회원 사업자번호 : ";
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 719);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 홈택스 현금영수증 연계 API SDK Example";
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnGetCertificateExpireDate;
        private System.Windows.Forms.Button btnGetCertificatePopUpURL;
        private System.Windows.Forms.Button btnGetFlatRateState;
        private System.Windows.Forms.Button btnGetFlatRatePopUpURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnRequestJob;
        private System.Windows.Forms.Button btnGetJobState;
        private System.Windows.Forms.Button btnListActiveJob;
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
        internal System.Windows.Forms.Button btnGetPopbillURL_CHRG;
        internal System.Windows.Forms.Button getPopbillURL_LOGIN;
        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.Button btnGetChargeInfo;
        private System.Windows.Forms.Button btnGetPartnerBalance1;
        internal System.Windows.Forms.Button btnGetBalance;
        internal System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.Button btnCheckID;
        internal System.Windows.Forms.Button btnCheckIsMember;
        internal System.Windows.Forms.Button btnJoinMember;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCorpNum;
        internal System.Windows.Forms.Label Label1;
    }
}

