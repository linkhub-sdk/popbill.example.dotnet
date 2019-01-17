/*
 * 팝빌 전자세금계산서 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2019-01-10
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 27, 30 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 * 3) 전자세금계산서 발행을 위해 공인인증서를 등록합니다. 두가지 방법 중 선택 
 *    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정] > [공인인증서 관리]
 *    - 공인인증서 등록 팝업 URL (GetTaxCertURL API)을 이용하여 등록
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
         * 세금계산서 관리번호 중복여부를 확인합니다.
         * - 관리번호는 1~24자리로 숫자, 영문 '-', '_' 조합으로 구성할 수 있습니다.
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

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
         * 1건의 세금계산서를 [즉시발행]합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20190110";

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
            taxinvoice.invoicerCorpNum = txtCorpNum.Text;

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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
            // 미기재시 taxinvoice.kwon = null;
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            // 미기재시 taxinvoice.ho = null;
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

            detail.serialNum = 1; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
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

            addContact.serialNum = 1; // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com"; // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명"; // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com"; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 지연발행 강제여부, 기본값 - False
            // 지연발행 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
            // 지연발행 세금계산서를 신고해야 하는 경우 forceIssue 값을 true 선언하여 발행(Issue API)을 호출할 수 있습니다.
            bool forceIssue = false;

            // 즉시발행 메모 
            String memo = "즉시발행 메모";

            // 전자거래명세서 동시작성여부
            bool writeSpecification = false;

            // 전자거래명세서 문서관리번호, 1~24자리 영문, 숫자 '_', '-' 조합으로 사업자별로 중복되지 않도록 구성
            String dealInvoiceMgtKey = "";

            // 발행안내메일 제목, 미기재시 기본양식으로 전송
            String emailSubject = "";

            try
            {
                Response response = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo,
                    writeSpecification, dealInvoiceMgtKey, emailSubject);

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
         * 1건의 세금계산서를 [임시저장]합니다.
         * - 세금계산서 임시저장(Register API) 호출후에는 발행(Issue API)을 호출해야만 국세청으로 전송됩니다.
         * - 정발행시 임시저장(Register)과 발행(Issue)을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스 연동을 권장합니다.
         * - 역발행시 임시저장(Register)과 역발행요청(Request)을 한번의 호출로 처리하는 즉시요청(RegistRequest API) 프로세스 연동을 권장합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
         */
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20190110";

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
            taxinvoice.invoicerCorpNum = txtCorpNum.Text;

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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
            // 미기재시 taxinvoice.kwon = null;
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            // 미기재시 taxinvoice.ho = null;
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

            detail.serialNum = 1; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
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

            addContact.serialNum = 1; // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com"; // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명"; // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com"; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 거래명세서 동시작성여부
            bool writeSpecification = false;

            try
            {
                Response response =
                    taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, writeSpecification);

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
         * 1건의 역발행 세금계산서를 [임시저장] 처리합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] 
         *   > 4.1. 세금계산서 구성"을 참조하시기 바랍니다.
         */
        private void btnRegister_Reverse_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20190110";

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

            // 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = "";

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
            taxinvoice.invoiceeCorpNum = txtCorpNum.Text;

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;

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
            // 미기재시 taxinvoice.kwon = null;
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            // 미기재시 taxinvoice.ho = null;
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

            detail.serialNum = 1; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
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

            addContact.serialNum = 1; // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com"; // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명"; // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com"; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

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
         * [임시저장] 또는 [발행대기] 상태의 세금계산서를 [공급자]가 [발행]합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
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
                Response response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue,
                    txtUserId.Text);

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
         * [(역)발행대기] 상태의 세금계산서를 [발행] 처리합니다.
         * - 발행(Issue API)을 호출하는 시점에서 포인트가 차감됩니다.
         *  - [발행완료] 세금계산서는 연동회원의 국세청 전송설정에 따라 익일/즉시전송 처리됩니다. 기본설정(익일전송)
         * - 국세청 전송설정은 "팝빌 로그인" > [전자세금계산서] > [환경설정] > [전자세금계산서 관리] > [국세청 전송 및 지연발행 설정] 탭에서 확인할 수 있습니다.
         * - 국세청 전송정책에 대한 사항은 "[전자세금계산서 API 연동매뉴얼] > 1.3. 국세청 전송 정책" 을 참조하시기 바랍니다
         */
        private void btnIssue_Reverse_sub_Click(object sender, EventArgs e)
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
                Response response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue,
                    txtUserId.Text);

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
         * [임시저장] 상태의 세금계산서의 항목을 [수정]합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20190110";

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
            taxinvoice.invoicerCorpNum = txtCorpNum.Text;

            // 공급자 종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // [필수] 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호_수정";

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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
            // 미기재시 taxinvoice.kwon = null;
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            // 미기재시 taxinvoice.ho = null;
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

            detail.serialNum = 1; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
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

            addContact.serialNum = 1; // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com"; // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명"; // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com"; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice,
                    txtUserId.Text);

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
         * 1건의 임시저장 역발행 세금계산서의 항목을 수정합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서 구성"을 참조하시기 바랍니다.
         */
        private void btnUpdate_Reverse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20190110";

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

            // [필수] 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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
            taxinvoice.invoiceeCorpNum = txtCorpNum.Text;

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
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
            // 미기재시 taxinvoice.kwon = null;
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            // 미기재시 taxinvoice.ho = null;
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

            detail.serialNum = 1; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
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

            addContact.serialNum = 1; // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com"; // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명"; // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com"; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);

            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice,
                    txtUserId.Text);

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
         * [발행완료] 상태의 세금계산서를 [공급자]가 [발행취소]합니다.
         * - [발행취소]는 국세청 전송전에만 가능합니다.
         * - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
         * - 발행취소 세금계산서에 사용된 문서관리번호를 재사용 하기 위해서는 삭제(Delete API)를 호출하여 해당세금계산서를 삭제해야 합니다.
         */
        private void btnCancelIssue_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response =
                    taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [발행완료] 상태의 세금계산서를 [공급자]가 [발행취소]합니다.
         * - [발행취소]는 국세청 전송전에만 가능합니다.
         * - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
         * - 발행취소 세금계산서에 사용된 문서관리번호를 재사용 하기 위해서는 삭제(Delete API)를 호출하여 해당세금계산서를 삭제해야 합니다.
         */
        private void btnCancelIssue_Reverse_sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response =
                    taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [발행완료] 상태의 세금계산서를 [공급자]가 [발행취소]합니다.
         * - [발행취소]는 국세청 전송전에만 가능합니다.
         * - 발행취소된 세금계산서는 국세청에 전송되지 않습니다.
         * - 발행취소 세금계산서에 사용된 문서관리번호를 재사용 하기 위해서는 삭제(Delete API)를 호출하여 해당세금계산서를 삭제해야 합니다.
         */
        private void btnCancelIssue_Sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            string memo = "발행취소 메모";

            try
            {
                Response response =
                    taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [임시저장] 상태의 세금계산서를 [공급자]가 [발행예정]합니다.
         * - 발행예정이란 공급자와 공급받는자 사이에 세금계산서 확인 후 발행하는 방법입니다.
         * - "[전자세금계산서 API 연동매뉴얼] > 1.2.1. 정발행 > 다. 임시저장 발행예정" 의 프로세스를 참조하시기 바랍니다.
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
                Response response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject,
                    txtUserId.Text);

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
         * [승인대기] 상태의 세금계산서를 [공급자]가 [취소]합니다.
         * - [취소]된 세금계산서를 삭제(Delete API)하면 등록된 문서관리번호를 재사용할 수 있습니다.
         */
        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 발행예정 취소 메모 
            string memo = "발행예정 취소 메모 ";

            try
            {
                Response response =
                    taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [승인대기] 상태의 세금계산서를 [공급받는자]가 [승인]합니다.
         */
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "발행예정 승인 메모";

            try
            {
                Response response =
                    taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [승인대기] 상태의 세금계산서를 [공급받는자]가 [거부]합니다.
         * - [거부]처리된 세금계산서를 삭제(Delete API)하면 등록된 문서관리번호를 재사용할 수 있습니다.
         */
        private void btnDeny_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            //메모
            string memo = "발행예정 거부 메모";

            try
            {
                Response response =
                    taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * 1건의 전자세금계산서를 [삭제]합니다.
         * - 세금계산서를 삭제해야만 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소], [발행예정 거부]
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
         * 1건의 전자세금계산서를 [삭제]합니다.
         * - 세금계산서를 삭제해야만 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소], [발행예정 거부]
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
         * 1건의 전자세금계산서를 [삭제]합니다.
         * - 세금계산서를 삭제해야만 문서관리번호(mgtKey)를 재사용할 수 있습니다.
         * - 삭제가능한 문서 상태 : [임시저장], [발행취소], [발행예정 취소], [발행예정 거부]
         */
        private void btnDelete_Reverse_sub_Click(object sender, EventArgs e)
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
         * [공급받는자]가 공급자에게 역발행 세금계산서를 [즉시 요청]합니다.
         * - 세금계산서 항목별 정보는 "[전자세금계산서 API 연동매뉴얼] > 4.1. (세금)계산서구성"을 참조하시기 바랍니다.
         * - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
         * - 역발행 즉시요청후 공급자가 [발행] 처리시 포인트가 차감되며 역발행 세금계산서 항목중 과금방향(ChargeDirection)에 기재한 값에 따라
         *   정과금(공급자과금) 또는 역과금(공급받는자과금) 처리됩니다.
         */
        private void btnRegistRequest_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            // [필수] 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20190110";

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

            // 공급자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerMgtKey = "";

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
            taxinvoice.invoiceeCorpNum = txtCorpNum.Text;

            // [필수] 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서관리번호, 숫자, 영문, '-', '_' 조합으로 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;

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
            // 미기재시 taxinvoice.kwon = null;
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            // 미기재시 taxinvoice.ho = null;
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

            detail.serialNum = 1; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재 
            detail.purchaseDT = "20190110"; // 거래일자
            detail.itemName = "품목명"; // 품목명 
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
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

            addContact.serialNum = 1; // 일련번호, 1부터 순차기재
            addContact.email = "test2@invoicee.com"; // 추가담당자 메일주소 
            addContact.contactName = "추가담당자명"; // 추가담당자 성명 

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재 
            addContact2.email = "test2@invoicee.com"; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 메모
            string memo = "즉시요청 메모";

            try
            {
                Response response = taxinvoiceService.RegistRequest(txtCorpNum.Text, taxinvoice, memo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 즉시요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 즉시요청");
            }
        }


        /*
         * [공급받는자]가 임시저장 상태의 역발행 세금계산서를 공급자에게 [발행요청] 합니다. 
         * - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
         * - 역발행 요청후 공급자가 [발행] 처리시 포인트가 차감되며 역발행 세금계산서 항목중 과금방향(ChargeDirection)에 기재한 값에 따라
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
                Response response =
                    taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [공급받는자]가 역)발행대기 상태의 세금계산서를 [취소]합니다. 
         * - [취소]한 세금계산서의 문서관리번호를 재사용하기 위해서는 삭제 (Delete API)를 호출해야 합니다.
         */
        private void btnCancelRequest_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "역발행 요청 취소 메모";

            try
            {
                Response response =
                    taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * [공급받는자]가 역)발행대기 상태의 세금계산서를 [취소]합니다. 
         * - [취소]한 세금계산서의 문서관리번호를 재사용하기 위해서는 삭제 (Delete API)를 호출해야 합니다.
         */
        private void btnCancelRequest_sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "역발행 요청 취소 메모";

            try
            {
                Response response =
                    taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

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
         * 공급받는자에게 요청받은 역)발행대기 상태의 세금계산서를 [공급자]가 [거부]합니다.
         * - 세금계산서의 문서관리번호를 재사용하기 위해서는 삭제 (Delete API)를 호출하여 [삭제] 처리해야 합니다.
         */
        private void btnRefuse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "역발행 요청 거부 메모";

            try
            {
                Response response =
                    taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        /*
         * 공급받는자에게 요청받은 역)발행대기 상태의 세금계산서를 [공급자]가 [거부]합니다.
         * - 세금계산서의 문서관리번호를 재사용하기 위해서는 삭제 (Delete API)를 호출하여 [삭제] 처리해야 합니다.
         */
        private void btnRefuse_sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모 
            string memo = "역발행 요청 거부 메모";

            try
            {
                Response response =
                    taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        /*
         * [발행완료] 상태의 세금계산서를 국세청으로 [즉시전송]합니다.
         * - 국세청 즉시전송을 호출하지 않은 세금계산서는 발행일 기준 익일 오후 3시에 팝빌 시스템에서 일괄적으로 국세청으로 전송합니다.
         * - 익일전송시 전송일이 법정공휴일인 경우 다음 영업일에 전송됩니다.
         * - 국세청 전송에 관한 사항은 "[전자세금계산서 API 연동매뉴얼] > 1.3 국세청 전송 정책" 을 참조하시기 바랍니다.
         */
        private void btnSendToNTS_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response =
                    taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

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
         * 1건의 세금계산서 상태/요약 정보를 확인합니다.
         * - 세금계산서 상태정보(GetInfo API) 응답항목에 대한 자세한 정보는
         *   "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                TaxinvoiceInfo taxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemKey (팝빌 관리번호) : " + taxinvoiceInfo.itemKey + CRLF;
                tmp += "taxType (과세형태) : " + taxinvoiceInfo.taxType + CRLF;
                tmp += "writeDate (작성일자) : " + taxinvoiceInfo.writeDate + CRLF;
                tmp += "regDT (임시저장 일자) : " + taxinvoiceInfo.regDT + CRLF;
                tmp += "issueType (발행형태) : " + taxinvoiceInfo.issueType + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + taxinvoiceInfo.supplyCostTotal + CRLF;
                tmp += "taxTotal (세액 합계) : " + taxinvoiceInfo.taxTotal + CRLF;
                tmp += "purposeType (영수/청구) : " + taxinvoiceInfo.purposeType + CRLF;
                tmp += "issueDT (발행일시) : " + taxinvoiceInfo.issueDT + CRLF;
                tmp += "lateIssueYN (지연발행 여부) : " + taxinvoiceInfo.lateIssueYN + CRLF;
                tmp += "preIssueDT (발행예정일시) : " + taxinvoiceInfo.preIssueDT + CRLF;
                tmp += "openYN (개봉 여부) : " + taxinvoiceInfo.openYN + CRLF;
                tmp += "openDT (개봉 일시) : " + taxinvoiceInfo.openDT + CRLF;
                tmp += "stateMemo (상태메모) : " + taxinvoiceInfo.stateMemo + CRLF;
                tmp += "stateCode (상태코드) : " + taxinvoiceInfo.stateCode + CRLF;
                tmp += "nstconfirmNum (국세청승인번호) : " + taxinvoiceInfo.ntsconfirmNum + CRLF;
                tmp += "ntsresult (국세청 전송결과) : " + taxinvoiceInfo.ntsresult + CRLF;
                tmp += "ntssendDT (국세청 전송일시) : " + taxinvoiceInfo.ntssendDT + CRLF;
                tmp += "ntsresultDT (국세청 결과 수신일시) : " + taxinvoiceInfo.ntsresultDT + CRLF;
                tmp += "ntssendErrCode (전송실패 사유코드) : " + taxinvoiceInfo.ntssendErrCode + CRLF;
                tmp += "modifyCode (수정 사유코드) : " + taxinvoiceInfo.modifyCode + CRLF;
                tmp += "interOPYN (연동문서 여부) : " + taxinvoiceInfo.interOPYN + CRLF;

                tmp += "invoicerCorpName (공급자 상호) : " + taxinvoiceInfo.invoicerCorpName + CRLF;
                tmp += "invoicerCorpNum (공급자 사업자번호) : " + taxinvoiceInfo.invoicerCorpNum + CRLF;
                tmp += "invoicerMgtKey (공급자 문서관리번호) : " + taxinvoiceInfo.invoicerMgtKey + CRLF;
                tmp += "invoicerPrintYN (공급자 인쇄여부) : " + taxinvoiceInfo.invoicerPrintYN + CRLF;

                tmp += "invoiceeCorpName (공급받는자 상호) : " + taxinvoiceInfo.invoiceeCorpName + CRLF;
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + taxinvoiceInfo.invoiceeCorpNum + CRLF;
                tmp += "invoiceePrintYN (공급받는자 문서관리번호) : " + taxinvoiceInfo.invoiceePrintYN + CRLF;
                tmp += "closeDownState (공급받는자 휴폐업상태코드) : " + taxinvoiceInfo.closeDownState + CRLF;
                tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + taxinvoiceInfo.closeDownStateDate + CRLF;

                tmp += "trusteeCorpName (수탁자 상호) : " + taxinvoiceInfo.trusteeCorpName + CRLF;
                tmp += "trusteeCorpNum (수탁자 사업자번호) : " + taxinvoiceInfo.trusteeCorpNum + CRLF;
                tmp += "trusteeMgtKey (수탁자 문서관리번호) : " + taxinvoiceInfo.trusteeMgtKey + CRLF;
                tmp += "trusteePrintYN (수탁자 인쇄여부) : " + taxinvoiceInfo.trusteePrintYN + CRLF;

                MessageBox.Show(tmp, "문서 상태/요약 정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문서 상태/요약 정보 조회");
            }
        }

        /*
         * 대량의 세금계산서 상태/요약 정보를 확인합니다. (최대 1000건)
         * - 세금계산서 상태정보(GetInfos API) 응답항목에 대한 자세한 정보는
         *   "[전자세금계산서 API 연동매뉴얼]  > 4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            //  조회할 세금계산서 문서관리번호 배열, (최대 1000건)
            MgtKeyList.Add("20190110-001");
            MgtKeyList.Add("20190110-002");
            MgtKeyList.Add("20190110-003");


            string tmp = "";

            try
            {
                List<TaxinvoiceInfo> taxinvoiceInfoList =
                    taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList);

                tmp = "writeDate(작성일자) | invoicerCorpName(공급자 상호) | invoicerCorpNum(공급자 사업자번호) | ";
                tmp += "invoiceeCorpName(공급받는자 상호) | invoiceeCorpNum(공급받는자 사업자번호) | taxTotal(공급가액 합계) |";
                tmp += "taxTotal(세액 합계) | closeDownState(휴폐업 상태) | closeDownStateDate(휴폐업 일자) " + CRLF + CRLF;
                foreach (TaxinvoiceInfo info in taxinvoiceInfoList)
                {
                    tmp += info.writeDate + " | " + info.invoicerCorpName + " | " + info.invoicerCorpNum + " | " +
                           info.invoiceeCorpName + " | " + info.invoiceeCorpNum + " | " + info.supplyCostTotal + " | " +
                           info.taxTotal + " | " + info.closeDownState + " | " + info.closeDownStateDate + CRLF;
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
         * 1건의 세금계산서 상세정보를 확인합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼] > 4.1 (세금)계산서 구성" 을 참조하시기 바랍니다.
         */
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Taxinvoice taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                string tmp = null;

                tmp += "ntsconfirmNum (국세청승인번호) : " + taxinvoice.ntsconfirmNum + CRLF;
                tmp += "issueType (발행형태) : " + taxinvoice.issueType + CRLF;
                tmp += "taxType (과세형태) : " + taxinvoice.taxType + CRLF;
                tmp += "issueTiming (발행시점) : " + taxinvoice.issueTiming + CRLF;
                tmp += "chargeDirection (과금방향) : " + taxinvoice.chargeDirection + CRLF;
                tmp += "serialNum (일련번호) : " + taxinvoice.serialNum + CRLF;
                tmp += "kwon (권) : " + taxinvoice.kwon + CRLF;
                tmp += "ho (호) : " + taxinvoice.ho + CRLF;
                tmp += "writeDate (작성일자) : " + taxinvoice.writeDate + CRLF;
                tmp += "purposeType (영수/청구) : " + taxinvoice.purposeType + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + taxinvoice.supplyCostTotal + CRLF;
                tmp += "taxTotal (세액 합계) : " + taxinvoice.taxTotal + CRLF;
                tmp += "totalAmount (합계금액) : " + taxinvoice.totalAmount + CRLF;
                tmp += "cash (현금) : " + taxinvoice.cash + CRLF;
                tmp += "chkBill (수표) : " + taxinvoice.chkBill + CRLF;
                tmp += "credit (외상) : " + taxinvoice.credit + CRLF;
                tmp += "note (어음) : " + taxinvoice.note + CRLF;
                tmp += "remark1 (비고1) : " + taxinvoice.remark1 + CRLF;
                tmp += "remark2 (비고2) : " + taxinvoice.remark2 + CRLF;
                tmp += "remakr3 (비고3) : " + taxinvoice.remark3 + CRLF;

                tmp += "invoicerMgtKey (공급자 관리번호) : " + taxinvoice.invoicerMgtKey + CRLF;
                tmp += "invoicerCorpNum (공급자 사업자번호) : " + taxinvoice.invoicerCorpNum + CRLF;
                tmp += "invoicerTaxRegID (공급자 종사업장 식별번호) : " + taxinvoice.invoicerTaxRegID + CRLF;
                tmp += "invoicerCorpName (공급자 상호) : " + taxinvoice.invoicerCorpName + CRLF;
                tmp += "invoicerCEOName (공급자 대표자성명) : " + taxinvoice.invoicerCEOName + CRLF;
                tmp += "invoicerAddr (공급자 주소) : " + taxinvoice.invoicerAddr + CRLF;
                tmp += "invoicerBizClass (공급자 종목) : " + taxinvoice.invoicerBizClass + CRLF;
                tmp += "invoicerBizType (공급자 업태) : " + taxinvoice.invoicerBizType + CRLF;
                tmp += "invoicerContactName (담당자 성명) : " + taxinvoice.invoicerContactName + CRLF;
                tmp += "invoicerTEL (담당자 연락처) : " + taxinvoice.invoicerTEL + CRLF;
                tmp += "invoicerHP (담당자 휴대폰) : " + taxinvoice.invoicerHP + CRLF;
                tmp += "invoicerEmail (담당자 이메일) : " + taxinvoice.invoicerEmail + CRLF;
                tmp += "invoicerSMSSendYN (문자전송 여부) : " + taxinvoice.invoicerSMSSendYN + CRLF;

                tmp += "invoiceeMgtKey (공급받는자 문서관리번호) : " + taxinvoice.invoiceeMgtKey + CRLF;
                tmp += "invoiceeType (공급받는자 구분) : " + taxinvoice.invoiceeType + CRLF;
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + taxinvoice.invoiceeCorpNum + CRLF;
                tmp += "invoiceeTaxRegID (공급받는자 종사업장 식별번호) : " + taxinvoice.invoiceeTaxRegID + CRLF;
                tmp += "invoiceeCorpName (공급받는자 상호) : " + taxinvoice.invoiceeCorpName + CRLF;
                tmp += "invoiceeCEOName (공급받는자 대표자 성명) : " + taxinvoice.invoiceeCEOName + CRLF;
                tmp += "invoiceeAddr (공급받는자 주소) : " + taxinvoice.invoiceeAddr + CRLF;
                tmp += "invoiceeBizType (공급받는자 업태) : " + taxinvoice.invoiceeBizType + CRLF;
                tmp += "invoiceeBizClass (공급받는자 종목) : " + taxinvoice.invoiceeBizClass + CRLF;
                tmp += "closeDownState (휴폐업상태) : " + taxinvoice.closeDownState + CRLF;
                tmp += "closeDownStateDate (휴폐업일자) : " + taxinvoice.closeDownStateDate + CRLF;
                tmp += "invoiceeContactName1 (담당자 성명) : " + taxinvoice.invoiceeContactName1 + CRLF;
                tmp += "invoiceeTEL1 (담당자 연락처) : " + taxinvoice.invoiceeTEL1 + CRLF;
                tmp += "invoiceeHP1 (담당자 휴대폰) : " + taxinvoice.invoiceeHP1 + CRLF;
                tmp += "invoiceeEmail1 (담당자 이메일) : " + taxinvoice.invoiceeEmail1 + CRLF;

                tmp += "modifyCode (수정 사유코드) : " + taxinvoice.modifyCode + CRLF;
                tmp += "orgNTSConfirmNum (원본 국세청승인번호) : " + taxinvoice.orgNTSConfirmNum + CRLF;
                tmp += "originalTaxinvoiceKey (원본 팝빌 관리번호) : " + taxinvoice.originalTaxinvoiceKey + CRLF + CRLF;

                if (!taxinvoice.detailList.Equals(null))
                {
                    tmp += "[detailList]" + CRLF;
                    for (int i = 0; i < taxinvoice.detailList.Count; i++)
                    {
                        tmp += "serialNum (일련번호) : " + taxinvoice.detailList[i].serialNum.ToString() + CRLF;
                        //tmp += "purchaseDT (거래일자): " + taxinvoice.detailList[i].purchaseDT + CRLF;
                        //tmp += "itemName (품목명) : " + taxinvoice.detailList[i].itemName + CRLF;
                        //tmp += "spec (규격) : " + taxinvoice.detailList[i].spec + CRLF;
                        //tmp += "qty (수량) : " + taxinvoice.detailList[i].qty + CRLF;
                        //tmp += "supplyCost (공급가액) : " + taxinvoice.detailList[i].supplyCost + CRLF;
                        //tmp += "tax (세액) : " + taxinvoice.detailList[i].tax + CRLF;
                        //tmp += "unitCost (단가) : " + taxinvoice.detailList[i].unitCost + CRLF;
                        //tmp += "reamark (비고) : " + taxinvoice.detailList[i].remark + CRLF + CRLF;
                    }

                    tmp += CRLF;
                }

                if (!taxinvoice.addContactList.Equals(null))
                {
                    tmp += "[addContactList]" + CRLF;
                    for (int i = 0; i < taxinvoice.addContactList.Count; i++)
                    {
                        tmp += "serialNum (일련번호) : " + taxinvoice.addContactList[i].serialNum.ToString() + CRLF;
                        //tmp += "contactName (담당자 성명) : " + taxinvoice.addContactList[i].contactName + CRLF;
                        //tmp += "email (이메일 주소) : " + taxinvoice.addContactList[i].email + CRLF + CRLF;
                    }

                    tmp += CRLF;
                }


                MessageBox.Show(tmp, "세금계산서 상세정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 상세정보 확인");
            }
        }

        /*
         * 검색조건을 사용하여 세금계산서 목록을 조회합니다.
         * - 응답항목에 대한 자세한 사항은 "[전자세금계산서 API 연동매뉴얼] > 4.2. (세금)계산서 상태정보 구성" 을 참조하시기 바랍니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // [필수] 일자유형, R-등록일자, I-발행일자, W-작성일자 중 1개기입
            String DType = "W";

            // [필수] 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20181201";

            // [필수] 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20190110";

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
                TISearchResult searchResult = taxinvoiceService.Search(txtCorpNum.Text, KeyType, DType, SDate, EDate,
                    State,
                    Type, TaxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page,
                    PerPage, InterOPYN, txtUserId.Text);

                String tmp = null;

                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF + CRLF;

                tmp +=
                    "itemKey | taxType | writeDate | regDT | invoicerCorpNum | invoicerCorpName | invoicerMgtKey | invoicerPrintYN |";
                tmp +=
                    " invoiceeCorpNum | invoiceeCorpName | invoiceeMgtKey | invoiceePrintYN | closeDownState | closeDownStateDate | ";
                tmp +=
                    "supplyCostTotal | taxTotal | purposeType | issueDT | stateCode | stateDT | lateIssueYN | interOPYN";
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
         * 세금계산서 상태 변경이력을 확인합니다.
         * - 상태 변경이력 확인(GetLogs API) 응답항목에 대한 자세한 정보는
         *   "[전자세금계산서 API 연동매뉴얼] > 3.5.5 상태 변경이력 확인" 을 참조하시기 바랍니다.
         */
        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                String tmp = "";

                List<TaxinvoiceLog> logList = taxinvoiceService.GetLogs(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                tmp += "docType(로그타입) | log(이력정보) | procType(처리형태) | procContactName(처리담당자) |";
                tmp += "procMemo(처리메모) | regDT(등록일시) | ip(아이피)" + CRLF + CRLF;
                foreach (TaxinvoiceLog log in logList)
                {
                    tmp += log.docLogType + " | " + log.log + " | " + log.procType + " | " + log.procContactName +
                           " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + CRLF;
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
         * 팝빌 > 전자세금계산서 > 임시(연동)문서함 팝업 URL을 반환합니다.
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
         * 팝빌 > 전자세금계산서 > 매출 문서함 팝업 URL을 반환합니다.
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
         * 팝빌 > 전자세금계산서 > 매입 문서함 팝업 URL을 반환합니다.
         * - 보안정책으로 인해 반환된 URL의 유효시간은 30초입니다.
         */
        private void btnGetURL_PBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

                MessageBox.Show(url, "매입문서함 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "매입문서함 팝업 URL");
            }
        }

        /*
         *  팝빌 > 전자세금계산서 > 매출 문서작성 팝업 URL을 반환합니다.
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
         * 1건의 전자세금계산서 보기 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
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
         * 대량의 세금계산서 인쇄팝업 URL을 반환합니다. (최대 100건)
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            // 인쇄할 세금계산서 문서관리번호, (최대 100건)
            MgtKeyList.Add("20190110-001");
            MgtKeyList.Add("20190110-002");
            MgtKeyList.Add("20190110-003");
            MgtKeyList.Add("20190110-004");

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
         * 팝빌에 로그인 상태로 접근할 수 있는 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

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
        private void btnGetSealURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetSealURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "인감 및 첨부문서 등록 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "인감 및 첨부문서 등록 URL");
            }
        }

        /*
         * 세금계산서에 첨부파일을 등록합니다.
         * - [임시저장] 상태의 세금계산서만 파일을 첨부할수 있습니다.
         * - 첨부파일은 최대 5개까지 등록할 수 있습니다.
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
                    Response response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                        strFileName, txtUserId.Text);

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
         * 세금계산서에 첨부된 파일을 삭제합니다.
         * - 파일을 식별하는 파일아이디는 첨부파일 목록(GetFiles API) 의 응답항목 중 파일아이디(AttachedFile) 값을 통해 확인할 수 있습니다.
         */
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                    txtFileID.Text, txtUserId.Text);

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
         * 세금계산서 첨부파일 목록을 확인합니다.
         * - 응답항목 중 파일아이디(AttachedFile) 항목은 파일삭제(DeleteFile API) 호출시 이용할 수 있습니다.
         */
        private void gtnGetFiles_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                List<AttachedFile> fileList = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text);


                string tmp = "serialNum(일련번호) | displayName(첨부파일명) | attachedFile(파일아이디) | regDT(등록일자)" + CRLF;

                foreach (AttachedFile file in fileList)
                {
                    tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " +
                           file.regDT + CRLF;

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
         * 세금계산서 발행안내 메일을 재전송합니다.
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 수신메일주소
            String email = "test@test.com";

            try
            {
                Response response =
                    taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, email, txtUserId.Text);

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
         * 알림문자를 전송합니다. (단문/SMS - 한글 최대 45자)
         * - 알림문자 전송시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인할 수 있습니다.
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
                Response response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, senderNum,
                    receiverNum, contents, txtUserId.Text);

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
         * - 전송내역 확인은 "팝빌 로그인" > [문자 팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인할 수 있습니다.
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
                Response response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, senderNum,
                    receiverNum, txtUserId.Text);

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
         * 1건의 전자명세서를 세금계산서에 첨부합니다 
         */
        private void btnAttachStmt_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형 
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 첨부할 명세서 종류 코드
            int DocItemCode = 121;

            // 첨부할 명세서 관리번호
            String DocMgtKey = "20190110-001";

            try
            {
                Response response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                    DocItemCode, DocMgtKey);
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
            String DocMgtKey = "20190110-001";

            try
            {
                Response response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                    DocItemCode, DocMgtKey);

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
         * 대용량 연계사업자 유통메일주소 목록을 반환합니다.
         */
        private void btnGetEmailPublicKey_Click(object sender, EventArgs e)
        {
            string tmp = "";

            try
            {
                List<EmailPublicKey> KeyList = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text);

                foreach (EmailPublicKey info in KeyList)
                {
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
         * 팝빌사이트에서 작성된 세금계산서에 파트너 문서관리번호를 할당합니다.
         */
        private void btnAssignMgtKey_Click(object sender, EventArgs e)
        {
            // 세금계산서 유형 SELL-매출, BUY-매입, TRUSTEE-위수탁
            MgtKeyType KeyType = MgtKeyType.SELL;

            // 세금계산서 아이템키, 목록조회(Search) API의 반환항목중 ItemKey 참조
            String itemKey = "018041823243600001";

            // 할당할 문서관리번호, 숫자, 영문, '-', '_' 조합으로 
            // 1~24자리까지 사업자번호별 중복없는 고유번호 할당
            String mgtKey = "1-20190110";

            try
            {
                Response response = taxinvoiceService.AssignMgtKey(txtCorpNum.Text, KeyType, itemKey, mgtKey);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "세금계산서 관리번호 할당");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "세금계산서 관리번호 할당");
            }
        }

        /*
         * 전자세금계산서 관련 메일전송 항목에 대한 전송여부를 목록으로 반환한다.
         */
        private void btnListEmailConfig_Click(object sender, EventArgs e)
        {
            String tmp = "";
            try
            {
                List<EmailConfig> resultList = taxinvoiceService.ListEmailConfig(txtCorpNum.Text, txtUserId.Text);

                tmp = "메일전송유형 | 전송여부" + CRLF;

                foreach (EmailConfig info in resultList)
                {
                    if (info.emailType == "TAX_ISSUE")
                        tmp += "[정발행] TAX_ISSUE (공급받는자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_ISSUE_INVOICER")
                        tmp += "[정발행] TAX_ISSUE_INVOICER (공급자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CHECK")
                        tmp += "[정발행] TAX_CHECK (공급자에게 전자세금계산서 수신확인 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CANCEL_ISSUE")
                        tmp += "[정발행] TAX_CANCEL_ISSUE (공급받는자에게 전자세금계산서 발행취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_SEND")
                        tmp += "[발행예정] TAX_SEND (공급받는자에게 [발행예정] 세금계산서 발송 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_ACCEPT")
                        tmp += "[발행예정] TAX_ACCEPT (공급자에게 [발행예정] 세금계산서 승인 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_ACCEPT_ISSUE")
                        tmp += "[발행예정] TAX_ACCEPT_ISSUE (공급자에게 [발행예정] 세금계산서 자동발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_DENY")
                        tmp += "[발행예정] TAX_DENY (공급자에게 [발행예정] 세금계산서 거부 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CANCEL_SEND")
                        tmp += "[발행예정] TAX_CANCEL_SEND (공급받는자에게 [발행예정] 세금계산서 취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_REQUEST")
                        tmp += "[역발행] TAX_REQUEST (공급자에게 세금계산서를 발행요청 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CANCEL_REQUEST")
                        tmp += "[역발행] TAX_CANCEL_REQUEST (공급받는자에게 세금계산서 취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_REFUSE")
                        tmp += "[역발행] TAX_REFUSE (공급받는자에게 세금계산서 거부 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_ISSUE")
                        tmp += "[위수탁 발행] TAX_TRUST_ISSUE (공급받는자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_ISSUE_TRUSTEE")
                        tmp += "[위수탁 발행] TAX_TRUST_ISSUE_TRUSTEE (수탁자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_ISSUE_INVOICER")
                        tmp += "[위수탁 발행] TAX_TRUST_ISSUE_INVOICER (공급자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_CANCEL_ISSUE")
                        tmp += "[위수탁 발행] TAX_TRUST_CANCEL_ISSUE (공급받는자에게 전자세금계산서 발행취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_CANCEL_ISSUE_INVOICER")
                        tmp += "[위수탁 발행] TAX_TRUST_CANCEL_ISSUE_INVOICER (공급자에게 전자세금계산서 발행취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_SEND")
                        tmp += "[위수탁 발행예정] TAX_TRUST_SEND (공급받는자에게 [발행예정] 세금계산서 발송 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_ACCEPT")
                        tmp += "[위수탁 발행예정] TAX_TRUST_ACCEPT (수탁자에게 [발행예정] 세금계산서 승인 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_ACCEPT_ISSUE")
                        tmp += "[위수탁 발행예정] TAX_TRUST_ACCEPT_ISSUE (수탁자에게 [발행예정] 세금계산서 자동발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_DENY")
                        tmp += "[위수탁 발행예정] TAX_TRUST_DENY (수탁자에게 [발행예정] 세금계산서 거부 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_TRUST_CANCEL_SEND")
                        tmp += "[위수탁 발행예정] TAX_TRUST_CANCEL_SEND (공급받는자에게 [발행예정] 세금계산서 취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CLOSEDOWN")
                        tmp += "[처리결과] TAX_CLOSEDOWN (거래처의 휴폐업 여부 확인 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_NTSFAIL_INVOICER")
                        tmp += "[처리결과] TAX_NTSFAIL_INVOICER (전자세금계산서 국세청 전송실패 안내) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_SEND_INFO")
                        tmp += "[정기발송] TAX_SEND_INFO (전월 귀속분 [매출 발행 대기] 세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "ETC_CERT_EXPIRATION")
                        tmp += "[정기발송] ETC_CERT_EXPIRATION (팝빌에서 이용중인 공인인증서의 갱신 메일) | " + info.sendYN + CRLF;
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
          전자세금계산서 메일전송 항목에 대한 전송여부를 수정합니다.
          메일전송유형
          [정발행]
          TAX_ISSUE : 공급받는자에게 전자세금계산서 발행 메일 입니다.
          TAX_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 메일 입니다.
          TAX_CHECK : 공급자에게 전자세금계산서 수신확인 메일 입니다.
          TAX_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 메일 입니다.

          [발행예정]
          TAX_SEND : 공급받는자에게 [발행예정] 세금계산서 발송 메일 입니다.
          TAX_ACCEPT : 공급자에게 [발행예정] 세금계산서 승인 메일 입니다.
          TAX_ACCEPT_ISSUE : 공급자에게 [발행예정] 세금계산서 자동발행 메일 입니다.
          TAX_DENY : 공급자에게 [발행예정] 세금계산서 거부 메일 입니다.
          TAX_CANCEL_SEND : 공급받는자에게 [발행예정] 세금계산서 취소 메일 입니다.

          [역발행]
          TAX_REQUEST : 공급자에게 세금계산서를 발행요청 메일 입니다.
          TAX_CANCEL_REQUEST : 공급받는자에게 세금계산서 취소 메일 입니다.
          TAX_REFUSE : 공급받는자에게 세금계산서 거부 메일 입니다.

          [위수탁발행]
          TAX_TRUST_ISSUE : 공급받는자에게 전자세금계산서 발행 메일 입니다.
          TAX_TRUST_ISSUE_TRUSTEE : 수탁자에게 전자세금계산서 발행 메일 입니다.
          TAX_TRUST_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 메일 입니다.
          TAX_TRUST_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 메일 입니다.
          TAX_TRUST_CANCEL_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행취소 메일 입니다.

          [위수탁 발행예정]
          TAX_TRUST_SEND : 공급받는자에게 [발행예정] 세금계산서 발송 메일 입니다.
          TAX_TRUST_ACCEPT : 수탁자에게 [발행예정] 세금계산서 승인 메일 입니다.
          TAX_TRUST_ACCEPT_ISSUE : 수탁자에게 [발행예정] 세금계산서 자동발행 메일 입니다.
          TAX_TRUST_DENY : 수탁자에게 [발행예정] 세금계산서 거부 메일 입니다.
          TAX_TRUST_CANCEL_SEND : 공급받는자에게 [발행예정] 세금계산서 취소 메일 입니다.

          [처리결과]
          TAX_CLOSEDOWN : 거래처의 휴폐업 여부 확인 메일 입니다.
          TAX_NTSFAIL_INVOICER : 전자세금계산서 국세청 전송실패 안내 메일 입니다.

          [정기발송]
          TAX_SEND_INFO : 전월 귀속분 [매출 발행 대기] 세금계산서 발행 메일 입니다.
          ETC_CERT_EXPIRATION : 팝빌에서 이용중인 공인인증서의 갱신 메일 입니다.
         */
        private void btnUpdateEmailConfig_Click(object sender, EventArgs e)
        {
            String EmailType = "TAX_ISSUE";

            //전송여부 (True-전송, False-미전송)
            bool SendYN = true;

            try
            {
                Response response =
                    taxinvoiceService.UpdateEmailConfig(txtCorpNum.Text, EmailType, SendYN, txtUserId.Text);


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
         * 팝빌 회원의 공인인증서를 등록하는 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         * - 팝빌에 등록된 공인인증서가 유효하지 않은 경우 (비밀번호 변경, 인증서 재발급/갱신, 만료일 경과)
         *   인증서를 재등록해야 정상적으로 전자세금계산서 발행이 가능합니다.
         */
        private void btnGetTaxCertURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetTaxCertURL(txtCorpNum.Text, txtUserId.Text);

                System.Diagnostics.Process.Start("IExplore.exe", url);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "공인인증서 등록 URL");
            }
        }

        /*
         * 팝빌에 등록되어 있는 공인인증서의 만료일자를 확인합니다.
         * - 공인인증서가 갱신/재발급/비밀번호 변경이 되는 경우 해당 인증서를
         *   재등록 하셔야 정상적으로 세금계산서를 발행할 수 있습니다.
         */
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
         * 팝빌에 등록된 공인인증서의 유효성을 확인합니다.
         */
        private void btnCheckCertValidation_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = taxinvoiceService.CheckCertValidation(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "공인인증서 유효성 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "공인인증서 유효성 확인");
            }
        }

        /*
         * 연동회원 잔여포인트를 확인합니다.
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
         * 팝빌 연동회원의 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "연동회원 포인트 충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
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
         * 파트너 포인트 충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
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
         * 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
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
         * 팝빌 회원아이디 중복여부를 확인합니다.
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
    }
}