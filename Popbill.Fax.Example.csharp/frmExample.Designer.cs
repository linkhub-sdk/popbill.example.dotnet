namespace Popbill.Fax.Example.csharp
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
            this.btnGetPartnerURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCorpInfo = new System.Windows.Forms.Button();
            this.btnGetCorpInfo = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnListContact = new System.Windows.Forms.Button();
            this.btnRegistContact = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.getPopbillURL_LOGIN = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnGetPopbillURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChargeInfo = new System.Windows.Forms.Button();
            this.btnUnitCost = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckID = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnResendFAXRN_same = new System.Windows.Forms.Button();
            this.btnResendFAXRN = new System.Windows.Forms.Button();
            this.btnCancelReserveRN = new System.Windows.Forms.Button();
            this.btnGetFaxResultRN = new System.Windows.Forms.Button();
            this.txtRequestNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnResendFAXSame = new System.Windows.Forms.Button();
            this.txtReceiptNum = new System.Windows.Forms.TextBox();
            this.btnResendFAX = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelReserve = new System.Windows.Forms.Button();
            this.btnGetFaxResult = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnGetURL_SENDER = new System.Windows.Forms.Button();
            this.btnGetSenderNumberList = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGetURL = new System.Windows.Forms.Button();
            this.txtReserveDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.GroupBox1.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(416, 13);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(143, 21);
            this.txtUserId.TabIndex = 15;
            this.txtUserId.Text = "testkorea";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.groupBox13);
            this.GroupBox1.Controls.Add(this.groupBox7);
            this.GroupBox1.Controls.Add(this.groupBox6);
            this.GroupBox1.Controls.Add(this.GroupBox5);
            this.GroupBox1.Controls.Add(this.groupBox12);
            this.GroupBox1.Controls.Add(this.GroupBox3);
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Location = new System.Drawing.Point(9, 41);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(992, 146);
            this.GroupBox1.TabIndex = 16;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "팝빌 기본 API";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnGetPartnerURL_CHRG);
            this.groupBox13.Controls.Add(this.btnGetPartnerBalance);
            this.groupBox13.Location = new System.Drawing.Point(429, 16);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(133, 112);
            this.groupBox13.TabIndex = 19;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "파트너과금 포인트";
            // 
            // btnGetPartnerURL_CHRG
            // 
            this.btnGetPartnerURL_CHRG.Location = new System.Drawing.Point(8, 50);
            this.btnGetPartnerURL_CHRG.Name = "btnGetPartnerURL_CHRG";
            this.btnGetPartnerURL_CHRG.Size = new System.Drawing.Size(119, 32);
            this.btnGetPartnerURL_CHRG.TabIndex = 0;
            this.btnGetPartnerURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPartnerURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPartnerURL_CHRG.Click += new System.EventHandler(this.btnGetPartnerURL_CHRG_Click);
            // 
            // btnGetPartnerBalance
            // 
            this.btnGetPartnerBalance.Location = new System.Drawing.Point(8, 19);
            this.btnGetPartnerBalance.Name = "btnGetPartnerBalance";
            this.btnGetPartnerBalance.Size = new System.Drawing.Size(118, 27);
            this.btnGetPartnerBalance.TabIndex = 3;
            this.btnGetPartnerBalance.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance.Click += new System.EventHandler(this.btnGetPartnerBalance_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnUpdateCorpInfo);
            this.groupBox7.Controls.Add(this.btnGetCorpInfo);
            this.groupBox7.Location = new System.Drawing.Point(849, 15);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(131, 113);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "회사정보 관련";
            // 
            // btnUpdateCorpInfo
            // 
            this.btnUpdateCorpInfo.Location = new System.Drawing.Point(9, 50);
            this.btnUpdateCorpInfo.Name = "btnUpdateCorpInfo";
            this.btnUpdateCorpInfo.Size = new System.Drawing.Size(113, 27);
            this.btnUpdateCorpInfo.TabIndex = 1;
            this.btnUpdateCorpInfo.Text = "회사정보 수정";
            this.btnUpdateCorpInfo.UseVisualStyleBackColor = true;
            this.btnUpdateCorpInfo.Click += new System.EventHandler(this.btnUpdateCorpInfo_Click);
            // 
            // btnGetCorpInfo
            // 
            this.btnGetCorpInfo.Location = new System.Drawing.Point(9, 21);
            this.btnGetCorpInfo.Name = "btnGetCorpInfo";
            this.btnGetCorpInfo.Size = new System.Drawing.Size(113, 27);
            this.btnGetCorpInfo.TabIndex = 0;
            this.btnGetCorpInfo.Text = "회사정보 조회";
            this.btnGetCorpInfo.UseVisualStyleBackColor = true;
            this.btnGetCorpInfo.Click += new System.EventHandler(this.btnGetCorpInfo_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnUpdateContact);
            this.groupBox6.Controls.Add(this.btnListContact);
            this.groupBox6.Controls.Add(this.btnRegistContact);
            this.groupBox6.Location = new System.Drawing.Point(709, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(129, 112);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "담당자 관련";
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(7, 78);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(114, 27);
            this.btnUpdateContact.TabIndex = 2;
            this.btnUpdateContact.Text = "담당자 정보 수정";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnListContact
            // 
            this.btnListContact.Location = new System.Drawing.Point(7, 48);
            this.btnListContact.Name = "btnListContact";
            this.btnListContact.Size = new System.Drawing.Size(114, 27);
            this.btnListContact.TabIndex = 1;
            this.btnListContact.Text = "담당자 목록 조회";
            this.btnListContact.UseVisualStyleBackColor = true;
            this.btnListContact.Click += new System.EventHandler(this.btnListContact_Click);
            // 
            // btnRegistContact
            // 
            this.btnRegistContact.Location = new System.Drawing.Point(7, 19);
            this.btnRegistContact.Name = "btnRegistContact";
            this.btnRegistContact.Size = new System.Drawing.Size(114, 26);
            this.btnRegistContact.TabIndex = 0;
            this.btnRegistContact.Text = "담당자 추가";
            this.btnRegistContact.UseVisualStyleBackColor = true;
            this.btnRegistContact.Click += new System.EventHandler(this.btnRegistContact_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.getPopbillURL_LOGIN);
            this.GroupBox5.Location = new System.Drawing.Point(569, 17);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(131, 111);
            this.GroupBox5.TabIndex = 2;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "팝빌 기본 URL";
            // 
            // getPopbillURL_LOGIN
            // 
            this.getPopbillURL_LOGIN.Location = new System.Drawing.Point(6, 19);
            this.getPopbillURL_LOGIN.Name = "getPopbillURL_LOGIN";
            this.getPopbillURL_LOGIN.Size = new System.Drawing.Size(118, 27);
            this.getPopbillURL_LOGIN.TabIndex = 0;
            this.getPopbillURL_LOGIN.Text = "팝빌 로그인 URL";
            this.getPopbillURL_LOGIN.UseVisualStyleBackColor = true;
            this.getPopbillURL_LOGIN.Click += new System.EventHandler(this.getPopbillURL_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnGetPopbillURL_CHRG);
            this.groupBox12.Controls.Add(this.btnGetBalance);
            this.groupBox12.Location = new System.Drawing.Point(291, 17);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(132, 111);
            this.groupBox12.TabIndex = 18;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "연동과금 포인트";
            // 
            // btnGetPopbillURL_CHRG
            // 
            this.btnGetPopbillURL_CHRG.Location = new System.Drawing.Point(6, 50);
            this.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG";
            this.btnGetPopbillURL_CHRG.Size = new System.Drawing.Size(118, 31);
            this.btnGetPopbillURL_CHRG.TabIndex = 1;
            this.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_CHRG.Click += new System.EventHandler(this.btnGetPopbillURL_CHRG_Click);
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(6, 20);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(118, 27);
            this.btnGetBalance.TabIndex = 2;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnGetChargeInfo);
            this.GroupBox3.Controls.Add(this.btnUnitCost);
            this.GroupBox3.Location = new System.Drawing.Point(147, 17);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(138, 111);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "포인트 관련";
            // 
            // btnGetChargeInfo
            // 
            this.btnGetChargeInfo.Location = new System.Drawing.Point(9, 53);
            this.btnGetChargeInfo.Name = "btnGetChargeInfo";
            this.btnGetChargeInfo.Size = new System.Drawing.Size(118, 29);
            this.btnGetChargeInfo.TabIndex = 4;
            this.btnGetChargeInfo.Text = "과금정보 확인";
            this.btnGetChargeInfo.UseVisualStyleBackColor = true;
            this.btnGetChargeInfo.Click += new System.EventHandler(this.btnGetChargeInfo_Click);
            // 
            // btnUnitCost
            // 
            this.btnUnitCost.Location = new System.Drawing.Point(9, 20);
            this.btnUnitCost.Name = "btnUnitCost";
            this.btnUnitCost.Size = new System.Drawing.Size(118, 28);
            this.btnUnitCost.TabIndex = 3;
            this.btnUnitCost.Text = "전송 단가 확인";
            this.btnUnitCost.UseVisualStyleBackColor = true;
            this.btnUnitCost.Click += new System.EventHandler(this.btnUnitCost_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.btnCheckID);
            this.GroupBox2.Controls.Add(this.btnCheckIsMember);
            this.GroupBox2.Controls.Add(this.btnJoinMember);
            this.GroupBox2.Location = new System.Drawing.Point(10, 17);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(129, 111);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "회원 정보";
            // 
            // btnCheckID
            // 
            this.btnCheckID.Location = new System.Drawing.Point(9, 49);
            this.btnCheckID.Name = "btnCheckID";
            this.btnCheckID.Size = new System.Drawing.Size(111, 26);
            this.btnCheckID.TabIndex = 3;
            this.btnCheckID.Text = "ID 중복 확인";
            this.btnCheckID.UseVisualStyleBackColor = true;
            this.btnCheckID.Click += new System.EventHandler(this.btnCheckID_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(9, 19);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(111, 27);
            this.btnCheckIsMember.TabIndex = 2;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(9, 78);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(111, 26);
            this.btnJoinMember.TabIndex = 1;
            this.btnJoinMember.Text = "회원 가입";
            this.btnJoinMember.UseVisualStyleBackColor = true;
            this.btnJoinMember.Click += new System.EventHandler(this.btnJoinMember_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(311, 18);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(101, 12);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "팝빌회원 아이디 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(144, 14);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(143, 21);
            this.txtCorpNum.TabIndex = 13;
            this.txtCorpNum.Text = "1234567890";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(16, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 12);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "팝빌회원 사업자번호 : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox1);
            this.groupBox4.Controls.Add(this.groupBox10);
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.btnGetURL);
            this.groupBox4.Controls.Add(this.txtReserveDT);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(9, 197);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(992, 464);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "팩스전송 관련 기능";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnResendFAXRN_same);
            this.groupBox10.Controls.Add(this.btnResendFAXRN);
            this.groupBox10.Controls.Add(this.btnCancelReserveRN);
            this.groupBox10.Controls.Add(this.btnGetFaxResultRN);
            this.groupBox10.Controls.Add(this.txtRequestNum);
            this.groupBox10.Controls.Add(this.label5);
            this.groupBox10.Location = new System.Drawing.Point(297, 109);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(273, 112);
            this.groupBox10.TabIndex = 33;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "요청번호 할당 전송건 처리";
            // 
            // btnResendFAXRN_same
            // 
            this.btnResendFAXRN_same.Location = new System.Drawing.Point(140, 74);
            this.btnResendFAXRN_same.Name = "btnResendFAXRN_same";
            this.btnResendFAXRN_same.Size = new System.Drawing.Size(120, 31);
            this.btnResendFAXRN_same.TabIndex = 31;
            this.btnResendFAXRN_same.Text = "동보 재전송";
            this.btnResendFAXRN_same.UseVisualStyleBackColor = true;
            this.btnResendFAXRN_same.Click += new System.EventHandler(this.btnResendFAXRN_same_Click);
            // 
            // btnResendFAXRN
            // 
            this.btnResendFAXRN.Location = new System.Drawing.Point(12, 74);
            this.btnResendFAXRN.Name = "btnResendFAXRN";
            this.btnResendFAXRN.Size = new System.Drawing.Size(119, 31);
            this.btnResendFAXRN.TabIndex = 30;
            this.btnResendFAXRN.Text = "재전송";
            this.btnResendFAXRN.UseVisualStyleBackColor = true;
            this.btnResendFAXRN.Click += new System.EventHandler(this.btnResendFAXRN_Click);
            // 
            // btnCancelReserveRN
            // 
            this.btnCancelReserveRN.Location = new System.Drawing.Point(140, 43);
            this.btnCancelReserveRN.Name = "btnCancelReserveRN";
            this.btnCancelReserveRN.Size = new System.Drawing.Size(119, 31);
            this.btnCancelReserveRN.TabIndex = 3;
            this.btnCancelReserveRN.Text = "예약 전송 취소";
            this.btnCancelReserveRN.UseVisualStyleBackColor = true;
            this.btnCancelReserveRN.Click += new System.EventHandler(this.btnCancelReserveRN_Click);
            // 
            // btnGetFaxResultRN
            // 
            this.btnGetFaxResultRN.Location = new System.Drawing.Point(12, 43);
            this.btnGetFaxResultRN.Name = "btnGetFaxResultRN";
            this.btnGetFaxResultRN.Size = new System.Drawing.Size(119, 30);
            this.btnGetFaxResultRN.TabIndex = 2;
            this.btnGetFaxResultRN.Text = "전송상태확인";
            this.btnGetFaxResultRN.UseVisualStyleBackColor = true;
            this.btnGetFaxResultRN.Click += new System.EventHandler(this.btnGetFaxResultRN_Click);
            // 
            // txtRequestNum
            // 
            this.txtRequestNum.Location = new System.Drawing.Point(77, 21);
            this.txtRequestNum.Name = "txtRequestNum";
            this.txtRequestNum.Size = new System.Drawing.Size(182, 21);
            this.txtRequestNum.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "요청번호 :";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnResendFAXSame);
            this.groupBox9.Controls.Add(this.txtReceiptNum);
            this.groupBox9.Controls.Add(this.btnResendFAX);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.btnCancelReserve);
            this.groupBox9.Controls.Add(this.btnGetFaxResult);
            this.groupBox9.Location = new System.Drawing.Point(17, 109);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(273, 112);
            this.groupBox9.TabIndex = 32;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "접수번호 관련 기능 (요청번호 미할당)";
            // 
            // btnResendFAXSame
            // 
            this.btnResendFAXSame.Location = new System.Drawing.Point(136, 75);
            this.btnResendFAXSame.Name = "btnResendFAXSame";
            this.btnResendFAXSame.Size = new System.Drawing.Size(120, 31);
            this.btnResendFAXSame.TabIndex = 30;
            this.btnResendFAXSame.Text = "동보 재전송";
            this.btnResendFAXSame.UseVisualStyleBackColor = true;
            this.btnResendFAXSame.Click += new System.EventHandler(this.btnResendFAXSame_Click);
            // 
            // txtReceiptNum
            // 
            this.txtReceiptNum.Location = new System.Drawing.Point(71, 19);
            this.txtReceiptNum.Name = "txtReceiptNum";
            this.txtReceiptNum.Size = new System.Drawing.Size(186, 21);
            this.txtReceiptNum.TabIndex = 17;
            // 
            // btnResendFAX
            // 
            this.btnResendFAX.Location = new System.Drawing.Point(11, 75);
            this.btnResendFAX.Name = "btnResendFAX";
            this.btnResendFAX.Size = new System.Drawing.Size(119, 31);
            this.btnResendFAX.TabIndex = 29;
            this.btnResendFAX.Text = "재전송";
            this.btnResendFAX.UseVisualStyleBackColor = true;
            this.btnResendFAX.Click += new System.EventHandler(this.btnResendFAX_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "접수번호 : ";
            // 
            // btnCancelReserve
            // 
            this.btnCancelReserve.Location = new System.Drawing.Point(136, 43);
            this.btnCancelReserve.Name = "btnCancelReserve";
            this.btnCancelReserve.Size = new System.Drawing.Size(120, 30);
            this.btnCancelReserve.TabIndex = 22;
            this.btnCancelReserve.Text = "예약 전송 취소";
            this.btnCancelReserve.UseVisualStyleBackColor = true;
            this.btnCancelReserve.Click += new System.EventHandler(this.btnCancelReserve_Click);
            // 
            // btnGetFaxResult
            // 
            this.btnGetFaxResult.Location = new System.Drawing.Point(10, 43);
            this.btnGetFaxResult.Name = "btnGetFaxResult";
            this.btnGetFaxResult.Size = new System.Drawing.Size(120, 30);
            this.btnGetFaxResult.TabIndex = 21;
            this.btnGetFaxResult.Text = "전송상태확인";
            this.btnGetFaxResult.UseVisualStyleBackColor = true;
            this.btnGetFaxResult.Click += new System.EventHandler(this.btnGetFaxResult_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnGetURL_SENDER);
            this.groupBox8.Controls.Add(this.btnGetSenderNumberList);
            this.groupBox8.Location = new System.Drawing.Point(693, 13);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(135, 101);
            this.groupBox8.TabIndex = 31;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "발신번호 관리";
            // 
            // btnGetURL_SENDER
            // 
            this.btnGetURL_SENDER.Location = new System.Drawing.Point(9, 58);
            this.btnGetURL_SENDER.Name = "btnGetURL_SENDER";
            this.btnGetURL_SENDER.Size = new System.Drawing.Size(117, 32);
            this.btnGetURL_SENDER.TabIndex = 22;
            this.btnGetURL_SENDER.Text = "발신번호 관리 팝업";
            this.btnGetURL_SENDER.UseVisualStyleBackColor = true;
            this.btnGetURL_SENDER.Click += new System.EventHandler(this.btnGetURL_SENDER_Click);
            // 
            // btnGetSenderNumberList
            // 
            this.btnGetSenderNumberList.Location = new System.Drawing.Point(9, 20);
            this.btnGetSenderNumberList.Name = "btnGetSenderNumberList";
            this.btnGetSenderNumberList.Size = new System.Drawing.Size(117, 32);
            this.btnGetSenderNumberList.TabIndex = 21;
            this.btnGetSenderNumberList.Text = "발신번호 목록 조회";
            this.btnGetSenderNumberList.UseVisualStyleBackColor = true;
            this.btnGetSenderNumberList.Click += new System.EventHandler(this.btnGetSenderNumberList_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(564, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(117, 32);
            this.btnSearch.TabIndex = 28;
            this.btnSearch.Text = "전송내역 기간조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(309, 54);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 31);
            this.button4.TabIndex = 27;
            this.button4.Text = "다수파일 동보전송";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(205, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 31);
            this.button3.TabIndex = 26;
            this.button3.Text = "다수 파일 전송";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(111, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 31);
            this.button2.TabIndex = 25;
            this.button2.Text = "동보 전송";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 31);
            this.button1.TabIndex = 24;
            this.button1.Text = "전송";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGetURL
            // 
            this.btnGetURL.Location = new System.Drawing.Point(443, 20);
            this.btnGetURL.Name = "btnGetURL";
            this.btnGetURL.Size = new System.Drawing.Size(117, 32);
            this.btnGetURL.TabIndex = 20;
            this.btnGetURL.Text = "전송내역조회 팝업";
            this.btnGetURL.UseVisualStyleBackColor = true;
            this.btnGetURL.Click += new System.EventHandler(this.btnGetURL_Click);
            // 
            // txtReserveDT
            // 
            this.txtReserveDT.Location = new System.Drawing.Point(205, 24);
            this.txtReserveDT.Name = "txtReserveDT";
            this.txtReserveDT.Size = new System.Drawing.Size(168, 21);
            this.txtReserveDT.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "예약시간(yyyyMMddHHmmss) : ";
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "OpenFileDialog1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(17, 225);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(954, 220);
            this.listBox1.TabIndex = 34;
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 673);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.Label1);
            this.Name = "frmExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팝빌 팩스 SDK C# Example";
            this.GroupBox1.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtUserId;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button btnGetPartnerBalance;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Button getPopbillURL_LOGIN;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button btnUnitCost;
        internal System.Windows.Forms.Button btnGetBalance;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button btnCheckIsMember;
        internal System.Windows.Forms.Button btnJoinMember;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCorpNum;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.TextBox txtReserveDT;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtReceiptNum;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetURL;
        private System.Windows.Forms.Button btnCancelReserve;
        private System.Windows.Forms.Button btnGetFaxResult;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnRegistContact;
        private System.Windows.Forms.Button btnListContact;
        private System.Windows.Forms.Button btnUpdateContact;
        internal System.Windows.Forms.Button btnCheckID;
        internal System.Windows.Forms.Button btnGetPopbillURL_CHRG;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnGetCorpInfo;
        private System.Windows.Forms.Button btnUpdateCorpInfo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetChargeInfo;
        private System.Windows.Forms.Button btnResendFAX;
        private System.Windows.Forms.Button btnResendFAXSame;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnGetSenderNumberList;
        private System.Windows.Forms.Button btnGetURL_SENDER;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btnGetPartnerURL_CHRG;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRequestNum;
        private System.Windows.Forms.Button btnGetFaxResultRN;
        private System.Windows.Forms.Button btnCancelReserveRN;
        private System.Windows.Forms.Button btnResendFAXRN_same;
        private System.Windows.Forms.Button btnResendFAXRN;
        private System.Windows.Forms.ListBox listBox1;
    }
}

