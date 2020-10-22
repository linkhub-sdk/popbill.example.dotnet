/*
 * 팝빌 문자 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - https://docs.popbill.com/message/tutorial/dotnet
 * - 업데이트 일자 : 2020-10-22
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 32, 35 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 * 2) 팝빌 개발용 사이트(test.popbill.com)에 연동회원으로 가입합니다.
 * 3) 발신번호 사전등록을 합니다. (등록방법은 사이트/API 두가지 방식이 있습니다.)
 *    - 1. 팝빌 사이트 로그인 > [문자/팩스] > [카카오톡] > [발신번호 사전등록] 메뉴에서 등록
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

namespace Popbill.Message.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private MessageService messageService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            // 문자 서비스 모듈 초기화
            messageService = new MessageService(LinkID, SecretKey);

            // 연동환경 설정값, true(개발용), false(상업용)
            messageService.IsTest = true;

            // 발급된 토큰에 대한 IP 제한기능 사용여부, 권장(True)
            messageService.IPRestrictOnOff = true;

            // 로컬PC 시간 사용 여부 true(사용), false(기본값) - 미사용
            messageService.UseLocalTimeYN = false;
        }


        /*
         * 문자 발신번호 목록을 반환합니다.
         * - https://docs.popbill.com/message/dotnet/api#GetSenderNumberList
         */
        private void btnGetSenderNumberList_Click(object sender, EventArgs e)
        {
            try
            {
                List<SenderNumber> SenderNumberList = messageService.GetSenderNumberList(txtCorpNum.Text);

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

        /*
         * 문자 발신번호 관리 팝업 URL을 반합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         * - https://docs.popbill.com/message/dotnet/api#GetSenderNumberMgtURL
         */
        private void btnGetSenderNumberMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "문자 발신번호 관리 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 발신번호 관리 팝업 URL");
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
         * SMS(단문)를 전송합니다.
         *  - 메시지 내용이 90Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendSMS
         */
        private void btnSendSMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042991";

            // 수신번호
            String receiver = "010111222";

            // 수신자명 
            String receiverName = "수신자명";

            // 메시지내용, 단문(SMS) 메시지는 90byte초과된 내용은 삭제되어 전송됨. 
            String contents = "단문 문자 메시지 내용. 90byte 초과시 삭제되어 전송";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, senderNum, receiver,
                    receiverName, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "단문(SMS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 전송");
            }
        }

        /*
         * [대량전송] SMS(단문)를 전송합니다.
         *  - 메시지 내용이 90Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendSMS_Multi
         */
        private void btnSendSMS_hund_Click(object sender, EventArgs e)
        {
            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 발신번호
                msg.sendNum = "07043042992";

                // 발신자명
                msg.senderName = "발신자명";

                // 수신번호
                msg.receiveNum = "010111222";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 메시지 내용, 단문(SMS) 메시지는 90byte초과된 내용은 삭제되어 전송됨. 
                msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }

            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text,
                    requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "단문(SMS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "SMS(단문) 전송");
            }
        }

        /*
         * [동보전송] SMS(단문)를 전송합니다.
         *  - 메시지 내용이 90Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendSMS_Same
         */
        private void btnSendSMS_Same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042992";

            // 동보 메시지 내용, 단문(SMS) 메시지는 90byte초과된 내용은 삭제되어 전송됨. 
            String contents = "동보전송 문자메시지 내용";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 수신번호 
                msg.receiveNum = "010111222";

                //수신자명
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }

            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, senderNum, contents, messages,
                    getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "SMS(단문) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "SMS(단문) 전송");
            }
        }

        /*
         * LMS(장문)를 전송합니다.
         *  - 메시지 내용이 2,000Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendLMS
         */
        private void btnSendLMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042992";

            // 수신번호 
            String receiver = "010111222";

            //수신자명
            String receiverName = "수신자명";

            // 메시지 제목
            String subject = "장문문자 메시지 제목";

            // 메시지 내용, 장문(LMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨. 
            String contents = "장문문자 메시지 내용, 2000byte초과시 길이가 조정되어 전송됨";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, senderNum, receiver,
                    receiverName, subject, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "LMS(장문) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "LMS(장문) 전송");
            }
        }

        /*
         * [대량전송] LMS(장문)를 전송합니다.
         *  - 메시지 내용이 2,000Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendLMS_Multi
         */
        private void btnSendLMS_hund_Click(object sender, EventArgs e)
        {
            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 발신번호
                msg.sendNum = "07043042992";

                // 발신자명
                msg.senderName = "발신자명";

                // 수신번호
                msg.receiveNum = "010111222";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 메시지 제목
                msg.subject = "장문 문자메시지 제목";

                // 메시지 내용, 장문(LMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨. 
                msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }

            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text,
                    requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "장문(LMS) 메시지 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송");
            }
        }

        /*
         * [동보전송] LNS(장문)를 전송합니다.
         *  - 메시지 내용이 2,000Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendLMS_Same
         */
        private void btnSendLMS_same_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042992";

            // 메시지 제목
            String subject = "동보 메시지 제목";

            // 메시지 내용, 장문(LMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨. 
            String contents = "동보 메시지 내용";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "010111222";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }

            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, senderNum, subject, contents, messages,
                    getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "장문(LMS) 메시지 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송");
            }
        }

        /*
         * XMS(단문/장문 자동인식)를 전송합니다.
         *  - 메시지 내용의 길이(90byte)에 따라 SMS/LMS(단문/장문)를 자동인식하여 전송합니다.
         *  - 90byte 초과시 LMS(장문)으로 인식 합니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendXMS
         */
        private void btnSendXMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042992";

            // 수신번호 
            String receiver = "010111222";

            // 수신자명
            String receiverName = "수신자명";

            // 메시지 제목
            String subject = "장문문자 메시지 제목";

            // 메시지내용, 90byte 기준으로 단문/장문이 자동으로 인식되어 전송됨, 최대 2000byte
            String contents = "문자 메시지 내용, 메시지의 길이에 따라 90byte를 기준으로 SMS/LMS가 자동 구분되어 전송됨";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, senderNum, receiver, receiverName, subject,
                    contents, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "자동인식(XMS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 전송");
            }
        }

        /*
         * [대량전송] XMS(단문/장문 자동인식)를 전송합니다.
         *  - 메시지 내용의 길이(90byte)에 따라 SMS/LMS(단문/장문)를 자동인식하여 전송합니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendXMS_Multi
         */
        private void btnSendXMS_hund_Click(object sender, EventArgs e)
        {
            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 발신번호
                msg.sendNum = "07043042992";

                // 발신자명
                msg.senderName = "발신자명";

                // 수신번호
                msg.receiveNum = "010111222";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 메시지 제목
                msg.subject = "문자메시지 제목";

                // 메시지내용, 90byte 기준으로 단문/장문이 자동으로 인식되어 전송됨, 최대 2000byte
                msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }

            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text,
                    requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "자동인식(XMS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 메시지 전송");
            }
        }

        /*
         * [동보전송] XMS(단문/장문 자동인식)를 전송합니다.
         *  - 메시지 내용의 길이(90byte)에 따라 SMS/LMS(단문/장문)를 자동인식하여 전송합니다.
         *  - https://docs.popbill.com/message/dotnet/api#SendXMS_Same
         */
        private void btnSendXMS_same_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042992";

            // 동보 메시지 제목
            String subject = "동보 메시지 제목";

            // 동보 메시지내용, 90byte 기준으로 단문/장문이 자동으로 인식되어 전송됨, 최대 2000byte
            String contents = "동보 단문문자 메시지 내용";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "010111222";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }

            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, senderNum, subject, contents, messages,
                    getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "자동인식(XMS) 메시지 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 메시지 전송");
            }
        }

        /*
         * MMS(포토)를 전송합니다.
         *  - 메시지 내용이 2,000Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - 이미지 파일의 크기는 최대 300Kbtye (JPEG), 가로/세로 1000px 이하 권장
         *  - https://docs.popbill.com/message/dotnet/api#SendMMS
         */
        private void btnSendMMS_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042992";

            // 수신번호 
            String receiver = "010111222";

            // 수신자명
            String receiverName = "수신자명";

            // 메시지 제목
            String subject = "장문문자 메시지 제목";

            // 메시지 내용, 포토(MMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨. 
            String contents = "장문 문자 메시지 내용. 최대길이 2000byte";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string mmsFilePath = fileDialog.FileName;

                try
                {
                    string receiptNum = messageService.SendMMS(txtCorpNum.Text, senderNum, receiver, receiverName,
                        subject, contents, mmsFilePath, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                    MessageBox.Show("접수번호 : " + receiptNum, "포토(MMS) 메시지 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "포토(MMS) 메시지 전송");
                }
            }
        }

        /*
         * [동보전송] MMS(포토)를 전송합니다.
         *  - 메시지 내용이 2,000Byte 초과시 메시지 내용은 자동으로 제거됩니다.
         *  - 이미지 파일의 크기는 최대 300Kbtye (JPEG), 가로/세로 1000px 이하 권장
         *  - https://docs.popbill.com/message/dotnet/api#SendMMS_Same
         */
        private void btnSendMMS_Same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042992";

            // 메시지 제목
            String subject = "동보메시지 제목";

            // 메시지 내용, 포토(MMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨. 
            String contents = "동보 문자 메시지 내용, 최대 2000byte";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고문자여부 (기본값 false)
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 10; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "010111222";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }


            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string mmsFilePath = fileDialog.FileName;

                try
                {
                    string receiptNum = messageService.SendMMS(txtCorpNum.Text, senderNum, subject,
                        contents, messages, mmsFilePath, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                    MessageBox.Show("접수번호 : " + receiptNum, "포토(MMS) 메시지 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "포토(MMS) 메시지 전송");
                }
            }
        }

        /*
         * 문자전송요청시 발급받은 접수번호(receiptNum)로 예약문자 전송을 취소합니다.
         * - 예약취소는 예약전송시간 10분전까지만 가능합니다.
         * - https://docs.popbill.com/message/dotnet/api#CancelReserve
         */
        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "예약문자 전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

        /*
         * 문자전송요청시 할당한 전송요청번호(requestNum)로 예약문자 전송을 취소합니다.
         * - 예예약취소는 예약전송시간 10분전까지만 가능합니다.
         * - https://docs.popbill.com/message/dotnet/api#CancelReserveRN
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "예약문자 전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

        /*
         * 문자전송요청시 발급받은 접수번호(receiptNum)로 전송상태를 확인합니다
         * - https://docs.popbill.com/message/dotnet/api#GetMessageResult
         */
        private void btnGetMessageResult_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text);

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    rowStr = null;
                    rowStr += ResultList[i].subject + " | ";
                    rowStr += ResultList[i].content + " | ";
                    rowStr += ResultList[i].sendNum + " | ";
                    rowStr += ResultList[i].senderName + " | ";
                    rowStr += ResultList[i].receiveNum + " | ";
                    rowStr += ResultList[i].receiveName + " | ";
                    rowStr += ResultList[i].receiptDT + " | ";
                    rowStr += ResultList[i].sendDT + " | ";
                    rowStr += ResultList[i].resultDT + " | ";
                    rowStr += ResultList[i].reserveDT + " | ";
                    rowStr += ResultList[i].state + " | ";
                    rowStr += ResultList[i].result + " | ";
                    rowStr += ResultList[i].type + " | ";
                    rowStr += ResultList[i].tranNet + " | ";
                    rowStr += ResultList[i].receiptNum + " | ";
                    rowStr += ResultList[i].requestNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
        }

        /*
         * 문자전송요청시 할당한 전송요청번호(requestNum)로 전송상태를 확인합니다
         * - https://docs.popbill.com/message/dotnet/api#GetMessageResultRN
         */
        private void btnGetMessagesRN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResultRN(txtCorpNum.Text, txtRequestNum.Text);

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    rowStr = null;
                    rowStr += ResultList[i].subject + " | ";
                    rowStr += ResultList[i].content + " | ";
                    rowStr += ResultList[i].sendNum + " | ";
                    rowStr += ResultList[i].senderName + " | ";
                    rowStr += ResultList[i].receiveNum + " | ";
                    rowStr += ResultList[i].receiveName + " | ";
                    rowStr += ResultList[i].receiptDT + " | ";
                    rowStr += ResultList[i].sendDT + " | ";
                    rowStr += ResultList[i].resultDT + " | ";
                    rowStr += ResultList[i].reserveDT + " | ";
                    rowStr += ResultList[i].state + " | ";
                    rowStr += ResultList[i].result + " | ";
                    rowStr += ResultList[i].type + " | ";
                    rowStr += ResultList[i].tranNet + " | ";
                    rowStr += ResultList[i].receiptNum + " | ";
                    rowStr += ResultList[i].requestNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
        }

        /*
         * 문자 전송내역 요약정보를 확인합니다. (최대 1000건)
         * - https://docs.popbill.com/message/dotnet/api#GetStates
         */
        private void btnGetStates_Click(object sender, EventArgs e)
        {
            List<string> ReciptNumList = new List<string>();

            ReciptNumList.Add("018090410000000416");
            ReciptNumList.Add("018090410000000395");

            listBox1.Items.Clear();
            try
            {
                List<MessageState> ResultList =
                    messageService.GetStates(txtCorpNum.Text, ReciptNumList, txtUserId.Text);

                string rowStr = "rNum(접수번호) | sn(일련번호) | stat(전송 상태코드) | rlt(전송 결과코드) | sDT(전송일시) | rDT(결과코드 수신일시) | net(전송 이동통신사명) | srt(구 전송결과 코드)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    rowStr = null;
                    rowStr += ResultList[i].rNum + " | ";
                    rowStr += ResultList[i].sn + " | ";
                    rowStr += ResultList[i].stat + " | ";
                    rowStr += ResultList[i].rlt + " | ";
                    rowStr += ResultList[i].sDT + " | ";
                    rowStr += ResultList[i].rDT + " | ";
                    rowStr += ResultList[i].net + " | ";
                    rowStr += ResultList[i].srt;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역 요약정보");
            }
        }

        /*
         * 검색조건을 사용하여 문자전송 내역을 조회합니다.
         * - 최대 검색기간 : 6개월 이내
         * - https://docs.popbill.com/message/dotnet/api#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 최대 검색기간 : 6개월 이내
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20190901";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20190930";

            // 전송상태값 배열, 1-대기, 2-성공, 3-실패, 4-취소
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            // 검색대상 배열, SMS, LMS, MMS
            String[] Item = new String[3];
            Item[0] = "SMS";
            Item[1] = "LMS";
            Item[2] = "MMS";

            // 예약여부, true-예약전송건 조회, false-즉시전송건 조회 
            bool ReserveYN = false;

            // 개인조회여부 true-개인조회, false-회사조회 
            bool SenderYN = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000건
            int PerPage = 100;

            // 조회 검색어, 문자 전송시 기재한 수신자명 또는 발신자명 기재
            String QString = "";

            listBox1.Items.Clear();
            try
            {
                MSGSearchResult searchResult = messageService.Search(txtCorpNum.Text, SDate, EDate, State,
                    Item, ReserveYN, SenderYN, Order, Page, PerPage, QString);

                String tmp = null;
                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF;

                MessageBox.Show(tmp, "전송내역조회 결과");

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < searchResult.list.Count; i++)
                {
                    rowStr = null;
                    rowStr += searchResult.list[i].subject + " | ";
                    rowStr += searchResult.list[i].content + " | ";
                    rowStr += searchResult.list[i].sendNum + " | ";
                    rowStr += searchResult.list[i].senderName + " | ";
                    rowStr += searchResult.list[i].receiveNum + " | ";
                    rowStr += searchResult.list[i].receiveName + " | ";
                    rowStr += searchResult.list[i].receiptDT + " | ";
                    rowStr += searchResult.list[i].sendDT + " | ";
                    rowStr += searchResult.list[i].resultDT + " | ";
                    rowStr += searchResult.list[i].reserveDT + " | ";
                    rowStr += searchResult.list[i].state + " | ";
                    rowStr += searchResult.list[i].result + " | ";
                    rowStr += searchResult.list[i].type + " | ";
                    rowStr += searchResult.list[i].tranNet + " | ";
                    rowStr += searchResult.list[i].receiptNum + " | ";
                    rowStr += searchResult.list[i].requestNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역조회 결과");
            }
        }

        /*
         * 문자 전송내역 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책에 따라 30초의 유효시간을 갖습니다.
         * - https://docs.popbill.com/message/dotnet/api#GetSentListURL
         */
        private void btnGetSentListURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetSentListURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "문자 전송내역 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송내역 팝업 URL");
            }
        }

        /*
         * 080 서비스 수신거부 목록을 확인합니다.
         * - https://docs.popbill.com/message/dotnet/api#GetAutoDenyList
         */
        private void btnGetAutoDenyList_Click(object sender, EventArgs e)
        {
            try
            {
                List<AutoDeny> AutoDenyList = messageService.GetAutoDenyList(txtCorpNum.Text);

                String tmp = null;

                tmp = "number(번호) / regDT(등록일시)" + CRLF;

                foreach (AutoDeny denyInfo in AutoDenyList)
                {
                    tmp += denyInfo.number + " / " + denyInfo.regDT + CRLF;
                }

                MessageBox.Show(tmp, "080 수신거부목록 조회");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "080 수신거부목록 확인");
            }
        }

        /*
         * 연동회원의 잔여포인트를 조회합니다.
         * - 파트너 과금 방식의 경우 파트너 잔여 포인트 조회(GetPartnerBalance API)를 이용하시기 바랍니다.
         * - https://docs.popbill.com/message/dotnet/api#GetBalance
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = messageService.GetBalance(txtCorpNum.Text);

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
         * - https://docs.popbill.com/message/dotnet/api#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "포인트 충전 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }

        /*
         * 파트너 잔여포인트를 확인합니다.
         * - 연동과금 방식의 경우 연동회원 잔여포인트 조회(GetBalance API)를 이용하시기 바랍니다.
         * - https://docs.popbill.com/message/dotnet/api#GetPartnerBalance
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = messageService.GetPartnerBalance(txtCorpNum.Text);

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
         * - https://docs.popbill.com/message/dotnet/api#GetPartnerURL
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 포인트충전 URL");
            }
        }

        /*
         * 단문(SMS) 메시지 전송단가를 확인합니다.
         * - https://docs.popbill.com/message/dotnet/api#GetUnitCost
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.SMS);

                MessageBox.Show("전송단가 : " + unitCost.ToString(), "단문(SMS) 메시지 전송단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 메시지 전송단가");
            }
        }

        /*
         * 장문(LMS) 메시지 전송단가를 확인합니다. 
         * - https://docs.popbill.com/message/dotnet/api#GetUnitCost
         */
        private void btnUnitCost_LMS_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.LMS);

                MessageBox.Show("전송단가 : " + unitCost.ToString(), "장문(LMS) 메시지 전송단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송단가");
            }
        }

        /*
         * 포토(MMS) 메시지 전송 단가를 확인합니다.
         * - https://docs.popbill.com/message/dotnet/api#GetUnitCost
         */
        private void btn_unitcost_mms_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.MMS);

                MessageBox.Show("전송단가 : " + unitCost.ToString(), "포토(MMS) 메시지 전송 단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포토(MMS) 메시지 전송 단가");
            }
        }

        /*
         * 문자 API 서비스 과금정보를 확인합니다.
         * - https://docs.popbill.com/message/dotnet/api#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            //메시지 타입,SMS-단문, LMS-장문, MMS-포토
            MessageType msgType = MessageType.SMS;

            try
            {
                ChargeInfo chrgInf = messageService.GetChargeInfo(txtCorpNum.Text, msgType);

                String tmp = null;

                tmp += "unitCost(전송단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod(과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem(과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, "단문(SMS) 과금정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 과금정보 확인");
            }
        }

        /*
         * 해당 사업자의 파트너 연동회원 가입여부를 확인합니다.
         * - https://docs.popbill.com/message/dotnet/api#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * - https://docs.popbill.com/message/dotnet/api#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CheckID(txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "ID 중복확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "ID 중복확인");
            }
        }

        /*
         * 연동회원 신규가입을 요청합니다.
         * - https://docs.popbill.com/message/dotnet/api#JoinMember
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
                Response response = messageService.JoinMember(joinInfo);

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
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다 
         * - https://docs.popbill.com/message/dotnet/api#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "팝빌 로그인 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 회사정보를 조회합니다.
         * - https://docs.popbill.com/message/dotnet/api#GetCorpInfo
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = messageService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
         * 회사정보를 수정합니다.
         * - https://docs.popbill.com/message/dotnet/api#UpdateCorpInfo
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
                Response response = messageService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

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
         * - https://docs.popbill.com/message/dotnet/api#RegistContact
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
                Response response = messageService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://docs.popbill.com/message/dotnet/api#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = messageService.ListContact(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/message/dotnet/api#UpdateContact
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

            // 회사조회 권한여부, true(회사조회), false(개인조회)
            contactInfo.searchAllAllowYN = true;

            // 관리자 권한여부 
            contactInfo.mgrYN = false;

            try
            {
                Response response = messageService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 수정");
            }
        }
    }
}