/*
 * 팝빌 예금주조회 API DotNet SDK Example
 * 
 * - 업데이트 일자 : 2020-08-05
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 28, 31 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
  */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.AccountCheck.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private AccountCheckService accountCheckService;

        private const string CRLF = "\r\n";


        public frmExample()
        {
            InitializeComponent();

            // 예금주조회 서비스 모듈 초기화
            accountCheckService = new AccountCheckService(LinkID, SecretKey);

            // 연동환경 설정값, 개발용(true), 상업용(false)
            accountCheckService.IsTest = true;

            // 발급된 토큰에 대한 IP 제한기능 사용여부, 권장(True)
            accountCheckService.IPRestrictOnOff = true;

            // 로컬PC 시간 사용 여부 true(사용), false(기본값) - 미사용
            accountCheckService.UseLocalTimeYN = false;
        }

        private void btnCheckAccountInfo_Click(object sender, EventArgs e)
        {
            String tmp = "";

            try
            {
                AccountCheckInfo result = accountCheckService.CheckAccountInfo(txtCorpNum.Text, txtBankCode.Text, txtAccountNumber.Text);

                tmp += "bankCode (기관코드) : " + result.bankCode + "\n";
                tmp += "accountNumber (계좌번호) : " + result.accountNumber + "\n";
                tmp += "accountName (예금주 성명) : " + result.accountName + "\n";
                tmp += "checkDate (확인일시) : " + result.checkDate + "\n";
                tmp += "resultCode (응답코드) : " + result.resultCode + "\n";
                tmp += "resultMessage (응답메시지) : " + result.resultMessage + "\n";

                MessageBox.Show(tmp, "예금주성명 조회");
            }

            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예금주조회");
            }
        }

        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = accountCheckService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "연동회원 가입여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입여부 확인");
            }
        }

        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = accountCheckService.CheckID(txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "회원아이디 중복여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회원아이디 중복여부 확인");
            }
        }

        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            
            JoinForm joinInfo = new JoinForm();

            // 아이디, 6자이상 50자 미만
            joinInfo.ID = "userid";

            // 비밀번호, 6자이상 20자 미만
            joinInfo.PWD = "pwd_must_be_long_enough";

            // 링크아이디
            joinInfo.LinkID = LinkID;

            // 사업자번호 "-" 제외
            joinInfo.CorpNum = "1231212312";

            // 대표자명 (최대 100자)
            joinInfo.CEOName = "대표자성명";

            // 상호 (최대 200자)
            joinInfo.CorpName = "상호";

            // 사업장 주소 (최대 300자)
            joinInfo.Addr = "주소";

            // 업태 (최대 100자)
            joinInfo.BizType = "업태";

            // 종목 (최대 100자)
            joinInfo.BizClass = "종목";

            // 담당자 성명 (최대 100자)
            joinInfo.ContactName = "담당자명";

            // 담당자 이메일 (최대 20자)
            joinInfo.ContactEmail = "test@test.com";

            // 담당자 연락처 (최대 20자)
            joinInfo.ContactTEL = "070-4304-2991";

            // 담당자 휴대폰번호 (최대 20자)
            joinInfo.ContactHP = "010-111-222";

            // 담당자 팩스번호 (최대 20자)
            joinInfo.ContactFAX = "02-6442-9700";

            try
            {
                Response response = accountCheckService.JoinMember(joinInfo);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "연동회원 가입요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입요청");
            }
        }

        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = accountCheckService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show("조회단가 : " + unitCost.ToString(), "조회단가 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "조회단가 확인");
            }
        }

        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = accountCheckService.GetChargeInfo(txtCorpNum.Text);

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

        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = accountCheckService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("연동회원 잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetChargeURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "포인트 충전 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }

        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = accountCheckService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " + remainPoint.ToString(), "파트너 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 포인트충전 URL");
            }
        }

        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetAccessURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 50자 미만
            contactInfo.id = "testkorea_20190110";

            //비밀번호, 6자 이상 20자 미만
            contactInfo.pwd = "user_password";

            //담당자 성명 (최대 100자) 
            contactInfo.personName = "담당자명";

            //담당자연락처 (최대 20자)
            contactInfo.tel = "070-4304-2991";

            //담당자 휴대폰번호 (최대 20자)
            contactInfo.hp = "010-111-222";

            //담당자 팩스번호 (최대 20자)
            contactInfo.fax = "070-4304-2991";

            //담당자 이메일 (최대 100자)
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;

            try
            {
                Response response = accountCheckService.RegistContact(txtCorpNum.Text, contactInfo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 추가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 추가");
            }
        }

        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = accountCheckService.ListContact(txtCorpNum.Text, txtUserID.Text);

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
                    tmp += "state (상태) : " + contactInfo.state + CRLF;
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

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {

            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserID.Text;

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자123";

            // 연락처 (최대 20자)
            contactInfo.tel = "070-4304-2991";

            // 휴대폰번호 (최대 20자)
            contactInfo.hp = "010-1234-1234";

            // 팩스번호 (최대 20자)
            contactInfo.fax = "02-6442-9700";

            // 이메일주소 (최대 100자)
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;

            try
            {
                Response response = accountCheckService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 정보수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보수정");
            }
        }

        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = accountCheckService.GetCorpInfo(txtCorpNum.Text, txtUserID.Text);

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

        private void btnUpdateCorpInfo_Click(object sender, EventArgs e)
        {

            CorpInfo corpInfo = new CorpInfo();

            // 대표자성명 (최대 100자)
            corpInfo.ceoname = "대표자명 테스트";

            // 상호 (최대 200자)
            corpInfo.corpName = "업체명";

            // 주소 (최대 300자)
            corpInfo.addr = "주소정보 수정";

            // 업태 (최대 100자)
            corpInfo.bizType = "업태정보 수정";

            // 종목 (최대 100자)
            corpInfo.bizClass = "종목 수정";

            try
            {
                Response response = accountCheckService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "회사정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회사정보 수정");
            }
        }

    }
}
