/*
* 팝빌 팩스 API .NET SDK C#.NET Example
* C#.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/fax/dotnet/getting-started/tutorial?fwn=csharp
*
* 업데이트 일자 : 2025-07-22
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
* 3) 발신번호 사전등록을 합니다. (등록방법은 사이트/API 두가지 방식이 있습니다.)
*    - 1. 팝빌 사이트 로그인 > [문자/팩스] > [팩스] > [발신번호 사전등록] 메뉴에서 등록
*    - 2. getSenderNumberMgtURL API를 통해 반환된 URL을 이용하여 발신번호 등록
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

            // 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
            faxService.IsTest = true;

            // 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
            faxService.IPRestrictOnOff = true;

            // 통신 IP 고정, true-사용, false-미사용, (기본값:false)
            faxService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
            faxService.UseLocalTimeYN = false;
        }

        /*
         * 팩스 발신번호 등록여부를 확인합니다.
         * - 발신번호 상태가 '승인'인 경우에만 리턴값 'Response'의 변수 'code'가 1로 반환됩니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/sendnum#CheckSenderNumber
         */
        private void btnCheckSenderNumber_Click(object sender, EventArgs e)
        {
            try
            {
                string senderNumber = "";

                Response response = faxService.CheckSenderNumber(txtCorpNum.Text, senderNumber);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + (response.code == 1 ? " (등록)" : " (미등록)") + CRLF +
                "응답메시지(message) : " + response.message, "팩스 발신번호 등록여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 발신번호 등록여부 확인");
            }
        }

        /*
         * 발신번호를 등록하고 내역을 확인하는 팩스 발신번호 관리 페이지 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/sendnum#GetSenderNumberMgtURL
         */
        private void btnGetSenderNumberMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팩스 발신번호 관리 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 발신번호 관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록한 연동회원의 팩스 발신번호 목록을 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/sendnum#GetSenderNumberList
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
                    tmp += "state (등록상태) : " + numInfo.state + CRLF;
                    tmp += "memo (메모) : " + numInfo.memo + CRLF + CRLF;
                }

                MessageBox.Show(tmp, "발신번호 목록 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
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
         * 팩스 1건을 전송합니다. (최대 전송파일 개수: 20개)
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAX
         */
        private void button1_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 수신번호
            String receiverNum = "";

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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        /*
         * 동일한 팩스파일을 다수의 수신자에게 전송하기 위해 팝빌에 접수합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAXSame
         */
        private void button2_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

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
                    receiver.receiveNum = "";

                    // 수신자명
                    receiver.receiveName = "수신자명칭_" + i;

                    // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                    receiver.interOPRefKey = "20220504" + i;

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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        /*
         * 팩스 1건을 전송합니다.(다중파일 전송) (최대 전송파일 개수: 20개)
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAXMulti
         */
        private void button3_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 수신번호
            String receiverNum = "";

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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "팩스 전송");
                }
            }
        }

        /*
         * 동일한 팩스파일을 다수의 수신자에게 전송하기 위해 팝빌에 접수합니다.(다중파일 동보전송) (최대 전송파일 개수 : 20개) (최대 1,000건)
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#SendFAXMultiSame
         */
        private void button4_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

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
                    receiver.receiveNum = "";

                    // 수신자명
                    receiver.receiveName = "수신자명칭_" + i;

                    // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                    receiver.interOPRefKey = "20220504" + i;

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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "팩스전송");
                }
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 팩스 1건을 재전송합니다.
         * - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAX
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 팩스 1건을 재전송합니다.
         * - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAXRN
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 다수건의 팩스를 재전송합니다. (최대 전송파일 개수: 20개) (최대 1,000건)
         * - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAXSame
         */
        private void btnResendFAXSame_Click(object sender, EventArgs e)
        {
            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "";

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
                receiver.receiveNum = "";

                // 수신자명
                receiver.receiveName = "수신자명칭_" + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiver.interOPRefKey = "20220504" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 다수건의 팩스를 재전송합니다. (최대 전송파일 개수: 20개) (최대 1,000건)
         * - 발신/수신 정보 미입력시 기존과 동일한 정보로 팩스가 전송되고, 접수일 기준 최대 60일이 경과되지 않는 건만 재전송이 가능합니다.
         * - 팩스 재전송 요청시 포인트가 차감됩니다. (전송실패시 환불처리)
         * - 변환실패 사유로 전송실패한 팩스 접수건은 재전송이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#ResendFAXRNSame
         */
        private void btnResendFAXRN_same_Click(object sender, EventArgs e)
        {
            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 발신번호, 공백으로 처리시 기존전송정보로 전송
            String senderNum = "";

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
                receiver.receiveNum = "";

                // 수신자명
                receiver.receiveName = "수신자명칭_" + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiver.interOPRefKey = "20220504" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송");
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 예약접수된 팩스 전송을 취소합니다. (예약시간 10분 전까지 가능)
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#CancelReserve
         */
        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "팩스 예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 예약전송 취소");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 예약접수된 팩스 전송을 취소합니다. (예약시간 10분 전까지 가능)
         * - https://developers.popbill.com/reference/fax/dotnet/api/send#CancelReserveRN
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "팩스 예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 예약전송 취소");
            }
        }

        /*
         * 팝빌에서 반환 받은 접수번호를 통해 팩스 전송상태 및 결과를 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/info#GetFaxResult
         */
        private void btnGetFaxResult_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResult(txtCorpNum.Text, txtReceiptNum.Text);

                string rowStr = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveNumType(수신번호 유형) | receiveName(수신자명) | " +
                                "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | cancelPageCnt(취소 페이지수) | reserveDT(예약시간) | " +
                                "receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | requestNum(요청번호) | " +
                                "chargePageCnt(과금 페이지수) | refundPageCnt(환불 페이지수) | tiffFileSize(변환파일용량(단위:byte)) | interOPRefKey(파트너 지정키)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    rowStr = null;
                    rowStr += ResultList[i].state + " | ";
                    rowStr += ResultList[i].result + " | ";
                    rowStr += ResultList[i].sendNum + " | ";
                    rowStr += ResultList[i].senderName + " | ";
                    rowStr += ResultList[i].receiveNum + " | ";
                    rowStr += ResultList[i].receiveNumType + " | ";
                    rowStr += ResultList[i].receiveName + " | ";
                    rowStr += ResultList[i].title + " | ";
                    rowStr += ResultList[i].sendPageCnt + " | ";
                    rowStr += ResultList[i].successPageCnt + " | ";
                    rowStr += ResultList[i].failPageCnt + " | ";
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
                    rowStr += ResultList[i].refundPageCnt + " | ";
                    rowStr += ResultList[i].tiffFileSize + " | ";
                    rowStr += ResultList[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송상태 확인");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 팩스 전송상태 및 결과를 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/info#GetFaxResultRN
         */
        private void btnGetFaxResultRN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<FaxResult> ResultList = faxService.GetFaxResultRN(txtCorpNum.Text, txtRequestNum.Text);

                string rowStr = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveNumType(수신번호 유형) | receiveName(수신자명) | " +
                                "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | cancelPageCnt(취소 페이지수) | reserveDT(예약시간) | " +
                                "receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | requestNum(요청번호) | " +
                                "chargePageCnt(과금 페이지수) | refundPageCnt(환불 페이지수) | tiffFileSize(변환파일용량(단위:byte)) | interOPRefKey(파트너 지정키)";

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
                    rowStr += ResultList[i].receiveNumType + " | ";
                    rowStr += ResultList[i].title + " | ";
                    rowStr += ResultList[i].sendPageCnt + " | ";
                    rowStr += ResultList[i].successPageCnt + " | ";
                    rowStr += ResultList[i].failPageCnt + " | ";
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
                    rowStr += ResultList[i].refundPageCnt + " | ";
                    rowStr += ResultList[i].tiffFileSize + " | ";
                    rowStr += ResultList[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송상태 확인");
            }
        }

        /*
         * 검색조건에 해당하는 팩스 전송내역 목록을 조회합니다. (조회기간 단위 : 최대 2개월)
         * - 팩스 접수일시로부터 2개월 이내 접수건만 조회할 수 있습니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/info#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 최대 검색기간 :2개월 이내
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20241201";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20241231";

            // 전송상태 배열 ("1" , "2" , "3" , "4" 중 선택, 다중 선택 가능)
            // └ 1 = 대기 , 2 = 성공 , 3 = 실패 , 4 = 취소
            // - 미입력 시 전체조회
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            // 예약여부 (null, false , true 중 택 1)
            // └ null = 전체조회, false = 즉시전송건 조회, true = 예약전송건 조회
            // - 미입력 시 전체조회
            bool ReserveYN = false;

            // 개인조회 여부 (false , true 중 택 1)
            // false = 접수한 팩스 전체 조회 (관리자권한)
            // true = 해당 담당자 계정으로 접수한 팩스만 조회 (개인권한)
            // 미입력시 기본값 false 처리
            bool SenderOnly = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000개
            int PerPage = 100;

            // 조회하고자 하는 발신자명 또는 수신자명
            // - 미입력시 전체조회
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

                string rowStr = "state(전송상태 코드) | result(전송결과 코드) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveNumType(수신번호 유형) | receiveName(수신자명) | " +
                                "title(팩스제목) | sendPageCnt(전체 페이지수) | successPageCnt(성공 페이지수) | failPageCnt(실패 페이지수) | cancelPageCnt(취소 페이지수) | reserveDT(예약시간) | " +
                                "receiptDT(접수시간) | sendDT(발송시간) | resultDT(전송결과 수신시간) | fileNames(전송 파일명 리스트) | receiptNum(접수번호) | requestNum(요청번호) | " +
                                "chargePageCnt(과금 페이지수) | refundPageCnt(환불 페이지수) | tiffFileSize(변환파일용량(단위:byte)) | interOPRefKey(파트너 지정키)";

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
                    rowStr += searchResult.list[i].receiveNumType + " | ";
                    rowStr += searchResult.list[i].title + " | ";
                    rowStr += searchResult.list[i].sendPageCnt + " | ";
                    rowStr += searchResult.list[i].successPageCnt + " | ";
                    rowStr += searchResult.list[i].failPageCnt + " | ";
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
                    rowStr += searchResult.list[i].refundPageCnt + " | ";
                    rowStr += searchResult.list[i].tiffFileSize + " | ";
                    rowStr += searchResult.list[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송내역 조회");
            }
        }

        /*
         * 팩스 전송내역 확인 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/info#GetSentListURL
         */
        private void btnGetSentListURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetSentListURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팩스 전송내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송내역 URL");
            }
        }

        /*
         * 팩스 변환결과 확인 팝업 URL을 반환하며, 팩스전송을 위한 TIF 포맷 변환 완료 후 호출 할 수 있습니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/info#GetPreviewURL
         */
        private void btnGetPreviewURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPreviewURL(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팩스 변환결과 확인 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 미리보기 URL");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트충전 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "포인트충전 팝업 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPartnerBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetPartnerURL(txtCorpNum.Text, "CHRG");

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
         * 팩스 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetUnitCost
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                // 수신번호 유형, 일반 / 지능 중 택 1
                String receiveNumType = "일반";

                float unitCost = faxService.GetUnitCost(txtCorpNum.Text, receiveNumType);

                MessageBox.Show(receiveNumType + "망 전송단가 : " + unitCost.ToString(), "팩스 전송단가 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팩스 전송단가 확인");
            }
        }

        /*
         * 팝빌 팩스 API 서비스 과금정보를 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // 수신번호 유형, 일반 / 지능 중 택 1
                String receiveNumType = "일반";

                ChargeInfo chrgInf = faxService.GetChargeInfo(txtCorpNum.Text, receiveNumType, txtUserId.Text);

                string tmp = null;
                tmp += "unitCost (전송단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod (과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem (과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, "과금정보 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "과금정보 조회");
            }
        }

        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = faxService.CheckID(txtUserId.Text);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#JoinMember
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
                Response response = faxService.JoinMember(joinInfo);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = faxService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

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
         * 연동회원의 회사정보를 확인합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#GetCorpInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "회사정보 조회");
            }
        }

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#UpdateCorpInfo
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#RegistContact
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
            contactInfo.tel = "";

            //담당자 이메일 (최대 100자)
            contactInfo.email = "";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = faxService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = faxService.GetContactInfo(txtCorpNum.Text, contactID);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#ListContact
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#UpdateContact
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserId.Text;

            // 담당자명
            contactInfo.personName = "담당자123";

            // 연락처
            contactInfo.tel = "";

            // 이메일주소
            contactInfo.email = "";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = faxService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 정보수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 정보수정");
            }
        }

        /**
         * 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#PaymentRequest
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
                PaymentResponse response = faxService.PaymentRequest(CorpNum, PaymentForm, UserID);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetPaymentHistory
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
                    faxService.GetPaymentHistory(CorpNum, SDate, EDate, Page, PerPage, UserID);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetSettleResult
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
                    faxService.GetSettleResult(CorpNum, SettleCode, UserID);

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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetUseHistory
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
                    faxService.GetUseHistory(CorpNum, SDate, EDate, Page, PerPage, Order, UserID);

                String tmp = "";

                foreach (UseHistory history in result.list)
                {
                    tmp += "서비스 코드(itemCode) : " + history.itemCode + CRLF;
                    tmp += "포인트 증감 유형(txType) : " + history.txType + CRLF;
                    tmp += "증감 포인트(txPoint) : " + history.txPoint + CRLF;
                    tmp += "잔여포인트(balance) : " + history.balance + CRLF;
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#Refund
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
                RefundResponse result = faxService.Refund(CorpNum, refundForm, UserID);
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetRefundHistory
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
                RefundHistoryResult result = faxService.GetRefundHistory(CorpNum, Page, PerPage, UserID);
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetRefundInfo
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
                RefundHistory result = faxService.GetRefundInfo(CorpNum, RefundCode, UserID);
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/point#GetRefundableBalance
         */
        public void btnGetRefundableBalance_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Double refundableBalance = faxService.GetRefundableBalance(CorpNum, UserID);
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
         * - https://developers.popbill.com/reference/fax/dotnet/api/member#QuitMember
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
                Response response = faxService.QuitMember(CorpNum, QuitReason, UserID);
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

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            String ContactID = "testkorea20250722_01";

            try
            {
                Response response = faxService.DeleteContact(txtCorpNum.Text, ContactID, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 삭제");
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
