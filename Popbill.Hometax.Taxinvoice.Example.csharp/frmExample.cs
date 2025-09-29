/*
* 팝빌 홈택스 전자세금계산서 API .NET SDK C#.NET Example
* C#.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/httaxinvoice/dotnet/getting-started/tutorial?fwn=csharp
*
* 업데이트 일자 : 2025-09-29
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
* 3) 홈택스 로그인 인증정보를 등록합니다. (부서사용자등록 / 공동인증서 등록)
*    - 팝빌로그인 > [홈택스수집] > [환경설정] > [인증 관리] 메뉴
*    - 홈택스수집 인증 관리 팝업 URL(GetCertificatePopUpURL API) 반환된 URL을 이용하여
*      홈택스 인증 처리를 합니다.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.HomeTax.Taxinvoice.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private HTTaxinvoiceService htTaxinvoiceService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 홈택스 전자세금계산서 연계 서비스 모듈 초기화
            htTaxinvoiceService = new HTTaxinvoiceService(LinkID, SecretKey);

            // 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
            htTaxinvoiceService.IsTest = true;

            // 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
            htTaxinvoiceService.IPRestrictOnOff = true;

            // 통신 IP 고정, true-사용, false-미사용, (기본값:false)
            htTaxinvoiceService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
            htTaxinvoiceService.UseLocalTimeYN = false;
        }

        /*
         * 홈택스에 신고된 전자세금계산서 매입/매출 내역 수집을 팝빌에 요청합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/job#RequestJob
         */
        private void btnRequestJob_Click(object sender, EventArgs e)
        {
            // 전자(세금)계산서 유형 SELL-매출, BUY-매입, TRUSTEE-수탁
            KeyType tiKeyType = KeyType.SELL;

            // 일자유형, W-작성일자, I-발행일자, S-전송일자
            String DType = "S";

            // 시작일자, 표시형식(yyyyMMdd)
            String SDate = "20250701";

            // 종료일자, 표시형식(yyyyMMdd)
            String EDate = "20250731";

            try
            {
                String jobID = htTaxinvoiceService.RequestJob(txtCorpNum.Text, tiKeyType, DType, SDate, EDate);

                MessageBox.Show("작업아이디(jobID) : " + jobID, "수집 요청");

                txtJobID.Text = jobID;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "수집 요청");
            }
        }

        /*
         * [RequestJob - 수집 요청] API를 호출하고 반환 받은 작업아이디(JobID)를 이용하여 수집 상태를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/job#GetJobState
         */
        private void btnGetJobState_Click(object sender, EventArgs e)
        {
            try
            {
                HTTaxinvoiceJobState jobState = htTaxinvoiceService.GetJobState(txtCorpNum.Text, txtJobID.Text);

                String tmp = "jobID (작업아이디) : " + jobState.jobID + CRLF;
                tmp += "jobState (수집상태) : " + jobState.jobState.ToString() + CRLF;
                tmp += "queryType (전자세금계산서 유형) : " + jobState.queryType + CRLF;
                tmp += "queryDateType (일자유형) : " + jobState.queryDateType + CRLF;
                tmp += "queryStDate (시작일자) : " + jobState.queryStDate + CRLF;
                tmp += "queryEnDate (종료일자) : " + jobState.queryEnDate + CRLF;
                tmp += "errorCode (수집 결과코드) : " + jobState.errorCode.ToString() + CRLF;
                tmp += "errorReason (오류메시지) : " + jobState.errorReason + CRLF;
                tmp += "jobStartDT (작업 시작일시) : " + jobState.jobStartDT + CRLF;
                tmp += "jobEndDT (작업 종료일시) : " + jobState.jobEndDT + CRLF;
                tmp += "collectCount (수집건수) : " + jobState.collectCount.ToString() + CRLF;
                tmp += "regDT (수집 요청일시) : " + jobState.regDT + CRLF;

                MessageBox.Show(tmp, "수집 상태 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "수집 상태 확인");
            }
        }

        /*
         * [RequestJob – 수집 요청] API를 호출하고 반환 받은 작업아이디(JobID) 목록의 수집 상태를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/job#ListActiveJob
         */
        private void btnListActiveJob_Click(object sender, EventArgs e)
        {
            try
            {
                List<HTTaxinvoiceJobState> jobList = htTaxinvoiceService.ListActiveJob(txtCorpNum.Text);

                String tmp = "jobID(작업아이디) | jobState(수집상태) | queryType(전자세금계산서 유형) | queryDateType(일자유형) | queryStDate(시작일자) |";
                tmp += "queryEnDate(종료일자) | errorCode(수집 결과코드) | errorReason(오류메시지) | jobStartDT(작업 시작일시) | jobEndDT(작업 종료일시) |";
                tmp += "collectCount(수집개수) | regDT(수집 요청일시) " + CRLF;

                for (int i = 0; i < jobList.Count; i++)
                {
                    tmp += jobList[i].jobID + " | ";
                    tmp += jobList[i].jobState.ToString() + " | ";
                    tmp += jobList[i].queryType + " | ";
                    tmp += jobList[i].queryDateType + " | ";
                    tmp += jobList[i].queryStDate + " | ";
                    tmp += jobList[i].queryEnDate + " | ";
                    tmp += jobList[i].errorCode.ToString() + " | ";
                    tmp += jobList[i].errorReason + " | ";
                    tmp += jobList[i].jobStartDT + " | ";
                    tmp += jobList[i].jobEndDT + " | ";
                    tmp += jobList[i].collectCount.ToString() + " | ";
                    tmp += jobList[i].regDT;

                    tmp += CRLF + CRLF;
                }

                if (jobList.Count > 0) txtJobID.Text = jobList[0].jobID;


                MessageBox.Show(tmp, "수집 요청 목록확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "수집 요청 목록확인");
            }
        }

        /*
         * 홈택스에서 수집된 전자세금계산서 매입/매출 내역을 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 문서형태 배열 ("N" 와 "M" 중 선택, 다중 선택 가능)
            // └ N = 일반 , M = 수정
            // - 미입력 시 전체조회
            String[] Type = { "N", "M" };

            // 과세형태 배열 ("T" , "N" , "Z" 중 선택, 다중 선택 가능)
            // └ T = 과세, N = 면세, Z = 영세
            // - 미입력 시 전체조회
            String[] TaxType = { "T", "N", "Z" };

            // 발행목적 배열 ("R" , "C", "N" 중 선택, 다중 선택 가능)
            // └ R = 영수, C = 청구, N = 없음
            // - 미입력 시 전체조회
            String[] PurposeType = { "R", "C", "N" };

            // 종사업장번호 유무 (null , "0" , "1" 중 택 1)
            // - null = 전체조회 , 0 = 없음, 1 = 있음
            String TaxRegIDYN = "";

            // 종사업장번호의 주체 ("S" , "B" , "T" 중 택 1)
            // - S = 공급자 , B = 공급받는자 , T = 수탁자
            String TaxRegIDType = "S";

            // 종사업장번호
            // - 다수기재 시 콤마(",")로 구분. ex) "0001,0002"
            // - 미입력 시 전체조회
            String TaxRegID = "";

            // 페이지번호
            int Page = 1;

            // 페이지당 목록개수, 최대 1000건
            int PerPage = 10;

            // 정렬방향 D-내림차순, A-오름차순
            String Order = "D";

            // 거래처 상호 / 사업자번호 (사업자) / 주민등록번호 (개인) / "9999999999999" (외국인) 중 검색하고자 하는 정보 입력
            // - 사업자번호 / 주민등록번호는 하이픈('-')을 제외한 숫자만 입력
            // - 미입력시 전체조회
            String SearchString = "";

            listBox1.Items.Clear();

            try
            {
                HTTaxinvoiceSearch searchInfo = htTaxinvoiceService.Search(txtCorpNum.Text, txtJobID.Text, Type, TaxType,
                                        PurposeType, TaxRegIDYN, TaxRegIDType, TaxRegID, Page, PerPage, Order, txtUserId.Text, SearchString);

                String tmp = "code (응답코드) : " + searchInfo.code.ToString() + CRLF;
                tmp += "message (응답메시지) : " + searchInfo.message + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchInfo.total.ToString() + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchInfo.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchInfo.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchInfo.pageCount + CRLF;

                MessageBox.Show(tmp, "수집 결과 조회");

                string rowStr = "invoiceType(구분) | writeDate(작성일자) | issueDate(발행일자) | sendDate(전송일자) | invoiceeCorpName(공급자 상호) | invoiceeCorpNum( 공급자 사업자번호) | " +
                                "taxType(과세형태) | supplyCost(공급가액) | modifyYN(문서형태) | ntsconfirmNum(국세청승인번호) ";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < searchInfo.list.Count; i++)
                {
                    rowStr = null;
                    rowStr += searchInfo.list[i].invoiceType + " | ";
                    rowStr += searchInfo.list[i].writeDate + " | ";
                    rowStr += searchInfo.list[i].issueDate + " | ";
                    rowStr += searchInfo.list[i].sendDate + " | ";
                    rowStr += searchInfo.list[i].invoiceeCorpName + " | ";
                    rowStr += searchInfo.list[i].invoiceeCorpNum + " | ";
                    rowStr += searchInfo.list[i].taxType + " | ";
                    rowStr += searchInfo.list[i].supplyCost + " | ";
                    rowStr += searchInfo.list[i].issueDate + " | ";

                    if ((bool)searchInfo.list[i].modifyYN)
                    {
                        rowStr += "수정" + " | ";
                    }
                    else
                    {
                        rowStr += "일반" + " | ";
                    }

                    rowStr += searchInfo.list[i].ntsconfirmNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "수집 결과 조회");
            }
        }

        /*
         * 홈택스에서 수집된 전자세금계산서 매입/매출 내역의 합계정보를 제공합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#Summary
         */
        private void btnSummary_Click(object sender, EventArgs e)
        {

            // 문서형태 배열 ("N" 와 "M" 중 선택, 다중 선택 가능)
            // └ N = 일반 , M = 수정
            // - 미입력 시 전체조회
            String[] Type = { "N", "M" };

            // 과세형태 배열 ("T" , "N" , "Z" 중 선택, 다중 선택 가능)
            // └ T = 과세, N = 면세, Z = 영세
            // - 미입력 시 전체조회
            String[] TaxType = { "T", "N", "Z" };

            // 발행목적 배열 ("R" , "C", "N" 중 선택, 다중 선택 가능)
            // └ R = 영수, C = 청구, N = 없음
            // - 미입력 시 전체조회
            String[] PurposeType = { "R", "C", "N" };

            // 종사업장번호 유무 (null , "0" , "1" 중 택 1)
            // - null = 전체조회 , 0 = 없음, 1 = 있음
            String TaxRegIDYN = "";

            // 종사업장번호의 주체 ("S" , "B" , "T" 중 택 1)
            // - S = 공급자 , B = 공급받는자 , T = 수탁자
            String TaxRegIDType = "S";

            // 종사업장번호
            // - 다수기재 시 콤마(",")로 구분. ex) "0001,0002"
            // - 미입력 시 전체조회
            String TaxRegID = "";

            // 거래처 상호 / 사업자번호 (사업자) / 주민등록번호 (개인) / "9999999999999" (외국인) 중 검색하고자 하는 정보 입력
            // - 사업자번호 / 주민등록번호는 하이픈('-')을 제외한 숫자만 입력
            // - 미입력시 전체조회
            String SearchString = "";

            try
            {
                HTTaxinvoiceSummary summaryInfo = htTaxinvoiceService.Summary(txtCorpNum.Text, txtJobID.Text, Type,
                                                            TaxType, PurposeType, TaxRegIDYN, TaxRegIDType, TaxRegID,
                                                            txtUserId.Text, SearchString);

                String tmp = "count (수집 결과 건수) : " + summaryInfo.count.ToString() + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + summaryInfo.supplyCostTotal.ToString() + CRLF;
                tmp += "taxTotal (세액 합계) : " + summaryInfo.taxTotal.ToString() + CRLF;
                tmp += "amountTotal (합계 금액) : " + summaryInfo.amountTotal.ToString() + CRLF;

                MessageBox.Show(tmp, "수집 결과 요약정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "수집결과 요약정보 조회");
            }
        }

        /*
         * 홈택스에서 수집된 전자세금계산서 1건의 상세정보를 제공합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetTaxinvoice
         */
        private void btnGetTaxinvocie_Click(object sender, EventArgs e)
        {
            try
            {
                HTTaxinvoice tiInfo = htTaxinvoiceService.GetTaxinvoice(txtCorpNum.Text, txtNTSconfirmNum.Text);

                String tmp = "writeDate (작성일자) : " + tiInfo.writeDate + CRLF;
                tmp += "issueDT (발행일시) : " + tiInfo.issueDT + CRLF;
                tmp += "invoiceType (전자세금계산서 종류) : " + tiInfo.invoiceType.ToString() + CRLF;
                tmp += "taxType (과세형태) : " + tiInfo.taxType + CRLF;
                tmp += "taxTotal (세액 합계) : " + tiInfo.taxTotal + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + tiInfo.supplyCostTotal + CRLF;
                tmp += "totalAmount (합계금액) : " + tiInfo.totalAmount + CRLF;

                tmp += "purposeType (영수/청구) : " + tiInfo.purposeType + CRLF;
                tmp += "serialNum (일련번호) : " + tiInfo.serialNum + CRLF;
                tmp += "cash (현금) : " + tiInfo.cash + CRLF;
                tmp += "chkBill (수표) : " + tiInfo.chkBill + CRLF;
                tmp += "credit (외상) : " + tiInfo.credit + CRLF;
                tmp += "note (어음) : " + tiInfo.note + CRLF;
                tmp += "remark1 (비고1) : " + tiInfo.remark1 + CRLF;
                tmp += "remakr2 (비고2) : " + tiInfo.remark2 + CRLF;
                tmp += "remark3 (비고3) : " + tiInfo.remark3 + CRLF;

                tmp += "ntsconfirmNum (국세청승인번호) : " + tiInfo.ntsconfirmNum + CRLF + CRLF;

                tmp += "========공급자 정보========" + CRLF;
                tmp += "inovicerCorpNum (사업자번호) : " + tiInfo.invoicerCorpNum + CRLF;
                tmp += "invoicerMgtKey (공급자 문서번호) : " + tiInfo.invoicerMgtKey + CRLF;
                tmp += "invoicerTaxRegID (종사업장 식별번호) : " + tiInfo.invoicerTaxRegID + CRLF;
                tmp += "invoicerCorpName (상호) : " + tiInfo.invoicerCorpName + CRLF;
                tmp += "invoicerCEOName (대표자 성명) : " + tiInfo.invoicerCEOName + CRLF;
                tmp += "invoicerAddr (주소) : " + tiInfo.invoicerAddr + CRLF;
                tmp += "invoicerBizType (업태) : " + tiInfo.invoicerBizType + CRLF;
                tmp += "invoicerBizClass (종목) : " + tiInfo.invoicerBizClass + CRLF;
                tmp += "invoicerContactName (담당자 성명) : " + tiInfo.invoicerContactName + CRLF;
                tmp += "invoicerDeptName (담당자 부서명) : " + tiInfo.invoicerDeptName + CRLF;
                tmp += "invoicerTEL (담당자 연락처) : " + tiInfo.invoicerTEL + CRLF;
                tmp += "invoicerEmail (담당자 이메일) : " + tiInfo.invoicerEmail + CRLF + CRLF;

                tmp += "========공급받는자 정보========" + CRLF;
                tmp += "invoiceeCorpNum (사업자번호) : " + tiInfo.invoiceeCorpNum + CRLF;
                tmp += "invoiceeType (공급받는자 구분) : " + tiInfo.invoiceeType + CRLF;
                tmp += "invoiceeMgtKey (공급받는자 문서번호) : " + tiInfo.invoiceeMgtKey + CRLF;
                tmp += "invoiceeTaxRegID (종사업장 식별번호) : " + tiInfo.invoiceeTaxRegID + CRLF;
                tmp += "invoiceeCorpName (상호) : " + tiInfo.invoiceeCorpName + CRLF;
                tmp += "invoiceeCEOName (대표자 성명) : " + tiInfo.invoiceeCEOName + CRLF;
                tmp += "invoiceeAddr (주소) : " + tiInfo.invoiceeAddr + CRLF;
                tmp += "invoiceeBizType (업태) : " + tiInfo.invoiceeBizType + CRLF;
                tmp += "invoiceeBizClass (종목) : " + tiInfo.invoiceeBizClass + CRLF;
                tmp += "invoiceeContactName1 (담당자 성명) : " + tiInfo.invoiceeContactName1 + CRLF;
                tmp += "invoiceeDeptName1 (담당자 부서명) : " + tiInfo.invoiceeDeptName1 + CRLF;
                tmp += "invoiceeTEL1 (담당자 연락처) : " + tiInfo.invoiceeTEL1 + CRLF;
                tmp += "invoiceeEmail1 (담당자 이메일) : " + tiInfo.invoiceeEmail1 + CRLF + CRLF;

               
                tmp += CRLF + "=========수정 전자(세금)계산서 정보========" + CRLF;
                tmp += "modifyCode (수정 사유코드) : " + tiInfo.modifyCode.ToString() + CRLF;
                tmp += "orgNTSConfirmNum (당초 국세청승인번호) : " + tiInfo.orgNTSConfirmNum + CRLF + CRLF;

                tmp += "========품목 정보========" + CRLF;

                for (int i = 0; i < tiInfo.detailList.Count; i++)
                {
                    tmp += "serialNum (일련번호) : " + tiInfo.detailList[i].serialNum.ToString() + CRLF;
                    tmp += "purchaseDT (거래일자) : " + tiInfo.detailList[i].purchaseDT + CRLF;
                    tmp += "itemName (품명) : " + tiInfo.detailList[i].itemName + CRLF;
                    tmp += "spec (규격) : " + tiInfo.detailList[i].spec + CRLF;
                    tmp += "qty (수량) : " + tiInfo.detailList[i].qty + CRLF;
                    tmp += "unitCost (단가) : " + tiInfo.detailList[i].unitCost + CRLF;
                    tmp += "supplyCost (공급가액) : " + tiInfo.detailList[i].supplyCost + CRLF;
                    tmp += "tax (세액) : " + tiInfo.detailList[i].tax + CRLF;
                    tmp += "remark (비고) : " + tiInfo.detailList[i].remark + CRLF;
                }


                MessageBox.Show(tmp, "상세정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "상세정보 조회");
            }
        }

        /*
         * 홈택스에서 수집된 전자세금계산서 1건의 상세정보를 XML 데이터 포맷으로 제공합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetXML
         */
        private void btnGetXML_Click(object sender, EventArgs e)
        {
            try
            {
                HTTaxinvoiceXML tiXML = htTaxinvoiceService.GetXML(txtCorpNum.Text, txtNTSconfirmNum.Text);

                String tmp = "ResultCode (응답코드) : " + tiXML.ResultCode.ToString() + CRLF;
                tmp += "Message (응답메시지) : " + tiXML.Message + CRLF;
                tmp += "retObject (전자세금계산서 XML 문서) : " + tiXML.retObject + CRLF;

                MessageBox.Show(tmp, "상세정보 조회 - XML");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "상세정보 조회 - XML");
            }
        }

        /*
         * 홈택스에서 수집된 전자세금계산서 1건의 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetPopUpURL
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            // 조회할 전자세금계산서 국세청승인번호
            String NTSConfirmNum = txtNTSconfirmNum.Text;

            try
            {
                String url = htTaxinvoiceService.GetPopUpURL(txtCorpNum.Text, NTSConfirmNum, txtUserId.Text);

                MessageBox.Show(url, "홈택스 전자세금계산서 보기 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "홈택스 전자세금계산서 보기 팝업 URL");
            }
        }

        /*
         * 홈택스에서 수집된 전자세금계산서 1건의 인쇄 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/search#GetPrintURL
         */
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            // 인쇄할 전자세금계산서 국세청승인번호
            String NTSConfirmNum = txtNTSconfirmNum.Text;

            try
            {
                String url = htTaxinvoiceService.GetPrintURL(txtCorpNum.Text, NTSConfirmNum);

                MessageBox.Show(url, "홈택스 전자세금계산서 인쇄 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "홈택스 전자세금계산서 인쇄 팝업 URL");
            }
        }

        /*
         * 홈택스 인증정보를 등록하는 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#GetCertificatePopUpURL
         */
        private void btnGetCertificatePopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                String url = htTaxinvoiceService.GetCertificatePopUpURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "홈택스수집 인증관리 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "홈택스수집 인증관리 URL");
            }
        }

        /*
         * 팝빌에 등록된 인증서의 만료일자를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#GetCertificateExpireDate
         */
        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = htTaxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show(expiration.ToString(), "인증서 만료일시 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인증서 만료일시 확인");
            }
        }

        /*
         * 팝빌에 등록된 인증서로 홈택스 로그인 가능 여부를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#CheckCertValidation
         */
        private void btnCheckCertValidation_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckCertValidation(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "인증서 로그인 테스트");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "인증서 로그인 테스트");
            }
        }

        /*
         * 팝빌에 전자세금계산서 전용 부서사용자를 등록합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#RegistDeptUser
         */
        private void btnRegistDeptUser_Click(object sender, EventArgs e)
        {
            // 부서사용자 아이디
            String deptUserID = "userid";

            // 부서사용자 비밀번호
            String deptUserPWD = "passwd";

            // 부서사용자 대표자 주민번호
            String identityNum = "";

            // 팝빌회원 아이디
            String userID = "testkorea";

            try
            {
                Response response = htTaxinvoiceService.RegistDeptUser(txtCorpNum.Text, deptUserID, deptUserPWD, identityNum, userID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "부서사용자 계정등록");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "부서사용자 계정등록");
            }
        }

        /*
         * 팝빌에 부서사용자 등록 여부를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#CheckDeptUser
         */
        private void btnCheckDeptUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckDeptUser(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "부서사용자 등록정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "부서사용자 등록정보 확인");
            }
        }

        /*
         * 팝빌에 등록된 부서사용자로 홈택스 로그인 가능 여부를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#CheckLoginDeptUser
         */
        private void btnCheckLoginDeptUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckLoginDeptUser(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "부서사용자 로그인 테스트");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "부서사용자 로그인 테스트");
            }
        }

        /*
         * 팝빌에 등록된 부서사용자 계정을 삭제합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/api/cert#DeleteDeptUser
         */
        private void btnDeleteDeptUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.DeleteDeptUser(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "부서사용자 등록정보 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "부서사용자 등록정보 삭제");
            }
        }

        /*
         * 정액제를 신청하는 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetFlatRatePopUpURL
         */
        private void btnGetFlatRatePopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                String url = htTaxinvoiceService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "정액제 서비스 신청 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 신청 팝업 URL");
            }
        }

        /*
         * 홈택스수집 정액제 서비스 상태를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetFlatRateState
         */
        private void btnGetFlatRateState_Click(object sender, EventArgs e)
        {
            try
            {
                HTFlatRate rateInfo = htTaxinvoiceService.GetFlatRateState(txtCorpNum.Text);

                String tmp = "referenceID (사업자번호) : " + rateInfo.referenceID + CRLF;
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 상태 확인");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetBalance
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = htTaxinvoiceService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");

            }
        }

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htTaxinvoiceService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트 충전 URL");
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htTaxinvoiceService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htTaxinvoiceService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetPartnerBalance
         */
        private void btnGetPartnerBalance1_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = htTaxinvoiceService.GetPartnerBalance(txtCorpNum.Text);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htTaxinvoiceService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 팝빌 홈택스수집(세금) API 서비스 과금정보를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = htTaxinvoiceService.GetChargeInfo(txtCorpNum.Text);

                string tmp = null;
                tmp += "unitCost (단가-월정액) : " + chrgInf.unitCost + CRLF;
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "연동회원 가입여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 가입여부 확인");
            }
        }

        /*
         * 사용하고자 하는 아이디의 중복여부를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckID(txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "ID 중복여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "ID 중복여부 확인");
            }
        }

        /*
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#JoinMember
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
                Response response = htTaxinvoiceService.JoinMember(joinInfo);

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
         * 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htTaxinvoiceService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팝빌 로그인 URL 확인");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL 확인");
            }
        }

        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#GetCorpInfo
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = htTaxinvoiceService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#UpdateCorpInfo
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
                Response response = htTaxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#RegistContact
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

            // 담당자 휴대폰 (최대 20자)
            contactInfo.tel = "";

            // 담당자 메일 (최대 100자)
            contactInfo.email = "";

            // 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = htTaxinvoiceService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 추가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 추가");
            }
        }

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = htTaxinvoiceService.GetContactInfo(txtCorpNum.Text, contactID);

                String tmp = null;

                tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                tmp += "personName (담당자 성명) : " + contactInfo.personName + CRLF;
                tmp += "tel (담당자 휴대폰) : " + contactInfo.tel + CRLF;
                tmp += "email (담당자 메일) : " + contactInfo.email + CRLF;
                tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                tmp += "searchRole (권한) : " + contactInfo.searchRole + CRLF;
                tmp += "mgrYN (역할) : " + contactInfo.mgrYN + CRLF;
                tmp += "state (계정상태) : " + contactInfo.state + CRLF;
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = htTaxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;

                foreach (Contact contactInfo in contactList)
                {
                    tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                    tmp += "personName (담당자 성명) : " + contactInfo.personName + CRLF;
                    tmp += "tel (담당자 휴대폰) : " + contactInfo.tel + CRLF;
                    tmp += "email (담당자 메일) : " + contactInfo.email + CRLF;
                    tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                    tmp += "searchRole (권한) : " + contactInfo.searchRole + CRLF;
                    tmp += "mgrYN (역할) : " + contactInfo.mgrYN + CRLF;
                    tmp += "state (계정상태) : " + contactInfo.state + CRLF;
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#UpdateContact
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 아이디
            contactInfo.id = txtUserId.Text;

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자123";

            // 담당자 휴대폰 (최대 20자)
            contactInfo.tel = "";

            // 담당자 이메일 (최대 100자)
            contactInfo.email = "";

            // 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = htTaxinvoiceService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 수정");
            }
        }

        /**
         * 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#PaymentRequest
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
                PaymentResponse response = htTaxinvoiceService.PaymentRequest(CorpNum, PaymentForm, UserID);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetPaymentHistory
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
                    htTaxinvoiceService.GetPaymentHistory(CorpNum, SDate, EDate, Page, PerPage, UserID);

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
                    + "결제내역" + CRLF + tmp,
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetSettleResult
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
                    htTaxinvoiceService.GetSettleResult(CorpNum, SettleCode, UserID);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetUseHistory
         */
        public void btnGetUseHistory_Click(object sender, EventArgs e)
        {
            // 팝빌 회원 아이디
            String CorpNum = "1234567890";

            // 조회 시작 일자
            String SDate = "20250701";

            // 조회 종료 일자
            String EDate = "20250731";

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
                    htTaxinvoiceService.GetUseHistory(CorpNum, SDate, EDate, Page, PerPage, Order, UserID);

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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#Refund
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
                RefundResponse result = htTaxinvoiceService.Refund(CorpNum, refundForm, UserID);
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetRefundHistory
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
                RefundHistoryResult result = htTaxinvoiceService.GetRefundHistory(CorpNum, Page, PerPage, UserID);
                String tmp = "";

                foreach (RefundHistory history in result.list)
                {
                    tmp += "reqDT (신청일시) :" + history.reqDT + CRLF ;
                    tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + CRLF ;
                    tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + CRLF ;
                    tmp += "accountNum (환불계좌번호) :" + history.accountNum + CRLF ;
                    tmp += "accountName (환불계좌 예금주명) :" + history.accountName + CRLF ;
                    tmp += "state (상태) : " + history.state.ToString() + CRLF ;
                    tmp += "reason (환불사유) : " + history.reason+ CRLF ;
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetRefundInfo
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
                RefundHistory result = htTaxinvoiceService.GetRefundInfo(CorpNum, RefundCode, UserID);
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/point#GetRefundableBalance
         */
        public void btnGetRefundableBalance_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Double refundableBalance = htTaxinvoiceService.GetRefundableBalance(CorpNum, UserID);
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
         * - https://developers.popbill.com/reference/httaxinvoice/dotnet/common-api/member#QuitMember
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
                Response response = htTaxinvoiceService.QuitMember(CorpNum, QuitReason, UserID);
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

        /**
         * 연동회원에 추가된 담당자를 삭제합니다.
         * - https://developers.popbill.com/reference/taxinvoice/java/common-api/member#DeleteContact
         */

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            String ContactID = "testkorea20250722_01";

            try
            {
                Response response = htTaxinvoiceService.DeleteContact(txtCorpNum.Text, ContactID, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 삭제");
            }
        }
    }
}
