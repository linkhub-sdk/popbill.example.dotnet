
/*
 * 팝빌 현금영수증 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2019-01-09
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 27, 30 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Popbill.Cashbill;

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

            // 연동환경 설정값, 개발용(true), 상업용(false)
            cashbillService.IsTest = true;
        }


        /*
         * 현금영수증 관리번호 중복여부를 확인합니다.
         * - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {

            try
            {
                bool InUse = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text);

                MessageBox.Show((InUse ? "사용중" : "미사용중"), "문서관리번호 중복여부 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서관리번호 중복여부 확인");
            }
        }

        /*
         * 1건의 현금영수증을 [즉시발행]합니다.
         * - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청 전송결과를 확인할 수 있습니다.
         * - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼] > 1.3. 국세청 전송정책"을 참조하시기 바랍니다.
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {

            // 메모
            String memo = "현금영수증 즉시발행 메모";

            Cashbill cashbill = new Cashbill();

            // [필수] 문서관리번호, 발행자별 고유번호 할당, 1~24자리 영문,숫자,'-','_' 조합으로 중복없이 구성.
            cashbill.mgtKey = txtMgtKey.Text;

            // [취소거래시 필수] 원본 현금영수증 국세청승인번호
            cashbill.orgConfirmNum = "";

            // [취소거래시 필수] 원본 현금영수증 거래일자
            cashbill.orgTradeDate = "";

            // [필수] 문서형태, { 승인거래, 취소거래 } 중 기재
            cashbill.tradeType = "승인거래";

            // [필수] 거래구분, { 소득공제용, 지출증빙용 } 중 기재
            cashbill.tradeUsage = "소득공제용";

            // 거래유형, { 일반, 도서공연, 대중교통 } 중 기재 
            cashbill.tradeOpt = "일반";

            // [필수] 과세형태, { 과세, 비과세 } 중 기재
            cashbill.taxationType = "과세";

            // [필수] 거래금액 ( 공급가액 + 세액 + 봉사료 ) 
            cashbill.totalAmount = "11000";

            // [필수] 공급가액
            cashbill.supplyCost = "10000";

            // [필수] 부가세 
            cashbill.tax = "1000";

            // [필수] 봉사료
            cashbill.serviceFee = "0";

            // [필수] 가맹점 사업자번호
            cashbill.franchiseCorpNum = txtCorpNum.Text;

            // 가맹점 상호
            cashbill.franchiseCorpName = "가맹점 상호";

            // 가맹점 대표자 성명
            cashbill.franchiseCEOName = "가맹점 대표자 성명";

            // 가맹점 주소
            cashbill.franchiseAddr = "가맹점 주소";

            // 가맹점 전화번호
            cashbill.franchiseTEL = "070-1234-1234";

            // [필수] 식별번호
            // 거래구분(tradeUsage) - '소득공제용' 인 경우 
            // - 주민등록/휴대폰/카드번호 기재 가능
            // 거래구분(tradeUsage) - '지출증빙용' 인 경우
            // - 사업자번호/주민등록/휴대폰/카드번호 기재 가능 
            cashbill.identityNum = "0101112222";

            // 주문자명
            cashbill.customerName = "주문자명";

            // 주문상품명
            cashbill.itemName = "주문상품명";

            // 주문번호
            cashbill.orderNumber = "주문번호";

            // 주문자 이메일
            cashbill.email = "test@test.com";

            // 주문자 휴대폰
            cashbill.hp = "010-111-222";

            // 주문자 팩스번호
            cashbill.fax = "02-6442-9700";

            // 발행시 알림문자 전송여부
            cashbill.smssendYN = false;

            try
            {
                Response response = cashbillService.RegistIssue(txtCorpNum.Text, cashbill, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 즉시발행");
            }
        }

        /*
         * 1건의 현금영수증을 [임시저장]합니다.
         * - [임시저장] 상태의 현금영수증은 발행(Issue API)을 호출해야만 국세청에 전송됩니다.
         * - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청 전송결과를 확인할 수 있습니다.
         * - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼] > 1.3. 국세청 전송정책"을 참조하시기 바랍니다.
         */
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Cashbill cashbill = new Cashbill();

            // [필수] 문서관리번호, 사업자별로 중복되지 않도록 관리번호 할당
            // 1~24자리 영문,숫자,'-','_' 조합 구성
            cashbill.mgtKey = txtMgtKey.Text;

            // [취소거래시 필수] 원본 현금영수증 국세청승인번호
            cashbill.orgConfirmNum = "";

            // [취소거래시 필수] 원본 현금영수증 거래일자
            cashbill.orgTradeDate = "";

            // [필수] 문서형태, { 승인거래, 취소거래 } 중 기재
            cashbill.tradeType = "승인거래";

            // [필수] 거래구분, { 소득공제용, 지출증빙용 } 중 기재
            cashbill.tradeUsage = "소득공제용";

            // 거래유형, { 일반, 도서공연, 대중교통 } 중 기재
            cashbill.tradeOpt = "도서공연";

            // [필수] 과세형태, { 과세, 비과세 } 중 기재
            cashbill.taxationType = "과세";

            // [필수] 거래금액 ( 공급가액 + 세액 + 봉사료 ) 
            cashbill.totalAmount = "11000";

            // [필수] 공급가액
            cashbill.supplyCost = "10000";

            // [필수] 부가세 
            cashbill.tax = "1000";

            // [필수] 봉사료
            cashbill.serviceFee = "0";

            // [필수] 가맹점 사업자번호
            cashbill.franchiseCorpNum = txtCorpNum.Text;

            // 가맹점 상호
            cashbill.franchiseCorpName = "가맹점 상호_abcd";

            // 가맹점 대표자 성명
            cashbill.franchiseCEOName = "가맹점 대표자";

            // 가맹점 주소
            cashbill.franchiseAddr = "가맹점 주소";

            // 가맹점 전화번호
            cashbill.franchiseTEL = "070-1234-1234";

            // [필수] 식별번호
            // 거래구분(tradeUsage) - '소득공제용' 인 경우 
            // - 주민등록/휴대폰/카드번호 기재 가능
            // 거래구분(tradeUsage) - '지출증빙용' 인 경우
            // - 사업자번호/주민등록/휴대폰/카드번호 기재 가능 
            cashbill.identityNum = "0101112222";

            // 주문자명
            cashbill.customerName = "주문자명";

            // 주문상품명
            cashbill.itemName = "주문상품명";

            // 주문번호
            cashbill.orderNumber = "주문번호";

            // 주문자 이메일
            cashbill.email = "test@test.com";

            // 주문자 휴대폰
            cashbill.hp = "010-111-222";

            // 주문자 팩스번호
            cashbill.fax = "02-6442-9700";

            // 발행시 알림문자 전송여부
            cashbill.smssendYN = false;

            try
            {
                Response response = cashbillService.Register(txtCorpNum.Text, cashbill, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 임시저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 임시저장");
            }
        }

        /*
         * 1건의 현금영수증을 [수정]합니다.
         * - [임시저장] 상태의 현금영수증만 수정할 수 있습니다.
         * - 국세청에 신고된 현금영수증은 수정할 수 없으며, 취소 현금영수증을 발행하여 취소처리 할 수 있습니다.
         */
        private void Button7_Click(object sender, EventArgs e)
        {
            Cashbill cashbill = new Cashbill();

            // [필수] 문서관리번호, 사업자별로 중복되지 않도록 관리번호 할당
            // 1~24자리 영문,숫자,'-','_' 조합 구성
            cashbill.mgtKey = txtMgtKey.Text;

            // [필수] 문서형태, {승인거래, 취소거래} 중 기재
            cashbill.tradeType = "승인거래";

            // [필수] 거래구분, {소득공제용, 지출증빙용} 중 기재
            cashbill.tradeUsage = "소득공제용";

            // 거래유형, {일반, 도서공연, 대중교통} 중 기재
            cashbill.tradeOpt = "일반";

            // [필수] 과세형태, { 과세, 비과세 } 중 기재
            cashbill.taxationType = "과세";

            // [필수] 거래금액 ( 공급가액 + 세액 + 봉사료 ) 
            cashbill.totalAmount = "11000";

            // [필수] 공급가액
            cashbill.supplyCost = "10000";

            // [필수] 부가세 
            cashbill.tax = "1000";

            // [필수] 봉사료
            cashbill.serviceFee = "0";


            // [필수] 식별번호
            // 거래구분(tradeUsage) - '소득공제용' 인 경우 
            // - 주민등록/휴대폰/카드번호 기재 가능
            // 거래구분(tradeUsage) - '지출증빙용' 인 경우
            // - 사업자번호/주민등록/휴대폰/카드번호 기재 가능 
            cashbill.identityNum = "0101112222";

            // [필수] 가맹점 사업자번호
            cashbill.franchiseCorpNum = txtCorpNum.Text;

            // 가맹점 상호
            cashbill.franchiseCorpName = "가맹점 상호_수정";

            // 가맹점 대표자 성명
            cashbill.franchiseCEOName = "가맹점 대표자_수정";

            // 가맹점 주소
            cashbill.franchiseAddr = "가맹점 주소";

            // 가맹점 전화번호 
            cashbill.franchiseTEL = "070-1234-1234";


            // 주문자명
            cashbill.customerName = "주문자명";

            // 주문상품명
            cashbill.itemName = "주문상품명";

            // 주문번호
            cashbill.orderNumber = "주문번호";

            // 주문자 이메일
            cashbill.email = "test@test.com";

            // 주문자 휴대폰
            cashbill.hp = "010-111-222";

            // 주문자 팩스번호
            cashbill.fax = "070-111-222";

            // 발행시 알림문자 전송여부
            cashbill.smssendYN = false;

            try
            {
                Response response = cashbillService.Update(txtCorpNum.Text, txtMgtKey.Text, cashbill, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 수정");
            }
        }

        /*
         * 1건의 [임시저장] 현금영수증을 [발행]합니다.
         * - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청 전송결과를 확인할 수 있습니다.
         * - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼] > 1.3. 국세청 전송정책"을 참조하시기 바랍니다.
         */
        private void btnIssue_Click(object sender, EventArgs e)
        {
            // 메모
            string memo = "발행 메모";

            try
            {
                Response response = cashbillService.Issue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 발행");
            }
        }

        /*
         * [발행완료] 상태의 현금영수증을 [발행취소]합니다.
         * - 발행취소는 국세청 전송전에만 가능합니다.
         * - 발행취소된 현금영수증은 국세청에 전송되지 않습니다.
         */
        private void btnCancelIssue_Click(object sender, EventArgs e)
        {
            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행취소");
            }
        }

        /*
         * [발행완료] 상태의 현금영수증을 [발행취소]합니다.
         * - 발행취소는 국세청 전송전에만 가능합니다.
         * - 발행취소된 현금영수증은 국세청에 전송되지 않습니다.
         */
        private void btnCancelIssueSub_Click(object sender, EventArgs e)
        {
            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "현금영수증 발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 발행취소");
            }
        }

        /*
         * [발행완료] 상태의 현금영수증을 [발행취소]합니다.
         * - 발행취소는 국세청 전송전에만 가능합니다.
         * - 발행취소된 현금영수증은 국세청에 전송되지 않습니다.
         */
        private void btnCancelIssue02_Click(object sender, EventArgs e)
        {
            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "취소현금영수증 발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "취소현금영수증 발행취소");
            }
        }


        /*
         * 1건의 현금영수증을 [삭제]합니다.
         * - 현금영수증을 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소]
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
         * 1건의 현금영수증을 [삭제]합니다.
         * - 현금영수증을 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소]
         */
        private void btnDeleteSub_Click(object sender, EventArgs e)
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
         * 1건의 현금영수증을 [삭제]합니다.
         * - 현금영수증을 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소]
         */
        private void btnDelete02_Click(object sender, EventArgs e)
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
         * 1건의 취소현금영수증을 [즉시발행]합니다.
         * - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청 전송결과를 확인할 수 있습니다.
         * - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼] > 1.3. 국세청 전송정책"을 참조하시기 바랍니다.
         * - 취소현금영수증 작성방법 안내 - http://blog.linkhub.co.kr/702
         */
        private void btnRevokRegistIssue_Click(object sender, EventArgs e)
        {

            // 원본현금영수증 국세청승인번호
            String orgConfirmNum = "820116333";

            // 원본현금영수증 거래일자
            String orgTradeDate = "20170711";

            try
            {
                Response response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text, orgConfirmNum, orgTradeDate);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "취소현금영수증 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "취소현금영수증 즉시발행");
            }
        }

        /*
         * 1건의 (부분)취소현금영수증을 [즉시발행]합니다.
         * - 발행일 기준 오후 5시 이전에 발행된 현금영수증은 다음날 오후 2시에 국세청 전송결과를 확인할 수 있습니다.
         * - 현금영수증 국세청 전송 정책에 대한 정보는 "[현금영수증 API 연동매뉴얼] > 1.3. 국세청 전송정책"을 참조하시기 바랍니다.
         */
        private void btnRevokeRegistIssue_part_Click(object sender, EventArgs e)
        {
            // 원본 현금영수증 국세청승인번호
            String orgConfirmNum = "820116333";

            // 원본현금영수증 거래일자
            String orgTradeDate = "20170711";

            // 알림문자 전송여부           
            bool smssendYN = false;

            // 메모
            String memo = "부분 취소발행 메모";

            // 부분취소여부, true-부분취소, false-전체취소
            bool isPartCancel = true;

            // 취소사유, 1-거래취소, 2-오류발급취소, 3-기타
            int cancelType = 1;

            // [취소] 공급가액
            String supplyCost = "3000";

            // [취소] 부가세
            String tax = "300";

            // [취소] 봉사료
            String serviceFee = "";

            // [취소] 합계금액
            String totalAmount = "3300";

            try
            {
                Response response = cashbillService.RevokeRegistIssue(txtCorpNum.Text, txtMgtKey.Text,
                    orgConfirmNum, orgTradeDate, smssendYN, memo, txtUserId.Text, isPartCancel, cancelType,
                    supplyCost, tax, serviceFee, totalAmount);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "(부분) 취소현금영수증 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "(부분) 취소현금영수증 즉시발행");
            }
        }

        /*
         * 1건의 현금영수증 상태/요약 정보를 확인합니다.
         * - 응답항목에 대한 자세한 정보는 "[현금영수증 API 연동매뉴얼] > 4.2. 현금영수증 상태정보 구성"을 참조하시기 바랍니다.
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {

            try
            {
                CashbillInfo cashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemKey (현금영수증 아이템키) : " + cashbillInfo.itemKey + CRLF;
                tmp += "mgtKey (문서관리번호) : " + cashbillInfo.mgtKey + CRLF;
                tmp += "tradeDate (거래일자) : " + cashbillInfo.tradeDate + CRLF;
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
         * 대량의 현금영수증 상태/요약 정보를 확인합니다. (최대 1000건)
         * - 응답항목에 대한 자세한 정보는 "[현금영수증 API 연동매뉴얼] > 4.2. 현금영수증 상태정보 구성"을 참조하시기 바랍니다.
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {

            List<string> MgtKeyList = new List<string>();

            // 현금영수증 문서관리번호 배열, 최대 1000건.
            MgtKeyList.Add("20190110-001");
            MgtKeyList.Add("20190110-002");
            MgtKeyList.Add("20190110-003");

            try
            {
                List<CashbillInfo> cashbillInfoList = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList);

                string tmp = null;

                for (int i = 0; i < cashbillInfoList.Count; i++)
                {
                    tmp += "itemKey (현금영수증 아이템키) : " + cashbillInfoList[i].itemKey + CRLF;
                    tmp += "mgtKey (문서관리번호) : " + cashbillInfoList[i].mgtKey + CRLF;
                    tmp += "tradeDate (거래일자) : " + cashbillInfoList[i].tradeDate + CRLF;
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
         * 현금영수증 1건의 상세정보를 조회합니다.
         * - 응답항목에 대한 자세한 사항은 "[현금영수증 API 연동매뉴얼] > 4.1. 현금영수증 구성" 을 참조하시기 바랍니다.
         */
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Cashbill cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = null;

                tmp += "mgtKey (문서관리번호) : " + cashbill.mgtKey + CRLF;
                tmp += "confirmNum (국세청승인번호) : " + cashbill.confirmNum + CRLF;
                tmp += "orgConfirmNum (원본 현금영수증 국세청승인번호) : " + cashbill.orgConfirmNum + CRLF;
                tmp += "orgTradeDate (원본 현금영수증 거래일자) : " + cashbill.orgTradeDate + CRLF;
                tmp += "tradeDate (거래일자) : " + cashbill.tradeDate + CRLF;
                tmp += "tradeType (문서형태) : " + cashbill.tradeType + CRLF;
                tmp += "tradeUsage (거래구분) : " + cashbill.tradeUsage + CRLF;
                tmp += "tradeOpt (거래유형) : " + cashbill.tradeOpt + CRLF;
                tmp += "taxationType (과세형태) : " + cashbill.taxationType + CRLF;
                tmp += "totalAmount (거래금액) : " + cashbill.totalAmount + CRLF;
                tmp += "supplyCost (공급가액) : " + cashbill.supplyCost + CRLF;
                tmp += "tax (부가세) : " + cashbill.tax + CRLF;
                tmp += "serviceFee (봉사료) : " + cashbill.serviceFee + CRLF;
                tmp += "franchiseCorpNum (가맹점 사업자번호) : " + cashbill.franchiseCorpNum + CRLF;
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
                tmp += "cancelType (취소사유) : " + cashbill.cancelType + CRLF;
                tmp += "smssendYN (알림문자 전송여부) : " + cashbill.smssendYN + CRLF;

                MessageBox.Show(tmp, "현금영수증 상세정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 상세정보 조회");
            }
        }

        /*
         * 검색조건을 사용하여 현금영수증 목록을 조회합니다.
         * - 응답항목에 대한 자세한 사항은 "[현금영수증 API 연동매뉴얼] > 4.2. 현금영수증 상태정보 구성" 을 참조하시기 바랍니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 검색일자 유형, R-등록일자, T-거래일자, I-발행일자
            String DType = "T";

            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20181201";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20190109";

            // 상태코드 배열, 2,3번째 자리에 와일드카드(*) 사용가능
            // - 상태코드에 대한 자세한 사항은 "[현금영수증 API 연동매뉴얼] >
            //   5.1. 현금영수증 상태코드"를 참고하시기 바랍니다.
            String[] State = new String[2];
            State[0] = "3**";
            State[1] = "4**";

            // 문서형태 배열, N-일반 현금영수증, C-취소 현금영수증
            String[] TradeType = new String[2];
            TradeType[0] = "N";
            TradeType[1] = "C";

            // 거래구분 배열, P-소득공제용, C-지출증빙용
            String[] TradeUsage = new String[2];
            TradeUsage[0] = "P";
            TradeUsage[1] = "C";

            // 거래유형 배열, N-일반, B-도서공연, T-대중교통
            String[] TradeOpt = new String[3];
            TradeOpt[0] = "N";
            TradeOpt[1] = "B";
            TradeOpt[2] = "T";

            // 과세형태 배열, T-과세, N-비과세 
            String[] TaxationType = new String[2];
            TaxationType[0] = "T";
            TaxationType[1] = "N";

            // 식별번호 조회, 미기재시 전체조회 
            String QString = "";

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000개 
            int PerPage = 30;

            try
            {

                CBSearchResult searchResult = cashbillService.Search(txtCorpNum.Text, DType, SDate, EDate, State, TradeType,
                                            TradeUsage, TradeOpt, TaxationType, QString, Order, Page, PerPage);

                String tmp = null;

                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;

                tmp += "itemKey | mgtKey | tradeDate | tradeType | tradeUsage | tradeOpt | taxationType | totalAmount | issueDT | regDT | stateMemo | stateCode | stateDT | ";
                tmp += "identityNum | itemName | customerName | ";
                tmp += "confirmNum | orgConfirmNum | orgTradeDate | ntssendDT | ntsresultDT | ntsresultCode | ntsresultMessage | ";
                tmp += "printYN" + CRLF;

                foreach (CashbillInfo cashbillInfo in searchResult.list)
                {
                    tmp += cashbillInfo.itemKey + " | ";
                    tmp += cashbillInfo.mgtKey + " | ";
                    tmp += cashbillInfo.tradeDate + " | ";
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
         * 현금영수증 상태 변경이력을 확인합니다.
         * - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
         *   "[현금영수증 API 연동매뉴얼] > 3.3.5 상태 변경이력 확인" 을 참조하시기 바랍니다.
         */
        private void btnGetLogs_Click(object sender, EventArgs e)
        {

            try
            {
                List<CashbillLog> logList = cashbillService.GetLogs(txtCorpNum.Text, txtMgtKey.Text);

                String tmp = "";

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
         * 팝빌 현금영수증 임시문서함 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX");

                MessageBox.Show(url, "팝빌 현금영수증 임시문서함 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 현금영수증 임시문서함 팝업 URL");
            }
        }

        /*
         * 팝빌 현금영수증 발행문서함 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

                MessageBox.Show(url, "팝빌 현금영수증 발행문서함 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 현금영수증 발행문서함 팝업 URL");
            }
        }

        /*
         * 팝빌 현금영수증 매출문서작성 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_WRITE_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE");

                MessageBox.Show(url, "팝빌 현금영수증 작성 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 현금영수증 작성 팝업 URL");
            }
        }

        /*
         * 1건의 현금영수증 보기 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 보기 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 보기 팝업 URL");
            }
        }

        /*
         * 1건의 현금영수증 인쇄 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 인쇄 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL");
            }
        }

        /*
         * 1건의 현금영수증 인쇄 팝업 URL을 반환합니다. (공급받는자용)
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다
         */
        private void btnEPrintURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 인쇄 팝업 URL - 공급받는자용");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL - 공급받는자용");
            }
        }

        /*
         * 대량의 현금영수증 인쇄 팝업 URL을 반환합니다. (최대100건)
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {

            List<string> MgtKeyList = new List<string>();

            // 현금영수증 문서관리번호 배열, 최대 100건.
            MgtKeyList.Add("20190110-001");
            MgtKeyList.Add("20190110-002");
            MgtKeyList.Add("20190110-003");
            MgtKeyList.Add("20190110-004");
            MgtKeyList.Add("20190110-005");
            MgtKeyList.Add("20190110-006");

            try
            {
                string url = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text);

                MessageBox.Show(url, "현금영수증 인쇄 팝업 URL - 대량");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "현금영수증 인쇄 팝업 URL - 대량");
            }
        }

        /*
         * 현금영수증 메일링크 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다. 
         */
        private void btnGetEmailURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetMailURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "메일링크 URL 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "메일링크 URL 확인");
            }
        }


        /*
         * 팝빌에 로그인 상태로 접근할 수 있는 팝업 URL을 반환합니다.
         * - URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }


        /*
         * 발행 안내메일을 재전송합니다.
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {

            // 수신메일주소
            string receiveEmail = "test@test.com";

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
         * 알림문자를 전송합니다. (단문/SMS - 한글 최대 45자)
         * - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [문자] > [전송내역] 탭에서 전송결과를 확인할 수 있습니다.
         */
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            // 발신번호
            string sendNum = "070-4304-2991";

            // 수신번호
            string receiveNum = "010-111-222";


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
         * 알림문자를 전송합니다. (단문/SMS - 한글 최대 45자)
         * - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [문자] > [전송내역] 탭에서 전송결과를 확인할 수 있습니다.
         */
        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            // 발신번호
            string sendNum = "070-4304-2991";

            // 수신팩스번호
            string receiverNum = "070-111-222";

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
         * 현금영수증 관련 메일전송 항목에 대한 전송여부를 목록을 반환합니다.
         */
        private void btnListEmailConfig_Click(object sender, EventArgs e)
        {
            String tmp = "";
            try
            {
                List<EmailConfig> resultList = cashbillService.ListEmailConfig(txtCorpNum.Text, txtUserId.Text);

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
         * 현금영수증 관련 메일전송 항목에 대한 전송여부를 수정합니다.
         *
         * 메일전송유형
         * CSH_ISSUE : 고객에게 현금영수증이 발행 되었음을 알려주는 메일 입니다.
         * CSH_CANCEL : 고객에게 현금영수증이 발행취소 되었음을 알려주는 메일 입니다.
         */
        private void btnUpdateEmailConfig_Click(object sender, EventArgs e)
        {
            String EmailType = "CSH_ISSUE";

            //전송여부 (True-전송, False-미전송)
            bool SendYN = true;

            try
            {
                Response response = cashbillService.UpdateEmailConfig(txtCorpNum.Text, EmailType, SendYN, txtUserId.Text);

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
         * 연동회원의 잔여포인트를 조회합니다.
         * - 파트너 과금 방식의 경우 파트너 잔여포인트 조회(GetPartnerBalance API) 기능을 사용하시기 바랍니다.
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
         * 팝빌 연동회원 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트충전 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트충전 팝업 URL");
            }
        }

        /*
         * 파트너 잔여포인트를 확인합니다.
         * - 연동과금 방식의 경우 연동회원 잔여포인트 조회 (GetBalance API)를 이용하시기 바랍니다. 
         */
        private void btnGetPartnerBalance1_Click(object sender, EventArgs e)
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
         * 파트너 포인트 충전 팝업 URL을 반환합니다. 
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 현금영수증 발행단가를 확인합니다.
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
         * 현금영수증 API 서비스 과금정보를 확인합니다.
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
         * 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
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
         * 팝빌 회원아이디 중복여부를 확인합니다.
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
         * 파트너의 연동회원으로 신규가입 처리합니다.
         */
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
         * 연동회원의 회사정보를 조회합니다.
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
         * 회사정보를 수정합니다.
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
         * 연동회원의 담당자를 추가합니다.
         */
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
         * 연동회원의 담당자 정보 목록을 확인합니다.
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

        /*
         * 담당자 정보를 수정합니다.
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserId.Text;

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

    }
}
