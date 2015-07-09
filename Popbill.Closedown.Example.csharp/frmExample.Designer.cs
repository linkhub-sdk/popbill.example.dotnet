namespace Popbill.Closedown.Example.csharp
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
            this.btnCheckCorpNum = new System.Windows.Forms.Button();
            this.btnCheckCorpNums = new System.Windows.Forms.Button();
            this.btnCheckIsMember = new System.Windows.Forms.Button();
            this.btnJoinMember = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCorpNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.btnGetBalance = new System.Windows.Forms.Button();
            this.btnUnitCost = new System.Windows.Forms.Button();
            this.btnGetPopbillURL_LOGIN = new System.Windows.Forms.Button();
            this.btnGetPopbillURL_CHRG = new System.Windows.Forms.Button();
            this.btnGetPartnerBalance = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCheckCorpNum = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheckCorpNum
            // 
            this.btnCheckCorpNum.Location = new System.Drawing.Point(245, 172);
            this.btnCheckCorpNum.Name = "btnCheckCorpNum";
            this.btnCheckCorpNum.Size = new System.Drawing.Size(77, 27);
            this.btnCheckCorpNum.TabIndex = 18;
            this.btnCheckCorpNum.Text = "단건조회";
            this.btnCheckCorpNum.UseVisualStyleBackColor = true;
            this.btnCheckCorpNum.Click += new System.EventHandler(this.btnCheckCorpNum_Click);
            // 
            // btnCheckCorpNums
            // 
            this.btnCheckCorpNums.Location = new System.Drawing.Point(335, 172);
            this.btnCheckCorpNums.Name = "btnCheckCorpNums";
            this.btnCheckCorpNums.Size = new System.Drawing.Size(77, 27);
            this.btnCheckCorpNums.TabIndex = 19;
            this.btnCheckCorpNums.Text = "대량조회";
            this.btnCheckCorpNums.UseVisualStyleBackColor = true;
            this.btnCheckCorpNums.Click += new System.EventHandler(this.btnCheckCorpNums_Click);
            // 
            // btnCheckIsMember
            // 
            this.btnCheckIsMember.Location = new System.Drawing.Point(39, 72);
            this.btnCheckIsMember.Name = "btnCheckIsMember";
            this.btnCheckIsMember.Size = new System.Drawing.Size(104, 27);
            this.btnCheckIsMember.TabIndex = 0;
            this.btnCheckIsMember.Text = "가입여부 확인";
            this.btnCheckIsMember.UseVisualStyleBackColor = true;
            this.btnCheckIsMember.Click += new System.EventHandler(this.btnCheckIsMember_Click);
            // 
            // btnJoinMember
            // 
            this.btnJoinMember.Location = new System.Drawing.Point(39, 105);
            this.btnJoinMember.Name = "btnJoinMember";
            this.btnJoinMember.Size = new System.Drawing.Size(104, 27);
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
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "팝빌 사업자번호 :";
            // 
            // txtCorpNum
            // 
            this.txtCorpNum.Location = new System.Drawing.Point(129, 9);
            this.txtCorpNum.Name = "txtCorpNum";
            this.txtCorpNum.Size = new System.Drawing.Size(115, 21);
            this.txtCorpNum.TabIndex = 3;
            this.txtCorpNum.Text = "1234567890";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "팝빌 아이디 : ";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(363, 9);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(118, 21);
            this.txtUserID.TabIndex = 5;
            this.txtUserID.Text = "testkorea";
            // 
            // btnGetBalance
            // 
            this.btnGetBalance.Location = new System.Drawing.Point(163, 70);
            this.btnGetBalance.Name = "btnGetBalance";
            this.btnGetBalance.Size = new System.Drawing.Size(104, 27);
            this.btnGetBalance.TabIndex = 6;
            this.btnGetBalance.Text = "잔여포인트 확인";
            this.btnGetBalance.UseVisualStyleBackColor = true;
            this.btnGetBalance.Click += new System.EventHandler(this.btnGetBalance_Click);
            // 
            // btnUnitCost
            // 
            this.btnUnitCost.Location = new System.Drawing.Point(163, 103);
            this.btnUnitCost.Name = "btnUnitCost";
            this.btnUnitCost.Size = new System.Drawing.Size(104, 27);
            this.btnUnitCost.TabIndex = 7;
            this.btnUnitCost.Text = "조회단가 확인";
            this.btnUnitCost.UseVisualStyleBackColor = true;
            this.btnUnitCost.Click += new System.EventHandler(this.btnUnitCost_Click);
            // 
            // btnGetPopbillURL_LOGIN
            // 
            this.btnGetPopbillURL_LOGIN.Location = new System.Drawing.Point(288, 71);
            this.btnGetPopbillURL_LOGIN.Name = "btnGetPopbillURL_LOGIN";
            this.btnGetPopbillURL_LOGIN.Size = new System.Drawing.Size(104, 27);
            this.btnGetPopbillURL_LOGIN.TabIndex = 8;
            this.btnGetPopbillURL_LOGIN.Text = "팝빌 로그인 URL";
            this.btnGetPopbillURL_LOGIN.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_LOGIN.Click += new System.EventHandler(this.btnGetPopbillURL_LOGIN_Click);
            // 
            // btnGetPopbillURL_CHRG
            // 
            this.btnGetPopbillURL_CHRG.Location = new System.Drawing.Point(288, 104);
            this.btnGetPopbillURL_CHRG.Name = "btnGetPopbillURL_CHRG";
            this.btnGetPopbillURL_CHRG.Size = new System.Drawing.Size(104, 27);
            this.btnGetPopbillURL_CHRG.TabIndex = 9;
            this.btnGetPopbillURL_CHRG.Text = "포인트 충전 URL";
            this.btnGetPopbillURL_CHRG.UseVisualStyleBackColor = true;
            this.btnGetPopbillURL_CHRG.Click += new System.EventHandler(this.btnGetPopbillURL_CHRG_Click);
            // 
            // btnGetPartnerBalance
            // 
            this.btnGetPartnerBalance.Location = new System.Drawing.Point(7, 19);
            this.btnGetPartnerBalance.Name = "btnGetPartnerBalance";
            this.btnGetPartnerBalance.Size = new System.Drawing.Size(119, 27);
            this.btnGetPartnerBalance.TabIndex = 10;
            this.btnGetPartnerBalance.Text = "파트너포인트 확인";
            this.btnGetPartnerBalance.UseVisualStyleBackColor = true;
            this.btnGetPartnerBalance.Click += new System.EventHandler(this.btnGetPartnerBalance_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(31, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 86);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "회원정보";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(156, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 85);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "포인트 관련";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(282, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 87);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "URL 관련";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnGetPartnerBalance);
            this.groupBox4.Location = new System.Drawing.Point(408, 55);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(133, 85);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "파트너 관련";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "조회 사업자번호 : ";
            // 
            // txtCheckCorpNum
            // 
            this.txtCheckCorpNum.Location = new System.Drawing.Point(138, 177);
            this.txtCheckCorpNum.Name = "txtCheckCorpNum";
            this.txtCheckCorpNum.Size = new System.Drawing.Size(100, 21);
            this.txtCheckCorpNum.TabIndex = 17;
            this.txtCheckCorpNum.Text = "4108600477";
            this.txtCheckCorpNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckCorpNum_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(19, 37);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(536, 112);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "팝빌 기본 API";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(16, 157);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(539, 56);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "휴폐업조회 API";
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 232);
            this.Controls.Add(this.btnCheckCorpNums);
            this.Controls.Add(this.btnCheckCorpNum);
            this.Controls.Add(this.txtCheckCorpNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGetPopbillURL_CHRG);
            this.Controls.Add(this.btnGetPopbillURL_LOGIN);
            this.Controls.Add(this.btnUnitCost);
            this.Controls.Add(this.btnGetBalance);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCorpNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnJoinMember);
            this.Controls.Add(this.btnCheckIsMember);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Name = "frmExample";
            this.Text = "팝빌 휴폐업조회 API SDK";
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnGetPopbillURL_LOGIN;
        private System.Windows.Forms.Button btnGetPopbillURL_CHRG;
        private System.Windows.Forms.Button btnGetPartnerBalance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCheckCorpNum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnCheckCorpNum;
        private System.Windows.Forms.Button btnCheckCorpNums;
    }
}

