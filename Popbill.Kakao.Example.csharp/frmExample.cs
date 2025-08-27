/*
* 팝빌 카카오톡 API .NET SDK C#.NET Example
* C#.NET 연동 튜토리얼 안내 : https://developers.popbill.com/guide/kakaotalk/dotnet/getting-started/tutorial?fwn=csharp
*
* 업데이트 일자 : 2025-08-27
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
* 3) 비즈니스 채널 등록 및 알림톡 템플릿을 신청합니다.
*    - 1. 비즈니스 채널 등록 (등록방법은 사이트/API 두가지 방식이 있습니다.)
*        └ 팝빌 사이트 로그인 [문자/팩스] > [카카오톡] > [카카오톡 관리] > '카카오톡 채널 관리' 메뉴에서 등록
*        └ GetPlusFriendMgtURL API 를 통해 반환된 URL을 이용하여 등록
*    - 2. 알림톡 템플릿 신청 (등록방법은 사이트/API 두가지 방식이 있습니다.)
*        └ 팝빌 사이트 로그인 [문자/팩스] > [카카오톡] > [카카오톡 관리] > '알림톡 템플릿 관리' 메뉴에서 등록
*        └ GetATSTemplateMgtURL API 를 통해 URL을 이용하여 등록.
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

            // 연동환경 설정, true-테스트, false-운영(Production), (기본값:true)
            kakaoService.IsTest = true;

            // 인증토큰 IP 검증 설정, true-사용, false-미사용, (기본값:true)
            kakaoService.IPRestrictOnOff = true;

            // 통신 IP 고정, true-사용, false-미사용, (기본값:false)
            kakaoService.UseStaticIP = false;

            // 로컬시스템 시간 사용여부, true-사용, false-미사용, (기본값:true)
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
         * 비즈니스 채널을 등록하는 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/channel#GetPlusFriendMgtURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "카카오톡채널 계정관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록한 연동회원의 비즈니스 채널 목록을 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/channel#ListPlusFriendID
         */
        private void btnListPlusFriendID_Click(object sender, EventArgs e)
        {
            try
            {
                List<PlusFriend> plusFriendList = kakaoService.ListPlusFriendID(txtCorpNum.Text);

                String tmp = null;

                foreach (PlusFriend friendInfo in plusFriendList)
                {
                    tmp += "검색용 아이디 (plusFriendID) : " + friendInfo.plusFriendID + CRLF;
                    tmp += "채널명 (plusFriendName) : " + friendInfo.plusFriendName + CRLF;
                    tmp += "등록일시 (regDT) : " + friendInfo.regDT + CRLF;
                    tmp += "채널 상태 (state) : " + friendInfo.state + CRLF;
                    tmp += "채널 상태 일시 (stateDT) : " + friendInfo.stateDT + CRLF + CRLF;
                }

                MessageBox.Show(tmp, "카카오톡채널 목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "카카오톡채널 목록 확인");
            }
        }

        /*
         * 카카오톡 발신번호 등록여부를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/sendnum#CheckSenderNumber
         */
        private void btnCheckSenderNumber_Click(object sender, EventArgs e)
        {
            try
            {
                string senderNumber = "";

                Response response = kakaoService.CheckSenderNumber(txtCorpNum.Text, senderNumber);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + (response.code == 1 ? " (등록)" : " (미등록)") + CRLF +
                "응답메시지(message) : " + response.message, "카카오톡 발신번호 등록여부 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "카카오톡 발신번호 등록여부 확인");
            }
        }

        /*
         * 대체문자 전송 발신번호를 등록하는 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/sendnum#GetSenderNumberMgtURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "발신번호 관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록한 연동회원의 카카오톡 발신번호 목록을 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/sendnum#GetSenderNumberList
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
                    tmp += "memo (메모) : " + numInfo.memo + CRLF;
                    tmp += "state (등록상태) : " + numInfo.state + CRLF;
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
         * 알림톡 템플릿 등록하는 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/template#GetATSTemplateMgtURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 관리 팝업 URL");
            }
        }

        /*
         * 승인된 알림톡 템플릿 정보를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/template#GetATSTemplate
         */
        private void btnGetATSTemplate_Click(object sender, EventArgs e)
        {
            // 확인할 템플릿 코드
            String templateCode = "022040000005";

            try
            {
                ATSTemplate templateInfo = kakaoService.GetATSTemplate(txtCorpNum.Text, templateCode);

                String tmp = null;

                tmp += "템플릿 코드 (templateCode) : " + templateInfo.templateCode + CRLF;
                tmp += "템플릿 제목 (templateName) : " + templateInfo.templateName + CRLF;
                tmp += "템플릿 내용 (template) : " + templateInfo.template + CRLF;
                tmp += "검색용 아이디 (plusFriendID) : " + templateInfo.plusFriendID + CRLF;
                tmp += "광고 메시지 (ads) : " + templateInfo.ads + CRLF;
                tmp += "부가 메시지 (appendix) : " + templateInfo.appendix + CRLF;
                tmp += "보안템플릿 여부(sercureYN) : " + templateInfo.secureYN + CRLF;
                tmp += "템플릿 상태(state) : " + templateInfo.state + CRLF;
                tmp += "템플릿 상태 일시(stateDT) : " + templateInfo.stateDT + CRLF;

                if (templateInfo.btns != null)
                {
                    foreach (KakaoButton buttonInfo in templateInfo.btns)
                    {
                        tmp += "[알림톡 버튼 정보]" + CRLF;
                        tmp += "버튼명 (n) : " + buttonInfo.n + CRLF;
                        tmp += "버튼유형 (t) : " + buttonInfo.t + CRLF;
                        tmp += "버튼링크1 (u1) : " + buttonInfo.u1 + CRLF;
                        tmp += "버튼링크2 (u2) : " + buttonInfo.u2 + CRLF;
                        tmp += "아웃링크 (tg) : " + buttonInfo.tg + CRLF;
                    }
                }
                

                MessageBox.Show(tmp, "알림톡 템플릿 정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 정보 확인");
            }
        }

        /*
         * 승인된 알림톡 템플릿 목록을 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/template#ListATSTemplate
         */
        private void btnListATSTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                List<ATSTemplate> templateList = kakaoService.ListATSTemplate(txtCorpNum.Text);

                String tmp = null;

                foreach (ATSTemplate templateInfo in templateList)
                {
                    tmp += "템플릿 코드 (templateCode) : " + templateInfo.templateCode + CRLF;
                    tmp += "템플릿 제목 (templateName) : " + templateInfo.templateName + CRLF;
                    tmp += "템플릿 내용 (template) : " + templateInfo.template + CRLF;
                    tmp += "검색용 아이디 (plusFriendID) : " + templateInfo.plusFriendID + CRLF;
                    tmp += "광고 메시지 (ads) : " + templateInfo.ads + CRLF;
                    tmp += "부가 메시지 (appendix) : " + templateInfo.appendix + CRLF;
                    tmp += "보안템플릿 여부(sercureYN) : " + templateInfo.secureYN + CRLF;
                    tmp += "템플릿 상태(state) : " + templateInfo.state + CRLF;
                    tmp += "템플릿 상태 일시(stateDT) : " + templateInfo.stateDT + CRLF;

                    if (templateInfo.btns != null)
                    {
                        foreach (KakaoButton buttonInfo in templateInfo.btns)
                        {
                            tmp += "[알림톡 버튼 정보]" + CRLF;
                            tmp += "버튼명 (n) : " + buttonInfo.n + CRLF;
                            tmp += "버튼유형 (t) : " + buttonInfo.t + CRLF;
                            tmp += "버튼링크1 (u1) : " + buttonInfo.u1 + CRLF;
                            tmp += "버튼링크2 (u2) : " + buttonInfo.u2 + CRLF;
                            tmp += "아웃링크 (tg) : " + buttonInfo.tg + CRLF;
                        }
                    }

                    

                    tmp += CRLF + CRLF;
                }

                MessageBox.Show(tmp, "알림톡 템플릿 목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 목록 확인");
            }
        }

        /*
         * 승인된 템플릿의 내용을 작성하여 1건의 알림톡 전송을 팝빌에 접수합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendATSOne
         */
        private void btnSendATS_one_Click(object sender, EventArgs e)
        {
            // 승인된 알림톡 템플릿코드
            // └ 알림톡 템플릿 관리 팝업 URL(GetATSTemplateMgtURL API) 함수, 알림톡 템플릿 목록 확인(ListATStemplate API) 함수를 호출하거나
            //   팝빌사이트에서 승인된 알림톡 템플릿 코드를  확인 가능.
            String templateCode = "022040000005";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 알림톡 템플릿 내용, 최대 1000자
            String content = "[ 팝빌 ]\n";
            content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            content += "해당 템플릿으로 전송 가능합니다.\n\n";
            content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            content += "팝빌 파트너센터 : 1600-8536\n";
            content += "support@linkhub.co.kr".Replace("\n", Environment.NewLine);

            // 대체문자 제목
            // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            String altSubject = "대체문자 제목";

            // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
            altContent += "[팝빌]\n";
            altContent += "신청하신 템플릿에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            altContent += "해당 템플릿으로 카카오톡 전송이 가능합니다.\n\n";
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            altContent += "팝빌 파트너센터 : 1600-8536\n";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 수신번호
            String receiverNum = "";

            // 수신자명
            String receiverName = "수신자명";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
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
            
            // 버튼 아웃 링크, 공백-카카오톡 인앱 브라우저, "out"- 디바이스 기본 브라우저(알림톡만 적용)
            btnInfo.tg = ""

            buttons.Add(btnInfo);
            */

            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, altSubject, altSendType,
                    getReserveDT(), receiverNum, receiverName, content, altContent, requestNum, buttons);

                MessageBox.Show("접수번호 : " + receiptNum, "알림톡(ATS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송");
            }
        }

        /*
         * 승인된 템플릿의 내용을 작성하여 다수건의 알림톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendATSMulti
         */
        private void btnSendATS_multi_Click(object sender, EventArgs e)
        {
            // 승인된 알림톡 템플릿코드
            // └ 알림톡 템플릿 관리 팝업 URL(GetATSTemplateMgtURL API) 함수, 알림톡 템플릿 목록 확인(ListATStemplate API) 함수를 호출하거나
            //   팝빌사이트에서 승인된 알림톡 템플릿 코드를  확인 가능.
            String templateCode = "022040000005";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();


            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "";

                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();

                // 알림톡 템플릿 내용, 최대 1000자
                String content = "[ 팝빌 ]\n";
                content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다.\n";
                content += "해당 템플릿으로 전송 가능합니다.\n\n";
                content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
                content += "팝빌 파트너센터 : 1600-8536\n";
                content += "support@linkhub.co.kr".Replace("\n", Environment.NewLine);
                receiverInfo.msg = content;

                // 대체문자 제목
                // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
                receiverInfo.altsjt = "대체문자 제목" + i;

                // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
                // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
                String altMessage = "카카오톡이 실패하여 문자로 전송됩니다.\n";
                altMessage += "[팝빌]\n";
                altMessage += "신청하신 템플릿에 대한 심사가 완료되어 승인 처리되었습니다.\n";
                altMessage += "해당 템플릿으로 카카오톡 전송이 가능합니다.\n\n";
                altMessage += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
                altMessage += "팝빌 파트너센터 : 1600-8536\n";
                receiverInfo.altmsg = altMessage;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20220504-" + i.ToString();

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송");
            }
        }

        /*
         * 승인된 템플릿 내용을 작성하여 다수건의 알림톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendATSSame
         */
        private void btnSendATS_same_Click(object sender, EventArgs e)
        {
            // 승인된 알림톡 템플릿코드
            // └ 알림톡 템플릿 관리 팝업 URL(GetATSTemplateMgtURL API) 함수, 알림톡 템플릿 목록 확인(ListATStemplate API) 함수를 호출하거나
            //   팝빌사이트에서 승인된 알림톡 템플릿 코드를  확인 가능.
            String templateCode = "022040000005";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            String content = "[ 팝빌 ]\n";
            content += "신청하신 #{템플릿코드}에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            content += "해당 템플릿으로 전송 가능합니다.\n\n";
            content += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            content += "팝빌 파트너센터 : 1600-8536\n";
            content += "support@linkhub.co.kr".Replace("\n", Environment.NewLine);

            // 대체문자 제목
            // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            String altSubject = "대체문자 제목";

            // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
            altContent += "[팝빌]\n";
            altContent += "신청하신 템플릿에 대한 심사가 완료되어 승인 처리되었습니다.\n";
            altContent += "해당 템플릿으로 카카오톡 전송이 가능합니다.\n\n";
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            altContent += "팝빌 파트너센터 : 1600-8536\n";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "";

                // 수신자명
                receiverInfo.rcvnm = "수신자명";

                // 파트너 지정키, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20220504-" + i.ToString();

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
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, content, altSubject,
                    altContent, altSendType, getReserveDT(), receivers, txtUserId.Text, requestNum, buttons);

                MessageBox.Show("접수번호 : " + receiptNum, "알림톡(ATS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송");
            }
        }

        /*
         * 텍스트로 구성된 1건의 친구톡 전송을 팝빌에 접수합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFTSOne
         */
        private void btnSendFTS_one_Click(object sender, EventArgs e)
        {
            // 검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 친구톡 내용, 최대 1000자
            String content = "[팝빌]\n";
            content += "알림톡이 아닌 친구톡입니다.\n";
            content += "템플릿을 등록하지 않아도 전송할 수 있습니다.\n";
            content += "하지만 채널이 친구로 등록되어 있지 않으면 친구톡이 전송되지 않습니다.\n";

            // 수신번호
            String receiverNum = "";

            // 수신자명
            String receiverName = "수신자명";

            // 대체문자 제목
            // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            String altSubject = "대체문자 제목";

            // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
            altContent += "[팝빌]\n";
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다.\n";
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다.\n\n";
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            altContent += "팝빌 파트너센터 : 1600-8536\n";


            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 알림톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "C";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
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
                string receiptNum = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject,
                    altContent, altSendType, receiverNum, receiverName, adsYN, getReserveDT(), buttons, txtUserId.Text,
                    requestNum);

                MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
            }
        }

        /*
         * 텍스트로 구성된 다수건의 친구톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFTSMulti
         */
        private void btnSendFTS_multi_Click(object sender, EventArgs e)
        {
            // 검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 친구톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "";

                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();

                // 친구톡 내용
                receiverInfo.msg = "개별 친구톡 내용" + i.ToString();

                // 대체문자 제목
                // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
                receiverInfo.altsjt = "대체문자 제목" + i;

                // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
                // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
                String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
                altContent += "[팝빌]\n";
                altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다.\n";
                altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다.\n\n";
                altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
                altContent += "팝빌 파트너센터 : 1600-8536\n";
                receiverInfo.altmsg = altContent;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20220504-" + i.ToString();

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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
            }
        }

        /*
         * 텍스트로 구성된 다수건의 친구톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFTSSame
         */
        private void btnSendFTS_same_Click(object sender, EventArgs e)
        {
            // 검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 친구톡내용, 최대 1000자
            String content = "친구톡 내용";

            // 대체문자 제목
            // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            String altSubject = "대체문자 제목";

            // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
            altContent += "[팝빌]\n";
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다.\n";
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다.\n\n";
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            altContent += "팝빌 파트너센터 : 1600-8536\n";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 친구톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();
                receiverInfo.rcv = "";
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
                string receiptNum = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject,
                    altContent, altSendType, adsYN, getReserveDT(), receivers, buttons, txtUserId.Text, requestNum);

                MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                txtReceiptNum.Text = receiptNum;
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
            }
        }

        /*
         * 이미지가 첨부된 1건의 친구톡 전송을 팝빌에 접수합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFMSOne
         */
        private void btnSendFMS_one_Click(object sender, EventArgs e)
        {
            // 검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 친구톡 내용, 최대 400자
            String content = "친구톡 내용";

            // 수신번호
            String receiverNum = "";

            // 수신자명
            String receiverName = "수신자명";

            // 대체문자 제목
            // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            String altSubject = "대체문자 제목";

            // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
            altContent += "[팝빌]\n";
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다.\n";
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다.\n\n";
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            altContent += "팝빌 파트너센터 : 1600-8536\n";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 친구톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
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

            // 이미지 링크 URL
            // └ 수신자가 친구톡 상단 이미지 클릭시 호출되는 URL
            // - 미입력시 첨부된 이미지를 링크 기능 없이 표시
            String imageURL = "http://www.popbill.com";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    string receiptNum = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject,
                        altContent, altSendType, receiverNum, receiverName, adsYN, getReserveDT(), buttons,
                        strFileName, imageURL, txtUserId.Text, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
                }
            }
        }

        /*
         * 이미지가 첨부된 다수건의 친구톡 전송을 팝빌에 접수하며, 수신자 별로 개별 내용을 전송합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFMSMulti
         */
        private void btnSendFMS_multi_Click(object sender, EventArgs e)
        {
            // 검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 친구톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();

                // 수신번호
                receiverInfo.rcv = "";

                // 수신자명
                receiverInfo.rcvnm = "수신자명" + i.ToString();

                // 친구톡내용, 최대 400자
                receiverInfo.msg = "개별 친구톡 내용" + i.ToString();

                // 대체문자 제목
                // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
                receiverInfo.altsjt = "대체문자 제목";

                // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
                // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
                String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
                altContent += "[팝빌]\n";
                altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다.\n";
                altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다.\n\n";
                altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
                altContent += "팝빌 파트너센터 : 1600-8536\n";
                receiverInfo.altmsg = altContent;

                // 파트너 지정키, 대량전송시, 수신자 구별용 메모
                receiverInfo.interOPRefKey = "20220504-" + i.ToString();

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

            // 이미지 링크 URL
            // └ 수신자가 친구톡 상단 이미지 클릭시 호출되는 URL
            // - 미입력시 첨부된 이미지를 링크 기능 없이 표시
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
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
                }
            }
        }

        /*
         * 이미지가 첨부된 다수건의 친구톡 전송을 팝빌에 접수하며, 모든 수신자에게 동일 내용을 전송합니다. (최대 1,000건)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#SendFMSSame
         */
        private void btnSendFMS_same_Click(object sender, EventArgs e)
        {
            // 검색용 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            // ※ 대체문자를 전송하는 경우에만 필수 입력
            String senderNum = "";

            // 친구톡 내용, 최대 400자
            String content = "친구톡 내용";

            // 대체문자 제목
            // - 메시지 길이(90byte)에 따라 장문(LMS)인 경우에만 적용.
            String altSubject = "대체문자 제목";

            // 대체문자 유형(altSendType)이 "A"일 경우, 대체문자로 전송할 내용 (최대 2000byte)
            // └ 팝빌이 메시지 길이에 따라 단문(90byte 이하) 또는 장문(90byte 초과)으로 전송처리
            String altContent = "카카오톡이 실패하여 문자로 전송됩니다.\n";
            altContent += "[팝빌]\n";
            altContent += "친구톡을 접수하였으나 실패하여 문자로 전송되었습니다.\n";
            altContent += "채널이 친구로 등록되어 있는지 확인해 주시길 바랍니다.\n\n";
            altContent += "문의사항 있으시면 파트너센터로 편하게 연락주시기 바랍니다.\n\n";
            altContent += "팝빌 파트너센터 : 1600-8536\n";

            // 대체문자 유형 (null , "C" , "A" 중 택 1)
            // null = 미전송, C = 친구톡과 동일 내용 전송 , A = 대체문자 내용(altContent)에 입력한 내용 전송
            String altSendType = "A";

            // 광고성 메시지 여부 ( true , false 중 택 1)
            // └ true = 광고 , false = 일반
            // - 미입력 시 기본값 false 처리
            Boolean adsYN = false;

            // 전송요청번호
            // 팝빌이 접수 단위를 식별할 수 있도록 파트너가 할당하는 식별번호.
            // 1~36자리로 구성. 영문, 숫자, 하이픈(-), 언더바(_)를 조합하여 팝빌 회원별로 중복되지 않도록 할당.
            String requestNum = "";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 2; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();
                // 수신번호
                receiverInfo.rcv = "";
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

            // 이미지 링크 URL
            // └ 수신자가 친구톡 상단 이미지 클릭시 호출되는 URL
            // - 미입력시 첨부된 이미지를 링크 기능 없이 표시
            String imageURL = "http://www.popbill.com";

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFileName = fileDialog.FileName;

                try
                {
                    string receiptNum = kakaoService.SendFMS(txtCorpNum.Text, plusFriendID, senderNum, content, altSubject,
                        altContent, altSendType, adsYN, getReserveDT(), receivers, buttons, strFileName, imageURL,
                        txtUserId.Text, requestNum);

                    MessageBox.Show("접수번호 : " + receiptNum, "친구톡(FTS) 전송");

                    txtReceiptNum.Text = receiptNum;
                }
                catch (PopbillException ex)
                {
                    MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                    "응답메시지(message) : " + ex.Message, "친구톡(FTS) 전송");
                }
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호로 예약된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserve
         */
        private void btnCacnelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CancelReserve(txtCorpNum.Text, txtReceiptNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약전송 취소");
            }
        }

        /*
         * 파트너가 할당한 요청번호로 예약된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserveRN
         */
        private void btnCancelReserveRN_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CancelReserveRN(txtCorpNum.Text, txtRequestNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약전송 취소");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약전송 취소");
            }
        }

        /*
        * 팝빌에서 반환받은 접수번호로 접수 건을 식별하여 수신번호에 예약된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
        * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReservebyRCV
        */
        private void btnCancelReservebyRCV_Click(object sender, EventArgs e)
        {

            // 전송 접수번호
            String receiptNum = "";

            // 수신번호
            String receiveNum = "";

            try
            {
                Response response = kakaoService.CancelReservebyRCV(txtCorpNum.Text, receiptNum, receiveNum, txtReceiptNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약전송 일부 취소 (접수번호)");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약전송 일부 취소 (접수번호)");
            }
        }

        /*
        * 파트너가 할당한 요청번호로 접수 건을 식별하여 수신번호에 예약된 카카오톡을 전송 취소합니다. (예약시간 10분 전까지 가능)
        * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/send#CancelReserveRNbyRCV
        */
        private void btnCancelReserveRNbyRCV_Click(object sender, EventArgs e)
        {
            // 전송 요청번호
            String requestNum = "";

            // 수신번호
            String receiveNum = "";

            try
            {
                Response response = kakaoService.CancelReserveRNbyRCV(txtCorpNum.Text, requestNum, receiveNum, txtReceiptNum.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "예약전송 일부 취소 (전송 요청번호)");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "예약전송 일부 취소 (전송 요청번호)");
            }
        }

        /*
         * 팝빌에서 반환받은 접수번호를 통해 알림톡/친구톡 전송상태 및 결과를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#GetMessages
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
                tmp += "plusFriendID (검색용 아이디) : " + info.plusFriendID + CRLF;
                tmp += "sendNum (발신번호) : " + info.sendNum + CRLF;
                tmp += "altSubject (대체문자 제목) : " + info.altSubject + CRLF;
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
                    "state (상태코드) | sendDT (전송일시) | result (카카오 결과코드) | resultDT (전송결과 수신일시) | contentType (카카오톡 유형) | receiveNum (수신번호) | " +
                    "receiveName (수신자명) | content (내용) | altSubject (대체문자 제목) | altContent(대체문자 내용) | altContentType (대체문자 전송타입) | altSendDT (대체문자 전송일시) | " +
                    "altResult (대체문자 전송결과 코드) | altResultDT (대체문자 전송결과 수신일시) | receiptNum (접수번호) | requestNum (요청번호) | interOPRefKey (파트너 지정키)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < info.msgs.Count; i++)
                {
                    rowStr = null;
                    rowStr += info.msgs[i].state + " | ";
                    rowStr += info.msgs[i].sendDT + " | ";
                    rowStr += info.msgs[i].result + " | ";
                    rowStr += info.msgs[i].resultDT + " | ";
                    rowStr += info.msgs[i].contentType + " | ";
                    rowStr += info.msgs[i].receiveNum + " | ";
                    rowStr += info.msgs[i].receiveName + " | ";
                    rowStr += info.msgs[i].content + " | ";
                    rowStr += info.msgs[i].altSubject + " | ";
                    rowStr += info.msgs[i].altContent + " | ";
                    rowStr += info.msgs[i].altContentType + " | ";
                    rowStr += info.msgs[i].altSendDT + " | ";
                    rowStr += info.msgs[i].altResult + " | ";
                    rowStr += info.msgs[i].altResultDT + " | ";
                    rowStr += info.msgs[i].receiptNum + " | ";
                    rowStr += info.msgs[i].requestNum + " | ";
                    rowStr += info.msgs[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "전송내역 확인");
            }
        }

        /*
         * 파트너가 할당한 요청번호를 통해 알림톡/친구톡 전송상태 및 결과를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#GetMessagesRN
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
                tmp += "plusFriendID (검색용 아이디) : " + info.plusFriendID + CRLF;
                tmp += "sendNum (발신번호) : " + info.sendNum + CRLF;
                tmp += "altSubject (대체문자 제목) : " + info.altSubject + CRLF;
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
                    "state (상태코드) | sendDT (전송일시) | result (카카오 결과코드) | resultDT (전송결과 수신일시) | contentType (카카오톡 유형) | receiveNum (수신번호) | " +
                    "receiveName (수신자명) | content (내용) | altSubject (대체문자 제목) | altContent(대체문자 내용) | altContentType (대체문자 전송타입) | altSendDT (대체문자 전송일시) | " +
                    "altResult (대체문자 전송결과 코드) | altResultDT (대체문자 전송결과 수신일시) | receiptNum (접수번호) | requestNum (요청번호) | interOPRefKey (파트너 지정키)";

                listBox1.Items.Add(rowStr);

                for (int i = 0; i < info.msgs.Count; i++)
                {
                    rowStr = null;
                    rowStr += info.msgs[i].state + " | ";
                    rowStr += info.msgs[i].sendDT + " | ";
                    rowStr += info.msgs[i].result + " | ";
                    rowStr += info.msgs[i].resultDT + " | ";
                    rowStr += info.msgs[i].contentType + " | ";
                    rowStr += info.msgs[i].receiveNum + " | ";
                    rowStr += info.msgs[i].receiveName + " | ";
                    rowStr += info.msgs[i].content + " | ";
                    rowStr += info.msgs[i].altSubject + " | ";
                    rowStr += info.msgs[i].altContent + " | ";
                    rowStr += info.msgs[i].altContentType + " | ";
                    rowStr += info.msgs[i].altSendDT + " | ";
                    rowStr += info.msgs[i].altResult + " | ";
                    rowStr += info.msgs[i].altResultDT + " | ";
                    rowStr += info.msgs[i].receiptNum + " | ";
                    rowStr += info.msgs[i].requestNum + " | ";
                    rowStr += info.msgs[i].interOPRefKey;

                    listBox1.Items.Add(rowStr);
                }
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "전송내역 확인");
            }
        }

        /*
         * 검색조건에 해당하는 카카오톡 전송내역을 조회합니다. (조회기간 단위 : 최대 2개월)
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#Search
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 최대 검색기한 : 6개월 이내
            // 시작일자, 날짜형식(yyyyMMdd)
            String SDate = "20250701";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20250731";

            // 전송상태 배열 ("0" , "1" , "2" , "3" , "4" , "5" 중 선택, 다중 선택 가능)
            // └ 0 = 전송대기 , 1 = 전송중 , 2 = 전송성공 , 3 = 대체문자 전송 , 4 = 전송실패 , 5 = 전송취소
            // - 미입력 시 전체조회
            String[] State = new String[6];
            State[0] = "0";
            State[1] = "1";
            State[2] = "2";
            State[3] = "3";
            State[4] = "4";
            State[5] = "5";

            // 검색대상 배열 ("ATS", "FTS", "FMS" 중 선택, 다중 선택 가능)
            // └ ATS = 알림톡 , FTS = 친구톡(텍스트) , FMS = 친구톡(이미지)
            // - 미입력 시 전체조회
            String[] Item = new String[3];
            Item[0] = "ATS";
            Item[1] = "FTS";
            Item[2] = "FMS";

            // 전송유형별 조회 (null , "0" , "1" 중 택 1)
            // └ null = 전체 , 0 = 즉시전송건 , 1 = 예약전송건
            // - 미입력 시 전체조회
            String ReserveYN = "";

            // 사용자권한별 조회 (true / false 중 택 1)
            // └ false = 접수한 카카오톡 전체 조회 (관리자권한)
            // └ true = 해당 담당자 계정으로 접수한 카카오톡만 조회 (개인권한)
            // - 미입력시 기본값 false 처리
            bool SenderYN = false;

            // 정렬방향, A-오름차순, D-내림차순
            String Order = "D";

            // 페이지 번호
            int Page = 1;

            // 페이지당 검색개수, 최대 1000건
            int PerPage = 100;

            // 조회하고자 하는 수신자명
            // - 미입력시 전체조회
            String QString = "";

            listBox1.Items.Clear();
            try
            {
                KakaoSearchResult searchResult = kakaoService.Search(txtCorpNum.Text, SDate, EDate, State,
                    Item, ReserveYN, SenderYN, Order, Page, PerPage, txtUserId.Text, QString);

                String tmp = null;
                tmp += "code (응답코드) : " + searchResult.code + CRLF;
                tmp += "message (응답메시지) : " + searchResult.message + CRLF;
                tmp += "total (총 검색결과 건수) : " + searchResult.total + CRLF;
                tmp += "perPage (페이지당 검색개수) : " + searchResult.perPage + CRLF;
                tmp += "pageNum (페이지 번호) : " + searchResult.pageNum + CRLF;
                tmp += "pageCount (페이지 개수) : " + searchResult.pageCount + CRLF;

                MessageBox.Show(tmp, "전송내역조회 결과");

                string rowStr =
                    "state (전송상태 코드) | sendDT (전송일시) | result (전송결과 코드) | resultDT (전송결과 수신일시) | contentType (카카오톡 유형) | receiveNum (수신번호) | " +
                    "receiveName (수신자명) | content (내용) | altSubject (대체문자 제목) | altContent(대체문자 내용) | altContentType (대체문자 전송타입) | altSendDT (대체문자 전송일시) | " +
                    "altResult (대체문자 전송결과 코드) | altResultDT (대체문자 전송결과 수신일시) | receiptNum (접수번호) | requestNum (요청번호) | interOPRefKey (파트너 지정키)";

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
                    rowStr += searchResult.list[i].altSubject + " | ";
                    rowStr += searchResult.list[i].altContent + " | ";
                    rowStr += searchResult.list[i].altContentType + " | ";
                    rowStr += searchResult.list[i].altSendDT + " | ";
                    rowStr += searchResult.list[i].altResult + " | ";
                    rowStr += searchResult.list[i].altResultDT + " | ";
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
         * 카카오톡 전송내역 팝업 URL을 반환합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/api/info#GetSentListURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "카카오톡 전송내역 팝업 URL");
            }
        }

        /*
         * 연동회원의 잔여포인트를 확인합니다.
         * - 과금방식이 파트너과금인 경우 파트너 잔여포인트 확인(GetPartnerBalance API) 함수를 통해 확인하시기 바랍니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 잔여포인트 확인");
            }
        }

        /*
         * 연동회원 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetChargeURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "포인트 충전 팝업 URL");
            }
        }

        /*
         * 연동회원 포인트 결제내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetPaymentURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 결제내역 URL");
            }
        }

        /*
         * 연동회원 포인트 사용내역 확인을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetUseHistoryURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "연동회원 포인트 사용내역 URL");
            }
        }

        /*
         * 파트너의 잔여포인트를 확인합니다.
         * - 과금방식이 연동과금인 경우 연동회원 잔여포인트 확인(GetBalance API) 함수를 이용하시기 바랍니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetPartnerBalance
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "파트너 잔여포인트 확인");
            }
        }

        /*
         * 파트너 포인트 충전을 위한 페이지의 팝업 URL을 반환합니다.
         * - 반환되는 URL은 보안 정책상 30초 동안 유효하며, 시간을 초과한 후에는 해당 URL을 통한 페이지 접근이 불가합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetPartnerURL
         */
        private void btnGetPartnerURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPartnerURL(txtCorpNum.Text, "CHRG");

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
         * 카카오톡(알림톡) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "알림톡(ATS) 전송단가");
            }
        }

        /*
         * 카카오톡(FTS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "친구톡 텍스트(FTS) 전송단가");
            }
        }

        /*
         * 카카오톡(FMS) 전송시 과금되는 포인트 단가를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetUnitCost
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "친구톡 이미지(FMS) 전송단가");
            }
        }

        /*
         * 팝빌 카카오톡 API 서비스 과금정보를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetChargeInfo
         */
        private void btnGetChargeInfo_Click(object sender, EventArgs e)
        {
            //카카오톡 타입, ATS-알림톡, FTS-친구톡 텍스트, FMS-친구톡 이미지
            KakaoType kakaoType = KakaoType.ATS;

            try
            {
                ChargeInfo chrgInf = kakaoService.GetChargeInfo(txtCorpNum.Text, kakaoType);

                String tmp = null;

                tmp += "unitCost (전송단가) : " + chrgInf.unitCost + CRLF;
                tmp += "chargeMethod (과금유형) : " + chrgInf.chargeMethod + CRLF;
                tmp += "rateSystem (과금제도) : " + chrgInf.rateSystem + CRLF;

                MessageBox.Show(tmp, kakaoType.ToString() + " 과금정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "과금정보 확인");
            }
        }

        /*
         * 사업자번호를 조회하여 연동회원 가입여부를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#CheckIsMember
         */
        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CheckIsMember(txtCorpNum.Text, LinkID);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#CheckID
         */
        private void btnCheckID_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = kakaoService.CheckID(txtUserId.Text);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#JoinMember
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
                Response response = kakaoService.JoinMember(joinInfo);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#GetAccessURL
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 연동회원의 회사정보를 확인합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#GetCorpInfo
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
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "회사정보 조회");
            }
        }

        /*
         * 연동회원의 회사정보를 수정합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#UpdateCorpInfo
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#RegistContact
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            //담당자 아이디, 6자 이상 50자 미만
            contactInfo.id = "testkorea";

            // 담당자 비밀번호, 8자 이상 20자 이하(영문, 숫자, 특수문자 조합)
            contactInfo.Password = "asdf8536!@#";

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자명";

            // 담당자 휴대폰 (최대 20자)
            contactInfo.tel = "";

            // 담당자 메일 (최대 100자)
            contactInfo.email = "";

            // 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = kakaoService.RegistContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#GetContactInfo
         */
        private void btnGetContactInfo_Click(object sender, EventArgs e)
        {
            // 확인할 담당자 아이디
            String contactID = "DOTNET_CONTACT_PASS_TEST19";

            try
            {
                Contact contactInfo = kakaoService.GetContactInfo(txtCorpNum.Text, contactID);

                String tmp = null;

                tmp += "id (아이디) : " + contactInfo.id + CRLF;
                tmp += "personName (담당자 성명) : " + contactInfo.personName + CRLF;
                tmp += "tel (담당자 휴대폰) : " + contactInfo.tel + CRLF;
                tmp += "email (담당자 메일) : " + contactInfo.email + CRLF;
                tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                tmp += "searchRole (권한) : " + contactInfo.searchRole + CRLF;
                tmp += "mgrYN (역할) : " + contactInfo.mgrYN + CRLF;
                tmp += "state (계정상태) : " + contactInfo.state + CRLF;
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#ListContact
         */
        private void btnListContact_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> contactList = kakaoService.ListContact(txtCorpNum.Text, txtUserId.Text);

                string tmp = null;

                foreach (Contact contactInfo in contactList)
                {
                    tmp += "id (아이디) : " + contactInfo.id + CRLF;
                    tmp += "personName (담당자 성명) : " + contactInfo.personName + CRLF;
                    tmp += "tel (담당자 휴대폰) : " + contactInfo.tel + CRLF;
                    tmp += "email (담당자 메일) : " + contactInfo.email + CRLF;
                    tmp += "regDT (등록일시) : " + contactInfo.regDT + CRLF;
                    tmp += "searchRole (권한) : " + contactInfo.searchRole + CRLF;
                    tmp += "mgrYN (역할) : " + contactInfo.mgrYN + CRLF;
                    tmp += "state (계정상태) : " + contactInfo.state + CRLF;
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#UpdateContact
         */
        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = txtUserId.Text;

            // 담당자 성명 (최대 100자)
            contactInfo.personName = "담당자123";

            // 담당자 휴대폰 (최대 20자)
            contactInfo.tel = "";

            // 담당자 메일 (최대 100자)
            contactInfo.email = "";

            // 권한, 1 : 개인권한, 2 : 읽기권한, 3 : 회사권한
            contactInfo.searchRole = 3;

            try
            {
                Response response = kakaoService.UpdateContact(txtCorpNum.Text, contactInfo, txtUserId.Text);

                MessageBox.Show("응답코드(code) : " + response.code.ToString() + CRLF +
                                "응답메시지(message) : " + response.message, "담당자 정보 수정");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + CRLF +
                                "응답메시지(message) : " + ex.Message, "담당자 정보 수정");
            }
        }

        /**
         * 연동회원 포인트 충전을 위해 무통장입금을 신청합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#PaymentRequest
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
                PaymentResponse response = kakaoService.PaymentRequest(CorpNum, PaymentForm, UserID);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetPaymentHistory
         */
        public void btnGetPaymentHistory_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 조회 시작 일자
            String SDate = "20250701";

            // 조회 종료 일자
            String EDate = "20250731";

            // 목록 페이지 번호
            int Page = 1;

            // 페이지당 목록 개수
            int PerPage = 500;

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                PaymentHistoryResult result =
                    kakaoService.GetPaymentHistory(CorpNum, SDate, EDate, Page, PerPage, UserID);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetSettleResult
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
                    kakaoService.GetSettleResult(CorpNum, SettleCode, UserID);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetUseHistory
         */
        public void btnGetUseHistory_Click(object sender, EventArgs e)
        {
            // 팝빌 회원 아이디
            String CorpNum = "1234567890";

            // 조회 시작 일자
            String SDate = "20250701";

            // 조회 종료 일자
            String EDate = "20250731";

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
                    kakaoService.GetUseHistory(CorpNum, SDate, EDate, Page, PerPage, Order, UserID);

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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#Refund
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
                RefundResponse result = kakaoService.Refund(CorpNum, refundForm, UserID);
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetRefundHistory
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
                RefundHistoryResult result = kakaoService.GetRefundHistory(CorpNum, Page, PerPage, UserID);
                String tmp = "";

                foreach (RefundHistory history in result.list)
                {
                    tmp += "reqDT (신청일시) :" + history.reqDT + CRLF ;
                    tmp += "requestPoint (환불 신청포인트) :" + history.requestPoint + CRLF ;
                    tmp += "accountBank (환불계좌 은행명) :" + history.accountBank + CRLF ;
                    tmp += "accountNum (환불계좌번호) :" + history.accountNum + CRLF ;
                    tmp += "accountName (환불계좌 예금주명) :" + history.accountName + CRLF ;
                    tmp += "state (상태) : " + history.state.ToString() + CRLF ;
                    tmp += "reason (환불사유) : " + history.reason + CRLF ;
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetRefundInfo
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
                RefundHistory result = kakaoService.GetRefundInfo(CorpNum, RefundCode, UserID);
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/point#GetRefundableBalance
         */
        public void btnGetRefundableBalance_Click(object sender, EventArgs e)
        {
            // 팝빌회원 사업자번호
            String CorpNum = "1234567890";

            // 팝빌 회원 아이디
            String UserID = "testkorea";

            try
            {
                Double refundableBalance = kakaoService.GetRefundableBalance(CorpNum, UserID);
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
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#QuitMember
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
                Response response = kakaoService.QuitMember(CorpNum, QuitReason, UserID);
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
         * 연동회원에 추가된 담당자를 삭제합니다.
         * - https://developers.popbill.com/reference/kakaotalk/dotnet/common-api/member#DeleteContact
         */

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            String ContactID = "testkorea20250722_01";

            try
            {
                Response response = kakaoService.DeleteContact(txtCorpNum.Text, ContactID, txtUserId.Text);

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
