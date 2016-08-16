namespace Popbill.Message.Example.csharp
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
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.btnGetPopbillURL_CHRG = new System.Windows.Forms.Button();
            this.getPopbillURL = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btn_unitcost_mms = new System.Windows.Forms.Button();
            this.btnUnitCost_LMS = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance = new System.Windows.Forms.Button();
            this.btnUnitCost = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnGetAutoDenyList = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSendMMS_Same = new System.Windows.Forms.Button();
            this.btnSendMMS = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCancelReserve = new System.Windows.Forms.Button();
            this.btnGetMessageResult = new System.Windows.Forms.Button();
            this.btnGetURL = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnSendXMS_same = new System.Windows.Forms.Button();
            this.btnSendXMS_hund = new System.Windows.Forms.Button();
            this.btnSendXMS_one = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSendLMS_same = new System.Windows.Forms.Button();
            this.btnSendLMS_hund = new System.Windows.Forms.Button();
            this.btnSendLMS_one = new System.Windows.Forms.Button();
            this.txtReceiptNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSendSMS_Same = new System.Windows.Forms.Button();
            this.btn_SendSMS_hund = new System.Windows.Forms.Button();
            this.btnSendSMS_one = new System.Windows.Forms.Button();
            this.txtReserveDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(405, 18);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(143, 21);
            this.txtUserId.TabIndex = 15;
            this.txtUserId.Text = "testkorea";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.groupBox11);
            this.GroupBox1.Controls.Add(this.groupBox10);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Location = new System.Drawing.Point(11, 50);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(860, 163);
            this.GroupBox1.TabIndex = 16;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox11.Controls.Add(this.btnGetCorpInfo);
            this.groupBox11.Location = new System.Drawing.Point(716, 14);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(130, 138);
            this.groupBox11.TabIndex = 4;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "회사정보 관련";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(7, 60);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(115, 31);
            this.btnUpdateCorpInfo.TabIndex = 2;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(7, 23);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(115, 31);
            this.btnGetCorpInfo.TabIndex = 1;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnUpdateContact);
            this.groupBox10.Controls.Add(this.btnListContact);
            this.groupBox10.Controls.Add(this.btnRegistContact);
            this.groupBox10.Location = new System.Drawing.Point(570, 16);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(136, 136);
            this.groupBox10.TabIndex = 3;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "담당자 관련";
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(7, 95);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(120, 31);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(7, 58);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(120, 31);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(7, 21);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(120, 31);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.btnGetPopbillURL_CHRG);
            this.GroupBox5.Controls.Add(this.getPopbillURL);
            this.GroupBox5.Location = new System.Drawing.Point(424, 16);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(138, 136);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
            // 
            // btnGetPopbillURL_CHRG
            // 
            this.btnGetPopbillURL_CHRG.Location = new System.Drawing.Point(7, 56);
            this.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG";
            this.btnGetPopbillURL_CHRG.Size = new System.Drawing.Size(123, 33);
            this.btnGetPopbillURL_CHRG.TabIndex = 1;
            this.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_CHRG.Click += new System.EventHandler(this.btnGetPopbillURL_CHRG_Click);
            // 
            // getPopbillURL
            // 
            this.getPopbillURL.Location = new System.Drawing.Point(7, 18);
            this.getPopbillURL.Name = "getPopbillURL";
            this.getPopbillURL.Size = new System.Drawing.Size(123, 32);
            this.getPopbillURL.TabIndex = 0;
            this.getPopbillURL.Text = "팝빌 로그인 URL";
            this.getPopbillURL.UseVisualStyleBackColor = true;
            this.getPopbillURL.Click += new System.EventHandler(this.getPopbillURL_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnGetChargeInfo);
            this.GroupBox3.Controls.Add(this.btn_unitcost_mms);
            this.GroupBox3.Controls.Add(this.btnUnitCost_LMS);
            this.GroupBox3.Controls.Add(this.btnGetPartnerBalance);
            this.GroupBox3.Controls.Add(this.btnUnitCost);
            this.GroupBox3.Controls.Add(this.btnGetBalance);
            this.GroupBox3.Location = new System.Drawing.Point(152, 16);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(266, 135);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(142, 94);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(113, 30);
            this.btnGetChargeInfo.TabIndex = 6;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // btn_unitcost_mms
            // 
            this.btn_unitcost_mms.Location = new System.Drawing.Point(11, 92);
            this.btn_unitcost_mms.Name = "btn_unitcost_mms";
            this.btn_unitcost_mms.Size = new System.Drawing.Size(121, 32);
            this.btn_unitcost_mms.TabIndex = 5;
            this.btn_unitcost_mms.Text = "포토 전송단가 확인";
            this.btn_unitcost_mms.UseVisualStyleBackColor = true;
            this.btn_unitcost_mms.Click += new System.EventHandler(this.btn_unitcost_mms_Click);
            // 
            // btnUnitCost_LMS
            // 
            this.btnUnitCost_LMS.Location = new System.Drawing.Point(11, 56);
            this.btnUnitCost_LMS.Name = "btnUnitCost_LMS";
            this.btnUnitCost_LMS.Size = new System.Drawing.Size(121, 32);
            this.btnUnitCost_LMS.TabIndex = 4;
            this.btnUnitCost_LMS.Text = "장문 전송단가 확인";
            this.btnUnitCost_LMS.UseVisualStyleBackColor = true;
            this.btnUnitCost_LMS.Click += new System.EventHandler(this.btnUnitCost_LMS_Click);
            // 
            // btnGetPartnerBalance
            // 
            this.btnGetPartnerBalance.Location = new System.Drawing.Point(141, 55);
            this.btnGetPartnerBalance.Name = "btnGetPartnerBalance";
            this.btnGetPartnerBalance.Size = new System.Drawing.Size(115, 33);
            this.btnGetPartnerBalance.TabIndex = 3;
            this.btnGetPartnerBalance.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance.Click += new System.EventHandler(this.btnGetPartnerBalance_Click);
            // 
            // btnUnitCost
            // 
            this.btnUnitCost.Location = new System.Drawing.Point(11, 18);
            this.btnUnitCost.Name = "btnUnitCost";
            this.btnUnitCost.Size = new System.Drawing.Size(121, 33);
            this.btnUnitCost.TabIndex = 3;
            this.btnUnitCost.Text = "단문 전송단가 확인";
            this.btnUnitCost.UseVisualStyleBackColor = true;
            this.btnUnitCost.Click += new System.EventHandler(this.btnUnitCost_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(141, 17);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(115, 33);
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
            this.GroupBox2.Location = new System.Drawing.Point(13, 16);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(133, 136);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(6, 54);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(118, 32);
            this.btnCheckID.TabIndex = 3;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(6, 19);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(118, 31);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(6, 90);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(118, 33);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(301, 23);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(101, 12);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "팝빌회원 아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(138, 19);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(143, 21);
            this.txtCorpNum.TabIndex = 13;
            this.txtCorpNum.Text = "1234567890";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 12);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "팝빌회원 사업자번호 : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnGetAutoDenyList);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Controls.Add(this.btnCancelReserve);
            this.groupBox4.Controls.Add(this.btnGetMessageResult);
            this.groupBox4.Controls.Add(this.btnGetURL);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.txtReceiptNum);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.txtReserveDT);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(8, 219);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(863, 439);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "문자전송 관련 기능";
            // 
            // btnGetAutoDenyList
            // 
            this.btnGetAutoDenyList.Location = new System.Drawing.Point(728, 19);
            this.btnGetAutoDenyList.Name = "btnGetAutoDenyList";
            this.btnGetAutoDenyList.Size = new System.Drawing.Size(121, 32);
            this.btnGetAutoDenyList.TabIndex = 26;
            this.btnGetAutoDenyList.Text = "080 수신거부목록";
            this.btnGetAutoDenyList.UseVisualStyleBackColor = true;
            this.btnGetAutoDenyList.Click += new System.EventHandler(this.btnGetAutoDenyList_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(466, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(130, 31);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "전송내역 기간조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSendMMS_Same);
            this.groupBox9.Controls.Add(this.btnSendMMS);
            this.groupBox9.Location = new System.Drawing.Point(561, 71);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(121, 55);
            this.groupBox9.TabIndex = 24;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "MMS 문자 전송";
            // 
            // btnSendMMS_Same
            // 
            this.btnSendMMS_Same.Location = new System.Drawing.Point(62, 20);
            this.btnSendMMS_Same.Name = "btnSendMMS_Same";
            this.btnSendMMS_Same.Size = new System.Drawing.Size(47, 27);
            this.btnSendMMS_Same.TabIndex = 2;
            this.btnSendMMS_Same.Text = "동보";
            this.btnSendMMS_Same.UseVisualStyleBackColor = true;
            this.btnSendMMS_Same.Click += new System.EventHandler(this.btnSendMMS_Same_Click);
            // 
            // btnSendMMS
            // 
            this.btnSendMMS.Location = new System.Drawing.Point(9, 20);
            this.btnSendMMS.Name = "btnSendMMS";
            this.btnSendMMS.Size = new System.Drawing.Size(47, 27);
            this.btnSendMMS.TabIndex = 0;
            this.btnSendMMS.Text = "1건";
            this.btnSendMMS.UseVisualStyleBackColor = true;
            this.btnSendMMS.Click += new System.EventHandler(this.btnSendMMS_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 175);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(744, 249);
            this.dataGridView1.TabIndex = 23;
            // 
            // btnCancelReserve
            // 
            this.btnCancelReserve.Location = new System.Drawing.Point(367, 139);
            this.btnCancelReserve.Name = "btnCancelReserve";
            this.btnCancelReserve.Size = new System.Drawing.Size(121, 32);
            this.btnCancelReserve.TabIndex = 22;
            this.btnCancelReserve.Text = "예약 전송 취소";
            this.btnCancelReserve.UseVisualStyleBackColor = true;
            this.btnCancelReserve.Click += new System.EventHandler(this.btnCancelReserve_Click);
            // 
            // btnGetMessageResult
            // 
            this.btnGetMessageResult.Location = new System.Drawing.Point(240, 139);
            this.btnGetMessageResult.Name = "btnGetMessageResult";
            this.btnGetMessageResult.Size = new System.Drawing.Size(121, 32);
            this.btnGetMessageResult.TabIndex = 21;
            this.btnGetMessageResult.Text = "전송상태확인";
            this.btnGetMessageResult.UseVisualStyleBackColor = true;
            this.btnGetMessageResult.Click += new System.EventHandler(this.btnGetMessageResult_Click);
            // 
            // btnGetURL
            // 
            this.btnGetURL.Location = new System.Drawing.Point(601, 19);
            this.btnGetURL.Name = "btnGetURL";
            this.btnGetURL.Size = new System.Drawing.Size(121, 32);
            this.btnGetURL.TabIndex = 20;
            this.btnGetURL.Text = "전송내역조회 팝업";
            this.btnGetURL.UseVisualStyleBackColor = true;
            this.btnGetURL.Click += new System.EventHandler(this.btnGetURL_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnSendXMS_same);
            this.groupBox8.Controls.Add(this.btnSendXMS_hund);
            this.groupBox8.Controls.Add(this.btnSendXMS_one);
            this.groupBox8.Location = new System.Drawing.Point(383, 71);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(172, 55);
            this.groupBox8.TabIndex = 19;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "XMS 문자 전송";
            // 
            // btnSendXMS_same
            // 
            this.btnSendXMS_same.Location = new System.Drawing.Point(115, 20);
            this.btnSendXMS_same.Name = "btnSendXMS_same";
            this.btnSendXMS_same.Size = new System.Drawing.Size(47, 27);
            this.btnSendXMS_same.TabIndex = 2;
            this.btnSendXMS_same.Text = "동보";
            this.btnSendXMS_same.UseVisualStyleBackColor = true;
            this.btnSendXMS_same.Click += new System.EventHandler(this.btnSendXMS_same_Click);
            // 
            // btnSendXMS_hund
            // 
            this.btnSendXMS_hund.Location = new System.Drawing.Point(62, 20);
            this.btnSendXMS_hund.Name = "btnSendXMS_hund";
            this.btnSendXMS_hund.Size = new System.Drawing.Size(47, 27);
            this.btnSendXMS_hund.TabIndex = 1;
            this.btnSendXMS_hund.Text = "100건";
            this.btnSendXMS_hund.UseVisualStyleBackColor = true;
            this.btnSendXMS_hund.Click += new System.EventHandler(this.btnSendXMS_hund_Click);
            // 
            // btnSendXMS_one
            // 
            this.btnSendXMS_one.Location = new System.Drawing.Point(9, 20);
            this.btnSendXMS_one.Name = "btnSendXMS_one";
            this.btnSendXMS_one.Size = new System.Drawing.Size(47, 27);
            this.btnSendXMS_one.TabIndex = 0;
            this.btnSendXMS_one.Text = "1건";
            this.btnSendXMS_one.UseVisualStyleBackColor = true;
            this.btnSendXMS_one.Click += new System.EventHandler(this.btnSendXMS_one_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnSendLMS_same);
            this.groupBox7.Controls.Add(this.btnSendLMS_hund);
            this.groupBox7.Controls.Add(this.btnSendLMS_one);
            this.groupBox7.Location = new System.Drawing.Point(204, 71);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(172, 55);
            this.groupBox7.TabIndex = 18;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "LMS 문자 전송";
            // 
            // btnSendLMS_same
            // 
            this.btnSendLMS_same.Location = new System.Drawing.Point(115, 20);
            this.btnSendLMS_same.Name = "btnSendLMS_same";
            this.btnSendLMS_same.Size = new System.Drawing.Size(47, 27);
            this.btnSendLMS_same.TabIndex = 2;
            this.btnSendLMS_same.Text = "동보";
            this.btnSendLMS_same.UseVisualStyleBackColor = true;
            this.btnSendLMS_same.Click += new System.EventHandler(this.btnSendLMS_same_Click);
            // 
            // btnSendLMS_hund
            // 
            this.btnSendLMS_hund.Location = new System.Drawing.Point(62, 20);
            this.btnSendLMS_hund.Name = "btnSendLMS_hund";
            this.btnSendLMS_hund.Size = new System.Drawing.Size(47, 27);
            this.btnSendLMS_hund.TabIndex = 1;
            this.btnSendLMS_hund.Text = "100건";
            this.btnSendLMS_hund.UseVisualStyleBackColor = true;
            this.btnSendLMS_hund.Click += new System.EventHandler(this.btnSendLMS_hund_Click);
            // 
            // btnSendLMS_one
            // 
            this.btnSendLMS_one.Location = new System.Drawing.Point(9, 20);
            this.btnSendLMS_one.Name = "btnSendLMS_one";
            this.btnSendLMS_one.Size = new System.Drawing.Size(47, 27);
            this.btnSendLMS_one.TabIndex = 0;
            this.btnSendLMS_one.Text = "1건";
            this.btnSendLMS_one.UseVisualStyleBackColor = true;
            this.btnSendLMS_one.Click += new System.EventHandler(this.btnSendLMS_one_Click);
            // 
            // txtReceiptNum
            // 
            this.txtReceiptNum.Location = new System.Drawing.Point(86, 145);
            this.txtReceiptNum.Name = "txtReceiptNum";
            this.txtReceiptNum.Size = new System.Drawing.Size(143, 21);
            this.txtReceiptNum.TabIndex = 17;
            this.txtReceiptNum.Text = "014102315000000005";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "접수번호 : ";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSendSMS_Same);
            this.groupBox6.Controls.Add(this.btn_SendSMS_hund);
            this.groupBox6.Controls.Add(this.btnSendSMS_one);
            this.groupBox6.Location = new System.Drawing.Point(23, 71);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(172, 55);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "SMS 문자 전송";
            // 
            // btnSendSMS_Same
            // 
            this.btnSendSMS_Same.Location = new System.Drawing.Point(115, 20);
            this.btnSendSMS_Same.Name = "btnSendSMS_Same";
            this.btnSendSMS_Same.Size = new System.Drawing.Size(47, 27);
            this.btnSendSMS_Same.TabIndex = 2;
            this.btnSendSMS_Same.Text = "동보";
            this.btnSendSMS_Same.UseVisualStyleBackColor = true;
            this.btnSendSMS_Same.Click += new System.EventHandler(this.btnSendSMS_Same_Click);
            // 
            // btn_SendSMS_hund
            // 
            this.btn_SendSMS_hund.Location = new System.Drawing.Point(62, 20);
            this.btn_SendSMS_hund.Name = "btn_SendSMS_hund";
            this.btn_SendSMS_hund.Size = new System.Drawing.Size(47, 27);
            this.btn_SendSMS_hund.TabIndex = 1;
            this.btn_SendSMS_hund.Text = "100건";
            this.btn_SendSMS_hund.UseVisualStyleBackColor = true;
            this.btn_SendSMS_hund.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSendSMS_one
            // 
            this.btnSendSMS_one.Location = new System.Drawing.Point(9, 20);
            this.btnSendSMS_one.Name = "btnSendSMS_one";
            this.btnSendSMS_one.Size = new System.Drawing.Size(47, 27);
            this.btnSendSMS_one.TabIndex = 0;
            this.btnSendSMS_one.Text = "1건";
            this.btnSendSMS_one.UseVisualStyleBackColor = true;
            this.btnSendSMS_one.Click += new System.EventHandler(this.btnSendSMS_one_Click);
            // 
            // txtReserveDT
            // 
            this.txtReserveDT.Location = new System.Drawing.Point(205, 31);
            this.txtReserveDT.Name = "txtReserveDT";
            this.txtReserveDT.Size = new System.Drawing.Size(186, 21);
            this.txtReserveDT.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "예약시간(yyyyMMddHHmmss) : ";
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 687);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 문자메시지  SDK C# Example";
            this.GroupBox1.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtUserId;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button btnGetPartnerBalance;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Button getPopbillURL;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button btnUnitCost;
        internal System.Windows.Forms.Button btnGetBalance;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button btnCheckIsMember;
        internal System.Windows.Forms.Button btnJoinMember;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCorpNum;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnUnitCost_LMS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSendSMS_one;
        internal System.Windows.Forms.TextBox txtReserveDT;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtReceiptNum;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_SendSMS_hund;
        private System.Windows.Forms.Button btnSendSMS_Same;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnSendLMS_same;
        private System.Windows.Forms.Button btnSendLMS_hund;
        private System.Windows.Forms.Button btnSendLMS_one;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnSendXMS_same;
        private System.Windows.Forms.Button btnSendXMS_hund;
        private System.Windows.Forms.Button btnSendXMS_one;
        private System.Windows.Forms.Button btnGetURL;
        private System.Windows.Forms.Button btnCancelReserve;
        private System.Windows.Forms.Button btnGetMessageResult;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnSendMMS_Same;
        private System.Windows.Forms.Button btnSendMMS;
        internal System.Windows.Forms.Button btn_unitcost_mms;
        private System.Windows.Forms.Button btnCheckID;
        internal System.Windows.Forms.Button btnGetPopbillURL_CHRG;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnRegistContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAutoDenyList;
        private System.Windows.Forms.Button btnGetChargeInfo;
    }
}

