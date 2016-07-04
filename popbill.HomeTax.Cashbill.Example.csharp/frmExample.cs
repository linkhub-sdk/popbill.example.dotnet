using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.HomeTax.Cashbill.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";
        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private HTCashbillService htCashbillService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            //초기화
            htCashbillService = new HTCashbillService(LinkID, SecretKey);
            //테스트를 완료한후 아래 변수를 false로 변경하거나, 아래줄을 삭제하여 실제 서비스 연결.
            htCashbillService.IsTest = true;
        }


        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckIsMember(조회할 사업자번호, 링크아이디)
                Response response = htCashbillService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "연동회원 가입여부 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "연동회원 가입여부 확인");

            }
        }

        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = htCashbillService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 조회");
            }
        }

        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            contactInfo.id = "test12341234";        // 담당자 아이디, 한글, 영문(대/소), 숫자, '-', '_' 6자 이상 20자 미만 구성
            contactInfo.pwd = "12345";              // 비밀번호, 6자 이상 20자 미만 구성
            contactInfo.personName = "담당자 명";   // 담당자명 
            contactInfo.tel = "070-7510-3710";      // 연락처
            contactInfo.hp = "010-1234-1234";       // 휴대폰번호
            contactInfo.fax = "070-7510-3710";      // 팩스번호 
            contactInfo.email = "code@linkhub.co.kr";   // 이메일주소
            contactInfo.searchAllAllowYN = false;   // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.mgrYN = false;              // 관리자 권한여부 

            try
            {
                Response response = htCashbillService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "담당자 추가");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "담당자 추가");
            }
        }

        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckID(조회할 회원아이디)
                Response response = htCashbillService.CheckID(txtUserId.Text);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "ID 중복확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "ID 중복확인");

            }
        }

        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            joinInfo.LinkID = LinkID;                 //링크아이디
            joinInfo.CorpNum = "1231212312";          //사업자번호 "-" 제외
            joinInfo.CEOName = "대표자성명";
            joinInfo.CorpName = "상호";
            joinInfo.Addr = "주소";
            joinInfo.BizType = "업태";
            joinInfo.BizClass = "업종";
            joinInfo.ID = "userid";                   //6자 이상 20자 미만
            joinInfo.PWD = "pwd_must_be_long_enough"; //6자 이상 20자 미만
            joinInfo.ContactName = "담당자명";
            joinInfo.ContactTEL = "02-999-9999";
            joinInfo.ContactHP = "010-1234-5678";
            joinInfo.ContactFAX = "02-999-9998";
            joinInfo.ContactEmail = "test@test.com";

            try
            {
                Response response = htCashbillService.JoinMember(joinInfo);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "연동회원 가입요청");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "연동회원 가입요청");
            }
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = htCashbillService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : " + remainPoint.ToString(), "잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "잔여포인트 확인");

            }
        }

        private void btnGetPartnerBalance1_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = htCashbillService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " + remainPoint.ToString(), "파트너 잔여포인트 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "파트너 잔여포인트 확인");

            }
        }

        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = htCashbillService.GetChargeInfo(txtCorpNum.Text);

                string tmp = null;
                tmp += "unitCost (단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod (과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem (과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, "과금정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "과금정보 확인");
            }
        }

        private void getPopbillURL_LOGIN_Click(object sender, EventArgs e)
        {

            try
            {
                string url = htCashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

                MessageBox.Show(url, "팝빌 로그인 URL 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "팝빌 로그인 URL 확인");

            }
        }

        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = htCashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url, "포인트 충전 URL 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message);

            }
        }

        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = htCashbillService.ListContact(txtCorpNum.Text, txtUserId.Text);

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
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "담당자 목록조회");
            }
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            contactInfo.personName = "담당자123";      // 담당자명 
            contactInfo.tel = "070-7510-3710";      // 연락처
            contactInfo.hp = "010-1234-1234";       // 휴대폰번호
            contactInfo.fax = "070-7510-3710";      // 팩스번호 
            contactInfo.email = "code@linkhub.co.kr";   // 이메일주소
            contactInfo.searchAllAllowYN = true;    // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.mgrYN = false;              // 관리자 권한여부 

            try
            {
                Response response = htCashbillService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "담당자 정보 수정");
            }
        }

        private void btnUpdateCorpInfo_Click(object sender, EventArgs e)
        {
            CorpInfo corpInfo = new CorpInfo();

            corpInfo.ceoname = "대표자명 테스트";
            corpInfo.corpName = "업체명";
            corpInfo.addr = "주소정보 수정";
            corpInfo.bizType = "업태정보 수정";
            corpInfo.bizClass = "업종정보 수정";

            try
            {
                Response response = htCashbillService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

                MessageBox.Show("[ " + response.code.ToString() + " ] " + response.message, "회사정보 수정");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 수정");
            }
        }

        private void btnRequestJob_Click(object sender, EventArgs e)
        {
            // 현금영수증 유형 SELL-매출, BUY-매입
            KeyType tiKeyType = KeyType.SELL;

            // 시작일자, 표시형식(yyyyMMdd)
            String SDate = "20160601";

            // 종료일자, 표시형식(yyyyMMdd)
            String EDate = "20160731";

            try
            {
                // 반환되는 작업아이디(jobID)의 유효시간은 1시간입니다.
                String jobID = htCashbillService.RequestJob(txtCorpNum.Text, tiKeyType, SDate, EDate);

                MessageBox.Show("작업아이디(jobID) : " + jobID, "수집 요청");

                txtJobID.Text = jobID;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "수집 요청");
            }            
        }

        private void btnGetJobState_Click(object sender, EventArgs e)
        {
            try
            {
                HTCashbillJobState jobState = htCashbillService.GetJobState(txtCorpNum.Text, txtJobID.Text);

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
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "수집 상태 확인");
            }            
        }

        private void btnListActiveJob_Click(object sender, EventArgs e)
        {
            try
            {
                List<HTCashbillJobState> jobList = htCashbillService.ListActiveJob(txtCorpNum.Text);

                String tmp = "jobID | jobState | queryType | queryDateType | queryStDate | queryEnDate | errorCode | ";
                tmp += "errorReason | jobStartDT | jobEndDT | collectCount | regDT ";

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


                MessageBox.Show(tmp, "수집 목록 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "수집 목록 확인");
            } 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 현금영수증 형태 배열, N-일반 현금영수증, C-취소 현금영수증
            String[] TradeType = { "N", "C" };

            // 거래용도 배열, P-소득공제용, C-지출증빙용
            String[] TradeUsage = { "P", "C"};

            // 페이지번호
            int Page = 1;

            // 페이지당 목록개수, 최대 1000건
            int PerPage = 10;

            // 정렬방향 D-내림차순, A-오름차순
            String Order = "D";

            listBox1.Items.Clear();

            try
            {
                HTCashbillSearch searchInfo = htCashbillService.Search(txtCorpNum.Text, txtJobID.Text, TradeType, TradeUsage, Page, PerPage, Order, txtUserId.Text);

                String tmp = "code (응답코드) : " + searchInfo.code.ToString() + CRLF;
                tmp += "message (응답메시지) : " + searchInfo.message + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchInfo.total.ToString() + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchInfo.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchInfo.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchInfo.pageCount + CRLF;

                MessageBox.Show(tmp, "수집 결과 조회");

                string rowStr = "거래유형 | 거래일시 | 거래처 식별번호 | 공급가액 | 세액 | 봉사료 | 거래금액 | 현금영수증 형태 | 국세청승인번호 ";
                listBox1.Items.Add(rowStr);

                // 현금영수증 항목에 대한 추가적인 정보는 [연동매뉴얼 4.1. 응답전문 구성] 를 참조하시기 바랍니다.
                for (int i = 0; i < searchInfo.list.Count; i++)
                {
                    rowStr = null;
                    rowStr += searchInfo.list[i].tradeUsage  + " | ";
                    rowStr += searchInfo.list[i].tradeDT + " | ";
                    rowStr += searchInfo.list[i].identityNum + " | ";
                    rowStr += searchInfo.list[i].supplyCost + " | ";
                    rowStr += searchInfo.list[i].tax + " | ";
                    rowStr += searchInfo.list[i].serviceFee + " | ";
                    rowStr += searchInfo.list[i].totalAmount + " | ";
                    rowStr += searchInfo.list[i].tradeType + " | ";
                    rowStr += searchInfo.list[i].ntsconfirmNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "수집 결과 조회");
            }     
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            // 현금영수증 형태 배열, N-일반 현금영수증, C-취소 현금영수증
            String[] TradeType = { "N", "C" };

            // 거래용도 배열, P-소득공제용, C-지출증빙용
            String[] TradeUsage = { "P", "C" };

            try
            {
                HTCashbillSummary summaryInfo = htCashbillService.Summary(txtCorpNum.Text, txtJobID.Text, TradeType, TradeUsage);

                String tmp = "count (수집 결과 건수) : " + summaryInfo.count.ToString() + CRLF;
                tmp += "supplyCostTotal (공급가액 합계) : " + summaryInfo.supplyCostTotal.ToString() + CRLF;
                tmp += "taxTotal (세액 합계) : " + summaryInfo.taxTotal.ToString() + CRLF;
                tmp += "serviceFeeTotal (봉사료 합계) : " + summaryInfo.serviceFeeTotal.ToString() + CRLF;
                tmp += "amountTotal (합계 금액) : " + summaryInfo.amountTotal.ToString() + CRLF;
                

                MessageBox.Show(tmp, "수집 결과 요약정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "수집 결과 요약정보 조회");
            }
        }

        private void btnGetFlatRatePopUpURL_Click(object sender, EventArgs e)
        {
            String url = htCashbillService.GetFlatRatePopUpURL(txtCorpNum.Text, txtUserId.Text);

            MessageBox.Show(url, "정액제 서비스 신청 URL");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HTFlatRate rateInfo = htCashbillService.GetFlatRateState(txtCorpNum.Text, txtUserId.Text);

                String tmp = "사업자번호 (referenceID) : " + rateInfo.referenceID + CRLF;
                tmp += "정액제 서비스 시작일시 (contractDT) : " + rateInfo.contractDT + CRLF;
                tmp += "정액제 서비스 종료일 (useEndDate) : " + rateInfo.useEndDate + CRLF;
                tmp += "자동연장 결제일 (baseDate) : " + rateInfo.baseDate.ToString() + CRLF;
                tmp += "정액제 서비스 상태 (state) : " + rateInfo.state.ToString() + CRLF;
                tmp += "정액제 서비스 해지신청 여부 (closeRequestYN) : " + rateInfo.closeRequestYN.ToString() + CRLF;
                tmp += "정액제 서비스 사용제한 여부 (useRestrictYN) : " + rateInfo.useRestrictYN.ToString() + CRLF;
                tmp += "정액제 서비스 만료 시 해지 여부 (closeOnExpired) : " + rateInfo.closeOnExpired.ToString() + CRLF;
                tmp += "미수금 보유 여부 (unPaidYN) : " + rateInfo.unPaidYN.ToString() + CRLF;

                MessageBox.Show(tmp, "정액제 서비스 상태 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "정액제 서비스 상태 확인");

            }
        }

        private void btnGetCertificatePopUpURL_Click(object sender, EventArgs e)
        {
            String url = htCashbillService.GetCertificatePopUpURL(txtCorpNum.Text, txtUserId.Text);

            MessageBox.Show(url, "공인인증서 등록 URL");
        }

        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = htCashbillService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show(expiration.ToString(), "공인인증서 만료일시 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "공인인증서 만료일시 확인");

            }
        }

    }
}
