
/*
 * 팝빌 전자명세서 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2017-05-25
 * - 연동 기술지원 연락처 : 1600-8536 / 070-4304-2991~2
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
        * 연동회원 가입을 요청합니다.
        * - 회원가입 전 아이디 확인(CheckID API)을 사용하여 아이디 중복여부를 확인할 수 있습니다. 
        */
        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            //링크아이디
            joinInfo.LinkID = LinkID;

            //사업자번호 "-" 제외
            joinInfo.CorpNum = "1231212312";

            //대표자명 
            joinInfo.CEOName = "대표자성명";

            //상호
            joinInfo.CorpName = "상호";

            //주소
            joinInfo.Addr = "주소";

            //업태
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
        * 연동회원의 잔여포인트를 조회합니다.
        * - 파트너 과금 방식의 경우 파트너 잔여포인트 조회(GetPartnerBalance API) 기능을 사용하시기 바랍니다.
        */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = statementService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : "+remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
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

                MessageBox.Show(cboItemCode.Text+" 발행단가 : "+unitCost.ToString(), "전자명세서 발행단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 발행단가");
            }          
        }


       /*
        * 팝빌 로그인 팝업 URL을 반환합니다.
        * - URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
        */
        private void btnGetPopbillURL_LOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "LOGIN");

                MessageBox.Show(url, "팝빌 로그인 URL");
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
                string url = statementService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "CHRG");

                MessageBox.Show(url, "포인트충전 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트충전 팝업 URL");
            }  
        }


        /*
         * 문서관리번호 중복여부를 확인합니다.
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {   
                bool InUse = statementService.CheckMgtKeyInuse(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                MessageBox.Show(InUse ? "사용중" : "미사용중","문서관리번호 중복여부 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서관리번호 중복여부 확인");
            }
        }


        /*
         * 1건의 전자명세서를 임시저장 합니다.
         * - 전자명세서 항목별 정보는 "[전자명세서 API 연동매뉴얼] > 4.1. 전자명세서 구성"
         *   을 참조하시기 바랍니다.
         * - 임시저장후 발행(Issue API)를 호출해야 수신자 담당자 메일로 전자명세서가 전송됩니다.
         * - 임시저장과 발행을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스 
         *   사용을 권장합니다.
         */
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20161019";

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
            statement.senderCorpName = "공급자 상호";

            // 발신자 대표자 성명 
            statement.senderCEOName = "공급자 대표자 성명";

            // 발신자 주소 
            statement.senderAddr = "공급자 주소";

            // 발신자 종목
            statement.senderBizClass = "공급자 종목";

            // 발신자 업태 
            statement.senderBizType = "공급자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "공급자 담당자명";

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
            statement.receiverCorpName = "공급받는자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "공급받는자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "공급받는자 주소";

            // 수신자 종목
            statement.receiverBizClass = "공급받는자 종목";

            // 수신자 업태 
            statement.receiverBizType = "공급받는자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "공급받는자 담당자명";

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

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000");          // 전잔액
            statement.propertyBag.Add("Deposit", "5000");           // 입금액
            statement.propertyBag.Add("CBalance", "20000");         // 현잔액

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
         * [임시저장] 상태의 전자명세서 기재항목을 수정합니다.
         * - 전자명세서 수정은 [임시저장] 상태에서만 가능합니다.
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20161017";

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
            statement.senderCorpName = "공급자 상호_수정";

            // 발신자 대표자 성명 
            statement.senderCEOName = "공급자 대표자 성명_수정";

            // 발신자 주소 
            statement.senderAddr = "공급자 주소";

            // 발신자 종목
            statement.senderBizClass = "공급자 종목";

            // 발신자 업태 
            statement.senderBizType = "공급자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "공급자 담당자명";

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
            statement.receiverCorpName = "공급받는자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "공급받는자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "공급받는자 주소";

            // 수신자 종목
            statement.receiverBizClass = "공급받는자 종목";

            // 수신자 업태 
            statement.receiverBizType = "공급받는자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "공급받는자 담당자명";

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

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000");          // 전잔액
            statement.propertyBag.Add("Deposit", "5000");           // 입금액
            statement.propertyBag.Add("CBalance", "20000");         // 현잔액
                        

            try
            {
                //Update(팝빌회원 사업자번호, 명세서코드, 문서관리번호, 명세서객체, 팝빌회원 아이디)
                Response response = statementService.Update(txtCorpNum.Text, selectedItemCode(), txtMgtKey.Text, statement, txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        /*
         * [임시저장] 상태의 전자명세서를 발행처리합니다. 
         */
        private void btnIssue_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            //메모
            string memo = "발행 메모";

            try
            {   
                Response response = statementService.Issue(txtCorpNum.Text, itemCode, txtMgtKey.Text, memo, txtUserID.Text);

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
                Response response = statementService.CancelIssue(txtCorpNum.Text, itemCode, txtMgtKey.Text, memo, txtUserID.Text);

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
         * 1건의 전자명세서를 [삭제]처리합니다.
         * - 삭제처리된 전자명세서의 문서관리번호는 재사용할 수 있습니다. 
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
                    Response response = statementService.AttachFile(txtCorpNum.Text, itemCode, txtMgtKey.Text, strFileName, txtUserID.Text);

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


                string tmp = "일련번호 | 표시명 | 파일아이디 | 등록일자" + CRLF;

                foreach (AttachedFile file in fileList)
                {
                    tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + CRLF;
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
         * 전자명세서에 첨부된 파일을 삭제합니다.
         * - 첨부된 파일을 식별하는 파일아이디는 첨부파일 목록(GetFiles API)의 응답항목 중
         *   파일아이디(AttachedFile) 항목을 통해 확인할 수 있습니다.
         */
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.DeleteFile(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtFileID.Text, txtUserID.Text);

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
         * 1건의 전자명세서 상태/요약 정보를 확인합니다.
         * - 전자명세서 상태정보(GetInfo API) 응답항목에 대한 자세한 정보는
         *   "[전자명세서 API 연동매뉴얼] > 4.2. 전자명세서 상태정보 구성" 을
         *   참조하시기 바랍니다. 
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {   
                StatementInfo statementInfo = statementService.GetInfo(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemCode : " + statementInfo.itemCode.ToString() + CRLF;
                tmp += "itemKey : " + statementInfo.itemKey + CRLF;
                tmp += "invoiceNum : " + statementInfo.invoiceNum + CRLF;
                tmp += "mgtKey : " + statementInfo.mgtKey + CRLF;
                tmp += "stateCode : " + statementInfo.stateCode.ToString() + CRLF;
                tmp += "taxType : " + statementInfo.taxType + CRLF;
                tmp += "purposeType : " + statementInfo.purposeType + CRLF;
                tmp += "writeDate : " + statementInfo.writeDate + CRLF;
                tmp += "senderCorpName : " + statementInfo.senderCorpName + CRLF;
                tmp += "senderCorpNum : " + statementInfo.senderCorpNum + CRLF;
                tmp += "senderPrintYN : " + statementInfo.senderPrintYN + CRLF;
                tmp += "receiverCorpName : " + statementInfo.receiverCorpName + CRLF;
                tmp += "receiverCorpNum : " + statementInfo.receiverCorpNum + CRLF;
                tmp += "receiverPrintYN : " + statementInfo.receiverPrintYN + CRLF;
                tmp += "supplyCostTotal : " + statementInfo.supplyCostTotal + CRLF;
                tmp += "taxTotal : " + statementInfo.taxTotal + CRLF;
                tmp += "issueDT : " + statementInfo.issueDT + CRLF;
                tmp += "stateDT : " + statementInfo.stateDT + CRLF;
                tmp += "openYN : " + statementInfo.openYN.ToString() + CRLF;
                tmp += "openDT : " + statementInfo.openDT + CRLF;
                tmp += "stateMemo : " + statementInfo.stateMemo + CRLF;
                tmp += "regDT : " + statementInfo.regDT + CRLF;

                MessageBox.Show(tmp, "전자명세서 상태/요약 정보");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 상태/요약 정보");    
            }
        }


        /*
         * 1건의 전자명세서 상세항목을 확인합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자명세서 API 연동매뉴얼] > 4.1. 전자명세서 구성"
         *   을 참조하시기 바랍니다.
         */
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {  
                Statement statement = statementService.GetDetailInfo(txtCorpNum.Text, itemCode, txtMgtKey.Text);
                
                string tmp = null;                                                                               
                
                tmp += "itemCode : "+statement.itemCode.ToString() +CRLF;
                tmp += "invoiceNum : " + statement.invoiceNum+ CRLF;
                tmp += "formCode : " + statement.formCode + CRLF;
                tmp += "writeDate : " + statement.writeDate + CRLF;
                tmp += "taxType : " + statement.taxType + CRLF + CRLF;


                tmp += "senderCorpNum : " + statement.senderCorpNum + CRLF;
                tmp += "senderTaxRegID : " + statement.senderTaxRegID + CRLF;
                tmp += "senderCorpName : " + statement.senderCorpName + CRLF;
                tmp += "senderCEOName : " + statement.senderCEOName + CRLF;
                tmp += "senderAddr : " + statement.senderAddr + CRLF;
                tmp += "senderBizClass : " + statement.senderBizClass + CRLF;
                tmp += "senderBizType : " + statement.senderBizType + CRLF;
                tmp += "senderContactName : " + statement.senderContactName + CRLF;
                tmp += "senderDeptName : " + statement.senderDeptName + CRLF;
                tmp += "senderTEL : " + statement.senderTEL + CRLF;
                tmp += "senderHP : " + statement.senderHP + CRLF;
                tmp += "senderEmail : " + statement.senderEmail + CRLF;
                tmp += "senderFAX : " + statement.senderFAX + CRLF + CRLF;

                tmp += "receiverCorpNum : " + statement.receiverCorpNum + CRLF;
                tmp += "receiverTaxRegID : " + statement.receiverTaxRegID + CRLF;
                tmp += "receiverCorpName : " + statement.receiverCorpName + CRLF;
                tmp += "receiverCEOName : " + statement.receiverCEOName + CRLF;
                tmp += "receiverAddr : " + statement.receiverAddr + CRLF;
                tmp += "receiverBizClass : " + statement.receiverBizClass + CRLF;
                tmp += "receiverBizType : " + statement.receiverBizType + CRLF;
                tmp += "receiverContactName : " + statement.receiverContactName + CRLF;
                tmp += "receiverDeptName : " + statement.receiverDeptName + CRLF;
                tmp += "receiverTEL : " + statement.receiverTEL + CRLF;
                tmp += "receiverHP : " + statement.receiverHP + CRLF;
                tmp += "receiverEmail : " + statement.receiverEmail + CRLF;
                tmp += "receiverFAX : " + statement.receiverFAX + CRLF + CRLF;

                tmp += "taxTotal : " + statement.taxTotal + CRLF;
                tmp += "supplyCostTotal : " + statement.supplyCostTotal + CRLF;
                tmp += "totalAmount : " + statement.totalAmount + CRLF;
                tmp += "purposeType : " + statement.purposeType + CRLF;
                tmp += "serialNum : " + statement.serialNum + CRLF;
                tmp += "remark1 : " + statement.remark1 + CRLF;
                tmp += "remark2 : " + statement.remark2 + CRLF;
                tmp += "remark3 : " + statement.remark3 + CRLF;
                tmp += "businessLicenseYN : " + statement.businessLicenseYN + CRLF;
                tmp += "bankBookYN : " + statement.bankBookYN + CRLF;
                tmp += "faxsendYN : " + statement.faxsendYN + CRLF;
                tmp += "smssendYN : " + statement.smssendYN + CRLF;
                tmp += "autoacceptYN : " + statement.autoacceptYN + CRLF + CRLF;

                if (!statement.detailList.Equals(null))
                {
                    tmp += "[detailList]" + CRLF;
                    for(int i=0; i<statement.detailList.Count(); i++)
                    {
                        tmp += "serialNum : "+ statement.detailList[i].serialNum.ToString() + CRLF;
                        //tmp += "itemName : " + statement.detailList[i].itemName + CRLF;
                        //tmp += "purchaseDT : " + statement.detailList[i].purchaseDT + CRLF;
                        //tmp += "qty : " + statement.detailList[i].qty + CRLF;
                        //tmp += "spec : " + statement.detailList[i].spec + CRLF;
                        //tmp += "supplyCost : " + statement.detailList[i].supplyCost+CRLF;
                        //tmp += "tax : " + statement.detailList[i].tax + CRLF;
                        //tmp += "unit : " + statement.detailList[i].unit + CRLF;
                        //tmp += "unitCost : " + statement.detailList[i].unitCost + CRLF;
                        //tmp += "reamark : " + statement.detailList[i].remark + CRLF + CRLF;
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

                MessageBox.Show(tmp,"전자명세서 상세정보 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 상세정보 확인");
            }
        }


        /*
         * 다량의 전자명세서 상태/요약 정보를 확인합니다. (최대 1000건)
         * - 전자명세서 상태정보(GetInfos API) 응답항목에 대한 자세한 정보는 
         *   "[전자명세서 API 연동매뉴얼] > 4.2. 전자명세서 상태정보 구성" 을 
         *   참조하시기 바랍니다.
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            List<string> MgtKeyList = new List<string>();

            //문서관리번호 배열, 최대 1000건
            MgtKeyList.Add("20161017-01");
            MgtKeyList.Add("20161017-02");
            MgtKeyList.Add("20161017-03");
            MgtKeyList.Add("20161017-04");
            MgtKeyList.Add("20161017-05");

            try
            {   
                List<StatementInfo> statementInfoList = statementService.GetInfos(txtCorpNum.Text, itemCode, MgtKeyList);

                string tmp = null;

                for (int i = 0; i < statementInfoList.Count; i++)
                {
                    if (statementInfoList[i].itemKey != null)
                    {

                        tmp += "itemCode : " + statementInfoList[i].itemCode.ToString() + CRLF;
                        tmp += "itemKey : " + statementInfoList[i].itemKey + CRLF;
                        tmp += "invoiceNum : " + statementInfoList[i].invoiceNum + CRLF;
                        tmp += "mgtKey : " + statementInfoList[i].mgtKey + CRLF;
                        tmp += "stateCode : " + statementInfoList[i].stateCode.ToString() + CRLF;
                        tmp += "taxType : " + statementInfoList[i].taxType + CRLF;
                        tmp += "purposeType : " + statementInfoList[i].purposeType + CRLF;
                        tmp += "writeDate : " + statementInfoList[i].writeDate + CRLF;
                        tmp += "senderCorpName : " + statementInfoList[i].senderCorpName + CRLF;
                        tmp += "senderCorpNum : " + statementInfoList[i].senderCorpNum + CRLF;
                        tmp += "receiverCorpName : " + statementInfoList[i].receiverCorpName + CRLF;
                        tmp += "receiverCorpNum : " + statementInfoList[i].receiverCorpNum + CRLF;
                        tmp += "supplyCostTotal : " + statementInfoList[i].supplyCostTotal + CRLF;
                        tmp += "taxTotal : " + statementInfoList[i].taxTotal + CRLF;
                        tmp += "issueDT : " + statementInfoList[i].issueDT + CRLF;
                        tmp += "stateDT : " + statementInfoList[i].stateDT + CRLF;
                        tmp += "openYN : " + statementInfoList[i].openYN.ToString() + CRLF;
                        tmp += "openDT : " + statementInfoList[i].openDT + CRLF;
                        tmp += "stateMemo : " + statementInfoList[i].stateMemo + CRLF;
                        tmp += "regDT : " + statementInfoList[i].regDT + CRLF + CRLF;
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
         * 전자명세서 상태 변경이력을 확인합니다.
         */
        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            try
            {
                List<StatementLog> logList = statementService.GetLogs(txtCorpNum.Text, itemcode, txtMgtKey.Text);

                string tmp = "";

                foreach (StatementLog log in logList)
                {
                    tmp += log.docLogType + " | " + log.log + " | " + log.procType + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + CRLF;
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
         * 발행 안내메일을 재전송합니다.
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            // 수신메일주소
            string ReceiverEmail = "test@test.com";

            try
            {   
                Response response = statementService.SendEmail(txtCorpNum.Text, itemcode, txtMgtKey.Text, ReceiverEmail, txtUserID.Text);

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
         * 알림문자를 전송합니다 (단문/SMS - 한글 최대 45자)
         * - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "[팝빌 홈페이지] 로그인 > [문자.팩스] > [전송내역] 탭에서
         *   전송결과를 확인할 수 있습니다.
         */
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            // 발신번호, [참고] 발신번호 세칙안내 - http://blog.linkhub.co.kr/3064
            string senderNum = "07043042991";

            // 수신번호
            string receiverNum = "010111222";

            // 문자메시지 내용,이 90Byte초과하는경우 길이가 조정되어 전송됨
            string msgContents = "dotnet 전자명세서 문자전송 테스트";

            try
            {   
                Response response = statementService.SendSMS(txtCorpNum.Text, itemcode, txtMgtKey.Text, senderNum, receiverNum, msgContents, txtUserID.Text);
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
         * - 팩스 전송요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "[팝빌 홈페이지] 로그인 > [문자.팩스] > [팩스] > [전송내역] 탭에서
         *   전송결과를 확인할 수 있습니다.
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
                Response response = statementService.SendFAX(txtCorpNum.Text, itemcode, txtMgtKey.Text, senderNum, receiverNum, txtUserID.Text);

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
         * 1건의 전자명세서 인쇄 팝업 URL(공급받는자)을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetEPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {   
                string url = statementService.GetEPrintURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);
                MessageBox.Show(url, "전자명세서 인쇄 팝업 URL-공급받는자용");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 인쇄 팝업 URL-공급받는자용");
            }
        }

        /*
         * 다수건의 전자명세서 인쇄팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간 갖습니다.
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            List<string> mgtKeyList = new List<string>();

            // 문서관리번호 배열, 최대 1000건
            mgtKeyList.Add("20161017-01");
            mgtKeyList.Add("20161017-02");
            mgtKeyList.Add("20161017-03");
            mgtKeyList.Add("20161017-04");
            mgtKeyList.Add("20161017-05");

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
         * 팝빌 > 임시(연동) 문서함 팝업 URL을 반환합니다.
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
         * 팝빌 > 발행 문서함 팝업 URL을 반환합니다.
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
        * 파트너 잔여포인트를 확인합니다. 
        * - 연동 과금 방식의 경우 연동회원 잔여포인트 조회 (GetBalance API) 기능을 사용하시기 바랍니다.
        */
        private void btnGetPartnerPoint_Click(object sender, EventArgs e)
        {
                    
            try
            {
                double remainPoint = statementService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " +remainPoint.ToString(), "파트너 잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
       
        }

        /*
         * 연동회원의 회사정보를 수정합니다.
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
         * 담당자 정보를 수정합니다.
         */
        private void UpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserID.Text;

            // 담당자명 
            contactInfo.personName = "담당자123";

            // 연락처
            contactInfo.tel = "070-4304-2991";

            // 휴대폰번호
            contactInfo.hp = "010-1234-1234";

            // 팩스번호 
            contactInfo.fax = "02-6442-9700";

            // 이메일주소
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
         * 아이디 중복여부를 확인합니다.
         * - 아이디는 6자이상 20자미만으로 작성하시기 바랍니다.
         * - 아이디는 대/소문자 구분되지 않습니다. 
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
            contactInfo.searchAllAllowYN = false;

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
         * 전자명세서를 팝빌에 등록하지 않고 팩스로 전송합니다.
         * - 팩스 전송 실패시 차감된 포인트는 환불처리 됩니다. 
         * - 전송결과 확인은 "[팝빌 홈페이지] 로그인 > [문자.팩스] > [팩스] > [전송내역] 페이지에서 
         *   확인하실 수 있습니다.
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
            statement.writeDate = "20161017";

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
            statement.senderCorpName = "공급자 상호";

            // 발신자 대표자 성명 
            statement.senderCEOName = "공급자 대표자 성명";

            // 발신자 주소 
            statement.senderAddr = "공급자 주소";

            // 발신자 종목
            statement.senderBizClass = "공급자 종목";

            // 발신자 업태 
            statement.senderBizType = "공급자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "공급자 담당자명";

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
            statement.receiverCorpName = "공급받는자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "공급받는자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "공급받는자 주소";

            // 수신자 종목
            statement.receiverBizClass = "공급받는자 종목";

            // 수신자 업태 
            statement.receiverBizType = "공급받는자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "공급받는자 담당자명";

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

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000");          // 전잔액
            statement.propertyBag.Add("Deposit", "5000");           // 입금액
            statement.propertyBag.Add("CBalance", "20000");         // 현잔액

            try
            {   
                String receiptNum = statementService.FAXSend(txtCorpNum.Text, statement,SendNum, ReceiveNum, txtUserID.Text);

                MessageBox.Show("팩스전송 접수번호 : " + receiptNum, "선팩스전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "선팩스전송");
            }
        }


        /*
         * 1건의 전자명세서를 즉시발행 처리합니다.
         * - 전자명세서 항목별 정보는 "[전자명세서 API 연동매뉴얼] > 4.1 전자명세서 구성"
         *   을 참조하시기 바랍니다.
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {
            // 즉시발행 메모
            String memo = "즉시발행 메모";


            // 전자명세서 객체
            Statement statement = new Statement();

            // [필수], 기재상 작성일자 날짜형식(yyyyMMdd)
            statement.writeDate = "20161019";

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
            statement.senderCorpName = "공급자 상호";

            // 발신자 대표자 성명 
            statement.senderCEOName = "공급자 대표자 성명";

            // 발신자 주소 
            statement.senderAddr = "공급자 주소";

            // 발신자 종목
            statement.senderBizClass = "공급자 종목";

            // 발신자 업태 
            statement.senderBizType = "공급자 업태,업태2";

            // 발신자 담당자 성명
            statement.senderContactName = "공급자 담당자명";

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
            statement.receiverCorpName = "공급받는자 상호";

            // 수신자 대표자 성명
            statement.receiverCEOName = "공급받는자 대표자 성명";

            // 수신자 주소 
            statement.receiverAddr = "공급받는자 주소";

            // 수신자 종목
            statement.receiverBizClass = "공급받는자 종목";

            // 수신자 업태 
            statement.receiverBizType = "공급받는자 업태";

            // 수신자 담당자 성명 
            statement.receiverContactName = "공급받는자 담당자명";

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

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재, 최대 99
            detail.purchaseDT = "20161017";     // 거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                   // 수량
            detail.unitCost = "100000";         // 단가
            detail.supplyCost = "100000";       // 공급가액
            detail.tax = "10000";               // 세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);


            // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
            statement.propertyBag = new propertyBag();              

            statement.propertyBag.Add("Balance", "15000");          // 전잔액
            statement.propertyBag.Add("Deposit", "5000");           // 입금액
            statement.propertyBag.Add("CBalance", "20000");         // 현잔액

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
         * [발행완료] 전자명세서를 [발행취소] 처리합니다.
         * - 발행취소된 전자명세서의 관리번호를 재사용하기 위해서는 삭제(Delete API)를 호출해야 합니다. 
         */
        private void btnCancelIssueSub_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            // 메모 
            String memo = "발행취소 메모";

            try
            {   
                Response response = statementService.CancelIssue(txtCorpNum.Text, itemCode, txtMgtKey.Text, memo, txtUserID.Text);

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
         * 1건의 전자명세서를 [삭제]처리합니다.
         * - 삭제처리된 전자명세서의 문서관리번호는 재사용할 수 있습니다. 
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
         * 검색조건을 사용하여 전자명세서 목록을 조회합니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // [필수] 검색일자 유형, R-등록일자, W-작성일자, I-발행일자
            String DType = "W";
            
            // [필수] 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20160901";

            // [필수] 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20161031";  

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

                DocSearchResult searchResult = statementService.Search(txtCorpNum.Text, DType, SDate, EDate, State, ItemCode, QString, Order, Page, PerPage);

                String tmp = null;
                tmp += "code : " + searchResult.code + CRLF;
                tmp += "total : " + searchResult.total + CRLF;
                tmp += "perPage : " + searchResult.perPage + CRLF;
                tmp += "pageNum : " + searchResult.pageNum + CRLF;
                tmp += "pageCount : " + searchResult.pageCount + CRLF;
                tmp += "message : " + searchResult.message + CRLF +CRLF;

                tmp += "itemCode | itemKey | mgtKey | taxType | writeDate | senderCorpName | senderCorpNum | senderPrintYN | ";
                tmp += " receiverCorpName | receiverCorpNum | receiverPrintYN | supplyCostTotal";
                tmp += " | taxTotal | stateCode" +CRLF;

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
         * 다른 전자명세서 1건을 첨부합니다.
         */
        private void btnAttachStmt_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();
            

            // 첨부할 전자명세서 코드, 121-거래명세서, 122-청구서, 123-견적서, 124-발주서, 125-입금표, 126-영수증
            int SubItemCode = 121;

            // 첨부할 전자명세서 관리번호 
            String SubMgtKey = "20160202-03";   

            try
            {   
                Response response = statementService.AttachStatement(txtCorpNum.Text, itemCode, txtMgtKey.Text, SubItemCode, SubMgtKey);

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
            String SubMgtKey = "20160202-03";   

            try
            {
                
                Response response = statementService.DetachStatement(txtCorpNum.Text, itemCode, txtMgtKey.Text, SubItemCode, SubMgtKey);

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
         * 인감 및 첨부문서 등록 URL을 반환합니다.
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void getPopbillURL_SEAL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "SEAL");
                
                MessageBox.Show(url, "인감 및 첨부문서 등록 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "인감 및 첨부문서 등록 URL");
            }
        }
    }
}

