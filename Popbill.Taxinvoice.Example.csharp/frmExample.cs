using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Popbill.Taxinvoice.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";
        //비밀키
        private string SecretKey = "2eQzzRuuygccucjyhFLMGkQEz7MzmDB9wA++aZROr1I=";

        private TaxinvoiceService taxinvoiceService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();
            //초기화
            taxinvoiceService = new TaxinvoiceService(LinkID, SecretKey);
            //테스트를 완료한후 아래 변수를 false로 변경하거나, 아래줄을 삭제하여 실제 서비스 연결.
            taxinvoiceService.IsTest = true;
        }

        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text);

            MessageBox.Show(url);
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
            joinInfo.Bizclass = "업종";
            joinInfo.ID = "userid";                   //6자 이상 20자 미만
            joinInfo.PWD = "pwd_must_be_long_enough"; //6자 이상 20자 미만
            joinInfo.ContactName = "담당자명";
            joinInfo.ContactTEL = "02-999-9999";
            joinInfo.ContactHP = "010-1234-5678";
            joinInfo.ContactFAX = "02-999-9998";
            joinInfo.ContactEmail = "test@test.com";

            try
            {
                Response response = taxinvoiceService.JoinMember(joinInfo);

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
                double remainPoint = taxinvoiceService.GetBalance(txtCorpNum.Text);

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
                double remainPoint = taxinvoiceService.GetPartnerBalance(txtCorpNum.Text);

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
                Response response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID);

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
                float unitCost = taxinvoiceService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show(expiration.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType) Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                bool InUse = taxinvoiceService.CheckMgtKeyInUse(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                MessageBox.Show((InUse ? "사용중" : "미사용중"));

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetEmailPublicKey_Click(object sender, EventArgs e)
        {
            try
            {
                List<EmailPublicKey> KeyList = taxinvoiceService.GetEmailPublicKeys(txtCorpNum.Text);

                MessageBox.Show(KeyList.Count.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Taxinvoice taxinvoice = new Taxinvoice();

            taxinvoice.writeDate = "20140923";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "정발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}


            taxinvoice.invoicerCorpNum = "1231212312";
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호";
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;             //문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = true;                    //발행시 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";
            taxinvoice.invoiceeCorpNum = "8888888888";
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = "";
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";

            taxinvoice.supplyCostTotal = "100000";                  //필수 공급가액 합계"
            taxinvoice.taxTotal = "10000";                          //필수 세액 합계
            taxinvoice.totalAmount = "110000";                      //필수 합계금액.  공급가액 + 세액

            taxinvoice.modifyCode = null;                           //수정세금계산서 작성시 1~6까지 선택기재.
            taxinvoice.originalTaxinvoiceKey = "";                  //수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
            taxinvoice.serialNum = "123";
            taxinvoice.cash = "";                                   //현금
            taxinvoice.chkBill = "";                                //수표
            taxinvoice.note = "";                                   //어음
            taxinvoice.credit = "";                                 //외상미수금
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";
            taxinvoice.kwon = 1;
            taxinvoice.ho = 1;

            taxinvoice.businessLicenseYN = false;                   //사업자등록증 이미지 첨부시 설정.
            taxinvoice.bankBookYN = false;                          //통장사본 이미지 첨부시 설정.
            taxinvoice.faxreceiveNum = "";                          //발행시 Fax발송기능 사용시 수신번호 기재.
            taxinvoice.faxsendYN = false;                           //발행시 Fax발송시 설정.

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;                                   //일련번호
            detail.purchaseDT = "20140319";                         //거래일자
            detail.itemName = "품목명";            
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;
            detail.itemName = "품목명";

            taxinvoice.detailList.Add(detail);

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.email = "test2@invoicee.com";
            addContact.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact);


            try
            {
                Response response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, false);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            try
            {
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행예정시 메모.", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.CancelSend(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행예정 취소시 메모.", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Taxinvoice taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                //자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

                string tmp = null;

                tmp += "InvoicerCorpNum : " + taxinvoice.invoicerCorpNum + CRLF;
                tmp += "InvoicerCorpName : " + taxinvoice.invoicerCorpName + CRLF;
                tmp += "InvoiceeCorpNum : " + taxinvoice.invoiceeCorpNum + CRLF;
                tmp += "InvoiceeCorpName : " + taxinvoice.invoiceeCorpName + CRLF;

                MessageBox.Show(tmp);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
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
                tmp += "invoiceeCorpName : " + taxinvoiceInfo.invoiceeCorpName + CRLF;
                tmp += "invoiceeCorpNum : " + taxinvoiceInfo.invoiceeCorpNum + CRLF;
                tmp += "invoiceeMgtKey : " + taxinvoiceInfo.invoiceeMgtKey + CRLF;
                tmp += "trusteeCorpName : " + taxinvoiceInfo.trusteeCorpName + CRLF;
                tmp += "trusteeCorpNum : " + taxinvoiceInfo.trusteeCorpNum + CRLF;
                tmp += "trusteeMgtKey : " + taxinvoiceInfo.trusteeMgtKey + CRLF;

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
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "TBOX");

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
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "SBOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetURL_PBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "PBOX");

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
                string url = taxinvoiceService.GetURL(txtCorpNum.Text, txtUserId.Text, "WRITE");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                List<TaxinvoiceLog> logList = taxinvoiceService.GetLogs(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                String tmp = "";


                foreach (TaxinvoiceLog log in logList)
                {
                    tmp += log.docLogType + " | " + log.log + " | " + log.procType + " | " + log.procCorpName + " | " + log.procContactName + " | " + log.procMemo + " | " + log.regDT + " | " + log.ip + CRLF;
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
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            //'최대 1000건.
            MgtKeyList.Add("1234");
            MgtKeyList.Add("12345");

            try
            {
                List<TaxinvoiceInfo> taxinvoiceInfoList = taxinvoiceService.GetInfos(txtCorpNum.Text, KeyType, MgtKeyList);

                //'TOGO Describe it.

                MessageBox.Show(taxinvoiceInfoList.Count.ToString());


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }

        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            try
            {
                Response response = taxinvoiceService.SendEmail(txtCorpNum.Text, KeyType, txtMgtKey.Text, "test@test.com", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {

            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.SendSMS(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "111-2222-4444", "발신문자 내용...", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.SendFAX(txtCorpNum.Text, KeyType, txtMgtKey.Text, "1111-2222", "000-2222-4444", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetPopUpURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }

        }

        private void btnEPrintURL_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetEmailURL_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                string url = taxinvoiceService.GetEPrintURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            List<string> MgtKeyList = new List<string>();

            //'최대 1000건.
            MgtKeyList.Add("1234");
            MgtKeyList.Add("12345");

            try
            {
                string url = taxinvoiceService.GetMassPrintURL(txtCorpNum.Text, KeyType, MgtKeyList, txtUserId.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnSendToNTS_Click(object sender, EventArgs e)
        {

            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.SendToNTS(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Issue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행시 메모", false, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCancelIssue_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Accept(txtCorpNum.Text, KeyType, txtMgtKey.Text, "승인시 메모.", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnDeny_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Deny(txtCorpNum.Text, KeyType, txtMgtKey.Text, "거부시 메모.", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Request(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청시 메모", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCancelRequest_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.CancelRequest(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 취소시 메모", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnRefuse_Click(object sender, EventArgs e)
        {

            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 거부시 메모", txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            Taxinvoice taxinvoice = new Taxinvoice();

            taxinvoice.writeDate = "20140923";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "정발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}


            taxinvoice.invoicerCorpNum = "1231212312";
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호 수정";
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;             //문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = true;                    //발행시 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";
            taxinvoice.invoiceeCorpNum = "8888888888";
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = "";
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";

            taxinvoice.supplyCostTotal = "100000";                  //필수 공급가액 합계"
            taxinvoice.taxTotal = "10000";                          //필수 세액 합계
            taxinvoice.totalAmount = "110000";                      //필수 합계금액.  공급가액 + 세액

            taxinvoice.modifyCode = null;                           //수정세금계산서 작성시 1~6까지 선택기재.
            taxinvoice.originalTaxinvoiceKey = "";                  //수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
            taxinvoice.serialNum = "123";
            taxinvoice.cash = "";                                   //현금
            taxinvoice.chkBill = "";                                //수표
            taxinvoice.note = "";                                   //어음
            taxinvoice.credit = "";                                 //외상미수금
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";
            taxinvoice.kwon = 1;
            taxinvoice.ho = 1;

            taxinvoice.businessLicenseYN = false;                   //사업자등록증 이미지 첨부시 설정.
            taxinvoice.bankBookYN = false;                          //통장사본 이미지 첨부시 설정.
            taxinvoice.faxreceiveNum = "";                          //발행시 Fax발송기능 사용시 수신번호 기재.
            taxinvoice.faxsendYN = false;                           //발행시 Fax발송시 설정.

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;                                   //일련번호
            detail.purchaseDT = "20140319";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;
            detail.itemName = "품목명";

            taxinvoice.detailList.Add(detail);

            taxinvoice.addContactList = new List<TaxinvoiceAddContact>();

            TaxinvoiceAddContact addContact = new TaxinvoiceAddContact();

            addContact.email = "test2@invoicee.com";
            addContact.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact);


            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnRegister_Reverse_Click(object sender, EventArgs e)
        {
            Taxinvoice taxinvoice = new Taxinvoice();

            taxinvoice.writeDate = "20140923";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "역발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}


            taxinvoice.invoicerCorpNum = "8888888888";
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호";
            taxinvoice.invoicerMgtKey = "";                         //공급자 발행까지 API로 발행하고자 할경우 정발행과 동일한 형태로 추가 기재.
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = true;                    //발행시 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";
            taxinvoice.invoiceeCorpNum = "1231212312";
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;            //문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";

            taxinvoice.supplyCostTotal = "100000";                  //필수 공급가액 합계"
            taxinvoice.taxTotal = "10000";                          //필수 세액 합계
            taxinvoice.totalAmount = "110000";                      //필수 합계금액.  공급가액 + 세액

            taxinvoice.modifyCode = null;                           //수정세금계산서 작성시 1~6까지 선택기재.
            taxinvoice.originalTaxinvoiceKey = "";                  //수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
            taxinvoice.serialNum = "123";
            taxinvoice.cash = "";                                   //현금
            taxinvoice.chkBill = "";                                //수표
            taxinvoice.note = "";                                   //어음
            taxinvoice.credit = "";                                 //외상미수금
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";
            taxinvoice.kwon = 1;
            taxinvoice.ho = 1;

            taxinvoice.businessLicenseYN = false;                   //사업자등록증 이미지 첨부시 설정.
            taxinvoice.bankBookYN = false;                          //통장사본 이미지 첨부시 설정.
            taxinvoice.faxreceiveNum = "";                          //발행시 Fax발송기능 사용시 수신번호 기재.
            taxinvoice.faxsendYN = false;                           //발행시 Fax발송시 설정.

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;                                   //일련번호
            detail.purchaseDT = "20140319";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;
            detail.itemName = "품목명";

            taxinvoice.detailList.Add(detail);

            try
            {
                Response response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnUpdate_Reverse_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            Taxinvoice taxinvoice = new Taxinvoice();

            taxinvoice.writeDate = "20140923";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "역발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}


            taxinvoice.invoicerCorpNum = "8888888888";
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호 수정";
            taxinvoice.invoicerMgtKey = "";                         //공급자 발행까지 API로 발행하고자 할경우 정발행과 동일한 형태로 추가 기재.
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = true;                    //발행시 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";
            taxinvoice.invoiceeCorpNum = "1231212312";
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;             //문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";

            taxinvoice.supplyCostTotal = "100000";                  //필수 공급가액 합계"
            taxinvoice.taxTotal = "10000";                          //필수 세액 합계
            taxinvoice.totalAmount = "110000";                      //필수 합계금액.  공급가액 + 세액

            taxinvoice.modifyCode = null;                           //수정세금계산서 작성시 1~6까지 선택기재.
            taxinvoice.originalTaxinvoiceKey = "";                  //수정세금계산서 작성시 원본세금계산서의 ItemKey기재. ItemKey는 문서확인.
            taxinvoice.serialNum = "123";
            taxinvoice.cash = "";                                   //현금
            taxinvoice.chkBill = "";                                //수표
            taxinvoice.note = "";                                   //어음
            taxinvoice.credit = "";                                 //외상미수금
            taxinvoice.remark1 = "비고1";
            taxinvoice.remark2 = "비고2";
            taxinvoice.remark3 = "비고3";
            taxinvoice.kwon = 1;
            taxinvoice.ho = 1;

            taxinvoice.businessLicenseYN = false;                   //사업자등록증 이미지 첨부시 설정.
            taxinvoice.bankBookYN = false;                          //통장사본 이미지 첨부시 설정.
            taxinvoice.faxreceiveNum = "";                          //발행시 Fax발송기능 사용시 수신번호 기재.
            taxinvoice.faxsendYN = false;                           //발행시 Fax발송시 설정.

            taxinvoice.detailList = new List<TaxinvoiceDetail>();

            TaxinvoiceDetail detail = new TaxinvoiceDetail();

            detail.serialNum = 1;                                   //일련번호
            detail.purchaseDT = "20140319";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";

            taxinvoice.detailList.Add(detail);

            detail = new TaxinvoiceDetail();

            detail.serialNum = 2;
            detail.itemName = "품목명";

            taxinvoice.detailList.Add(detail);

            try
            {
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;


                try
                {
                    Response response = taxinvoiceService.AttachFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, strFileName, txtUserId.Text);

                    MessageBox.Show(response.message);
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
                }

            }
        }

        private void gtnGetFiles_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                List<AttachedFile> fileList = taxinvoiceService.GetFiles(txtCorpNum.Text, KeyType, txtMgtKey.Text);


                string tmp = "일련번호 | 표시명 | 파일아이디 | 등록일자" + CRLF;

                foreach (AttachedFile file in fileList)
                {
                    tmp += file.serialNum.ToString() + " | " + file.displayName + " | " + file.attachedFile + " | " + file.regDT + CRLF;

                }
                MessageBox.Show(tmp);



            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                Response response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }

        }
    }
}
