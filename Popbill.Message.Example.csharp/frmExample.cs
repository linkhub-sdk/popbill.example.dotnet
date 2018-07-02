
/*
 * 팝빌 문자 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 :  2018-07-02
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 30, 33 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
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
        }

        /*
         * 팝빌 로그인 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다 
         */
        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

                MessageBox.Show(url, "팝빌 로그인 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 연동회원 신규가입을 요청합니다.
         */
        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            // 링크아이디
            joinInfo.LinkID = LinkID;

            // 사업자번호 "-" 제외
            joinInfo.CorpNum = "1231212312";

            // 대표자명 
            joinInfo.CEOName = "대표자성명";

            // 상호
            joinInfo.CorpName = "상호";

            // 주소
            joinInfo.Addr = "주소";

            // 업태
            joinInfo.BizType = "업태";

            // 종목
            joinInfo.BizClass = "종목";

            // 아이디
            joinInfo.ID = "userid";

            // 비밀번호
            joinInfo.PWD = "pwd_must_be_long_enough";

            // 담당자명
            joinInfo.ContactName = "담당자명";

            // 담당자 연락처
            joinInfo.ContactTEL = "070-4304-2991";

            // 담당자 휴대폰번호
            joinInfo.ContactHP = "010-111-222";

            // 담당자 팩스번호
            joinInfo.ContactFAX = "02-6442-9700";

            // 담당자 메일주소
            joinInfo.ContactEmail = "test@test.com";

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
         * 연동회원의 잔여포인트를 조회합니다.
         * - 파트너 과금 방식의 경우 파트너 잔여 포인트 조회(GetPartnerBalance API)를 이용하시기 바랍니다.
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
         * 파트너 잔여포인트를 확인합니다.
         * - 연동과금 방식의 경우 연동회원 잔여포인트 조회(GetBalance API)를 이용하시기 바랍니다.
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
         * 해당사업자의 회원가입 여부를 확인합니다.
         * - 사업자번호는 '-'를 제외한 10자리 숫자 문자열입니다.
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
                                "응답메시지(message) : " + ex.Message, "연동회원 가이병부 확인");

            }
        }

        /*
         * 단문(SMS) 메시지 전송단가를 확인합니다.
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text,MessageType.SMS);

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
         */
        private void btnUnitCost_LMS_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.LMS);

                MessageBox.Show("전송단가 : " +  unitCost.ToString(), "장문(LMS) 메시지 전송단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송단가");
            }
        }

        private DateTime? getReserveDT()
        {
            DateTime? reserveDT = null;
            if (String.IsNullOrEmpty(txtReserveDT.Text) == false)
            {

                reserveDT = DateTime.ParseExact(txtReserveDT.Text, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            }
            return reserveDT;
        }

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
                msg.sendNum = "07043042993";

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
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "단문(SMS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "SMS(단문) 전송");
            }
        }

        private void btnSendSMS_Same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042993";

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
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, senderNum, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "SMS(단문) 전송");

                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "SMS(단문) 전송");
            }
        }

        private void btnSendLMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042993";

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
                msg.sendNum = "07043042993";

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
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "장문(LMS) 메시지 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송");

            }
        }

        private void btnSendLMS_same_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042993";

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
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, senderNum, subject, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "장문(LMS) 메시지 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "장문(LMS) 메시지 전송");
            }
        }

        private void btnSendXMS_one_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042993";

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
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, senderNum, receiver, receiverName, subject, contents, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "자동인식(XMS) 전송" );

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 전송");
            }
        }

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
                msg.sendNum = "07043042993";

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
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN);

                MessageBox.Show("접수번호 : " + receiptNum, "자동인식(XMS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "자동인식(XMS) 메시지 전송");
            }
        }

        private void btnSendXMS_same_Click(object sender, EventArgs e)
        {
            // 발신번호 
            String senderNum = "07043042993";

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
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, senderNum, subject, contents, messages, getReserveDT(), txtUserId.Text, requestNum, adsYN);

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
         * 전송내역 조회 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX");

                MessageBox.Show(url, "문자 전송내역 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송내역 팝업 URL");
            }
        }

        private void btnSendMMS_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042993";

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
         * 포토(MMS) 메시지 전송 단가를 확인합니다.
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

        private void btnSendMMS_Same_Click(object sender, EventArgs e)
        {
            // 발신번호
            String senderNum = "07043042993";

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
         * 팝빌 회원아이디 중복여부를 확인합니다.
         * - 아이디는 6자 이상 20자 미만으로 작성하시기 바랍니다.
         * - 아이디는 대/소문자 구분되지 않습니다.
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
         * 연동회원의 담당자를 추가합니다.
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디, 6자 이상 20자 미만
            contactInfo.id = "userid";

            // 비밀번호, 6자 이상 20자 미만
            contactInfo.pwd = "this_is_password";

            // 담당자명 
            contactInfo.personName = "담당자명";

            // 담당자연락처
            contactInfo.tel = "070-4304-2991";
            
            // 담당자 휴대폰번호
            contactInfo.hp = "010-111-222";

            // 담당자 팩스번호 
            contactInfo.fax = "070-4304-2991";

            // 담당자 메일주소
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
            contactInfo.fax = "02-6442-9700";

            // 이메일주소
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

        /*
         * 회사정보를 조회합니다.
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
         */
        private void btnUpdateCorpInfo_Click(object sender, EventArgs e)
        {
            CorpInfo corpInfo = new CorpInfo();

            // 대표자성명
            corpInfo.ceoname = "대표자명 테스트";

            // 상호
            corpInfo.corpName = "업체명";

            // 주소
            corpInfo.addr = "주소정보 수정";

            // 업태 
            corpInfo.bizType = "업태정보 수정";

            // 종목
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
         * 팝빌 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url, "포인트 충전 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }

        /*
         * 검색조건을 사용하여 문자 전송내역을 확인합니다. 
         * - 응답항목에 대한 정보는 "[문자 API 연동매뉴얼] > 3.3.2 Search(전송내역 목록 조회)
         *   를 참조하시기 바랍니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 최대 검색기간 : 6개월 이내
            // 시작일자, 날짜형식(yyyMMdd)
            String SDate = "20180601";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20180630";  
            
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

            // 예약여부, true-예약전송만 조회 
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

                dataGridView1.DataSource = searchResult.list;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역조회 결과");
            }
        }

        /*
         * 080 수신거부 목록을 확인합니다.
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
                    tmp += denyInfo.number + " / " + denyInfo.regDT +CRLF;

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
         * 문자 API 서비스 과금정보를 확인합니다.
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            //메시지 타입,SMS-단문, LMS-장문, MMS-포토
            MessageType msgType = MessageType.SMS; 

            try
            {
                ChargeInfo chrgInf = messageService.GetChargeInfo(txtCorpNum.Text, msgType);

                String tmp = null;

                tmp += "unitCost(전송단가) : " + chrgInf.unitCost +CRLF;
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
         * 문자 발신번호 목록을 확인합니다.
         */
        private void btnGetSenderNumberList_Click(object sender, EventArgs e)
        {
            try
            {
                List<SenderNumber> SenderNumberList = messageService.GetSenderNumberList(txtCorpNum.Text);

                String tmp = null;

                foreach (SenderNumber numInfo in SenderNumberList)
                {
                    tmp += "발신번호(number) : "+numInfo.number + CRLF;
                    tmp += "대표번호 지정여부(representYN) : " + numInfo.representYN + CRLF;
                    tmp += "등록상태(state) : " + numInfo.state + CRLF + CRLF;

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
         * 문자 발신번호 관리 팝업 URL을 확인합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */ 
        private void btnGetURL_SENDER_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "SENDER");

                MessageBox.Show(url, "문자 발신번호 관리 팝업 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 발신번호 관리 팝업 URL");
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
                string url = messageService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        private void btnGetMessagesRN_Click(object sender, EventArgs e)
        {
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResultRN(txtCorpNum.Text, txtRequestNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
        }

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
         * 문자전송 상태 정보를 확인합니다.
         * - 응답항목에 대한 정보는 "[문자 API 연동매뉴얼] > 3.3.1 GetMessage (전송내역 확인)" 을
         *   참조하시기 바랍니다.
         */
        private void btnGetMessageResult_Click(object sender, EventArgs e)
        {
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }

        }


        /*
         * 문자 예약전송건을 취소합니다.
         * - 예약취소는 전송예약시간 10분전까지만 가능합니다.
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
         * 문자 전송내역 요약정보를 확인 합니다.
         */
        private void btnGetStates_Click(object sender, EventArgs e)
        {

            List<string> ReciptNumList = new List<string>();

            ReciptNumList.Add("018041717000000018");
            ReciptNumList.Add("018041717000000019");
            
            try
            {

                List<MessageState> ResultList = messageService.GetStates(txtCorpNum.Text, ReciptNumList, txtUserId.Text);
                dataGridView1.DataSource = ResultList;
            }
            catch(PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                               "응답메시지(message) : " + ex.Message, "예약문자 전송 취소");
            }
        }

    }
}
