/*
* 팝빌 문자 API .NET SDK C#.NET Example
* C#.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/sms/dotnet/getting-started/tutorial?fwn=csharp
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
*    - 1. 팝빌 사이트 로그인 > [문자/팩스] > [문자] > [발신번호 사전등록] 메뉴에서 등록
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

            // 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
            messageService.IsTest = true;

            // 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
            messageService.IPRestrictOnOff = true;

            // 통신 IP 고정, true-사용, false-미사용, (기본값:false)
            messageService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
            messageService.UseLocalTimeYN = false;
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
         * 문자 발신번호 등록여부를 확인합니다.
         * - 발신번호 상태가 '승인'인 경우에만 리턴값 'Response'의 변수 'code'가 1로 반환됩니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/sendnum#CheckSenderNumber
         */
        private void btnCheckSenderNumber_Click(object sender, EventArgs e)
        {
            try
            {
                string senderNumber = "";

                Response response = messageService.CheckSenderNumber(txtCorpNum.Text, senderNumber);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + (response.code==1?" (등록)":" (미등록)") + CRLF +
                "응답메시지(message) : " + response.message, "문자 발신번호 등록여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문자 발신번호 등록여부 확인");
            }
        }

        /*
         * 발신번호를 등록하고 내역을 확인하는 문자 발신번호 관리 페이지 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/sendnum#GetSenderNumberMgtURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문자 발신번호 관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록한 연동회원의 문자 발신번호 목록을 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/sendnum#GetSenderNumberList
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "발신번호 목록 조회");
            }
        }

        /*
         * 최대 90byte의 단문(SMS) 메시지 1건 전송을 팝빌에 접수합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendSMSOne
         */
        private void btnSendSMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 발신자명
            String senderName = "";

            // 수신번호
            String receiver = "";

            // 수신자명
            String receiverName = "수신자명";

            // 메시지내용, 단문(SMS) 메시지는 90byte초과된 내용은 삭제되어 전송됨.
            String contents = "단문 문자 메시지 내용. 90byte 초과시 삭제되어 전송";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, senderNum, senderName, receiver,
                    receiverName, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "단문(SMS) 전송");

                txtReceiptNum.Text = receiptNum;
                txtReceiptNumbyRCV.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 전송");
            }
        }

        /*
         * 최대 90byte의 단문(SMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendSMSMulti
         */
        private void btnSendSMS_hund_Click(object sender, EventArgs e)
        {
            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 2; i++)
            {
                Message msg = new Message();

                // 발신번호
                msg.sendNum = "";

                // 발신자명
                msg.senderName = "발신자명";

                // 수신번호
                msg.receiveNum = "";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 메시지 내용, 단문(SMS) 메시지는 90byte초과된 내용은 삭제되어 전송됨.
                msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "SMS(단문) 전송");
            }
        }

        /*
         * 최대 90byte의 단문(SMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendSMSSame
         */
        private void btnSendSMS_Same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 동보 메시지 내용, 단문(SMS) 메시지는 90byte초과된 내용은 삭제되어 전송됨.
            String contents = "동보전송 문자메시지 내용";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "";

                //수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "SMS(단문) 전송");
            }
        }

        /*
         * 최대 2,000byte의 장문(LMS) 메시지 1건 전송을 팝빌에 접수합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendLMSOne
         */
        private void btnSendLMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 수신번호
            String receiver = "";

            //수신자명
            String receiverName = "수신자명";

            // 메시지 제목
            String subject = "장문문자 메시지 제목";

            // 메시지 내용, 장문(LMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨.
            String contents = "장문문자 메시지 내용, 2000byte초과시 길이가 조정되어 전송됨";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "LMS(장문) 전송");
            }
        }

        /*
         * 최대 2,000byte의 장문(LMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendLMSMulti
         */
        private void btnSendLMS_hund_Click(object sender, EventArgs e)
        {
            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 발신번호
                msg.sendNum = "";

                // 발신자명
                msg.senderName = "발신자명";

                // 수신번호
                msg.receiveNum = "";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 메시지 제목
                msg.subject = "장문 문자메시지 제목";

                // 메시지 내용, 장문(LMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨.
                msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송");
            }
        }

        /*
         * 최대 2,000byte의 장문(LMS) 메시지 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendLMSMulti
         */
        private void btnSendLMS_same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 메시지 제목
            String subject = "동보 메시지 제목";

            // 메시지 내용, 장문(LMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨.
            String contents = "동보 메시지 내용";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송");
            }
        }

        /*
         * 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 1건의 메시지를 전송을 팝빌에 접수합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendXMSOne
         */
        private void btnSendXMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 수신번호
            String receiver = "";

            // 수신자명
            String receiverName = "수신자명";

            // 메시지 제목
            String subject = "장문문자 메시지 제목";

            // 메시지내용, 90byte 기준으로 단문/장문이 자동으로 인식되어 전송됨, 최대 2000byte
            String contents = "문자 메시지 내용, 메시지의 길이에 따라 90byte를 기준으로 SMS/LMS가 자동 구분되어 전송됨";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 전송");
            }
        }

        /*
         * 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 다수건의 메시지 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 수신자마다 개별 내용을 전송할 수 있습니다(대량전송).
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendXMSMulti
         */
        private void btnSendXMS_hund_Click(object sender, EventArgs e)
        {
            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 발신번호
                msg.sendNum = "";

                // 발신자명
                msg.senderName = "발신자명";

                // 수신번호
                msg.receiveNum = "";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 메시지 제목
                msg.subject = "문자메시지 제목";

                // 메시지내용, 90byte 기준으로 단문/장문이 자동으로 인식되어 전송됨, 최대 2000byte
                msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 메시지 전송");
            }
        }

        /*
         * 메시지 크기(90byte)에 따라 단문/장문(SMS/LMS)을 자동으로 인식하여 다수건의 메시지 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendXMSSame
         */
        private void btnSendXMS_same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 동보 메시지 제목
            String subject = "동보 메시지 제목";

            // 동보 메시지내용, 90byte 기준으로 단문/장문이 자동으로 인식되어 전송됨, 최대 2000byte
            String contents = "동보 단문문자 메시지 내용";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 메시지 전송");
            }
        }

        /*
         * 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 1건 전송을 팝빌에 접수합니다.
         * - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG, JPG), 가로/세로 1,000px 이하 권장
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendMMSOne
         */
        private void btnSendMMS_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 수신번호
            String receiver = "";

            // 수신자명
            String receiverName = "수신자명";

            // 메시지 제목
            String subject = "장문문자 메시지 제목";

            // 메시지 내용, 포토(MMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨.
            String contents = "장문 문자 메시지 내용. 최대길이 2000byte";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "포토(MMS) 메시지 전송");
                }
            }
        }

        /*
         * 최대 2,000byte의 메시지와 이미지로 구성된 포토문자(MMS) 다수건 전송을 팝빌에 접수합니다. (최대 1,000건)
         * - 모든 수신자에게 동일한 내용을 전송합니다(동보전송).
         * - 이미지 파일 포맷/규격 : 최대 300Kbyte(JPEG), 가로/세로 1,000px 이하 권장
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#SendMMSSame
         */
        private void btnSendMMS_Same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "";

            // 메시지 제목
            String subject = "동보메시지 제목";

            // 메시지 내용, 포토(MMS) 메시지는 2000byte초과된 내용은 삭제되어 전송됨.
            String contents = "동보 문자 메시지 내용, 최대 2000byte";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            List<Message> messages = new List<Message>();

            for (int i = 0; i < 10; i++)
            {
                Message msg = new Message();

                // 수신번호
                msg.receiveNum = "";

                // 수신자명
                msg.receiveName = "수신자명칭_" + i;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                msg.interOPRefKey = "20220504-" + i;

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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "포토(MMS) 메시지 전송");
                }
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 예약접수된 문자 메시지 전송을 취소합니다. (예약시간 10분 전까지 가능)
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReserve
         */
        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약문자 전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 예약접수된 문자 전송을 취소합니다. (예약시간 10분 전까지 가능)
         * - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReserveRN
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약문자 전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

        /*
        * 팝빌에서 반환받은 접수번호와 수신번호를 통해 예약접수된 문자 메시지 전송을 취소합니다. (예약시간 10분 전까지 가능)
        * - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReservebyRCV
        */
        private void btnCancelReservebyRCV_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReservebyRCV(txtCorpNum.Text, txtReceiptNumbyRCV.Text, txtReciveNumbyRCV.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약문자 전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

        /*
        * 파트너가 할당한 전송요청 번호와 수신번호를 통해 예약접수된 문자 전송을 취소합니다. (예약시간 10분 전까지 가능)
        * - https://developers.popbill.com/reference/sms/dotnet/api/send#CancelReserveRNbyRCV
        */
        private void btnCancelReserveRNbyRCV_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReserveRNbyRCV(txtCorpNum.Text, txtRequestNumbyRCV.Text, txtReciveNumRNbyRCV.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약문자 전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 문자 전송상태 및 결과를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/info#GetMessages
         */
        private void btnGetMessageResult_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text);

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey(파트너 지정키)";

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
                    rowStr += ResultList[i].requestNum + " | ";
                    rowStr += ResultList[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 문자 전송상태 및 결과를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/info#GetMessagesRN
         */
        private void btnGetMessagesRN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResultRN(txtCorpNum.Text, txtRequestNum.Text);

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey(파트너 지정키)";

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
                    rowStr += ResultList[i].requestNum + " | ";
                    rowStr += ResultList[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
        }

        /*
         * 검색조건에 해당하는 문자 전송내역을 조회합니다. (조회기간 단위 : 최대 2개월)
         * 문자 접수일시로부터 6개월 이내 접수건만 조회할 수 있습니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/info#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20250701";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20250731";

            // 전송상태 배열 ("1" , "2" , "3" , "4" 중 선택, 다중 선택 가능)
            // └ 1 = 대기 , 2 = 성공 , 3 = 실패 , 4 = 취소
            // - 미입력 시 전체조회
            String[] State = new String[4];
            State[0] = "1";
            State[1] = "2";
            State[2] = "3";
            State[3] = "4";

            // 검색대상 배열 ("SMS" , "LMS" , "MMS" 중 선택, 다중 선택 가능)
            // └ SMS = 단문 , LMS = 장문 , MMS = 포토문자
            // - 미입력 시 전체조회
            String[] Item = new String[3];
            Item[0] = "SMS";
            Item[1] = "LMS";
            Item[2] = "MMS";

            // 예약여부 (null, false , true 중 택 1)
            // └ null = 전체조회, false = 즉시전송건 조회, true = 예약전송건 조회
            // - 미입력 시 전체조회
            bool ReserveYN = false;

            // 개인조회 여부 (false , true 중 택 1)
            // └ false = 접수한 문자 전체 조회 (관리자권한)
            // └ true = 해당 담당자 계정으로 접수한 문자만 조회 (개인권한)
            // - 미입력시 기본값 false 처리
            bool SenderYN = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000건
            int PerPage = 100;

            // 조회하고자 하는 발신자명 또는 수신자명
            // - 미입력시 전체조회
            String QString = "";

            listBox1.Items.Clear();
            try
            {
                MSGSearchResult searchResult = messageService.Search(txtCorpNum.Text, SDate, EDate, State,
                    Item, ReserveYN, SenderYN, Order, Page, PerPage, QString);

                String tmp = null;
                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;

                MessageBox.Show(tmp, "전송내역조회 결과");

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호) | interOPRefKey(파트너 지정키)";

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
                    rowStr += searchResult.list[i].requestNum + " | ";
                    rowStr += searchResult.list[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "전송내역조회 결과");
            }
        }

        /*
         * 문자 전송내역 확인 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/info#GetSentListURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "문자 전송내역 팝업 URL");
            }
        }

        /*
         * 전용 080 번호에 등록된 수신거부 목록을 반환합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/info#GetAutoDenyList
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "080 수신거부목록 확인");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetChargeURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }

        /*
         *  연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPartnerBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPartnerURL(txtCorpNum.Text, "CHRG");

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
         * 문자(SMS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 메시지 전송단가");
            }
        }

        /*
         * 문자(LMS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송단가");
            }
        }

        /*
         * 문자(MMS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "포토(MMS) 메시지 전송 단가");
            }
        }

        /*
         * 팝빌 문자 API 서비스 과금정보를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetChargeInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 과금정보 확인");
            }
        }

        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CheckID(txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "ID 중복확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "ID 중복확인");
            }
        }

        /*
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#JoinMember
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
                Response response = messageService.JoinMember(joinInfo);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#GetAccessURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#GetCorpInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "회사정보 조회");
            }
        }

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#UpdateCorpInfo
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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#RegistContact
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
                Response response = messageService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = messageService.GetContactInfo(txtCorpNum.Text, contactID);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = messageService.ListContact(txtCorpNum.Text, txtUserId.Text);

                String tmp = null;

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#UpdateContact
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserId.Text;

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자123";

            // 연락처 (최대 20자)
            contactInfo.tel = "";

            // 이메일주소 (최대 100자)
            contactInfo.email = "";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = messageService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 수정");
            }
        }

        /*
         * 문자전송에 대한 전송결과 요약정보를 확인합니다.
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "전송내역 요약정보");
            }
        }

        /**
         * 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#PaymentRequest
         */
        private void btnPaymentRequest_Click(object sender, EventArgs e)
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
                PaymentResponse response = messageService.PaymentRequest(CorpNum, PaymentForm, UserID);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetPaymentHistory
         */
        private void btnGetPaymentHistory_Click(object sender, EventArgs e)
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
                    messageService.GetPaymentHistory(CorpNum, SDate, EDate, Page, PerPage, UserID);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetSettleResult
         */
        private void btnGetSettleResult_Click(object sender, EventArgs e)
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
                    messageService.GetSettleResult(CorpNum, SettleCode, UserID);

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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetUseHistory
         */
        private void btnGetUseHistory_Click(object sender, EventArgs e)
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
                    messageService.GetUseHistory(CorpNum, SDate, EDate, Page, PerPage, Order, UserID);

                String tmp = "";

                foreach (UseHistory history in result.list)
                {
                    tmp += "서비스 코드(itemCode) : " + history.itemCode + CRLF;
                    tmp += "포인트 증감 유형(txType) : " + history.txType + CRLF;
                    tmp += "증감 포인트(txPoint) : " + history.txPoint + CRLF;
                    tmp += "잔여 포인트(balance) : " + history.balance + CRLF;
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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#Refund
         */
        private void btnRefund_Click(object sender, EventArgs e)
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
                RefundResponse result = messageService.Refund(CorpNum, refundForm, UserID);
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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetRefundHistory
         */
        private void btnGetRefundHistory_Click(object sender, EventArgs e)
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
                RefundHistoryResult result = messageService.GetRefundHistory(CorpNum, Page, PerPage, UserID);
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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetRefundInfo
         */
        private void btnGetRefundInfo_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 환불 코드
            String RefundCode = "023040000017";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                RefundHistory result = messageService.GetRefundInfo(CorpNum, RefundCode, UserID);
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
         * - https://developers.popbill.com/reference/sms/dotnet/api/point#GetRefundableBalance
         */
        private void btnGetRefundableBalance_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Double refundableBalance = messageService.GetRefundableBalance(CorpNum, UserID);
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
         * - https://developers.popbill.com/reference/sms/dotnet/api/member#QuitMember
         */
        private void btnQuitMember_Click(object sender, EventArgs e)
        {

            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 탈퇴 사유
            String QuitReason = "탈퇴 사유";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Response response = messageService.QuitMember(CorpNum, QuitReason, UserID);
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

        /**
         * 팝빌회원에 등록된 080 수신거부 번호 정보를 확인합니다.
         * - https://developers.popbill.com/reference/sms/java/api/info#CheckAutoDenyNumber
         */
        private void btnCheckAutoDenyNumber_Click(object sender, EventArgs e)
        {
            String CorpNum = "1234567890";

            String UserID = "testkorea";

            try
            {
                AutoDenyNumberInfo response = messageService.CheckAutoDenyNumber(CorpNum, UserID);
                MessageBox.Show("전용 080 번호(smsdenyNumber) : " + response.smsdenyNumber + CRLF +
                                "등록 일시(regDT) : " + response.regDT,
                    "080 번호 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message,
                    "080 번호 확인");
            }
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            String ContactID = "testkorea20250722_01";

            try
            {
                Response response = messageService.DeleteContact(txtCorpNum.Text, ContactID, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 삭제");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 삭제");
            }
        }
    }
}
