namespace Popbill.Kakao.Example.csharp
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
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnGetPartnerURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetPopbillURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.getPopbillURL = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btn_unitcost_FMS = new System.Windows.Forms.Button();
            this.btnUnitCost_FTS = new System.Windows.Forms.Button();
            this.btnUnitCost_ATS = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.txtRequestNum = new System.Windows.Forms.TextBox();
            this.btnCancelReserveRN = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGetMessagesRN = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.txtReceiptNum = new System.Windows.Forms.TextBox();
            this.btnCacnelReserve = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetMessages = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetURL_BOX = new System.Windows.Forms.Button();
            this.btnListATSTemplate = new System.Windows.Forms.Button();
            this.btnGetURL_TEMPLATE = new System.Windows.Forms.Button();
            this.btnGetSenderNumberList = new System.Windows.Forms.Button();
            this.btnGetURL_SENDER = new System.Windows.Forms.Button();
            this.btnListPlusFriendID = new System.Windows.Forms.Button();
            this.btnGetURL_PLUSFRIEND = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnSendFMS_multi = new System.Windows.Forms.Button();
            this.btnSendFMS_same = new System.Windows.Forms.Button();
            this.btnSendFMS_one = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSendFTS_multi = new System.Windows.Forms.Button();
            this.btnSendFTS_same = new System.Windows.Forms.Button();
            this.btnSendFTS_one = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSendATS_multi = new System.Windows.Forms.Button();
            this.btnSendATS_same = new System.Windows.Forms.Button();
            this.btnSendATS_one = new System.Windows.Forms.Button();
            this.txtReserveDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GroupBox1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(403, 11);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(143, 21);
            this.txtUserId.TabIndex = 20;
            this.txtUserId.Text = "testkorea";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.groupBox14);
            this.GroupBox1.Controls.Add(this.groupBox11);
            this.GroupBox1.Controls.Add(this.groupBox10);
            this.GroupBox1.Controls.Add(this.groupBox13);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Location = new System.Drawing.Point(9, 43);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(1159, 163);
            this.GroupBox1.TabIndex = 21;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btnGetPartnerURL_CHRG);
            this.groupBox14.Controls.Add(this.btnGetPartnerBalance);
            this.groupBox14.Location = new System.Drawing.Point(575, 16);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(140, 137);
            this.groupBox14.TabIndex = 6;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "파트너과금 포인트";
            // 
            // btnGetPartnerURL_CHRG
            // 
            this.btnGetPartnerURL_CHRG.Location = new System.Drawing.Point(6, 59);
            this.btnGetPartnerURL_CHRG.Name = "btnGetPartnerURL_CHRG";
            this.btnGetPartnerURL_CHRG.Size = new System.Drawing.Size(125, 35);
            this.btnGetPartnerURL_CHRG.TabIndex = 4;
            this.btnGetPartnerURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPartnerURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPartnerURL_CHRG.Click += new System.EventHandler(this.btnGetPartnerURL_CHRG_Click);
            // 
            // btnGetPartnerBalance
            // 
            this.btnGetPartnerBalance.Location = new System.Drawing.Point(6, 22);
            this.btnGetPartnerBalance.Name = "btnGetPartnerBalance";
            this.btnGetPartnerBalance.Size = new System.Drawing.Size(124, 33);
            this.btnGetPartnerBalance.TabIndex = 3;
            this.btnGetPartnerBalance.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance.Click += new System.EventHandler(this.btnGetPartnerBalance_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox11.Controls.Add(this.btnGetCorpInfo);
            this.groupBox11.Location = new System.Drawing.Point(1017, 14);
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
            this.btnUpdateCorpInfo.UseCompatibleTextRendering = true;
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
            this.groupBox10.Location = new System.Drawing.Point(871, 16);
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
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnGetPopbillURL_CHRG);
            this.groupBox13.Controls.Add(this.btnGetBalance);
            this.groupBox13.Location = new System.Drawing.Point(424, 16);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(145, 137);
            this.groupBox13.TabIndex = 5;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "연동과금 포인트";
            // 
            // btnGetPopbillURL_CHRG
            // 
            this.btnGetPopbillURL_CHRG.Location = new System.Drawing.Point(6, 58);
            this.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG";
            this.btnGetPopbillURL_CHRG.Size = new System.Drawing.Size(123, 33);
            this.btnGetPopbillURL_CHRG.TabIndex = 1;
            this.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_CHRG.Click += new System.EventHandler(this.btnGetPopbillURL_CHRG_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(6, 21);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(122, 33);
            this.btnGetBalance.TabIndex = 2;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.getPopbillURL);
            this.GroupBox5.Location = new System.Drawing.Point(725, 16);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(138, 136);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
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
            this.GroupBox3.Controls.Add(this.btn_unitcost_FMS);
            this.GroupBox3.Controls.Add(this.btnUnitCost_FTS);
            this.GroupBox3.Controls.Add(this.btnUnitCost_ATS);
            this.GroupBox3.Location = new System.Drawing.Point(125, 16);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(293, 135);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(167, 20);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(113, 30);
            this.btnGetChargeInfo.TabIndex = 6;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // btn_unitcost_FMS
            // 
            this.btn_unitcost_FMS.Location = new System.Drawing.Point(11, 92);
            this.btn_unitcost_FMS.Name = "btn_unitcost_FMS";
            this.btn_unitcost_FMS.Size = new System.Drawing.Size(150, 32);
            this.btn_unitcost_FMS.TabIndex = 5;
            this.btn_unitcost_FMS.Text = "친구톡(이미지) 전송단가";
            this.btn_unitcost_FMS.UseVisualStyleBackColor = true;
            this.btn_unitcost_FMS.Click += new System.EventHandler(this.btn_unitcost_FMS_Click);
            // 
            // btnUnitCost_FTS
            // 
            this.btnUnitCost_FTS.Location = new System.Drawing.Point(11, 56);
            this.btnUnitCost_FTS.Name = "btnUnitCost_FTS";
            this.btnUnitCost_FTS.Size = new System.Drawing.Size(150, 32);
            this.btnUnitCost_FTS.TabIndex = 4;
            this.btnUnitCost_FTS.Text = "친구톡(텍스트) 전송단가";
            this.btnUnitCost_FTS.UseVisualStyleBackColor = true;
            this.btnUnitCost_FTS.Click += new System.EventHandler(this.btnUnitCost_FTS_Click);
            // 
            // btnUnitCost_ATS
            // 
            this.btnUnitCost_ATS.Location = new System.Drawing.Point(11, 18);
            this.btnUnitCost_ATS.Name = "btnUnitCost_ATS";
            this.btnUnitCost_ATS.Size = new System.Drawing.Size(150, 33);
            this.btnUnitCost_ATS.TabIndex = 3;
            this.btnUnitCost_ATS.Text = "알림톡 전송단가";
            this.btnUnitCost_ATS.UseVisualStyleBackColor = true;
            this.btnUnitCost_ATS.Click += new System.EventHandler(this.btnUnitCost_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnCheckID);
            this.GroupBox2.Controls.Add(this.btnCheckIsMember);
            this.GroupBox2.Controls.Add(this.btnJoinMember);
            this.GroupBox2.Location = new System.Drawing.Point(13, 16);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(106, 136);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(6, 54);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(93, 32);
            this.btnCheckID.TabIndex = 3;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(6, 19);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(93, 31);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(6, 90);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(93, 33);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(299, 16);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(101, 12);
            this.Label2.TabIndex = 19;
            this.Label2.Text = "팝빌회원 아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(136, 12);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(143, 21);
            this.txtCorpNum.TabIndex = 18;
            this.txtCorpNum.Text = "1234567890";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 12);
            this.Label1.TabIndex = 17;
            this.Label1.Text = "팝빌회원 사업자번호 : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox1);
            this.groupBox4.Controls.Add(this.groupBox15);
            this.groupBox4.Controls.Add(this.groupBox12);
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.txtReserveDT);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(9, 212);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1159, 515);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "카카오톡 관련 기능";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(13, 252);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1126, 244);
            this.listBox1.TabIndex = 27;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.txtRequestNum);
            this.groupBox15.Controls.Add(this.btnCancelReserveRN);
            this.groupBox15.Controls.Add(this.label5);
            this.groupBox15.Controls.Add(this.btnGetMessagesRN);
            this.groupBox15.Location = new System.Drawing.Point(328, 171);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(309, 78);
            this.groupBox15.TabIndex = 26;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "요청번호 할당 전송건 처리";
            // 
            // txtRequestNum
            // 
            this.txtRequestNum.Location = new System.Drawing.Point(153, 16);
            this.txtRequestNum.Name = "txtRequestNum";
            this.txtRequestNum.Size = new System.Drawing.Size(138, 21);
            this.txtRequestNum.TabIndex = 6;
            // 
            // btnCancelReserveRN
            // 
            this.btnCancelReserveRN.Location = new System.Drawing.Point(152, 39);
            this.btnCancelReserveRN.Name = "btnCancelReserveRN";
            this.btnCancelReserveRN.Size = new System.Drawing.Size(138, 31);
            this.btnCancelReserveRN.TabIndex = 8;
            this.btnCancelReserveRN.Text = "예약전송 취소";
            this.btnCancelReserveRN.UseVisualStyleBackColor = true;
            this.btnCancelReserveRN.Click += new System.EventHandler(this.btnCancelReserveRN_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "요청번호(requestNum) : ";
            // 
            // btnGetMessagesRN
            // 
            this.btnGetMessagesRN.Location = new System.Drawing.Point(12, 39);
            this.btnGetMessagesRN.Name = "btnGetMessagesRN";
            this.btnGetMessagesRN.Size = new System.Drawing.Size(134, 31);
            this.btnGetMessagesRN.TabIndex = 7;
            this.btnGetMessagesRN.Text = "전송상태 확인";
            this.btnGetMessagesRN.UseVisualStyleBackColor = true;
            this.btnGetMessagesRN.Click += new System.EventHandler(this.btnGetMessagesRN_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.txtReceiptNum);
            this.groupBox12.Controls.Add(this.btnCacnelReserve);
            this.groupBox12.Controls.Add(this.label4);
            this.groupBox12.Controls.Add(this.btnGetMessages);
            this.groupBox12.Location = new System.Drawing.Point(13, 171);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(309, 78);
            this.groupBox12.TabIndex = 25;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "접수번호 관련 기능 (요청번호 미할당)";
            // 
            // txtReceiptNum
            // 
            this.txtReceiptNum.Location = new System.Drawing.Point(153, 16);
            this.txtReceiptNum.Name = "txtReceiptNum";
            this.txtReceiptNum.Size = new System.Drawing.Size(138, 21);
            this.txtReceiptNum.TabIndex = 6;
            // 
            // btnCacnelReserve
            // 
            this.btnCacnelReserve.Location = new System.Drawing.Point(152, 39);
            this.btnCacnelReserve.Name = "btnCacnelReserve";
            this.btnCacnelReserve.Size = new System.Drawing.Size(138, 31);
            this.btnCacnelReserve.TabIndex = 8;
            this.btnCacnelReserve.Text = "예약전송 취소";
            this.btnCacnelReserve.UseVisualStyleBackColor = true;
            this.btnCacnelReserve.Click += new System.EventHandler(this.btnCacnelReserve_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "접수번호(receiptNum) : ";
            // 
            // btnGetMessages
            // 
            this.btnGetMessages.Location = new System.Drawing.Point(12, 39);
            this.btnGetMessages.Name = "btnGetMessages";
            this.btnGetMessages.Size = new System.Drawing.Size(134, 31);
            this.btnGetMessages.TabIndex = 7;
            this.btnGetMessages.Text = "전송상태 확인";
            this.btnGetMessages.UseVisualStyleBackColor = true;
            this.btnGetMessages.Click += new System.EventHandler(this.btnGetMessages_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSearch);
            this.groupBox9.Controls.Add(this.btnGetURL_BOX);
            this.groupBox9.Controls.Add(this.btnListATSTemplate);
            this.groupBox9.Controls.Add(this.btnGetURL_TEMPLATE);
            this.groupBox9.Controls.Add(this.btnGetSenderNumberList);
            this.groupBox9.Controls.Add(this.btnGetURL_SENDER);
            this.groupBox9.Controls.Add(this.btnListPlusFriendID);
            this.groupBox9.Controls.Add(this.btnGetURL_PLUSFRIEND);
            this.groupBox9.Location = new System.Drawing.Point(750, 20);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(397, 197);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "카카오톡 관리";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(205, 146);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(184, 36);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "전송내역 목록 조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGetURL_BOX
            // 
            this.btnGetURL_BOX.Location = new System.Drawing.Point(205, 104);
            this.btnGetURL_BOX.Name = "btnGetURL_BOX";
            this.btnGetURL_BOX.Size = new System.Drawing.Size(184, 36);
            this.btnGetURL_BOX.TabIndex = 9;
            this.btnGetURL_BOX.Text = "전송내역 조회 팝업 URL";
            this.btnGetURL_BOX.UseVisualStyleBackColor = true;
            this.btnGetURL_BOX.Click += new System.EventHandler(this.btnGetURL_BOX_Click);
            // 
            // btnListATSTemplate
            // 
            this.btnListATSTemplate.Location = new System.Drawing.Point(205, 62);
            this.btnListATSTemplate.Name = "btnListATSTemplate";
            this.btnListATSTemplate.Size = new System.Drawing.Size(184, 36);
            this.btnListATSTemplate.TabIndex = 8;
            this.btnListATSTemplate.Text = "알림톡 템플릿 목록 확인";
            this.btnListATSTemplate.UseVisualStyleBackColor = true;
            this.btnListATSTemplate.Click += new System.EventHandler(this.btnListATSTemplate_Click);
            // 
            // btnGetURL_TEMPLATE
            // 
            this.btnGetURL_TEMPLATE.Location = new System.Drawing.Point(205, 20);
            this.btnGetURL_TEMPLATE.Name = "btnGetURL_TEMPLATE";
            this.btnGetURL_TEMPLATE.Size = new System.Drawing.Size(184, 36);
            this.btnGetURL_TEMPLATE.TabIndex = 7;
            this.btnGetURL_TEMPLATE.Text = "알림톡 템플릿관리 팝업 URL";
            this.btnGetURL_TEMPLATE.UseVisualStyleBackColor = true;
            this.btnGetURL_TEMPLATE.Click += new System.EventHandler(this.btnGetURL_TEMPLATE_Click);
            // 
            // btnGetSenderNumberList
            // 
            this.btnGetSenderNumberList.Location = new System.Drawing.Point(16, 146);
            this.btnGetSenderNumberList.Name = "btnGetSenderNumberList";
            this.btnGetSenderNumberList.Size = new System.Drawing.Size(184, 36);
            this.btnGetSenderNumberList.TabIndex = 6;
            this.btnGetSenderNumberList.Text = "발신번호 목록 확인";
            this.btnGetSenderNumberList.UseVisualStyleBackColor = true;
            this.btnGetSenderNumberList.Click += new System.EventHandler(this.btnGetSenderNumberList_Click);
            // 
            // btnGetURL_SENDER
            // 
            this.btnGetURL_SENDER.Location = new System.Drawing.Point(16, 104);
            this.btnGetURL_SENDER.Name = "btnGetURL_SENDER";
            this.btnGetURL_SENDER.Size = new System.Drawing.Size(184, 36);
            this.btnGetURL_SENDER.TabIndex = 5;
            this.btnGetURL_SENDER.Text = "발신번호 관리 팝업 URL";
            this.btnGetURL_SENDER.UseVisualStyleBackColor = true;
            this.btnGetURL_SENDER.Click += new System.EventHandler(this.btnGetURL_SENDER_Click);
            // 
            // btnListPlusFriendID
            // 
            this.btnListPlusFriendID.Location = new System.Drawing.Point(16, 62);
            this.btnListPlusFriendID.Name = "btnListPlusFriendID";
            this.btnListPlusFriendID.Size = new System.Drawing.Size(184, 36);
            this.btnListPlusFriendID.TabIndex = 4;
            this.btnListPlusFriendID.Text = "플러스친구 목록 확인";
            this.btnListPlusFriendID.UseVisualStyleBackColor = true;
            this.btnListPlusFriendID.Click += new System.EventHandler(this.btnListPlusFriendID_Click);
            // 
            // btnGetURL_PLUSFRIEND
            // 
            this.btnGetURL_PLUSFRIEND.Location = new System.Drawing.Point(16, 20);
            this.btnGetURL_PLUSFRIEND.Name = "btnGetURL_PLUSFRIEND";
            this.btnGetURL_PLUSFRIEND.Size = new System.Drawing.Size(184, 36);
            this.btnGetURL_PLUSFRIEND.TabIndex = 3;
            this.btnGetURL_PLUSFRIEND.Text = "플러스친구 계정관리 팝업 URL";
            this.btnGetURL_PLUSFRIEND.UseVisualStyleBackColor = true;
            this.btnGetURL_PLUSFRIEND.Click += new System.EventHandler(this.btnGetURL_PLUSFRIEND_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnSendFMS_multi);
            this.groupBox8.Controls.Add(this.btnSendFMS_same);
            this.groupBox8.Controls.Add(this.btnSendFMS_one);
            this.groupBox8.Location = new System.Drawing.Point(13, 107);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(360, 55);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "친구톡(이미지) 전송";
            // 
            // btnSendFMS_multi
            // 
            this.btnSendFMS_multi.Location = new System.Drawing.Point(240, 16);
            this.btnSendFMS_multi.Name = "btnSendFMS_multi";
            this.btnSendFMS_multi.Size = new System.Drawing.Size(111, 30);
            this.btnSendFMS_multi.TabIndex = 2;
            this.btnSendFMS_multi.Text = "개별 1000건 전송";
            this.btnSendFMS_multi.UseVisualStyleBackColor = true;
            this.btnSendFMS_multi.Click += new System.EventHandler(this.btnSendFMS_multi_Click);
            // 
            // btnSendFMS_same
            // 
            this.btnSendFMS_same.Location = new System.Drawing.Point(123, 16);
            this.btnSendFMS_same.Name = "btnSendFMS_same";
            this.btnSendFMS_same.Size = new System.Drawing.Size(111, 30);
            this.btnSendFMS_same.TabIndex = 1;
            this.btnSendFMS_same.Text = "대량 1000건 전송";
            this.btnSendFMS_same.UseVisualStyleBackColor = true;
            this.btnSendFMS_same.Click += new System.EventHandler(this.btnSendFMS_same_Click);
            // 
            // btnSendFMS_one
            // 
            this.btnSendFMS_one.Location = new System.Drawing.Point(6, 16);
            this.btnSendFMS_one.Name = "btnSendFMS_one";
            this.btnSendFMS_one.Size = new System.Drawing.Size(111, 30);
            this.btnSendFMS_one.TabIndex = 0;
            this.btnSendFMS_one.Text = "1건 전송";
            this.btnSendFMS_one.UseVisualStyleBackColor = true;
            this.btnSendFMS_one.Click += new System.EventHandler(this.btnSendFMS_one_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnSendFTS_multi);
            this.groupBox7.Controls.Add(this.btnSendFTS_same);
            this.groupBox7.Controls.Add(this.btnSendFTS_one);
            this.groupBox7.Location = new System.Drawing.Point(379, 46);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(360, 55);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "친구톡(텍스트) 전송";
            // 
            // btnSendFTS_multi
            // 
            this.btnSendFTS_multi.Location = new System.Drawing.Point(240, 16);
            this.btnSendFTS_multi.Name = "btnSendFTS_multi";
            this.btnSendFTS_multi.Size = new System.Drawing.Size(111, 30);
            this.btnSendFTS_multi.TabIndex = 2;
            this.btnSendFTS_multi.Text = "개별 1000건 전송";
            this.btnSendFTS_multi.UseVisualStyleBackColor = true;
            this.btnSendFTS_multi.Click += new System.EventHandler(this.btnSendFTS_multi_Click);
            // 
            // btnSendFTS_same
            // 
            this.btnSendFTS_same.Location = new System.Drawing.Point(123, 16);
            this.btnSendFTS_same.Name = "btnSendFTS_same";
            this.btnSendFTS_same.Size = new System.Drawing.Size(111, 30);
            this.btnSendFTS_same.TabIndex = 1;
            this.btnSendFTS_same.Text = "대량 1000건 전송";
            this.btnSendFTS_same.UseVisualStyleBackColor = true;
            this.btnSendFTS_same.Click += new System.EventHandler(this.btnSendFTS_same_Click);
            // 
            // btnSendFTS_one
            // 
            this.btnSendFTS_one.Location = new System.Drawing.Point(6, 16);
            this.btnSendFTS_one.Name = "btnSendFTS_one";
            this.btnSendFTS_one.Size = new System.Drawing.Size(111, 30);
            this.btnSendFTS_one.TabIndex = 0;
            this.btnSendFTS_one.Text = "1건 전송";
            this.btnSendFTS_one.UseVisualStyleBackColor = true;
            this.btnSendFTS_one.Click += new System.EventHandler(this.btnSendFTS_one_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSendATS_multi);
            this.groupBox6.Controls.Add(this.btnSendATS_same);
            this.groupBox6.Controls.Add(this.btnSendATS_one);
            this.groupBox6.Location = new System.Drawing.Point(13, 46);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(360, 55);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "알림톡 전송";
            // 
            // btnSendATS_multi
            // 
            this.btnSendATS_multi.Location = new System.Drawing.Point(240, 16);
            this.btnSendATS_multi.Name = "btnSendATS_multi";
            this.btnSendATS_multi.Size = new System.Drawing.Size(111, 30);
            this.btnSendATS_multi.TabIndex = 2;
            this.btnSendATS_multi.Text = "개별 1000건 전송";
            this.btnSendATS_multi.UseVisualStyleBackColor = true;
            this.btnSendATS_multi.Click += new System.EventHandler(this.btnSendATS_multi_Click);
            // 
            // btnSendATS_same
            // 
            this.btnSendATS_same.Location = new System.Drawing.Point(123, 16);
            this.btnSendATS_same.Name = "btnSendATS_same";
            this.btnSendATS_same.Size = new System.Drawing.Size(111, 30);
            this.btnSendATS_same.TabIndex = 1;
            this.btnSendATS_same.Text = "대량 1000건 전송";
            this.btnSendATS_same.UseVisualStyleBackColor = true;
            this.btnSendATS_same.Click += new System.EventHandler(this.btnSendATS_same_Click);
            // 
            // btnSendATS_one
            // 
            this.btnSendATS_one.Location = new System.Drawing.Point(6, 16);
            this.btnSendATS_one.Name = "btnSendATS_one";
            this.btnSendATS_one.Size = new System.Drawing.Size(111, 30);
            this.btnSendATS_one.TabIndex = 0;
            this.btnSendATS_one.Text = "1건 전송";
            this.btnSendATS_one.UseVisualStyleBackColor = true;
            this.btnSendATS_one.Click += new System.EventHandler(this.btnSendATS_one_Click);
            // 
            // txtReserveDT
            // 
            this.txtReserveDT.Location = new System.Drawing.Point(224, 21);
            this.txtReserveDT.Name = "txtReserveDT";
            this.txtReserveDT.Size = new System.Drawing.Size(249, 21);
            this.txtReserveDT.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "예약전송시간(yyyyMMddHHmmss) :";
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "OpenFileDialog1";
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 740);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 카카오톡 API SDK C# Example";
            this.GroupBox1.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtUserId;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button btnGetPartnerURL_CHRG;
        internal System.Windows.Forms.Button btnGetPartnerBalance;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnRegistContact;
        private System.Windows.Forms.GroupBox groupBox13;
        internal System.Windows.Forms.Button btnGetPopbillURL_CHRG;
        internal System.Windows.Forms.Button btnGetBalance;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Button getPopbillURL;
        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.Button btnGetChargeInfo;
        internal System.Windows.Forms.Button btn_unitcost_FMS;
        internal System.Windows.Forms.Button btnUnitCost_FTS;
        internal System.Windows.Forms.Button btnUnitCost_ATS;
        internal System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.Button btnCheckID;
        internal System.Windows.Forms.Button btnCheckIsMember;
        internal System.Windows.Forms.Button btnJoinMember;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCorpNum;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReserveDT;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnSendFMS_multi;
        private System.Windows.Forms.Button btnSendFMS_same;
        private System.Windows.Forms.Button btnSendFMS_one;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnSendFTS_multi;
        private System.Windows.Forms.Button btnSendFTS_same;
        private System.Windows.Forms.Button btnSendFTS_one;
        private System.Windows.Forms.Button btnSendATS_multi;
        private System.Windows.Forms.Button btnSendATS_same;
        private System.Windows.Forms.Button btnSendATS_one;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnListPlusFriendID;
        private System.Windows.Forms.Button btnGetURL_PLUSFRIEND;
        private System.Windows.Forms.Button btnGetSenderNumberList;
        private System.Windows.Forms.Button btnGetURL_SENDER;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetURL_BOX;
        private System.Windows.Forms.Button btnListATSTemplate;
        private System.Windows.Forms.Button btnGetURL_TEMPLATE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReceiptNum;
        private System.Windows.Forms.Button btnGetMessages;
        private System.Windows.Forms.Button btnCacnelReserve;
        internal System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.TextBox txtRequestNum;
        private System.Windows.Forms.Button btnCancelReserveRN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGetMessagesRN;
        private System.Windows.Forms.ListBox listBox1;
    }
}

