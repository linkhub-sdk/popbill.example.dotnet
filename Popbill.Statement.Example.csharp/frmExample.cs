using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.Statement.Example.csharp
{
    public partial class frmExample : Form
    {
        // 링크아이디
        private string LinkID = "TESTER";

        // 비밀키
        private string SecretKey = "dOkfnobxAlQQwNVnkJWrM96g3a/M9DbRb99wFzdAGQA=";
        
        private StatementService statementService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            statementService = new StatementService(LinkID, SecretKey);
            
            // 연동환경 설정값, 테스트완료후 false로 변경시 상업용으로 전환 가능.
            // true(테스트용), false(상업용)
            statementService.IsTest = true;
        }

        // 명세서코드 변환
        private int selectedItemCode()
        {
            int selectedItemCode = 121;
            if (cboItemCode.Text == "거래명세서") selectedItemCode = 121;
            if (cboItemCode.Text == "청구서") selectedItemCode = 122;
            if (cboItemCode.Text == "견적서") selectedItemCode = 123;
            if (cboItemCode.Text == "발주서") selectedItemCode = 124;
            if (cboItemCode.Text == "입금표") selectedItemCode = 125;
            if (cboItemCode.Text == "영수증") selectedItemCode = 126;

            return selectedItemCode;
        }


        // 회원가입여부 확인 
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                
                //CheckIsMember(사업자번호, 링크아이디)
                Response response = statementService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show(response.code.ToString() + " | " + response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }


        // 연동회원 가입요청
        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            joinInfo.LinkID = LinkID;
            joinInfo.CorpNum = "1234567890";          //사업자번호 "-" 제외
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
                Response response = statementService.JoinMember(joinInfo);

                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }


        // 잔여포인트 확인
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = statementService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("잔여포인트 : "+remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }


        // 요금단가확인
        private void btnGetUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = statementService.GetUnitCost(txtCorpNum.Text, selectedItemCode());

                MessageBox.Show(cboItemCode.Text+" 발행단가 : "+unitCost.ToString());
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }          
        }


        // 팝빌 로그인 URL
        private void btnGetPopbillURL_LOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "LOGIN");

                MessageBox.Show(url);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }  
        }


        // 포인트 충전 팝업 URL
        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetPopbillURL(txtCorpNum.Text, txtUserID.Text, "CHRG");

                MessageBox.Show(url);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }  
        }
        
        // 파트너 잔여포인트 확인
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = statementService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " +remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        // 문서관리번호 사용여부 확인
        private void btnCheckMgtKeyInUse_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {


                bool InUse = statementService.CheckMgtKeyInuse(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                MessageBox.Show(InUse ? "사용중" : "미사용중");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        // 전자명세서 임시저장
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement();

            statement.writeDate = "20150310";             //필수, 기재상 작성일자 (yyyyMMdd)
            statement.purposeType = "영수";               //필수, {영수, 청구}
            statement.taxType = "과세";                   //필수, {과세, 영세, 면세}
            statement.formCode = txtFormCode.Text;        //맞춤양식코드, 기본값을 공백('')으로 처리하면 기본양식으로 처리.

            statement.itemCode = selectedItemCode();      //명세서코드

            statement.mgtKey = txtMgtKey.Text;            //문서관리번호

            statement.senderCorpNum = txtCorpNum.Text;    
            statement.senderTaxRegID = "";                //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            statement.senderCorpName = "공급자 상호";
            statement.senderCEOName = "공급자 대표자 성명";
            statement.senderAddr = "공급자 주소";
            statement.senderBizClass = "공급자 업종";
            statement.senderBizType = "공급자 업태,업태2";
            statement.senderContactName = "공급자 담당자명";
            statement.senderEmail = "test@test.com";
            statement.senderTEL = "070-7070-0707";
            statement.senderHP = "010-000-2222";
            statement.receiverCorpNum = "8888888888";
            statement.receiverCorpName = "공급받는자 상호";
            statement.receiverCEOName = "공급받는자 대표자 성명";
            statement.receiverAddr = "공급받는자 주소";
            statement.receiverBizClass = "공급받는자 업종";
            statement.receiverBizType = "공급받는자 업태";
            statement.receiverContactName = "공급받는자 담당자명";
            statement.receiverEmail = "test@receiver.com";

            statement.supplyCostTotal = "200000";         //필수 공급가액 합계
            statement.taxTotal = "20000";                 //필수 세액 합계
            statement.totalAmount = "220000";             //필수 합계금액.  공급가액 + 세액

            statement.serialNum = "123";                 //기재상 일련번호 항목
            statement.remark1 = "비고1";
            statement.remark2 = "비고2";
            statement.remark3 = "비고3";

            statement.businessLicenseYN = false; //사업자등록증 이미지 첨부시 설정.
            statement.bankBookYN = false;         //통장사본 이미지 첨부시 설정.

            statement.detailList = new List<StatementDetail>();

            StatementDetail detail = new StatementDetail();

            detail.serialNum = 1;                                   //일련번호, 1~99까지 순차기재
            detail.purchaseDT = "20150309";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2;                                   //일련번호, 1~99까지 순차기재
            detail.purchaseDT = "20150309";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            statement.propertyBag = new propertyBag();              // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 

            statement.propertyBag.Add("Balance", "15000");          // 전잔액
            statement.propertyBag.Add("Deposit", "5000");           // 입금액
            statement.propertyBag.Add("CBalance", "20000");         // 현잔액

            try
            {
                Response response = statementService.Register(txtCorpNum.Text, statement, txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 전자명세서 수정 - 임시저장상태의 문서만 가능
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement();

            statement.writeDate = "20150310";             //필수, 기재상 작성일자 (yyyyMMdd)
            statement.purposeType = "영수";               //필수, {영수, 청구}
            statement.taxType = "과세";                   //필수, {과세, 영세, 면세}
            statement.formCode = txtFormCode.Text;        //맞춤양식코드, 기본값을 공백('')으로 처리하면 기본양식으로 처리.  

            statement.itemCode = selectedItemCode();      //명세서코드

            statement.mgtKey = txtMgtKey.Text;            //문서관리번호

            statement.senderCorpNum = txtCorpNum.Text;
            statement.senderTaxRegID = "";                //종사업자 식별번호. 필요시 기재. 형식은 숫자 4자리.
            statement.senderCorpName = "공급자 상호";
            statement.senderCEOName = "공급자 대표자 성명";
            statement.senderAddr = "공급자 주소";
            statement.senderBizClass = "공급자 업종";
            statement.senderBizType = "공급자 업태,업태2";
            statement.senderContactName = "공급자 담당자명";
            statement.senderEmail = "test@test.com";
            statement.senderTEL = "070-7070-0707";
            statement.senderHP = "010-000-2222";
            statement.receiverCorpNum = "8888888888";
            statement.receiverCorpName = "공급받는자 상호";
            statement.receiverCEOName = "공급받는자 대표자 성명";
            statement.receiverAddr = "공급받는자 주소";
            statement.receiverBizClass = "공급받는자 업종";
            statement.receiverBizType = "공급받는자 업태";
            statement.receiverContactName = "공급받는자 담당자명";
            statement.receiverEmail = "test@receiver.com";

            statement.supplyCostTotal = "200000";         //필수 공급가액 합계
            statement.taxTotal = "20000";                 //필수 세액 합계
            statement.totalAmount = "220000";             //필수 합계금액.  공급가액 + 세액

            statement.serialNum = "123";                 //기재상 일련번호 항목
            statement.remark1 = "비고1";
            statement.remark2 = "비고2";
            statement.remark3 = "비고3";

            statement.businessLicenseYN = false; //사업자등록증 이미지 첨부시 설정.
            statement.bankBookYN = false;         //통장사본 이미지 첨부시 설정.

            statement.detailList = new List<StatementDetail>();

            StatementDetail detail = new StatementDetail();

            detail.serialNum = 1;                                   //일련번호, 1~99까지 순차기재
            detail.purchaseDT = "20150309";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);

            detail = new StatementDetail();

            detail.serialNum = 2;                                   //일련번호, 1~99까지 순차기재
            detail.purchaseDT = "20150309";                         //거래일자
            detail.itemName = "품목명";
            detail.spec = "규격";
            detail.qty = "1";                                       //수량
            detail.unitCost = "100000";                             //단가
            detail.supplyCost = "100000";                           //공급가액
            detail.tax = "10000";                                   //세액
            detail.remark = "품목비고";
            detail.spare1 = "spare1";
            detail.spare1 = "spare2";
            detail.spare1 = "spare3";
            detail.spare1 = "spare4";
            detail.spare1 = "spare5";

            statement.detailList.Add(detail);


            statement.propertyBag = new propertyBag();

            statement.propertyBag.Add("Balance", "15000");          // 추가속성항목, 자세한사항은 "전자명세서 API 연동매뉴얼> 5.2 기본양식 추가속성 테이블" 참조. 
                        

            try
            {
                Response response = statementService.Update(txtCorpNum.Text, selectedItemCode(), txtMgtKey.Text, statement, txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 발행 
        private void btnIssue_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.Issue(txtCorpNum.Text, itemCode, txtMgtKey.Text, "발행메모", txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        
        // 발행취소
        private void btnCancel_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.CancelIssue(txtCorpNum.Text, itemCode, txtMgtKey.Text, "발행취소 메모", txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }

        }

        //삭제
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.Delete(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        //파일첨부, 문서당 최대 5개
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;


                try
                {
                    Response response = statementService.AttachFile(txtCorpNum.Text, itemCode, txtMgtKey.Text, strFileName, txtUserID.Text);

                    MessageBox.Show(response.code + " | " + response.message);
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
                }

            }
        }

        

        // 첨부파일 목록
        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                List<AttachedFile> fileList = statementService.GetFiles(txtCorpNum.Text, itemCode, txtMgtKey.Text);


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


        //첨부파일 삭제
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Response response = statementService.DeleteFile(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtFileID.Text, txtUserID.Text);

                MessageBox.Show(response.code + " | " + response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 문서정보 조회
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                StatementInfo statementInfo = statementService.GetInfo(txtCorpNum.Text, itemCode, txtMgtKey.Text);

                string tmp = null;

                tmp += "itemCode : " + statementInfo.itemCode.ToString() + CRLF;
                tmp += "itemKey : " + statementInfo.itemKey + CRLF;
                tmp += "invoiceNum : " + statementInfo.invoiceNum + CRLF;
                tmp += "mgtKey : " + statementInfo.mgtKey + CRLF;
                tmp += "stateCode : " + statementInfo.stateCode.ToString() + CRLF;
                tmp += "taxType : " + statementInfo.taxType + CRLF;
                tmp += "purposeType : " + statementInfo.purposeType + CRLF;
                tmp += "writeDate : " + statementInfo.writeDate + CRLF;
                tmp += "senderCorpName : " + statementInfo.senderCorpName + CRLF;
                tmp += "senderCorpNum : " + statementInfo.senderCorpNum + CRLF;
                tmp += "receiverCorpName : " + statementInfo.receiverCorpName + CRLF;
                tmp += "receiverCorpNum : " + statementInfo.receiverCorpNum + CRLF;
                tmp += "supplyCostTotal : " + statementInfo.supplyCostTotal + CRLF;
                tmp += "taxTotal : " + statementInfo.taxTotal + CRLF;
                tmp += "issueDT : " + statementInfo.issueDT + CRLF;
                tmp += "stateDT : " + statementInfo.stateDT + CRLF;
                tmp += "openYN : " + statementInfo.openYN.ToString() + CRLF;
                tmp += "openDT : " + statementInfo.openDT + CRLF;
                tmp += "stateMemo : " + statementInfo.stateMemo + CRLF;
                tmp += "regDT : " + statementInfo.regDT + CRLF;

                MessageBox.Show(tmp);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 문서 상세정보 조회
        private void btnGetDetailInfo_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                Statement statement = statementService.GetDetailInfo(txtCorpNum.Text, itemCode, txtMgtKey.Text);
                
                string tmp = null;                                                                               
                
                tmp += "itemCode : "+statement.itemCode.ToString() +CRLF;
                tmp += "invoiceNum : " + statement.invoiceNum+ CRLF;
                tmp += "formCode : " + statement.formCode + CRLF;
                tmp += "writeDate : " + statement.writeDate + CRLF;
                tmp += "taxType : " + statement.taxType + CRLF + CRLF;


                tmp += "senderCorpNum : " + statement.senderCorpNum + CRLF;
                tmp += "senderTaxRegID : " + statement.senderTaxRegID + CRLF;
                tmp += "senderCorpName : " + statement.senderCorpName + CRLF;
                tmp += "senderCEOName : " + statement.senderCEOName + CRLF;
                tmp += "senderAddr : " + statement.senderAddr + CRLF;
                tmp += "senderBizClass : " + statement.senderBizClass + CRLF;
                tmp += "senderBizType : " + statement.senderBizType + CRLF;
                tmp += "senderContactName : " + statement.senderContactName + CRLF;
                tmp += "senderDeptName : " + statement.senderDeptName + CRLF;
                tmp += "senderTEL : " + statement.senderTEL + CRLF;
                tmp += "senderHP : " + statement.senderHP + CRLF;
                tmp += "senderEmail : " + statement.senderEmail + CRLF;
                tmp += "senderFAX : " + statement.senderFAX + CRLF + CRLF;

                tmp += "receiverCorpNum : " + statement.receiverCorpNum + CRLF;
                tmp += "receiverTaxRegID : " + statement.receiverTaxRegID + CRLF;
                tmp += "receiverCorpName : " + statement.receiverCorpName + CRLF;
                tmp += "receiverCEOName : " + statement.receiverCEOName + CRLF;
                tmp += "receiverAddr : " + statement.receiverAddr + CRLF;
                tmp += "receiverBizClass : " + statement.receiverBizClass + CRLF;
                tmp += "receiverBizType : " + statement.receiverBizType + CRLF;
                tmp += "receiverContactName : " + statement.receiverContactName + CRLF;
                tmp += "receiverDeptName : " + statement.receiverDeptName + CRLF;
                tmp += "receiverTEL : " + statement.receiverTEL + CRLF;
                tmp += "receiverHP : " + statement.receiverHP + CRLF;
                tmp += "receiverEmail : " + statement.receiverEmail + CRLF;
                tmp += "receiverFAX : " + statement.receiverFAX + CRLF + CRLF;

                tmp += "taxTotal : " + statement.taxTotal + CRLF;
                tmp += "supplyCostTotal : " + statement.supplyCostTotal + CRLF;
                tmp += "totalAmount : " + statement.totalAmount + CRLF;
                tmp += "purposeType : " + statement.purposeType + CRLF;
                tmp += "serialNum : " + statement.serialNum + CRLF;
                tmp += "remark1 : " + statement.remark1 + CRLF;
                tmp += "remark2 : " + statement.remark2 + CRLF;
                tmp += "remark3 : " + statement.remark3 + CRLF;
                tmp += "businessLicenseYN : " + statement.businessLicenseYN + CRLF;
                tmp += "bankBookYN : " + statement.bankBookYN + CRLF;
                tmp += "faxsendYN : " + statement.faxsendYN + CRLF;
                tmp += "smssendYN : " + statement.smssendYN + CRLF;
                tmp += "autoacceptYN : " + statement.autoacceptYN + CRLF + CRLF;

                if (!statement.detailList.Equals(null))
                {
                    tmp += "[detailList]" + CRLF;
                    for(int i=0; i<statement.detailList.Count(); i++)
                    {
                        tmp += "serialNum : "+ statement.detailList[i].serialNum.ToString() + CRLF;
                        //tmp += "itemName : " + statement.detailList[i].itemName + CRLF;
                        //tmp += "purchaseDT : " + statement.detailList[i].purchaseDT + CRLF;
                        //tmp += "qty : " + statement.detailList[i].qty + CRLF;
                        //tmp += "spec : " + statement.detailList[i].spec + CRLF;
                        //tmp += "supplyCost : " + statement.detailList[i].supplyCost+CRLF;
                        //tmp += "tax : " + statement.detailList[i].tax + CRLF;
                        //tmp += "unit : " + statement.detailList[i].unit + CRLF;
                        //tmp += "unitCost : " + statement.detailList[i].unitCost + CRLF;
                        //tmp += "reamark : " + statement.detailList[i].remark + CRLF + CRLF;
                    }
                    tmp += CRLF;
                }
                if (!statement.propertyBag.Equals(null))
                {
                    tmp += "[propertyBag]" + CRLF;
                    foreach (string key in statement.propertyBag.fromJsonDic().Keys)
                    {

                        tmp += key + " : " + statement.propertyBag.getValue(key) + CRLF;
                    }
                }
                MessageBox.Show(tmp);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 다량 문서조회
        private void btnGetInfos_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            List<string> MgtKeyList = new List<string>();

            //최대 1000건
            MgtKeyList.Add("20150310-01");
            MgtKeyList.Add("20150310-02");


            try
            {
                List<StatementInfo> statementInfoList = statementService.GetInfos(txtCorpNum.Text, itemCode, MgtKeyList);

                string tmp = null;

                for (int i = 0; i < statementInfoList.Count; i++)
                {
                    if (statementInfoList[i].itemKey != null)
                    {

                        tmp += "itemCode : " + statementInfoList[i].itemCode.ToString() + CRLF;
                        tmp += "itemKey : " + statementInfoList[i].itemKey + CRLF;
                        tmp += "invoiceNum : " + statementInfoList[i].invoiceNum + CRLF;
                        tmp += "mgtKey : " + statementInfoList[i].mgtKey + CRLF;
                        tmp += "stateCode : " + statementInfoList[i].stateCode.ToString() + CRLF;
                        tmp += "taxType : " + statementInfoList[i].taxType + CRLF;
                        tmp += "purposeType : " + statementInfoList[i].purposeType + CRLF;
                        tmp += "writeDate : " + statementInfoList[i].writeDate + CRLF;
                        tmp += "senderCorpName : " + statementInfoList[i].senderCorpName + CRLF;
                        tmp += "senderCorpNum : " + statementInfoList[i].senderCorpNum + CRLF;
                        tmp += "receiverCorpName : " + statementInfoList[i].receiverCorpName + CRLF;
                        tmp += "receiverCorpNum : " + statementInfoList[i].receiverCorpNum + CRLF;
                        tmp += "supplyCostTotal : " + statementInfoList[i].supplyCostTotal + CRLF;
                        tmp += "taxTotal : " + statementInfoList[i].taxTotal + CRLF;
                        tmp += "issueDT : " + statementInfoList[i].issueDT + CRLF;
                        tmp += "stateDT : " + statementInfoList[i].stateDT + CRLF;
                        tmp += "openYN : " + statementInfoList[i].openYN.ToString() + CRLF;
                        tmp += "openDT : " + statementInfoList[i].openDT + CRLF;
                        tmp += "stateMemo : " + statementInfoList[i].stateMemo + CRLF;
                        tmp += "regDT : " + statementInfoList[i].regDT + CRLF + CRLF;
                    }
                }

                MessageBox.Show(tmp);
            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }


        }


        // 문서 이력
        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            try
            {
                List<StatementLog> logList = statementService.GetLogs(txtCorpNum.Text, itemcode, txtMgtKey.Text);

                string tmp = "";

                foreach (StatementLog log in logList)
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


        // 알림 메일 전송
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();
            String ReceiverEmail = "test@test.com";

            try
            {
               Response response = statementService.SendEmail(txtCorpNum.Text, itemcode, txtMgtKey.Text, ReceiverEmail, txtUserID.Text);

               MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        //알림 문자전송
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            string senderNum = "07075103710";
            string receiverNum = "010111222";
            string msgContents = "dotnet 전자명세서 문자전송 테스트";
            try
            {
                Response response = statementService.SendSMS(txtCorpNum.Text, itemcode, txtMgtKey.Text, senderNum, receiverNum, msgContents, txtUserID.Text);
                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        //전자명세서 팩스 전송
        private void btnSendFAX_Click(object sender, EventArgs e)
        {
            int itemcode = selectedItemCode();

            string senderNum = "07075103710";
            string receiverNum = "000111222";
            
            try
            {
                Response response = statementService.SendFAX(txtCorpNum.Text, itemcode, txtMgtKey.Text, senderNum, receiverNum, txtUserID.Text);
                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        //전자명세서 내용 보기 URL
        private void btnGetPopUpURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetPopUpURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);
                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        //인쇄 팝업 URL
        private void btnGetPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetPrintURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);
                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 인쇄 팝업 URL(공급받는자)
        private void btnGetEPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetEPrintURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);
                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        // 대랑인쇄 팝업 URL
        private void btnGetMassPrintURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            List<string> mgtKeyList = new List<string>();

            mgtKeyList.Add("20150311-01");
            mgtKeyList.Add("20150311-02");
            mgtKeyList.Add("20150311-03");


            try
            {
                string url = statementService.GetMassPrintURL(txtCorpNum.Text, itemCode, mgtKeyList, txtUserID.Text);
                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 공급받는자 메일링크 URL
        private void btnGetMailURL_Click(object sender, EventArgs e)
        {
            int itemCode = selectedItemCode();

            try
            {
                string url = statementService.GetMailURL(txtCorpNum.Text, itemCode, txtMgtKey.Text, txtUserID.Text);
                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }


        // 임시문서함 URL
        private void btnGetURL_TBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetURL(txtCorpNum.Text, txtUserID.Text, "TBOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        //발행문서함 URL
        private void btnGetURL_SBOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = statementService.GetURL(txtCorpNum.Text, txtUserID.Text, "SBOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        

    }
}

