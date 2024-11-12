/*
* 팝빌 전자세금계산서 API .NET SDK C#.NET Example
* C#.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/taxinvoice/dotnet/getting-started/tutorial?fwn=csharp
*
* 업데이트 일자 : 2024-11-11
* 연동기술지원 연락처 : 1600-9854
* 연동기술지원 이메일 : code@linkhubcorp.com
*         
* <테스트 연동개발 준비사항>
* 1) API Key 변경 (연동신청 시 메일로 전달된 정보)
*     - LinkID : 링크허브에서 발급한 링크아이디
*     - SecretKey : 링크허브에서 발급한 비밀키
* 2) SDK 환경설정 옵션 설정
*     - IsTest : 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
*     - IPRestrictOnOff : 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
*     - UseStaticIP : 통신 IP 고정, true-사용, false-미사용, (기본값:false)
*     - UseLocalTimeYN : 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
* 3) 전자세금계산서 발행을 위해 공동인증서를 등록합니다.
*    - 팝빌사이트 로그인 > [전자세금계산서] > [환경설정] > [공동인증서 관리]
*    - 공동인증서 등록 팝업 URL (GetTaxCertURL API)을 이용하여 등록
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

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

            // 세금계산서 서비스 객체 초기화
            taxinvoiceService = new TaxinvoiceService(LinkID, SecretKey);

            // 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
            taxinvoiceService.IsTest = true;

            // 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
            taxinvoiceService.IPRestrictOnOff = true;

            // 통신 IP 고정, true-사용, false-미사용, (기본값:false)
            taxinvoiceService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
            taxinvoiceService.UseLocalTimeYN = false;
        }


        /*
         * 파트너가 세금계산서 관리 목적으로 할당하는 문서번호의 사용여부를 확인합니다.
         * - 이미 사용 중인 문서번호는 중복 사용이 불가하고, 세금계산서가 삭제된 경우에만 문서번호의 재사용이 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#CheckMgtKeyInUse
         */
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                bool InUse = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show((InUse ? "사용중" : "미사용중"), "문서번호 중복확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문서번호 중복확인");
            }
        }

        /*
         * 작성된 세금계산서 데이터를 팝빌에 저장과 동시에 발행(전자서명)하여 "발행완료" 상태로 처리합니다.
         * - 세금계산서 국세청 전송 정책 [https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/policy-of-send-to-nts]
         * - "발행완료"된 전자세금계산서는 국세청 전송 이전에 발행취소(CancelIssue API) 함수로 국세청 신고 대상에서 제외할 수 있습니다.
         * - 임시저장(Register API) 함수와 발행(Issue API) 함수를 한 번의 프로세스로 처리합니다.
         * - 세금계산서 발행을 위해서 공급자의 인증서가 팝빌 인증서버에 사전등록 되어야 합니다.
         *   └ 위수탁발행의 경우, 수탁자의 인증서 등록이 필요합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#RegistIssue
         */
        private void btnRegistIssue_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체
            Taxinvoice taxinvoice = new Taxinvoice();

            // 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20220504";

            // 과금방향, {정과금, 역과금} 중 기재
            // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금";

            // 발행형태, {정발행, 역발행, 위수탁} 중 기재
            taxinvoice.issueType = "정발행";

            // {영수, 청구, 없음} 중 기재
            taxinvoice.purposeType = "영수";

            // 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";

            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = txtCorpNum.Text;

            // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // 공급자 대표자 성명
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
            taxinvoice.invoicerEmail = "";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "";

            // 공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = "";

            // 발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
            taxinvoice.invoiceeType = "사업자";

            // 공급받는자 사업자번호
            // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = "8888888888";

            // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoiceeTaxRegID = "";

            // 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // 공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "";

            // 공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소
            // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = "";

            // 공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = "";

            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // 세액 합계
            taxinvoice.taxTotal = "10000";

            // 합계금액,  공급가액 합계 + 세액 합계
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

            // 비고
            // {invoiceeType}이 "외국인" 이면 remark1 필수
            // - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;

            // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = false;

            /**************************************************************************
             *                      수정 세금계산서 기재정보
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
             * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = "";


            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; //비고

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

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
            addContact.email = ""; // 추가담당자 메일주소
            addContact.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재
            addContact2.email = ""; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 지연발행 강제여부  (true / false 중 택 1)
            // └ true = 가능 , false = 불가능
            // - 미입력 시 기본값 false 처리
            // - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
            // - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
            //   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
            bool forceIssue = false;

            // 즉시발행 메모
            String memo = "즉시발행 메모";

            // 거래명세서 동시작성여부 (true / false 중 택 1)
            // └ true = 사용 , false = 미사용
            // - 미입력 시 기본값 false 처리
            bool writeSpecification = false;

            // {writeSpecification} = true인 경우, 거래명세서 문서번호 할당
            // - 미입력시 기본값 세금계산서 문서번호와 동일하게 할당
            String dealInvoiceMgtKey = "";

            // 발행안내메일 제목, 미기재시 기본양식으로 전송
            String emailSubject = "";

            try
            {
                IssueResponse response = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo,
                    writeSpecification, dealInvoiceMgtKey, emailSubject);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message  + CRLF +
                                "국세청승인번호(ntsConfirmNum) : " + response.ntsConfirmNum, "즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "즉시발행");
            }
        }

        /*
         * 최대 100건의 세금계산서 발행을 한번의 요청으로 접수합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#BulkSubmit
         */
        private void btnBulkSubmit_Click(object sender, EventArgs e)
        {
            // 세금계산서 객체정보 목록
            List<Taxinvoice> taxinvoiceList = new List<Taxinvoice>();

            // 지연발행 강제여부  (true / false 중 택 1)
            // └ true = 가능 , false = 불가능
            // - 미입력 시 기본값 false 처리
            // - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
            // - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
            //   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
            bool forceIssue = false;

            for (int i = 0; i < 100; i++)
            {
                // 세금계산서 정보 객체
                Taxinvoice taxinvoice = new Taxinvoice();

                // 기재상 작성일자, 날짜형식(yyyyMMdd)
                taxinvoice.writeDate = "20220504";

                // 과금방향, {정과금, 역과금} 중 기재
                // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
                // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
                taxinvoice.chargeDirection = "정과금";

                // 발행형태, {정발행, 역발행, 위수탁} 중 기재
                taxinvoice.issueType = "정발행";

                // {영수, 청구, 없음} 중 기재
                taxinvoice.purposeType = "영수";

                // 과세형태, {과세, 영세, 면세} 중 기재
                taxinvoice.taxType = "과세";


                /*****************************************************************
                 *                         공급자 정보                           *
                 *****************************************************************/

                // 공급자 사업자번호, '-' 제외 10자리
                taxinvoice.invoicerCorpNum = "1234567890";

                // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
                taxinvoice.invoicerTaxRegID = "";

                // 공급자 상호
                taxinvoice.invoicerCorpName = "공급자 상호";

                // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
                taxinvoice.invoicerMgtKey = txtSubmitID.Text + "-" + i;

                // 공급자 대표자 성명
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
                taxinvoice.invoicerEmail = "";

                // 공급자 담당자 연락처
                taxinvoice.invoicerTEL = "";

                // 공급자 담당자 휴대폰번호
                taxinvoice.invoicerHP = "";

                // 발행 안내 문자 전송여부 (true / false 중 택 1)
                // └ true = 전송 , false = 미전송
                // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
                // - 전송 시 포인트 차감되며, 전송실패시 환불처리
                taxinvoice.invoicerSMSSendYN = false;


                /*********************************************************************
                 *                         공급받는자 정보                           *
                 *********************************************************************/

                // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
                taxinvoice.invoiceeType = "사업자";

                // 공급받는자 사업자번호
                // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
                // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
                // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
                taxinvoice.invoiceeCorpNum = "8888888888";

                // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
                taxinvoice.invoiceeTaxRegID = "";

                // 공급받는자 상호
                taxinvoice.invoiceeCorpName = "공급받는자 상호";

                // 공급받는자 대표자 성명
                taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

                // 공급받는자 주소
                taxinvoice.invoiceeAddr = "공급받는자 주소";

                // 공급받는자 종목
                taxinvoice.invoiceeBizClass = "공급받는자 종목";

                // 공급받는자 업태
                taxinvoice.invoiceeBizType = "공급받는자 업태";

                // 공급받는자 담당자 연락처
                taxinvoice.invoiceeTEL1 = "";

                // 공급받는자 담당자명
                taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

                // 공급받는자 담당자 메일주소
                // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
                // 실제 거래처의 메일주소가 기재되지 않도록 주의
                taxinvoice.invoiceeEmail1 = "";

                // 공급받는자 담당자 휴대폰번호
                taxinvoice.invoiceeHP1 = "";

                /*********************************************************************
                 *                          세금계산서 정보                          *
                 *********************************************************************/

                // 공급가액 합계
                taxinvoice.supplyCostTotal = "100000";

                // 세액 합계
                taxinvoice.taxTotal = "10000";

                // 합계금액,  공급가액 합계 + 세액 합계
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

                // 비고
                // {invoiceeType}이 "외국인" 이면 remark1 필수
                // - 외국인 등록번호 또는 여권번호 입력
                taxinvoice.remark1 = "비고1";
                taxinvoice.remark2 = "비고2";
                taxinvoice.remark3 = "비고3";

                // 기재상 권 항목, 최대값 32767
                taxinvoice.kwon = 1;

                // 기재상 호 항목, 최대값 32767
                taxinvoice.ho = 1;

                // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
                // └ true = 첨부 , false = 미첨부(기본값)
                // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
                taxinvoice.businessLicenseYN = false;

                // 통장사본 이미지 첨부여부 (true / false 중 택 1)
                // └ true = 첨부 , false = 미첨부(기본값)
                // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
                taxinvoice.bankBookYN = false;

                /**************************************************************************
                 *                      수정 세금계산서 기재정보
                 * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
                 * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
                 *************************************************************************/

                // 수정사유코드, 1~6까지 선택기재.
                taxinvoice.modifyCode = null;

                // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
                taxinvoice.orgNTSConfirmNum = "";

                /**************************************************************************
                 *                         상세항목(품목) 정보                            *
                 * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
                 *   세금계산서 발행이 가능합니다.                                        *
                 * - 최대 99건까지 작성가능                                               *
                 **************************************************************************/

                taxinvoice.detailList = new List<TaxinvoiceDetail>();

                TaxinvoiceDetail detail = new TaxinvoiceDetail();

                detail.serialNum = 1; // 일련번호, 1부터 순차기재
                detail.purchaseDT = "20220504"; // 거래일자
                detail.itemName = "품목명"; // 품목명
                detail.spec = "규격"; // 규격
                detail.qty = "1"; // 수량
                detail.unitCost = "50000"; // 단가
                detail.supplyCost = "50000"; // 공급가액
                detail.tax = "5000"; // 세액
                detail.remark = "품목비고"; //비고

                taxinvoice.detailList.Add(detail);

                detail = new TaxinvoiceDetail();

                detail.serialNum = 2; // 일련번호, 1부터 순차기재
                detail.purchaseDT = "20220504"; // 거래일자
                detail.itemName = "품목명"; // 품목명
                detail.spec = "규격"; // 규격
                detail.qty = "1"; // 수량
                detail.unitCost = "50000"; // 단가
                detail.supplyCost = "50000"; // 공급가액
                detail.tax = "5000"; // 세액
                detail.remark = "품목비고"; // 비고

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
                addContact.email = ""; // 추가담당자 메일주소
                addContact.contactName = "추가담당자명"; // 추가담당자 성명

                taxinvoice.addContactList.Add(addContact);

                TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

                addContact2.serialNum = 2; // 일련번호, 1부터 순차기재
                addContact2.email = ""; // 추가담당자 메일주소
                addContact2.contactName = "추가담당자명"; // 추가담당자 성명

                taxinvoice.addContactList.Add(addContact2);

                taxinvoiceList.Add(taxinvoice);
            }

            try
            {
                BulkResponse response = taxinvoiceService.BulkSubmit(txtCorpNum.Text, txtSubmitID.Text, taxinvoiceList, forceIssue);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message + CRLF +
                                "접수아이디(receiptID) : " + response.receiptID, "초대량 발행 접수");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "초대량 발행 접수");
            }
        }

        /*
         * 접수시 기재한 SubmitID를 사용하여 세금계산서 접수결과를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#GetBulkResult
         */
        private void btnGetBulkResult_Click(object sender, EventArgs e)
        {
            try
            {
                BulkTaxinvoiceResult result = taxinvoiceService.GetBulkResult(txtCorpNum.Text, txtSubmitID.Text);

                String tmp = null;

                tmp += "응답 코드(code) : " + result.code + CRLF;
                tmp += "응답메시지(message) : " + result.message + CRLF;
                tmp += "제출아이디(submitID) : " + result.submitID + CRLF;
                tmp += "세금계산서 접수 건수(submitCount) : " + result.submitCount + CRLF;
                tmp += "세금계산서 발행 성공 건수(successCount) : " + result.successCount + CRLF;
                tmp += "세금계산서 발행 실패 건수(failCount) : " + result.failCount + CRLF;
                tmp += "접수상태코드(txState) : " + result.txState + CRLF;
                tmp += "접수 결과코드(txResultCode) : " + result.txResultCode + CRLF;
                tmp += "발행처리 시작일시(txStartDT) : " + result.txStartDT + CRLF;
                tmp += "발행처리 완료일시(txEndDT) : " + result.txEndDT + CRLF;
                tmp += "접수일시(receiptDT) : " + result.receiptDT + CRLF;
                tmp += "접수아이디(receiptID) : " + result.receiptID + CRLF;

                if (result.issueResult != null)
                {
                    int i = 1;
                    foreach (BulkTaxinvoiceIssueResult issueResult in result.issueResult)
                    {
                        tmp += "===========발행결과[" + i.ToString() + "/" + result.issueResult.Count + "]===========" + CRLF;
                        tmp += "공급자 문서번호(invoicerMgtKey) : " + issueResult.invoicerMgtKey + CRLF;
                        tmp += "응답코드(code) : " + issueResult.code + CRLF;
                        tmp += "응답메시지(message) : " + issueResult.message + CRLF;
                        tmp += "국세청승인번호(ntsconfirmNum) : " + issueResult.ntsconfirmNum + CRLF;
                        tmp += "발행일시(issueDT) : " + issueResult.issueDT + CRLF;
                        i++;
                    }
                }
                MessageBox.Show(tmp, "초대량 접수결과 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "초대량 접수결과 확인");
            }
        }

        /*
         * 작성된 정발행 세금계산서 데이터를 팝빌에 저장합니다.
         * - "임시저장" 상태의 세금계산서는 발행(Issue) 함수를 호출하여 "발행완료" 처리한 경우에만 국세청으로 전송됩니다.
         * - 정발행 시 임시저장(Register)과 발행(Issue)을 한번의 호출로 처리하는 즉시발행(RegistIssue API) 프로세스 연동을 권장합니다.
         * - 세금계산서 파일첨부 기능을 구현하는 경우, 임시저장(Register API) -> 파일첨부(AttachFile API) -> 발행(Issue API) 함수를 차례로 호출합니다.
         * - 임시저장된 세금계산서는 팝빌 사이트 '임시문서함'에서 확인 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Register
         */
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체
            Taxinvoice taxinvoice = new Taxinvoice();

            // 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20220504";

            // 과금방향, {정과금, 역과금} 중 기재
            // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금";

            // 발행형태, {정발행, 역발행, 위수탁} 중 기재
            taxinvoice.issueType = "정발행";

            // {영수, 청구, 없음} 중 기재
            taxinvoice.purposeType = "영수";


            // 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = txtCorpNum.Text;

            // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // 공급자 대표자 성명
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
            taxinvoice.invoicerEmail = "";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "";

            // 공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = "";

            // 발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
            taxinvoice.invoiceeType = "사업자";

            // 공급받는자 사업자번호
            // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = "8888888888";

            // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoiceeTaxRegID = "";

            // 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // 공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "";

            // 공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소
            // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = "";

            // 공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = "";

            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // 세액 합계
            taxinvoice.taxTotal = "10000";

            // 합계금액,  공급가액 합계 + 세액 합계
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

            // 비고
            // {invoiceeType}이 "외국인" 이면 remark1 필수
            // - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;

            // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = false;

            /**************************************************************************
             *                      수정 세금계산서 기재정보
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
             * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = "";


            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; //비고

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

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
            addContact.email = ""; // 추가담당자 메일주소
            addContact.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재
            addContact2.email = ""; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 거래명세서 동시작성여부 (true / false 중 택 1)
            // └ true = 사용 , false = 미사용
            // - 미입력 시 기본값 false 처리
            bool writeSpecification = false;

            try
            {
                Response response =
                    taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, writeSpecification);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 임시저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 임시저장");
            }
        }

        /*
         * 작성된 역발행 세금계산서 데이터를 팝빌에 저장합니다.
         * - "임시저장" 상태의 세금계산서는 역발행요청(Request) 함수를 호출하여 "(역)발행대기" 상태가 되며
         *    공급자가 팝빌 사이트 또는 발행(Issue) 함수를 호출하여 발행한 경우에만 국세청으로 전송됩니다.
         * - 역발행 시 임시저장(Register)과 역발행요청(Request)을 한번의 호출로 처리하는 즉시요청(RegistRequest API) 프로세스 연동을 권장합니다.
         * - 세금계산서 파일첨부 기능을 구현하는 경우, 임시저장(Register API) -> 파일첨부(AttachFile API) -> 역발행 요청(Request API) 함수를 차례로 호출합니다.
         * - 역발행 세금계산서를 저장하는 경우, 객체 'Taxinvoice'의 변수 'chargeDirection' 값을 통해 과금 주체를 지정할 수 있습니다.
         *   └ 정과금 : 공급자 과금 , 역과금 : 공급받는자 과금
         * - 임시저장된 세금계산서는 팝빌 사이트 '임시문서함'에서 확인 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Register
         */
        private void btnRegister_Reverse_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체
            Taxinvoice taxinvoice = new Taxinvoice();

            // 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20220504";

            // 과금방향, {정과금, 역과금} 중 기재
            // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금";

            // 발행형태, {정발행, 역발행, 위수탁} 중 기재
            taxinvoice.issueType = "역발행";

            // {영수, 청구, 없음} 중 기재
            taxinvoice.purposeType = "영수";

            // 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "8888888888";

            // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = "";

            // 공급자 대표자 성명
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
            taxinvoice.invoicerEmail = "";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "";

            // 공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = "";

            // 발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
            taxinvoice.invoiceeType = "사업자";

            // 공급받는자 사업자번호
            // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = txtCorpNum.Text;

            // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoiceeTaxRegID = "";

            // 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;

            // 공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "";

            // 공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소
            // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = "";

            // 공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = "";

            // 역발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급자 담당자 휴대폰번호 {invoicerHP} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // 세액 합계
            taxinvoice.taxTotal = "10000";

            // 합계금액,  공급가액 합계 + 세액 합계
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

            // 비고
            // {invoiceeType}이 "외국인" 이면 remark1 필수
            // - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;

            // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = false;

            /**************************************************************************
             *                      수정 세금계산서 기재정보
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
             * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = "";


            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; //비고

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; //비고

            taxinvoice.detailList.Add(detail);

            try
            {
                Response response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "역발행 세금계산서 임시저장");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "역발행 세금계산서 임시저장");
            }
        }

        /*
        * "임시저장" 상태의 세금계산서를 발행(전자서명)하며, "발행완료" 상태로 처리합니다.
        * - 세금계산서 국세청 전송정책 [https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/policy-of-send-to-nts]
        * - "발행완료" 된 전자세금계산서는 국세청 전송 이전에 발행취소(CancelIssue API) 함수로 국세청 신고 대상에서 제외할 수 있습니다.
        * - 세금계산서 발행을 위해서 공급자의 인증서가 팝빌 인증서버에 사전등록 되어야 합니다.
        *   └ 위수탁발행의 경우, 수탁자의 인증서 등록이 필요합니다.
        * - 세금계산서 발행 시 공급받는자에게 발행 메일이 발송됩니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Issue
         */
        private void btnIssue_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            String memo = "발행메모";

            // 지연발행 강제여부  (true / false 중 택 1)
            // └ true = 가능 , false = 불가능
            // - 미입력 시 기본값 false 처리
            // - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
            // - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
            //   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
            bool forceIssue = false;

            try
            {
                IssueResponse response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue,
                    txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message + CRLF +
                                "국세청승인번호(ntsConfirmNum) : " + response.ntsConfirmNum, "세금계산서 발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 발행");
            }
        }

        /*
        * "(역)발행대기" 상태의 세금계산서를 발행(전자서명)하며, "발행완료" 상태로 처리합니다.
        * - 세금계산서 국세청 전송정책 [https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/policy-of-send-to-nts]
        * - "발행완료" 된 전자세금계산서는 국세청 전송 이전에 발행취소(CancelIssue API) 함수로 국세청 신고 대상에서 제외할 수 있습니다.
        * - 세금계산서 발행을 위해서 공급자의 인증서가 팝빌 인증서버에 사전등록 되어야 합니다.
        *   └ 위수탁발행의 경우, 수탁자의 인증서 등록이 필요합니다.
        * - 세금계산서 발행 시 공급받는자에게 발행 메일이 발송됩니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Issue
         */
        private void btnIssue_Reverse_sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 메모
            String memo = "발행메모";

            // 지연발행 강제여부  (true / false 중 택 1)
            // └ true = 가능 , false = 불가능
            // - 미입력 시 기본값 false 처리
            // - 발행마감일이 지난 세금계산서를 발행하는 경우, 가산세가 부과될 수 있습니다.
            // - 가산세가 부과되더라도 발행을 해야하는 경우에는 forceIssue의 값을
            //   true로 선언하여 발행(Issue API)를 호출하시면 됩니다.
            bool forceIssue = false;

            try
            {
                IssueResponse response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, forceIssue,
                    txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message + CRLF +
                                "국세청승인번호(ntsConfirmNum) : " + response.ntsConfirmNum, "세금계산서 발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 발행");
            }
        }

        /*
         * "임시저장" 상태의 정발행 세금계산서를 수정합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Update
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체
            Taxinvoice taxinvoice = new Taxinvoice();

            // 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20220504";

            // 과금방향, {정과금, 역과금} 중 기재
            // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금";

            // 발행형태, {정발행, 역발행, 위수탁} 중 기재
            taxinvoice.issueType = "정발행";

            // {영수, 청구, 없음} 중 기재
            taxinvoice.purposeType = "영수";

            // 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = txtCorpNum.Text;

            // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호_수정";

            // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // 공급자 대표자 성명
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
            taxinvoice.invoicerEmail = "";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "";

            // 공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = "";

            // 발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
            taxinvoice.invoiceeType = "사업자";

            // 공급받는자 사업자번호
            // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = "8888888888";

            // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoiceeTaxRegID = "";

            // 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // 공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "";

            // 공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소
            // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = "";

            // 공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = "";

            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // 세액 합계
            taxinvoice.taxTotal = "10000";

            // 합계금액,  공급가액 합계 + 세액 합계
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

            // 비고
            // {invoiceeType}이 "외국인" 이면 remark1 필수
            // - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;

            // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = false;

            /**************************************************************************
             *                      수정 세금계산서 기재정보
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
             * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = "";


            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

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
            addContact.email = ""; // 추가담당자 메일주소
            addContact.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2; // 일련번호, 1부터 순차기재
            addContact2.email = ""; // 추가담당자 메일주소
            addContact2.contactName = "추가담당자명"; // 추가담당자 성명

            taxinvoice.addContactList.Add(addContact2);


            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice,
                    txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "임시저장 세금계산서 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "임시저장 세금계산서 수정");
            }
        }

        /*
         * "임시저장" 상태의 역발행 세금계산서를 수정합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Update
         */
        private void btnUpdate_Reverse_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            // 세금계산서 정보 객체
            Taxinvoice taxinvoice = new Taxinvoice();

            // 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20220504";

            // 과금방향, {정과금, 역과금} 중 기재
            // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금";

            // 발행형태, {정발행, 역발행, 위수탁} 중 기재
            taxinvoice.issueType = "역발행";

            // {영수, 청구, 없음} 중 기재
            taxinvoice.purposeType = "영수";

            // 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "8888888888";

            // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;

            // 공급자 대표자 성명
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
            taxinvoice.invoicerEmail = "";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "";

            // 공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = "";

            // 발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
            taxinvoice.invoiceeType = "사업자";

            // 공급받는자 사업자번호
            // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = txtCorpNum.Text;

            // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoiceeTaxRegID = "";

            // 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoiceeMgtKey = "";

            // 공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "";

            // 공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소
            // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = "";

            // 공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = "";

            // 역발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급자 담당자 휴대폰번호 {invoicerHP} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // 세액 합계
            taxinvoice.taxTotal = "10000";

            // 합계금액,  공급가액 합계 + 세액 합계
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

            // 비고
            // {invoiceeType}이 "외국인" 이면 remark1 필수
            // - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;

            // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = false;

            /**************************************************************************
             *                      수정 세금계산서 기재정보
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
             * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = "";


            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

            taxinvoice.detailList.Add(detail);


            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice,
                    txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "역발행 세금계산서 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "역발행 세금계산서 수정");
            }
        }

        /*
         * 국세청 전송 이전 "발행완료" 상태의 전자세금계산서를 "발행취소"하고 국세청 신고대상에서 제외합니다.
         * - Delete(삭제)함수를 호출하여 "발행취소" 상태의 전자세금계산서를 삭제하면, 문서번호 재사용이 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelIssue
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "발행취소");
            }
        }


        /*
         * 국세청 전송 이전 "발행완료" 상태의 전자세금계산서를 "발행취소"하고 국세청 신고대상에서 제외합니다.
         * - Delete(삭제)함수를 호출하여 "발행취소" 상태의 전자세금계산서를 삭제하면, 문서번호 재사용이 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelIssue
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "발행취소");
            }
        }

        /*
         * 국세청 전송 이전 "발행완료" 상태의 전자세금계산서를 "발행취소"하고 국세청 신고대상에서 제외합니다.
         * - Delete(삭제)함수를 호출하여 "발행취소" 상태의 전자세금계산서를 삭제하면, 문서번호 재사용이 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelIssue
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "발행취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "발행취소");
            }
        }

        /*
         * 삭제 가능한 상태의 세금계산서를 삭제합니다.
         * - 삭제 가능한 상태: "임시저장", "발행취소", "역발행거부", "역발행취소", "전송실패"
         * - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Delete
         */
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 삭제");
            }
        }

        /*
         * 삭제 가능한 상태의 세금계산서를 삭제합니다.
         * - 삭제 가능한 상태: "임시저장", "발행취소", "역발행거부", "역발행취소", "전송실패"
         * - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Delete
         */
        private void btnDelete_Sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 삭제");
            }
        }

       /*
        * 삭제 가능한 상태의 세금계산서를 삭제합니다.
        * - 삭제 가능한 상태: "임시저장", "발행취소", "역발행거부", "역발행취소", "전송실패"
        * - 세금계산서를 삭제해야만 문서번호(mgtKey)를 재사용할 수 있습니다.
        * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Delete
        */
        private void btnDelete_Reverse_sub_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 삭제");
            }
        }

        /*
        * 공급받는자가 작성한 세금계산서 데이터를 팝빌에 저장하고 공급자에게 송부하여 발행을 요청합니다.
        * - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
        * - 발행 요청된 세금계산서는 "(역)발행대기" 상태이며, 공급자가 팝빌 사이트 또는 함수를 호출하여 발행한 경우에만 국세청으로 전송됩니다.
        * - 공급자는 팝빌 사이트의 "매출 발행 대기함"에서 발행대기 상태의 역발행 세금계산서를 확인할 수 있습니다.
        * - 임시저장(Register API) 함수와 역발행 요청(Request API) 함수를 한 번의 프로세스로 처리합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#RegistRequest
         */
        private void btnRegistRequest_Click(object sender, EventArgs e)
        {
            // 세금계산서 정보 객체
            Taxinvoice taxinvoice = new Taxinvoice();

            // 기재상 작성일자, 날짜형식(yyyyMMdd)
            taxinvoice.writeDate = "20220504";

            // 과금방향, {정과금, 역과금} 중 기재
            // └ 정과금 = 공급자 과금 , 역과금 = 공급받는자 과금
            // -'역과금'은 역발행 세금계산서 발행 시에만 이용가능
            taxinvoice.chargeDirection = "정과금";

            // 발행형태, {정발행, 역발행, 위수탁} 중 기재
            taxinvoice.issueType = "역발행";

            // {영수, 청구, 없음} 중 기재
            taxinvoice.purposeType = "영수";

            // 과세형태, {과세, 영세, 면세} 중 기재
            taxinvoice.taxType = "과세";


            /*****************************************************************
             *                         공급자 정보                           *
             *****************************************************************/

            // 공급자 사업자번호, '-' 제외 10자리
            taxinvoice.invoicerCorpNum = "8888888888";

            // 공급자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerTaxRegID = "";

            // 공급자 상호
            taxinvoice.invoicerCorpName = "공급자 상호";

            // 공급자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoicerMgtKey = "";

            // 공급자 대표자 성명
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
            taxinvoice.invoicerEmail = "";

            // 공급자 담당자 연락처
            taxinvoice.invoicerTEL = "";

            // 공급자 담당자 휴대폰번호
            taxinvoice.invoicerHP = "";

            // 발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급받는자 (주)담당자 휴대폰번호 {invoiceeHP1} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoicerSMSSendYN = false;


            /*********************************************************************
             *                         공급받는자 정보                           *
             *********************************************************************/

            // 공급받는자 구분, {사업자, 개인, 외국인} 중 기재
            taxinvoice.invoiceeType = "사업자";

            // 공급받는자 사업자번호
            // - {invoiceeType}이 "사업자" 인 경우, 사업자번호 (하이픈 ('-') 제외 10자리)
            // - {invoiceeType}이 "개인" 인 경우, 주민등록번호 (하이픈 ('-') 제외 13자리)
            // - {invoiceeType}이 "외국인" 인 경우, "9999999999999" (하이픈 ('-') 제외 13자리)
            taxinvoice.invoiceeCorpNum = txtCorpNum.Text;

            // 공급받는자 종사업장 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoiceeTaxRegID = "";

            // 공급받는자 상호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";

            // [역발행시 필수] 공급받는자 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;

            // 공급받는자 대표자 성명
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";

            // 공급받는자 주소
            taxinvoice.invoiceeAddr = "공급받는자 주소";

            // 공급받는자 종목
            taxinvoice.invoiceeBizClass = "공급받는자 종목";

            // 공급받는자 업태
            taxinvoice.invoiceeBizType = "공급받는자 업태";

            // 공급받는자 담당자 연락처
            taxinvoice.invoiceeTEL1 = "";

            // 공급받는자 담당자명
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";

            // 공급받는자 담당자 메일주소
            // 팝빌 테스트 환경에서 테스트하는 경우에도 안내 메일이 전송되므로,
            // 실제 거래처의 메일주소가 기재되지 않도록 주의
            taxinvoice.invoiceeEmail1 = "";

            // 공급받는자 담당자 휴대폰번호
            taxinvoice.invoiceeHP1 = "";

            // 역발행 안내 문자 전송여부 (true / false 중 택 1)
            // └ true = 전송 , false = 미전송
            // └ 공급자 담당자 휴대폰번호 {invoicerHP} 값으로 문자 전송
            // - 전송 시 포인트 차감되며, 전송실패시 환불처리
            taxinvoice.invoiceeSMSSendYN = false;


            /*********************************************************************
             *                          세금계산서 정보                          *
             *********************************************************************/

            // 공급가액 합계
            taxinvoice.supplyCostTotal = "100000";

            // 세액 합계
            taxinvoice.taxTotal = "10000";

            // 합계금액,  공급가액 합계 + 세액 합계
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

            // 비고
            // {invoiceeType}이 "외국인" 이면 remark1 필수
            // - 외국인 등록번호 또는 여권번호 입력
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";

            // 기재상 권 항목, 최대값 32767
            taxinvoice.kwon = 1;

            // 기재상 호 항목, 최대값 32767
            taxinvoice.ho = 1;

            // 사업자등록증 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.businessLicenseYN = false;

            // 통장사본 이미지 첨부여부 (true / false 중 택 1)
            // └ true = 첨부 , false = 미첨부(기본값)
            // - 팝빌 사이트 또는 인감 및 첨부문서 등록 팝업 URL (GetSealURL API) 함수를 이용하여 등록
            taxinvoice.bankBookYN = false;

            /**************************************************************************
             *                      수정 세금계산서 기재정보
             * - 수정세금계산서 관련 정보는 연동매뉴얼 또는 개발가이드 링크 참조
             * - [참고] 수정세금계산서 작성방법 안내 - https://developers.popbill.com/guide/taxinvoice/dotnet/introduction/modified-taxinvoice
             *************************************************************************/

            // 수정사유코드, 1~6까지 선택기재.
            taxinvoice.modifyCode = null;

            // 수정세금계산서 작성시 원본세금계산서의 국세청승인번호
            taxinvoice.orgNTSConfirmNum = "";


            /**************************************************************************
             *                         상세항목(품목) 정보                            *
             * - 상세항목 정보는 세금계산서 필수기재사항이 아니므로 작성하지 않더라도 *
             *   세금계산서 발행이 가능합니다.                                        *
             * - 최대 99건까지 작성가능                                               *
             **************************************************************************/

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2; // 일련번호, 1부터 순차기재
            detail.purchaseDT = "20220504"; // 거래일자
            detail.itemName = "품목명"; // 품목명
            detail.spec = "규격"; // 규격
            detail.qty = "1"; // 수량
            detail.unitCost = "50000"; // 단가
            detail.supplyCost = "50000"; // 공급가액
            detail.tax = "5000"; // 세액
            detail.remark = "품목비고"; // 비고

            taxinvoice.detailList.Add(detail);


            // 메모
            string memo = "즉시요청 메모";

            try
            {
                Response response = taxinvoiceService.RegistRequest(txtCorpNum.Text, taxinvoice, memo);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 즉시요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 즉시요청");
            }
        }


        /*
        * 공급받는자가 저장된 역발행 세금계산서를 공급자에게 송부하여 발행 요청합니다.
        * - 역발행 세금계산서 프로세스를 구현하기 위해서는 공급자/공급받는자가 모두 팝빌에 회원이여야 합니다.
        * - 역발행 요청된 세금계산서는 "(역)발행대기" 상태이며, 공급자가 팝빌 사이트 또는 함수를 호출하여 발행한 경우에만 국세청으로 전송됩니다.
        * - 공급자는 팝빌 사이트의 "매출 발행 대기함"에서 발행대기 상태의 역발행 세금계산서를 확인할 수 있습니다.
        * - 역발행 요청시 공급자에게 역발행 요청 메일이 발송됩니다.
        * - 공급자가 역발행 세금계산서 발행시 포인트가 과금됩니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Request
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "역발행 요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "역발행 요청");
            }
        }


        /*
        * 공급자가 요청받은 역발행 세금계산서를 발행하기 전, 공급받는자가 역발행요청을 취소합니다.
        * - 함수 호출시 상태 값이 "취소"로 변경되고, 해당 역발행 세금계산서는 공급자에 의해 발행 될 수 없습니다.
        * - [취소]한 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API) 함수를 호출해야 합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelRequest
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "역발행 요청 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "역발행 요청 취소");
            }
        }

        /*
        * 공급자가 요청받은 역발행 세금계산서를 발행하기 전, 공급받는자가 역발행요청을 취소합니다.
        * - 함수 호출시 상태 값이 "취소"로 변경되고, 해당 역발행 세금계산서는 공급자에 의해 발행 될 수 없습니다.
        * - [취소]한 세금계산서의 문서번호를 재사용하기 위해서는 삭제 (Delete API) 함수를 호출해야 합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#CancelRequest
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "역발행 요청 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "역발행 요청 취소");
            }
        }

        /*
         * 공급자가 공급받는자에게 역발행 요청 받은 세금계산서의 발행을 거부합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Refuse
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
         * 공급자가 공급받는자에게 역발행 요청 받은 세금계산서의 발행을 거부합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#Refuse
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
        * "발행완료" 상태의 전자세금계산서를 국세청에 즉시 전송하며, 함수 호출 후 최대 30분 이내에 전송 처리가 완료됩니다.
        * - 국세청 즉시전송을 호출하지 않은 세금계산서는 발행일 기준 다음 영업일 오후 3시에 팝빌 시스템에서 일괄적으로 국세청으로 전송합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/issue#SendToNTS
         */
        private void btnSendToNTS_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response =
                    taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "국세청 즉시전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "국세청 즉시전송");
            }
        }

        /*
         * 세금계산서 1건의 상태 및 요약정보를 확인합니다.
         * 리턴값 'TaxinvoiceInfo'의 변수 'stateCode'를 통해 세금계산서의 상태코드를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetInfo
         */
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                TaxinvoiceInfo taxinvoiceInfo = taxinvoiceService.GetInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemKey (팝빌번호) : " + taxinvoiceInfo.itemKey + CRLF;
                tmp += "taxType (과세형태) : " + taxinvoiceInfo.taxType + CRLF;
                tmp += "writeDate (작성일자) : " + taxinvoiceInfo.writeDate + CRLF;
                tmp += "regDT (임시저장 일자) : " + taxinvoiceInfo.regDT + CRLF;
                tmp += "issueType (발행형태) : " + taxinvoiceInfo.issueType + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + taxinvoiceInfo.supplyCostTotal + CRLF;
                tmp += "taxTotal (세액 합계) : " + taxinvoiceInfo.taxTotal + CRLF;
                tmp += "purposeType (영수/청구) : " + taxinvoiceInfo.purposeType + CRLF;
                tmp += "issueDT (발행일시) : " + taxinvoiceInfo.issueDT + CRLF;
                tmp += "lateIssueYN (지연발행 여부) : " + taxinvoiceInfo.lateIssueYN + CRLF;
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
                tmp += "invoicerMgtKey (공급자 문서번호) : " + taxinvoiceInfo.invoicerMgtKey + CRLF;
                tmp += "invoicerPrintYN (공급자 인쇄여부) : " + taxinvoiceInfo.invoicerPrintYN + CRLF;

                tmp += "invoiceeCorpName (공급받는자 상호) : " + taxinvoiceInfo.invoiceeCorpName + CRLF;
                tmp += "invoiceeCorpNum (공급받는자 사업자번호) : " + taxinvoiceInfo.invoiceeCorpNum + CRLF;
                tmp += "invoiceePrintYN (공급받는자 문서번호) : " + taxinvoiceInfo.invoiceePrintYN + CRLF;
                tmp += "closeDownState (공급받는자 휴폐업상태코드) : " + taxinvoiceInfo.closeDownState + CRLF;
                tmp += "closeDownStateDate (공급받는자 휴폐업일자) : " + taxinvoiceInfo.closeDownStateDate + CRLF;

                tmp += "trusteeCorpName (수탁자 상호) : " + taxinvoiceInfo.trusteeCorpName + CRLF;
                tmp += "trusteeCorpNum (수탁자 사업자번호) : " + taxinvoiceInfo.trusteeCorpNum + CRLF;
                tmp += "trusteeMgtKey (수탁자 문서번호) : " + taxinvoiceInfo.trusteeMgtKey + CRLF;
                tmp += "trusteePrintYN (수탁자 인쇄여부) : " + taxinvoiceInfo.trusteePrintYN + CRLF;

                MessageBox.Show(tmp, "문서 상태/요약 정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문서 상태/요약 정보 조회");
            }
        }

        /*
         * 다수건의 세금계산서 상태 및 요약 정보를 확인합니다. (1회 호출 시 최대 1,000건 확인 가능)
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetInfos
         */
        private void btnGetInfos_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            //  조회할 세금계산서 문서번호 배열, (최대 1000건)
            MgtKeyList.Add("20220504-001");
            MgtKeyList.Add("20220504-002");


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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문서 상태정보 대량 확인");
            }
        }

        /*
         * 세금계산서 1건의 상세정보를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetDetailInfo
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

                tmp += "invoicerMgtKey (공급자 문서번호) : " + taxinvoice.invoicerMgtKey + CRLF;
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

                tmp += "invoiceeMgtKey (공급받는자 문서번호) : " + taxinvoice.invoiceeMgtKey + CRLF;
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 상세정보 확인");
            }
        }

        /*
         * 세금계산서 1건의 상세정보를 XML로 반환합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetXML
         */
        private void btnGetXML_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                TaxinvoiceXML taxinvoiceXML = taxinvoiceService.GetXML(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                string tmp = null;

                tmp += "code (응답코드) : " + taxinvoiceXML.code + CRLF;
                tmp += "message (응답메시지) : " + taxinvoiceXML.message + CRLF;
                tmp += "retObject (전자세금계산서 XML 문서) : " + taxinvoiceXML.retObject + CRLF;

                MessageBox.Show(tmp, "세금계산서 상세정보 확인(XML)");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 상세정보 확인(XML)");
            }

        }

        /*
         * 검색조건에 해당하는 세금계산서를 조회합니다. (조회기간 단위 : 최대 6개월)
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 일자유형 ("R" , "W" , "I" 중 택 1)
            // - R = 등록일자 , W = 작성일자 , I = 발행일자
            String DType = "W";

            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20220501";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20220531";

            // 세금계산서 상태코드 배열 (2,3번째 자리에 와일드카드(*) 사용 가능)
            // - 미입력시 전체조회
            String[] State = new String[5];
            State[0] = "100";
            State[1] = "3**";
            State[2] = "4**";
            State[3] = "5**";
            State[4] = "6**";

            // 문서유형 배열 ("N" , "M" 중 선택, 다중 선택 가능)
            // - N = 일반세금계산서 , M = 수정세금계산서
            // - 미입력시 전체조회
            String[] Type = new String[2];
            Type[0] = "N";
            Type[1] = "M";

            // 과세형태 배열 ("T" , "N" , "Z" 중 선택, 다중 선택 가능)
            // - T = 과세 , N = 면세 , Z = 영세
            // - 미입력시 전체조회
            String[] TaxType = new String[3];
            TaxType[0] = "T";
            TaxType[1] = "N";
            TaxType[2] = "Z";

            // 발행형태 배열 ("N" , "R" , "T" 중 선택, 다중 선택 가능)
            // - N = 정발행 , R = 역발행 , T = 위수탁발행
            // - 미입력시 전체조회
            String[] IssueType = new String[3];
            IssueType[0] = "N";
            IssueType[1] = "R";
            IssueType[2] = "T";

            // 등록유형 배열 ("P" , "H" 중 선택, 다중 선택 가능)
            // - P = 팝빌에서 등록 , H = 홈택스 또는 외부ASP 등록
            // - 미입력시 전체조회
            String[] RegType = new String[2];
            RegType[0] = "P";
            RegType[1] = "H";

            // 공급받는자 휴폐업상태 배열 ("N" , "0" , "1" , "2" , "3" , "4" 중 선택, 다중 선택 가능)
            // - N = 미확인 , 0 = 미등록 , 1 = 사업 , 2 = 폐업 , 3 = 휴업 , 4 = 확인실패
            // - 미입력시 전체조회
            String[] CloseDownState = new String[5];
            CloseDownState[0] = "N";
            CloseDownState[1] = "0";
            CloseDownState[2] = "1";
            CloseDownState[3] = "2";
            CloseDownState[4] = "3";

            // 종사업장번호 유무 (null , "0" , "1" 중 택 1)
            // - null = 전체 , 0 = 없음, 1 = 있음
            String TaxRegIDYN = "";

            // 종사업장번호의 주체 ("S" , "B" , "T" 중 택 1)
            // └ S = 공급자 , B = 공급받는자 , T = 수탁자
            // - 미입력시 전체조회
            String TaxRegIDType = "S";

            // 종사업장번호
            // 다수기재시 콤마(",")로 구분하여 구성 ex ) "0001,0002"
            // - 미입력시 전체조회
            String TaxRegID = "";

            // 지연발행 여부 (null , true , false 중 택 1)
            // - null = 전체조회 , true = 지연발행 , false = 정상발행
            bool? LateOnly = null;

            // 거래처 상호 / 사업자번호 (사업자) / 주민등록번호 (개인) / "9999999999999" (외국인) 중 검색하고자 하는 정보 입력
            // └ 사업자번호 / 주민등록번호는 하이픈('-')을 제외한 숫자만 입력
            // - 미입력시 전체조회
            String QString = "";

            // 문서번호 또는 국세청승인번호 조회
            String MgtKey = "";

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000건
            int PerPage = 30;

            // 연동문서 여부 (null , "0" , "1" 중 택 1)
            // └ null = 전체조회 , 0 = 일반문서 , 1 = 연동문서
            // - 일반문서 : 팝빌 사이트를 통해 저장 또는 발행한 세금계산서
            // - 연동문서 : 팝빌 API를 통해 저장 또는 발행한 세금계산서
            String InterOPYN = "";

            try
            {
                TISearchResult searchResult = taxinvoiceService.Search(txtCorpNum.Text, KeyType, DType, SDate, EDate,
                    State, Type, TaxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page,
                    PerPage, InterOPYN, txtUserId.Text, RegType, CloseDownState, MgtKey);

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문서목록 조회");
            }
        }

        /*
         * 세금계산서의 상태에 대한 변경이력을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetLogs
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문서 상태변경 이력확인");
            }
        }


        /*
         * 로그인 상태로 팝빌 사이트의 전자세금계산서 임시문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
         */
        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX");

                MessageBox.Show(url, "임시 문서함 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "임시 문서함 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 전자세금계산서 매출문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
         */
        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX");

                MessageBox.Show(url, "매출문서함 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "매출문서함 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 전자세금계산서 매입문서함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
         */
        private void btnGetURL_PBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

                MessageBox.Show(url, "매입문서함 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "매입문서함 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 전자세금계산서 매출문서작성 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
         */
        private void btnGetURL_WRITE_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE");

                MessageBox.Show(url, "정발행 작성 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "정발행 작성 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 전자세금계산서 매출 발행 대기함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
         */
        private void btnGetURL_SWBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SWBOX");

                MessageBox.Show(url, "매출 발행 대기함 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "매출 발행 대기함 팝업 URL");
            }
        }

        /*
         * 로그인 상태로 팝빌 사이트의 전자세금계산서 매입 발행 대기함 메뉴에 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/info#GetURL
         */
        private void btnGetURL_PWBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PWBOX");

                MessageBox.Show(url, "매입 발행 대기함 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "매입 발행 대기함 팝업 URL");
            }
        }

        /*
         * 세금계산서 1건의 상세 정보 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetPopUpURL
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 보기 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 보기 팝업 URL");
            }
        }

        /*
         * 세금계산서 1건의 상세정보 페이지(사이트 상단, 좌측 메뉴 및 버튼 제외)의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetViewURL
         */
        private void btnGetViewURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetViewURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 보기 팝업 URL");
                textURL.Text = url;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 보기 팝업 URL");
            }
        }

        /*
         * 세금계산서 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환하며, 페이지내에서 인쇄 설정값을 "공급자" / "공급받는자" / "공급자+공급받는자"용 중 하나로 지정할 수 있습니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetPrintURL
         */
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            // 발행형태
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 인쇄 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 인쇄 팝업 URL");
            }
        }

        /*
         * 세금계산서 1건을 구버전 양식으로 인쇄하기 위한 페이지의 팝업 URL을 반환하며, 페이지내에서 인쇄 설정값을 "공급자" / "공급받는자" / "공급자+공급받는자"용 중 하나로 지정할 수 있습니다..
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetOldPrintURL
         */
        private void btnGetOldPrintURL_Click(object sender, EventArgs e)
        {
            // 발행형태
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetOldPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 (구)인쇄 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 인쇄 팝업 URL");
            }

        }
        /*
         * "공급받는자" 용 세금계산서 1건을 인쇄하기 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetEPrintURL
         */
        private void btnEPrintURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 인쇄(공급받는자) 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 인쇄(공급받는자) 팝업 URL");
            }
        }

        /*
         * 다수건의 세금계산서를 인쇄하기 위한 페이지의 팝업 URL을 반환합니다. (최대 100건)
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetMassPrintURL
         */
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            // 인쇄할 세금계산서 문서번호, (최대 100건)
            MgtKeyList.Add("20220504-001");
            MgtKeyList.Add("20220504-002");
            try
            {
                string url = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 대량인쇄 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 대량인쇄 팝업 URL");
            }
        }

        /*
         * 안내메일과 관련된 전자세금계산서를 확인 할 수 있는 상세 페이지의 팝업 URL을 반환하며, 해당 URL은 메일 하단의 "전자세금계산서 보기" 버튼의 링크와 같습니다.
         * - 함수 호출로 반환 받은 URL에는 유효시간이 없습니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetMailURL
         */
        private void btnGetEmailURL_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetMailURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "메일링크 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "메일링크 팝업 URL");
            }
        }

        /*
         * 전자세금계산서 PDF 파일을 다운 받을 수 있는 URL을 반환합니다.
         * - 반환되는 URL은 보안정책상 30초의 유효시간을 갖으며, 유효시간 이후 호출시 정상적으로 페이지가 호출되지 않습니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/view#GetPDFURL
         */
        private void btnGetPDFURL_Click(object sender, EventArgs e)
        {
            // 발행형태
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetPDFURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url, "세금계산서 PDF 다운로드 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 인쇄 팝업 URL");
            }
        }

        /*
         * 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 세금계산서에 첨부할 인감, 사업자등록증, 통장사본을 등록하는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetSealURL
         */
        private void btnGetSealURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetSealURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "인감 및 첨부문서 등록 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인감 및 첨부문서 등록 URL");
            }
        }

        /*
         * "임시저장" 상태의 세금계산서에 1개의 파일을 첨부합니다. (최대 5개)
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#AttachFile
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

                    MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                    "응답메시지(message) : " + response.message, "첨부파일 등록");
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "첨부파일 등록");
                }
            }
        }

        /*
         * "임시저장" 상태의 세금계산서에 첨부된 1개의 파일을 삭제합니다.
         * - 파일 식별을 위해 첨부 시 할당되는 'FileID'는 첨부파일 목록 확인(GetFiles API) 함수를 호출하여 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#DeleteFile
         */
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                    txtFileID.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "첨부파일 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "첨부파일 삭제");
            }
        }


        /*
         * 세금계산서에 첨부된 파일목록을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetFiles
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "첨부파일 목록 확인");
            }
        }


        /*
         * 세금계산서와 관련된 안내 메일을 재전송 합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#SendEmail
         */
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 수신메일주소
            String email = "";

            try
            {
                Response response =
                    taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, email, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "발행 안내메일 재전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "발행 안내메일 재전송");
            }
        }

        /*
         * 세금계산서와 관련된 안내 SMS(단문) 문자를 재전송하는 함수로, 팝빌 사이트 [문자·팩스] > [문자] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
         * - 메시지는 최대 90byte까지 입력 가능하고, 초과한 내용은 자동으로 삭제되어 전송합니다. (한글 최대 45자)
         * - 함수 호출시 포인트가 과금됩니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#SendSMS
         */
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 발신번호
            string senderNum = "";

            // 수신번호
            string receiverNum = "";

            // 문자메시지 내용, 90byte 초과시 길이가 조정되어 전송됨
            string contents = "알림문자 전송내용, 90byte 초과된 내용은 삭제되어 전송됨";

            try
            {
                Response response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, senderNum,
                    receiverNum, contents, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "알림문자 전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림문자 전송");
            }
        }

        /*
         * 세금계산서를 팩스로 전송하는 함수로, 팝빌 사이트 [문자·팩스] > [팩스] > [전송내역] 메뉴에서 전송결과를 확인 할 수 있습니다.
         * - 함수 호출시 포인트가 과금됩니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#SendFAX
         */
        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 발신번호
            string senderNum = "";

            // 수신팩스번호
            string receiverNum = "";

            try
            {
                Response response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, senderNum,
                    receiverNum, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 팩스전송");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 팩스전송");
            }
        }

        /*
         * 팝빌 전자명세서 API를 통해 발행한 전자명세서를 세금계산서에 첨부합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#AttachStatement
         */
        private void btnAttachStmt_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 첨부할 명세서 종류 코드
            int DocItemCode = 121;

            // 첨부할 명세서 문서번호
            String DocMgtKey = "20220504-001";

            try
            {
                Response response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                    DocItemCode, DocMgtKey);
                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "전자명세서 첨부");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "전자명세서 첨부");
            }
        }

        /*
         * 세금계산서에 첨부된 전자명세서를 해제합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#DetachStatement
         */
        private void btnDetachStmt_Click(object sender, EventArgs e)
        {
            // 세금계산서 발행유형
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            // 첨부해제할 명세서 종류 코드
            int DocItemCode = 121;

            // 첨부해제할 명세서 문서번호
            String DocMgtKey = "20220504-001";

            try
            {
                Response response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, txtMgtKey.Text,
                    DocItemCode, DocMgtKey);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "전자명세서 첨부해제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "전자명세서 첨부해제 ");
            }
        }

        /*
         * 팝빌 사이트를 통해 발행하였지만 문서번호가 존재하지 않는 세금계산서에 문서번호를 할당합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#AssignMgtKey
         */
        private void btnAssignMgtKey_Click(object sender, EventArgs e)
        {
            // 세금계산서 유형 SELL-매출, BUY-매입, TRUSTEE-위수탁
            MgtKeyType KeyType = MgtKeyType.SELL;

            // 세금계산서 팝빌번호, 목록조회(Search) API의 반환항목중 ItemKey 참조
            String itemKey = "021041823243600001";

            // 할당할 문서번호, 최대 24자리, 영문, 숫자 '-', '_'를 조합하여 사업자별로 중복되지 않도록 구성
            String mgtKey = "20220504-003";

            try
            {
                Response response = taxinvoiceService.AssignMgtKey(txtCorpNum.Text, KeyType, itemKey, mgtKey);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "세금계산서 문서번호 할당");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 문서번호 할당");
            }
        }

        /*
         * 세금계산서 관련 메일 항목에 대한 발송설정을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#ListEmailConfig
         */
        private void btnListEmailConfig_Click(object sender, EventArgs e)
        {
            String tmp = "";
            try
            {
                List<EmailConfig> resultList = taxinvoiceService.ListEmailConfig(txtCorpNum.Text);

                tmp = "메일전송유형 | 전송여부" + CRLF;

                foreach (EmailConfig info in resultList)
                {
                    if (info.emailType == "TAX_ISSUE_INVOICER")
                        tmp += "[정발행] TAX_ISSUE_INVOICER (공급자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CHECK")
                        tmp += "[정발행] TAX_CHECK (공급자에게 전자세금계산서 수신확인 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CANCEL_ISSUE")
                        tmp += "[정발행] TAX_CANCEL_ISSUE (공급받는자에게 전자세금계산서 발행취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_REQUEST")
                        tmp += "[역발행] TAX_REQUEST (공급자에게 전자세금계산서를 발행요청 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_CANCEL_REQUEST")
                        tmp += "[역발행] TAX_CANCEL_REQUEST (공급받는자에게 전자세금계산서 취소 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_REFUSE")
                        tmp += "[역발행] TAX_REFUSE (공급받는자에게 전자세금계산서 거부 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_REVERSE_ISSUE")
                        tmp += "[역발행] TAX_REVERSE_ISSUE (공급받는자에게 전자세금계산서 발행 메일) | " + info.sendYN + CRLF;
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
                    if (info.emailType == "TAX_CLOSEDOWN")
                        tmp += "[처리결과] TAX_CLOSEDOWN (거래처의 사업자등록상태(휴폐업) 확인 메일) | " + info.sendYN + CRLF;
                    if (info.emailType == "TAX_NTSFAIL_INVOICER")
                        tmp += "[처리결과] TAX_NTSFAIL_INVOICER (전자세금계산서 국세청 전송실패 안내) | " + info.sendYN + CRLF;
                    if (info.emailType == "ETC_CERT_EXPIRATION")
                        tmp += "[정기발송] ETC_CERT_EXPIRATION (팝빌에서 이용중인 인증서의 갱신 메일) | " + info.sendYN + CRLF;
                }

                MessageBox.Show(tmp, "알림메일 전송목록 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림메일 전송목록 조회");
            }
        }

        /* 세금계산서 관련 메일 항목에 대한 발송설정을 수정합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#UpdateEmailConfig

          메일전송유형
          [정발행]
          TAX_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 사실을 안내하는 메일
          TAX_CHECK : 공급자에게 전자세금계산서 수신확인 사실을 안내하는 메일
          TAX_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 사실을 안내하는 메일

          [역발행]
          TAX_REQUEST : 공급자에게 전자세금계산서를 발행을 요청하는 메일
          TAX_CANCEL_REQUEST : 공급받는자에게 전자세금계산서 취소 사실을 안내하는 메일
          TAX_REFUSE : 공급받는자에게 전자세금계산서 거부 사실을 안내하는 메일
          TAX_REVERSE_ISSUE : 공급받는자에게 전자세금계산서 발행 사실을 안내하는 메일

          [위수탁발행]
          TAX_TRUST_ISSUE : 공급받는자에게 전자세금계산서 발행 사실을 안내하는 메일
          TAX_TRUST_ISSUE_TRUSTEE : 수탁자에게 전자세금계산서 발행 사실을 안내하는 메일
          TAX_TRUST_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행 사실을 안내하는 메일
          TAX_TRUST_CANCEL_ISSUE : 공급받는자에게 전자세금계산서 발행취소 사실을 안내하는 메일
          TAX_TRUST_CANCEL_ISSUE_INVOICER : 공급자에게 전자세금계산서 발행취소 사실을 안내하는 메일

          [처리결과]
          TAX_CLOSEDOWN : 거래처의 사업자등록상태(휴폐업)를 확인하여 안내하는 메일
          TAX_NTSFAIL_INVOICER : 전자세금계산서 국세청 전송실패를 안내하는 메일

          [정기발송]
          ETC_CERT_EXPIRATION : 팝빌에 등록된 인증서의 만료예정을 안내하는 메일
         */
        private void btnUpdateEmailConfig_Click(object sender, EventArgs e)
        {
            String EmailType = "TAX_ISSUE_INVOICER";

            //전송여부 (True-전송, False-미전송)
            bool SendYN = true;

            try
            {
                Response response =
                    taxinvoiceService.UpdateEmailConfig(txtCorpNum.Text, EmailType, SendYN);


                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "알림메일 전송설정 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림메일 전송설정 수정");
            }
        }

        /*
         * 연동회원의 국세청 전송 옵션 설정 상태를 확인합니다.
         * - 국세청 전송 옵션 설정은 팝빌 사이트 [전자세금계산서] > [환경설정] > [세금계산서 관리] 메뉴에서 설정할 수 있으며, API로 설정은 불가능 합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/etc#GetSendToNTSConfig
         */
        private void btnGetSendToNTSConfig_Click(object sender, EventArgs e)
        {
            try
            {
                bool sendToNTSConfig =
                    taxinvoiceService.GetSendToNTSConfig(txtCorpNum.Text);


                MessageBox.Show("국세청 전송 설정 확인 : " + sendToNTSConfig.ToString() + CRLF +
                                "True(발행 즉시 전송) False(익일 자동 전송)");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, ".국세청 전송 설정 확인");
            }
        }

        /*
         * 전자세금계산서 발행에 필요한 인증서를 팝빌 인증서버에 등록하기 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - 인증서 갱신/재발급/비밀번호 변경한 경우, 변경된 인증서를 팝빌 인증서버에 재등록 해야합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#GetTaxCertURL
         */
        private void btnGetTaxCertURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetTaxCertURL(txtCorpNum.Text, txtUserId.Text);

                Process objProcess = Process.Start("IEXPLORE.EXE", "-nomerge "+url);
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인증서 등록 URL");
            }
        }

        /*
         * 팝빌 인증서버에 등록된 인증서의 만료일을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#GetCertificateExpireDate
         */
        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show("인증서 만료일시 : " + expiration.ToString(), "인증서 만료일시 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인증서 만료일시 확인");
            }
        }


        /*
         * 팝빌 인증서버에 등록된 인증서의 유효성을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#CheckCertValidation
         */
        private void btnCheckCertValidation_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = taxinvoiceService.CheckCertValidation(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "인증서 유효성 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인증서 유효성 확인");
            }
        }

        /*
         * 팝빌 인증서버에 등록된 인증서의 정보를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/cert#GetTaxCertInfo
         */
        private void btnGetTaxCertInfo_Click(object sender, EventArgs e)
        {
            try
            {
                TaxinvoiceCertificate taxinvoiceCertificate = taxinvoiceService.GetTaxCertInfo(txtCorpNum.Text);

                string tmp = null;

                tmp += "regDT (등록일시) : " + taxinvoiceCertificate.regDT + CRLF;
                tmp += "expireDT (만료일시) : " + taxinvoiceCertificate.expireDT + CRLF;
                tmp += "issuerDN (인증서 발급자 DN) : " + taxinvoiceCertificate.issuerDN + CRLF;
                tmp += "subjectDN (등록된 인증서 DN) : " + taxinvoiceCertificate.subjectDN + CRLF;
                tmp += "issuerName (인증서 종류) : " + taxinvoiceCertificate.issuerName + CRLF;
                tmp += "oid (OID) : " + taxinvoiceCertificate.oid + CRLF;
                tmp += "regContactName (등록 담당자 성명) : " + taxinvoiceCertificate.regContactName + CRLF;
                tmp += "regContactID (등록 담당자 아이디) : " + taxinvoiceCertificate.regContactID + CRLF;

                MessageBox.Show(tmp, "인증서 정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인증서 정보 확인");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 조회");
            }
        }


        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "연동회원 포인트 충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "연동회원 포인트 결제내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 결제내역 URL");
            }
        }

        /*
         * 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "연동회원 포인트 사용내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 사용내역 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPartnerBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }


        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "파트너 포인트충전 URL");
            }
        }

        /*
         * 세금계산서 발행시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "세금계산서 발행단가 확인");
            }
        }


        /*
         * 팝빌 전자세금계산서 API 서비스 과금정보를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetChargeInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "과금정보 확인");
            }
        }


        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "연동회원 가입여부확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입여부확인");
            }
        }

        /*
         * 사용하고자 하는 아이디의 중복여부를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = taxinvoiceService.CheckID(txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "ID 중복확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "ID 중복확인");
            }
        }

        /*
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#JoinMember
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
                Response response = taxinvoiceService.JoinMember(joinInfo);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "연동회원 가입요청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입요청");
            }
        }


        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#GetCorpInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "회사정보 조회");
            }
        }

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#UpdateCorpInfo
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

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "회사정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "회사정보 수정");
            }
        }

        /*
        * 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
        * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#RegistContact
        */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 50자 미만
            contactInfo.id = "dotnet_test_002";

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
                Response response = taxinvoiceService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 추가등록");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 추가등록");
            }
        }

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = taxinvoiceService.GetContactInfo(txtCorpNum.Text, contactID);

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 확인");
            }
        }

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#ListContact
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 목록조회");
            }
        }

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#UpdateContact
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
                Response response = taxinvoiceService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당저 정보 수정");
            }
        }

        /**
         * 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#PaymentRequest
         */
        public void btnPaymentRequest_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 무통장 입금 신청 객체
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
            PaymentForm.settleCost = "1000";

            // 팝빌회원 아이디
            String UserID = "testkorea";


            try
            {
                PaymentResponse response = taxinvoiceService.PaymentRequest(CorpNum, PaymentForm, UserID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message + CRLF +
                                "정산코드" + response.settleCode,
                    "연동회원 무통장 입금신청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 무통장 입금신청");
            }
        }

        /**
         * 연동회원의 포인트 결제내역을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetPaymentHistory
         */
        public void btnGetPaymentHistory_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 조회 시작 일자
            String SDate = "20230501";

            // 조회 종료 일자
            String EDate = "20230530";

            // 목록 페이지 번호
            int Page = 1;

            // 페이지당 목록 개수
            int PerPage = 500;

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                PaymentHistoryResult result =
                    taxinvoiceService.GetPaymentHistory(CorpNum, SDate, EDate, Page, PerPage, UserID);

                String tmp = "";

                foreach (PaymentHistory history in result.list)
                {
                    tmp += "결제 내용(productType) : " + history.productType + CRLF;
                    tmp += "결제 상품명(productName) : " + history.productName + CRLF;
                    tmp += "결제 유형(settleType) : " + history.settleType + CRLF;
                    tmp += "담당자명(settlerName) : " + history.settlerName + CRLF;
                    tmp += "담당자메일(settlerEmail) : " + history.settlerEmail + CRLF;
                    tmp += "결제 금액(settleCost) : " + history.settleCost + CRLF;
                    tmp += "충전포인트(settlePoint) : " + history.settlePoint + CRLF;
                    tmp += "결제 상태(settleState) : " + history.settleState.ToString() + CRLF;
                    tmp += "등록일시(regDT) : " + history.regDT + CRLF;
                    tmp += "상태일시(stateDT) : " + history.stateDT + CRLF;
                    tmp += CRLF;
                }

                MessageBox.Show(
                    "응답코드(code) : " + result.code.ToString() + CRLF+
                    "총 검색결과 건수(total) : " + result.total.ToString() + CRLF+
                    "페이지당 검색개수(perPage) : " + result.perPage.ToString() +CRLF+
                    "페이지 번호(pageNum) : " + result.pageNum.ToString() +CRLF+
                    "페이지 개수(pageCount) : " + result.pageCount.ToString() +CRLF
                    + "결제내역"+CRLF+tmp,
                    "연동회원 포인트 결제내역 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 결제내역 확인");
            }
        }

        /**
         * 연동회원 포인트 무통장 입금신청내역 1건을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetSettleResult
         */
        public void btnGetSettleResult_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 정산 코드
            String SettleCode = "202301160000000010";

            // 팝빌회원 아이디
            String UserID = "testkorea";

            try
            {
                PaymentHistory result =
                    taxinvoiceService.GetSettleResult(CorpNum, SettleCode, UserID);

                MessageBox.Show(
                    "결제 내용(productType) : " + result.productType + CRLF +
                        "결제 상품명(productName) : " + result.productName + CRLF +
                        "결제 유형(settleType) : " + result.settleType + CRLF +
                        "담당자명(settlerName) : " + result.settlerName + CRLF +
                        "담당자메일(settlerEmail) : " + result.settlerEmail + CRLF +
                        "결제 금액(settleCost) : " + result.settleCost + CRLF +
                        "충전포인트(settlePoint) : " + result.settlePoint + CRLF +
                        "결제 상태(settleState) : " + result.settleState.ToString() + CRLF +
                        "등록일시(regDT) : " + result.regDT + CRLF +
                        "상태일시(stateDT) : " + result.stateDT + CRLF,
                    "무통장 입금 신청내역 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "무통장 입금 신청내역 확인");
            }
        }

        /**
         * 연동회원의 포인트 사용내역을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetUseHistory
         */
        public void btnGetUseHistory_Click(object sender, EventArgs e)
        {
            // 팝빌 회원 아이디
            String CorpNum = "1234567890";

            // 조회 시작 일자
            String SDate = "20230501";

            // 조회 종료 일자
            String EDate = "20230530";

            // 목록 페이지 번호
            int Page = 1;

            // 페이지당 목록 개수
            int PerPage = 500;

            // 목록 정렬 방향
            String Order = "D";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                UseHistoryResult result =
                    taxinvoiceService.GetUseHistory(CorpNum, SDate, EDate, Page, PerPage, Order, UserID);

                String tmp = "";

                foreach (UseHistory history in result.list)
                {
                    tmp += "서비스 코드(itemCode) : " + history.itemCode + CRLF;
                    tmp += "포인트 증감 유형(txType) : " + history.txType + CRLF;
                    tmp += "증감 포인트(txPoint) : " + history.txPoint + CRLF;
                    tmp += "잔여 포인트(balance) : " + history.balance + CRLF;
                    tmp += "포인트 증감 일시(txDT) : " + history.txDT + CRLF;
                    tmp += "담당자 아이디(userID) : " + history.userID + CRLF;
                    tmp += "담당자명(userName) : " + history.userName + CRLF;
                    tmp += CRLF;
                }

                MessageBox.Show(
                    "응답코드(code) : " + result.code.ToString() + CRLF+
                    "총 검색결과 건수(total) : " + result.total.ToString() + CRLF+
                    "페이지당 검색개수(perPage) : " + result.perPage.ToString() +CRLF+
                    "페이지 번호(pageNum) : " + result.pageNum.ToString() +CRLF+
                    "페이지 개수(pageCount) : " + result.pageCount.ToString() +CRLF +
                    "사용내역"+CRLF+
                    tmp,
                     "포인트 사용내역 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "포인트 사용내역 확인");
            }
        }

        /**
         * 연동회원 포인트를 환불 신청합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#Refund
         */
        public void btnRefund_Click(object sender, EventArgs e)
        {
            // 팝빌 회원 사업자번호
            String CorpNum = "1234567890";

            // 환불 신청 객체
            RefundForm refundForm = new RefundForm();

            // 담당자명
            refundForm.ContactName = "담당자명";

            // 담당자 연락처
            refundForm.TEL = "010-1234-1234";

            // 환불 신청 포인트
            refundForm.RequestPoint = "100";

            // 은행명
            refundForm.AccountBank = "국민";

            // 계좌 번호
            refundForm.AccountNum = "123-12-10981204";

            // 예금주명
            refundForm.AccountName = "예금주";

            // 환불 사유
            refundForm.Reason = "환불 사유";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                RefundResponse result = taxinvoiceService.Refund(CorpNum, refundForm, UserID);
                MessageBox.Show(
                    "code (응답 코드) : " + result.code.ToString() + CRLF +
                    "message (응답 메시지) : " + result.message + CRLF +
                    "refundCode (환불코드) : " + result.refundCode,
                    "환불 신청");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "환불 신청");
            }
        }

        /**
         * 연동회원의 포인트 환불신청내역을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetRefundHistory
         */
        public void btnGetRefundHistory_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 목록 페이지 번호
            int Page = 1;

            // 페이지당 목록 개수
            int PerPage = 500;

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                RefundHistoryResult result = taxinvoiceService.GetRefundHistory(CorpNum, Page, PerPage, UserID);
                String tmp = "";

                foreach (RefundHistory history in result.list)
                {
                    tmp += "reqDT (신청일시) :" + history.reqDT + CRLF ;
                    tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + CRLF ;
                    tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + CRLF ;
                    tmp += "accountNum (환불계좌번호) :" + history.accountNum + CRLF ;
                    tmp += "accountName (환불계좌 예금주명) :" + history.accountName + CRLF ;
                    tmp += "state (상태) : " + history.state.ToString() + CRLF ;
                    tmp += "reason (환불사유) : " + history.reason + CRLF;
                    tmp += CRLF;
                }

                MessageBox.Show(
                    "응답코드(code) : " + result.code.ToString() + CRLF+
                    "총 검색결과 건수(total) : " + result.total.ToString() + CRLF+
                    "페이지당 검색개수(perPage) : " + result.perPage.ToString() +CRLF+
                    "페이지 번호(pageNum) : " + result.pageNum.ToString() +CRLF+
                    "페이지 개수(pageCount) : " + result.pageCount.ToString() +CRLF +
                    "환불내역"+CRLF+
                    tmp,
                "환불 신청내역 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message,
                "환불 신청내역 확인");
            }
        }


        /**
         * 포인트 환불에 대한 상세정보 1건을 확인합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetRefundInfo
         */
        public void btnGetRefundInfo_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 환불 코드
            String RefundCode = "023040000017";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                RefundHistory result = taxinvoiceService.GetRefundInfo(CorpNum, RefundCode, UserID);
                MessageBox.Show(
                    "reqDT (신청일시) :" + result.reqDT + CRLF+
                        "requestPoint (환불 신청포인트) :" + result.requestPoint + CRLF+
                        "accountBank (환불계좌 은행명) :" + result.accountBank + CRLF+
                        "accountNum (환불계좌번호) :" + result.accountNum + CRLF+
                        "accountName (환불계좌 예금주명) :" + result.accountName + CRLF+
                        "state (상태) : " + result.state.ToString() + CRLF+
                        "reason (환불사유) : " + result.reason,
                        "환불 신청 상세정보 확인"
                    );
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message,
                    "환불 신청 상세정보 확인");
            }
        }

        /**
         * 환불 가능한 포인트를 확인합니다. (보너스 포인트는 환불가능포인트에서 제외됩니다.)
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/point#GetRefundableBalance
         */
        public void btnGetRefundableBalance_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Double refundableBalance = taxinvoiceService.GetRefundableBalance(CorpNum, UserID);
                MessageBox.Show("refundableBalance (환불 가능 포인트) : "+ refundableBalance.ToString(),
                    "환불 가능 포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message,
                    "환불 가능 포인트 확인");
            }
        }

        /**
         * 가입된 연동회원의 탈퇴를 요청합니다.
         * - 회원탈퇴 신청과 동시에 팝빌의 모든 서비스 이용이 불가하며, 관리자를 포함한 모든 담당자 계정도 일괄탈퇴 됩니다.
         * - 회원탈퇴로 삭제된 데이터는 복원이 불가능합니다.
         * - 관리자 계정만 회원탈퇴가 가능합니다.
         * - https://developers.popbill.com/reference/taxinvoice/dotnet/api/member#QuitMember
         */
        public void btnQuitMember_Click(object sender, EventArgs e)
        {

            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 탈퇴 사유
            String QuitReason = "탈퇴 사유";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Response response = taxinvoiceService.QuitMember(CorpNum, QuitReason, UserID);
                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message,
                                "팝빌 회원 탈퇴");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message,
                                "팝빌 회원 탈퇴");
            }
        }

        // /*
        //  * [승인대기] 상태의 세금계산서를 [공급받는자]가 [거부]합니다.
        //  * - [거부]처리된 세금계산서를 삭제(Delete API)하면 등록된 문서번호를 재사용할 수 있습니다.
        //  */
        // private void btnDeny_Click(object sender, EventArgs e)
        // {
        //     // 세금계산서 발행유형
        //     MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
        //
        //     //메모
        //     string memo = "발행예정 거부 메모";
        //
        //     try
        //     {
        //         Response response =
        //             taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);
        //
        //         MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + response.message, "발행예정 거부");
        //     }
        //     catch (PopbillException ex)
        //     {
        //         MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + ex.Message, "발행예정 거부");
        //     }
        // }


        //  * [임시저장] 상태의 세금계산서를 [공급자]가 [발행예정]합니다.
        //  * - 발행예정이란 공급자와 공급받는자 사이에 세금계산서 확인 후 발행하는 방법입니다.
        //  * - "[전자세금계산서 API 연동매뉴얼] > 1.2.1. 정발행 > 다. 임시저장 발행예정" 의 프로세스를 참조하시기 바랍니다.
        //  */
        // private void btnSend_Click(object sender, EventArgs e)
        // {
        //     // 세금계산서 발행유형
        //     MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
        //
        //     //메모
        //     String Memo = "발행예정 메모";
        //
        //     //발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송
        //     String EmailSubject = "";
        //
        //     try
        //     {
        //         Response response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject,
        //             txtUserId.Text);
        //
        //         MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + response.message, "세금계산서 발행예정 ");
        //     }
        //     catch (PopbillException ex)
        //     {
        //         MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + ex.Message, "세금계산서 발행예정");
        //     }
        // }
        //
        // /*
        //  * [승인대기] 상태의 세금계산서를 [공급자]가 [취소]합니다.
        //  * - [취소]된 세금계산서를 삭제(Delete API)하면 등록된 문서번호를 재사용할 수 있습니다.
        //  */
        // private void btnCancelSend_Click(object sender, EventArgs e)
        // {
        //     // 세금계산서 발행유형
        //     MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
        //
        //     // 발행예정 취소 메모
        //     string memo = "발행예정 취소 메모 ";
        //
        //     try
        //     {
        //         Response response =
        //             taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);
        //
        //         MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + response.message, "발행예정 취소");
        //     }
        //     catch (PopbillException ex)
        //     {
        //         MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + ex.Message, "발행예정 취소 ");
        //     }
        // }
        //
        // /*
        //  * [승인대기] 상태의 세금계산서를 [공급받는자]가 [승인]합니다.
        //  */
        // private void btnAccept_Click(object sender, EventArgs e)
        // {
        //     // 세금계산서 발행유형
        //     MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
        //
        //     // 메모
        //     string memo = "발행예정 승인 메모";
        //
        //     try
        //     {
        //         Response response =
        //             taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, memo, txtUserId.Text);
        //
        //         MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + response.message, "발행예정 승인");
        //     }
        //     catch (PopbillException ex)
        //     {
        //         MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
        //                         "응답메시지(message) : " + ex.Message, "발행예정 승인");
        //     }
        // }
    }
}
