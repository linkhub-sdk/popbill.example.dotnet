/*
 * 팝빌 전자명세서 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2019-01-10
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
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

namespace Popbill.Statement.Example.csharp
{
    public partial class frmExample : Form
    {
        // 링크아이디
        private string LinkID = "TESTER";

        // 비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private StatementService statementService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 전자명세서 모듈 초기화
            statementService = new StatementService(LinkID, SecretKey);

            // 연동환경 설정값, true(개발용), false(상업용).
            statementService.IsTest = true;
        }

        // 명세서 종류코드 반환
        private int selectedItemCode()
        {
            int selectedItemCode = 121;
            if (cboItemCode.Text == "거래명세서") selectedItemCode = 121;
            if (cboItemCode.Text == "청구서") selectedItemCode = 122;
            if (cboItemCode.Text == "견적서") selectedItemCode = 123;
            if (cboItemCode.Text == "발주서") selectedItemCode = 124;
            if (cboItemCode.Text == "입금표") selectedItemCode = 125;
            if (cboItemCode.Text == "영수증") selectedItemCode = 126;

            return selectedItemCode;
        }

        /*
         * 전자명세서 관리번호 중복여부를 확인합니다.
         * - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                bool InUse = statementService.CheckMgtKeyInuse(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                MessageBox.Show(InUse ? "사용중" : "미사용중", "문서관리번호 중복여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서관리번호 중복여부 확인");
            }
        }

        /*
         * 1건의 전자명세서를 [즉시발행]합니다.
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {
            // 즉시발행 메모
            String memo = "즉시발행 메모";

            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20190110";

            // [필수], {영수, 청구} 중 기재 
            statement.purposeType = "영수";

            // [필수], 과세형태, {과세, 영세, 면세} 중 기재
            statement.taxType = "과세";

            // 맞춤양식코드, 기본값을 공백('')으로 처리하면 기본양식으로 처리.
            statement.formCode = txtFormCode.Text;

            // [필수] 전자명세서 양식코드
            statement.itemCode = selectedItemCode();

            // [필수] 문서관리번호, 1~24자리 숫자, 영문, '-', '_' 조합으로 사업자별로 중복되지 않도록 구성
            statement.mgtKey = txtMgtKey.Text;


            /**************************************************************************
             *                          발신자 정보                                   *
             **************************************************************************/

            // [필수] 발신자 사업자번호
            statement.senderCorpNum = txtCorpNum.Text;

            // 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            statement.senderTaxRegID = "";

            // 발신자 상호
            statement.senderCorpName = "발신자 상호";

            // 발신자 대표자 성명 
            statement.senderCEOName = "발신자 대표자 성명";

            // 발신자 주소 
            statement.senderAddr = "발신자 주소";

            // 발신자 종목
            statement.senderBizClass = "발신자 종목";

            // 발신자 업태 
            statement.senderBizType = "발신자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "발신자 담당자명";

            // 발신자 메일주소 
            statement.senderEmail = "test@test.com";

            // 발신자 연락처
            statement.senderTEL = "070-7070-0707";

            // 발신자 휴대폰번호 
            statement.senderHP = "010-000-2222";


            /**************************************************************************
             *                             수신자 정보                                *
             **************************************************************************/

            // 수신자 사업자번호
            statement.receiverCorpNum = "8888888888";

            // [필수] 수신자 상호
            statement.receiverCorpName = "수신자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "수신자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "수신자 주소";

            // 수신자 종목
            statement.receiverBizClass = "수신자 종목";

            // 수신자 업태 
            statement.receiverBizType = "수신자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "수신자 담당자명";

            // 수신자 메일주소 
            statement.receiverEmail = "test@receiver.com";

            /**************************************************************************
             *                         전자명세서 기재항목                            *
             **************************************************************************/

            // [필수] 공급가액 합계
            statement.supplyCostTotal = "200000";

            // [필수] 세액 합계
            statement.taxTotal = "20000";

            // 합계금액
            statement.totalAmount = "220000";

            // 기재상 일련번호 항목
            statement.serialNum = "123";

            // 기재상 비고 항목
            statement.remark1 = "비고1";
            statement.remark2 = "비고2";
            statement.remark3 = "비고3";

            // 사업자등록증 이미지 첨부여부
            statement.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            statement.bankBookYN = false;

            statement.detailList = new List<StatementDetail>();

            StatementDetail detail = new StatementDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000"); // 전잔액
            statement.propertyBag.Add("Deposit", "5000"); // 입금액
            statement.propertyBag.Add("CBalance", "20000"); // 현잔액

            try
            {
                Response response = statementService.RegistIssue(txtCorpNum.Text, statement, memo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 즉시발행");
            }
        }

        /*
         * 1건의 전자명세서를 [임시저장]합니다.
         */
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20190110";

            // [필수], {영수, 청구} 중 기재 
            statement.purposeType = "영수";

            // [필수], 과세형태, {과세, 영세, 면세} 중 기재
            statement.taxType = "과세";

            // 맞춤양식코드, 기본값을 공백('')으로 처리하면 기본양식으로 처리.
            statement.formCode = txtFormCode.Text;

            // [필수] 전자명세서 양식코드
            statement.itemCode = selectedItemCode();

            // [필수] 문서관리번호, 1~24자리 숫자, 영문, '-', '_' 조합으로 사업자별로 중복되지 않도록 구성
            statement.mgtKey = txtMgtKey.Text;


            /**************************************************************************
             *                              발신자 정보                               *
             **************************************************************************/

            // [필수] 발신자 사업자번호
            statement.senderCorpNum = txtCorpNum.Text;

            // 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            statement.senderTaxRegID = "";

            // 발신자 상호
            statement.senderCorpName = "발신자 상호";

            // 발신자 대표자 성명 
            statement.senderCEOName = "발신자 대표자 성명";

            // 발신자 주소 
            statement.senderAddr = "발신자 주소";

            // 발신자 종목
            statement.senderBizClass = "발신자 종목";

            // 발신자 업태 
            statement.senderBizType = "발신자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "발신자 담당자명";

            // 발신자 메일주소 
            statement.senderEmail = "test@test.com";

            // 발신자 연락처
            statement.senderTEL = "070-7070-0707";

            // 발신자 휴대폰번호 
            statement.senderHP = "010-000-2222";


            /**************************************************************************
             *                             수신자 정보                                *
             **************************************************************************/

            // 수신자 사업자번호
            statement.receiverCorpNum = "8888888888";

            // [필수] 수신자 상호
            statement.receiverCorpName = "수신자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "수신자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "수신자 주소";

            // 수신자 종목
            statement.receiverBizClass = "수신자 종목";

            // 수신자 업태 
            statement.receiverBizType = "수신자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "수신자 담당자명";

            // 수신자 메일주소 
            statement.receiverEmail = "test@receiver.com";

            /**************************************************************************
             *                         전자명세서 기재항목                            *
             **************************************************************************/

            // [필수] 공급가액 합계
            statement.supplyCostTotal = "200000";

            // [필수] 세액 합계
            statement.taxTotal = "20000";

            // 합계금액
            statement.totalAmount = "220000";

            // 기재상 일련번호 항목
            statement.serialNum = "123";

            // 기재상 비고 항목
            statement.remark1 = "비고1";
            statement.remark2 = "비고2";
            statement.remark3 = "비고3";

            // 사업자등록증 이미지 첨부여부
            statement.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            statement.bankBookYN = false;

            statement.detailList = new List<StatementDetail>();

            StatementDetail detail = new StatementDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000"); // 전잔액
            statement.propertyBag.Add("Deposit", "5000"); // 입금액
            statement.propertyBag.Add("CBalance", "20000"); // 현잔액

            try
            {
                Response response = statementService.Register(txtCorpNum.Text, statement, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 임시저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 임시저장");
            }
        }

        /*
         * 1건의 전자명세서를 [수정]합니다.
         * - [임시저장] 상태의 전자명세서만 수정할 수 있습니다.
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20190110";

            // [필수], {영수, 청구} 중 기재 
            statement.purposeType = "영수";

            // [필수], 과세형태, {과세, 영세, 면세} 중 기재
            statement.taxType = "과세";

            // 맞춤양식코드, 기본값을 공백('')으로 처리하면 기본양식으로 처리.
            statement.formCode = txtFormCode.Text;

            // [필수] 전자명세서 양식코드
            statement.itemCode = selectedItemCode();

            // [필수] 문서관리번호, 1~24자리 숫자, 영문, '-', '_' 조합으로 사업자별로 중복되지 않도록 구성
            statement.mgtKey = txtMgtKey.Text;


            /**************************************************************************
             *                          발신자 정보                                   *
             **************************************************************************/

            // [필수] 발신자 사업자번호
            statement.senderCorpNum = txtCorpNum.Text;

            // 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            statement.senderTaxRegID = "";

            // 발신자 상호
            statement.senderCorpName = "발신자 상호_수정";

            // 발신자 대표자 성명 
            statement.senderCEOName = "발신자 대표자 성명_수정";

            // 발신자 주소 
            statement.senderAddr = "발신자 주소";

            // 발신자 종목
            statement.senderBizClass = "발신자 종목";

            // 발신자 업태 
            statement.senderBizType = "발신자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "발신자 담당자명";

            // 발신자 메일주소 
            statement.senderEmail = "test@test.com";

            // 발신자 연락처
            statement.senderTEL = "070-7070-0707";

            // 발신자 휴대폰번호 
            statement.senderHP = "010-000-2222";


            /**************************************************************************
             *                             수신자 정보                                *
             **************************************************************************/

            // 수신자 사업자번호
            statement.receiverCorpNum = "8888888888";

            // [필수] 수신자 상호
            statement.receiverCorpName = "수신자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "수신자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "수신자 주소";

            // 수신자 종목
            statement.receiverBizClass = "수신자 종목";

            // 수신자 업태 
            statement.receiverBizType = "수신자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "수신자 담당자명";

            // 수신자 메일주소 
            statement.receiverEmail = "test@receiver.com";

            /**************************************************************************
             *                         전자명세서 기재항목                            *
             **************************************************************************/

            // [필수] 공급가액 합계
            statement.supplyCostTotal = "200000";

            // [필수] 세액 합계
            statement.taxTotal = "20000";

            // 합계금액
            statement.totalAmount = "220000";

            // 기재상 일련번호 항목
            statement.serialNum = "123";

            // 기재상 비고 항목
            statement.remark1 = "비고1";
            statement.remark2 = "비고2";
            statement.remark3 = "비고3";

            // 사업자등록증 이미지 첨부여부
            statement.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            statement.bankBookYN = false;

            statement.detailList = new List<StatementDetail>();

            StatementDetail detail = new StatementDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000"); // 전잔액
            statement.propertyBag.Add("Deposit", "5000"); // 입금액
            statement.propertyBag.Add("CBalance", "20000"); // 현잔액


            try
            {
                //Update(팝빌회원 사업자번호, 명세서코드, 문서관리번호, 명세서객체, 팝빌회원 아이디)
                Response response = statementService.Update(txtCorpNum.Text, selectedItemCode(), txtMgtKey.Text,
                    statement, txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message, "전자명세서 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message, "전자명세서 수정");
            }
        }

        /*
         * 1건의 [임시저장] 상태의 전자명세서를 [발행]합니다.
         */
        private void btnIssue_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            //메모
            string memo = "발행 메모";

            try
            {
                Response response =
                    statementService.Issue(txtCorpNum.Text, itemCode, txtMgtKey.Text, memo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 발행");
            }
        }


        /*
         * [발행완료] 전자명세서를 [발행취소] 처리합니다.
         * - 발행취소 전자명세서에 사용된 문서관리번호(mgtKey)를 재사용하기 위해서는
         *   삭제 (Delete API)를 호출하여 [삭제] 해야합니다.
         */
        private void btnCancel_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response =
                    statementService.CancelIssue(txtCorpNum.Text, itemCode, txtMgtKey.Text, memo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 발행취소");
            }
        }

        /*
         * 1건의 전자명세서를 [발행취소]합니다.
         */
        private void btnCancelIssueSub_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            // 메모 
            String memo = "발행취소 메모";

            try
            {
                Response response =
                    statementService.CancelIssue(txtCorpNum.Text, itemCode, txtMgtKey.Text, memo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 발행취소");
            }
        }

        /*
         * 1건의 전자명세서를 [삭제]합니다.
         * - 전자명세서를 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소]
         */
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.Delete(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 삭제");
            }
        }

        /*
         * 1건의 전자명세서를 [삭제]합니다.
         * - 전자명세서를 삭제하면 사용된 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소]
         */
        private void btnDeleteSub_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.Delete(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 삭제");
            }
        }

        /*
         * 1건의 전자명세서 상태/요약 정보를 확인합니다.
         * - 응답항목에 대한 자세한 정보는 "[전자명세서 API 연동매뉴얼] > 3.2.1. GetInfo (상태 확인)"을 참조하시기 바랍니다.
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                StatementInfo statementInfo = statementService.GetInfo(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemCode (문서종류코드) : " + statementInfo.itemCode.ToString() + CRLF;
                tmp += "itemKey (팝빌 관리번호) : " + statementInfo.itemKey + CRLF;
                tmp += "invoiceNum (문서고유번호) : " + statementInfo.invoiceNum + CRLF;
                tmp += "mgtKey (문서관리번호) : " + statementInfo.mgtKey + CRLF;
                tmp += "taxType (세금형태) : " + statementInfo.taxType + CRLF;
                tmp += "writeDate (작성일자) : " + statementInfo.writeDate + CRLF;
                tmp += "regDT (임시저장일시) : " + statementInfo.regDT + CRLF;
                tmp += "senderCorpName (발신자 상호) : " + statementInfo.senderCorpName + CRLF;
                tmp += "senderCorpNum (발신자 사업자등록번호) : " + statementInfo.senderCorpNum + CRLF;
                tmp += "senderPrintYN (발신자 인쇄여부) : " + statementInfo.senderPrintYN + CRLF;
                tmp += "receiverCorpName (수신자 상호): " + statementInfo.receiverCorpName + CRLF;
                tmp += "receiverCorpNum (수신자 사업자등록번호) : " + statementInfo.receiverCorpNum + CRLF;
                tmp += "receiverPrintYN (수신자 인쇄여부) : " + statementInfo.receiverPrintYN + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + statementInfo.supplyCostTotal + CRLF;
                tmp += "taxTotal (세액 합계) : " + statementInfo.taxTotal + CRLF;
                tmp += "purposeType (영수/청구) : " + statementInfo.purposeType + CRLF;
                tmp += "issueDT (발행일시) : " + statementInfo.issueDT + CRLF;
                tmp += "stateCode (상태코드) : " + statementInfo.stateCode.ToString() + CRLF;
                tmp += "stateDT (상태 변경일시) : " + statementInfo.stateDT + CRLF;
                tmp += "stateMemo (상태메모) : " + statementInfo.stateMemo + CRLF;
                tmp += "openYN (개봉 여부) : " + statementInfo.openYN.ToString() + CRLF;
                tmp += "openDT (개봉 일시) : " + statementInfo.openDT + CRLF;

                MessageBox.Show(tmp, "전자명세서 상태/요약 정보");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 상태/요약 정보");
            }
        }

        /*
         * 다수건의 전자명세서 상태/요약 정보를 확인합니다.
         * - 응답항목에 대한 자세한 정보는 "[전자명세서 API 연동매뉴얼] > 3.2.2. GetInfos (상태 대량 확인)"을 참조하시기 바랍니다.
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            List<string> MgtKeyList = new List<string>();

            //문서관리번호 배열, 최대 1000건
            MgtKeyList.Add("20190110-001");
            MgtKeyList.Add("20190110-002");
            MgtKeyList.Add("20190110-003");
            MgtKeyList.Add("20190110-004");
            MgtKeyList.Add("20190110-005");

            try
            {
                List<StatementInfo> statementInfoList =
                    statementService.GetInfos(txtCorpNum.Text, itemCode, MgtKeyList);

                string tmp = null;

                for (int i = 0; i < statementInfoList.Count; i++)
                {
                    if (statementInfoList[i].itemKey != null)
                    {
                        tmp += "itemCode (문서종류코드) : " + statementInfoList[i].itemCode.ToString() + CRLF;
                        tmp += "itemKey (팝빌 관리번호) : " + statementInfoList[i].itemKey + CRLF;
                        tmp += "invoiceNum (문서고유번호) : " + statementInfoList[i].invoiceNum + CRLF;
                        tmp += "mgtKey (문서관리번호) : " + statementInfoList[i].mgtKey + CRLF;
                        tmp += "taxType (세금형태) : " + statementInfoList[i].taxType + CRLF;
                        tmp += "writeDate (작성일자) : " + statementInfoList[i].writeDate + CRLF;
                        tmp += "regDT (임시저장일시) : " + statementInfoList[i].regDT + CRLF;
                        tmp += "senderCorpName (발신자 상호) : " + statementInfoList[i].senderCorpName + CRLF;
                        tmp += "senderCorpNum (발신자 사업자등록번호) : " + statementInfoList[i].senderCorpNum + CRLF;
                        tmp += "senderPrintYN (발신자 인쇄여부) : " + statementInfoList[i].senderPrintYN + CRLF;
                        tmp += "receiverCorpName (수신자 상호): " + statementInfoList[i].receiverCorpName + CRLF;
                        tmp += "receiverCorpNum (수신자 사업자등록번호) : " + statementInfoList[i].receiverCorpNum + CRLF;
                        tmp += "receiverPrintYN (수신자 인쇄여부) : " + statementInfoList[i].receiverPrintYN + CRLF;
                        tmp += "supplyCostTotal (공급가액 합계) : " + statementInfoList[i].supplyCostTotal + CRLF;
                        tmp += "taxTotal (세액 합계) : " + statementInfoList[i].taxTotal + CRLF;
                        tmp += "purposeType (영수/청구) : " + statementInfoList[i].purposeType + CRLF;
                        tmp += "issueDT (발행일시) : " + statementInfoList[i].issueDT + CRLF;
                        tmp += "stateCode (상태코드) : " + statementInfoList[i].stateCode.ToString() + CRLF;
                        tmp += "stateDT (상태 변경일시) : " + statementInfoList[i].stateDT + CRLF;
                        tmp += "stateMemo (상태메모) : " + statementInfoList[i].stateMemo + CRLF;
                        tmp += "openYN (개봉 여부) : " + statementInfoList[i].openYN.ToString() + CRLF;
                        tmp += "openDT (개봉 일시) : " + statementInfoList[i].openDT + CRLF + CRLF;
                    }
                }

                MessageBox.Show(tmp, "전자명세서 상태/요약 정보 - 대량");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 상태/요약 정보 - 대량");
            }
        }

        /*
         * 전자명세서 1건의 상세정보를 조회합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자명세서 API 연동매뉴얼] > 4.1. 전자명세서 구성" 을 참조하시기 바랍니다.
         */
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Statement statement = statementService.GetDetailInfo(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemCode(문서종류코드): " + statement.itemCode + CRLF;
                tmp += "mgtKey(관리번호): " + statement.mgtKey + CRLF;
                tmp += "invoiceNum(문서고유번호): " + statement.invoiceNum + CRLF;
                tmp += "formCode(맞춤양식 코드): " + statement.formCode + CRLF;
                tmp += "writeDate(작성일자): " + statement.writeDate + CRLF;
                tmp += "taxType(세금형태): " + statement.taxType + CRLF;
                tmp += "purposeType(영수/청구): " + statement.purposeType + CRLF;
                tmp += "serialNum(일련번호): " + statement.serialNum + CRLF;
                tmp += "taxTotal(세액 합계): " + statement.taxTotal + CRLF;
                tmp += "supplyCostTotal(공급가액 합계): " + statement.supplyCostTotal + CRLF;
                tmp += "totalAmount(합계금액): " + statement.totalAmount + CRLF;
                tmp += "remark1(비고1): " + statement.remark1 + CRLF;
                tmp += "remark2(비고2): " + statement.remark2 + CRLF;
                tmp += "remark3(비고3): " + statement.remark3 + CRLF;

                tmp += "senderCorpNum(발신자 사업자번호): " + statement.senderCorpNum + CRLF;
                tmp += "senderTaxRegID(발신자 종사업장번호): " + statement.senderTaxRegID + CRLF;
                tmp += "senderCorpName(발신자 상호): " + statement.senderCorpName + CRLF;
                tmp += "senderCEOName(발신자 대표자 성명): " + statement.senderCEOName + CRLF;
                tmp += "senderAddr(발신자 주소): " + statement.senderAddr + CRLF;
                tmp += "senderBizType(발신자 업태): " + statement.senderBizType + CRLF;
                tmp += "senderBizClass(발신자 종목): " + statement.senderBizClass + CRLF;
                tmp += "senderContactName(발신자 성명): " + statement.senderContactName + CRLF;
                tmp += "senderDeptName(발신자 부서명): " + statement.senderDeptName + CRLF;
                tmp += "senderTEL(발신자 연락처): " + statement.senderTEL + CRLF;
                tmp += "senderHP(발신자 휴대전화): " + statement.senderHP + CRLF;
                tmp += "senderEmail(발신자 이메일주소): " + statement.senderEmail + CRLF;
                tmp += "senderFAX(발신자 팩스번호): " + statement.senderFAX + CRLF;

                tmp += "receiverCorpNum(수신자 사업자번호): " + statement.receiverCorpNum + CRLF;
                tmp += "receiverTaxRegID(수신자 종사업장번호): " + statement.receiverTaxRegID + CRLF;
                tmp += "receiverCorpName(수신자 상호): " + statement.receiverCorpName + CRLF;
                tmp += "receiverCEOName(수신자 대표자 성명): " + statement.receiverCEOName + CRLF;
                tmp += "receiverAddr(수신자 주소): " + statement.receiverAddr + CRLF;
                tmp += "receiverBizType(수신자 업태): " + statement.receiverBizType + CRLF;
                tmp += "receiverBizClass(수신자 종목): " + statement.receiverBizClass + CRLF;
                tmp += "receiverContactName(수신자 성명): " + statement.receiverContactName + CRLF;
                tmp += "receiverDeptName(수신자 부서명): " + statement.receiverDeptName + CRLF;
                tmp += "receiverTEL(수신자 연락처): " + statement.receiverTEL + CRLF;
                tmp += "receiverHP(수신자 휴대전화): " + statement.receiverHP + CRLF;
                tmp += "receiverEmail(수신자 이메일주소): " + statement.receiverEmail + CRLF;
                tmp += "receiverFAX(수신자 팩스번호): " + statement.receiverFAX + CRLF + CRLF;

                if (!statement.detailList.Equals(null))
                {
                    tmp += "[detailList]" + CRLF;
                    for (int i = 0; i < statement.detailList.Count(); i++)
                    {
                        tmp += "serialNum (일련번호) : " + statement.detailList[i].serialNum.ToString() + CRLF;
                        //tmp += "purchaseDT (거래일자): " + statement.detailList[i].purchaseDT + CRLF;
                        //tmp += "itemName (품목명) : " + statement.detailList[i].itemName + CRLF;
                        //tmp += "spec (규격) : " + statement.detailList[i].spec + CRLF;
                        //tmp += "qty (수량) : " + statement.detailList[i].qty + CRLF;
                        //tmp += "supplyCost (공급가액) : " + statement.detailList[i].supplyCost+CRLF;
                        //tmp += "tax (세액) : " + statement.detailList[i].tax + CRLF;
                        //tmp += "unitCost (단가) : " + statement.detailList[i].unitCost + CRLF;
                        //tmp += "reamark (비고) : " + statement.detailList[i].remark + CRLF + CRLF;
                    }

                    tmp += CRLF;
                }

                if (!statement.propertyBag.Equals(null))
                {
                    tmp += "[propertyBag]" + CRLF;
                    foreach (string key in statement.propertyBag.fromJsonDic().Keys)
                    {
                        tmp += key + " : " + statement.propertyBag.getValue(key) + CRLF;
                    }
                }

                MessageBox.Show(tmp, "전자명세서 상세정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 상세정보 확인");
            }
        }


        /*
         * 검색조건을 사용하여 전자명세서 목록을 조회합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자명세서 API 연동매뉴얼] > 3.2.4. Search (목록 조회)" 를 참조하시기 바랍니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // [필수] 검색일자 유형, R-등록일자, W-작성일자, I-발행일자
            String DType = "W";

            // [필수] 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20181201";

            // [필수] 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20190109";

            // 전송상태값 배열, 미기재시 전체 상태조회, 문서상태 값 3자리의 배열, 2,3번째 자리에 와일드카드 가능
            String[] State = new String[4];
            State[0] = "100";
            State[1] = "2**";
            State[2] = "3**";
            State[3] = "4**";

            //명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표, 126-영수증
            int[] ItemCode = { 121, 122, 123, 124, 125, 126 };

            // 거래처 조회, 거래처 등록번호, 상호 조회, 미기재시 전체조회
            String QString = "";

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 목록개수, 최대 1000개
            int PerPage = 50;

            try
            {
                DocSearchResult searchResult = statementService.Search(txtCorpNum.Text, DType, SDate, EDate, State,
                    ItemCode, QString, Order, Page, PerPage);

                String tmp = null;
                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF + CRLF;

                tmp +=
                    "itemCode | itemKey | mgtKey | taxType | writeDate | senderCorpName | senderCorpNum | senderPrintYN | ";
                tmp += " receiverCorpName | receiverCorpNum | receiverPrintYN | supplyCostTotal";
                tmp += " | taxTotal | stateCode" + CRLF;

                foreach (StatementInfo statementInfo in searchResult.list)
                {
                    tmp += statementInfo.itemCode + " | ";
                    tmp += statementInfo.itemKey + " | ";
                    tmp += statementInfo.mgtKey + " | ";
                    tmp += statementInfo.taxType + " | ";
                    tmp += statementInfo.writeDate + " | ";
                    tmp += statementInfo.senderCorpName + " | ";
                    tmp += statementInfo.senderCorpNum + " | ";
                    tmp += statementInfo.senderPrintYN + " | ";
                    tmp += statementInfo.receiverCorpName + " | ";
                    tmp += statementInfo.receiverCorpNum + " | ";
                    tmp += statementInfo.receiverPrintYN + " | ";
                    tmp += statementInfo.supplyCostTotal + " | ";
                    tmp += statementInfo.taxTotal + " | ";
                    tmp += statementInfo.stateCode;
                    tmp += CRLF;
                }

                MessageBox.Show(tmp, "전자명세서 목록조회 결과");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 목록조회 결과");
            }
        }


        /*
         * 전자명세서 상태 변경이력을 확인합니다.
         * - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
         *  "[전자명세서 API 연동매뉴얼] > 3.2.5 GetLogs (상태 변경이력 확인)" 을 참조하시기 바랍니다.
         */
        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            try
            {
                List<StatementLog> logList = statementService.GetLogs(txtCorpNum.Text, itemcode, txtMgtKey.Text);

                string tmp = "";

                tmp += "docType(로그타입) | log(이력정보) | procType(처리형태) | procContactName(처리담당자) |";
                tmp += "procMemo(처리메모) | regDT(등록일시) | ip(아이피)" + CRLF + CRLF;
                foreach (StatementLog log in logList)
                {
                    tmp += log.docLogType + " | " + log.log + " | " + log.procType + " | " + log.procContactName +
                           " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + CRLF;
                }

                MessageBox.Show(tmp, "문서 상태변경 이력");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서 상태변경 이력");
            }
        }

        /*
         * 팝빌 > 전자명세서 > 임시(연동) 문서함 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetURL(txtCorpNum.Text, txtUserID.Text, "TBOX");

                MessageBox.Show(url, "임시 연동문서함 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "임시 연동문서함 팝업 URL");
            }
        }

        /*
         * 팝빌 > 전자명세서 >  발행 문서함 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetURL(txtCorpNum.Text, txtUserID.Text, "SBOX");

                MessageBox.Show(url, "발행 문서함 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행 문서함 팝업 URL");
            }
        }

        /*
         * 1건의 전자명세서 보기 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다. 
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetPopUpURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);

                MessageBox.Show(url, "전자명세서 보기 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 보기 팝업 URL");
            }
        }


        /*
         * 1건의 전자명세서 인쇄팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다
         */
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetPrintURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);

                MessageBox.Show(url, "전자명세서 인쇄 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 인쇄 팝업 URL");
            }
        }


        /*
         * 1건의 전자명세서 인쇄 팝업 URL(수신자)을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetEPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetEPrintURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);
                MessageBox.Show(url, "전자명세서 인쇄 팝업 URL-수신자용");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 인쇄 팝업 URL-수신자용");
            }
        }

        /*
         * 다수건의 전자명세서 인쇄팝업 URL을 반환합니다. (최대 100건)
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간 갖습니다.
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            List<string> mgtKeyList = new List<string>();

            // 문서관리번호 배열 (최대 100건)
            mgtKeyList.Add("20190110-01");
            mgtKeyList.Add("20190110-02");
            mgtKeyList.Add("20190110-03");
            mgtKeyList.Add("20190110-04");
            mgtKeyList.Add("20190110-05");

            try
            {
                string url = statementService.GetMassPrintURL(txtCorpNum.Text, itemCode, mgtKeyList, txtUserID.Text);

                MessageBox.Show(url, "전자명세서 인쇄팝업 URL - 대량");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명새서 인쇄팝업 URL - 대량");
            }
        }


        /*
         * 수신자 메일링크 URL을 반환합니다.
         * - 메일링크 URL은 유효시간이 존재하지 않습니다.
         */
        private void btnGetMailURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetMailURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);

                MessageBox.Show(url, "수신자 메일링크 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수신자 메일링크 URL");
            }
        }


        /*
         * 팝빌에 로그인 상태로 접근할 수 있는 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetAccessURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 인감 및 첨부문서 등록 URL을 반환합니다.
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void getPopbillURL_SEAL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetSealURL(txtCorpNum.Text, txtUserID.Text);

                MessageBox.Show(url, "인감 및 첨부문서 등록 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "인감 및 첨부문서 등록 URL");
            }
        }

        /*
         * 전자명세서에 첨부파일을 등록합니다.
         *  - [임시저장] 상태의 전자명세서만 파일을 첨부할수 있습니다.
         *  - 첨부파일은 최대 5개까지 등록할 수 있습니다.
         */
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    Response response = statementService.AttachFile(txtCorpNum.Text, itemCode, txtMgtKey.Text,
                        strFileName, txtUserID.Text);

                    MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + response.message, "첨부파일 등록");
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "첨부파일 등록");
                }
            }
        }


        /*
         * 전자명세서에 첨부된 파일을 삭제합니다.
         * - 첨부된 파일을 식별하는 파일아이디는 첨부파일 목록(GetFiles API)의 응답항목 중
         *   파일아이디(AttachedFile) 항목을 통해 확인할 수 있습니다.
         */
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.DeleteFile(txtCorpNum.Text, itemCode, txtMgtKey.Text,
                    txtFileID.Text, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "첨부파일 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "첨부파일 삭제");
            }
        }

        /*
         * 첨부파일에 등록된 파일의 목록을 확인합니다.
         * - 응답항목 중 파일아이디(AttachedFile)항목은 파일삭제 (DeleteFile API)
         *   호출시 사용됩니다.
         */
        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                List<AttachedFile> fileList = statementService.GetFiles(txtCorpNum.Text, itemCode, txtMgtKey.Text);


                string tmp = "serialNum(일련번호) | displayName(첨부파일명) | attachedFile(파일아이디) | regDT(등록일자)" + CRLF;

                foreach (AttachedFile file in fileList)
                {
                    tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " +
                           file.regDT + CRLF;
                    txtFileID.Text = file.attachedFile;
                }

                MessageBox.Show(tmp);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        /*
         * 발행 안내메일을 재전송합니다.
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            // 수신메일주소
            string ReceiverEmail = "test@test.com";

            try
            {
                Response response = statementService.SendEmail(txtCorpNum.Text, itemcode, txtMgtKey.Text, ReceiverEmail,
                    txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "발행 안내메일 재전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행 안내메일 재전송");
            }
        }


        /*
         * 알림문자를 전송합니다. (단문/SMS- 한글 최대 45자)
         * - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [문자] > [전송내역] 탭에서 전송결과를 확인할 수 있습니다.
         */
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            // 발신번호, [참고] 발신번호 세칙안내 - http://blog.linkhub.co.kr/3064
            string senderNum = "07043042991";

            // 수신번호
            string receiverNum = "010111222";

            // 문자메시지 내용,이 90Byte초과하는경우 길이가 조정되어 전송됨
            string msgContents = "전자명세서 문자전송 테스트 dotnet";

            try
            {
                Response response = statementService.SendSMS(txtCorpNum.Text, itemcode, txtMgtKey.Text, senderNum,
                    receiverNum, msgContents, txtUserID.Text);
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
         * 전자명세서를 팩스전송합니다.
         * - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인할 수 있습니다.
         */
        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            // 발신번호 
            string senderNum = "07043042991";

            // 수신번호
            string receiverNum = "000111222";

            try
            {
                Response response = statementService.SendFAX(txtCorpNum.Text, itemcode, txtMgtKey.Text, senderNum,
                    receiverNum, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 팩스전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 팩스전송");
            }
        }

        /*
         * 팝빌에 전자명세서를 등록하지 않고 공급받는자에게 팩스전송합니다.
         * - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 팩스 발행 요청시 작성한 문서관리번호는 팩스전송 파일명으로 사용됩니다.
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인할 수 있습니다.
         * - 팩스 전송결과를 확인하기 위해서는 선팩스 전송 요청 시 반환받은 접수번호를 이용하여
         *   팩스 API의 전송결과 확인 (GetFaxDetail) API를 이용하면 됩니다.
         */
        private void btnFAXSend_Click(object sender, EventArgs e)
        {
            // [필수] 팩스 발신번호
            String SendNum = "07043042991";

            // [필수] 선팩스전송 수신팩스번호 
            String ReceiveNum = "010111222";

            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20190110";

            // [필수], {영수, 청구} 중 기재 
            statement.purposeType = "영수";

            // [필수], 과세형태, {과세, 영세, 면세} 중 기재
            statement.taxType = "과세";

            // 맞춤양식코드, 기본값을 공백('')으로 처리하면 기본양식으로 처리.
            statement.formCode = txtFormCode.Text;

            // [필수] 전자명세서 양식코드
            statement.itemCode = selectedItemCode();

            // [필수] 문서관리번호, 1~24자리 숫자, 영문, '-', '_' 조합으로 사업자별로 중복되지 않도록 구성
            statement.mgtKey = txtMgtKey.Text;


            /**************************************************************************
             *                          발신자 정보                                   *
             **************************************************************************/

            // [필수] 발신자 사업자번호
            statement.senderCorpNum = txtCorpNum.Text;

            // 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            statement.senderTaxRegID = "";

            // 발신자 상호
            statement.senderCorpName = "발신자 상호";

            // 발신자 대표자 성명 
            statement.senderCEOName = "발신자 대표자 성명";

            // 발신자 주소 
            statement.senderAddr = "발신자 주소";

            // 발신자 종목
            statement.senderBizClass = "발신자 종목";

            // 발신자 업태 
            statement.senderBizType = "발신자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "발신자 담당자명";

            // 발신자 메일주소 
            statement.senderEmail = "test@test.com";

            // 발신자 연락처
            statement.senderTEL = "070-7070-0707";

            // 발신자 휴대폰번호 
            statement.senderHP = "010-000-2222";


            /**************************************************************************
             *                             수신자 정보                                *
             **************************************************************************/

            // 수신자 사업자번호
            statement.receiverCorpNum = "8888888888";

            // [필수] 수신자 상호
            statement.receiverCorpName = "수신자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "수신자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "수신자 주소";

            // 수신자 종목
            statement.receiverBizClass = "수신자 종목";

            // 수신자 업태 
            statement.receiverBizType = "수신자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "수신자 담당자명";

            // 수신자 메일주소 
            statement.receiverEmail = "test@receiver.com";

            /**************************************************************************
             *                         전자명세서 기재항목                            *
             **************************************************************************/

            // [필수] 공급가액 합계
            statement.supplyCostTotal = "200000";

            // [필수] 세액 합계
            statement.taxTotal = "20000";

            // 합계금액
            statement.totalAmount = "220000";

            // 기재상 일련번호 항목
            statement.serialNum = "123";

            // 기재상 비고 항목
            statement.remark1 = "비고1";
            statement.remark2 = "비고2";
            statement.remark3 = "비고3";

            // 사업자등록증 이미지 첨부여부
            statement.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            statement.bankBookYN = false;

            statement.detailList = new List<StatementDetail>();

            StatementDetail detail = new StatementDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1"; // 수량
            detail.unitCost = "100000"; // 단가
            detail.supplyCost = "100000"; // 공급가액
            detail.tax = "10000"; // 세액
            detail.remark = "품목비고"; // 비고
            detail.spare1 = "spare1"; // 여분1
            detail.spare1 = "spare2"; // 여분2
            detail.spare1 = "spare3"; // 여분3
            detail.spare1 = "spare4"; // 여분4
            detail.spare1 = "spare5"; // 여분5

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000"); // 전잔액
            statement.propertyBag.Add("Deposit", "5000"); // 입금액
            statement.propertyBag.Add("CBalance", "20000"); // 현잔액

            try
            {
                String receiptNum =
                    statementService.FAXSend(txtCorpNum.Text, statement, SendNum, ReceiveNum, txtUserID.Text);

                MessageBox.Show("팩스전송 접수번호 : " + receiptNum, "선팩스전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "선팩스전송");
            }
        }

        /*
         * 전자명세서에 다른 전자명세서 1건을 첨부합니다.
         */
        private void btnAttachStmt_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();


            // 첨부할 전자명세서 코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표, 126-영수증
            int SubItemCode = 121;

            // 첨부할 전자명세서 관리번호 
            String SubMgtKey = "20190109-100";

            try
            {
                Response response = statementService.AttachStatement(txtCorpNum.Text, itemCode, txtMgtKey.Text,
                    SubItemCode, SubMgtKey);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "다른 전자명세서 첨부");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "다른 전자명세서 첨부");
            }
        }

        /*
         * 첨부된 전자명세서 1건을 첨부해제 합니다. 
         */
        private void btnDetachStmt_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            // 첨부해제할 명세서 코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표, 126-영수증
            int SubItemCode = 121;

            // 첨부해제할 명세서 관리번호 
            String SubMgtKey = "20190109-100";

            try
            {
                Response response = statementService.DetachStatement(txtCorpNum.Text, itemCode, txtMgtKey.Text,
                    SubItemCode, SubMgtKey);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 첨부해제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 첨부해제 ");
            }
        }


        /*
         * 전자명세서 메일전송 항목에 대한 전송여부를 목록으로 반환합니다.
         */
        private void btnListEmailConfig_Click(object sender, EventArgs e)
        {
            String tmp = "";
            try
            {
                List<EmailConfig> resultList = statementService.ListEmailConfig(txtCorpNum.Text, txtUserID.Text);

                tmp = "메일전송유형 | 전송여부" + CRLF;

                foreach (EmailConfig info in resultList)
                {
                    if (info.emailType == "SMT_ISSUE")
                        tmp += "SMT_ISSUE (수신자에게 전자명세서가 발행 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "SMT_ACCEPT")
                        tmp += "SMT_ACCEPT (발신자에게 전자명세서가 승인 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "SMT_DENY")
                        tmp += "SMT_DENY (발신자게에 전자명세서가 거부 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "SMT_CANCEL")
                        tmp += "SMT_CANCEL (수신자게에 전자명세서가 취소 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "SMT_CANCEL_ISSUE")
                        tmp += "SMT_CANCEL_ISSUE (수신자에게 전자명세서가 발행취소 되었음을 알려주는 메일) | " + info.sendYN + CRLF;
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
           전자명세서 메일전송 항목에 대한 전송여부를 수정합니다.
           메일전송유형
           SMT_ISSUE : 수신자에게 전자명세서가 발행 되었음을 알려주는 메일입니다.
           SMT_ACCEPT : 발신자에게 전자명세서가 승인 되었음을 알려주는 메일입니다.
           SMT_DENY : 발신자게에 전자명세서가 거부 되었음을 알려주는 메일입니다.
           SMT_CANCEL : 수신자게에 전자명세서가 취소 되었음을 알려주는 메일입니다.
           SMT_CANCEL_ISSUE : 수신자에게 전자명세서가 발행취소 되었음을 알려주는 메일입니다.
         */
        private void btnUpdateEmailConfig_Click(object sender, EventArgs e)
        {
            String EmailType = "SMT_ISSUE";

            //전송여부 (True-전송, False-미전송)
            bool SendYN = true;

            try
            {
                Response response =
                    statementService.UpdateEmailConfig(txtCorpNum.Text, EmailType, SendYN, txtUserID.Text);

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
                double remainPoint = statementService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 팝빌 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetChargeURL(txtCorpNum.Text, txtUserID.Text);

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
         * - 연동 과금 방식의 경우 연동회원 잔여포인트 조회 (GetBalance API) 기능을 사용하시기 바랍니다.
         */
        private void btnGetPartnerPoint_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = statementService.GetPartnerBalance(txtCorpNum.Text);

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
                string url = statementService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 전자명세서 발행단가를 확인합니다.
         */
        private void btnGetUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = statementService.GetUnitCost(txtCorpNum.Text, selectedItemCode());

                MessageBox.Show(cboItemCode.Text + " 발행단가 : " + unitCost.ToString(), "전자명세서 발행단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 발행단가");
            }
        }

        /*
         * 전자명세서 API 서비스 과금정보를 확인합니다.
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            //명세서 종류코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표, 126-영수증
            int itemCode = selectedItemCode();

            try
            {
                ChargeInfo chrgInf = statementService.GetChargeInfo(txtCorpNum.Text, itemCode);

                string tmp = null;
                tmp += "unitCost (단가) : " + chrgInf.unitCost + CRLF;
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
         * 사업자의 파트너 연동회원 가입여부를 확인합니다.
         * - 사업자등록번호는 '-' 제외한 10자리 숫자 문자열입니다.
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = statementService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "연동회원 가입여부확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입여부확인");
            }
        }

        /*
         * 팝빌 회원아이디 중복여부를 확인합니다.
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = statementService.CheckID(txtUserID.Text);

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
         * 팝빌 회원아이디 중복여부를 확인합니다.
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
                Response response = statementService.JoinMember(joinInfo);

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
                CorpInfo corpInfo = statementService.GetCorpInfo(txtCorpNum.Text, txtUserID.Text);

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
                Response response = statementService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserID.Text);

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
                Response response = statementService.RegistContact(txtCorpNum.Text, contactInfo, txtUserID.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 추가등록");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 추가등록");
            }
        }

        /*
         * 연동회원의 담당자 정보 목록을 확인합니다.
         */
        private void ListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = statementService.ListContact(txtCorpNum.Text, txtUserID.Text);

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
        private void UpdateContact_Click(object sender, EventArgs e)
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
                Response response = statementService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserID.Text);

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