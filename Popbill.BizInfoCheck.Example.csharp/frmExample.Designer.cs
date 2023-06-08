namespace Popbill.BizInfoCheck.Example.csharp
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
            this.btnCheckBizInfo = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.btnUnitCost = new System.Windows.Forms.Button();
            this.btnGetAccessURL = new System.Windows.Forms.Button();
            this.btnGetChargeURL = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnQuitMember = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btnPaymentRequest = new System.Windows.Forms.Button();
            this.btnGetSettleResult = new System.Windows.Forms.Button();
            this.btnGetPaymentHistory = new System.Windows.Forms.Button();
            this.btnGetUseHistory = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnGetRefundHistory = new System.Windows.Forms.Button();
            this.btnGetRefundInfo = new System.Windows.Forms.Button();
            this.btnGetRefundableBalance = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnGetContactInfo = new System.Windows.Forms.Button();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCheckCorpNum = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnGetUseHistoryURL = new System.Windows.Forms.Button();
            this.btnGetPaymentURL = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetPartnerURL_CHRG = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textURL = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheckBizInfo
            // 
            this.btnCheckBizInfo.Location = new System.Drawing.Point(228, 17);
            this.btnCheckBizInfo.Name = "btnCheckBizInfo";
            this.btnCheckBizInfo.Size = new System.Drawing.Size(77, 31);
            this.btnCheckBizInfo.TabIndex = 18;
            this.btnCheckBizInfo.Text = "단건조회";
            this.btnCheckBizInfo.UseVisualStyleBackColor = true;
            this.btnCheckBizInfo.Click += new System.EventHandler(this.btnCheckBizInfo_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(39, 75);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(104, 32);
            this.btnCheckIsMember.TabIndex = 0;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(39, 148);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(104, 32);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "팝빌회원 사업자번호 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(148, 9);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(115, 21);
            this.txtCorpNum.TabIndex = 3;
            this.txtCorpNum.Text = "1234567890";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "팝빌회원 아이디 : ";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(388, 9);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(118, 21);
            this.txtUserID.TabIndex = 5;
            this.txtUserID.Text = "testkorea";
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(6, 20);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(136, 32);
            this.btnGetBalance.TabIndex = 6;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // btnUnitCost
            // 
            this.btnUnitCost.Location = new System.Drawing.Point(6, 20);
            this.btnUnitCost.Name = "btnUnitCost";
            this.btnUnitCost.Size = new System.Drawing.Size(119, 32);
            this.btnUnitCost.TabIndex = 7;
            this.btnUnitCost.Text = "조회단가 확인";
            this.btnUnitCost.UseVisualStyleBackColor = true;
            this.btnUnitCost.Click += new System.EventHandler(this.btnUnitCost_Click);
            // 
            // btnGetAccessURL
            // 
            this.btnGetAccessURL.Location = new System.Drawing.Point(6, 20);
            this.btnGetAccessURL.Name = "btnGetAccessURL";
            this.btnGetAccessURL.Size = new System.Drawing.Size(118, 32);
            this.btnGetAccessURL.TabIndex = 8;
            this.btnGetAccessURL.Text = "팝빌 로그인 URL";
            this.btnGetAccessURL.UseVisualStyleBackColor = true;
            this.btnGetAccessURL.Click += new System.EventHandler(this.btnGetAccessURL_Click);
            // 
            // btnGetChargeURL
            // 
            this.btnGetChargeURL.Location = new System.Drawing.Point(6, 56);
            this.btnGetChargeURL.Name = "btnGetChargeURL";
            this.btnGetChargeURL.Size = new System.Drawing.Size(136, 32);
            this.btnGetChargeURL.TabIndex = 9;
            this.btnGetChargeURL.Text = "포인트 충전 URL";
            this.btnGetChargeURL.UseVisualStyleBackColor = true;
            this.btnGetChargeURL.Click += new System.EventHandler(this.btnGetChargeURL_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckID);
            this.groupBox1.Controls.Add(this.btnQuitMember);
            this.groupBox1.Location = new System.Drawing.Point(31, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 176);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "회원정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(8, 56);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(104, 32);
            this.btnCheckID.TabIndex = 22;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            //
            // btnQuitMember
            //
            this.btnQuitMember.Location = new System.Drawing.Point(8, 132);
            this.btnQuitMember.Name = "btnQuitMember";
            this.btnQuitMember.Size = new System.Drawing.Size(104, 32);
            this.btnQuitMember.TabIndex = 23;
            this.btnQuitMember.Text = "팝빌 회원 탈퇴";
            this.btnQuitMember.Click += new System.EventHandler(this.btnQuitMember_Click);
            //
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetChargeInfo);
            this.groupBox2.Controls.Add(this.btnUnitCost);
            this.groupBox2.Controls.Add(this.btnPaymentRequest);
            this.groupBox2.Controls.Add(this.btnGetSettleResult);
            this.groupBox2.Controls.Add(this.btnGetPaymentHistory);
            this.groupBox2.Controls.Add(this.btnGetUseHistory);
            this.groupBox2.Controls.Add(this.btnRefund);
            this.groupBox2.Controls.Add(this.btnGetRefundHistory);
            this.groupBox2.Controls.Add(this.btnGetRefundInfo);
            this.groupBox2.Controls.Add(this.btnGetRefundableBalance);
            this.groupBox2.Location = new System.Drawing.Point(156, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 386);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(6, 56);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(119, 32);
            this.btnGetChargeInfo.TabIndex = 1;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            //
            // btnPaymentRequest
            //
            this.btnPaymentRequest.Location = new System.Drawing.Point(6, 92);
            this.btnPaymentRequest.Name = "btnPaymentRequest";
            this.btnPaymentRequest.Size = new System.Drawing.Size(119, 32);
            this.btnPaymentRequest.TabIndex = 8;
            this.btnPaymentRequest.Text = "연동회원 무통장 입금신청";
            this.btnPaymentRequest.Click += new System.EventHandler(this.btnPaymentRequest_Click);
            //
            // btnGetSettleResult
            //
            this.btnGetSettleResult.Location = new System.Drawing.Point(6, 128);
            this.btnGetSettleResult.Name = "btnGetSettleResult";
            this.btnGetSettleResult.Size = new System.Drawing.Size(119, 32);
            this.btnGetSettleResult.TabIndex = 0;
            this.btnGetSettleResult.Text = "무통장 입금신청 정보확인";
            this.btnGetSettleResult.Click += new System.EventHandler(this.btnGetSettleResult_Click);
            //
            // btnGetPaymentHistory
            //
            this.btnGetPaymentHistory.Location = new System.Drawing.Point(6, 166);
            this.btnGetPaymentHistory.Name = "btnGetPaymentHistory";
            this.btnGetPaymentHistory.Size = new System.Drawing.Size(119, 32);
            this.btnGetPaymentHistory.TabIndex = 0;
            this.btnGetPaymentHistory.Text = "연동회원 포인트 결제내역 확인";
            this.btnGetPaymentHistory.Click += new System.EventHandler(this.btnGetPaymentHistory_Click);
            //
            // btnGetUseHistory
            //
            this.btnGetUseHistory.Location = new System.Drawing.Point(6, 202);
            this.btnGetUseHistory.Name = "btnGetUseHistory";
            this.btnGetUseHistory.Size = new System.Drawing.Size(119, 32);
            this.btnGetUseHistory.TabIndex = 0;
            this.btnGetUseHistory.Text = "연동회원 포인트 사용내역 확인";
            this.btnGetUseHistory.Click += new System.EventHandler(this.btnGetUseHistory_Click);
            //
            // btnRefund
            //
            this.btnRefund.Location = new System.Drawing.Point(6, 238);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(119, 32);
            this.btnRefund.TabIndex = 0;
            this.btnRefund.Text = "연동회원 포인트 환불신청";
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            //
            // btnGetRefundHistory
            //
            this.btnGetRefundHistory.Location = new System.Drawing.Point(6, 274);
            this.btnGetRefundHistory.Name = "btnGetRefundHistory";
            this.btnGetRefundHistory.Size = new System.Drawing.Size(119, 32);
            this.btnGetRefundHistory.TabIndex = 0;
            this.btnGetRefundHistory.Text = "연동회원 포인트 환불내역 확인";
            this.btnGetRefundHistory.Click += new System.EventHandler(this.btnGetRefundHistory_Click);
            //
            // btnGetRefundInfo
            //
            this.btnGetRefundInfo.Location = new System.Drawing.Point(6, 310);
            this.btnGetRefundInfo.Name = "btnGetRefundInfo";
            this.btnGetRefundInfo.Size = new System.Drawing.Size(119, 32);
            this.btnGetRefundInfo.TabIndex = 0;
            this.btnGetRefundInfo.Text = "환불 신청 상태 조회";
            this.btnGetRefundInfo.Click += new System.EventHandler(this.btnGetRefundInfo_Click);
            //
            // btnGetRefundableBalance
            //
            this.btnGetRefundableBalance.Location = new System.Drawing.Point(6, 346);
            this.btnGetRefundableBalance.Name = "btnGetRefundableBalance";
            this.btnGetRefundableBalance.Size = new System.Drawing.Size(119, 32);
            this.btnGetRefundableBalance.TabIndex = 0;
            this.btnGetRefundableBalance.Text = "환불 가능 포인트 조회";
            this.btnGetRefundableBalance.Click += new System.EventHandler(this.btnGetRefundableBalance_Click);
            //
            // btnGetPartnerBalance
            // 
            this.btnGetPartnerBalance.Location = new System.Drawing.Point(9, 20);
            this.btnGetPartnerBalance.Name = "btnGetPartnerBalance";
            this.btnGetPartnerBalance.Size = new System.Drawing.Size(118, 32);
            this.btnGetPartnerBalance.TabIndex = 0;
            this.btnGetPartnerBalance.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance.Click += new System.EventHandler(this.btnGetPartnerBalance_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGetAccessURL);
            this.groupBox3.Location = new System.Drawing.Point(575, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(130, 174);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "팝빌 기본 URL";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnGetContactInfo);
            this.groupBox4.Controls.Add(this.btnUpdateContact);
            this.groupBox4.Controls.Add(this.btnListContact);
            this.groupBox4.Controls.Add(this.btnRegistContact);
            this.groupBox4.Location = new System.Drawing.Point(711, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(133, 174);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "담당자 관련";
            // 
            // btnGetContactInfo
            // 
            this.btnGetContactInfo.Location = new System.Drawing.Point(8, 55);
            this.btnGetContactInfo.Name = "btnGetContactInfo";
            this.btnGetContactInfo.Size = new System.Drawing.Size(117, 32);
            this.btnGetContactInfo.TabIndex = 3;
            this.btnGetContactInfo.Text = "담당자 정보 확인";
            this.btnGetContactInfo.UseVisualStyleBackColor = true;
            this.btnGetContactInfo.Click += new System.EventHandler(this.btnGetContactInfo_Click);
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(8, 126);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(117, 32);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(8, 91);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(117, 32);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(8, 20);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(117, 32);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "조회 사업자번호 : ";
            // 
            // txtCheckCorpNum
            // 
            this.txtCheckCorpNum.Location = new System.Drawing.Point(119, 23);
            this.txtCheckCorpNum.Name = "txtCheckCorpNum";
            this.txtCheckCorpNum.Size = new System.Drawing.Size(100, 21);
            this.txtCheckCorpNum.TabIndex = 17;
            this.txtCheckCorpNum.Text = "6798700433";
            this.txtCheckCorpNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckCorpNum_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox12);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox13);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Location = new System.Drawing.Point(12, 36);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(998, 413);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "팝빌 기본 API";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnGetUseHistoryURL);
            this.groupBox12.Controls.Add(this.btnGetPaymentURL);
            this.groupBox12.Controls.Add(this.btnGetBalance);
            this.groupBox12.Controls.Add(this.btnGetChargeURL);
            this.groupBox12.Location = new System.Drawing.Point(281, 19);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(149, 176);
            this.groupBox12.TabIndex = 22;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "연동과금 포인트";
            // 
            // btnGetUseHistoryURL
            // 
            this.btnGetUseHistoryURL.Location = new System.Drawing.Point(7, 128);
            this.btnGetUseHistoryURL.Name = "btnGetUseHistoryURL";
            this.btnGetUseHistoryURL.Size = new System.Drawing.Size(136, 32);
            this.btnGetUseHistoryURL.TabIndex = 11;
            this.btnGetUseHistoryURL.Text = "포인트 사용내역 URL";
            this.btnGetUseHistoryURL.UseVisualStyleBackColor = true;
            this.btnGetUseHistoryURL.Click += new System.EventHandler(this.btnGetUseHistoryURL_Click);
            // 
            // btnGetPaymentURL
            // 
            this.btnGetPaymentURL.Location = new System.Drawing.Point(7, 92);
            this.btnGetPaymentURL.Name = "btnGetPaymentURL";
            this.btnGetPaymentURL.Size = new System.Drawing.Size(136, 32);
            this.btnGetPaymentURL.TabIndex = 10;
            this.btnGetPaymentURL.Text = "포인트 결제내역 URL";
            this.btnGetPaymentURL.UseVisualStyleBackColor = true;
            this.btnGetPaymentURL.Click += new System.EventHandler(this.btnGetPaymentURL_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox7.Controls.Add(this.btnGetCorpInfo);
            this.groupBox7.Location = new System.Drawing.Point(851, 21);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(130, 174);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "회사정보 관련";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(10, 55);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(111, 32);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(10, 20);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(111, 32);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnGetPartnerURL_CHRG);
            this.groupBox13.Controls.Add(this.btnGetPartnerBalance);
            this.groupBox13.Location = new System.Drawing.Point(436, 21);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(133, 174);
            this.groupBox13.TabIndex = 23;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "파트너과금 포인트";
            // 
            // btnGetPartnerURL_CHRG
            // 
            this.btnGetPartnerURL_CHRG.Location = new System.Drawing.Point(8, 55);
            this.btnGetPartnerURL_CHRG.Name = "btnGetPartnerURL_CHRG";
            this.btnGetPartnerURL_CHRG.Size = new System.Drawing.Size(119, 32);
            this.btnGetPartnerURL_CHRG.TabIndex = 0;
            this.btnGetPartnerURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPartnerURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPartnerURL_CHRG.Click += new System.EventHandler(this.btnGetPartnerURL_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnCheckBizInfo);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.txtCheckCorpNum);
            this.groupBox6.Location = new System.Drawing.Point(12, 455);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(410, 59);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "기업정보조회 API";
            this.groupBox6.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(573, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "응답 URL :";
            // 
            // textURL
            // 
            this.textURL.Location = new System.Drawing.Point(643, 9);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(333, 21);
            this.textURL.TabIndex = 23;
            //
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1022, 521);
            this.Controls.Add(this.textURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnJoinMember);
            this.Controls.Add(this.btnCheckIsMember);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 기업정보조회 API SDK";
            this.Load += new System.EventHandler(this.frmExample_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnCheckIsMember;
        private System.Windows.Forms.Button btnJoinMember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCorpNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Button btnGetBalance;
        private System.Windows.Forms.Button btnUnitCost;
        private System.Windows.Forms.Button btnGetAccessURL;
        private System.Windows.Forms.Button btnGetChargeURL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCheckCorpNum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnCheckBizInfo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        private System.Windows.Forms.Button btnGetPartnerBalance;
        private System.Windows.Forms.Button btnCheckID;
        private System.Windows.Forms.Button btnRegistContact;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnGetChargeInfo;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btnGetPartnerURL_CHRG;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textURL;
        private System.Windows.Forms.Button btnGetUseHistoryURL;
        private System.Windows.Forms.Button btnGetPaymentURL;
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

