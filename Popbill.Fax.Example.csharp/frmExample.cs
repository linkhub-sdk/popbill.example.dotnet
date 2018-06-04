
/*
 * 팝빌 팩스 API DotNet SDK Example
 * 
 * - DotNet C# SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2018-03-21
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 30, 33 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 */

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
        // 링크아이디
        private string LinkID = "TESTER";

        // 비밀키, 유출에 주의
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private FaxService faxService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 팩스 서비스 모듈 초기화
            faxService = new FaxService(LinkID, SecretKey);

            // 연동환경 설정값 true(개발용), false(상업용)
            faxService.IsTest = true;
        }

        /*
         * 팝빌 로그인 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다. 
         */
        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

                MessageBox.Show(url, "팝빌 로그인 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 연동회원 신규가입을 요청합니다.
         */
        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            // 링크아이디
            joinInfo.LinkID = LinkID;

            // 사업자번호 "-" 제외, 10자리
            joinInfo.CorpNum = "1231212312";

            // 대표자명 
            joinInfo.CEOName = "대표자성명";

            // 상호
            joinInfo.CorpName = "상호";

            // 주소
            joinInfo.Addr = "주소";

            // 업태
            joinInfo.BizType = "업태";

            // 종목
            joinInfo.BizClass = "종목";

            // 아이디, 6자이상 20자 미만
            joinInfo.ID = "userid";

            // 비밀번호, 6자이상 20자 미만
            joinInfo.PWD = "pwd_must_be_long_enough";

            // 담당자명
            joinInfo.ContactName = "담당자명";

            // 담당자 연락처
            joinInfo.ContactTEL = "070-4304-2991";

            // 담당자 휴대폰번호
            joinInfo.ContactHP = "010-111-222";

            // 담당자 팩스번호
            joinInfo.ContactFAX = "070-111-222";

            // 담당자 메일주소
            joinInfo.ContactEmail = "test@test.com";

            try
            {
                Response response = faxService.JoinMember(joinInfo);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "연동회원 가입요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입요청");
            }
        }

        /*
         * 연동회원의 잔여포인트를 조회합니다.
         * - 파트너 과금 방식의 경우 파트너 잔여포인트 조회(GetPartnerBalance API)를 이용하시기 바랍니다. 
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = faxService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("연동회원 잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 연동과금 방식의 경우 연동회원 잔여포인트 조회 (GetBalance API)를 이용하시기 바랍니다.
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = faxService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " + remainPoint.ToString(), "파트너 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 해당사업자의 연동회원 가입여부를 확인합니다.
         * - 사업자번호는 '-' 제외한 10자리 숫자 문자열입니다.
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "연동회원 가입여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입여부 확인");
            }
        }

        /*
         * 팩스 전송단가를 확인합니다.
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = faxService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show("팩스 전송단가 확인 : " + unitCost.ToString(), "팩스 전송단가 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송단가 확인");

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

        /*
         * 팩스 전송내역 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX");

                MessageBox.Show(url, "팩스 전송내역 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송내역 URL");
            }
        }

        /*
         * 팩스 예약전송건을 취소처리합니다.
         * - 예약전송 취소는 전송예약시간 10분전까지만 가능합니다.
         */
        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "팩스 예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 예약전송 취소");
            }
        }

        /*
         * 팩스전송 상태정보 조회
         * - 응답항목에 대한 정보는 "[팩스 API 연동매뉴얼] > 3.3.1 GetFaxDetail 
         *   (전송내역 및 전송상태 확인)" 을 참조하시기 바랍니다. 
         */
        private void btnGetFaxResult_Click(object sender, EventArgs e)
        {
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송상태 확인");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 수신번호
            String receiverNum = "070111222";

            // 수신자명 
            String receiverName = "수신자명";

            // 광고팩스 전송여부
            bool adsYN = false;
            
            // 팩스제목
            String title = "팩스 전송 제목 테스트";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;
                
                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receiverNum, receiverName, 
                        strFileName, getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }

            }
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 팩스제목
            String title = "팩스 동보전송 제목";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                // 수신자정보 배열 (최대 1000건)
                List<FaxReceiver> receivers = new List<FaxReceiver>();

                for (int i = 0; i < 10; i++)
                {
                    FaxReceiver receiver = new FaxReceiver();

                    // 수신번호
                    receiver.receiveNum = "070111222";

                    // 수신자명
                    receiver.receiveName = "수신자명칭_" + i;
                    receivers.Add(receiver);
                }

                // 광고팩스 전송여부
                bool adsYN = false;

                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receivers, strFileName,
                        getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 수신번호
            String receiverNum = "070111222";

            // 수신자명
            String receiverName = "수신자명";

            // 광고팩스 전송여부
            bool adsYN = false;

            // 팩스제목
            String title = "팩스 다수파일전송 제목";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            List<String> filePaths = new List<string>();

            // 팩스전송파일, 최대 20개
            while (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
               filePaths.Add( fileDialog.FileName);

            }

            if(filePaths.Count > 0)
            {
                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receiverNum, receiverName, 
                        filePaths, getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");
            
                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";   

            // 광고팩스 전송여부 
            bool adsYN = false;

            // 팩스제목
            String title = "팩스 다중파일 동보전송 제목";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            List<String> filePaths = new List<string>();

            // 팩스전송파일, 최대 20개
            while (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filePaths.Add(fileDialog.FileName);
            }

            if (filePaths.Count > 0)
            {
                List<FaxReceiver> receivers = new List<FaxReceiver>();

                for (int i = 0; i < 10; i++)
                {
                    // 수신정보 배열(최대 1000건)
                    FaxReceiver receiver = new FaxReceiver();

                    // 수신번호
                    receiver.receiveNum = "070111222";

                    // 수신자명
                    receiver.receiveName = "수신자명칭_" + i;
                    receivers.Add(receiver);
                }

                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receivers, 
                        filePaths, getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스전송");
                }

            }
        }

        /*
         * 팝빌 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url, "포인트충전 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트충전 팝업 URL");
            }
        }

        /*
         * 팝빌 회원아이디 중복여부를 확인합니다.
         * - 아이디는 6자 이상 20미만으로 작성하시기 바랍니다.
         * - 아이디는 대/소문자 구분되지 않습니다.
         * 
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckID(txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "ID 중복여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "ID 중복여부 확인");
            }
        }

        /*
         * 연동회원의 담당자를 추가합니다.
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 20자 미만
            contactInfo.id = "userid";

            //비밀번호, 6자 이상 20자 미만
            contactInfo.pwd = "this_is_password";

            //담당자명 
            contactInfo.personName = "담당자명";

            //담당자연락처
            contactInfo.tel = "070-4304-2991";

            //담당자 휴대폰번호
            contactInfo.hp = "010-111-222";

            //담당자 팩스번호 
            contactInfo.fax = "070-4304-2991";

            //담당자 메일주소
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;

            try
            {
                Response response = faxService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 추가");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 추가");
            }
        }

        /*
         * 연동회원의 담당자 목록을 확인합니다.
         */
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 목록조회");
            }
        }

        /*
         * 담당자 정보를 수정합니다.
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserId.Text;

            // 담당자명 
            contactInfo.personName = "담당자123";

            // 연락처
            contactInfo.tel = "070-4304-2991";

            // 휴대폰번호
            contactInfo.hp = "010-1234-1234";

            // 팩스번호 
            contactInfo.fax = "070-111-222";

            // 이메일주소
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false; 

            try
            {
                Response response = faxService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 정보수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보수정");
            }
        }

        /*
         * 회사정보를 조회합니다.
         */
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회사정보 조회");
            }
        }

        private void btnUpdateCorpInfo_Click(object sender, EventArgs e)
        {
            CorpInfo corpInfo = new CorpInfo();

            // 대표자성명
            corpInfo.ceoname = "대표자명 테스트";

            // 상호
            corpInfo.corpName = "업체명";

            // 주소
            corpInfo.addr = "주소정보 수정";

            // 업태 
            corpInfo.bizType = "업태정보 수정";

            // 종목
            corpInfo.bizClass = "종목 수정";

            try
            {
                Response response = faxService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "회사정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회사정보 수정");
            }
        }

        /*
         * 검색조건을 사용하여 팩스 전송내역을 확인합니다.
         * - 응답항목에 대한 정보는 "[팩스 API 연동매뉴얼] > 3.3.2. Search(전송내역 목록 조회)" 를 
         *   참조하여 주시기 바랍니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20160901";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20161031";     

            //전송상태 배열 1-대기, 2-성공, 3-실패, 4-취소
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            // 예약여부, True-예약전송건 검색
            bool ReserveYN = false;

            // 개인조회여부, True-개인조회, False-회사조회
            bool SenderOnly = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000개
            int PerPage = 100;   

            try
            {
                FAXSearchResult searchResult = faxService.Search(txtCorpNum.Text, SDate, EDate, State, ReserveYN, SenderOnly, Order, Page, PerPage);
                
                String tmp = null;

                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수): " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF + CRLF;

                MessageBox.Show(tmp, "팩스 전송내역 조회");

                dataGridView1.DataSource = searchResult.list;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송내역 조회");
            }
        }

        /*
         * 팩스 APi서비스 과금정보를 확인합니다.
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = faxService.GetChargeInfo(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;
                tmp += "unitCost (전송단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod (과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem (과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, "과금정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "과금정보 조회");
            }
        }
    
        /*
         * 팩스 재전송을 요청합니다.
         */
        private void btnResendFAX_Click(object sender, EventArgs e)
        {
            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "";

            // 수신번호/수신자명을 공백으로 처리시 기존전송정보로 전송
            // 수신번호
            String receiverNum = "";

            // 수신자명 
            String receiverName = "";

            // 팩스제목
            String title = "팩스 재전송 제목";

            try
            {
                String receiptNum = faxService.ResendFAX(txtCorpNum.Text, txtReceiptNum.Text, 
                    senderNum, senderName, receiverNum, receiverName, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 팩스 재전송을 요청합니다. (동보전송)
         */
        private void btnResendFAXSame_Click(object sender, EventArgs e)
        {
            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "07043042991";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "발신자명";

            // 팩스제목
            String title = "팩스재전송 제목";

            // 수신자정보를 변경하지 않고 기존 전송정보로 전송하는 경우
            List<FaxReceiver> receivers = null;

            


            // 수신자정보를 변경하여 재전송하는 경우, 아래코드 참조
            // 수신자정보 배열 (최대 1000건)
            //List<FaxReceiver> receivers = new List<FaxReceiver>();

            /* 
            for (int i = 0; i < 10; i++)
            {
                FaxReceiver receiver = new FaxReceiver();

                // 수신번호
                receiver.receiveNum = "111-2222-3333";

                // 수신자명
                receiver.receiveName = "수신자명칭_" + i;
                receivers.Add(receiver);
            }
            */

            try
            {
                String receiptNum = faxService.ResendFAX(txtCorpNum.Text, txtReceiptNum.Text, senderNum, senderName, receivers, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 팩스 발신번호 목록을 조회합니다.
         */
        private void btnGetSenderNumberList_Click(object sender, EventArgs e)
        {
            try
            {
                List<SenderNumber> SenderNumberList = faxService.GetSenderNumberList(txtCorpNum.Text);

                String tmp = null;

                foreach (SenderNumber numInfo in SenderNumberList)
                {
                    tmp += "발신번호(number) : " + numInfo.number + CRLF;
                    tmp += "대표번호 지정여부(representYN) : " + numInfo.representYN + CRLF;
                    tmp += "등록상태(state) : " + numInfo.state + CRLF + CRLF;

                }

                MessageBox.Show(tmp, "발신번호 목록 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발신번호 목록 조회");
            }
        }

        /*
         * 팩스 발신번호 관리 팝업 URL을 확인합니다.
         */
        private void btnGetURL_SENDER_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetURL(txtCorpNum.Text, txtUserId.Text, "SENDER");

                MessageBox.Show(url, "팩스 발신번호 관리 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 발신번호 관리 팝업 URL");
            }
        }

        /*
         * 파트너 포인트 충전 팝업 URL을 반환합니다. 
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }


        /*
         * 팩스전송 요청시 기재한 요청번호(requestNum)을 이용하여 전송결과 정보를 확인합니다.
         */
        private void btnGetFaxResultRN_Click(object sender, EventArgs e)
        {
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResultRN(txtCorpNum.Text, txtRequestNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송상태 확인");
            }
        }

        /*
         * 팩스전송 요청시 기재한 요청번호(requestNum)을 이용하여  예약전송건을 취소처리합니다.
         * - 예약전송 취소는 전송예약시간 10분전까지만 가능합니다.
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "팩스 예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 예약전송 취소");
            }
        }


        /*
         * 팩스전송 요청시 기재한 요청번호(requestNum)을 이용하여 팩스 재전송을 요청합니다.
         */
        private void btnResendFAXRN_Click(object sender, EventArgs e)
        {

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String assignRequestNum = "";

            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "";

            // 수신번호/수신자명을 공백으로 처리시 기존전송정보로 전송
            // 수신번호
            String receiverNum = "";

            // 수신자명 
            String receiverName = "";

            // 팩스제목
            String title = "팩스 재전송 제목";

            try
            {
                String receiptNum = faxService.ResendFAXRN(txtCorpNum.Text, txtRequestNum.Text, assignRequestNum,
                    senderNum, senderName, receiverNum, receiverName, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 팩스전송 요청시 기재한 요청번호(requestNum)을 이용하여 팩스 재전송을 요청합니다.
         */
        private void btnResendFAXRN_same_Click(object sender, EventArgs e)
        {

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String assignRequestNum = "";

            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "07043042991";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "발신자명";

            // 팩스제목
            String title = "팩스재전송 제목";

            // 수신자정보를 변경하지 않고 기존 전송정보로 전송하는 경우
            List<FaxReceiver> receivers = null;

            // 수신자정보를 변경하여 재전송하는 경우, 아래코드 참조
            // 수신자정보 배열 (최대 1000건)
            //List<FaxReceiver> receivers = new List<FaxReceiver>();

            /* 
            for (int i = 0; i < 10; i++)
            {
                FaxReceiver receiver = new FaxReceiver();

                // 수신번호
                receiver.receiveNum = "111-2222-3333";

                // 수신자명
                receiver.receiveName = "수신자명칭_" + i;
                receivers.Add(receiver);
            }
            */

            try
            {
                String receiptNum = faxService.ResendFAXRN(txtCorpNum.Text, txtRequestNum.Text, assignRequestNum, senderNum, senderName, receivers, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        

    }
}