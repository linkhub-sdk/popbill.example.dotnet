/*
 * 팝빌 예금주조회 API DotNet SDK Example
 *
 * - DotNet C# SDK 연동환경 설정방법 안내 : [개발가이드] - https://developers.popbill.com/guide/accountcheck/dotnet/getting-started/tutorial?fwn=csharp
 * - 업데이트 일자 : 2022-05-04
 * - 연동 기술지원 연락처 : 1600-9854
 * - 연동 기술지원 이메일 : code@linkhubcorp.com
 *
 * <테스트 연동개발 준비사항>
 * 1) 27, 30 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
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

            // 연동환경 설정값, true-개발용, false-상업용
            accountCheckService.IsTest = true;

            // 인증토큰 발급 IP 제한 On/Off, true-사용, false-미사용, 기본값(true)
            accountCheckService.IPRestrictOnOff = true;

            // 팝빌 API 서비스 고정 IP 사용여부, true-사용, false-미사용, 기본값(false)
            accountCheckService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, 기본값(false)
            accountCheckService.UseLocalTimeYN = false;
        }

        /*
         * 1건의 예금주성명을 조회합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/check#CheckAccountInfo
         */
        private void btnCheckAccountInfo_Click(object sender, EventArgs e)
        {
            String tmp = "";

            try
            {
                AccountCheckInfo result =
                    accountCheckService.CheckAccountInfo(txtCorpNum.Text, txtBankCode.Text, txtAccountNumber.Text);

                tmp += "bankCode (기관코드) : " + result.bankCode + "\n";
                tmp += "accountNumber (계좌번호) : " + result.accountNumber + "\n";
                tmp += "accountName (예금주 성명) : " + result.accountName + "\n";
                tmp += "checkDate (확인일시) : " + result.checkDate + "\n";
                tmp += "result (응답코드) : " + result.result + "\n";
                tmp += "resultMessage (응답메시지) : " + result.resultMessage + "\n";

                MessageBox.Show(tmp, "예금주성명 조회");
            }

            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예금주성명 조회");
            }
        }

        /*
         * 1건의 예금주실명을 조회합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/check#CheckDepositorInfo
         */
        private void btnCheckDepositorInfo_Click(object sender, EventArgs e)
        {
            String tmp = "";

            try
            {
                DepositorCheckInfo result = accountCheckService.CheckDepositorInfo(txtCorpNum.Text, txtBankCodeDC.Text,
                    txtAccountNumberDC.Text, txtIdentityNumTypeDC.Text, txtIdentityNumDC.Text);

                tmp += "bankCode (기관코드) : " + result.bankCode + "\n";
                tmp += "accountNumber (계좌번호) : " + result.accountNumber + "\n";
                tmp += "accountName (예금주 성명) : " + result.accountName + "\n";
                tmp += "checkDate (확인일시) : " + result.checkDate + "\n";
                tmp += "identityNumType (등록번호 유형) : " + result.identityNumType + "\n";
                tmp += "identityNum (등록번호) : " + result.identityNum + "\n";
                tmp += "result (응답코드) : " + result.result + "\n";
                tmp += "resultMessage (응답메시지) : " + result.resultMessage + "\n";

                MessageBox.Show(tmp, "예금주실명 조회");
            }

            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예금주실명 조회");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetBalance
         */
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

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetChargeURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "포인트 충전 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetPaymentURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "연동회원 포인트 결제내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 결제내역 URL");
            }
        }

        /*
         * 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetUseHistoryURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "연동회원 포인트 사용내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 사용내역 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetPartnerBalance
         */
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

        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 포인트충전 URL");
            }
        }

        /*
         * 예금주 성명/실명 조회시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetUnitCost
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            // 서비스 유형, 성명 / 실명 중 택 1
            String serviceType = "실명";

            try
            {
                float unitCost = accountCheckService.GetUnitCost(txtCorpNum.Text, serviceType, txtUserID.Text);

                MessageBox.Show(serviceType + " 조회단가 : " + unitCost.ToString(), "조회단가 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "조회단가 확인");
            }
        }

        /*
         * 예금주조회 API 서비스 과금정보를 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            // 서비스 유형, 성명 / 실명 중 택 1
            String serviceType = "실명";

            try
            {
                ChargeInfo chrgInf = accountCheckService.GetChargeInfo(txtCorpNum.Text, txtUserID.Text, serviceType);

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

        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#CheckIsMember
         */
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

        /*
         * 사용하고자 하는 아이디의 중복여부를 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#CheckID
         */
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

        /*
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#JoinMember
         */
        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            // 아이디, 6자이상 50자 미만
            joinInfo.ID = "userid";

            // 비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
            joinInfo.Password = "asdf8536!@#";

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
            joinInfo.ContactEmail = "";

            // 담당자 연락처 (최대 20자)
            joinInfo.ContactTEL = "";

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

        /*
         * 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = accountCheckService.GetAccessURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#GetCorpInfo
         */
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

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#UpdateCorpInfo
         */
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

        /*
         * 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#RegistContact
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 50자 미만
            contactInfo.id = "testkorea110";

            // 담당자 비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
            contactInfo.Password = "asdf8536!@#";

            //담당자 성명 (최대 100자)
            contactInfo.personName = "담당자명";

            //담당자연락처 (최대 20자)
            contactInfo.tel = "";

            //담당자 이메일 (최대 100자)
            contactInfo.email = "";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

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

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = accountCheckService.GetContactInfo(txtCorpNum.Text, contactID);

                String tmp = null;

                tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                tmp += "personName (담당자명) : " + contactInfo.personName + CRLF;
                tmp += "tel (연락처) : " + contactInfo.tel + CRLF;
                tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole + CRLF;
                tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN + CRLF;
                tmp += "state (상태) : " + contactInfo.state + CRLF;
                tmp += CRLF;

                MessageBox.Show(tmp, "담당자 정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 확인");
            }
        }

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#ListContact
         */
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
                    tmp += "tel (연락처) : " + contactInfo.tel + CRLF;
                    tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                    tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                    tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole + CRLF;
                    tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN + CRLF;
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

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#UpdateContact
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserID.Text;

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자123";

            // 연락처 (최대 20자)
            contactInfo.tel = "";

            // 이메일주소 (최대 100자)
            contactInfo.email = "";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

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


        /**
         * 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#PaymentRequest
         */
        public void btnPaymentRequest_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            PaymentForm PaymentForm = new PaymentForm();

            // 담당자명
            PaymentForm.settlerName = "담당자명";

            // 담당자 이메일
            PaymentForm.settlerEmail = "담당자 이메일";

            // 담당자 휴대폰
            PaymentForm.notifyHP = "담당자 휴대폰";

            // 입금자명
            PaymentForm.paymentName = "입금자명";

            // 결제금액
            PaymentForm.settleCost = "결제금액";

            // 팝빌회원 아이디
            String UserID = "testkorea";


            try
            {
                PaymentResponse response = accountCheckService.PaymentRequest(CorpNum, PaymentForm, UserID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message + "\r\n" +
                                "정산코드" + response.settleCode,
                    "연동회원 무통장 입금신청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 무통장 입금신청");
            }
        }

        /**
         * 연동회원의 포인트 결제내역을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetPaymentHistory
         */
        public void btnGetPaymentHistory_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            String SDate = "";

            String EDate = "";

            int Page = 1;

            int PerPage = 500;

            String UserID = "tstkorea";

            try
            {
                PaymentHistoryResult result =
                    accountCheckService.GetPaymentHistory(CorpNum, SDate, EDate, Page, PerPage, UserID);

                MessageBox.Show(
                    "",
                    "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("",
                    "");
            }
        }

        /**
         * 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetSettleResult
         */
        public void btnGetSettleResult_Click(object sender, EventArgs e)
        {
            String CorpNum = "";
            String SettleCode = "";
            String UserID = "";
            try
            {
                PaymentHistory result =
                    accountCheckService.GetSettleResult(CorpNum, SettleCode, UserID);

                MessageBox.Show(
                    "",
                    "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("",
                    "");
            }
        }

        /**
         * 연동회원의 포인트 사용내역을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetUseHistory
         */
        public void btnGetUseHistory_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            String SDate = "20230501";

            String EDate = "20230530";

            int Page = 1;

            int PerPage = 500;

            String Order = "D";

            String UserID = "testkorea";

            try
            {
                UseHistoryResult result =
                    accountCheckService.GetUseHistory(CorpNum, SDate, EDate, Page, PerPage, Order, UserID);
                MessageBox.Show("", "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("", "");
            }
        }

        /**
         * 연동회원 포인트를 환불 신청합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#Refund
         */
        public void btnRefund_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            RefundForm refundForm = new RefundForm();
            refundForm.ContactName = "";
            refundForm.Reason = "";
            refundForm.TEL = "";
            refundForm.AccountBank = "";
            refundForm.AccountName = "";
            refundForm.AccountNum = "";
            refundForm.ContactName = "";
            refundForm.RequestPoint = "";

            String UserID = "testkorea";

            try
            {
                RefundResponse result = accountCheckService.Refund(CorpNum, refundForm, UserID);
                MessageBox.Show("", "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("", "");
            }
        }

        /**
         * 연동회원의 포인트 환불신청내역을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetRefundHistory
         */
        public void btnGetRefundHistory_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            int Page = 1;

            int PerPage = 500;

            String UserID = "testkorea";

            try
            {
                RefundHistoryResult result = accountCheckService.GetRefundHistory(CorpNum, Page, PerPage, UserID);
                MessageBox.Show("", "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("", "");
            }
        }


        /**
         * 포인트 환불에 대한 상세정보 1건을 확인합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetRefundInfo
         */
        public void btnGetRefundInfo_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            String RefundCode = "";

            String UserID = "testkorea";

            try
            {
                RefundHistory result = accountCheckService.GetRefundInfo(CorpNum, RefundCode, UserID);
                MessageBox.Show("", "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("", "");
            }
        }

        /**
         * 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/point#GetRefundableBalance
         */
        public void btnGetRefundableBalance_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";


            String UserID = "testkorea";

            try
            {
                double result = accountCheckService.GetRefundableBalance(CorpNum, UserID);
                MessageBox.Show("", "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("", "");
            }
        }

        /**
         * 가입된 연동회원의 탈퇴를 요청합니다.
         * - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
         * - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
         * - 관리자 계정만 회원탈퇴가 가능합니다.
         * - https://developers.popbill.com/reference/accountcheck/dotnet/api/member#QuitMember
         */
        public void btnQuitMember_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            String QuitReason = "";

            String UserID = "testkorea";

            try
            {
                Response result = accountCheckService.QuitMember(CorpNum, QuitReason, UserID);
                MessageBox.Show("", "");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("", "");
            }
        }
    }
}