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
            //현금영수증 몯류 초기화
            cashbillService = new CashbillService(LinkID, SecretKey);

            //연동환경 설정값, 테스트용(true), 상업옹(false)
            cashbillService.IsTest = true;
        }

        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            
            try
            {
                string url = cashbillService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text);
                
                MessageBox.Show(url);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

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
            joinInfo.ZipCode = "500-100";
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
                Response response = cashbillService.JoinMember(joinInfo);

                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {

            try
            {
                double remainPoint = cashbillService.GetBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = cashbillService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckIsMember(조회할 사업자번호, 링크아이디)
                Response response = cashbillService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show(response.code.ToString() + " | " + response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = cashbillService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
        
            try
            {
                bool InUse = cashbillService.CheckMgtKeyInUse(txtCorpNum.Text, txtMgtKey.Text);

                MessageBox.Show((InUse ? "사용중" : "미사용중"));

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Cashbill cashbill = new Cashbill();
            
            cashbill.mgtKey = txtMgtKey.Text;        //문서관리번호, 발행자별 고유번호 할당, 1~24자리 영문,숫자,'-','_' 조합으로 중복없이 구성.
            cashbill.tradeType = "승인거래";         //거래유형 {승인거래, 취소거래} 중 기재
            cashbill.franchiseCorpNum = txtCorpNum.Text;    //발행자 사업자번호
            cashbill.franchiseCorpName = "발행자 상호";
            cashbill.franchiseCEOName = "발행자 대표자";
            cashbill.franchiseAddr = "발행자 주소";
            cashbill.franchiseTEL = "070-1234-1234";

            cashbill.tradeUsage = "소득공제용";      //현금영수증 형태 , {소득공제용, 지출증빙용}중 기재
            cashbill.identityNum = "01041680206";    //거래처식별번호, 
            cashbill.customerName = "고객명";
            cashbill.itemName = "상품명";
            cashbill.orderNumber = "주문번호";
            cashbill.email = "test@test.com";
            cashbill.hp = "111-1234-1234";
            cashbill.fax = "777-444-3333";
            cashbill.serviceFee = "0";              //봉사료
            cashbill.supplyCost = "10000";          //공급가액
            cashbill.tax = "1000";                  //세액
            cashbill.totalAmount = "11000";         //거래금액(봉사료+공급가액+세액)
            
            cashbill.taxationType = "과세";         //과세형태, {과세, 비과세}

            cashbill.smssendYN =  false;            //발행시 문자전송여부 
          
            try
            {
                //Register(팝빌회원 사업자번호, 현금영수증 객체, 팝빌회원 아이디)
                Response response = cashbillService.Register(txtCorpNum.Text, cashbill, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
         
            try
            {
                //Delete(팝빌회원 사업자번호, 문서관리번호, 팝빌회원 아이디)
                Response response = cashbillService.Delete(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {

            try
            {
                //GetDetailInfo(팝빌회원 사업자번호, 문서관리번호)
                Cashbill cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = null;

                tmp += "mgtKey : " + cashbill.mgtKey + CRLF;
                tmp += "tradeType : " + cashbill.tradeType + CRLF;
                tmp += "tradeUsage : " + cashbill.tradeUsage + CRLF;
                tmp += "taxationType: " + cashbill.taxationType + CRLF;
                tmp += "tradeDate : " + cashbill.tradeDate + CRLF;
                tmp += "supplyCost : " + cashbill.supplyCost + CRLF;
                tmp += "tax : " + cashbill.tax + CRLF;
                tmp += "serviceFee : " + cashbill.serviceFee + CRLF;
                tmp += "totalAmount : " + cashbill.totalAmount + CRLF;
                tmp += "franchiseCorpNum : " + cashbill.franchiseCorpNum + CRLF;
                tmp += "franchiseCorpName : " + cashbill.franchiseCorpName + CRLF;
                tmp += "franchiseCEOName : " + cashbill.franchiseCEOName + CRLF;
                tmp += "franchiseAddr : " + cashbill.franchiseAddr + CRLF;
                tmp += "franchiseTEL : " + cashbill.franchiseTEL + CRLF;
                tmp += "identityNum : " + cashbill.identityNum + CRLF;
                tmp += "customerName: " + cashbill.customerName + CRLF;
                tmp += "itemName : " + cashbill.itemName + CRLF;
                tmp += "orderNumber : " + cashbill.orderNumber + CRLF;
                tmp += "hp : " + cashbill.hp + CRLF;
                tmp += "fax : " + cashbill.fax + CRLF;
                tmp += "confirmNum : " + cashbill.confirmNum + CRLF;
                tmp += "orgConfirmNum : " + cashbill.orgConfirmNum + CRLF;
                tmp += "smssendYN : " + cashbill.smssendYN + CRLF;
                tmp += "faxsendYN : " + cashbill.faxsendYN + CRLF;
                
                MessageBox.Show(tmp);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
         
            try
            {
                CashbillInfo cashbillInfo = cashbillService.GetInfo(txtCorpNum.Text, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemKey : " + cashbillInfo.itemKey + CRLF;
                tmp += "mgtKey : " + cashbillInfo.mgtKey + CRLF;
                tmp += "tradeDate : " + cashbillInfo.tradeDate + CRLF;
                tmp += "issueDT : " + cashbillInfo.issueDT + CRLF;
                tmp += "customerName : " + cashbillInfo.customerName + CRLF;
                tmp += "itemName : " + cashbillInfo.itemName + CRLF;
                tmp += "identityNum : " + cashbillInfo.identityNum + CRLF;
                tmp += "taxationType : " + cashbillInfo.taxationType + CRLF;

                tmp += "totalAmount : " + cashbillInfo.totalAmount + CRLF;
                tmp += "tradeUsage : " + cashbillInfo.tradeUsage + CRLF;
                tmp += "tradeType : " + cashbillInfo.tradeType + CRLF;
                tmp += "stateCode : " + cashbillInfo.stateCode + CRLF;
                tmp += "stateDT : " + cashbillInfo.stateDT + CRLF;
                tmp += "printYN : " + cashbillInfo.printYN + CRLF;

                tmp += "confirmNum : " + cashbillInfo.confirmNum + CRLF;
                tmp += "orgTradeDate : " + cashbillInfo.orgTradeDate + CRLF;
                tmp += "orgConfirmNum : " + cashbillInfo.orgConfirmNum + CRLF;

                tmp += "ntssendDT : " + cashbillInfo.ntssendDT + CRLF;
                tmp += "ntsresult : " + cashbillInfo.ntsresult + CRLF;
                tmp += "ntsresultDT : " + cashbillInfo.ntsresultDT + CRLF;
                tmp += "ntsresultCode : " + cashbillInfo.ntsresultCode + CRLF;
                tmp += "ntsresultMessage : " + cashbillInfo.ntsresultMessage + CRLF;

                tmp += "regDT : " + cashbillInfo.regDT;

                MessageBox.Show(tmp);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetURL_WRITE_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

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
                
                MessageBox.Show(tmp);



            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetInfos_Click(object sender, EventArgs e)
        {
        
            List<string> MgtKeyList = new List<string>();

            //'최대 1000건.
            MgtKeyList.Add("20150626-30");
            MgtKeyList.Add("20150626-31");

            try
            {
                List<CashbillInfo> cashbillInfoList = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList);

                //'TOGO Describe it.

                string tmp = null;

                for (int i = 0; i < cashbillInfoList.Count; i++)
                {
                    tmp += "itemKey : " + cashbillInfoList[i].itemKey + CRLF;
                    tmp += "mgtKey : " + cashbillInfoList[i].mgtKey + CRLF;
                    tmp += "tradeDate : " + cashbillInfoList[i].tradeDate + CRLF;
                    tmp += "issueDT : " + cashbillInfoList[i].issueDT + CRLF;
                    tmp += "customerName : " + cashbillInfoList[i].customerName + CRLF;
                    tmp += "itemName : " + cashbillInfoList[i].itemName + CRLF;
                    tmp += "identityNum : " + cashbillInfoList[i].identityNum + CRLF;
                    tmp += "taxationType : " + cashbillInfoList[i].taxationType + CRLF;

                    tmp += "totalAmount : " + cashbillInfoList[i].totalAmount + CRLF;
                    tmp += "tradeUsage : " + cashbillInfoList[i].tradeUsage + CRLF;
                    tmp += "tradeType : " + cashbillInfoList[i].tradeType + CRLF;
                    tmp += "stateCode : " + cashbillInfoList[i].stateCode + CRLF;
                    tmp += "stateDT : " + cashbillInfoList[i].stateDT + CRLF;
                    tmp += "printYN : " + cashbillInfoList[i].printYN + CRLF;

                    tmp += "confirmNum : " + cashbillInfoList[i].confirmNum + CRLF;
                    tmp += "orgConfirmNum : " + cashbillInfoList[i].orgConfirmNum + CRLF;

                    tmp += "ntssendDT : " + cashbillInfoList[i].ntssendDT + CRLF;
                    tmp += "ntsresult : " + cashbillInfoList[i].ntsresult + CRLF;
                    tmp += "ntsresultDT : " + cashbillInfoList[i].ntsresultDT + CRLF;
                    tmp += "ntsresultCode : " + cashbillInfoList[i].ntsresultCode + CRLF;
                    tmp += "ntsresultMessage : " + cashbillInfoList[i].ntsresultMessage + CRLF+CRLF;
                }

                MessageBox.Show(tmp);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }

        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
          
            try
            {
                //SendEmail(팝빌회원 사업자번호, 문서관리번호, 수신메일주소, 팝빌회원 아이디)
                Response response = cashbillService.SendEmail(txtCorpNum.Text, txtMgtKey.Text, "test@test.com", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {

            try
            {
                //SendSMS(팝빌회원 사업자번호, 문서관리번호, 발신번호, 수신번호, 문자내용, 팝빌회원 아이디)
                //문자내용의 길이가 90Byte를 초과하는 경우 길이가 조정되어 전송됨
                Response response = cashbillService.SendSMS(txtCorpNum.Text, txtMgtKey.Text, "1111-2222", "111-2222-4444", "발신문자 내용...", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            try
            {
                //SendFAX(팝빌회원 사업자번호, 문서관리번호, 발신번호, 수신번호, 팝빌회원 아이디)
                Response response = cashbillService.SendFAX(txtCorpNum.Text, txtMgtKey.Text, "1111-2222", "000-2222-4444", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            try
            {
                //GetPopUpURL(팝빌회원 사업자번호, 문서관리번호, 팝빌회원 아이디)
                string url = cashbillService.GetPopUpURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            try
            {
                //GetPrintURL(팝빌회원 사업자번호, 문서관리번호, 팝빌회원 아이디)
                string url = cashbillService.GetPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }

        }

        private void btnEPrintURL_Click(object sender, EventArgs e)
        {
          
            try
            {
                //GetEPrintURL(팝빌회원 사업자번호, 문서관리번호, 팝빌회원 아이디)
                string url = cashbillService.GetEPrintURL(txtCorpNum.Text, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetEmailURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = cashbillService.GetMailURL(txtCorpNum.Text,  txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
          
            List<string> MgtKeyList = new List<string>();

            //문서관리번호 배열, 최대 1000건.
            MgtKeyList.Add("1234");
            MgtKeyList.Add("12345");

            try
            {
                //GetMassPrintURL(팝빌회원 사업자번호, 문서관리번호 배열, 팝빌회원 아이디)
                string url = cashbillService.GetMassPrintURL(txtCorpNum.Text, MgtKeyList, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

    
        private void btnIssue_Click(object sender, EventArgs e)
        {
          
            try
            {
                //Issue(팝빌회원 사업자번호, 문서관리번호, 메모, 팝빌회원 아이디)
                Response response = cashbillService.Issue(txtCorpNum.Text,  txtMgtKey.Text, "발행시 메모",  txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCancelIssue_Click(object sender, EventArgs e)
        {
          
            try
            {
                //CancelIssue(팝빌회원 사업자번호, 문서관리번호, 메모, 팝빌회원 아이디)
                Response response = cashbillService.CancelIssue(txtCorpNum.Text, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {

            Cashbill cashbill = new Cashbill();

            cashbill.mgtKey = txtMgtKey.Text;        //문서관리번호, 발행자별 고유번호 할당, 1~24자리 영문,숫자,'-','_' 조합으로 중복없이 구성.
            cashbill.tradeType = "승인거래";         //거래유형 {승인거래, 취소거래} 중 기재
            cashbill.franchiseCorpNum = txtCorpNum.Text;    //발행자 사업자번호
            cashbill.franchiseCorpName = "발행자 상호_수정";
            cashbill.franchiseCEOName = "발행자 대표자_수정";
            cashbill.franchiseAddr = "발행자 주소";
            cashbill.franchiseTEL = "070-1234-1234";

            cashbill.tradeUsage = "소득공제용";      //현금영수증 형태 , {소득공제용, 지출증빙용}중 기재
            cashbill.identityNum = "01041680206";    //거래처식별번호, 
            cashbill.customerName = "고객명";
            cashbill.itemName = "상품명";
            cashbill.orderNumber = "주문번호";
            cashbill.email = "test@test.com";
            cashbill.hp = "111-1234-1234";
            cashbill.fax = "777-444-3333";
            cashbill.serviceFee = "0";              //봉사료
            cashbill.supplyCost = "10000";          //공급가액
            cashbill.tax = "1000";                  //세액
            cashbill.totalAmount = "11000";         //거래금액(봉사료+공급가액+세액)

            cashbill.taxationType = "과세";         //과세형태, {과세, 비과세}

            cashbill.smssendYN = false;            //발행시 문자전송여부 


            try
            {
                //Update(팝빌회원 사업자번호, 문서관리번호, 현금영수증객체, 팝빌회원 아이디)
                Response response = cashbillService.Update(txtCorpNum.Text, txtMgtKey.Text, cashbill, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

    }
}
