/*
 * 팝빌 홈택스 전자세금계산서 연계 API DotNet SDK Example
 *
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - https://docs.popbill.com/httaxinvoice/tutorial/dotnet
 * - 업데이트 일자 : 2021-08-05
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 *
 * <테스트 연동개발 준비사항>
 * 1) 32, 35 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 * 3) 홈택스 인증 처리를 합니다. (부서사용자등록 / 공인인증서 등록)
 *    - 팝빌로그인 > [홈택스연동] > [환경설정] > [인증 관리] 메뉴
 *    - 홈택스연동 인증 관리 팝업 URL(GetCertificatePopUpURL API) 반환된 URL을 이용하여 홈택스 인증 처리를 합니다.
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

            // 연동환경 설정값, 개발용(true), 상업용(false)
            htTaxinvoiceService.IsTest = true;

            // 발급된 토큰에 대한 IP 제한기능 사용여부, 권장(True)
            htTaxinvoiceService.IPRestrictOnOff = true;

            // 팝빌 API 서비스 고정 IP 사용여부, true-사용, false-미사용, 기본값(false)
            htTaxinvoiceService.UseStaticIP = false;

            // 로컬PC 시간 사용 여부 true(사용), false(기본값) - 미사용
            htTaxinvoiceService.UseLocalTimeYN = false;
        }

        /*
         * 홈택스에 신고된 전자세금계산서 매입/매출 내역 수집을 팝빌에 요청합니다. (조회기간 단위 : 최대 3개월)
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#RequestJob
         */
        private void btnRequestJob_Click(object sender, EventArgs e)
        {
            // 전자(세금)계산서 유형 SELL-매출, BUY-매입, TRUSTEE-수탁
            KeyType tiKeyType = KeyType.SELL;

            // 일자유형, W-작성일자, I-발행일자, S-전송일자
            String DType = "S";

            // 시작일자, 표시형식(yyyyMMdd)
            String SDate = "20210701";

            // 종료일자, 표시형식(yyyyMMdd)
            String EDate = "20210730";

            try
            {
                String jobID = htTaxinvoiceService.RequestJob(txtCorpNum.Text, tiKeyType, DType, SDate, EDate);

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
         * 함수 RequestJob(수집 요청)를 통해 반환 받은 작업 아이디의 상태를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetJobState
         */
        private void btnGetJobState_Click(object sender, EventArgs e)
        {
            try
            {
                HTTaxinvoiceJobState jobState = htTaxinvoiceService.GetJobState(txtCorpNum.Text, txtJobID.Text);

                String tmp = "jobID (작업아이디) : " + jobState.jobID + CRLF;
                tmp += "jobState (수집상태) : " + jobState.jobState.ToString() + CRLF;
                tmp += "queryType (수집유형) : " + jobState.queryType + CRLF;
                tmp += "queryDateType (일자유형) : " + jobState.queryDateType + CRLF;
                tmp += "queryStDate (시작일자) : " + jobState.queryStDate + CRLF;
                tmp += "queryEnDate (종료일자) : " + jobState.queryEnDate + CRLF;
                tmp += "errorCode (오류코드) : " + jobState.errorCode.ToString() + CRLF;
                tmp += "errorReason (오류메시지) : " + jobState.errorReason + CRLF;
                tmp += "jobStartDT (작업 시작일시) : " + jobState.jobStartDT + CRLF;
                tmp += "jobEndDT (작업 종료일시) : " + jobState.jobEndDT + CRLF;
                tmp += "collectCount (수집개수) : " + jobState.collectCount.ToString() + CRLF;
                tmp += "regDT (수집유형) : " + jobState.regDT + CRLF;

                MessageBox.Show(tmp, "수집 상태 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 상태 확인");
            }
        }

        /*
         * 전자세금계산서 매입/매출 내역 수집요청에 대한 상태 목록을 확인합니다.
         * - 수집 요청 후 1시간이 경과한 수집 요청건은 상태정보가 반환되지 않습니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#ListActiveJob
         */
        private void btnListActiveJob_Click(object sender, EventArgs e)
        {
            try
            {
                List<HTTaxinvoiceJobState> jobList = htTaxinvoiceService.ListActiveJob(txtCorpNum.Text);

                String tmp = "jobID(작업아이디) | jobState(수집상태) | queryType(수집유형) | queryDateType(일자유형) | queryStDate(시작일자) |";
                tmp += "queryEnDate(종료일자) | errorCode(오류코드) | errorReason(오류메시지) | jobStartDT(작업 시작일시) | jobEndDT(작업 종료일시) |";
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 요청 목록확인");
            }
        }

        /*
         * 함수 GetJobState(수집 상태 확인)를 통해 상태 정보가 확인된 작업아이디를 활용하여 수집된 전자세금계산서 매입/매출 내역을 조회합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 문서형태 배열, N-일반 전자세금계산서, M-수정 전자세금계산서
            String[] Type = { "N", "M" };

            // 과세형태, T-과세, N-면세, Z-영세
            String[] TaxType = { "T", "N", "Z" };

            // 영수/청구, R-영수, C-청구, N-없음
            String[] PurposeType = { "R", "C", "N" };

            // 종사업장유무, 공백-전체조회 0-종사업장번호 없는 경우 조회, 1-종사업장번호 조건에 따라 조회
            String TaxRegIDYN = "";

            // 종사업장번호 사업자 유형, S-공급자, B-공급받는자, T-수탁자
            String TaxRegIDType = "S";

            // 종사업장번호, 콤마(",")로 구분하여 구성 ex) "0001,1234"
            String TaxRegID = "";

            // 페이지번호
            int Page = 1;

            // 페이지당 목록개수, 최대 1000건
            int PerPage = 10;

            // 정렬방향 D-내림차순, A-오름차순
            String Order = "D";

            // 조회 검색어, 거래처 사업자번호 또는 거래처명 like 검색
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집 결과 조회");
            }
        }

        /*
         * 함수 GetJobState(수집 상태 확인)를 통해 상태 정보가 확인된 작업아이디를 활용하여 수집된 전자세금계산서 매입/매출 내역의 요약 정보를 조회합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#Summary
         */
        private void btnSummary_Click(object sender, EventArgs e)
        {

            // 문서형태 배열, N-일반 전자세금계산서, M-수정 전자세금계산서
            String[] Type = { "N", "M" };

            // 과세형태, T-과세, N-면세, Z-영세
            String[] TaxType = { "T", "N", "Z" };

            // 영수/청구, R-영수, C-청구, N-없음
            String[] PurposeType = { "R", "C", "N" };

            // 종사업장유무, 공백-전체조회 0-종사업장번호 없는 경우 조회, 1-종사업장번호 조건에 따라 조회
            String TaxRegIDYN = "";

            // 종사업장번호 사업자 유형, S-공급자, B-공급받는자, T-수탁자
            String TaxRegIDType = "S";

            // 종사업장번호, 콤마(",")로 구분하여 구성 ex) "0001,1234"
            String TaxRegID = "";

            // 조회 검색어, 거래처 사업자번호 또는 거래처명 like 검색
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "수집결과 요약정보 조회");
            }
        }

        /*
         * 국세청 승인번호를 통해 수집한 전자세금계산서 1건의 상세정보를 반환합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetTaxinvoice
         */
        private void btnGetTaxinvocie_Click(object sender, EventArgs e)
        {
            try
            {
                HTTaxinvoice tiInfo = htTaxinvoiceService.GetTaxinvoice(txtCorpNum.Text, txtNTSconfirmNum.Text, txtUserId.Text);

                String tmp = "writeDate (작성일자) : " + tiInfo.writeDate + CRLF;
                tmp += "issueDT (발행일시) : " + tiInfo.issueDT + CRLF;
                tmp += "invoiceType (전자세금계산서 종류) : " + tiInfo.invoiceType.ToString() + CRLF;
                tmp += "taxTotal (과세형태) : " + tiInfo.taxTotal + CRLF;
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
                tmp += "invoicerTaxRegID (종사업장번호) : " + tiInfo.invoicerTaxRegID + CRLF;
                tmp += "invoicerCorpName (상호) : " + tiInfo.invoicerCorpName + CRLF;
                tmp += "invoicerCEOName (대표자 성명) : " + tiInfo.invoicerCEOName + CRLF;
                tmp += "invoicerAddr (주소) : " + tiInfo.invoicerAddr + CRLF;
                tmp += "invoicerBizType (업태) : " + tiInfo.invoicerBizType + CRLF;
                tmp += "invoicerBizClass (종목) : " + tiInfo.invoicerBizClass + CRLF;
                tmp += "invoicerContactName (담당자 성명) : " + tiInfo.invoicerContactName + CRLF;
                tmp += "invoicerTEL (담당자 연락처) : " + tiInfo.invoicerTEL + CRLF;
                tmp += "invoicerEmail (담당자 이메일) : " + tiInfo.invoicerEmail + CRLF + CRLF;

                tmp += "========공급받는자 정보========" + CRLF;
                tmp += "invoiceeCorpNum (사업자번호) : " + tiInfo.invoiceeCorpNum + CRLF;
                tmp += "invoiceeType (공급받는자 구분) : " + tiInfo.invoiceeType + CRLF;
                tmp += "invoiceeMgtKey (공급받는자 문서번호) : " + tiInfo.invoiceeMgtKey + CRLF;
                tmp += "invoiceeTaxRegID (종사업장번호) : " + tiInfo.invoiceeTaxRegID + CRLF;
                tmp += "invoiceeCorpName (상호) : " + tiInfo.invoiceeCorpName + CRLF;
                tmp += "invoiceeCEOName (대표자 성명) : " + tiInfo.invoiceeCEOName + CRLF;
                tmp += "invoiceeAddr (주소) : " + tiInfo.invoiceeAddr + CRLF;
                tmp += "invoiceeBizType (업태) : " + tiInfo.invoiceeBizType + CRLF;
                tmp += "invoiceeBizClass (종목) : " + tiInfo.invoiceeBizClass + CRLF;
                tmp += "invoiceeContactName1 (담당자 성명) : " + tiInfo.invoiceeContactName1 + CRLF;
                tmp += "invoiceeTEL1 (담당자 연락처) : " + tiInfo.invoiceeTEL1 + CRLF;
                tmp += "invoiceeEmail1 (담당자 이메일) : " + tiInfo.invoiceeEmail1 + CRLF + CRLF;

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

                tmp += CRLF + "=========수정 전자(세금)계산서 정보========" + CRLF;
                tmp += "modifyCode (수정 사유코드) : " + tiInfo.modifyCode.ToString() + CRLF;
                tmp += "orgNTSConfirmNum (원본 전자세금계산서국세청승인번호) : " + tiInfo.orgNTSConfirmNum + CRLF + CRLF;

                MessageBox.Show(tmp, "상세정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "상세정보 조회");
            }
        }

        /*
         * 국세청 승인번호를 통해 수집한 전자세금계산서 1건의 상세정보를 XML 형태의 문자열로 반환합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetXML
         */
        private void btnGetXML_Click(object sender, EventArgs e)
        {
            try
            {
                HTTaxinvoiceXML tiXML = htTaxinvoiceService.GetXML(txtCorpNum.Text, txtNTSconfirmNum.Text, txtUserId.Text);

                String tmp = "ResultCode (응답코드) : " + tiXML.ResultCode.ToString() + CRLF;
                tmp += "Message (전자세금계산서 국세청승인번호) : " + tiXML.Message + CRLF;
                tmp += "retObject (전자세금계산서 XML 문서) : " + tiXML.retObject + CRLF;

                MessageBox.Show(tmp, "상세정보 조회 - XML");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "상세정보 조회 - XML");
            }
        }

        /*
         * 수집된 전자세금계산서 1건의 상세내역을 확인하는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetPopUpURL
         */
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            // 조회할 전자세금계산서 국세청승인번호
            String NTSConfirmNum = txtNTSconfirmNum.Text;

            try
            {
                String url = htTaxinvoiceService.GetPopUpURL(txtCorpNum.Text, NTSConfirmNum);

                MessageBox.Show(url, "홈택스 전자세금계산서 보기 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "홈택스 전자세금계산서 보기 팝업 URL");
            }
        }

        /*
         * 홈택스연동 인증정보를 관리하는 페이지의 팝업 URL을 반환합니다.
         * - 인증방식에는 부서사용자/공인인증서 인증 방식이 있습니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetCertificatePopUpURL
         */
        private void btnGetCertificatePopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                String url = htTaxinvoiceService.GetCertificatePopUpURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "홈택스연동 인증관리 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "홈택스연동 인증관리 URL");
            }
        }

        /*
         * 홈택스연동 인증을 위해 팝빌에 등록된 인증서 만료일자를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetCertificateExpireDate
         */
        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = htTaxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show(expiration.ToString(), "공인인증서 만료일시 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "공인인증서 만료일시 확인");
            }
        }

        /*
         * 팝빌에 등록된 인증서로 홈택스 로그인 가능 여부를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#CheckCertValidation
         */
        private void btnCheckCertValidation_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckCertValidation(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "홈택스 공인인증서 로그인 테스트");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "홈택스 공인인증서 로그인 테스트");
            }
        }

        /*
         * 홈택스연동 인증을 위해 팝빌에 전자세금계산서용 부서사용자 계정을 등록합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#RegistDeptUser
         */
        private void btnRegistDeptUser_Click(object sender, EventArgs e)
        {
            // 홈택스에서 생성한 전자세금계산서 부서사용자 아이디
            String deptUserID = "userid_test";

            // 홈택스에서 생성한 전자세금계산서 부서사용자 비밀번호
            String deptUserPWD = "passwd_test";

            try
            {
                Response response = htTaxinvoiceService.RegistDeptUser(txtCorpNum.Text, deptUserID, deptUserPWD);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "부서사용자 계정등록");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "부서사용자 계정등록");
            }
        }

        /*
         * 홈택스연동 인증을 위해 팝빌에 등록된 전자세금계산서용 부서사용자 계정을 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#CheckDeptUser
         */
        private void btnCheckDeptUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckDeptUser(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "부서사용자 등록정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "부서사용자 등록정보 확인");
            }
        }

        /*
         * 팝빌에 등록된 전자세금계산서용 부서사용자 계정 정보로 홈택스 로그인 가능 여부를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#CheckLoginDeptUser
         */
        private void btnCheckLoginDeptUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckLoginDeptUser(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "부서사용자 로그인 테스트");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "부서사용자 로그인 테스트");
            }
        }

        /*
         * 팝빌에 등록된 홈택스 전자세금계산서용 부서사용자 계정을 삭제합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#DeleteDeptUser
         */
        private void btnDeleteDeptUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.DeleteDeptUser(txtCorpNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "부서사용자 등록정보 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "부서사용자 등록정보 삭제");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");

            }
        }

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetChargeURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetPaymentURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 결제내역 URL");
            }
        }

        /*
         * 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetUseHistoryURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 사용내역 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetPartnerBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetPartnerURL
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htTaxinvoiceService.GetPartnerURL(txtCorpNum.Text, "CHRG");

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
         * 팝빌 홈택스연동(세금) API 서비스 과금정보를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetChargeInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "과금정보 확인");
            }
        }

        /*
         * 홈택스연동 정액제 서비스 신청 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetFlatRatePopUpURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 신청 팝업 URL");
            }
        }

        /*
         * 홈택스연동 정액제 서비스 상태를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetFlatRateState
         */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HTFlatRate rateInfo = htTaxinvoiceService.GetFlatRateState(txtCorpNum.Text, txtUserId.Text);

                String tmp = "referenceID(사업자번호) : " + rateInfo.referenceID + CRLF;
                tmp += "contractDT(정액제 서비스 시작일시) : " + rateInfo.contractDT + CRLF;
                tmp += "useEndDate(정액제 서비스 종료일) : " + rateInfo.useEndDate + CRLF;
                tmp += "baseDate(자동연장 결제일) : " + rateInfo.baseDate.ToString() + CRLF;
                tmp += "state(정액제 서비스 상태) : " + rateInfo.state.ToString() + CRLF;
                tmp += "closeRequestYN(정액제 서비스 해지신청 여부) : " + rateInfo.closeRequestYN.ToString() + CRLF;
                tmp += "useRestrictYN(정액제 서비스 사용제한 여부) : " + rateInfo.useRestrictYN.ToString() + CRLF;
                tmp += "closeOnExpired(정액제 서비스 만료 시 해지 여부) : " + rateInfo.closeOnExpired.ToString() + CRLF;
                tmp += "unPaidYN(미수금 보유 여부) : " + rateInfo.unPaidYN.ToString() + CRLF;

                MessageBox.Show(tmp, "정액제 서비스 상태 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "정액제 서비스 상태 확인");
            }
        }

        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = htTaxinvoiceService.CheckID(txtUserId.Text);

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
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#JoinMember
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
            joinInfo.ContactEmail = "test@test.com";

            // 담당자 연락처 (최대 20자)
            joinInfo.ContactTEL = "070-4304-2991";

            // 담당자 휴대폰번호 (최대 20자)
            joinInfo.ContactHP = "010-111-222";

            // 담당자 팩스번호 (최대 20자)
            joinInfo.ContactFAX = "02-6442-9700";

            try
            {
                Response response = htTaxinvoiceService.JoinMember(joinInfo);

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
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetAccessURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL 확인");
            }
        }

        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetCorpInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회사정보 조회");
            }
        }

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#UpdateCorpInfo
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
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#RegistContact
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
            contactInfo.tel = "070-4304-2991";

            //담당자 휴대폰번호 (최대 20자)
            contactInfo.hp = "010-111-222";

            //담당자 팩스번호 (최대 20자)
            contactInfo.fax = "070-4304-2991";

            //담당자 이메일 (최대 100자)
            contactInfo.email = "dev@linkhub.co.kr";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = htTaxinvoiceService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = htTaxinvoiceService.GetContactInfo(txtCorpNum.Text, contactID, txtUserId.Text);

                String tmp = null;

                tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                tmp += "personName (담당자명) : " + contactInfo.personName + CRLF;
                tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                tmp += "hp (휴대폰번호) : " + contactInfo.hp + CRLF;
                tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole + CRLF;
                tmp += "tel (연락처) : " + contactInfo.tel + CRLF;
                tmp += "fax (팩스번호) : " + contactInfo.fax + CRLF;
                tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN + CRLF;
                tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
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
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#ListContact
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
                    tmp += "personName (담당자명) : " + contactInfo.personName + CRLF;
                    tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                    tmp += "hp (휴대폰번호) : " + contactInfo.hp + CRLF;
                    tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole + CRLF;
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
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#UpdateContact
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

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = htTaxinvoiceService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * 수집된 전자세금계산서 1건의 상세내역을 인쇄하는 페이지의 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/httaxinvoice/dotnet/api#GetPrintURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "홈택스 전자세금계산서 인쇄 팝업 URL");
            }
        }
    }
}
