/*
 * 팝빌 팩스 API DotNet SDK Example
 * 
 * - DotNet C# SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 :  2019-01-09
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 29, 32 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Popbill.Fax.Example.csharp
{
    public partial class frmExample : Form
    {
        // 링크아이디
        private string LinkID = "TESTER";

        // 비밀키, 유출에 주의
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private FaxService faxService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 팩스 서비스 모듈 초기화
            faxService = new FaxService(LinkID, SecretKey);

            // 연동환경 설정값 true(개발용), false(상업용)
            faxService.IsTest = true;
        }

        /*
         * 팩스 발신번호 관리 팝업 URL을 반합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetSenderNumberMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팩스 발신번호 관리 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 발신번호 관리 팝업 URL");
            }
        }

        /*
         * 팩스 발신번호 목록을 반환합니다.
         */
        private void btnGetSenderNumberList_Click(object sender, EventArgs e)
        {
            try
            {
                List<SenderNumber> SenderNumberList = faxService.GetSenderNumberList(txtCorpNum.Text);

                String tmp = null;

                foreach (SenderNumber numInfo in SenderNumberList)
                {
                    tmp += "number (발신번호) : " + numInfo.number + CRLF;
                    tmp += "representYN (대표번호 지정여부) : " + numInfo.representYN + CRLF;
                    tmp += "state (등록상태) : " + numInfo.state + CRLF + CRLF;
                }

                MessageBox.Show(tmp, "발신번호 목록 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발신번호 목록 조회");
            }
        }

        private DateTime? getReserveDT()
        {
            DateTime? reserveDT = null;
            if (String.IsNullOrEmpty(txtReserveDT.Text) == false)
            {
                reserveDT = DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss",
                    System.Globalization.CultureInfo.InvariantCulture);
            }

            return reserveDT;
        }

        /*
         * 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
         * - 팩스전송 문서 파일포맷 안내 : http://blog.linkhub.co.kr/2561
         */
        private void button1_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 수신번호
            String receiverNum = "070111222";

            // 수신자명 
            String receiverName = "수신자명";

            // 광고팩스 전송여부
            bool adsYN = false;

            // 팩스제목
            String title = "팩스 전송 제목 테스트";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receiverNum, receiverName,
                        strFileName, getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        /*
         * [동보전송] 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
         * - 팩스전송 문서 파일포맷 안내 : http://blog.linkhub.co.kr/2561
         */
        private void button2_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 팩스제목
            String title = "팩스 동보전송 제목";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                // 수신자정보 배열 (최대 1000건)
                List<FaxReceiver> receivers = new List<FaxReceiver>();

                for (int i = 0; i < 10; i++)
                {
                    FaxReceiver receiver = new FaxReceiver();

                    // 수신번호
                    receiver.receiveNum = "070111222";

                    // 수신자명
                    receiver.receiveName = "수신자명칭_" + i;
                    receivers.Add(receiver);
                }

                // 광고팩스 전송여부
                bool adsYN = false;

                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receivers, strFileName,
                        getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        /*
         * [다중파일 전송] 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
         * - 팩스전송 문서 파일포맷 안내 : http://blog.linkhub.co.kr/2561
         */
        private void button3_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 수신번호
            String receiverNum = "070111222";

            // 수신자명
            String receiverName = "수신자명";

            // 광고팩스 전송여부
            bool adsYN = false;

            // 팩스제목
            String title = "팩스 다수파일전송 제목";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            List<String> filePaths = new List<string>();

            // 팩스전송파일, 최대 20개
            while (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filePaths.Add(fileDialog.FileName);
            }

            if (filePaths.Count > 0)
            {
                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receiverNum, receiverName,
                        filePaths, getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        /*
         * [다중파일 동보전송] 팩스를 전송합니다. (전송할 파일 개수는 최대 20개까지 가능)
         * - 팩스전송 문서 파일포맷 안내 : http://blog.linkhub.co.kr/2561
         */
        private void button4_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042991";

            // 광고팩스 전송여부 
            bool adsYN = false;

            // 팩스제목
            String title = "팩스 다중파일 동보전송 제목";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            List<String> filePaths = new List<string>();

            // 팩스전송파일, 최대 20개
            while (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filePaths.Add(fileDialog.FileName);
            }

            if (filePaths.Count > 0)
            {
                List<FaxReceiver> receivers = new List<FaxReceiver>();

                for (int i = 0; i < 10; i++)
                {
                    // 수신정보 배열(최대 1000건)
                    FaxReceiver receiver = new FaxReceiver();

                    // 수신번호
                    receiver.receiveNum = "070111222";

                    // 수신자명
                    receiver.receiveName = "수신자명칭_" + i;
                    receivers.Add(receiver);
                }

                try
                {
                    String receiptNum = faxService.SendFAX(txtCorpNum.Text, senderNum, receivers,
                        filePaths, getReserveDT(), txtUserId.Text, adsYN, title, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "팩스전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "팩스전송");
                }
            }
        }

        /*
         * 팩스를 재전송합니다.
         * - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         */
        private void btnResendFAX_Click(object sender, EventArgs e)
        {
            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "";

            // 수신번호/수신자명을 공백으로 처리시 기존전송정보로 전송
            // 수신번호
            String receiverNum = "";

            // 수신자명 
            String receiverName = "";

            // 팩스제목
            String title = "팩스 재전송 제목";

            try
            {
                String receiptNum = faxService.ResendFAX(txtCorpNum.Text, txtReceiptNum.Text,
                    senderNum, senderName, receiverNum, receiverName, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 전송요청번호(requestNum)을 할당한 팩스를 재전송합니다.
         * - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         */
        private void btnResendFAXRN_Click(object sender, EventArgs e)
        {
            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "";

            // 수신번호/수신자명을 공백으로 처리시 기존전송정보로 전송
            // 수신번호
            String receiverNum = "";

            // 수신자명 
            String receiverName = "";

            // 팩스제목
            String title = "팩스 재전송 제목";

            try
            {
                String receiptNum = faxService.ResendFAXRN(txtCorpNum.Text, txtRequestNum.Text, requestNum,
                    senderNum, senderName, receiverNum, receiverName, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * [동전송] 팩스를 재전송합니다.
         * - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         */
        private void btnResendFAXSame_Click(object sender, EventArgs e)
        {
            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "07043042991";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "발신자명";

            // 팩스제목
            String title = "팩스재전송 제목";

            // 수신자정보를 변경하지 않고 기존 전송정보로 전송하는 경우
            List<FaxReceiver> receivers = null;


            // 수신자정보를 변경하여 재전송하는 경우, 아래코드 참조
            // 수신자정보 배열 (최대 1000건)
            //List<FaxReceiver> receivers = new List<FaxReceiver>();

            /* 
            for (int i = 0; i < 10; i++)
            {
                FaxReceiver receiver = new FaxReceiver();

                // 수신번호
                receiver.receiveNum = "111-2222-3333";

                // 수신자명
                receiver.receiveName = "수신자명칭_" + i;
                receivers.Add(receiver);
            }
            */

            try
            {
                String receiptNum = faxService.ResendFAX(txtCorpNum.Text, txtReceiptNum.Text, senderNum, senderName,
                    receivers, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * [동보전] 전송요청번호(requestNum)을 할당한 팩스를 재전송합니다.
         * - 접수일로부터 60일이 경과된 경우 재전송할 수 없습니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         */
        private void btnResendFAXRN_same_Click(object sender, EventArgs e)
        {
            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "07043042991";

            // 발신자명, 공백으로 처리시 기존전송정보로 전송
            String senderName = "발신자명";

            // 팩스제목
            String title = "팩스재전송 제목";

            // 수신자정보를 변경하지 않고 기존 전송정보로 전송하는 경우
            List<FaxReceiver> receivers = null;

            // 수신자정보를 변경하여 재전송하는 경우, 아래코드 참조
            // 수신자정보 배열 (최대 1000건)
            //List<FaxReceiver> receivers = new List<FaxReceiver>();

            /* 
            for (int i = 0; i < 10; i++)
            {
                FaxReceiver receiver = new FaxReceiver();

                // 수신번호
                receiver.receiveNum = "111-2222-3333";

                // 수신자명
                receiver.receiveName = "수신자명칭_" + i;
                receivers.Add(receiver);
            }
            */

            try
            {
                String receiptNum = faxService.ResendFAXRN(txtCorpNum.Text, txtRequestNum.Text, requestNum, senderNum,
                    senderName, receivers, getReserveDT(), txtUserId.Text, title);

                MessageBox.Show("접수번호 : " + receiptNum, "팩스 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 팩스전송요청시 발급받은 접수번호(receiptNum)로 팩스 예약전송건을 취소합니다.
         * - 예약전송 취소는 예약전송시간 10분전까지 가능하며, 팩스변환 이후 가능합니다.
         */
        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "팩스 예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 예약전송 취소");
            }
        }

        /*
         * 팩스전송요청시 할당한 전송요청번호(requestNum)로 팩스 예약전송건을 취소합니다.
         * - 예약전송 취소는 예약전송시간 10분전까지 가능하며, 팩스변환 이후 가능합니다.
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "팩스 예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 예약전송 취소");
            }
        }

        /*
         * 팩스전송요청시 발급받은 접수번호(receiptNum)로 전송결과를 확인합니다
         * - 응답항목에 대한 자세한 사항은 "[팩스 API 연동매뉴얼] >  3.3.1 GetFaxDetail (전송내역 및 전송상태 확인)을 참조하시기 바랍니다.
         */
        private void btnGetFaxResult_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text);

                string rowStr = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | " +
                                "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | refundPageCnt(환불 페이지수) | " +
                                "cancelPageCnt(취소 페이지수) | reserveDT(예약시간) | receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | " +
                                "receiptNum(접수번호) | requestNum(요청번호) | chargePageCnt(과금 페이지수) | tiffFileSize(변환파일용량(단위:byte))";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    rowStr = null;
                    rowStr += ResultList[i].state + " | ";
                    rowStr += ResultList[i].result + " | ";
                    rowStr += ResultList[i].sendNum + " | ";
                    rowStr += ResultList[i].senderName + " | ";
                    rowStr += ResultList[i].receiveNum + " | ";
                    rowStr += ResultList[i].receiveName + " | ";
                    rowStr += ResultList[i].title + " | ";
                    rowStr += ResultList[i].sendPageCnt + " | ";
                    rowStr += ResultList[i].successPageCnt + " | ";
                    rowStr += ResultList[i].failPageCnt + " | ";
                    rowStr += ResultList[i].refundPageCnt + " | ";
                    rowStr += ResultList[i].cancelPageCnt + " | ";
                    rowStr += ResultList[i].reserveDT + " | ";
                    rowStr += ResultList[i].receiptDT + " | ";
                    rowStr += ResultList[i].sendDT + " | ";
                    rowStr += ResultList[i].resultDT + " | ";

                    for (int j = 0; j < ResultList[i].fileNames.Count(); j++)
                    {
                        if (j == ResultList[i].fileNames.Count() - 1)
                        {
                            rowStr += ResultList[i].fileNames[j].ToString() + " | ";
                        }
                        else
                        {
                            rowStr += ResultList[i].fileNames[j].ToString() + ",";
                        }
                    }

                    rowStr += ResultList[i].receiptNum + " | ";
                    rowStr += ResultList[i].requestNum + " | ";
                    rowStr += ResultList[i].chargePageCnt + " | ";
                    rowStr += ResultList[i].tiffFileSize;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송상태 확인");
            }
        }

        /*
         * 팩스전송요청시 할당한 전송요청번호(requestNum)으로 전송결과를 확인합니다
         * - 응답항목에 대한 자세한 사항은 "[팩스 API 연동매뉴얼] >  3.3.2 GetFaxDetailRN (전송내역 및 전송상태 확인 - 요청번호 할당)을 참조하시기 바랍니다.
         */
        private void btnGetFaxResultRN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResultRN(txtCorpNum.Text, txtRequestNum.Text);

                string rowStr = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | " +
                                "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | refundPageCnt(환불 페이지수) | " +
                                "cancelPageCnt(취소 페이지수) | reserveDT(예약시간) | receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | " +
                                "receiptNum(접수번호) | requestNum(요청번호) | chargePageCnt(과금 페이지수) | tiffFileSize(변환파일용량(단위:byte))";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    rowStr = null;
                    rowStr += ResultList[i].state + " | ";
                    rowStr += ResultList[i].result + " | ";
                    rowStr += ResultList[i].sendNum + " | ";
                    rowStr += ResultList[i].senderName + " | ";
                    rowStr += ResultList[i].receiveNum + " | ";
                    rowStr += ResultList[i].receiveName + " | ";
                    rowStr += ResultList[i].title + " | ";
                    rowStr += ResultList[i].sendPageCnt + " | ";
                    rowStr += ResultList[i].successPageCnt + " | ";
                    rowStr += ResultList[i].failPageCnt + " | ";
                    rowStr += ResultList[i].refundPageCnt + " | ";
                    rowStr += ResultList[i].cancelPageCnt + " | ";
                    rowStr += ResultList[i].reserveDT + " | ";
                    rowStr += ResultList[i].receiptDT + " | ";
                    rowStr += ResultList[i].sendDT + " | ";
                    rowStr += ResultList[i].resultDT + " | ";

                    for (int j = 0; j < ResultList[i].fileNames.Count(); j++)
                    {
                        if (j == ResultList[i].fileNames.Count() - 1)
                        {
                            rowStr += ResultList[i].fileNames[j].ToString() + " | ";
                        }
                        else
                        {
                            rowStr += ResultList[i].fileNames[j].ToString() + ",";
                        }
                    }

                    rowStr += ResultList[i].receiptNum + " | ";
                    rowStr += ResultList[i].requestNum + " | ";
                    rowStr += ResultList[i].chargePageCnt + " | ";
                    rowStr += ResultList[i].tiffFileSize;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송상태 확인");
            }
        }

        /*
         * 검색조건을 사용하여 팩스전송 내역을 조회합니다.
         * - 최대 검색기간 : 6개월 이내
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 최대 검색기간 : 6개월 이내 
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20181201";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20190110";

            //전송상태 배열 1-대기, 2-성공, 3-실패, 4-취소
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            // 예약여부, true-예약전송건 검색, false-즉시전송건 검색 
            bool ReserveYN = false;

            // 개인조회여부, True-개인조회, False-회사조회
            bool SenderOnly = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000개
            int PerPage = 100;

            // 조회 검색어, 팩스 전송시 기재한 발신자명 또는 수신자명 기재
            String QString = "";

            listBox1.Items.Clear();
            try
            {
                FAXSearchResult searchResult = faxService.Search(txtCorpNum.Text, SDate, EDate, State, ReserveYN,
                    SenderOnly, Order, Page, PerPage, QString);

                String tmp = null;

                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수): " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF + CRLF;

                MessageBox.Show(tmp, "팩스 전송내역 조회");

                string rowStr = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) | "+
                                "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | refundPageCnt(환불 페이지수) | " +
                                "cancelPageCnt(취소 페이지수) | reserveDT(예약시간) | receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | " +
                                "receiptNum(접수번호) | requestNum(요청번호) | chargePageCnt(과금 페이지수) | tiffFileSize(변환파일용량(단위:byte))";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < searchResult.list.Count; i++)
                {
                    rowStr = null;
                    rowStr += searchResult.list[i].state + " | ";
                    rowStr += searchResult.list[i].result + " | ";
                    rowStr += searchResult.list[i].sendNum + " | ";
                    rowStr += searchResult.list[i].senderName + " | ";
                    rowStr += searchResult.list[i].receiveNum + " | ";
                    rowStr += searchResult.list[i].receiveName + " | ";
                    rowStr += searchResult.list[i].title + " | ";
                    rowStr += searchResult.list[i].sendPageCnt + " | ";
                    rowStr += searchResult.list[i].successPageCnt + " | ";
                    rowStr += searchResult.list[i].failPageCnt + " | ";
                    rowStr += searchResult.list[i].refundPageCnt + " | ";
                    rowStr += searchResult.list[i].cancelPageCnt + " | ";
                    rowStr += searchResult.list[i].reserveDT + " | ";
                    rowStr += searchResult.list[i].receiptDT + " | ";
                    rowStr += searchResult.list[i].sendDT + " | ";
                    rowStr += searchResult.list[i].resultDT + " | ";

                    for (int j = 0; j < searchResult.list[i].fileNames.Count(); j++)
                    {
                        if (j == searchResult.list[i].fileNames.Count() - 1)
                        {
                            rowStr += searchResult.list[i].fileNames[j].ToString() + " | ";
                        }
                        else
                        {
                            rowStr += searchResult.list[i].fileNames[j].ToString() + ",";
                        }
                    }

                    rowStr += searchResult.list[i].receiptNum + " | ";
                    rowStr += searchResult.list[i].requestNum + " | ";
                    rowStr += searchResult.list[i].chargePageCnt + " | ";
                    rowStr += searchResult.list[i].tiffFileSize;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송내역 조회");
            }
        }

        /*
         * 팩스 전송내역 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetSentListURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetSentListURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팩스 전송내역 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송내역 URL");
            }
        }

        /*
         * 접수한 팩스 전송건에 대한 미리보기 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetPreviewURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPreviewURL(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팩스 미리보기 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 미리보기 URL");
            }
        }

        /*
         * 연동회원의 잔여포인트를 조회합니다.
         * - 파트너 과금 방식의 경우 파트너 잔여포인트 조회(GetPartnerBalance API)를 이용하시기 바랍니다. 
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = faxService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("연동회원 잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 팝빌 연동회원 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트충전 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트충전 팝업 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 연동과금 방식의 경우 연동회원 잔여포인트 조회 (GetBalance API)를 이용하시기 바랍니다.
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = faxService.GetPartnerBalance(txtCorpNum.Text);

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
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 포인트충전 URL");
            }
        }

        /*
         * 팩스 전송단가를 확인합니다.
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = faxService.GetUnitCost(txtCorpNum.Text);

                MessageBox.Show("팩스 전송단가 확인 : " + unitCost.ToString(), "팩스 전송단가 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팩스 전송단가 확인");
            }
        }

        /*
         * 팩스 APi서비스 과금정보를 확인합니다.
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ChargeInfo chrgInf = faxService.GetChargeInfo(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;
                tmp += "unitCost (전송단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod (과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem (과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, "과금정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "과금정보 조회");
            }
        }

        /*
         * 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * 팝빌 회원아이디 중복여부를 확인합니다.
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckID(txtUserId.Text);

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
                Response response = faxService.JoinMember(joinInfo);

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
         * 팝빌에 로그인 상태로 접근할 수 있는 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 회사정보를 조회합니다.
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = faxService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
                Response response = faxService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

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
                Response response = faxService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * 연동회원의 담당자 목록을 확인합니다.
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = faxService.ListContact(txtCorpNum.Text, txtUserId.Text);

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

            // 담당자명 
            contactInfo.personName = "담당자123";

            // 연락처
            contactInfo.tel = "070-4304-2991";

            // 휴대폰번호
            contactInfo.hp = "010-1234-1234";

            // 팩스번호 
            contactInfo.fax = "070-111-222";

            // 이메일주소
            contactInfo.email = "dev@linkhub.co.kr";

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;

            try
            {
                Response response = faxService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 정보수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보수정");
            }
        }
    }
}