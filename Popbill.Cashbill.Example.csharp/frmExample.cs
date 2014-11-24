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
        private string SecretKey = "Oafp98tjXpqjzPZRBL9lB1RsXR9zodOxCoPue7PfsQc=";

        private CashbillService cashbillService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();
            //초기화
            cashbillService = new CashbillService(LinkID, SecretKey);
            //테스트를 완료한후 아래 변수를 false로 변경하거나, 아래줄을 삭제하여 실제 서비스 연결.
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

            joinInfo.LinkID = LinkID;
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

                           
            cashbill.mgtKey = txtMgtKey.Text;        //발행자별 고유번호 할당, 1~24자리 영문,숫자조합으로 중복없이 구성.
            cashbill.tradeType = "승인거래";         //승인거래 or 취소거래
            cashbill.franchiseCorpNum = txtCorpNum.Text;
            cashbill.franchiseCorpName = "발행자 상호";
            cashbill.franchiseCEOName = "발행자 대표자";
            cashbill.franchiseAddr = "발행자 주소";
            cashbill.franchiseTEL = "070-1234-1234";
            cashbill.identityNum = "01041680206";
            cashbill.customerName = "고객명";
            cashbill.itemName = "상품명";
            cashbill.orderNumber = "주문번호";
            cashbill.email = "test@test.com";
            cashbill.hp = "111-1234-1234";
            cashbill.fax = "777-444-3333";
            cashbill.serviceFee = "0";
            cashbill.supplyCost = "10000";
            cashbill.tax = "1000";
            cashbill.totalAmount = "11000";
            cashbill.tradeUsage = "소득공제용";      //소득공제용 or 지출증빙용
            cashbill.taxationType = "과세";          //과세 or 비과세

            cashbill.smssendYN =  false;
          
            try
            {
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
                Response response = cashbillService.Delete(txtCorpNum.Text,  txtMgtKey.Text, txtUserId.Text);

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
                Cashbill cashbill = cashbillService.GetDetailInfo(txtCorpNum.Text,  txtMgtKey.Text);

                //자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

                string tmp = null;

                tmp += "franchiseCorpNum : " + cashbill.franchiseCorpNum + CRLF;
                tmp += "franchiseCorpName : " + cashbill.franchiseCorpName + CRLF;
                tmp += "identityNum : " + cashbill.identityNum + CRLF;
                tmp += "customerName : " + cashbill.customerName + CRLF;

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
            MgtKeyList.Add("1234");
            MgtKeyList.Add("12345");

            try
            {
                List<CashbillInfo> cashbillInfoList = cashbillService.GetInfos(txtCorpNum.Text, MgtKeyList);

                //'TOGO Describe it.

                MessageBox.Show(cashbillInfoList.Count.ToString());


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
                string url = cashbillService.GetEPrintURL(txtCorpNum.Text,  txtMgtKey.Text, txtUserId.Text);

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
                string url = cashbillService.GetEPrintURL(txtCorpNum.Text,  txtMgtKey.Text, txtUserId.Text);

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

            //'최대 1000건.
            MgtKeyList.Add("1234");
            MgtKeyList.Add("12345");

            try
            {
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

            cashbill.mgtKey = txtMgtKey.Text;        //발행자별 고유번호 할당, 1~24자리 영문,숫자조합으로 중복없이 구성.
            cashbill.tradeType = "승인거래";         //승인거래 or 취소거래
            cashbill.franchiseCorpNum = txtCorpNum.Text;
            cashbill.franchiseCorpName = "발행자 상호_수정";
            cashbill.franchiseCEOName = "발행자 대표자";
            cashbill.franchiseAddr = "발행자 주소";
            cashbill.franchiseTEL = "070-1234-1234";
            cashbill.identityNum = "01041680206";
            cashbill.customerName = "고객명";
            cashbill.itemName = "상품명";
            cashbill.orderNumber = "주문번호";
            cashbill.email = "test@test.com";
            cashbill.hp = "111-1234-1234";
            cashbill.fax = "777-444-3333";
            cashbill.serviceFee = "0";
            cashbill.supplyCost = "10000";
            cashbill.tax = "1000";
            cashbill.totalAmount = "11000";
            cashbill.tradeUsage = "소득공제용";      //소득공제용 or 지출증빙용
            cashbill.taxationType = "과세";          //과세 or 비과세

            cashbill.smssendYN = false;


            try
            {
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
