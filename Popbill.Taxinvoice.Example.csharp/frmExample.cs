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
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private TaxinvoiceService taxinvoiceService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            //전자세금계산서 모듈 초기화
            taxinvoiceService = new TaxinvoiceService(LinkID, SecretKey);

            //연동환경 설정값, 테스트용(true), 상업용(false)
            taxinvoiceService.IsTest = true;
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
                Response response = taxinvoiceService.JoinMember(joinInfo);

                MessageBox.Show(response.message, "회원가입");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회원가입");

            }
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {

            try
            {
                double remainPoint = taxinvoiceService.GetBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString(), "잔여포인트 조회");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ "+ ex.code.ToString() + " ] " + ex.Message, "잔여포인트 조회");

            }
        }


        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckIsMember(조회할 사업자번호, 링크아이디)
                Response response = taxinvoiceService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show(response.code.ToString() + " | " + response.message, "연동회원 가입여부확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " +ex.code.ToString() + " ] " + ex.Message, "연동회원 가입여부 확인");

            }
        }

        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = taxinvoiceService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show(unitCost.ToString(), "발행단가 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ "+ex.code.ToString() + " ] " + ex.Message , "발행단가 확인");

            }
        }

        private void btnGetCertificateExpireDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime expiration = taxinvoiceService.GetCertificateExpireDate(txtCorpNum.Text);

                MessageBox.Show(expiration.ToString(), "공인인증서 만료일시 확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "공인인증서 만료일시 확인");

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

            taxinvoice.writeDate = "20150615";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "정발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}

            taxinvoice.invoicerCorpNum = "1234567890";              //공급자 사업자번호
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호";
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;             //정발행시 필수, 문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = false;                    //정발행시(공급자->공급받는자) 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";                     //공급받는자 구분, {사업자, 개인, 외국인}
            taxinvoice.invoiceeCorpNum = "8888888888";              //공급받는자 사업자번호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = "";                         //역발행시 필수, 문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";
            taxinvoice.invoiceeHP1 = "010-111-222";
            taxinvoice.invoiceeSMSSendYN = false;                   //역발행시(공급받는자->공급자) 문자발송기능 사용시 활용

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

            addContact.serialNum = 1;
            addContact.email = "test2@invoicee.com";
            addContact.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;
            addContact2.email = "test2@invoicee.com";
            addContact2.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact2);

            try
            {
                //Register(팝빌회원 사업자번호, 세금계산서 객체, 팝빌회원 아이디, 거래명세서 동시작성 여부)
                Response response = taxinvoiceService.Register(txtCorpNum.Text, taxinvoice, txtUserId.Text, false);

                MessageBox.Show(response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            Taxinvoice taxinvoice = new Taxinvoice();

            taxinvoice.writeDate = "20150615";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "정발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}

            taxinvoice.invoicerCorpNum = "1234567890";              //공급자 사업자번호
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호_수정";
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;             //정발행시 필수, 문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = false;                    //정발행시(공급자->공급받는자) 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";                     //공급받는자 구분, {사업자, 개인, 외국인}
            taxinvoice.invoiceeCorpNum = "8888888888";              //공급받는자 사업자번호
            taxinvoice.invoiceeCorpName = "공급받는자 상호_수정";
            taxinvoice.invoiceeMgtKey = "";                         //역발행시 필수, 문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";
            taxinvoice.invoiceeHP1 = "010-111-222";
            taxinvoice.invoiceeSMSSendYN = false;                   //역발행시(공급받는자->공급자) 문자발송기능 사용시 활용

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

            addContact.serialNum = 1;
            addContact.email = "test2@invoicee.com";
            addContact.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;
            addContact2.email = "test2@invoicee.com";
            addContact2.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact2);

            try
            {
                //Update(팝빌회원 사업자번호, 발행유형, 문서관리번호, 세금계산서객체, 팝빌회원 아이디)
                Response response = taxinvoiceService.Update(txtCorpNum.Text, KeyType, txtMgtKey.Text, taxinvoice, txtUserId.Text);

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
                //Delete(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원아이디)
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

            String Memo = "발행예정 메모";
            
            String EmailSubject = ""; //발행예정 메일제목, 공백으로 처리시 기본메일 제목으로 전송

            try
            {
                //Send(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 발행예정 메일제목, 팝빌회원 아이디)
                Response response = taxinvoiceService.Send(txtCorpNum.Text, KeyType, txtMgtKey.Text, Memo, EmailSubject, txtUserId.Text);

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
                //CancelSend(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
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
                //GetDetailInfo(팝빌회원 사업자번호, 발행유형, 문서관리번호)
                Taxinvoice taxinvoice = taxinvoiceService.GetDetailInfo(txtCorpNum.Text, KeyType, txtMgtKey.Text);

                //자세한 문세정보는 작성시 항목을 참조하거나, 연동메뉴얼 참조.

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
                tmp += "invoiceeSMSSendYN : " + taxinvoice.invoiceeSMSSendYN + CRLF +CRLF;

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

                // 세금계산서 항목에 대한 자세한 정보는 [전자세금계산서 API 메뉴얼 > 4.1. 세금계산서 구성] 참조.
             
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
                //GetInfo(팝빌회원 사업자번호, 발행유형, 문서관리번호)
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
                //GetLogs(팝빌회원 사업자번호, 발행유형, 문서관리번호)
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
                //GetInfos(팝빌회원 사업자번호, 발행유형, 문서관리번호 배열)
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
                //SendEmail(팝빌회원 사업자번호, 발행유형, 문서관리번호, 수신메일주소, 팝빌회원 아이디)
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
                //문자내용 길이가 90Byte 초과시 길이가 조정되어 전송됨.
                //SendSMS(팝빌회원 사업자번호, 발행유형, 문서관리번호, 발신번호, 수신번호, 문자내용, 팝빌회원 아이디)
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
                //SendFAX(팝빌회원 사업자번호, 발행유형, 문서관리번호, 발신번호, 수신번호, 팝빌회원 아이디)
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
                //GetPopUpURL(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원 아이디)
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
                //GetPrintURL(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원 아이디)
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
                //GetEPrintURL(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원 아이디)
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
                //GetMailURL(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원 아이디)
                string url = taxinvoiceService.GetMailURL(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

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
                //GetMassPrintURL(팝빌회원 사업자번호, 발행유형, 문서관리번호 배열, 팝빌회원 아이디)
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
                //SendToNTS(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원 아이디)
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
                //Issue(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 지연발행 강제발행 여부, 팝빌회원 아이디)
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
                //CancelIssue(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
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
                //Accept(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
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
                //Deny(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
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
                //Request(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
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
                //CancelRequest(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
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
                //Refuse(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
                Response response = taxinvoiceService.Refuse(txtCorpNum.Text, KeyType, txtMgtKey.Text, "역발행 요청 거부시 메모", txtUserId.Text);

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
            
            taxinvoice.invoicerCorpNum = "8888888888";              //공급자 사업자번호
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
            taxinvoice.invoicerSMSSendYN = false;                    //정발행시(공급자>공급받는자) 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";                     // 공급받는자 구분 {사업자, 개인, 외국인}
            taxinvoice.invoiceeCorpNum = "1231212312";              // 공급받는자 사업자번호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = txtMgtKey.Text;            //문서관리번호 1~24자리까지 공급받는자 사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@invoicee.com";
            taxinvoice.invoiceeHP1 = "010-111-222";
            taxinvoice.invoiceeSMSSendYN = false;                   //역발행시(공급받는자>공급자) 문자발송여부

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
                //Register(팝빌회원 사업자번호, 세금계산서 객체, 팝빌회원 아이디)
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
                //Update(팝빌회원 사업자번호, 발행유형, 문서관리번호, 세금계산서객체, 팝빌회원 아이디)
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
                    //AttachFile(팝빌회원 사업자번호, 발행유형, 문서관리번호, 첨부파일경로, 팝빌회원 아이디)
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
                //GetFiles(팝빌회원 사업자번호, 발행유형, 문서관리번호)
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
                //DeleteFile(팝빌회원 사업자번호, 발행유형, 문서관리번호, 파일아이디, 팝빌회원 아이디)
                //파일아이디의 경우 GetFiles API의 attachedFile 변수값 참조
                Response response = taxinvoiceService.DeleteFile(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtFileID.Text, txtUserId.Text);

                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                //GetInfos(팝빌회원 사업자번호, 발행유형, 문서관리번호 배열)
                List<Contact> contactList = taxinvoiceService.ListContact(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(contactList[0].id);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void getPopbillURL_LOGIN_Click(object sender, EventArgs e)
        {
            string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

            MessageBox.Show(url, "팝빌 로그인 URL");
        }

        private void getPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

            MessageBox.Show(url, "포인트 충전 URL");
        }

        private void getPopbillURL_CERT_Click(object sender, EventArgs e)
        {
            string url = taxinvoiceService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CERT");

            MessageBox.Show(url, "공인인증서 등록 URL");
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
                Response response = taxinvoiceService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("["+response.code.ToString() + "] " + response.message, "담당자 추가");
                
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("["+ex.code.ToString() + "] " + ex.Message, "담당자 추가");
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
                Response response = taxinvoiceService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("[" + response.code.ToString() + "] " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[" + ex.code.ToString() + "] " + ex.Message, "담당자 정보 수정");
            }
        }

        private void btnGetPartnerBalance_Click_1(object sender, EventArgs e)
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

        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckID(조회할 회원아이디)
                Response response = taxinvoiceService.CheckID(txtUserId.Text);

                MessageBox.Show("[ "+ response.code.ToString() + " ] " + response.message,"ID 중복확인");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ "+ex.code.ToString() + " ] " + ex.Message, "ID 중복확인");

            }
        }

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
                MessageBox.Show("[ "+ ex.code.ToString() + " ] " + ex.Message, "담당자 목록조회");
            }
        }

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
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 조회");
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
                Response response = taxinvoiceService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

                MessageBox.Show("[ " + response.code.ToString() + " ] " + response.message, "회사정보 수정");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "회사정보 수정");
            }
           
        }

        private void btnRegistIssue_Click(object sender, EventArgs e)
        {
            bool forceIssue = false;        // 지연발행 강제여부
            String memo = "즉시발행 메모";  // 즉시발행 메모 
            
            // 세금계산서 정보 객체 
            Taxinvoice taxinvoice = new Taxinvoice();

            taxinvoice.writeDate = "20160201";                      //필수, 기재상 작성일자
            taxinvoice.chargeDirection = "정과금";                  //필수, {정과금, 역과금}
            taxinvoice.issueType = "정발행";                        //필수, {정발행, 역발행, 위수탁}
            taxinvoice.purposeType = "영수";                        //필수, {영수, 청구}
            taxinvoice.issueTiming = "직접발행";                    //필수, {직접발행, 승인시자동발행}
            taxinvoice.taxType = "과세";                            //필수, {과세, 영세, 면세}

            taxinvoice.invoicerCorpNum = "1234567890";              //공급자 사업자번호
            taxinvoice.invoicerTaxRegID = "";                       //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            taxinvoice.invoicerCorpName = "공급자 상호";
            taxinvoice.invoicerMgtKey = txtMgtKey.Text;             //정발행시 필수, 문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoicerCEOName = "공급자 대표자 성명";
            taxinvoice.invoicerAddr = "공급자 주소";
            taxinvoice.invoicerBizClass = "공급자 업종";
            taxinvoice.invoicerBizType = "공급자 업태,업태2";
            taxinvoice.invoicerContactName = "공급자 담당자명";
            taxinvoice.invoicerEmail = "test@test.com";
            taxinvoice.invoicerTEL = "070-7070-0707";
            taxinvoice.invoicerHP = "010-000-2222";
            taxinvoice.invoicerSMSSendYN = false;                    //정발행시(공급자->공급받는자) 문자발송기능 사용시 활용

            taxinvoice.invoiceeType = "사업자";                     //공급받는자 구분, {사업자, 개인, 외국인}
            taxinvoice.invoiceeCorpNum = "8888888888";              //공급받는자 사업자번호
            taxinvoice.invoiceeCorpName = "공급받는자 상호";
            taxinvoice.invoiceeMgtKey = "";                         //역발행시 필수, 문서관리번호 1~24자리까지 공급자사업자번호별 중복없는 고유번호 할당
            taxinvoice.invoiceeCEOName = "공급받는자 대표자 성명";
            taxinvoice.invoiceeAddr = "공급받는자 주소";
            taxinvoice.invoiceeBizClass = "공급받는자 업종";
            taxinvoice.invoiceeBizType = "공급받는자 업태";
            taxinvoice.invoiceeTEL1 = "070-1234-1234";
            taxinvoice.invoiceeContactName1 = "공급받는자 담당자명";
            taxinvoice.invoiceeEmail1 = "test@test.com";
            taxinvoice.invoiceeHP1 = "010-111-222";
            taxinvoice.invoiceeSMSSendYN = false;                   //역발행시(공급받는자->공급자) 문자발송기능 사용시 활용

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

            addContact.serialNum = 1;
            addContact.email = "test2@invoicee.com";
            addContact.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact);

            TaxinvoiceAddContact addContact2 = new TaxinvoiceAddContact();

            addContact2.serialNum = 2;
            addContact2.email = "test2@invoicee.com";
            addContact2.contactName = "추가담당자명";

            taxinvoice.addContactList.Add(addContact2);

            try
            {
                //RegistIssue(팝빌회원 사업자번호, 세금계산서 객체, 지연발행 강제여부, 메모)
                Response response = taxinvoiceService.RegistIssue(txtCorpNum.Text, taxinvoice, forceIssue, memo);

                MessageBox.Show("[ "+response.code +" ] "+response.message, "즉시발행");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ "+ex.code.ToString() + " ] " + ex.Message, "즉시발행");
            }
        }

        private void btnCancelIssue_Sub_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            try
            {
                //CancelIssue(팝빌회원 사업자번호, 발행유형, 문서관리번호, 메모, 팝빌회원 아이디)
                Response response = taxinvoiceService.CancelIssue(txtCorpNum.Text, KeyType, txtMgtKey.Text, "발행취소시 메모.", txtUserId.Text);

                MessageBox.Show("[ "+ response.code + " ] "+ response.message, "발행취소");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ " + ex.code.ToString() + " ] " + ex.Message, "발행취소");
            }
        }

        private void btnDelete_Sub_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
            
            try
            {
                //Delete(팝빌회원 사업자번호, 발행유형, 문서관리번호, 팝빌회원아이디)
                Response response = taxinvoiceService.Delete(txtCorpNum.Text, KeyType, txtMgtKey.Text, txtUserId.Text);

                MessageBox.Show(response.message, "삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message, "삭제");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {   
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);

            String DType = "I";         // [필수] 일자유형, R-등록일자, I-발행일자, W-작성일자 중 1개기입
            String SDate = "20160101";  // [필수] 시작일자
            String EDate = "20160201";  // [필수] 종료일자
            
            // 전송상태값 배열, 미기재시 전체 상태조회, 문서상태 값 3자리의 배열, 2,3번째 자리에 와일드카드 가능
            String[] State = new String[3];
            State[0] = "100";
            State[1] = "2**";
            State[2] = "3**";

            // 문서유형 배열, N-일반세금계산서, M-수정세금계산서
            String[] Type = new String[2];
            Type[0] = "N";
            Type[1] = "M";

            // 과세형태 배열, T-과세, N-면세, Z-영세 
            String[] TaxType = new String[3];
            TaxType[0] = "T";
            TaxType[1] = "N";
            TaxType[2] = "Z";

            bool? LateOnly = null;  // 지연발행 여부, 미기재시 전체, true-지연발행분 조회, false-정상발행분 조회
            String Order = "D";     // 정렬방향, A-오름차순, D-내림차순
            int Page = 1;           // 페이지번호
            int PerPage = 25;       // 페이지당 검색개수, 최대 1000건

            try
            {
                TISearchResult searchResult = taxinvoiceService.Search(txtCorpNum.Text, KeyType, DType, SDate, EDate, State, Type, TaxType, LateOnly, Order, Page, PerPage);
               
                String tmp = null;

                tmp += "code : " + searchResult.code + CRLF;
                tmp += "total : " + searchResult.total + CRLF;
                tmp += "perPage : " + searchResult.perPage + CRLF;
                tmp += "pageNum : " + searchResult.pageNum + CRLF;
                tmp += "pageCount : " + searchResult.pageCount + CRLF;
                tmp += "message : " + searchResult.message + CRLF + CRLF;

                tmp += "itemKey | taxType | writeDate | regDT | invoicerCorpNum | invoicerCorpName | invoicerMgtKey | invoiceeCorpNum | invoiceeCorpName | invoiceeMgtKey ";
                tmp += "supplyCostTotal | taxTotal | purposeType | issueDT | stateCode | stateDT | lateIssueYN ";
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
                    tmp += taxinvoiceInfo.invoiceeCorpNum + " | ";
                    tmp += taxinvoiceInfo.invoiceeCorpName + " | ";
                    tmp += taxinvoiceInfo.invoiceeMgtKey + " | ";
                    tmp += taxinvoiceInfo.supplyCostTotal + " | ";
                    tmp += taxinvoiceInfo.taxTotal + " | ";
                    tmp += taxinvoiceInfo.purposeType + " | ";
                    tmp += taxinvoiceInfo.issueDT + " | ";
                    tmp += taxinvoiceInfo.stateCode + " | ";
                    tmp += taxinvoiceInfo.stateDT + " | ";
                    tmp += taxinvoiceInfo.lateIssueYN;

                    tmp += CRLF;
                }

                MessageBox.Show(tmp, "문서목록 조회");
        

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("[ "+ ex.code.ToString() + " ] " + ex.Message, "문서목록 조회");
            }
           
        }

        private void btnAttachStmt_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
            String MgtKey = txtMgtKey.Text;     // 세금계산서 관리번호
            int DocItemCode = 121;              // 첨부할 명세서 종류 코드
            String DocMgtKey = "20160202-01";   // 첨부할 명세서 관리번호

            try
            {
                Response response = taxinvoiceService.AttachStatement(txtCorpNum.Text, KeyType, MgtKey, DocItemCode, DocMgtKey);
                MessageBox.Show("[ " + response.code + " ] " + response.message, "전자명세서 첨부");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message, "전자명세서 첨부");
            }
        }

        private void btnDetachStmt_Click(object sender, EventArgs e)
        {
            MgtKeyType KeyType = (MgtKeyType)Enum.Parse(typeof(MgtKeyType), cboMgtKeyType.Text);
            String MgtKey = txtMgtKey.Text;     // 세금계산서 관리번호 
            int DocItemCode = 121;              // 첨부해제할 명세서 종류 코드 
            String DocMgtKey = "20160202-01";   // 첨부해제할 명세서 관리번호 

            try
            {
                Response response = taxinvoiceService.DetachStatement(txtCorpNum.Text, KeyType, MgtKey, DocItemCode, DocMgtKey);
                MessageBox.Show("[ " + response.code + " ] " + response.message, "전자명세서 첨부해제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message, "전자명세서 첨부해제");
            }
        }
    }
}
