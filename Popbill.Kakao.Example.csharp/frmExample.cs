/*
 * 팝빌 카카오톡 API DotNet SDK Example
 *
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - https://docs.popbill.com/kakao/tutorial/dotnet
 * - 업데이트 일자 : 2021-12-16
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

namespace Popbill.Kakao.Example.csharp
{
    public partial class frmExample : Form
    {
        //링크아이디
        private string LinkID = "TESTER";

        //비밀키
        private string SecretKey = "SwWxqU+0TErBXy/9TVjIPEnI0VTUMMSQZtJf3Ed8q3I=";

        private KakaoService kakaoService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();


            // 카카오톡 서비스 모듈 초기화
            kakaoService = new KakaoService(LinkID, SecretKey);

            // 연동환경 설정값, true(개발용), false(상업용)
            kakaoService.IsTest = true;

            // 발급된 토큰에 대한 IP 제한기능 사용여부, 권장(True)
            kakaoService.IPRestrictOnOff = true;

            // 팝빌 API 서비스 고정 IP 사용여부, true-사용, false-미사용, 기본값(false)
            kakaoService.UseStaticIP = false;

            // 로컬PC 시간 사용 여부 true(사용), false(기본값) - 미사용
            kakaoService.UseLocalTimeYN = false;
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
         * 카카오톡 채널을 등록하고 내역을 확인하는 카카오톡 채널 관리 페이지 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetPlusFriendMgtURL
         */
        private void btnGetPlusFriendMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPlusFriendMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "카카오톡채널 계정관리 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "카카오톡채널 계정관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록한 연동회원의 카카오톡 채널 목록을 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#ListPlusFriendID
         */
        private void btnListPlusFriendID_Click(object sender, EventArgs e)
        {
            try
            {
                List<PlusFriend> plusFriendList = kakaoService.ListPlusFriendID(txtCorpNum.Text);

                String tmp = null;

                foreach (PlusFriend friendInfo in plusFriendList)
                {
                    tmp += "카카오톡채널 아이디(plusFriendID) : " + friendInfo.plusFriendID + CRLF;
                    tmp += "카카오톡채널 이름(plusFriendName) : " + friendInfo.plusFriendName + CRLF;
                    tmp += "등록일시(regDT) : " + friendInfo.regDT + CRLF + CRLF;
                }

                MessageBox.Show(tmp, "카카오톡채널 목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "카카오톡채널 목록 확인");
            }
        }

        /*
         * 발신번호를 등록하고 내역을 확인하는 카카오톡 발신번호 관리 페이지 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetSenderNumberMgtURL
         */
        private void btnGetSenderNumberMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetSenderNumberMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "발신번호 관리 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발신번호 관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록한 연동회원의 카카오톡 발신번호 목록을 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetSenderNumberList
         */
        private void btnGetSenderNumberList_Click(object sender, EventArgs e)
        {
            try
            {
                List<SenderNumber> SenderNumberList = kakaoService.GetSenderNumberList(txtCorpNum.Text, txtUserId.Text);

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
         * 알림톡 템플릿을 신청하고 승인심사 결과를 확인하며 등록 내역을 확인하는 알림톡 템플릿 관리 페이지 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetATSTemplateMgtURL
         */
        private void btnGetATSTemplateMgtURL_Click(object sender, EventArgs e)
        {
            try
            {
                String url = kakaoService.GetATSTemplateMgtURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "알림톡 템플릿 관리 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 관리 팝업 URL");
            }
        }

        /*
         * 승인된 알림톡 템플릿 정보를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetATSTemplate
         */
        private void btnGetATSTemplate_Click(object sender, EventArgs e)
        {
            // 확인할 템플릿 코드
            String templateCode = "021010000076";

            try
            {
                ATSTemplate templateInfo = kakaoService.GetATSTemplate(txtCorpNum.Text, templateCode, txtUserId.Text);

                String tmp = null;

                tmp += "템플릿 코드(templateCode) : " + templateInfo.templateCode + CRLF;
                tmp += "템플릿 제목(templateName) : " + templateInfo.templateName + CRLF;
                tmp += "템플릿 내용(template) : " + templateInfo.template + CRLF;
                tmp += "카카오톡채널 아이디(plusFriendID) : " + templateInfo.plusFriendID + CRLF;
                tmp += "광고 메시지(ads) : " + templateInfo.ads + CRLF;
                tmp += "부가 메시지(appendix) : " + templateInfo.appendix + CRLF;
                if (templateInfo.btns != null)
                {
                    foreach (KakaoButton buttonInfo in templateInfo.btns)
                    {
                        tmp += "[알림톡 버튼 정보]" + CRLF;
                        tmp += "버튼명(n) : " + buttonInfo.n + CRLF;
                        tmp += "버튼유형(t) : " + buttonInfo.t + CRLF;
                        tmp += "버튼링크1(u1) : " + buttonInfo.u1 + CRLF;
                        tmp += "버튼링크1(u2) : " + buttonInfo.u2 + CRLF;
                    }
                }
                MessageBox.Show(tmp, "알림톡 템플릿 정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 정보 확인");
            }
        }

        /*
         * 승인된 알림톡 템플릿 목록을 확인합니다.
         * - 반환항목중 템플릿코드(templateCode)는 알림톡 전송시 사용됩니다.
         * - https://docs.popbill.com/kakao/dotnet/api#ListATSTemplate
         */
        private void btnListATSTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                List<ATSTemplate> templateList = kakaoService.ListATSTemplate(txtCorpNum.Text);

                String tmp = null;

                foreach (ATSTemplate templateInfo in templateList)
                {
                    tmp += "템플릿 코드(templateCode) : " + templateInfo.templateCode + CRLF;
                    tmp += "템플릿 제목(templateName) : " + templateInfo.templateName + CRLF;
                    tmp += "템플릿 내용(template) : " + templateInfo.template + CRLF;
                    tmp += "카카오톡채널 아이디(plusFriendID) : " + templateInfo.plusFriendID + CRLF;
                    tmp += "광고 메시지(ads) : " + templateInfo.ads + CRLF;
                    tmp += "부가 메시지(appendix) : " + templateInfo.appendix + CRLF;

                    if (templateInfo.btns != null)
                    {
                        foreach (KakaoButton buttonInfo in templateInfo.btns)
                        {
                            tmp += "[알림톡 버튼 정보]" + CRLF;
                            tmp += "버튼명(n) : " + buttonInfo.n + CRLF;
                            tmp += "버튼유형(t) : " + buttonInfo.t + CRLF;
                            tmp += "버튼링크1(u1) : " + buttonInfo.u1 + CRLF;
                            tmp += "버튼링크1(u2) : " + buttonInfo.u2 + CRLF;
                        }
                    }

                    tmp += CRLF + CRLF;
                }

                MessageBox.Show(tmp, "알림톡 템플릿 목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 목록 확인");
            }
        }

        /*
         * 승인된 템플릿의 내용을 작성하여 1건의 알림톡 전송을 팝빌에 접수합니다.
         * - 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
         * - https://docs.popbill.com/kakao/dotnet/api#SendATS
         */
        private void btnSendATS_one_Click(object sender, EventArgs e)
        {
            // 알림톡 템플릿 코드, ListATSTemplate API의 templateCode 확인
            String templateCode = "019020000163";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042991";

            // 알림톡 템플릿 내용, 최대 1000자
            String content = "[ 팝빌 ]\n";
            content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            content += "해당 템플릿으로 전송 가능합니다.\n\n";
            content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            content += "팝빌 파트너센터 : 1600-8536\n";
            content += "support@linkhub.co.kr".Replace("\n", Environment.NewLine);

            // 대체문자 메시지 내용
            String altContent = "대체문자 메시지 내용";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "A";

            // 수신번호
            String receiverNum = "010111222";

            // 수신자명
            String receiverName = "수신자명";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 버튼정보를 수정하지 않고 템플릿 신청시 기재한 정보로 전송하는 경우 null 처리
            List<KakaoButton> buttons = null;


            // 버튼링크 URL 에 #{템플릿변수}를 기재하여 승인받은경우 URL 수정하여 전송
            /*
            List<KakaoButton> buttons = new List<KakaoButton>();

            KakaoButton btnInfo = new KakaoButton();

            // 버튼명
            btnInfo.n = "템플릿 안내";

            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";

            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "https://www.popbill.com";

            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";

            buttons.Add(btnInfo);
            */

            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, altSendType,
                    getReserveDT(), receiverNum, receiverName, content, altContent, requestNum, buttons);

                MessageBox.Show("접수번호 : " + receiptNum, "알림톡(ATS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송");
            }
        }

        /*
         * 승인된 템플릿의 내용을 작성하여 다수건의 알림톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
         * - 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
         * - https://docs.popbill.com/kakao/dotnet/api#SendATS_Multi
         */
        private void btnSendATS_multi_Click(object sender, EventArgs e)
        {
            // 알림톡 템플릿 코드, ListATSTemplate API의 templateCode 확인
            String templateCode = "019020000163";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042991";

            // 알림톡 템플릿 내용, 최대 1000자
            String content = "[ 팝빌 ]\n";
            content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            content += "해당 템플릿으로 전송 가능합니다.\n\n";
            content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            content += "팝빌 파트너센터 : 1600-8536\n";
            content += "support@linkhub.co.kr".Replace("\n", Environment.NewLine);

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "A";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();


            for (int i = 0; i < 5; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "010111222";

                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();

                // 알림톡 템플릿 내용, 최대 1000자
                receiverInfo.msg = content;

                // 대체문자 내용
                receiverInfo.altmsg = "대체문자 내용입니다";

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20210701-" + i.ToString();

                // 수신자별 개별 버튼내용 전송하는 경우
                // 개별 버튼의 개수는 템플릿 신청 시 승인받은 버튼의 개수와 동일하게 생성, 다를경우 실패 처리
                // 버튼링크URL에 #{템플릿변수}를 기재하여 승인받은 경우 URL 수정가능
                // 버튼 표시명, 버튼 유형 수정 불가능
                /*
                // 수신자별 개별 버튼정보 리스트 생성
                List<KakaoButton> btns = new List<KakaoButton>();

                // 개별 버튼정보 생성
                KakaoButton btnInfo1 = new KakaoButton();

                // 버튼명
                btnInfo1.n = "템플릿 안내";

                // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                btnInfo1.t = "WL";

                // 버튼링크1 [앱링크] Android / [웹링크] Mobile
                btnInfo1.u1 = "https://www.popbill.com";

                // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
                btnInfo1.u2 = "http://test.popbill.com" + i.ToString();

                // 개별 버튼정보 리스트에 개별 버튼정보 추가
                btns.Add(btnInfo1);

                // 개별 버튼정보 생성
                KakaoButton btnInfo2 = new KakaoButton();

                // 버튼명
                btnInfo2.n = "템플릿 안내";

                // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                btnInfo2.t = "WL";

                // 버튼링크1 [앱링크] Android / [웹링크] Mobile
                btnInfo2.u1 = "https://www.test.com";

                // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
                btnInfo2.u2 = "http://test.test.com" + i.ToString();

                // 개별 버튼정보 리스트에 개별 버튼정보 추가
                btns.Add(btnInfo2);

                // 수신자 정보에 개별 버튼정보 리스트 추가
                receiverInfo.btns = btns;
                */
                receivers.Add(receiverInfo);
            }

            // 버튼정보를 수정하지 않고 템플릿 신청시 기재한 정보로 전송하는 경우 null 처리
            // 개별 버튼정보 전송하는 경우 null 처리
            List<KakaoButton> buttons = null;

            // 동일 버튼정보, 수신자별 동일 버튼내용 전송하는 경우
            // 버튼링크 URL 에 #{템플릿변수}를 기재하여 승인받은경우 URL 수정하여 전송
            /*
            List<KakaoButton> buttons = new List<KakaoButton>();

            KakaoButton btnInfo = new KakaoButton();

            // 버튼명
            btnInfo.n = "템플릿 안내";

            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";

            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "https://www.popbill.com";

            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";

            buttons.Add(btnInfo);
            */

            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum,
                    altSendType, getReserveDT(), receivers, txtUserId.Text, requestNum, buttons);

                MessageBox.Show("접수번호 : " + receiptNum, "알림톡(ATS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송");
            }
        }

        /*
         * 승인된 템플릿 내용을 작성하여 다수건의 알림톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
         * - 사전에 승인된 템플릿의 내용과 알림톡 전송내용(content)이 다를 경우 전송실패 처리됩니다.
         * - https://docs.popbill.com/kakao/dotnet/api#SendATS_Same
         */
        private void btnSendATS_same_Click(object sender, EventArgs e)
        {
            // 알림톡 템플릿 코드, ListATSTemplate API의 templateCode 확인
            String templateCode = "019020000163";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "01043245117";

            String content = "[ 팝빌 ]\n";
            content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            content += "해당 템플릿으로 전송 가능합니다.\n\n";
            content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            content += "팝빌 파트너센터 : 1600-8536\n";
            content += "support@linkhub.co.kr".Replace("\n", Environment.NewLine);

            // 대체문자 메시지 내용
            String altContent = "대체문자 메시지 내용";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "A";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 5; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "010111222";

                // 수신자명
                receiverInfo.rcvnm = "수신자명";

                // 파트너 지정키, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20210701-" + i.ToString();

                receivers.Add(receiverInfo);
            }


            // 버튼정보를 수정하지 않고 템플릿 신청시 기재한 정보로 전송하는 경우 null 처리
            List<KakaoButton> buttons = null;


            // 버튼링크 URL 에 #{템플릿변수}를 기재하여 승인받은경우 URL 수정하여 전송
            /*
            List<KakaoButton> buttons = new List<KakaoButton>();

            KakaoButton btnInfo = new KakaoButton();

            // 버튼명
            btnInfo.n = "템플릿 안내";

            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";

            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "https://www.popbill.com";

            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";

            buttons.Add(btnInfo);
            */

            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, content,
                    altContent, altSendType, getReserveDT(), receivers, txtUserId.Text, requestNum, buttons);

                MessageBox.Show("접수번호 : " + receiptNum, "알림톡(ATS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송");
            }
        }

        /*
         * 텍스트로 구성된 1건의 친구톡 전송을 팝빌에 접수합니다.
         * - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
         * - https://docs.popbill.com/kakao/dotnet/api#SendFTS
         */
        private void btnSendFTS_one_Click(object sender, EventArgs e)
        {
            // 카카오톡채널 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042992";

            // 친구톡 내용, 최대 1000자
            String content = "친구톡 내용";

            // 수신번호
            String receiverNum = "010111222";

            // 수신자명
            String receiverName = "수신자명";

            // 대체문자 메시지 내용
            String altContent = "대체문자 내용";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();

            KakaoButton btnInfo = new KakaoButton();

            // 버튼명
            btnInfo.n = "버튼이름";

            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";

            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "http://www.popbill.com";

            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";

            buttons.Add(btnInfo);

            try
            {
                string receiptNum = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content,
                    altContent, altSendType, receiverNum, receiverName, adsYN, getReserveDT(), buttons, txtUserId.Text,
                    requestNum);

                MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
            }
        }

        /*
         * 텍스트로 구성된 다수건의 친구톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
         * - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
         * - https://docs.popbill.com/kakao/dotnet/api#SendFTS_Multi
         */
        private void btnSendFTS_multi_Click(object sender, EventArgs e)
        {
            // 카카오톡채널 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042939";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 5; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "010111222";

                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();

                // 친구톡 내용
                receiverInfo.msg = "개별 친구톡 내용" + i.ToString();

                // 대체문자 내용
                receiverInfo.altmsg = "대체문자 전송내용" + i.ToString();

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20210701-" + i.ToString();

                // 수신자별 개별 버튼내용 전송하는 경우
                // 생성 가능 개수 최대 5개
                /*
                // 수신자별 개별 버튼정보 리스트 생성
                List<KakaoButton> btns = new List<KakaoButton>();

                // 개별 버튼정보 생성
                KakaoButton btnInfo1 = new KakaoButton();

                // 버튼명
                btnInfo1.n = "템플릿 안내";

                // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                btnInfo1.t = "WL";

                // 버튼링크1 [앱링크] Android / [웹링크] Mobile
                btnInfo1.u1 = "https://www.popbill.com";

                // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
                btnInfo1.u2 = "http://test.popbill.com" + i.ToString();

                // 개별 버튼정보 리스트에 개별 버튼정보 추가
                btns.Add(btnInfo1);

                // 개별 버튼정보 생성
                KakaoButton btnInfo2 = new KakaoButton();

                // 버튼명
                btnInfo2.n = "템플릿 안내";

                // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                btnInfo2.t = "WL";

                // 버튼링크1 [앱링크] Android / [웹링크] Mobile
                btnInfo2.u1 = "https://www.test.com";

                // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
                btnInfo2.u2 = "http://test.test.com" + i.ToString();

                // 개별 버튼정보 리스트에 개별 버튼정보 추가
                btns.Add(btnInfo2);

                // 수신자 정보에 개별 버튼정보 리스트 추가
                receiverInfo.btns = btns;
                */
                receivers.Add(receiverInfo);
            }

            // 버튼정보를 전송하지 않는 경우, null처리
            // 개별 버튼정보 전송하는 경우, null처리
            // List<KakaoButton> buttons = null;

            // 동일 버튼정보, 수신자별 동일 버튼정보 전송하는 경우
            List<KakaoButton> buttons = new List<KakaoButton>();
            // 생성 가능 개수 최대 5개
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";
            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";
            buttons.Add(btnInfo);

            try
            {
                string receiptNum = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum,
                    altSendType, adsYN, getReserveDT(), receivers, buttons, txtUserId.Text, requestNum);

                MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
            }
        }

        /*
         * 텍스트로 구성된 다수건의 친구톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
         * - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
         * - https://docs.popbill.com/kakao/dotnet/api#SendFTS_Same
         */
        private void btnSendFTS_same_Click(object sender, EventArgs e)
        {
            // 카카오톡채널 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042992";

            // 친구톡내용, 최대 1000자
            String content = "친구톡 내용";

            // 대체문자 메시지 내용
            String altContent = "대체문자 내용";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 5; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();
                receiverInfo.rcv = "010111222";
                receiverInfo.rcvnm = "수신자명" + i.ToString();
                receivers.Add(receiverInfo);
            }

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";
            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";
            buttons.Add(btnInfo);

            try
            {
                string receiptNum = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content,
                    altContent, altSendType, adsYN, getReserveDT(), receivers, buttons, txtUserId.Text, requestNum);

                MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
            }
        }

        /*
         * 이미지가 첨부된 1건의 친구톡 전송을 팝빌에 접수합니다.
         * - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
         * - 이미지 파일 규격: 전송 포맷 – JPG 파일 (.jpg, .jpeg), 용량 – 최대 500 Kbyte, 크기 – 가로 500px 이상, 가로 기준으로 세로 0.5~1.3배 비율 가능
         * - https://docs.popbill.com/kakao/dotnet/api#SendFMS
         */
        private void btnSendFMS_one_Click(object sender, EventArgs e)
        {
            // 카카오톡채널 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042992";

            // 친구톡 내용, 최대 400자
            String content = "친구톡 내용";

            // 수신번호
            String receiverNum = "010111222";

            // 수신자명
            String receiverName = "수신자명";

            // 대체문자 내용
            String altContent = "대체문자 내용";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송 여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";
            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";
            buttons.Add(btnInfo);

            // 첨부된 이미지의 링크 URL
            String imageURL = "http://www.popbill.com";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    string receiptNum = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content,
                        altContent, altSendType, receiverNum, receiverName, adsYN, getReserveDT(), buttons,
                        strFileName, imageURL, txtUserId.Text, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
                }
            }
        }

        /*
         * 이미지가 첨부된 다수건의 친구톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
         * - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
         * - 이미지 파일 규격: 전송 포맷 – JPG 파일 (.jpg, .jpeg), 용량 – 최대 500 Kbyte, 크기 – 가로 500px 이상, 가로 기준으로 세로 0.5~1.3배 비율 가능
         * - https://docs.popbill.com/kakao/dotnet/api#SendFMS_Multi
         */
        private void btnSendFMS_multi_Click(object sender, EventArgs e)
        {
            // 카카오톡채널 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042939";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송 여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 5; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "010111222";

                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();

                // 친구톡내용, 최대 400자
                receiverInfo.msg = "개별 친구톡 내용" + i.ToString();

                // 대체문자 내용
                receiverInfo.altmsg = "대체문자 전송내용" + i.ToString();

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20210701-" + i.ToString();

                // 수신자별 개별 버튼내용 전송하는 경우
                // 생성 가능 개수 최대 5개
                /*
                // 수신자별 개별 버튼정보 리스트 생성
                List<KakaoButton> btns = new List<KakaoButton>();

                // 개별 버튼정보 생성
                KakaoButton btnInfo1 = new KakaoButton();

                // 버튼명
                btnInfo1.n = "템플릿 안내";

                // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                btnInfo1.t = "WL";

                // 버튼링크1 [앱링크] Android / [웹링크] Mobile
                btnInfo1.u1 = "https://www.popbill.com";

                // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
                btnInfo1.u2 = "http://test.popbill.com" + i.ToString();

                // 개별 버튼정보 리스트에 개별 버튼정보 추가
                btns.Add(btnInfo1);

                // 개별 버튼정보 생성
                KakaoButton btnInfo2 = new KakaoButton();

                // 버튼명
                btnInfo2.n = "템플릿 안내";

                // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
                btnInfo2.t = "WL";

                // 버튼링크1 [앱링크] Android / [웹링크] Mobile
                btnInfo2.u1 = "https://www.test.com";

                // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
                btnInfo2.u2 = "http://test.test.com" + i.ToString();

                // 개별 버튼정보 리스트에 개별 버튼정보 추가
                btns.Add(btnInfo2);

                // 수신자 정보에 개별 버튼정보 리스트 추가
                receiverInfo.btns = btns;
                */
                receivers.Add(receiverInfo);
            }

            // 버튼정보를 전송하지 않는 경우, null처리
            // 개별 버튼정보 전송하는 경우, null처리
            // List<KakaoButton> buttons = null;


            // 동일 버튼정보, 수신자별 동일 버튼정보 전송하는 경우
            List<KakaoButton> buttons = new List<KakaoButton>();
            // 생성 가능 개수 최대 5개
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";
            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";
            buttons.Add(btnInfo);

            // 첨부된 이미지의 링크 URL
            String imageURL = "http://www.popbill.com";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    string receiptNum = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum,
                        altSendType, adsYN, getReserveDT(), receivers, buttons, strFileName, imageURL,
                        txtUserId.Text, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
                }
            }
        }

        /*
         * 이미지가 첨부된 다수건의 친구톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
         * - 친구톡의 경우 야간 전송은 제한됩니다. (20:00 ~ 익일 08:00)
         * - 이미지 파일 규격: 전송 포맷 – JPG 파일 (.jpg, .jpeg), 용량 – 최대 500 Kbyte, 크기 – 가로 500px 이상, 가로 기준으로 세로 0.5~1.3배 비율 가능
         * - https://docs.popbill.com/kakao/dotnet/api#SendFMS_Same
         */
        private void btnSendFMS_same_Click(object sender, EventArgs e)
        {
            // 카카오톡채널 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042992";

            // 친구톡 내용, 최대 400자
            String content = "친구톡 내용";

            // 대체문자 메시지 내용
            String altContent = "대체문자 내용";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송 여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();
                // 수신번호
                receiverInfo.rcv = "010111222";
                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();
                receivers.Add(receiverInfo);
            }

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형 DS(-배송조회 / WL - 웹링크 / AL - 앱링크 / MD - 메시지전달 / BK - 봇키워드)
            btnInfo.t = "WL";
            // 버튼링크1 [앱링크] Android / [웹링크] Mobile
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2 [앱링크] IOS / [웹링크] PC URL
            btnInfo.u2 = "http://test.popbill.com";
            buttons.Add(btnInfo);

            // 첨부된 이미지의 링크 URL
            String imageURL = "http://www.popbill.com";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    string receiptNum = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content,
                        altContent, altSendType, adsYN, getReserveDT(), receivers, buttons, strFileName, imageURL,
                        txtUserId.Text, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                    "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
                }
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 예약접수된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
         * - https://docs.popbill.com/kakao/dotnet/api#CancelReserve
         */
        private void btnCacnelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예약전송 취소");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 예약접수된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
         * - https://docs.popbill.com/kakao/dotnet/api#CancelReserveRN
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "예약전송 취소");
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 알림톡/친구톡 전송상태 및 결과를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetMessages
         */
        private void btnGetMessages_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                KakaoSentResult info = kakaoService.GetMessages(txtCorpNum.Text, txtReceiptNum.Text);

                String tmp = "";
                tmp += "contentType (카카오톡 유형) : " + info.contentType + CRLF;
                tmp += "templateCode (템플릿 코드) : " + info.templateCode + CRLF;
                tmp += "plusFriendID (카카오톡채널 아이디) : " + info.plusFriendID + CRLF;
                tmp += "sendNum (발신번호) : " + info.sendNum + CRLF;
                tmp += "altContent (대체문자 내용) : " + info.altContent + CRLF;
                tmp += "altSendType (대체문자 유형) : " + info.altSendType + CRLF;
                tmp += "reserveDT (예약일시) : " + info.reserveDT + CRLF;
                tmp += "adsYN (광고전송 여부) : " + info.adsYN + CRLF;
                tmp += "imageURL (친구톡 이미지 URL) : " + info.imageURL + CRLF;
                tmp += "sendCnt (전송건수) : " + info.sendCnt + CRLF;
                tmp += "successCnt (성공건수) : " + info.successCnt + CRLF;
                tmp += "failCnt (실패건수) : " + info.failCnt + CRLF;
                tmp += "altCnt (대체문자 건수) : " + info.altCnt + CRLF;
                tmp += "cancelCnt (취소건수) : " + info.cancelCnt + CRLF + CRLF;

                MessageBox.Show(tmp, "전송내역 확인");

                string rowStr =
                    "state (전송상태 코드) | sendDT (전송일시) | result (전송결과 코드) | resultDT (전송결과 수신일시) | contentType (카카오톡 유형) | " +
                    "receiveNum (수신번호) | receiveName (수신자명) | content (내용) | altContentType (대체문자 전송타입) | altSendDT (대체문자 전송일시) | " +
                    "altResult (대체문자 전송결과 코드) | altResultDT (대체문자 전송결과 수신일시) | interOPRefKey (파트너 지정키) | receiptNum (접수번호) | requestNum (요청번호)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < info.msgs.Count; i++)
                {
                    rowStr = null;
                    rowStr += info.msgs[i].state + " | ";
                    rowStr += info.msgs[i].sendDT + " | ";
                    rowStr += info.msgs[i].receiveNum + " | ";
                    rowStr += info.msgs[i].receiveName + " | ";
                    rowStr += info.msgs[i].content + " | ";
                    rowStr += info.msgs[i].result + " | ";
                    rowStr += info.msgs[i].resultDT + " | ";
                    rowStr += info.msgs[i].altContent + " | ";
                    rowStr += info.msgs[i].altContentType + " | ";
                    rowStr += info.msgs[i].altSendDT + " | ";
                    rowStr += info.msgs[i].altResult + " | ";
                    rowStr += info.msgs[i].altResultDT + " | ";
                    rowStr += info.msgs[i].interOPRefKey + " | ";
                    rowStr += info.msgs[i].receiptNum + " | ";
                    rowStr += info.msgs[i].requestNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역 확인");
            }
        }

        /*
         * 파트너가 할당한 전송요청 번호를 통해 알림톡/친구톡 전송상태 및 결과를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetMessagesRN
         */
        private void btnGetMessagesRN_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                KakaoSentResult info = kakaoService.GetMessagesRN(txtCorpNum.Text, txtRequestNum.Text);

                String tmp = "";
                tmp += "contentType (카카오톡 유형) : " + info.contentType + CRLF;
                tmp += "templateCode (템플릿 코드) : " + info.templateCode + CRLF;
                tmp += "plusFriendID (카카오톡채널 아이디) : " + info.plusFriendID + CRLF;
                tmp += "sendNum (발신번호) : " + info.sendNum + CRLF;
                tmp += "altContent (대체문자 내용) : " + info.altContent + CRLF;
                tmp += "altSendType (대체문자 유형) : " + info.altSendType + CRLF;
                tmp += "reserveDT (예약일시) : " + info.reserveDT + CRLF;
                tmp += "adsYN (광고전송 여부) : " + info.adsYN + CRLF;
                tmp += "imageURL (친구톡 이미지 URL) : " + info.imageURL + CRLF;
                tmp += "sendCnt (전송건수) : " + info.sendCnt + CRLF;
                tmp += "successCnt (성공건수) : " + info.successCnt + CRLF;
                tmp += "failCnt (실패건수) : " + info.failCnt + CRLF;
                tmp += "altCnt (대체문자 건수) : " + info.altCnt + CRLF;
                tmp += "cancelCnt (취소건수) : " + info.cancelCnt + CRLF + CRLF;

                MessageBox.Show(tmp, "전송내역 확인");

                string rowStr =
                    "state (전송상태 코드) | sendDT (전송일시) | result (전송결과 코드) | resultDT (전송결과 수신일시) | contentType (카카오톡 유형) | " +
                    "receiveNum (수신번호) | receiveName (수신자명) | content (내용) | altContentType (대체문자 전송타입) | altSendDT (대체문자 전송일시) | " +
                    "altResult (대체문자 전송결과 코드) | altResultDT (대체문자 전송결과 수신일시) | receiptNum (접수번호) | requestNum (요청번호)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < info.msgs.Count; i++)
                {
                    rowStr = null;
                    rowStr += info.msgs[i].state + " | ";
                    rowStr += info.msgs[i].sendDT + " | ";
                    rowStr += info.msgs[i].receiveNum + " | ";
                    rowStr += info.msgs[i].receiveName + " | ";
                    rowStr += info.msgs[i].content + " | ";
                    rowStr += info.msgs[i].result + " | ";
                    rowStr += info.msgs[i].resultDT + " | ";
                    rowStr += info.msgs[i].altContent + " | ";
                    rowStr += info.msgs[i].altContentType + " | ";
                    rowStr += info.msgs[i].altSendDT + " | ";
                    rowStr += info.msgs[i].altResult + " | ";
                    rowStr += info.msgs[i].altResultDT + " | ";
                    rowStr += info.msgs[i].receiptNum + " | ";
                    rowStr += info.msgs[i].requestNum;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역 확인");
            }
        }

        /*
         * 검색조건에 해당하는 카카오톡 전송내역을 조회합니다. (조회기간 단위 : 최대 2개월)
         * - 카카오톡 접수일시로부터 6개월 이내 접수건만 조회할 수 있습니다.
         * - https://docs.popbill.com/kakao/dotnet/api#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 최대 검색기한 : 6개월 이내
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20210701";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20210730";

            // 전송상태값 배열, 0-대기, 1- 전송중, 2-대기, 3-성공, 4-실패, 5-취소
            String[] State = new String[6];
            State[0] = "0";
            State[1] = "1";
            State[2] = "2";
            State[3] = "3";
            State[4] = "4";
            State[5] = "5";

            // 검색대상 배열, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
            String[] Item = new String[3];
            Item[0] = "ATS";
            Item[1] = "FTS";
            Item[2] = "FMS";

            // 예약여부, 공백-전체조회, 0-일반전송건 조회, 1-예약전송건 조회
            String ReserveYN = "";

            // 개인조회여부 true-개인조회, false-회사조회
            bool SenderYN = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000건
            int PerPage = 100;

            // 조회 검색어, 카카오톡 전송시 기재한 수신자명 입력
            String QString = "";

            listBox1.Items.Clear();
            try
            {
                KakaoSearchResult searchResult = kakaoService.Search(txtCorpNum.Text, SDate, EDate, State,
                    Item, ReserveYN, SenderYN, Order, Page, PerPage, txtUserId.Text, QString);

                String tmp = null;
                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF;

                MessageBox.Show(tmp, "전송내역조회 결과");

                string rowStr =
                    "state (전송상태 코드) | sendDT (전송일시) | result (전송결과 코드) | resultDT (전송결과 수신일시) | contentType (카카오톡 유형) | " +
                    "receiveNum (수신번호) | receiveName (수신자명) | content (내용) | altContentType (대체문자 전송타입) | altSendDT (대체문자 전송일시) | " +
                    "altResult (대체문자 전송결과 코드) | altResultDT (대체문자 전송결과 수신일시) | receiptNum (접수번호) | requestNum (요청번호)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < searchResult.list.Count; i++)
                {
                    rowStr = null;
                    rowStr += searchResult.list[i].state + " | ";
                    rowStr += searchResult.list[i].sendDT + " | ";
                    rowStr += searchResult.list[i].result + " | ";
                    rowStr += searchResult.list[i].resultDT + " | ";
                    rowStr += searchResult.list[i].contentType + " | ";
                    rowStr += searchResult.list[i].receiveNum + " | ";
                    rowStr += searchResult.list[i].receiveName + " | ";
                    rowStr += searchResult.list[i].content + " | ";
                    rowStr += searchResult.list[i].altContentType + " | ";
                    rowStr += searchResult.list[i].altSendDT + " | ";
                    rowStr += searchResult.list[i].altResult + " | ";
                    rowStr += searchResult.list[i].altResultDT + " | ";
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
         * 팝빌 사이트와 동일한 카카오톡 전송내역을 확인하는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetSentListURL
         */
        private void btnGetSentListURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetSentListURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "카카오톡 전송내역 팝업 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "카카오톡 전송내역 팝업 URL");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트(GetPartnerBalance API)를 통해 확인하시기 바랍니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetBalance
         */
        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = kakaoService.GetBalance(txtCorpNum.Text);

                MessageBox.Show("연동회원 잔여포인트 : " + remainPoint.ToString(), "연동회원 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetChargeURL
         */
        private void btnGetChargeURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetChargeURL(txtCorpNum.Text, txtUserId.Text);

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
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetPaymentURL
         */
        private void btnGetPaymentURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPaymentURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "연동회원 포인트 결제내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 결제내역 URL");
            }
        }

        /*
         * 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetUseHistoryURL
         */
        private void btnGetUseHistoryURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetUseHistoryURL(txtCorpNum.Text, txtUserId.Text);

                MessageBox.Show(url, "연동회원 포인트 사용내역 URL");
                textURL.Text = url;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 사용내역 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트(GetBalance API)를 이용하시기 바랍니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetPartnerBalance
         */
        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = kakaoService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show("파트너 잔여포인트 : " + remainPoint.ToString(), "파트너 잔여포인트 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetPartnerURL
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPartnerURL(txtCorpNum.Text, "CHRG");

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
         * 카카오톡(알림톡) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetUnitCost
         */
        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = kakaoService.GetUnitCost(txtCorpNum.Text, KakaoType.ATS);

                MessageBox.Show("전송단가 : " + unitCost.ToString(), "알림톡(ATS) 전송단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송단가");
            }
        }

        /*
         * 카카오톡(FTS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetUnitCost
         */
        private void btnUnitCost_FTS_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = kakaoService.GetUnitCost(txtCorpNum.Text, KakaoType.FTS);

                MessageBox.Show("전송단가 : " + unitCost.ToString(), "친구톡 텍스트(FTS) 전송단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "친구톡 텍스트(FTS) 전송단가");
            }
        }

        /*
         * 카카오톡(FMS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetUnitCost
         */
        private void btn_unitcost_FMS_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = kakaoService.GetUnitCost(txtCorpNum.Text, KakaoType.FMS);

                MessageBox.Show("전송단가 : " + unitCost.ToString(), "친구톡 이미지(FMS) 전송단가");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "친구톡 이미지(FMS) 전송단가");
            }
        }

        /*
         * 팝빌 카카오톡 API 서비스 과금정보를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            //카카오톡 타입, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
            KakaoType kakaoType = KakaoType.ATS;

            try
            {
                ChargeInfo chrgInf = kakaoService.GetChargeInfo(txtCorpNum.Text, kakaoType);

                String tmp = null;

                tmp += "unitCost(전송단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod(과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem(과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, kakaoType.ToString() + " 과금정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "과금정보 확인");
            }
        }

        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * 사용하고자 하는 아이디의 중복여부를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CheckID(txtUserId.Text);

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
         * 사용자를 연동회원으로 가입처리합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#JoinMember
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
            joinInfo.ContactEmail = "test@test.com";

            // 담당자 연락처 (최대 20자)
            joinInfo.ContactTEL = "070-4304-2991";

            // 담당자 휴대폰번호 (최대 20자)
            joinInfo.ContactHP = "010-111-222";

            // 담당자 팩스번호 (최대 20자)
            joinInfo.ContactFAX = "02-6442-9700";

            try
            {
                Response response = kakaoService.JoinMember(joinInfo);

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
         * 팝빌 사이트에 로그인 상태로 접근할 수 있는 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetAccessURL
         */
        private void btnGetAccessURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetAccessURL(txtCorpNum.Text, txtUserId.Text);

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
         * 연동회원 사업자번호에 담당자(팝빌 로그인 계정)를 추가합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#RegistContact
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
            contactInfo.tel = "070-4304-2991";

            //담당자 휴대폰번호 (최대 20자)
            contactInfo.hp = "010-111-222";

            //담당자 팩스번호 (최대 20자)
            contactInfo.fax = "070-4304-2991";

            //담당자 이메일 (최대 100자)
            contactInfo.email = "dev@linkhub.co.kr";

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = kakaoService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보을 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = kakaoService.GetContactInfo(txtCorpNum.Text, contactID, txtUserId.Text);

                String tmp = null;

                tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                tmp += "personName (담당자명) : " + contactInfo.personName + CRLF;
                tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                tmp += "hp (휴대폰번호) : " + contactInfo.hp + CRLF;
                tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole + CRLF;
                tmp += "tel (연락처) : " + contactInfo.tel + CRLF;
                tmp += "fax (팩스번호) : " + contactInfo.fax + CRLF;
                tmp += "mgrYN (관리자 여부) : " + contactInfo.mgrYN + CRLF;
                tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                tmp += "state (상태) : " + contactInfo.state + CRLF;
                tmp += CRLF;

                MessageBox.Show(tmp, "담당자 정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 확인");
            }
        }

        /*
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 목록을 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = kakaoService.ListContact(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;

                foreach (Contact contactInfo in contactList)
                {
                    tmp += "id (담당자 아이디) : " + contactInfo.id + CRLF;
                    tmp += "personName (담당자명) : " + contactInfo.personName + CRLF;
                    tmp += "email (담당자 이메일) : " + contactInfo.email + CRLF;
                    tmp += "hp (휴대폰번호) : " + contactInfo.hp + CRLF;
                    tmp += "searchRole (담당자 권한) : " + contactInfo.searchRole + CRLF;
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
         * 연동회원 사업자번호에 등록된 담당자(팝빌 로그인 계정) 정보를 수정합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#UpdateContact
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

            // 담당자 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = kakaoService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * 연동회원의 회사정보를 확인합니다.
         * - https://docs.popbill.com/kakao/dotnet/api#GetCorpInfo
         */
        private void btnGetCorpInfo_Click(object sender, EventArgs e)
        {
            try
            {
                CorpInfo corpInfo = kakaoService.GetCorpInfo(txtCorpNum.Text, txtUserId.Text);

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
         * - https://docs.popbill.com/kakao/dotnet/api#UpdateCorpInfo
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
                Response response = kakaoService.UpdateCorpInfo(txtCorpNum.Text, corpInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + response.message, "회사정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "회사정보 수정");
            }
        }
    }
}
