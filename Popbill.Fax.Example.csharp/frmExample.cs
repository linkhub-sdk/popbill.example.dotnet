using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.Fax.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";
        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private FaxService faxService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            //초기화
            faxService = new FaxService(LinkID, SecretKey);
            //테스트를 완료한후 아래 변수를 false로 변경하거나, 아래줄을 삭제하여 실제 서비스 연결.
            faxService.IsTest = true;
        }

        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            joinInfo.LinkID = LinkID;
            joinInfo.CorpNum = "1231212312";          //사업자번호 "-" 제외
            joinInfo.CEOName = "대표자성명";
            joinInfo.CorpName = "상호";
            joinInfo.Addr = "주소";
            joinInfo.ZipCode = "500-100";
            joinInfo.BizType = "업태";
            joinInfo.BizClass = "업종";
            joinInfo.ID = "userid";                   //6자 이상 20자 미만
            joinInfo.PWD = "pwd_must_be_long_enough"; //6자 이상 20자 미만
            joinInfo.ContactName = "담당자명";
            joinInfo.ContactTEL = "02-999-9999";
            joinInfo.ContactHP = "010-1234-5678";
            joinInfo.ContactFAX = "02-999-9998";
            joinInfo.ContactEmail = "test@test.com";

            try
            {
                Response response = faxService.JoinMember(joinInfo);

                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {

            try
            {
                double remainPoint = faxService.GetBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = faxService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show(response.code.ToString() + " | " + response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = faxService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }
       

        private DateTime? getReserveDT()
        {
            DateTime? reserveDT = null;
            if (String.IsNullOrEmpty(txtReserveDT.Text) == false)
            {

                reserveDT = DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            }
            return reserveDT;
        }


        private void btnGetURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetFaxResult_Click(object sender, EventArgs e)
        {
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;


                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", "111-2222-3333", "수신자 명칭", strFileName, getReserveDT(), txtUserId.Text);

                    MessageBox.Show("접수번호 : " + receiptNum);
                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                List<FaxReceiver> receivers = new List<FaxReceiver>();

                for (int i = 0; i < 100; i++)
                {
                    FaxReceiver receiver = new FaxReceiver();
                    receiver.receiveNum = "111-2222-3333";
                    receiver.receiveName = "수신자명칭_" + i;
                    receivers.Add(receiver);
                }

                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", receivers, strFileName, getReserveDT(), txtUserId.Text);

                    MessageBox.Show("접수번호 : " + receiptNum);
                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<String> filePaths = new List<string>();

            while (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
               filePaths.Add( fileDialog.FileName);

            }

            if(filePaths.Count > 0)
            {
                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", "111-2222-3333", "수신자 명칭", filePaths, getReserveDT(), txtUserId.Text);

                    MessageBox.Show("접수번호 : " + receiptNum);
                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<String> filePaths = new List<string>();

            while (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filePaths.Add(fileDialog.FileName);

            }

            if (filePaths.Count > 0)
            {
                List<FaxReceiver> receivers = new List<FaxReceiver>();

                for (int i = 0; i < 100; i++)
                {
                    FaxReceiver receiver = new FaxReceiver();
                    receiver.receiveNum = "111-2222-3333";
                    receiver.receiveName = "수신자명칭_" + i;
                    receivers.Add(receiver);
                }


                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, "070-7510-6766", receivers, filePaths, getReserveDT(), txtUserId.Text);

                    MessageBox.Show("접수번호 : " + receiptNum);
                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
                }

            }
        }

        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckID(조회할 회원아이디)
                Response response = faxService.CheckID(txtUserId.Text);

                MessageBox.Show("[ " + response.code.ToString() + " ] " + response.message, "ID 중복확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "ID 중복확인");

            }
        }

        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            contactInfo.id = "test12341234";        // 담당자 아이디, 한글, 영문(대/소), 숫자, '-', '_' 6자 이상 20자 미만 구성
            contactInfo.pwd = "12345";              // 비밀번호, 6자 이상 20자 미만 구성
            contactInfo.personName = "담당자 명";   // 담당자명 
            contactInfo.tel = "070-7510-3710";      // 연락처
            contactInfo.hp = "010-1234-1234";       // 휴대폰번호
            contactInfo.fax = "070-7510-3710";      // 팩스번호 
            contactInfo.email = "code@linkhub.co.kr";   // 이메일주소
            contactInfo.searchAllAllowYN = false;   // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.mgrYN = false;              // 관리자 권한여부 

            try
            {
                Response response = faxService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "담당자 추가");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "담당자 추가");
            }
        }

        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = faxService.ListContact(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;

                foreach (Contact contactInfo in contactList)
                {
                    tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                    tmp += "personName (담당자명) : " + contactInfo.personName + CRLF;
                    tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                    tmp += "hp (휴대폰번호) : " + contactInfo.hp + CRLF;
                    tmp += "searchAllAllowYN (회사조회 여부) : " + contactInfo.searchAllAllowYN + CRLF;
                    tmp += "tel (연락처) : " + contactInfo.tel + CRLF;
                    tmp += "fax (팩스번호) : " + contactInfo.fax + CRLF;
                    tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN + CRLF;
                    tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                    tmp += CRLF;
                }

                MessageBox.Show(tmp, "담당자 목록조회");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "담당자 목록조회");
            }
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            contactInfo.personName = "담당자123";      // 담당자명 
            contactInfo.tel = "070-7510-3710";      // 연락처
            contactInfo.hp = "010-1234-1234";       // 휴대폰번호
            contactInfo.fax = "070-7510-3710";      // 팩스번호 
            contactInfo.email = "code@linkhub.co.kr";   // 이메일주소
            contactInfo.searchAllAllowYN = true;    // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.mgrYN = false;              // 관리자 권한여부 

            try
            {
                Response response = faxService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "담당자 정보 수정");
            }
        }

        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = faxService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;
                tmp += "ceoname (대표자명) : " + corpInfo.ceoname + CRLF;
                tmp += "corpNamem (상호명) : " + corpInfo.corpName + CRLF;
                tmp += "addr (주소) : " + corpInfo.addr + CRLF;
                tmp += "bizType (업태) : " + corpInfo.bizType + CRLF;
                tmp += "bizClass (종목) : " + corpInfo.bizClass + CRLF;

                MessageBox.Show(tmp, "회사정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 조회");
            }
        }

        private void btnUpdateCorpInfo_Click(object sender, EventArgs e)
        {
            CorpInfo corpInfo = new CorpInfo();

            corpInfo.ceoname = "대표자명 테스트";
            corpInfo.corpName = "업체명";
            corpInfo.addr = "주소정보 수정";
            corpInfo.bizType = "업태정보 수정";
            corpInfo.bizClass = "업종정보 수정";

            try
            {
                Response response = faxService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

                MessageBox.Show("[ " + response.code.ToString() + " ] " + response.message, "회사정보 수정");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 수정");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String SDate = "20160101";      // 시작일자, yyyyMMdd
            String EDate = "20160202";      // 종료일자, yyyyMMdd

            //전송상태 배열 1-대기, 2-성공, 3-실패, 4-취소
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            bool ReserveYN = false;     // 예약여부, True-예약전송건 검색
            bool SenderOnly = false;    // 개인조회여부, True-개인조회
            int Page = 1;       // 페이지 번호
            int PerPage = 15;   // 페이지당 검색개수, 최대 1000개

            try
            {
                FAXSearchResult searchResult = faxService.Search(txtCorpNum.Text, SDate, EDate, State, ReserveYN, SenderOnly, Page, PerPage);
                
                String tmp = null;

                tmp += "code : " + searchResult.code + CRLF;
                tmp += "total : " + searchResult.total + CRLF;
                tmp += "perPage : " + searchResult.perPage + CRLF;
                tmp += "pageNum : " + searchResult.pageNum + CRLF;
                tmp += "pageCount : " + searchResult.pageCount + CRLF;
                tmp += "message : " + searchResult.message + CRLF + CRLF;

                tmp += "sendState | convState | sendNum | receiveNum | receiveName | sendPageCnt | successPageCnt | failPageCnt | refundPageCnt | ";
                tmp += "cancelPageCnt | reserveDT | sendDT | resultDT | sendResult";
                tmp += CRLF;
                

                foreach (FaxResult faxInfo in searchResult.list)
                {
                    tmp += faxInfo.sendState + " | ";
                    tmp += faxInfo.convState + " | ";
                    tmp += faxInfo.sendNum + " | ";
                    tmp += faxInfo.receiveNum + " | ";
                    tmp += faxInfo.receiveName + " | ";
                    tmp += faxInfo.sendPageCnt + " | ";
                    tmp += faxInfo.successPageCnt + " | ";
                    tmp += faxInfo.failPageCnt + " | ";
                    tmp += faxInfo.refundPageCnt + " | ";
                    tmp += faxInfo.cancelPageCnt + " | ";
                    tmp += faxInfo.reserveDT + " | ";
                    tmp += faxInfo.sendDT + " | ";
                    tmp += faxInfo.resultDT + " | ";
                    tmp += faxInfo.sendResult;
                    
                    tmp += CRLF;
                }

                MessageBox.Show(tmp, "팩스 전송내역 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "팩스 전송내역 조회");
            }
        }
    }
}