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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnDeleteDeptUser = new System.Windows.Forms.Button();
            this.btnCheckLoginDeptUser = new System.Windows.Forms.Button();
            this.btnCheckDeptUser = new System.Windows.Forms.Button();
            this.btnRegistDeptUser = new System.Windows.Forms.Button();
            this.btnCheckCertValidation = new System.Windows.Forms.Button();
            this.btnGetCertificateExpireDate = new System.Windows.Forms.Button();
            this.btnGetCertificatePopUpURL = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
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
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetPartnerURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance1 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnGetUseHistoryURL = new System.Windows.Forms.Button();
            this.btnGetPaymentURL = new System.Windows.Forms.Button();
            this.btnGetChargeURL = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btnPaymentRequest = new System.Windows.Forms.Button();
            this.btnGetSettleResult = new System.Windows.Forms.Button();
            this.btnGetPaymentHistory = new System.Windows.Forms.Button();
            this.btnGetUseHistory = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnGetRefundHistory = new System.Windows.Forms.Button();
            this.btnGetRefundableBalance = new System.Windows.Forms.Button();
            this.btnGetRefundInfo = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetContactInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.btnQuitMember = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.btnGetAccessURL = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textURL = new System.Windows.Forms.TextBox();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Controls.Add(this.groupBox11);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.listBox1);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.txtJobID);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(12, 272);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(996, 486);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "홈택스 현금영수증 연계 관련 API";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnDeleteDeptUser);
            this.groupBox10.Controls.Add(this.btnCheckLoginDeptUser);
            this.groupBox10.Controls.Add(this.btnCheckDeptUser);
            this.groupBox10.Controls.Add(this.btnRegistDeptUser);
            this.groupBox10.Controls.Add(this.btnCheckCertValidation);
            this.groupBox10.Controls.Add(this.btnGetCertificateExpireDate);
            this.groupBox10.Controls.Add(this.btnGetCertificatePopUpURL);
            this.groupBox10.Location = new System.Drawing.Point(603, 20);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(361, 159);
            this.groupBox10.TabIndex = 8;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "홈택스 인증관련 기능";
            // 
            // btnDeleteDeptUser
            // 
            this.btnDeleteDeptUser.Location = new System.Drawing.Point(184, 90);
            this.btnDeleteDeptUser.Name = "btnDeleteDeptUser";
            this.btnDeleteDeptUser.Size = new System.Drawing.Size(163, 28);
            this.btnDeleteDeptUser.TabIndex = 10;
            this.btnDeleteDeptUser.Text = "부서사용자 등록정보 삭제";
            this.btnDeleteDeptUser.UseVisualStyleBackColor = true;
            this.btnDeleteDeptUser.Click += new System.EventHandler(this.btnDeleteDeptUser_Click);
            // 
            // btnCheckLoginDeptUser
            // 
            this.btnCheckLoginDeptUser.Location = new System.Drawing.Point(184, 55);
            this.btnCheckLoginDeptUser.Name = "btnCheckLoginDeptUser";
            this.btnCheckLoginDeptUser.Size = new System.Drawing.Size(163, 28);
            this.btnCheckLoginDeptUser.TabIndex = 9;
            this.btnCheckLoginDeptUser.Text = "부서사용자 로그인 테스트";
            this.btnCheckLoginDeptUser.UseVisualStyleBackColor = true;
            this.btnCheckLoginDeptUser.Click += new System.EventHandler(this.btnCheckLoginDeptUser_Click);
            // 
            // btnCheckDeptUser
            // 
            this.btnCheckDeptUser.Location = new System.Drawing.Point(184, 20);
            this.btnCheckDeptUser.Name = "btnCheckDeptUser";
            this.btnCheckDeptUser.Size = new System.Drawing.Size(163, 28);
            this.btnCheckDeptUser.TabIndex = 8;
            this.btnCheckDeptUser.Text = "부서사용자 등록정보 확인";
            this.btnCheckDeptUser.UseVisualStyleBackColor = true;
            this.btnCheckDeptUser.Click += new System.EventHandler(this.btnCheckDeptUser_Click);
            // 
            // btnRegistDeptUser
            // 
            this.btnRegistDeptUser.Location = new System.Drawing.Point(6, 124);
            this.btnRegistDeptUser.Name = "btnRegistDeptUser";
            this.btnRegistDeptUser.Size = new System.Drawing.Size(163, 28);
            this.btnRegistDeptUser.TabIndex = 7;
            this.btnRegistDeptUser.Text = "부서사용자 계정등록";
            this.btnRegistDeptUser.UseVisualStyleBackColor = true;
            this.btnRegistDeptUser.Click += new System.EventHandler(this.btnRegistDeptUser_Click);
            // 
            // btnCheckCertValidation
            // 
            this.btnCheckCertValidation.Location = new System.Drawing.Point(6, 90);
            this.btnCheckCertValidation.Name = "btnCheckCertValidation";
            this.btnCheckCertValidation.Size = new System.Drawing.Size(163, 28);
            this.btnCheckCertValidation.TabIndex = 6;
            this.btnCheckCertValidation.Text = "인증서 로그인 테스트";
            this.btnCheckCertValidation.UseVisualStyleBackColor = true;
            this.btnCheckCertValidation.Click += new System.EventHandler(this.btnCheckCertValidation_Click);
            // 
            // btnGetCertificateExpireDate
            // 
            this.btnGetCertificateExpireDate.Location = new System.Drawing.Point(6, 55);
            this.btnGetCertificateExpireDate.Name = "btnGetCertificateExpireDate";
            this.btnGetCertificateExpireDate.Size = new System.Drawing.Size(163, 28);
            this.btnGetCertificateExpireDate.TabIndex = 5;
            this.btnGetCertificateExpireDate.Text = "인증서 만료일자 확인";
            this.btnGetCertificateExpireDate.UseVisualStyleBackColor = true;
            this.btnGetCertificateExpireDate.Click += new System.EventHandler(this.btnGetCertificateExpireDate_Click);
            // 
            // btnGetCertificatePopUpURL
            // 
            this.btnGetCertificatePopUpURL.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetCertificatePopUpURL.Location = new System.Drawing.Point(6, 20);
            this.btnGetCertificatePopUpURL.Name = "btnGetCertificatePopUpURL";
            this.btnGetCertificatePopUpURL.Size = new System.Drawing.Size(163, 28);
            this.btnGetCertificatePopUpURL.TabIndex = 4;
            this.btnGetCertificatePopUpURL.Text = "홈택스연동 인증관리 URL";
            this.btnGetCertificatePopUpURL.UseVisualStyleBackColor = true;
            this.btnGetCertificatePopUpURL.Click += new System.EventHandler(this.btnGetCertificatePopUpURL_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnGetFlatRateState);
            this.groupBox11.Controls.Add(this.btnGetFlatRatePopUpURL);
            this.groupBox11.Location = new System.Drawing.Point(396, 20);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(177, 159);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "부가기능";
            // 
            // btnGetFlatRateState
            // 
            this.btnGetFlatRateState.Location = new System.Drawing.Point(8, 55);
            this.btnGetFlatRateState.Name = "btnGetFlatRateState";
            this.btnGetFlatRateState.Size = new System.Drawing.Size(163, 31);
            this.btnGetFlatRateState.TabIndex = 1;
            this.btnGetFlatRateState.Text = "정액제 서비스 상태 확인";
            this.btnGetFlatRateState.UseVisualStyleBackColor = true;
            this.btnGetFlatRateState.Click += new System.EventHandler(this.btnGetFlatRateState_Click);
            // 
            // btnGetFlatRatePopUpURL
            // 
            this.btnGetFlatRatePopUpURL.Location = new System.Drawing.Point(8, 20);
            this.btnGetFlatRatePopUpURL.Name = "btnGetFlatRatePopUpURL";
            this.btnGetFlatRatePopUpURL.Size = new System.Drawing.Size(163, 31);
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
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(11, 214);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(953, 256);
            this.listBox1.TabIndex = 4;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSummary);
            this.groupBox9.Controls.Add(this.btnSearch);
            this.groupBox9.Location = new System.Drawing.Point(195, 21);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(160, 157);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "매출/매입 수집결과 조회";
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(6, 55);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(148, 31);
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
            this.groupBox8.Size = new System.Drawing.Size(139, 158);
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
            this.btnListActiveJob.Size = new System.Drawing.Size(125, 34);
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
            this.GroupBox1.Controls.Add(this.groupBox13);
            this.GroupBox1.Controls.Add(this.groupBox12);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Location = new System.Drawing.Point(12, 31);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(996, 235);
            this.GroupBox1.TabIndex = 22;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnGetPartnerURL_CHRG);
            this.groupBox13.Controls.Add(this.btnGetPartnerBalance1);
            this.groupBox13.Location = new System.Drawing.Point(708, 23);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(133, 158);
            this.groupBox13.TabIndex = 25;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "파트너과금 포인트";
            // 
            // btnGetPartnerURL_CHRG
            // 
            this.btnGetPartnerURL_CHRG.Location = new System.Drawing.Point(8, 52);
            this.btnGetPartnerURL_CHRG.Name = "btnGetPartnerURL_CHRG";
            this.btnGetPartnerURL_CHRG.Size = new System.Drawing.Size(119, 30);
            this.btnGetPartnerURL_CHRG.TabIndex = 0;
            this.btnGetPartnerURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPartnerURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPartnerURL_CHRG.Click += new System.EventHandler(this.btnGetPartnerURL_Click);
            // 
            // btnGetPartnerBalance1
            // 
            this.btnGetPartnerBalance1.Location = new System.Drawing.Point(8, 19);
            this.btnGetPartnerBalance1.Name = "btnGetPartnerBalance1";
            this.btnGetPartnerBalance1.Size = new System.Drawing.Size(119, 30);
            this.btnGetPartnerBalance1.TabIndex = 4;
            this.btnGetPartnerBalance1.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance1.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance1.Click += new System.EventHandler(this.btnGetPartnerBalance1_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnGetUseHistoryURL);
            this.groupBox12.Controls.Add(this.btnGetPaymentURL);
            this.groupBox12.Controls.Add(this.btnGetChargeURL);
            this.groupBox12.Controls.Add(this.btnGetBalance);
            this.groupBox12.Location = new System.Drawing.Point(550, 24);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(152, 157);
            this.groupBox12.TabIndex = 24;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "연동과금 포인트";
            // 
            // btnGetUseHistoryURL
            // 
            this.btnGetUseHistoryURL.Location = new System.Drawing.Point(6, 116);
            this.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL";
            this.btnGetUseHistoryURL.Size = new System.Drawing.Size(140, 30);
            this.btnGetUseHistoryURL.TabIndex = 4;
            this.btnGetUseHistoryURL.Text = "포인트 사용내역 URL";
            this.btnGetUseHistoryURL.UseVisualStyleBackColor = true;
            this.btnGetUseHistoryURL.Click += new System.EventHandler(this.btnGetUseHistoryURL_Click);
            // 
            // btnGetPaymentURL
            // 
            this.btnGetPaymentURL.Location = new System.Drawing.Point(6, 84);
            this.btnGetPaymentURL.Name = "btnGetPaymentURL";
            this.btnGetPaymentURL.Size = new System.Drawing.Size(140, 30);
            this.btnGetPaymentURL.TabIndex = 3;
            this.btnGetPaymentURL.Text = "포인트 결제내역 URL";
            this.btnGetPaymentURL.UseVisualStyleBackColor = true;
            this.btnGetPaymentURL.Click += new System.EventHandler(this.btnGetPaymentURL_Click);
            // 
            // btnGetChargeURL
            // 
            this.btnGetChargeURL.Location = new System.Drawing.Point(6, 52);
            this.btnGetChargeURL.Name = "btnGetChargeURL";
            this.btnGetChargeURL.Size = new System.Drawing.Size(140, 30);
            this.btnGetChargeURL.TabIndex = 1;
            this.btnGetChargeURL.Text = "포인트 충전 URL";
            this.btnGetChargeURL.UseVisualStyleBackColor = true;
            this.btnGetChargeURL.Click += new System.EventHandler(this.btnGetChargeURL_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(6, 19);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(140, 30);
            this.btnGetBalance.TabIndex = 2;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnGetChargeInfo);
            this.GroupBox3.Controls.Add(this.btnPaymentRequest);
            this.GroupBox3.Controls.Add(this.btnGetSettleResult);
            this.GroupBox3.Controls.Add(this.btnGetPaymentHistory);
            this.GroupBox3.Controls.Add(this.btnGetUseHistory);
            this.GroupBox3.Controls.Add(this.btnRefund);
            this.GroupBox3.Controls.Add(this.btnGetRefundHistory);
            this.GroupBox3.Controls.Add(this.btnGetRefundableBalance);
            this.GroupBox3.Controls.Add(this.btnGetRefundInfo);
            this.GroupBox3.Location = new System.Drawing.Point(260, 15);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(284, 210);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(6, 19);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(130, 32);
            this.btnGetChargeInfo.TabIndex = 5;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // btnPaymentRequest
            // 
            this.btnPaymentRequest.Location = new System.Drawing.Point(6, 57);
            this.btnPaymentRequest.Name = "btnPaymentRequest";
            this.btnPaymentRequest.Size = new System.Drawing.Size(130, 32);
            this.btnPaymentRequest.TabIndex = 8;
            this.btnPaymentRequest.Text = "무통장 입금신청";
            this.btnPaymentRequest.Click += new System.EventHandler(this.btnPaymentRequest_Click);
            // 
            // btnGetSettleResult
            // 
            this.btnGetSettleResult.Location = new System.Drawing.Point(6, 95);
            this.btnGetSettleResult.Name = "btnGetSettleResult";
            this.btnGetSettleResult.Size = new System.Drawing.Size(130, 32);
            this.btnGetSettleResult.TabIndex = 0;
            this.btnGetSettleResult.Text = "무통장 입금신청 정보확인";
            this.btnGetSettleResult.Click += new System.EventHandler(this.btnGetSettleResult_Click);
            // 
            // btnGetPaymentHistory
            // 
            this.btnGetPaymentHistory.Location = new System.Drawing.Point(6, 133);
            this.btnGetPaymentHistory.Name = "btnGetPaymentHistory";
            this.btnGetPaymentHistory.Size = new System.Drawing.Size(130, 32);
            this.btnGetPaymentHistory.TabIndex = 0;
            this.btnGetPaymentHistory.Text = "포인트 결제내역 확인";
            this.btnGetPaymentHistory.Click += new System.EventHandler(this.btnGetPaymentHistory_Click);
            // 
            // btnGetUseHistory
            // 
            this.btnGetUseHistory.Location = new System.Drawing.Point(142, 20);
            this.btnGetUseHistory.Name = "btnGetUseHistory";
            this.btnGetUseHistory.Size = new System.Drawing.Size(130, 32);
            this.btnGetUseHistory.TabIndex = 0;
            this.btnGetUseHistory.Text = "포인트 사용내역 확인";
            this.btnGetUseHistory.Click += new System.EventHandler(this.btnGetUseHistory_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Location = new System.Drawing.Point(142, 58);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(130, 32);
            this.btnRefund.TabIndex = 0;
            this.btnRefund.Text = "포인트 환불신청";
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnGetRefundHistory
            // 
            this.btnGetRefundHistory.Location = new System.Drawing.Point(142, 96);
            this.btnGetRefundHistory.Name = "btnGetRefundHistory";
            this.btnGetRefundHistory.Size = new System.Drawing.Size(130, 32);
            this.btnGetRefundHistory.TabIndex = 0;
            this.btnGetRefundHistory.Text = "포인트 환불내역 확인";
            this.btnGetRefundHistory.Click += new System.EventHandler(this.btnGetRefundHistory_Click);
            // 
            // btnGetRefundableBalance
            // 
            this.btnGetRefundableBalance.Location = new System.Drawing.Point(142, 172);
            this.btnGetRefundableBalance.Name = "btnGetRefundableBalance";
            this.btnGetRefundableBalance.Size = new System.Drawing.Size(130, 32);
            this.btnGetRefundableBalance.TabIndex = 0;
            this.btnGetRefundableBalance.Text = "환불 가능 포인트 조회";
            this.btnGetRefundableBalance.Click += new System.EventHandler(this.btnGetRefundableBalance_Click);
            // 
            // btnGetRefundInfo
            // 
            this.btnGetRefundInfo.Location = new System.Drawing.Point(142, 134);
            this.btnGetRefundInfo.Name = "btnGetRefundInfo";
            this.btnGetRefundInfo.Size = new System.Drawing.Size(130, 32);
            this.btnGetRefundInfo.TabIndex = 0;
            this.btnGetRefundInfo.Text = "환불 신청 상태 조회";
            this.btnGetRefundInfo.Click += new System.EventHandler(this.btnGetRefundInfo_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnUpdateCorpInfo);
            this.GroupBox2.Controls.Add(this.btnGetContactInfo);
            this.GroupBox2.Controls.Add(this.btnGetCorpInfo);
            this.GroupBox2.Controls.Add(this.btnCheckID);
            this.GroupBox2.Controls.Add(this.btnUpdateContact);
            this.GroupBox2.Controls.Add(this.btnCheckIsMember);
            this.GroupBox2.Controls.Add(this.btnListContact);
            this.GroupBox2.Controls.Add(this.btnJoinMember);
            this.GroupBox2.Controls.Add(this.btnRegistContact);
            this.GroupBox2.Controls.Add(this.btnQuitMember);
            this.GroupBox2.Location = new System.Drawing.Point(11, 15);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(243, 196);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(123, 159);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(111, 29);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetContactInfo
            // 
            this.btnGetContactInfo.Location = new System.Drawing.Point(123, 54);
            this.btnGetContactInfo.Name = "btnGetContactInfo";
            this.btnGetContactInfo.Size = new System.Drawing.Size(111, 29);
            this.btnGetContactInfo.TabIndex = 3;
            this.btnGetContactInfo.Text = "담당자 정보 확인";
            this.btnGetContactInfo.UseVisualStyleBackColor = true;
            this.btnGetContactInfo.Click += new System.EventHandler(this.btnGetContactInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(6, 159);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(111, 29);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(6, 54);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(111, 29);
            this.btnCheckID.TabIndex = 3;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(123, 124);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(111, 29);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(6, 19);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(111, 29);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(123, 89);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(111, 29);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(6, 89);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(111, 29);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(123, 19);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(111, 29);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // btnQuitMember
            // 
            this.btnQuitMember.Location = new System.Drawing.Point(6, 124);
            this.btnQuitMember.Name = "btnQuitMember";
            this.btnQuitMember.Size = new System.Drawing.Size(111, 29);
            this.btnQuitMember.TabIndex = 23;
            this.btnQuitMember.Text = "팝빌 회원 탈퇴";
            this.btnQuitMember.Click += new System.EventHandler(this.btnQuitMember_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.btnGetAccessURL);
            this.GroupBox5.Location = new System.Drawing.Point(848, 24);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(131, 157);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
            // 
            // btnGetAccessURL
            // 
            this.btnGetAccessURL.Location = new System.Drawing.Point(6, 19);
            this.btnGetAccessURL.Name = "btnGetAccessURL";
            this.btnGetAccessURL.Size = new System.Drawing.Size(116, 30);
            this.btnGetAccessURL.TabIndex = 0;
            this.btnGetAccessURL.Text = "팝빌 로그인 URL";
            this.btnGetAccessURL.UseVisualStyleBackColor = true;
            this.btnGetAccessURL.Click += new System.EventHandler(this.btnGetAccessURL_Click);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(577, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "응답 URL :";
            // 
            // textURL
            // 
            this.textURL.Location = new System.Drawing.Point(647, 7);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(329, 21);
            this.textURL.TabIndex = 9;
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 766);
            this.Controls.Add(this.textURL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 홈택스 현금영수증 연동 API SDK Example";
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox11;
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
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnRegistContact;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Button btnGetChargeURL;
        internal System.Windows.Forms.Button btnGetAccessURL;
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
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btnGetPartnerURL_CHRG;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnCheckCertValidation;
        private System.Windows.Forms.Button btnGetCertificateExpireDate;
        private System.Windows.Forms.Button btnGetCertificatePopUpURL;
        private System.Windows.Forms.Button btnDeleteDeptUser;
        private System.Windows.Forms.Button btnCheckLoginDeptUser;
        private System.Windows.Forms.Button btnCheckDeptUser;
        private System.Windows.Forms.Button btnRegistDeptUser;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textURL;
        internal System.Windows.Forms.Button btnGetUseHistoryURL;
        internal System.Windows.Forms.Button btnGetPaymentURL;
        private System.Windows.Forms.Button btnGetContactInfo;
        private System.Windows.Forms.Button btnPaymentRequest;
        private System.Windows.Forms.Button btnGetSettleResult;
        private System.Windows.Forms.Button btnGetPaymentHistory;
        private System.Windows.Forms.Button btnGetUseHistory;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.Button btnGetRefundHistory;
        private System.Windows.Forms.Button btnGetRefundableBalance;
        private System.Windows.Forms.Button btnGetRefundInfo;
        private System.Windows.Forms.Button btnQuitMember;
    }
}
