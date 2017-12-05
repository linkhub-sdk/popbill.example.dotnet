
/*
 * 팝빌 전자세금계산서 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2017-12-05
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 29, 32 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 * 3) 전자세금계산서 발행을 위해 공인인증서를 등록합니다. 두가지 방법 중 선택 
 *    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정] > [공인인증서 관리]
 *    - 공인인증서 등록 팝업 URL (GetPopbillURL API)을 이용하여 등록
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Popbill.Taxinvoice.Example.csharp
{
    public partial class frmExample : Form
    {

        // 링크아이디
        private string LinkID = "TESTER";

        // 비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        // 세금계산서 서비스 객체 선언
        private TaxinvoiceService taxinvoiceService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            //세금계산서 서비스 객체 초기화
            taxinvoiceService = new TaxinvoiceService(LinkID, SecretKey);

            //연동환경 설정값, 개발용(true), 상업용(false)
            taxinvoiceService.IsTest = true;
        }


        /*
         * 연동회원 가입요청을 신청합니다.
         * - 회원가입 전 아이디확인(CheckID API)를 사용하여 아이디 중복여부를 확인할 수 있습니다. 
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
                Response response = taxinvoiceService.JoinMember(joinInfo);

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
                double remainPoint = taxinvoiceService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 조회");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 조회");
            }
        }

        
        /*
         * 사업자의 파트너 연동회원 가입여부를 확인합니다.
         * - 사업자등록번호는 '-' 제외한 10자리 숫자 문자열입니다.
         * - 링크아이디는 29번라인에 선언되어있는 인증정보(LInkID) 입니다.
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * 전자세금계산서 발행단가를 확인합니다.
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = taxinvoiceService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show("발행단가 : " + unitCost.ToString(), "세금계산서 발행단가 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 발행단가 확인");
            }
        }

        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show("공인인증서 만료일시 : " + expiration.ToString(), "공인인증서 만료일시 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "공인인증서 만료일시 확인");
            }
        }

        /*
         *  문서관리번호 중복여부를 확인합니다.
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType) Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                bool InUse = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show((InUse ? "사용중" : "미사용중"), "문서관리번호 중복확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서관리번호 중복확인");
            }
        }

        /*
         * 대용량 연계사업자 메일목록을 확인합니다. 
         */
        private void btnGetEmailPublicKey_Click(object sender, EventArgs e)
        {
            string tmp = "";

            try
            {
                List<EmailPublicKey> KeyList = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text);

                foreach (EmailPublicKey info in KeyList ){
                    tmp += info.confirmNum + " | " + info.email + CRLF;
                }
                MessageBox.Show(tmp, "연계사업자 메일목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연계사업자 메일목록 확인");
            }
        }


       /* 
        * 1건의 세금계산서를 임시저장 합니다.
        * - 세금계산서 임시저장(Register API) 호출후에는 발행(Issue API)을 호출해야만
        *   국세청으로 전송됩니다.
        * - 임시저장과 발행을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스
        *   연동을 권장합니다.
        * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서
        *   구성"을 참조하시기 바랍니다.
        */
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날자형식(yyyyMMdd)
            taxinvoice.writeDate = "20161013";

            // [필수] 과금방향, {정과금, 역과금}중 선택
            // - 정과금(공급자과금), 역과금(공급받는자과금)
            // - 역과금은 역발행 세금계산서를 발행하는 경우만 가능
            taxinvoice.chargeDirection = "정과금";

            // [필수] 발행형태, {정발행, 역발행, 위수탁} 중 기재 
            taxinvoice.issueType = "정발행";

            // [필수] {영수, 청구} 중 기재
            taxinvoice.purposeType = "영수";

            // [필수] 발행시점, {직접발행, 승인시자동발행} 중 기재 
            // - {승인시자동발행}은 발행예정 프로세스에서만 이용가능
            taxinvoice.issueTiming = "직접발행";

            // [필수] 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // [필수] 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "1234567890";

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 
            //        1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // [필수] 공급자 대표자 성명 
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";

            // 공급자 주소 
            taxinvoice.invoicerAddr = "공급자 주소";

            // 공급자 종목
            taxinvoice.invoicerBizClass = "공급자 종목";

            // 공급자 업태 
            taxinvoice.invoicerBizType = "공급자 업태,업태2";

            // 공급자 담당자 성명 
            taxinvoice.invoicerContactName = "공급자 담당자명";

            // 공급자 담당자 메일주소 
            taxinvoice.invoicerEmail = "test@test.com";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "070-4304-2991";

            // 공급자 담당자 휴대폰번호 
            taxinvoice.invoicerHP = "010-000-2222";

            // 발행시 알림문자 전송여부
            // - 공급받는자 담당자 휴대폰번호(invoiceeHP1)로 전송
            // - 전송시 포인트가 차감되며 전송실패하는 경우 포인트 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // [필수] 공급받는자 구분, {사업자, 개인, 외국인} 중 기재 
            taxinvoice.invoiceeType = "사업자";

            // [필수] 공급받는자 사업자번호, '-'제외 10자리
            taxinvoice.invoiceeCorpNum = "8888888888";

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로
            // 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = "";

            // [필수] 공급받는자 대표자 성명 
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소 
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태 
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "070-1234-1234";

            // 공급받는자 담당자명 
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소 
            taxinvoice.invoiceeEmail1 = "test@test.com";

            // 공급받는자 담당자 휴대폰번호 
            taxinvoice.invoiceeHP1 = "010-111-222";

            // 역발행시 알림문자 전송여부 
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // [필수] 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // [필수] 세액 합계
            taxinvoice.taxTotal = "10000";

            // [필수] 합계금액,  공급가액 합계 + 세액 합계
            taxinvoice.totalAmount = "110000";

            // 기재상 일련번호 항목 
            taxinvoice.serialNum = "123";

            // 기재상 현금 항목 
            taxinvoice.cash = "";

            // 기재상 수표 항목
            taxinvoice.chkBill = "";

            // 기재상 어음 항목
            taxinvoice.note = "";

            // 기재상 외상미수금 항목
            taxinvoice.credit = "";

            // 기재상 비고 항목
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;


            // 사업자등록증 이미지 첨부여부
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            taxinvoice.bankBookYN = false;


            /**************************************************************************
             *        수정세금계산서 정보 (수정세금계산서 작성시에만 기재             *
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조      *
             * - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650  *
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 ItemKey기재
            // - 원본세금계산서의 ItemKey는 문서정보 (GetInfo API) 응답항목으로 확인할 수 있습니다.
            taxinvoice.originalTaxinvoiceKey = "";



            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);


            /*************************************************************************
            *                           추가담당자 정보                              *  
            * - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우  *
            *   담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.   *
            * - 최대 5개까지 기재가능                                                *
            *************************************************************************/

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.serialNum = 1;                   // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com";    // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명";    // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;                  // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com";   // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명";   // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);
    

            // 거래명세서 동시작성여부
            bool writeSpecification = false;

            try
            {
                Response response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, writeSpecification);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 임시저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 임시저장");
            }
        }
        
        /*
         * 세금계산서 기재항목을 수정합니다.
         * - 세금계산서 수정은 [임시저장] 상태인 경우에만 가능합니다.
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날자형식(yyyyMMdd)
            taxinvoice.writeDate = "20161013";

            // [필수] 과금방향, {정과금, 역과금}중 선택
            // - 정과금(공급자과금), 역과금(공급받는자과금)
            // - 역과금은 역발행 세금계산서를 발행하는 경우만 가능
            taxinvoice.chargeDirection = "정과금";

            // [필수] 발행형태, {정발행, 역발행, 위수탁} 중 기재 
            taxinvoice.issueType = "정발행";

            // [필수] {영수, 청구} 중 기재
            taxinvoice.purposeType = "영수";

            // [필수] 발행시점, {직접발행, 승인시자동발행} 중 기재 
            // - {승인시자동발행}은 발행예정 프로세스에서만 이용가능
            taxinvoice.issueTiming = "직접발행";

            // [필수] 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // [필수] 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "1234567890";

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호_수정";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 
            //        1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // [필수] 공급자 대표자 성명 
            taxinvoice.invoicerCEOName = "공급자 대표자 성명_수정";

            // 공급자 주소 
            taxinvoice.invoicerAddr = "공급자 주소";

            // 공급자 종목
            taxinvoice.invoicerBizClass = "공급자 종목";

            // 공급자 업태 
            taxinvoice.invoicerBizType = "공급자 업태,업태2";

            // 공급자 담당자 성명 
            taxinvoice.invoicerContactName = "공급자 담당자명";

            // 공급자 담당자 메일주소 
            taxinvoice.invoicerEmail = "test@test.com";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "070-4304-2991";

            // 공급자 담당자 휴대폰번호 
            taxinvoice.invoicerHP = "010-000-2222";

            // 발행시 알림문자 전송여부
            // - 공급받는자 담당자 휴대폰번호(invoiceeHP1)로 전송
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // [필수] 공급받는자 구분, {사업자, 개인, 외국인} 중 기재 
            taxinvoice.invoiceeType = "사업자";

            // [필수] 공급받는자 사업자번호, '-'제외 10자리
            taxinvoice.invoiceeCorpNum = "8888888888";

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로
            //                 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = "";

            // [필수] 공급받는자 대표자 성명 
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소 
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태 
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "070-1234-1234";

            // 공급받는자 담당자명 
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소 
            taxinvoice.invoiceeEmail1 = "test@test.com";

            // 공급받는자 담당자 휴대폰번호 
            taxinvoice.invoiceeHP1 = "010-111-222";

            // 역발행시 알림문자 전송여부 
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // [필수] 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // [필수] 세액 합계
            taxinvoice.taxTotal = "10000";

            // [필수] 합계금액,  공급가액 합계 + 세액 합계
            taxinvoice.totalAmount = "110000";

            // 기재상 일련번호 항목 
            taxinvoice.serialNum = "123";

            // 기재상 현금 항목 
            taxinvoice.cash = "";

            // 기재상 수표 항목
            taxinvoice.chkBill = "";

            // 기재상 어음 항목
            taxinvoice.note = "";

            // 기재상 외상미수금 항목
            taxinvoice.credit = "";

            // 기재상 비고 항목
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;


            // 사업자등록증 이미지 첨부여부
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            taxinvoice.bankBookYN = false;


            /**************************************************************************
             *        수정세금계산서 정보 (수정세금계산서 작성시에만 기재             *
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조      *
             * - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650  *
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 ItemKey기재
            // - 원본세금계산서의 ItemKey는 문서정보 (GetInfo API) 응답항목으로 확인할 수 있습니다.
            taxinvoice.originalTaxinvoiceKey = "";



            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);


            /*************************************************************************
            *                           추가담당자 정보                              *  
            * - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우  *
            *   담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.   *
            * - 최대 5개까지 기재가능                                                *
            *************************************************************************/

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.serialNum = 1;                   // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com";    // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명";    // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;                  // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com";   // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명";   // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "임시저장 세금계산서 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "임시저장 세금계산서 수정");
            }
        }

        /*
         * 1건의 세금계산서를 삭제합니다.
         * - 등록된 문서관리번호는 삭제 이후 재사용할 수 있습니다.
         */
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
               
            try
            {
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 삭제");  
    
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 삭제");
            }
        }


        /*
         * 임시저장 세금계산서 1건을 [발행예정] 처리합니다.
         * - 발행예정이란 공급자가 작성한 세금계산서를 공급받는자에게 메일을 통해 
         *   기재정보에 대한 내용확인을 요청한 이후 발행하는 방법입니다.
         */
        private void btnSend_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            //메모
            String Memo = "발행예정 메모";

            //발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송
            String EmailSubject = ""; 

            try
            {   
                Response response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 발행예정 ");


            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 발행예정");
            }
        }

        /*
         * 발행예정 요청한 세금계산서를 [취소]처리합니다.
         */
        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 발행예정 취소 메모 
            string memo = "발행예정 취소 메모 ";

            try
            {   
                Response response = taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "발행예정 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행예정 취소 ");
            }
        }


        /*
         * 1건의 세금계산서 상세항목을 확인합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼]
         *   > 4.1 (세금)계산서 구성" 을 참조하시기 바랍니다.
         */
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                Taxinvoice taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                string tmp = null;

                tmp += "writeDate  : " + taxinvoice.writeDate + CRLF;
                tmp += "chargeDirection  : " + taxinvoice.chargeDirection + CRLF;
                tmp += "issueType  : " + taxinvoice.issueType + CRLF;
                tmp += "issueTiming  : " + taxinvoice.issueTiming + CRLF;
                tmp += "taxType  : " + taxinvoice.taxType + CRLF +CRLF;
                tmp += "invoicerCorpNum  : " + taxinvoice.invoicerCorpNum + CRLF;
                tmp += "invoicerMgtKey  : " + taxinvoice.invoicerMgtKey + CRLF;
                tmp += "invoicerTaxRegID  : " + taxinvoice.invoicerTaxRegID + CRLF;
                tmp += "invoicerCorpName  : " + taxinvoice.invoicerCorpName + CRLF;
                tmp += "invoicerCEOName  : " + taxinvoice.invoicerCEOName + CRLF;
                tmp += "invoicerAddr  : " + taxinvoice.invoicerAddr + CRLF;
                tmp += "invoicerBizClass  : " + taxinvoice.invoicerBizClass + CRLF;
                tmp += "invoicerBizType  : " + taxinvoice.invoicerBizType + CRLF;
                tmp += "invoicerContactName  : " + taxinvoice.invoicerContactName + CRLF;
                tmp += "invoicerTEL   : " + taxinvoice.invoicerTEL + CRLF;
                tmp += "invoicerHP  : " + taxinvoice.invoicerHP + CRLF;
                tmp += "invoicerEmail  : " + taxinvoice.invoicerEmail + CRLF;
                tmp += "invoicerSMSSendYN  : " + taxinvoice.invoicerSMSSendYN + CRLF +CRLF;

                tmp += "invoiceeCorpNum : " + taxinvoice.invoiceeCorpNum + CRLF;
                tmp += "invoiceeMgtKey : " + taxinvoice.invoiceeMgtKey + CRLF;
                tmp += "invoiceeTaxRegID : " + taxinvoice.invoiceeTaxRegID + CRLF;
                tmp += "invoiceeCorpName : " + taxinvoice.invoiceeCorpName + CRLF;
                tmp += "invoiceeCEOName : " + taxinvoice.invoiceeCEOName + CRLF;
                tmp += "invoiceeAddr : " + taxinvoice.invoiceeAddr + CRLF;
                tmp += "invoiceeBizClass : " + taxinvoice.invoiceeBizClass + CRLF;
                tmp += "invoiceeBizType: " + taxinvoice.invoiceeBizType + CRLF;
                tmp += "invoiceeContactName1 : " + taxinvoice.invoiceeContactName1 + CRLF;
                tmp += "invoiceeDeptName1 : " + taxinvoice.invoiceeDeptName1 + CRLF;
                tmp += "invoiceeTEL1 : " + taxinvoice.invoiceeTEL1 + CRLF;
                tmp += "invoiceeHP1 : " + taxinvoice.invoiceeHP1 + CRLF;
                tmp += "invoiceeEmail1 : " + taxinvoice.invoiceeEmail1 + CRLF;
                tmp += "invoiceeSMSSendYN : " + taxinvoice.invoiceeSMSSendYN + CRLF;
                tmp += "closeDownState : " + taxinvoice.closeDownState + CRLF;
                tmp += "closeDownStateDate: " + taxinvoice.closeDownStateDate + CRLF + CRLF;

                tmp += "taxTotal : " + taxinvoice.taxTotal + CRLF;
                tmp += "supplyCostTotal : " + taxinvoice.supplyCostTotal + CRLF;
                tmp += "totalAmount : " + taxinvoice.totalAmount + CRLF;
                tmp += "modifyCode : " + taxinvoice.modifyCode + CRLF;
                tmp += "purposeType : " + taxinvoice.purposeType + CRLF;
                tmp += "serialNum : " + taxinvoice.serialNum + CRLF;
                tmp += "cash : " + taxinvoice.cash + CRLF;
                tmp += "chkBill : " + taxinvoice.chkBill + CRLF;
                tmp += "ntsconfirmNum : " + taxinvoice.ntsconfirmNum + CRLF;
                tmp += "originalTaxinvoiceKey : " + taxinvoice.originalTaxinvoiceKey + CRLF;

                MessageBox.Show(tmp, "세금계산서 상세정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 상세정보 확인");
            }
        }

        /*
         * 1건의 세금계산서 상태/요약 정보를 확인합니다.
         * - 세금계산서 상태정보(GetInfo API) 응답항목에 대한 자세한 정보는
         *  "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성"
         *   을 참조하시기 바랍니다.
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                TaxinvoiceInfo taxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemKey : " + taxinvoiceInfo.itemKey + CRLF;
                tmp += "taxType : " + taxinvoiceInfo.taxType + CRLF;
                tmp += "writeDate : " + taxinvoiceInfo.writeDate + CRLF;
                tmp += "regDT : " + taxinvoiceInfo.regDT + CRLF;

                tmp += "invoicerCorpName : " + taxinvoiceInfo.invoicerCorpName + CRLF;
                tmp += "invoicerCorpNum : " + taxinvoiceInfo.invoicerCorpNum + CRLF;
                tmp += "invoicerMgtKey : " + taxinvoiceInfo.invoicerMgtKey + CRLF;
                tmp += "invoicerPrintYN : " + taxinvoiceInfo.invoicerPrintYN + CRLF;
                tmp += "invoiceeCorpName : " + taxinvoiceInfo.invoiceeCorpName + CRLF;
                tmp += "invoiceeCorpNum : " + taxinvoiceInfo.invoiceeCorpNum + CRLF;
                tmp += "invoiceeMgtKey : " + taxinvoiceInfo.invoiceeMgtKey + CRLF;
                tmp += "invoiceePrintYN : " + taxinvoiceInfo.invoiceePrintYN + CRLF;
                tmp += "closeDownState : " + taxinvoiceInfo.closeDownState + CRLF;
                tmp += "closeDownStateDate : " + taxinvoiceInfo.closeDownStateDate + CRLF;
                tmp += "interOPYN : " + taxinvoiceInfo.interOPYN + CRLF;

                tmp += "supplyCostTotal : " + taxinvoiceInfo.supplyCostTotal + CRLF;
                tmp += "taxTotal : " + taxinvoiceInfo.taxTotal + CRLF;
                tmp += "purposeType : " + taxinvoiceInfo.purposeType + CRLF;
                tmp += "modifyCode : " + taxinvoiceInfo.modifyCode.ToString() + CRLF;
                tmp += "issueType : " + taxinvoiceInfo.issueType + CRLF;

                tmp += "issueDT : " + taxinvoiceInfo.issueDT + CRLF;
                tmp += "preIssueDT : " + taxinvoiceInfo.preIssueDT + CRLF;

                tmp += "stateCode : " + taxinvoiceInfo.stateCode.ToString() + CRLF;
                tmp += "stateDT : " + taxinvoiceInfo.stateDT + CRLF;

                tmp += "openYN : " + taxinvoiceInfo.openYN.ToString() + CRLF;
                tmp += "openDT : " + taxinvoiceInfo.openDT + CRLF;
                tmp += "ntsresult : " + taxinvoiceInfo.ntsresult + CRLF;
                tmp += "ntsconfirmNum : " + taxinvoiceInfo.ntsconfirmNum + CRLF;
                tmp += "ntssendDT : " + taxinvoiceInfo.ntssendDT + CRLF;
                tmp += "ntsresultDT : " + taxinvoiceInfo.ntsresultDT + CRLF;
                tmp += "ntssendErrCode : " + taxinvoiceInfo.ntssendErrCode + CRLF;
                tmp += "stateMemo : " + taxinvoiceInfo.stateMemo;

                MessageBox.Show(tmp, "문서 상태/요약 정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서 상태/요약 정보 조회");
            }
        }


        /*
         * 팝빌 > 임시(연동)문서함 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX");

                MessageBox.Show(url, "임시연동함 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "임시연동함 팝업 URL");
            }
        }

        /*
         * 팝빌 > 매출 문서함 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX");

                MessageBox.Show(url, "매출문서함 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "매출문서함 팝업 URL");
            }
        }

        /*
         * 팝빌 > 매입 문서함 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetURL_PBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

                MessageBox.Show(url,"매입문서함 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "매입문서함 팝업 URL");
            }
        }

        /*
         *  팝빌 > 매출 문서작성 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetURL_WRITE_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE");

                MessageBox.Show(url, "매출문서 작성 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "매출문서 작성 팝업 URL");
            }
        }

        /*
         * 세금계산서 상태 변경이력을 확인합니다.
         * - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
         *   "[전자세금계산서 API 연동매뉴얼] > 3.5.5. 상태 변경이력 확인"
         *   을 참조하시기 바랍니다.
         */
        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                String tmp = "";
             
                List<TaxinvoiceLog> logList = taxinvoiceService.GetLogs(txtCorpNum.Text, KeyType, txtMgtKey.Text);


                foreach (TaxinvoiceLog log in logList)
                {
                    tmp += log.docLogType + " | " + log.log + " | " + log.procType + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + CRLF;
                }
                
                MessageBox.Show(tmp, "문서 상태변경 이력확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서 상태변경 이력확인");
            }
        }


        /*
         * 대량의 세금계산서 상태/요약 정보를 확인합니다. (최대 1000건)
         * - 세금계산서 상태정보(GetInfos API) 응답항목에 대한 자세한 정보는
         *  "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성"
         *  을 참조하시기 바랍니다. 
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            //  조회할 세금계산서 문서관리번호 배열, (최대 1000건)
            MgtKeyList.Add("20161221-03");
            MgtKeyList.Add("20161221-02");
            MgtKeyList.Add("20161221-01");
            

            string tmp = "";

            try
            {
                List<TaxinvoiceInfo> taxinvoiceInfoList = taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList);

                tmp = "발행일자 | 공급자 상호 | 공급자 사업자번호 | 공급받는자 상호 | 공급받는자 사업자번호 | 공급가액 합계 | 세액 합계 | 휴폐업 상태 | 휴폐업 일자" + CRLF;
                foreach (TaxinvoiceInfo info in taxinvoiceInfoList)
                {
                    tmp += info.writeDate + " | " + info.invoicerCorpName + " | " + info.invoicerCorpNum + " | " + info.invoiceeCorpName + " | " + 
                            info.invoiceeCorpNum + " | " + info.supplyCostTotal + " | " +  info.taxTotal + " | "
                            + info.closeDownState + " | " + info.closeDownStateDate + CRLF ;
                }

                MessageBox.Show(tmp, "문서 상태정보 대량 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서 상태정보 대량 확인");
            }
        }


        /*
         * 발행 안내메일을 재전송합니다.
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 수신메일주소
            String email = "test@test.com";

            try
            {   
                Response response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, email, txtUserId.Text);

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
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [전송내역] 탭에서
         *   전송결과를 확인할 수 있습니다.
         */
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 발신번호, [참고] 발신번호 세칙안내 - http://blog.linkhub.co.kr/3064
            string senderNum = "070-4304-2991";

            // 수신번호
            string receiverNum = "010-111-222";

            // 문자메시지 내용, 90byte 초과시 길이가 조정되어 전송됨
            string contents = "알림문자 전송내용, 90byte 초과된 내용은 삭제되어 전송됨";
               
            try
            {   
                Response response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, senderNum, receiverNum, contents, txtUserId.Text);

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
         * 전자세금계산서를 팩스전송합니다.
         * - 팩스 전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역]
         *   메뉴에서 전송결과를 확인할 수 있습니다.
         */
        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 발신번호
            string senderNum = "070-4304-2991";

            // 수신팩스번호
            string receiverNum = "070-111-222";

            try
            {
                Response response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, senderNum, receiverNum, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 팩스전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 팩스전송");
            }
        }

        /*
         *  1건의 전자세금계산서 보기 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                string url = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 보기 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 보기 팝업 URL");
            }
        }


        /*
         * 1건의 전자세금계산서 인쇄팝업 URL을 반환합니다.
          - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            // 발행형태
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                string url = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 인쇄 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 인쇄 팝업 URL");
            }
        }

        /*
         * 세금계산서 인쇄(공급받는자용) URL을 반환합니다.
         * - URL 보안정책에 따라 반환된 URL은 30초의 유효시간을 갖습니다.
         */
        private void btnEPrintURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                string url = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 인쇄(공급받는자) 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 인쇄(공급받는자) 팝업 URL");
            }
        }

        /*
         * 공급받는자 메일링크 URL을 반환합니다.
         * - 메일링크 URL은 유효시간이 존재하지 않습니다.
         */
        private void btnGetEmailURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                string url = taxinvoiceService.GetMailURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "메일링크 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "메일링크 팝업 URL");
            }
        }

        /*
         *  대량의 전자세금계산서 인쇄팝업 URL을 반환합니다.
         *  - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            // 인쇄할 세금계산서 문서관리번호, (최대 1000건)
            MgtKeyList.Add("20161014-01");
            MgtKeyList.Add("20161014-02");
            MgtKeyList.Add("20161014-03");
            MgtKeyList.Add("20161014-04");

            try
            {
                string url = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 대량인쇄 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 대량인쇄 팝업 URL");
            }
        }

        /* [발행완료] 상태의 세금계산서를 국세청으로 즉시전송합니다.
         * - 국세청 즉시전송을 호출하지 않은 세금계산서는 발행일 기준 익일 오후 3시에
         *   팝빌 시스템에서 일괄적으로 국세청으로 전송합니다.
         * - 익일전송시 전송일이 법정공휴일인 경우 다음 영업일에 전송됩니다.
         * - 국세청 전송에 관한 사항은 "[전자세금계산서 API 연동매뉴얼] > 1.3 국세청
         *   전송 정책" 을 참조하시기 바랍니다.
         */
        private void btnSendToNTS_Click(object sender, EventArgs e)
        {

            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                
                Response response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "국세청 즉시전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "국세청 즉시전송");
            }
        }


        /*
         * [임시저장] 상태의 세금계산서를 [발행] 처리합니다.
         * - 발행(Issue API)을 호출하는 시점에서 포인트가 차감됩니다.
         *  - [발행완료] 세금계산서는 연동회원의 국세청 전송설정에 따라
         *    익일/즉시전송 처리됩니다. 기본설정(익일전송)
         * - 국세청 전송설정은 "팝빌 로그인" > [전자세금계산서] > [환경설정] >
         *   [전자세금계산서 관리] > [국세청 전송 및 지연발행 설정] 탭에서
         *   확인할 수 있습니다.
         * - 국세청 전송정책에 대한 사항은 "[전자세금계산서 API 연동매뉴얼] >
         *   1.3. 국세청 전송 정책" 을 참조하시기 바랍니다
         */
        private void btnIssue_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            String memo = "발행메모";

           // 지연발행 강제여부, 기본값 - False
           // 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
           // 지연발행 세금계산서를 신고해야 하는 경우 forceIssue 값을 True로 선언하여 발행(Issue API)을 호출할 수 있습니다.
            bool forceIssue = false;

            try
            {
                
                Response response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 발행");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 발행");
            }
        }

        /*
         * [발행완료] 세금계산서를 [발행취소] 처리합니다.
         * - [발행취소]는 국세청 전송전에만 가능합니다.
         * - 발행취소 세금계산서는 국세청에 전송되지 않습니다.
         * - 발행취소 세금계산서에 사용된 문서관리번호를 재사용 하기 위해서는
         *   삭제(Delete API)를 호출하여 [삭제] 처리 하셔야 합니다.
         */
        private void btnCancelIssue_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            string memo = "발행취소 메모";

            try
            {   
                Response response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * 공급자가 발행예정한 세금계산서를 [승인] 처리합니다. 
         */
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
                
            // 메모 
            string memo = "발행예정 승인 메모";

            try
            {
               
                Response response = taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "발행예정 승인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행예정 승인");
            }
        }


        /*
         * 발행예정 세금계산서를 [거부] 처리합니다.
         */
        private void btnDeny_Click(object sender, EventArgs e)
        {

            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            //메모
            string memo = "발행예정 거부 메모";

            try
            {   
                Response response = taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "발행예정 거부");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발행예정 거부");
            }
        }

        /*
         *  공급받는자가 공급자에게 1건의 세금계산서 역발행을 요청합니다.
         * - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두
         *   팝빌에 회원이여야 합니다.
         * - 역발행 요청후 공급자가 [발행] 처리시 포인트가 차감되며
         *   세금계산서 항목중 과금방향(ChargeDirection) 에 기재한 값에 따라
         *   정과금(공급자과금) 또는 역과금(공급받는자과금) 처리됩니다.
         */
        private void btnRequest_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            string memo = "역발행 요청시 메모";

            try
            {   
                Response response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "역발행 요청");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "역발행 요청");
            }
        }

        /*
         * 역발행 세금계산서를 [취소] 처리합니다.
         * - [취소]한 세금계산서의 문서관리번호를 재사용하기 위해서는 
         *   삭제 (Delete API)를 호출해야 합니다.
         */
        private void btnCancelRequest_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "역발행 요청 취소 메모";
            
            try
            {   
                Response response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "역발행 요청 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "역발행 요청 취소");
            }
        }

        /*
         * 공급받은자로부터 요청받은 역발행 세금계산서를 [거부]처리합니다. 
         */
        private void btnRefuse_Click(object sender, EventArgs e)
        {

            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "역발행 요청 거부 메모";

            try
            {
               
                Response response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        /*
         * 1건의 역발행 세금계산서를 [임시저장] 처리합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] 
         *   > 4.1. 세금계산서 구성"을 참조하시기 바랍니다.
         */
        private void btnRegister_Reverse_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날자형식(yyyyMMdd)
            taxinvoice.writeDate = "20161013";

            // [필수] 과금방향, {정과금, 역과금}중 선택
            // - 정과금(공급자과금), 역과금(공급받는자과금)
            // - 역과금은 역발행 세금계산서를 발행하는 경우만 가능
            taxinvoice.chargeDirection = "정과금";

            // [필수] 발행형태, {정발행, 역발행, 위수탁} 중 기재 
            taxinvoice.issueType = "역발행";

            // [필수] {영수, 청구} 중 기재
            taxinvoice.purposeType = "영수";

            // [필수] 발행시점, {직접발행, 승인시자동발행} 중 기재 
            // - {승인시자동발행}은 발행예정 프로세스에서만 이용가능
            taxinvoice.issueTiming = "직접발행";

            // [필수] 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // [필수] 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "8888888888";

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 
            //        1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // [필수] 공급자 대표자 성명 
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";

            // 공급자 주소 
            taxinvoice.invoicerAddr = "공급자 주소";

            // 공급자 종목
            taxinvoice.invoicerBizClass = "공급자 종목";

            // 공급자 업태 
            taxinvoice.invoicerBizType = "공급자 업태,업태2";

            // 공급자 담당자 성명 
            taxinvoice.invoicerContactName = "공급자 담당자명";

            // 공급자 담당자 메일주소 
            taxinvoice.invoicerEmail = "test@test.com";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "070-4304-2991";

            // 공급자 담당자 휴대폰번호 
            taxinvoice.invoicerHP = "010-000-2222";

            // 발행시 알림문자 전송여부
            // - 공급받는자 담당자 휴대폰번호(invoiceeHP1)로 전송
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // [필수] 공급받는자 구분, {사업자, 개인, 외국인} 중 기재 
            taxinvoice.invoiceeType = "사업자";

            // [필수] 공급받는자 사업자번호, '-'제외 10자리
            taxinvoice.invoiceeCorpNum = "1234567890";

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로
            //                 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = "";

            // [필수] 공급받는자 대표자 성명 
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소 
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태 
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "070-1234-1234";

            // 공급받는자 담당자명 
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소 
            taxinvoice.invoiceeEmail1 = "test@test.com";

            // 공급받는자 담당자 휴대폰번호 
            taxinvoice.invoiceeHP1 = "010-111-222";

            // 역발행시 알림문자 전송여부 
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // [필수] 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // [필수] 세액 합계
            taxinvoice.taxTotal = "10000";

            // [필수] 합계금액,  공급가액 합계 + 세액 합계
            taxinvoice.totalAmount = "110000";

            // 기재상 일련번호 항목 
            taxinvoice.serialNum = "123";

            // 기재상 현금 항목 
            taxinvoice.cash = "";

            // 기재상 수표 항목
            taxinvoice.chkBill = "";

            // 기재상 어음 항목
            taxinvoice.note = "";

            // 기재상 외상미수금 항목
            taxinvoice.credit = "";

            // 기재상 비고 항목
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;


            // 사업자등록증 이미지 첨부여부
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            taxinvoice.bankBookYN = false;


            /**************************************************************************
             *        수정세금계산서 정보 (수정세금계산서 작성시에만 기재             *
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조      *
             * - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650  *
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 ItemKey기재
            // - 원본세금계산서의 ItemKey는 문서정보 (GetInfo API) 응답항목으로 확인할 수 있습니다.
            taxinvoice.originalTaxinvoiceKey = "";



            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);


            /*************************************************************************
            *                           추가담당자 정보                              *  
            * - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우  *
            *   담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.   *
            * - 최대 5개까지 기재가능                                                *
            *************************************************************************/

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.serialNum = 1;                   // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com";    // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명";    // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;                  // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com";   // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명";   // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);

            try
            {
                Response response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "역발행 세금계산서 임시저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "역발행 세금계산서 임시저장");
            }
        }

        /*
         * 1건의 임시저장 역발행 세금계산서의 항목을 수정합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] 
         * > 4.1. (세금)계산서 구성"을 참조하시기 바랍니다.
         */
        private void btnUpdate_Reverse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날자형식(yyyyMMdd)
            taxinvoice.writeDate = "20161013";

            // [필수] 과금방향, {정과금, 역과금}중 선택
            // - 정과금(공급자과금), 역과금(공급받는자과금)
            // - 역과금은 역발행 세금계산서를 발행하는 경우만 가능
            taxinvoice.chargeDirection = "정과금";

            // [필수] 발행형태, {정발행, 역발행, 위수탁} 중 기재 
            taxinvoice.issueType = "역발행";

            // [필수] {영수, 청구} 중 기재
            taxinvoice.purposeType = "영수";

            // [필수] 발행시점, {직접발행, 승인시자동발행} 중 기재 
            // - {승인시자동발행}은 발행예정 프로세스에서만 이용가능
            taxinvoice.issueTiming = "직접발행";

            // [필수] 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // [필수] 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "8888888888";

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 
            //        1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // [필수] 공급자 대표자 성명 
            taxinvoice.invoicerCEOName = "공급자 대표자 성명_수정";

            // 공급자 주소 
            taxinvoice.invoicerAddr = "공급자 주소_수정";

            // 공급자 종목
            taxinvoice.invoicerBizClass = "공급자 종목";

            // 공급자 업태 
            taxinvoice.invoicerBizType = "공급자 업태,업태2";

            // 공급자 담당자 성명 
            taxinvoice.invoicerContactName = "공급자 담당자명";

            // 공급자 담당자 메일주소 
            taxinvoice.invoicerEmail = "test@test.com";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "070-4304-2991";

            // 공급자 담당자 휴대폰번호 
            taxinvoice.invoicerHP = "010-000-2222";

            // 발행시 알림문자 전송여부
            // - 공급받는자 담당자 휴대폰번호(invoiceeHP1)로 전송
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // [필수] 공급받는자 구분, {사업자, 개인, 외국인} 중 기재 
            taxinvoice.invoiceeType = "사업자";

            // [필수] 공급받는자 사업자번호, '-'제외 10자리
            taxinvoice.invoiceeCorpNum = "1234567890";

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로
            //                 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = "";

            // [필수] 공급받는자 대표자 성명 
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소 
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태 
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "070-1234-1234";

            // 공급받는자 담당자명 
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소 
            taxinvoice.invoiceeEmail1 = "test@test.com";

            // 공급받는자 담당자 휴대폰번호 
            taxinvoice.invoiceeHP1 = "010-111-222";

            // 역발행시 알림문자 전송여부 
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // [필수] 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // [필수] 세액 합계
            taxinvoice.taxTotal = "10000";

            // [필수] 합계금액,  공급가액 합계 + 세액 합계
            taxinvoice.totalAmount = "110000";

            // 기재상 일련번호 항목 
            taxinvoice.serialNum = "123";

            // 기재상 현금 항목 
            taxinvoice.cash = "";

            // 기재상 수표 항목
            taxinvoice.chkBill = "";

            // 기재상 어음 항목
            taxinvoice.note = "";

            // 기재상 외상미수금 항목
            taxinvoice.credit = "";

            // 기재상 비고 항목
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;


            // 사업자등록증 이미지 첨부여부
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 
            taxinvoice.bankBookYN = false;


            /**************************************************************************
             *        수정세금계산서 정보 (수정세금계산서 작성시에만 기재             *
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조      *
             * - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650  *
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 ItemKey기재
            // - 원본세금계산서의 ItemKey는 문서정보 (GetInfo API) 응답항목으로 확인할 수 있습니다.
            taxinvoice.originalTaxinvoiceKey = "";



            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);


            /*************************************************************************
            *                           추가담당자 정보                              *  
            * - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우  *
            *   담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.   *
            * - 최대 5개까지 기재가능                                                *
            *************************************************************************/

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.serialNum = 1;                   // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com";    // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명";    // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;                  // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com";   // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명";   // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);

            try
            {   
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "역발행 세금계산서 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "역발행 세금계산서 수정");
            }
        }

        /*
         * 세금계산서에 첨부파일을 등록합니다.
         *  - [임시저장] 상태의 세금계산서만 파일을 첨부할수 있습니다.
         *  - 첨부파일은 최대 5개까지 등록할 수 있습니다.
         */
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    Response response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text);

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
         * 세금계산서에 첨부된 파일의 목록을 확인합니다.
         * - 응답항목 중 파일아이디(AttachedFile) 항목은 파일삭제(DeleteFile API) 시 사용됩니다.
         */
        private void gtnGetFiles_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {   
                List<AttachedFile> fileList = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text);


                string tmp = "일련번호 | 표시명 | 파일아이디 | 등록일자" + CRLF;

                foreach (AttachedFile file in fileList)
                {
                    tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + CRLF;

                    txtFileID.Text = file.attachedFile;
                }
                MessageBox.Show(tmp, "첨부파일 목록 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "첨부파일 목록 확인");
            }
        }

        /*
         * 세금계산서에 첨부된 파일을 삭제합니다.
         * - 파일을 식별하는 파일아이디는 첨부파일 목록(GetFileList API) 의 응답항목
         *   중 파일아이디(AttachedFile) 값을 통해 확인할 수 있습니다.          
         */
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
                
            try
            {   
                Response response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text);

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
         * 팝빌 로그인 팝업 URL을 반환합니다.
         * - URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void getPopbillURL_LOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

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
        private void getPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url, "연동회원 포인트 충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 URL");
            }
            
        }

        /*
         * 공인인증서 등록 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void getPopbillURL_CERT_Click(object sender, EventArgs e)
        {
            string url;

            try
            {
                url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CERT");
                MessageBox.Show(url, "공인인증서 등록 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "공인인증서 등록 URL");
            }            
        }

        /*
         * 연동회원의 담당자를 추가합니다. 
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 20자 미만
            contactInfo.id = "testkorea_20161014";

            //비밀번호, 6자 이상 20자 미만
            contactInfo.pwd = "user_password";

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
                Response response = taxinvoiceService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
            contactInfo.fax = "02-6442-9700";

            // 이메일주소
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;              

            try
            {
                Response response = taxinvoiceService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당저 정보 수정");
            }
        }

        /*
         * 파트너 잔여포인트를 확인합니다. 
         * - 연동 과금 방식의 경우 연동회원 잔여포인트 조회 (GetBalance API) 기능을 사용하시기 바랍니다.
         */
        private void btnGetPartnerBalance_Click_1(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = taxinvoiceService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " + remainPoint.ToString(), "파트너 잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");

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
                Response response = taxinvoiceService.CheckID(txtUserId.Text);

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
         * 연동회원의 담당자 정보 목록을 확인합니다.
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = taxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text);

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
         * 연동회원의 회사정보를 조회합니다.
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = taxinvoiceService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
                Response response = taxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

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
         * 1건의 세금계산서를 즉시발행 처리합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서
         *   구성"을 참조하시기 바랍니다.
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {   
            
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날자형식(yyyyMMdd)
            taxinvoice.writeDate = "20170323";

            // [필수] 과금방향, {정과금, 역과금}중 선택
            // - 정과금(공급자과금), 역과금(공급받는자과금)
            // - 역과금은 역발행 세금계산서를 발행하는 경우만 가능
            taxinvoice.chargeDirection = "정과금";

            // [필수] 발행형태, {정발행, 역발행, 위수탁} 중 기재 
            taxinvoice.issueType = "정발행";
            
            // [필수] {영수, 청구} 중 기재
            taxinvoice.purposeType = "영수";
            
            // [필수] 발행시점, {직접발행, 승인시자동발행} 중 기재 
            // - {승인시자동발행}은 발행예정 프로세스에서만 이용가능
            taxinvoice.issueTiming = "직접발행";                    

            // [필수] 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";                            


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // [필수] 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "1234567890";
            
            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";                       

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 
            //        1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // [필수] 공급자 대표자 성명 
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";

            // 공급자 주소 
            taxinvoice.invoicerAddr = "공급자 주소";

            // 공급자 종목
            taxinvoice.invoicerBizClass = "공급자 종목";

            // 공급자 업태 
            taxinvoice.invoicerBizType = "공급자 업태,업태2";

            // 공급자 담당자 성명 
            taxinvoice.invoicerContactName = "공급자 담당자명";

            // 공급자 담당자 메일주소 
            taxinvoice.invoicerEmail = "test@test.com";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "070-4304-2991";

            // 공급자 담당자 휴대폰번호 
            taxinvoice.invoicerHP = "010-000-2222";

            // 발행시 알림문자 전송여부
            // - 공급받는자 담당자 휴대폰번호(invoiceeHP1)로 전송
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // [필수] 공급받는자 구분, {사업자, 개인, 외국인} 중 기재 
            taxinvoice.invoiceeType = "사업자";                     

            // [필수] 공급받는자 사업자번호, '-'제외 10자리
            taxinvoice.invoiceeCorpNum = "8888888888";

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로
            //                 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = "";

            // [필수] 공급받는자 대표자 성명 
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소 
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태 
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "070-1234-1234";

            // 공급받는자 담당자명 
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소 
            taxinvoice.invoiceeEmail1 = "test@test.com";

            // 공급받는자 담당자 휴대폰번호 
            taxinvoice.invoiceeHP1 = "010-111-222";

            // 역발행시 알림문자 전송여부 
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // [필수] 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // [필수] 세액 합계
            taxinvoice.taxTotal = "10000";

            // [필수] 합계금액,  공급가액 합계 + 세액 합계
            taxinvoice.totalAmount = "110000";                      

            // 기재상 일련번호 항목 
            taxinvoice.serialNum = "123";

            // 기재상 현금 항목 
            taxinvoice.cash = "";

            // 기재상 수표 항목
            taxinvoice.chkBill = "";

            // 기재상 어음 항목
            taxinvoice.note = "";

            // 기재상 외상미수금 항목
            taxinvoice.credit = "";

            // 기재상 비고 항목
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;


            // 사업자등록증 이미지 첨부여부
            taxinvoice.businessLicenseYN = false;                   

            // 통장사본 이미지 첨부여부 
            taxinvoice.bankBookYN = false;

            
            /**************************************************************************
             *        수정세금계산서 정보 (수정세금계산서 작성시에만 기재             *
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조      *
             * - [참고] 수정세금계산서 작성방법 안내 - http://blog.linkhub.co.kr/650  *
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 ItemKey기재
            // - 원본세금계산서의 ItemKey는 문서정보 (GetInfo API) 응답항목으로 확인할 수 있습니다.
            taxinvoice.originalTaxinvoiceKey = "";



            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;               // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20161013";     // 거래일자
            detail.itemName = "품목명";         // 품목명 
            detail.spec = "규격";               // 규격
            detail.qty = "1";                   // 수량
            detail.unitCost = "50000";          // 단가
            detail.supplyCost = "50000";        // 공급가액
            detail.tax = "5000";                // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            
            /*************************************************************************
            *                           추가담당자 정보                              *  
            * - 세금계산서 발행안내 메일을 수신받을 공급받는자 담당자가 다수인 경우  *
            *   담당자 정보를 추가하여 발행안내메일을 다수에게 전송할 수 있습니다.   *
            * - 최대 5개까지 기재가능                                                *
            *************************************************************************/

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.serialNum = 1;                   // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com";    // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명";    // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;                  // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com";   // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명";   // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 지연발행 강제여부
            bool forceIssue = false;

            // 즉시발행 메모 
            String memo = "즉시발행 메모";

            try
            {   
                Response response = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "즉시발행");
            }
        }

        /*
         * [발행완료] 상태의 세금계산서 1건을 발행취소 처리합니다.  
         * - [발행취소] 처리된 세금계산서는 국세청에 전송되지 않습니다.
         * - [발행취소] 상태 세금계산서에 사용된 문서관리번호를 재사용 하기 위해서는 
         *   삭제 (Delete API)를 호출해야합니다. 
         */
        private void btnCancelIssue_Sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            string memo = "발행취소 메모";

            try
            {   
                Response response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * 1건의 세금계산서를 삭제합니다.
         * - 세금계산서에 사용된 문서관리번호는 삭제 이후 재사용할 수 있습니다.
         */
        private void btnDelete_Sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
            
            try
            {   
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 삭제");
            }
        }

        /*
         * 검색조건을 사용하여 세금계산서 목록을 조회합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼] >
         *   4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // [필수] 일자유형, R-등록일자, I-발행일자, W-작성일자 중 1개기입
            String DType = "W";

            // [필수] 시작일자, 날자형식(yyyyMMdd)
            String SDate = "20171101";

            // [필수] 종료일자, 날자형식(yyyyMMdd)
            String EDate = "20171231";  
            
            // 상태코드 배열, 미기재시 전체 상태조회, 문서상태 값 3자리의 배열, 2,3번째 자리에 와일드카드 가능
            String[] State = new String[5];
            State[0] = "";
            State[1] = "3**";
            State[2] = "4**";
            State[3] = "5**";
            State[4] = "6**";

            // 문서유형 배열, N-일반세금계산서, M-수정세금계산서
            String[] Type = new String[2];
            Type[0] = "N";
            Type[1] = "M";

            // 과세형태 배열, T-과세, N-면세, Z-영세 
            String[] TaxType = new String[3];
            TaxType[0] = "T";
            TaxType[1] = "N";
            TaxType[2] = "Z";

            // 발행유형 배열, N-정발행 / R-역발행 / T-위수탁
            String[] IssueType = new String[3];
            IssueType[0] = "N";
            IssueType[1] = "R";
            IssueType[2] = "T";

            // 종사업장 유무, 공백-전체조회, 0-종사업장 없는 문서 조회, 1-종사업장번호 조건에 따라 조회
            String TaxRegIDYN = "";

            // 종사업장번호 유형, S-공급자, B-공급받는자, T-수탁자
            String TaxRegIDType = "S";

            // 종사업장번호, 콤마(",")로 구분하여 구성 ex) "0001,1234"
            String TaxRegID = "";

            // 지연발행 여부, 미기재시 전체, true-지연발행분 조회, false-정상발행분 조회
            bool? LateOnly = null;

            // 거래처 조회, 거래처 사업자등록번호 또는 상호명 기재, 미기재시 전체조회 
            String QString = "";

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000건
            int PerPage = 30;       

            // 일반/연동문서 구분, 공백-전체조회, 0-일반문서 조회, 1-연동문서조회
            String InterOPYN = "";

            try
            {
                TISearchResult searchResult = taxinvoiceService.Search(txtCorpNum.Text, KeyType, DType, SDate, EDate, State, 
                                        Type, TaxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page, PerPage, InterOPYN, txtUserId.Text);
               
                String tmp = null;

                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF + CRLF;

                tmp += "itemKey | taxType | writeDate | regDT | invoicerCorpNum | invoicerCorpName | invoicerMgtKey | invoicerPrintYN |";
                tmp += " invoiceeCorpNum | invoiceeCorpName | invoiceeMgtKey | invoiceePrintYN | closeDownState | closeDownStateDate | ";
                tmp += "supplyCostTotal | taxTotal | purposeType | issueDT | stateCode | stateDT | lateIssueYN | interOPYN";
                tmp += CRLF + CRLF;

                foreach (TaxinvoiceInfo taxinvoiceInfo in searchResult.list)
                {
                    tmp += taxinvoiceInfo.itemKey + " | ";
                    tmp += taxinvoiceInfo.taxType + " | ";
                    tmp += taxinvoiceInfo.writeDate + " | ";
                    tmp += taxinvoiceInfo.regDT + " | ";
                    tmp += taxinvoiceInfo.invoicerCorpNum + " | ";
                    tmp += taxinvoiceInfo.invoicerCorpName + " | ";
                    tmp += taxinvoiceInfo.invoicerMgtKey + " | ";
                    tmp += taxinvoiceInfo.invoicerPrintYN + " | ";
                    tmp += taxinvoiceInfo.invoiceeCorpNum + " | ";
                    tmp += taxinvoiceInfo.invoiceeCorpName + " | ";
                    tmp += taxinvoiceInfo.invoiceeMgtKey + " | ";
                    tmp += taxinvoiceInfo.invoiceePrintYN + " | ";
                    tmp += taxinvoiceInfo.closeDownState + " | ";
                    tmp += taxinvoiceInfo.closeDownStateDate + " | ";
                    tmp += taxinvoiceInfo.supplyCostTotal + " | ";
                    tmp += taxinvoiceInfo.taxTotal + " | ";
                    tmp += taxinvoiceInfo.purposeType + " | ";
                    tmp += taxinvoiceInfo.issueDT + " | ";
                    tmp += taxinvoiceInfo.stateCode + " | ";
                    tmp += taxinvoiceInfo.stateDT + " | ";
                    tmp += taxinvoiceInfo.lateIssueYN + " | ";
                    tmp += taxinvoiceInfo.interOPYN;


                    tmp += CRLF;
                }

                MessageBox.Show(tmp, "문서목록 조회");
        

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서목록 조회");
            }
           
        }

        /*
         * 1건의 전자명세서를 세금계산서에 첨부합니다 
         */
        private void btnAttachStmt_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 첨부할 명세서 종류 코드
            int DocItemCode = 121;

            // 첨부할 명세서 관리번호
            String DocMgtKey = "20160202-01";   

            try
            {
                Response response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, DocItemCode, DocMgtKey);
                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "전자명세서 첨부");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전자명세서 첨부");
            }
        }

        /*
         * 세금계산서에 첨부되어 있는 전자명세서를 [첨부해제]합니다.
         */
        private void btnDetachStmt_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 첨부해제할 명세서 종류 코드 
            int DocItemCode = 121;

            // 첨부해제할 명세서 관리번호 
            String DocMgtKey = "20160202-01";

            try
            {
                Response response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text, DocItemCode, DocMgtKey);

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
         * 전자세금계산서 API 서비스 과금정보를 확인합니다. 
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = taxinvoiceService.GetChargeInfo(txtCorpNum.Text);

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
         * 인감 및 첨부문서 등록 URL을 반환합니다.
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void getPopbillURL_SEAL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "SEAL");

                MessageBox.Show(url, "인감 및 첨부문서 등록 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "인감 및 첨부문서 등록 URL");
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
                string url = taxinvoiceService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }
    }
}
