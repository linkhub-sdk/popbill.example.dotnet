/*
 * 팝빌 계좌조회 API DotNet SDK Example
 *
 * - DotNet C# SDK 연동환경 설정방법 안내 : [개발가이드] - https://docs.popbill.com/easyfinbank/tutorial/dotnet_csharp
 * - 업데이트 일자 : 2022-05-04
 * - 연동 기술지원 연락처 : 1600-9854
 * - 연동 기술지원 이메일 : code@linkhubcorp.com
 *
 * <테스트 연동개발 준비사항>
 * 1) 29, 32 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
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

namespace Popbill.EasyFin.Bank.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private EasyFinBankService easyFinBankService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 계좌조회 서비스 모듈 초기화
            easyFinBankService = new EasyFinBankService(LinkID, SecretKey);

            // 연동환경 설정값, true-개발용, false-상업용
            easyFinBankService.IsTest = true;

            // 인증토큰 발급 IP 제한 On/Off, true-사용, false-미사용, 기본값(true)
            easyFinBankService.IPRestrictOnOff = true;

            // 팝빌 API 서비스 고정 IP 사용여부, true-사용, false-미사용, 기본값(false)
            easyFinBankService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, 기본값(false)
            easyFinBankService.UseLocalTimeYN = false;
        }

        /*
         * 계좌조회 서비스를 이용할 계좌를 팝빌에 등록합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#RegistBankAccount
         */
        private void btnRegistBankAccount_Click(object sender, EventArgs e)
        {
            EasyFinBankAccountForm info = new EasyFinBankAccountForm();

            // 기관코드
            // 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
            // SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
            // 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
            info.BankCode = "";

            // 계좌번호, 하이픈('-') 제외
            info.AccountNumber = "";

            // 계좌비밀번호
            info.AccountPWD = "";

            // 계좌유형, "법인" 또는 "개인" 입력
            info.AccountType = "";

            // 예금주 식별정보 ('-' 제외)
            // 계좌유형이 "법인"인 경우 : 사업자번호(10자리)
            // 계좌유형이 "개인"인 경우 : 예금주 생년월일 (6자리-YYMMDD)
            info.IdentityNumber = "";

            // 계좌 별칭
            info.AccountName = "";

            // 인터넷뱅킹 아이디 (국민은행 필수)
            info.BankID = "";

            // 조회전용 계정 아이디 (대구은행, 신협, 신한은행 필수)
            info.FastID = "";

            // 조회전용 계정 비밀번호 (대구은행, 신협, 신한은행 필수)
            info.FastPWD = "";

            // 정액제 이용할 개월수, 1~12 입력가능
            // - 미입력시 기본값 1개월 처리
            // - 파트너 과금방식의 경우 입력값에 관계없이 1개월 처리
            info.UsePeriod = "";

            // 메모
            info.Memo = "";

            try
            {
                Response response = easyFinBankService.RegistBankAccount(txtCorpNum.Text, info);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "계좌 등록");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "계좌 등록");
            }
        }

        /*
         * 팝빌에 등록된 계좌정보를 수정합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#UpdateBankAccount
         */
        private void btnUpdateBankAccount_Click(object sender, EventArgs e)
        {
            EasyFinBankAccountForm info = new EasyFinBankAccountForm();

            // 기관코드
            // 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
            // SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
            // 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
            info.BankCode = "";

            // 계좌번호, 하이픈('-') 제외
            info.AccountNumber = "";

            // 계좌비밀번호
            info.AccountPWD = "";

            // 계좌 별칭
            info.AccountName = "";

            // 인터넷뱅킹 아이디 (국민은행 필수)
            info.BankID = "";

            // 조회전용 계정 아이디 (대구은행, 신협, 신한은행 필수)
            info.FastID = "";

            // 조회전용 계정 비밀번호 (대구은행, 신협, 신한은행 필수)
            info.FastPWD = "";

            // 메모
            info.Memo = "";


            try
            {
                Response response = easyFinBankService.UpdateBankAccount(txtCorpNum.Text, info);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "계좌 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "계좌 수정");
            }
        }

        /*
         * 팝빌에 등록된 계좌 정보를 확인합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetBankAccountInfo
         */
        private void btnGetBankAccountInfo_Click(object sender, EventArgs e)
        {
            // 기관코드
            // 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
            // SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
            // 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
            String BankCode = "";

            // 계좌번호, 하이픈('-') 제외
            String AccountNumber = "";

            try
            {
                EasyFinBankAccount bankInfo = easyFinBankService.GetBankAccountInfo(txtCorpNum.Text, BankCode, AccountNumber);

                String tmp = null;
                tmp += "accountNumber (계좌번호) : " + bankInfo.accountNumber + CRLF;
                tmp += "bankCode (기관코드) : " + bankInfo.bankCode + CRLF;
                tmp += "accountName (계좌 별칭) : " + bankInfo.accountName +CRLF;
                tmp += "accountType (계좌 유형) : " +bankInfo.accountType +CRLF;
                tmp += "state (계좌 상태) : " + bankInfo.state.ToString() + CRLF;
                tmp += "regDT (등록일시) : " + bankInfo.regDT + CRLF;
                tmp += "contractDT (정액제 서비스 시작일시) : " + bankInfo.contractDT + CRLF;
                tmp += "useEndDate (정액제 서비스 종료일자) : " + bankInfo.useEndDate + CRLF;
                tmp += "baseDate (자동연장 결제일) : " + bankInfo.baseDate.ToString() + CRLF;
                tmp += "contractState (정액제 서비스 상태) : " + bankInfo.contractState.ToString() + CRLF;
                tmp += "closeRequestYN (정액제 서비스 해지신청 여부) : " + bankInfo.closeRequestYN.ToString() + CRLF;
                tmp += "useRestrictYN (정액제 서비스 사용제한 여부) : " + bankInfo.useRestrictYN.ToString() + CRLF;
                tmp += "closeOnExpired (정액제 서비스 만료 시 해지 여부) : " +bankInfo.closeOnExpired.ToString() + CRLF;
                tmp += "unPaiedYN (미수금 보유 여부) : " + bankInfo.unPaidYN.ToString() + CRLF;
                tmp += "memo (메모) : "+ bankInfo.memo;


                MessageBox.Show(tmp, "계좌정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "계좌 목록 확인");
            }

        }

        /*
         * 팝빌에 등록된 은행계좌 목록을 반환한다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#ListBankAccount
         */
        private void btnListBankAccount_Click(object sender, EventArgs e)
        {

            try
            {
                List<EasyFinBankAccount> bankAccountList = easyFinBankService.ListBankAccount(txtCorpNum.Text);

                String tmp = "bankCode (기관코드) | accountNumber (계좌번호) | accountName (계좌별칭) | accountType (계좌유형) | state (정액제 상태) |";
                tmp += " regDT (등록일시) | contractDT (정액제 서비스 시작일시) | useEndDate (정액제 서비스 종료일자) | baseDate (자동연장 결제일) |";
                tmp += " contractState (정액제 서비스 상태) | closeRequestYN (정액제 해지신청 여부) | useRestrictYN (정액제 사용제한 여부) |";
                tmp += " closeOnExpired (정액제 만료 시 해지여부) | unPaiedYN (미수금 보유 여부) | memo (메모) " + CRLF + CRLF;

                for (int i = 0; i < bankAccountList.Count; i++)
                {
                    tmp += bankAccountList[i].bankCode + " | ";
                    tmp += bankAccountList[i].accountNumber + " | ";
                    tmp += bankAccountList[i].accountName + " | ";
                    tmp += bankAccountList[i].accountType + " | ";
                    tmp += bankAccountList[i].state.ToString() + " | ";
                    tmp += bankAccountList[i].regDT + " | ";
                    tmp += bankAccountList[i].contractDT + " | ";
                    tmp += bankAccountList[i].baseDate.ToString() + " | ";
                    tmp += bankAccountList[i].useEndDate + " | ";
                    tmp += bankAccountList[i].contractState.ToString() + " | ";
                    tmp += bankAccountList[i].closeRequestYN.ToString() + " | ";
                    tmp += bankAccountList[i].useRestrictYN.ToString() + " | ";
                    tmp += bankAccountList[i].closeOnExpired.ToString() + " | ";
                    tmp += bankAccountList[i].unPaidYN.ToString() + " | ";

                    tmp += bankAccountList[i].memo;

                    tmp += CRLF ;
                }

                MessageBox.Show(tmp, "계좌 목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "계좌 목록 확인");
            }
        }

        /*
         * 계좌 등록, 수정 및 삭제할 수 있는 계좌 관리 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetBankAccountMgtURL
         */
        private void btnBankAccountMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                String url = easyFinBankService.GetBankAccountMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "계좌 관리 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 신청 팝업 URL");
            }
        }

        /*
         * 계좌의 정액제 해지를 요청합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#CloseBankAccount
         */
        private void btnCloseBankAccount_Click(object sender, EventArgs e)
        {
            // 기관코드
            // 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
            // SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
            // 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
            String BankCode = "";

            // 계좌번호, 하이픈('-') 제외
            String AccountNumber = "";

            // 해지유형, "일반", "중도" 중 택 1
            // 일반(일반해지) – 이용중인 정액제 기간 만료 후 해지
            // 중도(중도해지) – 해지 요청일 기준으로 정지되고 팝빌 담당자가 승인시 해지
            // └ 중도일 경우, 정액제 잔여기간은 일할로 계산되어 포인트 환불 (무료 이용기간 중 해지하면 전액 환불)
            String CloseType = "중도";


            try
            {
                Response response = easyFinBankService.CloseBankAccount(txtCorpNum.Text, BankCode, AccountNumber, CloseType);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "계좌 정액제 해지요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "계좌 정액제 해지요청");
            }
        }

        /*
         * 신청한 정액제 해지요청을 취소합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#RevokeCloseBankAccount
         */
        private void btnRevokeCloseBankAccount_Click(object sender, EventArgs e)
        {

            // 기관코드
            // 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
            // SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
            // 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
            String BankCode = "";

            // 계좌번호, 하이픈('-') 제외
            String AccountNumber = "";

            try
            {
                Response response = easyFinBankService.RevokeCloseBankAccount(txtCorpNum.Text, BankCode, AccountNumber);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "계좌 정액제 해지요청 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "계좌 정액제 해지요청 취소");
            }
        }

        /*
         * 등록된 계좌를 삭제합니다.
         * - 정액제가 아닌 종량제 이용 시에만 등록된 계좌를 삭제할 수 있습니다.
         * - 정액제 이용 시 정액제 해지요청(CloseBankAccount API) 함수를 사용하여 정액제를 해제할 수 있습니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#DeleteBankAccount
         */
        private void btnDeleteBankAccount_Click(object sender, EventArgs e)
        {
            // 기관코드
            // 산업은행-0002 / 기업은행-0003 / 국민은행-0004 /수협은행-0007 / 농협은행-0011 / 우리은행-0020
            // SC은행-0023 / 대구은행-0031 / 부산은행-0032 / 광주은행-0034 / 제주은행-0035 / 전북은행-0037
            // 경남은행-0039 / 새마을금고-0045 / 신협은행-0048 / 우체국-0071 / KEB하나은행-0081 / 신한은행-0088 /씨티은행-0027
            String BankCode = "";

            // 계좌번호, 하이픈('-') 제외
            String AccountNumber = "";

            try
            {
                Response response = easyFinBankService.DeleteBankAccount(txtCorpNum.Text, BankCode, AccountNumber);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "종량제 계좌 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "종량제 계좌 삭제");
            }
        }

        /*
         * 계좌 거래내역을 확인하기 위해 팝빌에 수집요청을 합니다. (조회기간 단위 : 최대 1개월)
         * - 조회일로부터 최대 3개월 이전 내역까지 조회할 수 있습니다.
         * - 반환 받은 작업아이디는 함수 호출 시점부터 1시간 동안 유효합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#RequestJob
         */
        private void btnRequestJob_Click(object sender, EventArgs e)
        {
            // 기관코드
            String BankCode = "";

            // 은행 계좌번호
            String AccountNumber = "";

            // 시작일자, 표시형식(yyyyMMdd)
            String SDate = "20220501";

            // 종료일자, 표시형식(yyyyMMdd)
            String EDate = "20220504";

            try
            {
                String jobID = easyFinBankService.RequestJob(txtCorpNum.Text, BankCode, AccountNumber, SDate, EDate);

                MessageBox.Show("작업아이디(jobID) : " + jobID, "수집 요청");

                txtJobID.Text = jobID;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 요청");
            }
        }

        /*
         * 수집 요청(RequestJob API) 함수를 통해 반환 받은 작업 아이디의 상태를 확인합니다.
         * - 거래 내역 조회(Search API) 함수 또는 거래 요약 정보 조회(Summary API) 함수를 사용하기 전에
         *   수집 작업의 진행 상태, 수집 작업의 성공 여부를 확인해야 합니다.
         * - 작업 상태(jobState) = 3(완료)이고 수집 결과 코드(errorCode) = 1(수집성공)이면
         *   거래 내역 조회(Search) 또는 거래 요약 정보 조회(Summary) 를 해야합니다.
         * - 작업 상태(jobState)가 3(완료)이지만 수집 결과 코드(errorCode)가 1(수집성공)이 아닌 경우에는
         *   오류메시지(errorReason)로 수집 실패에 대한 원인을 파악할 수 있습니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetJobState
         */
        private void btnGetJobState_Click(object sender, EventArgs e)
        {
            try
            {
                EasyFinBankJobState jobState = easyFinBankService.GetJobState(txtCorpNum.Text, txtJobID.Text);

                String tmp = "jobID (작업아이디) : " + jobState.jobID + CRLF;
                tmp += "jobState (수집상태) : " + jobState.jobState.ToString() + CRLF;
                tmp += "startDate (시작일자) : " + jobState.startDate + CRLF;
                tmp += "endDate (종료일자) : " + jobState.endDate + CRLF;
                tmp += "errorCode (오류코드) : " + jobState.errorCode.ToString() + CRLF;
                tmp += "errorReason (오류메시지) : " + jobState.errorReason + CRLF;
                tmp += "jobStartDT (작업 시작일시) : " + jobState.jobStartDT + CRLF;
                tmp += "jobEndDT (작업 종료일시) : " + jobState.jobEndDT + CRLF;
                tmp += "regDT (수집 요청일시) : " + jobState.regDT + CRLF;

                MessageBox.Show(tmp, "수집 상태 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 상태 확인");
            }
        }

        /*
         * 수집 요청(RequestJob API) 함수를 통해 반환 받은 작업아이디의 목록을 확인합니다.
         * - 수집 요청 후 1시간이 경과한 수집 요청건은 상태정보가 반환되지 않습니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#ListActiveJob
         */
        private void btnListActiveJob_Click(object sender, EventArgs e)
        {
            try
            {
                List<EasyFinBankJobState> jobList = easyFinBankService.ListACtiveJob(txtCorpNum.Text);

                String tmp = "jobID (작업아이디) | jobState (수집상태) | startDate (시작일자) |";
                tmp += " endDate (종료일자) | errorCode (오류코드) | errorReason (오류메시지) | jobStartDT (수집 시작일시) | jobEndDT (수집 종료일시) |";
                tmp += " regDT (수집 요청일시) " + CRLF;

                for (int i = 0; i < jobList.Count; i++)
                {
                    tmp += jobList[i].jobID + " | ";
                    tmp += jobList[i].jobState.ToString() + " | ";
                    tmp += jobList[i].startDate + " | ";
                    tmp += jobList[i].endDate + " | ";
                    tmp += jobList[i].errorCode.ToString() + " | ";
                    tmp += jobList[i].errorReason + " | ";
                    tmp += jobList[i].jobStartDT + " | ";
                    tmp += jobList[i].jobEndDT + " | ";
                    tmp += jobList[i].regDT;
                    tmp += CRLF;
                }

                if (jobList.Count > 0) txtJobID.Text = jobList[0].jobID;

                MessageBox.Show(tmp, "수집 작업 목록확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 요청 목록확인");
            }
        }

        /*
         * 수집 상태 확인(GetJobState API) 함수를 통해 상태 정보가 확인된 작업아이디를 활용하여 계좌 거래 내역을 조회합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 거래유형 배열 ("I" 와 "O" 중 선택, 다중 선택 가능)
            // └ I = 입금 , O = 출금
            // - 미입력 시 전체조회
            String[] TradeType = { "I", "O" };

            // "입·출금액" / "메모" / "비고" 중 검색하고자 하는 값 입력
            // - 메모 = 거래내역 메모저장(SaveMemo API) 함수를 사용하여 저장한 값
            // - 비고 = EasyFinBankSearchDetail의 remark1, remark2, remark3 값
            // - 미입력시 전체조회
            String SearchString = "";

            // 페이지번호
            int Page = 1;

            // 페이지당 목록개수, 최대 1000건
            int PerPage = 10;

            // 정렬방향 D-내림차순, A-오름차순
            String Order = "D";

            listBox1.Items.Clear();

            try
            {
                EasyFinBankSearchResult searchInfo = easyFinBankService.Search(txtCorpNum.Text, txtJobID.Text,
                    TradeType, SearchString, Page, PerPage, Order);

                String tmp = "code (응답코드) : " + searchInfo.code.ToString() + CRLF;
                tmp += "message (응답메시지) : " + searchInfo.message + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchInfo.total.ToString() + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchInfo.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchInfo.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchInfo.pageCount + CRLF;
                tmp += "lastScrapDT (최종 조회일시) : " + searchInfo.lastScrapDT + CRLF;

                MessageBox.Show(tmp, "수집 결과 조회");

                string rowStr = "tid (거래내역 아이디) | trdate (거래일자) | trserial (거래일련번호) | trdt (거래일시) | accIn (입금액) | accOut (출금액) | balance (잔액) | " + CRLF +
                                "remark1 (비고1) | remark2 (비고2) | remark3 (비고3) | remark4 (비고4) | memo (메모)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < searchInfo.list.Count; i++)
                {
                    rowStr = null;
                    rowStr += searchInfo.list[i].tid + " | ";
                    rowStr += searchInfo.list[i].trdate+ " | ";
                    rowStr += searchInfo.list[i].trserial.ToString() + " | ";
                    rowStr += searchInfo.list[i].trdt + " | ";
                    rowStr += searchInfo.list[i].accIn + " | ";
                    rowStr += searchInfo.list[i].accOut + " | ";
                    rowStr += searchInfo.list[i].balance + " | ";
                    rowStr += searchInfo.list[i].remark1 + " | ";
                    rowStr += searchInfo.list[i].remark2 + " | ";
                    rowStr += searchInfo.list[i].remark3 + " | ";
                    rowStr += searchInfo.list[i].remark4 + " | ";
                    rowStr += searchInfo.list[i].memo;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 결과 조회");
            }
        }

        /*
         * 수집 상태 확인(GetJobState API) 함수를 통해 상태 정보가 확인된 작업아이디를 활용하여 계좌 거래내역의 요약 정보를 조회합니다.
         * - 요약 정보는 입·출 금액 합계, 입·출 거래 건수를 가리킵니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#Summary
         */
        private void btnSummary_Click(object sender, EventArgs e)
        {
             // 거래유형 배열 ("I" 와 "O" 중 선택, 다중 선택 가능)
             // └ I = 입금 , O = 출금
             // - 미입력 시 전체조회
            String[] TradeType = { "I", "O" };

            // "입·출금액" / "메모" / "비고" 중 검색하고자 하는 값 입력
            // - 메모 = 거래내역 메모저장(SaveMemo API) 함수를 사용하여 저장한 값
            // - 비고 = EasyFinBankSearchDetail의 remark1, remark2, remark3 값
            // - 미입력시 전체조회
            String SearchString = "";

            try
            {
                EasyFinBankSummary searchInfo = easyFinBankService.Summary(txtCorpNum.Text, txtJobID.Text,
                    TradeType, SearchString);

                String tmp = "count (수집결과 건수) : " + searchInfo.count + CRLF;
                tmp += "cntAccIn (입금거래 건수) : " + searchInfo.cntAccIn + CRLF;
                tmp += "cntAccOut (출금거래 건수) : " + searchInfo.cntAccOut + CRLF;
                tmp += "totalAccIn (입금액 합계) : " + searchInfo.totalAccIn + CRLF;
                tmp += "totalAccOut (출금액 합계) : " + searchInfo.totalAccOut + CRLF;

                MessageBox.Show(tmp, "거래내역 요약정보 조회");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "거래내역 요약정보 조회");
            }
        }

        /*
         * 한 건의 거래 내역에 메모를 저장합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#SaveMemo
         */
        private void btnSaveMemo_Click(object sender, EventArgs e)
        {
             // 메모를 저장할 거래내역 아이디
             // └ 거래내역 조회(Seach API) 함수의 반환값인 EasyFinBankSearchDetail 의 tid를 통해 확인 가능
            String TID = txtTID.Text;

            // 메모
            String Memo = "메모저장";

            try
            {
                Response response = easyFinBankService.SaveMemo(txtCorpNum.Text, TID, Memo);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "거래내역 메모저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "거래내역 메모저장");
            }
        }

        /*
         * 계좌조회 정액제 서비스 신청 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetFlatRatePopUpURL
         */
        private void btnGetFlatRatePopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                String url = easyFinBankService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "정액제 서비스 신청 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 신청 팝업 URL");
            }
        }

        /*
         * 계좌조회 정액제 서비스 상태를 확인합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetFlatRateState
         */
        private void btnGetFlatRateState_Click(object sender, EventArgs e)
        {
            // 기관코드
            String BankCode = "";

            // 은행 계좌번호
            String AccountNumber = "";

            try
            {
                EasyFinBankFlatRate rateInfo = easyFinBankService.GetFlatRateState(txtCorpNum.Text, BankCode, AccountNumber);

                String tmp = "referenceID (계좌아이디) : " + rateInfo.referenceID + CRLF;
                tmp += "contractDT (정액제 서비스 시작일시) : " + rateInfo.contractDT + CRLF;
                tmp += "useEndDate (정액제 서비스 종료일) : " + rateInfo.useEndDate + CRLF;
                tmp += "baseDate (자동연장 결제일) : " + rateInfo.baseDate.ToString() + CRLF;
                tmp += "state (정액제 서비스 상태) : " + rateInfo.state.ToString() + CRLF;
                tmp += "closeRequestYN (정액제 서비스 해지신청 여부) : " + rateInfo.closeRequestYN.ToString() + CRLF;
                tmp += "useRestrictYN (정액제 서비스 사용제한 여부) : " + rateInfo.useRestrictYN.ToString() + CRLF;
                tmp += "closeOnExpired (정액제 서비스 만료 시 해지 여부) : " + rateInfo.closeOnExpired.ToString() + CRLF;
                tmp += "unPaidYN (미수금 보유 여부) : " + rateInfo.unPaidYN.ToString() + CRLF;

                MessageBox.Show(tmp, "정액제 서비스 상태 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 상태 확인");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetBalance
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = easyFinBankService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = easyFinBankService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트 충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = easyFinBankService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = easyFinBankService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

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
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetPartnerBalance
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = easyFinBankService.GetPartnerBalance(txtCorpNum.Text);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = easyFinBankService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 팝빌 계좌조회 API 서비스 과금정보를 확인합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = easyFinBankService.GetChargeInfo(txtCorpNum.Text);

                string tmp = null;
                tmp += "unitCost (단가-월정액) : " + chrgInf.unitCost + CRLF;
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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = easyFinBankService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = easyFinBankService.CheckID(txtUserId.Text);

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
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#JoinMember
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
                Response response = easyFinBankService.JoinMember(joinInfo);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {

            try
            {
                string url = easyFinBankService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팝빌 로그인 URL 확인");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL 확인");
            }
        }

        /*
         * 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#RegistContact
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {

            Contact contactInfo = new Contact();

            // 담당자 아이디, 6자 이상 50자 미만
            contactInfo.id = "testkorea";

            // 담당자 비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
            contactInfo.Password = "asdf8536!@#";

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자명";

            // 담당자연락처 (최대 20자)
            contactInfo.tel = "";

            // 담당자 이메일 (최대 100자)
            contactInfo.email = "";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = easyFinBankService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = easyFinBankService.GetContactInfo(txtCorpNum.Text, contactID);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = easyFinBankService.ListContact(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/easyfinbank/dotnet/api#UpdateContact
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserId.Text;

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
                Response response = easyFinBankService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 수정");
            }
        }

        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#GetCorpInfo
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = easyFinBankService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://docs.popbill.com/easyfinbank/dotnet/api#UpdateCorpInfo
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
                Response response = easyFinBankService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

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
