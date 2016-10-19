
/*
 * 팝빌 휴폐업조회 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2016-10-18
 * - 연동 기술지원 연락처 : 1600-8536 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : dev@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 26, 29 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
  */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Popbill.Closedown;

namespace Popbill.Closedown.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private ClosedownService closedownService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 휴폐업조회 서비스 모듈 초기화
            closedownService = new ClosedownService(LinkID, SecretKey);

            // 연동환경 설정값, 개발용(true), 상업용(false)
            closedownService.IsTest = true;
        }

        /*
         * 해당사업자의 연동회원 가입여부를 확인합니다.
         * - 사업자번호는 '-'를 제외한 10자리 숫자 문자열입니다.
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = closedownService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * 연동회원가입을 요청합니다.
         */
        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            // 링크아이디
            joinInfo.LinkID = LinkID;

            // 사업자번호 "-" 제외
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
            joinInfo.ContactFAX = "02-6442-9700";

            // 담당자 메일주소
            joinInfo.ContactEmail = "test@test.com";

            try
            {
                Response response = closedownService.JoinMember(joinInfo);

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
         * - 파트너 과금 방식의 경우 파트너 잔여 포인트 조회(GetPartnerBalance API)를 이용하시기 바랍니다.
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = closedownService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("연동회원 잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }
    

        /*
         * 휴폐업 조회단가 확인
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = closedownService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show("조회단가 : " +unitCost.ToString(), "휴폐업 조회단가 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "휴폐업 조회단가 확인");

            }
        }

       /*
        * 팝빌 로그인 팝업 URL을 반환합니다.
        * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다 
        */
        private void btnGetPopbillURL_LOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                string url = closedownService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "LOGIN");

                MessageBox.Show(url,"팝빌 로그인 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
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
                string url = closedownService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "CHRG");

                MessageBox.Show(url, "포인트 충전 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }


        /*
         * 1건의 사업자 휴폐업정보를 조회합니다.
         */
        private void btnCheckCorpNum_Click(object sender, EventArgs e)
        {
            String tmp = "";
   
            try
            {
                CorpState result = closedownService.checkCorpNum(txtCorpNum.Text, txtCheckCorpNum.Text);

                tmp += "* state (휴폐업상태) : null-알수없음, 0-등록되지 않은 사업자번호, 1-사업중, 2-폐업, 3-휴업\n";
                tmp += "* type (사업 유형) : null-알수없음, 1-일반과세자, 2-면세과세자, 3-간이과세자, 4-비영리법인, 국가기관\n\n";
                tmp += "corpNum(사업자번호) : " + result.corpNum +"\n";
                tmp += "state(휴폐업상태) : " + result.state + "\n";
                tmp += "type(사업유형) : " + result.type + "\n";
                tmp += "stateDate(휴폐업일자) : " + result.stateDate + "\n";
                tmp += "checkDate(국세청확인일자) : " + result.checkDate + "\n";

                MessageBox.Show(tmp,"휴폐업조회 - 단건");
            }

            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "휴폐업조회 - 단건");
            }
        }

        /*
         * 사업자 휴폐업정보를 조회합니다 (대량).
         */
        private void btnCheckCorpNums_Click(object sender, EventArgs e)
        {
            String tmp = "";

            List<String> CorpNumList = new List<string>();

            // 조회할 사업자번호 배열, 최대 1000건
            CorpNumList.Add("1234567890");
            CorpNumList.Add("4352343543");
            CorpNumList.Add("6798700433");

            try
            {
                List<CorpState> corpStateList = closedownService.checkCorpNums(txtCorpNum.Text, CorpNumList);

                tmp += "* state (휴폐업상태) : null-알수없음, 0-등록되지 않은 사업자번호, 1-사업중, 2-폐업, 3-휴업" + CRLF;
                tmp += "* type (사업 유형) : null-알수없음, 1-일반과세자, 2-면세과세자, 3-간이과세자, 4-비영리법인, 국가기관" + CRLF + CRLF;

                for (int i = 0; i < corpStateList.Count; i++)
                {
                    tmp += "corpNum(사업자번호) : " + corpStateList[i].corpNum + CRLF;
                    tmp += "state(휴폐업상태) : " + corpStateList[i].state + CRLF;
                    tmp += "type(사업유형) : " + corpStateList[i].type + CRLF;
                    tmp += "stateDate(휴폐업1일자) : " + corpStateList[i].stateDate + CRLF;
                    tmp += "checkDate(국세청확인일자) : " + corpStateList[i].checkDate + CRLF + CRLF;
                }
                MessageBox.Show(tmp, "휴폐업조회 - 대량");   
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "휴폐업조회 - 대량");
            }

        }

        private void txtCheckCorpNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCheckCorpNum.PerformClick();
            }
        }

        /*
         * 파트너 잔여포인트를 확인합니다.
         * - 연동과금 방식의 경우 연동회원 잔여포인트 조회(GetBalance API)를 이용하시기 바랍니다.
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = closedownService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " + remainPoint.ToString(), "파트너 잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 팝빌 회원아이디 중복여부를 확인합니다.
         * - 아이디는 6자 이상 20자 미만으로 작성하시기 바랍니다.
         * - 아이디는 대/소문자 구분되지 않습니다.
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = closedownService.CheckID(txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "회원아이디 중복여부 확인");
		
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회원아이디 중복여부 확인");

            }
        }

        /*
         * 연동회원의 담당자를 추가합니다.
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디, 한글, 영문(대/소), 숫자, '-', '_' 6자 이상 20자 미만 구성
            contactInfo.id = "test12341234";

            // 비밀번호, 6자 이상 20자 미만 구성
            contactInfo.pwd = "this_is_password";

            // 담당자명 
            contactInfo.personName = "담당자 명";

            // 연락처
            contactInfo.tel = "070-4304-2991";

            // 휴대폰번호
            contactInfo.hp = "010-1234-1234";

            // 팩스번호 
            contactInfo.fax = "070-4321-4321";

            // 이메일주소
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;

            try
            {
                Response response = closedownService.RegistContact(txtCorpNum.Text, contactInfo, txtUserID.Text);

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
         * 연동회원 담당자 목록을 확인합니다.
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = closedownService.ListContact(txtCorpNum.Text, txtUserID.Text);

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
                Response response = closedownService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserID.Text);

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
                CorpInfo corpInfo = closedownService.GetCorpInfo(txtCorpNum.Text, txtUserID.Text);

                string tmp = null;

                tmp += "ceoname (대표자명) : " + corpInfo.ceoname + CRLF;
                tmp += "corpName (상호명) : " + corpInfo.corpName + CRLF;
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

        /*
         * 회사정보를 수정합니다.
         */
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
                Response response = closedownService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserID.Text);

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
         * 휴폐업조회 API 서비스 과금정보를 확인합니다.
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = closedownService.GetChargeInfo(txtCorpNum.Text);

                string tmp = null;

                tmp += "unitCost (조회단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod (과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem (과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, "과금정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "과금정보 확인");
            }
        }
    }
}
