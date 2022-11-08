/*
 * 팝빌 현금영수증 API DotNet SDK Example
 *
 * - DotNet C# SDK 연동환경 설정방법 안내 : [개발가이드] - https://docs.popbill.com/cashbill/tutorial/dotnet_csharp
 * - 업데이트 일자 : 2022-11-08
 * - 연동 기술지원 연락처 : 1600-9854
 * - 연동 기술지원 이메일 : code@linkhubcorp.com
 *
 * <테스트 연동개발 준비사항>
 * 1) 27, 30 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Popbill.Cashbill.Example.csharp
{

    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private CashbillService cashbillService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 현금영수증 모듈 초기화
            cashbillService = new CashbillService(LinkID, SecretKey);

            // 연동환경 설정값, true-개발용, false-상업용
            cashbillService.IsTest = true;

            // 인증토큰 발급 IP 제한 On/Off, true-사용, false-미사용, 기본값(true)
            cashbillService.IPRestrictOnOff = true;

            // 팝빌 API 서비스 고정 IP 사용여부, true-사용, false-미사용, 기본값(false)
            cashbillService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, 기본값(false)
            cashbillService.UseLocalTimeYN = false;
        }

        /*
         * 파트너가 현금영수증 관리 목적으로 할당하는 문서번호 사용여부를 확인합니다.
         * - 이미 사용 중인 문서번호는 중복 사용이 불가하고, 현금영수증이 삭제된 경우에만 문서번호의 재사용이 가능합니다.
         * - 문서번호는 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
         * - https://docs.popbill.com/cashbill/dotnet/api#CheckMgtKeyInUse
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {

            try
            {
                bool InUse = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text);

                MessageBox.Show((InUse ? "사용중" : "미사용중"), "문서번호 중복여부 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서번호 중복여부 확인");
            }
        }

        /*
         * 작성된 현금영수증 데이터를 팝빌에 저장과 동시에 발행하여 "발행완료" 상태로 처리합니다.
         * - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
         * - https://docs.popbill.com/cashbill/dotnet/api#RegistIssue
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {

            // 메모
            string memo = "현금영수증 즉시발행 메모";

            // 안내메일 제목, 공백처리시 기본양식으로 전송
            string emailSubject = "메일제목 테스트";

            Cashbill cashbill = new Cashbill();

            // 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            cashbill.mgtKey = txtMgtKey.Text;

            // 문서형태, { 승인거래, 취소거래 } 중 기재
            cashbill.tradeType = "승인거래";

            // 거래구분, { 소득공제용, 지출증빙용 } 중 기재
            cashbill.tradeUsage = "소득공제용";

            // 거래유형, {일반, 도서공연, 대중교통} 중 기재
            // - 미입력시 기본값 "일반" 처리
            cashbill.tradeOpt = "일반";

            // 과세형태, { 과세, 비과세 } 중 기재
            cashbill.taxationType = "과세";

            // 거래금액
            cashbill.totalAmount = "11000";

            // 공급가액
            cashbill.supplyCost = "10000";

            // 부가세
            cashbill.tax = "1000";

            // 봉사료
            cashbill.serviceFee = "0";

            // 가맹점 사업자번호
            cashbill.franchiseCorpNum = txtCorpNum.Text;

            // 가맹점 종사업장 식별번호
            cashbill.franchiseTaxRegID = "";

            // 가맹점 상호
            cashbill.franchiseCorpName = "가맹점 상호";

            // 가맹점 대표자 성명
            cashbill.franchiseCEOName = "가맹점 대표자 성명";

            // 가맹점 주소
            cashbill.franchiseAddr = "가맹점 주소";

            // 가맹점 전화번호
            cashbill.franchiseTEL = "";

            // 식별번호, 거래구분에 따라 작성
            // └ 소득공제용 - 주민등록/휴대폰/카드번호(현금영수증 카드)/자진발급용 번호(010-000-1234) 기재가능
            // └ 지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호(현금영수증 카드) 기재가능
            // └ 주민등록번호 13자리, 휴대폰번호 10~11자리, 카드번호 13~19자리, 사업자번호 10자리 입력 가능
            cashbill.identityNum = "01012341324";

            // 주문자명
            cashbill.customerName = "주문자명";

            // 주문상품명
            cashbill.itemName = "주문상품명";

            // 주문번호
            cashbill.orderNumber = "주문번호";

            // 주문자 이메일
            // 팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            cashbill.email = "";

            // 발행시 알림문자 전송여부
            // 미입력시 기본값 False 처리
            cashbill.smssendYN = false;

            // 주문자 휴대폰
            // - {smssendYN} 의 값이 true 인 경우 아래 휴대폰번호로 안내 문자 전송
            cashbill.hp = "";

            // 거래일시, 날짜(yyyyMMddHHmmss)
            // 당일, 전일만 가능
            cashbill.tradeDT = "20221108000000";

            try
            {
                CBIssueResponse response = cashbillService.RegistIssue(txtCorpNum.Text, cashbill, memo, txtUserId.Text, emailSubject);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message + "\r\n" +
                                "국세청 승인번호(confirmNum) : " + response.confirmNum + "\r\n" +
                                "거래일자(tradeDate) : " + response.tradeDate + "\r\n" +
                                "거래일시(tradeDT) : " + response.tradeDT, "현금영수증 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 즉시발행");
            }
        }


        /*
         * 최대 100건의 현금영수증 발행을 한번의 요청으로 접수합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#BulkSubmit
         */
        private void btnBulkSubmit_Click(object sender, EventArgs e)
        {
            // 현금영수증 객체정보 목록
            List<Cashbill> cashbillList = new List<Cashbill>();

            for (int i = 0; i < 100; i++)
            {
                Cashbill cashbill = new Cashbill();

                // 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
                cashbill.mgtKey = txtSubmitID.Text + "-" + i;

                // // [취소거래시 필수] 원본 현금영수증 국세청승인번호
                // cashbill.orgConfirmNum = "";
                //
                // // [취소거래시 필수] 원본 현금영수증 거래일자
                // cashbill.orgTradeDate = "";

                // 문서형태, { 승인거래, 취소거래 } 중 기재
                cashbill.tradeType = "승인거래";

                // 거래구분, { 소득공제용, 지출증빙용 } 중 기재
                cashbill.tradeUsage = "소득공제용";

                // 거래유형, {일반, 도서공연, 대중교통} 중 기재
                // - 미입력시 기본값 "일반" 처리
                cashbill.tradeOpt = "일반";

                // 과세형태, { 과세, 비과세 } 중 기재
                cashbill.taxationType = "과세";

                // 거래금액 ( 공급가액 + 세액 + 봉사료 )
                cashbill.totalAmount = "11000";

                // 공급가액
                cashbill.supplyCost = "10000";

                // 부가세
                cashbill.tax = "1000";

                // 봉사료
                cashbill.serviceFee = "0";

                // 가맹점 사업자번호
                cashbill.franchiseCorpNum = txtCorpNum.Text;

                // 가맹점 종사업장 식별번호
                cashbill.franchiseTaxRegID = "";

                // 가맹점 상호
                cashbill.franchiseCorpName = "가맹점 상호";

                // 가맹점 대표자 성명
                cashbill.franchiseCEOName = "가맹점 대표자 성명";

                // 가맹점 주소
                cashbill.franchiseAddr = "가맹점 주소";

                // 가맹점 전화번호
                cashbill.franchiseTEL = "";

                // 식별번호, 거래구분에 따라 작성
                // └ 소득공제용 - 주민등록/휴대폰/카드번호(현금영수증 카드)/자진발급용 번호(010-000-1234) 기재가능
                // └ 지출증빙용 - 사업자번호/주민등록/휴대폰/카드번호(현금영수증 카드) 기재가능
                // └ 주민등록번호 13자리, 휴대폰번호 10~11자리, 카드번호 13~19자리, 사업자번호 10자리 입력 가능
                cashbill.identityNum = "0101112222";

                // 주문자명
                cashbill.customerName = "주문자명";

                // 주문상품명
                cashbill.itemName = "주문상품명";

                // 주문번호
                cashbill.orderNumber = "주문번호";

                // 주문자 이메일
                // 팝빌 개발환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
                // 실제 거래처의 메일주소가 기재되지 않도록 주의
                cashbill.email = "";

                // 발행시 알림문자 전송여부
                // 미입력시 기본값 False 처리
                cashbill.smssendYN = false;

                // 주문자 휴대폰
                // - {smssendYN} 의 값이 true 인 경우 아래 휴대폰번호로 안내 문자 전송
                cashbill.hp = "";

                // 거래일시, 날짜(yyyyMMddHHmmss)
                // 당일, 전일만 가능
                cashbill.tradeDT = "20221108000000";

                cashbillList.Add(cashbill);
            }

            try
            {
                BulkResponse response = cashbillService.BulkSubmit(txtCorpNum.Text, txtSubmitID.Text, cashbillList);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message + "\r\n" +
                                "접수아이디(receiptID) : " + response.receiptID, "초대량 발행 접수");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "초대량 발행 접수");
            }
        }

        /*
         * 접수시 기재한 SubmitID를 사용하여 현금영수증 접수결과를 확인합니다.
         * - 개별 현금영수증 처리상태는 접수상태(txState)가 완료(2) 시 반환됩니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetBulkResult
         */
        private void btnGetBulkResult_Click(object sender, EventArgs e)
        {
            try
            {
                BulkCashbillResult result = cashbillService.GetBulkResult(txtCorpNum.Text, txtSubmitID.Text);

                string tmp = null;

                tmp += "응답 코드(code) : " + result.code + CRLF;
                tmp += "응답메시지(message) : " + result.message + CRLF;
                tmp += "제출아이디(submitID) : " + result.submitID + CRLF;
                tmp += "현금영수증 접수 건수(submitCount) : " + result.submitCount + CRLF;
                tmp += "현금영수증 발행 성공 건수(successCount) : " + result.successCount + CRLF;
                tmp += "현금영수증 발행 실패 건수(failCount) : " + result.failCount + CRLF;
                tmp += "접수상태코드(txState) : " + result.txState + CRLF;
                tmp += "접수 결과코드(txResultCode) : " + result.txResultCode + CRLF;
                tmp += "발행처리 시작일시(txStartDT) : " + result.txStartDT + CRLF;
                tmp += "발행처리 완료일시(txEndDT) : " + result.txEndDT + CRLF;
                tmp += "접수일시(receiptDT) : " + result.receiptDT + CRLF;
                tmp += "접수아이디(receiptID) : " + result.receiptID + CRLF;

                if (result.issueResult != null)
                {
                    int i = 1;
                    foreach (BulkCashbillIssueResult issueResult in result.issueResult)
                    {
                        tmp += "===========발행결과[" + i.ToString() + "/" + result.issueResult.Count + "]===========" + CRLF;
                        tmp += "응답코드(code) : " + issueResult.code + CRLF;
                        tmp += "문서번호(MgtKey) : " + issueResult.mgtKey + CRLF;
                        tmp += "국세청승인번호(confirmNum) : " + issueResult.confirmNum + CRLF;
                        tmp += "거래일자(tradeDate) : " + issueResult.tradeDate + CRLF;
                        tmp += "거래일시(tradeDT) : " + issueResult.tradeDT + CRLF;
                        i++;
                    }
                }
                MessageBox.Show(tmp, "초대량 접수결과 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "초대량 접수결과 확인");
            }
        }

        /*
         * 삭제 가능한 상태의 현금영수증을 삭제합니다.
         * - 삭제 가능한 상태: "전송실패"
         * - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#Delete
         */
        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                Response response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 삭제");
            }
        }

        /*
         * 삭제 가능한 상태의 현금영수증을 삭제합니다.
         * - 삭제 가능한 상태: "전송실패"
         * - 현금영수증을 삭제하면 사용된 문서번호(mgtKey)를 재사용할 수 있습니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#Delete
         */
        private void btnRevokeDelete_Click(object sender, EventArgs e)
        {

            try
            {
                Response response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "취소현금영수증 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "취소현금영수증 삭제");
            }
        }

        /*
         * 취소 현금영수증 데이터를 팝빌에 저장과 동시에 발행하여 "발행완료" 상태로 처리합니다.
         * - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
         * - https://docs.popbill.com/cashbill/dotnet/api#RevokeRegistIssue
         */
        private void btnRevokRegistIssue_Click(object sender, EventArgs e)
        {

            // 원본현금영수증 국세청승인번호
            string orgConfirmNum = "TB0000001";

            // 원본현금영수증 거래일자
            string orgTradeDate = "20220501";

            try
            {
                CBIssueResponse response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message + "\r\n" +
                                "국세청 승인번호(confirmNum) : " + response.confirmNum + "\r\n" +
                                "거래일자(tradeDate) : " + response.tradeDate + "\r\n" +
                                "거래일시(tradeDT) : " + response.tradeDT, "취소현금영수증 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "취소현금영수증 즉시발행");
            }
        }

        /*
         * 작성된 (부분)취소 현금영수증 데이터를 팝빌에 저장과 동시에 발행하여 "발행완료" 상태로 처리합니다.
         * - 취소 현금영수증의 금액은 원본 금액을 넘을 수 없습니다.
         * - 현금영수증 국세청 전송 정책 : https://docs.popbill.com/cashbill/ntsSendPolicy?lang=dotnet
         * - https://docs.popbill.com/cashbill/dotnet/api#RevokeRegistIssue
         */
        private void btnRevokeRegistIssue_part_Click(object sender, EventArgs e)
        {
            // 원본 현금영수증 국세청승인번호
            string orgConfirmNum = "TB0000015";

            // 원본현금영수증 거래일자
            string orgTradeDate = "20220501";

            // 안내 문자 전송여부 , true / false 중 택 1
            // └ true = 전송 , false = 미전송
            // └ 원본 현금영수증의 구매자(고객)의 휴대폰번호 문자 전송
            bool smssendYN = false;

            // 메모
            string memo = "부분 취소발행 메모";

            // 현금영수증 취소유형 - true 기재
            bool isPartCancel = true;

            // 취소사유 , 1 / 2 / 3 중 택 1
            // └ 1 = 거래취소 , 2 = 오류발급취소 , 3 = 기타
            // └ 미입력시 기본값 1 처리
            int cancelType = 1;

            // [취소] 공급가액
            // - 취소할 공급가액 입력
            string supplyCost = "3000";

            // [취소] 부가세
            // - 취소할 부가세 입력
            string tax = "300";

            // [취소] 봉사료
            // - 취소할 봉사료 입력
            string serviceFee = "";

            // [취소] 거래금액 (공급가액+부가세+봉사료)
            // - 취소할 거래금액 입력
            string totalAmount = "3300";

            // 안내메일 제목, 공백처리시 기본양식으로 전송
            string emailSubject = "메일제목 테스트";

            // 거래일시, 날짜(yyyyMMddHHmmss)
            // 당일, 전일만 가능
            string tradeDT = "20221108000000";

            try
            {
                CBIssueResponse response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text,
                    orgConfirmNum, orgTradeDate, smssendYN, memo, txtUserId.Text, isPartCancel, cancelType,
                    supplyCost, tax, serviceFee, totalAmount, emailSubject, tradeDT);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message + "\r\n" +
                                "국세청 승인번호(confirmNum) : " + response.confirmNum + "\r\n" +
                                "거래일자(tradeDate) : " + response.tradeDate + "\r\n" +
                                "거래일시(tradeDT) : " + response.tradeDT , "(부분) 취소현금영수증 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "(부분) 취소현금영수증 즉시발행");
            }
        }

        /*
         * 현금영수증 1건의 상태 및 요약정보를 확인합니다.
         * - 리턴값 'CashbillInfo'의 변수 'stateCode'를 통해 현금영수증의 상태코드를 확인합니다.
         * - 현금영수증 상태코드 [https://docs.popbill.com/cashbill/stateCode?lang=dotnet]
         * - https://docs.popbill.com/cashbill/dotnet/api#GetInfo
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {

            try
            {
                CashbillInfo cashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemKey (팝빌번호) : " + cashbillInfo.itemKey + CRLF;
                tmp += "mgtKey (문서번호) : " + cashbillInfo.mgtKey + CRLF;
                tmp += "tradeDate (거래일자) : " + cashbillInfo.tradeDate + CRLF;
                tmp += "tradeDT (거래일시) : " + cashbillInfo.tradeDT + CRLF;
                tmp += "tradeType (문서형태) : " + cashbillInfo.tradeType + CRLF;
                tmp += "tradeUsage (거래구분) : " + cashbillInfo.tradeUsage + CRLF;
                tmp += "tradeOpt (거래유형) : " + cashbillInfo.tradeOpt + CRLF;
                tmp += "taxationType (과세형태) : " + cashbillInfo.taxationType + CRLF;
                tmp += "totalAmount (거래금액) : " + cashbillInfo.totalAmount + CRLF;
                tmp += "issueDT (발행일시) : " + cashbillInfo.issueDT + CRLF;
                tmp += "regDT (등록일시) : " + cashbillInfo.regDT + CRLF;
                tmp += "stateMemo (상태메모) : " + cashbillInfo.stateMemo + CRLF;
                tmp += "stateCode (상태코드) : " + cashbillInfo.stateCode + CRLF;
                tmp += "stateDT (상태변경일시) : " + cashbillInfo.stateDT + CRLF;
                tmp += "identityNum (식별번호) : " + cashbillInfo.identityNum + CRLF;
                tmp += "itemName (주문상품명) : " + cashbillInfo.itemName + CRLF;
                tmp += "customerName (주문자명) : " + cashbillInfo.customerName + CRLF;
                tmp += "confirmNum (국세청승인번호) : " + cashbillInfo.confirmNum + CRLF;
                tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cashbillInfo.orgConfirmNum + CRLF;
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cashbillInfo.orgTradeDate + CRLF;
                tmp += "ntssendDT (국세청 전송일시) : " + cashbillInfo.ntssendDT + CRLF;
                tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cashbillInfo.ntsresultDT + CRLF;
                tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cashbillInfo.ntsresultCode + CRLF;
                tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cashbillInfo.ntsresultMessage + CRLF;
                tmp += "printYN (인쇄여부) : " + cashbillInfo.printYN + CRLF;

                MessageBox.Show(tmp, "현금영수증 상태/요약 정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 상태/요약 정보 확인");
            }
        }

        /*
         * 다수건의 현금영수증 상태 및 요약 정보를 확인합니다. (1회 호출 시 최대 1,000건 확인 가능)
         * - 리턴값 'CashbillInfo'의 변수 'stateCode'를 통해 현금영수증의 상태코드를 확인합니다.
         * - 현금영수증 상태코드 [https://docs.popbill.com/cashbill/stateCode?lang=dotnet]
         * - https://docs.popbill.com/cashbill/dotnet/api#GetInfos
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {

            List<string> MgtKeyList = new List<string>();

            // 현금영수증 문서번호 배열, 최대 1000건.
            MgtKeyList.Add("20220504-001");
            MgtKeyList.Add("20220504-002");

            try
            {
                List<CashbillInfo> cashbillInfoList = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList);

                string tmp = null;

                for (int i = 0; i < cashbillInfoList.Count; i++)
                {
                    tmp += "itemKey (팝빌번호) : " + cashbillInfoList[i].itemKey + CRLF;
                    tmp += "mgtKey (문서번호) : " + cashbillInfoList[i].mgtKey + CRLF;
                    tmp += "tradeDate (거래일자) : " + cashbillInfoList[i].tradeDate + CRLF;
                    tmp += "tradeDT (거래일시) : " + cashbillInfoList[i].tradeDT + CRLF;
                    tmp += "tradeType (문서형태) : " + cashbillInfoList[i].tradeType + CRLF;
                    tmp += "tradeUsage (거래구분) : " + cashbillInfoList[i].tradeUsage + CRLF;
                    tmp += "tradeOpt (거래유형) : " + cashbillInfoList[i].tradeOpt + CRLF;
                    tmp += "taxationType (과세형태) : " + cashbillInfoList[i].taxationType + CRLF;
                    tmp += "totalAmount (거래금액) : " + cashbillInfoList[i].totalAmount + CRLF;
                    tmp += "issueDT (발행일시) : " + cashbillInfoList[i].issueDT + CRLF;
                    tmp += "regDT (등록일시) : " + cashbillInfoList[i].regDT + CRLF;
                    tmp += "stateMemo (상태메모) : " + cashbillInfoList[i].stateMemo + CRLF;
                    tmp += "stateCode (상태코드) : " + cashbillInfoList[i].stateCode + CRLF;
                    tmp += "stateDT (상태변경일시) : " + cashbillInfoList[i].stateDT + CRLF;
                    tmp += "identityNum (식별번호) : " + cashbillInfoList[i].identityNum + CRLF;
                    tmp += "itemName (주문상품명) : " + cashbillInfoList[i].itemName + CRLF;
                    tmp += "customerName (주문자명) : " + cashbillInfoList[i].customerName + CRLF;
                    tmp += "confirmNum (국세청승인번호) : " + cashbillInfoList[i].confirmNum + CRLF;
                    tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cashbillInfoList[i].orgConfirmNum + CRLF;
                    tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cashbillInfoList[i].orgTradeDate + CRLF;
                    tmp += "ntssendDT (국세청 전송일시) : " + cashbillInfoList[i].ntssendDT + CRLF;
                    tmp += "ntsresultDT (국세청 처리결과 수신일시) : " + cashbillInfoList[i].ntsresultDT + CRLF;
                    tmp += "ntsresultCode (국세청 처리결과 상태코드) : " + cashbillInfoList[i].ntsresultCode + CRLF;
                    tmp += "ntsresultMessage (국세청 처리결과 메시지) : " + cashbillInfoList[i].ntsresultMessage + CRLF;
                    tmp += "printYN (인쇄여부) : " + cashbillInfoList[i].printYN + CRLF + CRLF;
                }

                MessageBox.Show(tmp, "현금영수증 상태/요약 정보 조회 - 대량");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 상태/요약 정보 조회 - 대량");
            }

        }

        /*
         * 현금영수증 1건의 상세정보를 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetDetailInfo
         *
         */
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Cashbill cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = null;

                tmp += "mgtKey (문서번호) : " + cashbill.mgtKey + CRLF;
                tmp += "confirmNum (국세청승인번호) : " + cashbill.confirmNum + CRLF;
                tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cashbill.orgConfirmNum + CRLF;
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cashbill.orgTradeDate + CRLF;
                tmp += "tradeDate (거래일자) : " + cashbill.tradeDate + CRLF;
                tmp += "tradeDT (거래일시) : " + cashbill.tradeDT + CRLF;
                tmp += "tradeType (문서형태) : " + cashbill.tradeType + CRLF;
                tmp += "tradeUsage (거래구분) : " + cashbill.tradeUsage + CRLF;
                tmp += "tradeOpt (거래유형) : " + cashbill.tradeOpt + CRLF;
                tmp += "taxationType (과세형태) : " + cashbill.taxationType + CRLF;
                tmp += "totalAmount (거래금액) : " + cashbill.totalAmount + CRLF;
                tmp += "supplyCost (공급가액) : " + cashbill.supplyCost + CRLF;
                tmp += "tax (부가세) : " + cashbill.tax + CRLF;
                tmp += "serviceFee (봉사료) : " + cashbill.serviceFee + CRLF;
                tmp += "franchiseCorpNum (가맹점 사업자번호) : " + cashbill.franchiseCorpNum + CRLF;
                tmp += "franchiseTaxRegID  (가맹점 종사업장 식별번호) : " + cashbill.franchiseTaxRegID + CRLF;
                tmp += "franchiseCorpName (가맹점 상호) : " + cashbill.franchiseCorpName + CRLF;
                tmp += "franchiseCEOName (가맹점 대표자 성명) : " + cashbill.franchiseCEOName + CRLF;
                tmp += "franchiseAddr (가맹점 주소) : " + cashbill.franchiseAddr + CRLF;
                tmp += "franchiseTEL (가맹점 전화번호) : " + cashbill.franchiseTEL + CRLF;
                tmp += "identityNum (식별번호) : " + cashbill.identityNum + CRLF;
                tmp += "customerName (주문자명) : " + cashbill.customerName + CRLF;
                tmp += "itemName (주문상품명) : " + cashbill.itemName + CRLF;
                tmp += "orderNumber (주문번호) : " + cashbill.orderNumber + CRLF;
                tmp += "email (이메일) : " + cashbill.email + CRLF;
                tmp += "hp (휴대폰) : " + cashbill.hp + CRLF;
                tmp += "smssendYN (알림문자 전송여부) : " + cashbill.smssendYN + CRLF;
                tmp += "cancelType (취소사유) : " + cashbill.cancelType + CRLF;

                MessageBox.Show(tmp, "현금영수증 상세정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 상세정보 조회");
            }
        }

        /*
         * 검색조건에 해당하는 현금영수증을 조회합니다. (조회기간 단위 : 최대 6개월)
         * - https://docs.popbill.com/cashbill/dotnet/api#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 일자 유형 ("R" , "T" , "I" 중 택 1)
            // └ R = 등록일자 , T = 거래일자 , I = 발행일자
            string DType = "T";

            // 시작일자, 날짜형식(yyyyMMdd)
            string SDate = "20220501";

            // 종료일자, 날짜형식(yyyyMMdd)
            string EDate = "20220531";

            // 상태코드 배열 (2,3번째 자리에 와일드카드(*) 사용 가능)
            // - 미입력시 전체조회
            string[] State = new string[2];
            State[0] = "3**";

            // 문서형태 배열 ("N" , "C" 중 선택, 다중 선택 가능)
            // - N = 일반 현금영수증 , C = 취소 현금영수증
            // - 미입력시 전체조회
            string[] TradeType = new string[2];
            TradeType[0] = "N";
            TradeType[1] = "C";

            // 거래구분 배열 ("P" , "C" 중 선택, 다중 선택 가능)
            // - P = 소득공제용 , C = 지출증빙용
            // - 미입력시 전체조회
            string[] TradeUsage = new string[2];
            TradeUsage[0] = "P";
            TradeUsage[1] = "C";

            // 거래유형 배열 ("N" , "B" , "T" 중 선택, 다중 선택 가능)
            // - N = 일반 , B = 도서공연 , T = 대중교통
            // - 미입력시 전체조회
            string[] TradeOpt = new string[3];
            TradeOpt[0] = "N";
            TradeOpt[1] = "B";
            TradeOpt[2] = "T";

            // 과세형태 배열 ("T" , "N" 중 선택, 다중 선택 가능)
            // - T = 과세 , N = 비과세
            // - 미입력시 전체조회
            string[] TaxationType = new string[2];
            TaxationType[0] = "T";
            TaxationType[1] = "N";

            // 식별번호 조회, 미기재시 전체조회
            string QString = "";

            // 정렬방향, A-오름차순, D-내림차순
            string Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000개
            int PerPage = 30;

            // 가맹점 종사업장 번호
            // └ 다수건 검색시 콤마(",")로 구분. 예) "1234,1000"
            // └ 미입력시 전제조회
            string FranchiseTaxRegID = "";

            try
            {

                CBSearchResult searchResult = cashbillService.Search(txtCorpNum.Text, DType, SDate, EDate, State, TradeType,
                                            TradeUsage, TradeOpt, TaxationType, QString, Order, Page, PerPage, FranchiseTaxRegID);

                string tmp = null;

                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF + CRLF;

                tmp += "itemKey | mgtKey | tradeDate | tradeType | tradeUsage | tradeOpt | taxationType | totalAmount | issueDT | ";
                tmp += "regDT | stateMemo | stateCode | stateDT | identityNum | itemName | customerName | ";
                tmp += "confirmNum | orgConfirmNum | orgTradeDate | ntssendDT | ntsresultDT | ntsresultCode | ntsresultMessage | ";
                tmp += "printYN" + CRLF;

                foreach (CashbillInfo cashbillInfo in searchResult.list)
                {
                    tmp += cashbillInfo.itemKey + " | ";
                    tmp += cashbillInfo.mgtKey + " | ";
                    tmp += cashbillInfo.tradeDate + " | ";
                    tmp += cashbillInfo.tradeDT + " | ";
                    tmp += cashbillInfo.tradeType + " | ";
                    tmp += cashbillInfo.tradeUsage + " | ";
                    tmp += cashbillInfo.tradeOpt + " | ";
                    tmp += cashbillInfo.taxationType + " | ";
                    tmp += cashbillInfo.totalAmount + " | ";
                    tmp += cashbillInfo.issueDT + " | ";
                    tmp += cashbillInfo.regDT + " | ";
                    tmp += cashbillInfo.stateMemo + " | ";
                    tmp += cashbillInfo.stateCode + " | ";
                    tmp += cashbillInfo.stateDT + " | ";
                    tmp += cashbillInfo.identityNum + " | ";
                    tmp += cashbillInfo.itemName + " | ";
                    tmp += cashbillInfo.customerName + " | ";
                    tmp += cashbillInfo.confirmNum + " | ";
                    tmp += cashbillInfo.orgConfirmNum + " | ";
                    tmp += cashbillInfo.orgTradeDate + " | ";
                    tmp += cashbillInfo.ntssendDT + " | ";
                    tmp += cashbillInfo.ntsresultDT + " | ";
                    tmp += cashbillInfo.ntsresultCode + " | ";
                    tmp += cashbillInfo.ntsresultMessage + " | ";
                    tmp += cashbillInfo.printYN;
                    tmp += CRLF;
                }

                MessageBox.Show(tmp, "현금영수증 목록 조회");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 목록 조회");
            }
        }

        /*
         * 현금영수증의 상태에 대한 변경이력을 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetLogs
         */
        private void btnGetLogs_Click(object sender, EventArgs e)
        {

            try
            {
                List<CashbillLog> logList = cashbillService.GetLogs(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = "";

                foreach (CashbillLog log in logList)
                {
                    tmp += log.docLogType + " | " + log.log + " | " + log.procType + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + CRLF;
                }

                MessageBox.Show(tmp, "현금영수증 상태변경 이력 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 상태변경 이력 확인");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 현금영수증 임시문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetURL
         */
        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX");

                MessageBox.Show(url, "팝빌 현금영수증 임시문서함 팝업 URL");
                textURL.Text = url;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 현금영수증 임시문서함 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 현금영수증 발행문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetURL
         */
        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

                MessageBox.Show(url, "팝빌 현금영수증 발행문서함 팝업 URL");
                textURL.Text = url;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 현금영수증 발행문서함 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 현금영수증 매출문서작성 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetURL
         */
        private void btnGetURL_WRITE_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE");

                MessageBox.Show(url, "팝빌 현금영수증 작성 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 현금영수증 작성 팝업 URL");
            }
        }

        /*
         * 현금영수증 1건의 상세 정보 페이지의 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetPopUpURL
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 보기 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 보기 팝업 URL");
            }
        }

        /*
         * 현금영수증 1건의 상세 정보 페이지(사이트 상단, 좌측 메뉴 및 버튼 제외)의 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetViewURL
         */
        private void btnGetViewURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetViewURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 보기 팝업 URL(메뉴/버튼 제외)");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 보기 팝업 URL(메뉴/버튼 제외)");
            }
        }

        /*
         * 현금영수증 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetPrintURL
         */
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 인쇄 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL");
            }
        }

        /*
         * 다수건의 현금영수증을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다. (최대 100건)
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetMassPrintURL
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {

            List<string> MgtKeyList = new List<string>();

            // 현금영수증 문서번호 배열, 최대 100건.
            MgtKeyList.Add("20220504-001");
            MgtKeyList.Add("20220504-002");

            try
            {
                string url = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 인쇄 팝업 URL - 대량");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL - 대량");
            }
        }

        /*
         * 구매자가 수신하는 현금영수증 안내 메일의 하단에 버튼 URL 주소를 반환합니다.
         * - 함수 호출로 반환 받은 URL에는 유효시간이 없습니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetMailURL
         */
        private void btnGetEmailURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetMailURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "메일링크 URL 확인");
                textURL.Text = url;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "메일링크 URL 확인");
            }
        }


        /*
         * 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

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
         * 현금영수증과 관련된 안내 메일을 재전송 합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#SendEmail
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {

            // 수신메일주소
            string receiveEmail = "";

            try
            {
                Response response = cashbillService.SendEmail(txtCorpNum.Text, txtMgtKey.Text, receiveEmail, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "발행안내메일 재전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행안내메일 재전송");
            }
        }


        /*
         * 현금영수증과 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [문자·팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
         * - 메시지는 최대 90byte까지 입력 가능하고, 초과한 내용은 자동으로 삭제되어 전송합니다. (한글 최대 45자)
         * - 함수 호출 시 포인트가 과금됩니다. (전송실패시 환불처리)
         * - https://docs.popbill.com/cashbill/dotnet/api#SendSMS
         */
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            // 발신번호
            string sendNum = "";

            // 수신번호
            string receiveNum = "";


            // 메시지 내용, 90byte 초과시 길이가 조정되어 전송됨
            string contents = "문자 메시지 내용은 90byte초과시 길이가 조정되어 전송됩니다.";
            try
            {
                Response response = cashbillService.SendSMS(txtCorpNum.Text, txtMgtKey.Text, sendNum, receiveNum, contents, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "알림문자 전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림문자 전송");
            }
        }

        /*
         * 현금영수증을 팩스로 전송하는 함수로, 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
         * - 함수 호출 시 포인트가 과금됩니다. (전송실패시 환불처리)
         * - https://docs.popbill.com/cashbill/dotnet/api#SendFAX
         */
        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            // 발신번호
            string sendNum = "";

            // 수신팩스번호
            string receiverNum = "";

            try
            {
                Response response = cashbillService.SendFAX(txtCorpNum.Text, txtMgtKey.Text, sendNum, receiverNum, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 팩스전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 팩스전송");
            }
        }

        /*
         * 팝빌 사이트를 통해 발행하였지만 문서번호가 존재하지 않는 현금영수증에 문서번호를 할당합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#AssignMgtKey
         */
        private void btnAssignMgtKey_Click(object sender, EventArgs e)
        {
            // 팝빌번호, 목록조회(Search) API의 반환항목중 ItemKey 참조
            string itemKey = "";

            // 할당할 문서번호, 숫자, 영문, '-', '_' 조합으로
            // 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            string mgtKey = "20220504-002";

            try
            {
                Response response = cashbillService.AssignMgtKey(txtCorpNum.Text, itemKey, mgtKey);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 문서번호 할당");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 문서번호 할당");
            }
        }

        /*
         * 현금영수증 관련 메일 항목에 대한 발송설정을 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#ListEmailConfig
         */
        private void btnListEmailConfig_Click(object sender, EventArgs e)
        {
            string tmp = "";
            try
            {
                List<EmailConfig> resultList = cashbillService.ListEmailConfig(txtCorpNum.Text);

                tmp = "메일전송유형 | 전송여부" + CRLF;

                foreach (EmailConfig info in resultList)
                {
                    if (info.emailType == "CSH_ISSUE") tmp += "CSH_ISSUE (고객에게 현금영수증이 발행 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "CSH_CANCEL") tmp += "CSH_CANCEL (고객에게 현금영수증이 발행취소 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
                }
                MessageBox.Show(tmp, "알림메일 전송목록 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림메일 전송목록 조회");
            }
        }

        /*
         * 현금영수증 관련 메일 항목에 대한 발송설정을 수정합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#UpdateEmailConfig
         *
         * 메일전송유형
         * CSH_ISSUE : 고객에게 현금영수증이 발행 되었음을 알려주는 메일 입니다.
         * CSH_CANCEL : 고객에게 현금영수증이 발행취소 되었음을 알려주는 메일 입니다.
         */
        private void btnUpdateEmailConfig_Click(object sender, EventArgs e)
        {
            string EmailType = "CSH_ISSUE";

            //전송여부 (True-전송, False-미전송)
            bool SendYN = true;

            try
            {
                Response response = cashbillService.UpdateEmailConfig(txtCorpNum.Text, EmailType, SendYN);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "알림메일 전송설정 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림메일 전송설정 수정");
            }
        }


        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetBalance
         *
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {

            try
            {
                double remainPoint = cashbillService.GetBalance(txtCorpNum.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트충전 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트충전 팝업 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#GetPartnerBalance
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = cashbillService.GetPartnerBalance(txtCorpNum.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPartnerURL(txtCorpNum.Text, "CHRG");

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
         * 현금영수증 발행시 과금되는 포인트 단가를 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetUnitCost
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = cashbillService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show("발행단가 : " + unitCost.ToString(), "현금영수증 발행단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 발행단가");
            }
        }


        /*
         * 팝빌 현금영수증 API 서비스 과금정보를 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = cashbillService.GetChargeInfo(txtCorpNum.Text);

                string tmp = null;
                tmp += "unitCost (발행단가) : " + chrgInf.unitCost + CRLF;
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
         * - https://docs.popbill.com/cashbill/dotnet/api#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = cashbillService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "연동회원 가입여부확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입여부 확인");

            }
        }


        /*
         * 사용하고자 하는 아이디의 중복여부를 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = cashbillService.CheckID(txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "ID 중복확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "ID 중복확인");
            }
        }


        /*
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#JoinMember
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
                Response response = cashbillService.JoinMember(joinInfo);

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
         * 연동회원의 회사정보를 확인합니다.
         * - https://docs.popbill.com/cashbill/dotnet/api#GetCorpInfo
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = cashbillService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#UpdateCorpInfo
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
                Response response = cashbillService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#RegistContact
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 50자 미만
            contactInfo.id = "testkorea";

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
                Response response = cashbillService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            string contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = cashbillService.GetContactInfo(txtCorpNum.Text, contactID, txtUserId.Text);

                string tmp = null;

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
         * - https://docs.popbill.com/cashbill/dotnet/api#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = cashbillService.ListContact(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/cashbill/dotnet/api#UpdateContact
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
                Response response = cashbillService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * 현금영수증 PDF 파일을 다운 받을 수 있는 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         */
        private void btnGetPDFURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPDFURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 PDF 다운로드 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL");
            }
        }

        /*
         * 1건의 현금영수증 인쇄 팝업 URL을 반환합니다. (공급받는자용)
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         */
        private void btnEPrintURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 인쇄 팝업 URL - 공급받는자용");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL - 공급받는자용");
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
