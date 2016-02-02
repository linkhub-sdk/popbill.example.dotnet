using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.Message.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";
        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private MessageService messageService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            //초기화
            messageService = new MessageService(LinkID, SecretKey);
            //테스트를 완료한후 아래 변수를 false로 변경하거나, 아래줄을 삭제하여 실제 서비스 연결.
            messageService.IsTest = true;
        }

        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

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
                Response response = messageService.JoinMember(joinInfo);

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
                double remainPoint = messageService.GetBalance(txtCorpNum.Text);

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
                double remainPoint = messageService.GetPartnerBalance(txtCorpNum.Text);

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
                Response response = messageService.CheckIsMember(txtCorpNum.Text, LinkID);

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
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text,MessageType.SMS);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }
       
        private void btnUnitCost_LMS_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.LMS);

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

        private void btnSendSMS_one_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, "07075106766", "11122223333", "수신자명칭", "단문 문자 메시지 내용. 90Byte", getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.sendNum = "07075106766";
                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
                msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text,messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendSMS_Same_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
            
                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text,"07075106766","동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendLMS_one_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNum = messageService.SendLMS(
                                        txtCorpNum.Text, 
                                        "07075106766", 
                                        "11122223333", 
                                        "수신자명칭",
                                        "장문문자 메시지 제목",
                                        "장문 문자 메시지 내용. 2000Byte", 
                                        getReserveDT(), 
                                        txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendLMS_hund_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.sendNum = "07075106766";
                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
                msg.subject = "장문 문자메시지 제목";
                msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendLMS_same_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, "07075106766","동보 메시지 제목", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendXMS_one_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNum = messageService.SendXMS(
                                        txtCorpNum.Text,
                                        "07075106766",
                                        "11122223333",
                                        "수신자명칭",
                                        "장문문자 메시지 제목",
                                        "문자 메시지 내용. 메시지의 길이에 따라 90Byte를 기준으로 SMS/LMS를 선택전송",
                                        getReserveDT(),
                                        txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendXMS_hund_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.sendNum = "07075106766";
                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
                msg.subject = "문자메시지 제목";
                msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendXMS_same_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, "07075106766", "동보 메시지 제목", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX");

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
                Response response = messageService.CancelReserve(txtCorpNum.Text,txtReceiptNum.Text,txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetMessageResult_Click(object sender, EventArgs e)
        {
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSendMMS_Click(object sender, EventArgs e)
        {
            try
            {
                string mmsFilePath = "c:\\1425866455.jpg";

                string receiptNum = messageService.SendMMS(
                                        txtCorpNum.Text,
                                        "07075106766",
                                        "11122223333",
                                        "수신자명칭",
                                        "장문문자 메시지 제목",
                                        "장문 문자 메시지 내용. 2000Byte",
                                        mmsFilePath,
                                        getReserveDT(),
                                        txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btn_unitcost_mms_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.MMS);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendMMS_Same_Click(object sender, EventArgs e)
        {
            string mmsFilePath = "c:\\1425866455.jpg";

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendMMS(txtCorpNum.Text, "07075106766", "동보 메시지 제목", "동보 문자 메시지 내용", messages,mmsFilePath, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

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
                Response response = messageService.CheckID(txtUserId.Text);

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
                Response response = messageService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
                List<Contact> contactList = messageService.ListContact(txtCorpNum.Text, txtUserId.Text);

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
                Response response = messageService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
                CorpInfo corpInfo = messageService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
                Response response = messageService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

                MessageBox.Show("[ " + response.code.ToString() + " ] " + response.message, "회사정보 수정");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 수정");
            }
        }

        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {           
            String SDate = "20160120";  // 시작일자, yyyyMMdd
            String EDate = "20160202";  // 종료일자, yyyyMMdd
            
            // 전송상태값 배열, 1-대기, 2-성공, 3-실패, 4-취소
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            // 검색대상 배열, SMS, LMS, MMS
            String[] Item = new String[3];
            Item[0] = "SMS";
            Item[1] = "LMS";
            Item[2] = "MMS";

            bool ReserveYN = false;     // 예약여부, true-예약전송만 조회 
            bool SenderYN = false;      // 개인조회여부 true-개인조회

            String Order = "D";         // 정렬방향, A-오름차순, D-내림차순
            int Page = 1;               // 페이지 번호
            int PerPage = 100;          // 페이지당 검색개수, 최대 1000건

            try
            {
                MSGSearchResult searchResult = messageService.Search(txtCorpNum.Text, SDate, EDate, State, Item, ReserveYN, SenderYN, Order, Page, PerPage);
                
                String tmp = null;
                tmp += "code : " + searchResult.code + CRLF;
                tmp += "total : " + searchResult.total + CRLF;
                tmp += "perPage : " + searchResult.perPage + CRLF;
                tmp += "pageNum : " + searchResult.pageNum + CRLF;
                tmp += "pageCount : " + searchResult.pageCount + CRLF;
                tmp += "message : " + searchResult.message + CRLF;

                MessageBox.Show(tmp, "전송내역조회 결과");

                dataGridView1.DataSource = searchResult.list;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString()+ " ] " +ex.Message);
            }

        }

    }
}
