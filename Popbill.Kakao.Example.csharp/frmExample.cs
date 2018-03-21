﻿
/*
 * 팝빌 카카오톡 API DotNet SDK Example
 * 
 * - DotNet SDK 연동환경 설정방법 안내 : [개발가이드] - http://blog.linkhub.co.kr/587
 * - 업데이트 일자 : 2018-03-21
 * - 연동 기술지원 연락처 : 1600-9854 / 070-4304-2991~2
 * - 연동 기술지원 이메일 : code@linkhub.co.kr
 * 
 * <테스트 연동개발 준비사항>
 * 1) 30, 33 라인에 선언된 링크아이디(LinkID)와 비밀키(SecretKey)를 
 *    링크허브 가입시 메일로 발급받은 인증정보로 변경합니다.
 *    
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

        /*
         * 해당사업자의 회원가입 여부를 확인합니다.
         * - 사업자번호는 '-'를 제외한 10자리 숫자 문자열입니다.
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
                                "응답메시지(message) : " + ex.Message, "연동회원 가이병부 확인");

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

            // 팝빌회원 아이디
            joinInfo.ID = "userid";

            // 팝빌회원 비밀번호
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
        * 알림톡(ATS) 전송단가를 확인합니다.
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
        * 친구톡 텍스트(FTS) 전송단가를 확인합니다.
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
        * 친구톡 이미지(FMS) 전송단가를 확인합니다.
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
         * 카카오톡 API 서비스 과금정보를 확인합니다.
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

                MessageBox.Show(tmp, kakaoType.ToString()+" 과금정보 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "단문(SMS) 과금정보 확인");
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
         * 팝빌 포인트충전 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetPopbillURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "CHRG");

                MessageBox.Show(url, "포인트 충전 팝업 URL");

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
         * 파트너 포인트 충전 팝업 URL을 반환합니다. 
         * - 반환된 URL은 보안정책상 30초의 유효시간을 갖습니다.
         */
        private void btnGetPartnerURL_CHRG_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPartnerURL(txtCorpNum.Text, "CHRG");

                MessageBox.Show(url, "파트너 포인트충전 URL");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 팝빌 로그인 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다 
         */
        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, "LOGIN");

                MessageBox.Show(url, "팝빌 로그인 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "팝빌 로그인 URL");
            }
        }

        /*
         * 연동회원의 담당자를 추가합니다.
         */
        private void btnRegistContact_Click(object sender, EventArgs e)
        {
            Contact contactInfo = new Contact();

            // 담당자 아이디
            contactInfo.id = "userid";

            // 비밀번호
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
         * 연동회원의 담당자 목록을 확인합니다.
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
         * 회사정보를 조회합니다.
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


        /*
         * 알림톡 전송을 요청합니다. 
         */
        private void btnSendATS_one_Click(object sender, EventArgs e)
        {
            // 알림톡 템플릿 코드, ListATSTemplate API의 templateCode 확인
            String templateCode = "018020000001";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";
            
            // 알림톡 템플릿 내용, 최대 1000자
            String content = "[테스트] 테스트 템플릿입니다.";

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
            
            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, altSendType, 
                    getReserveDT(), receiverNum, receiverName, content, altContent, requestNum);

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
         * 알림톡 동일내용 대량전송을 요청합니다. 
         */
        private void btnSendATS_same_Click(object sender, EventArgs e)
        {
            // 알림톡 템플릿 코드, ListATSTemplate API의 templateCode 확인
            String templateCode = "018020000001";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

            // 알림톡 템플릿 내용, 최대 1000자
            String content = "[테스트] 테스트 템플릿입니다.";

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
                receivers.Add(receiverInfo);
            }

            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, content,
                    altContent, altSendType, getReserveDT(), receivers, txtUserId.Text, requestNum);

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
         * 알림톡 개별내용 대량전송을 요청합니다. 
         */
        private void btnSendATS_multi_Click(object sender, EventArgs e)
        {
            // 알림톡 템플릿 코드, ListATSTemplate API의 templateCode 확인
            String templateCode = "018020000001";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "A";

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "04";

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
                receiverInfo.msg = "[테스트] 테스트 템플릿입니다." + i.ToString();
                // 대체문자 내용
                receiverInfo.altmsg = "대체문자 내용입니다";
                receivers.Add(receiverInfo);
            }

            try
            {
                string receiptNum = kakaoService.SendATS(txtCorpNum.Text, templateCode, senderNum, 
                    altSendType, getReserveDT(), receivers, txtUserId.Text, requestNum);

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
         * 친구톡 1건 전송을 요청합니다. 
         */
        private void btnSendFTS_one_Click(object sender, EventArgs e)
        {
            // 플러스친구 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

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
            String requestNum = "04";

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형
            btnInfo.t = "WL";
            // 버튼링크1
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2
            btnInfo.u2 = "http://test.popbill.com";
            buttons.Add(btnInfo);

            try
            {
                string receiptNum = kakaoService.SendFTS(txtCorpNum.Text, plusFriendID, senderNum, content, 
                    altContent, altSendType, receiverNum, receiverName, adsYN, getReserveDT(), buttons, txtUserId.Text, requestNum);

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
         * 친구톡 동일내용 대량전송을 요청합니다. 
         */
        private void btnSendFTS_same_Click(object sender, EventArgs e)
        {
            // 플러스친구 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";
            
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
            String requestNum = "05";

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
            // 버튼유형
            btnInfo.t = "WL";
            // 버튼링크1
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2
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
         * 친구톡 개별내용 대량전송을 요청합니다. 
         */
        private void btnSendFTS_multi_Click(object sender, EventArgs e)
        {
            // 플러스친구 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

            // 대체문자 유형, 공백-미전송, C-알림톡 내용, A-대체문자 내용
            String altSendType = "C";

            // 광고전송여부
            Boolean adsYN = false;

            // 전송요청번호, 파트너가 전송요청에 대한 관리번호를 직접 할당하여 관리하는 경우 기재
            // 최대 36자리, 영문, 숫자, 언더바('_'), 하이픈('-')을 조합하여 사업자별로 중복되지 않도록 구성
            String requestNum = "06";

            // 수신자정보 배열, 최대 1000건
            List<KakaoReceiver> receivers = new List<KakaoReceiver>();
            for (int i = 0; i < 5; i++)
            {
                KakaoReceiver receiverInfo = new KakaoReceiver();
                receiverInfo.rcv = "010111222";
                receiverInfo.rcvnm = "수신자명" + i.ToString();
                receiverInfo.msg = "개별 친구톡 내용" + i.ToString();
                receiverInfo.altmsg = "대체문자 전송내용" + i.ToString();
                receivers.Add(receiverInfo);
            }

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형
            btnInfo.t = "WL";
            // 버튼링크1
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2
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
         * 친구톡(이미지) 1건 전송을 요청합니다. 
         */
        private void btnSendFMS_one_Click(object sender, EventArgs e)
        {
            // 플러스친구 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

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
            // 버튼유형
            btnInfo.t = "WL";
            // 버튼링크1
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2
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
         * 친구톡(이미지) 동일내용 대량전송을 요청합니다. 
         */
        private void btnSendFMS_same_Click(object sender, EventArgs e)
        {
            // 플러스친구 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

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
            // 버튼유형
            btnInfo.t = "WL";
            // 버튼링크1
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2
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
         * 친구톡(이미지) 개별내용 대량전송을 요청합니다. 
         */
        private void btnSendFMS_multi_Click(object sender, EventArgs e)
        {
            // 플러스친구 아이디, ListPlusFriendID API 의 plusFriendID 참고
            String plusFriendID = "@팝빌";

            // 팝빌에 사전 등록된 발신번호
            String senderNum = "07043042993";

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
                receivers.Add(receiverInfo);
            }

            // 버튼배열, 최대 5개
            List<KakaoButton> buttons = new List<KakaoButton>();
            KakaoButton btnInfo = new KakaoButton();
            // 버튼명
            btnInfo.n = "버튼이름";
            // 버튼유형
            btnInfo.t = "WL";
            // 버튼링크1
            btnInfo.u1 = "http://www.popbill.com";
            // 버튼링크2
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
         * 플러스친구 계정관리 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_PLUSFRIEND_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "PLUSFRIEND");
                MessageBox.Show(url, "플러스친구 계정관리 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "플러스친구 계정관리 팝업 URL");
            }
        }

        /*
         * 팝빌에 등록된 플러스친구 계정 목록을 반환합니다. 
         */
        private void btnListPlusFriendID_Click(object sender, EventArgs e)
        {
            try
            {
                List<PlusFriend> plusFriendList= kakaoService.ListPlusFriendID(txtCorpNum.Text);

                String tmp = null;

                foreach (PlusFriend friendInfo in plusFriendList)
                {
                    tmp += "플러스친구 아이디(plusFriendID) : " + friendInfo.plusFriendID + CRLF;
                    tmp += "플러스친구 이름(plusFriendName) : " + friendInfo.plusFriendName + CRLF;
                    tmp += "등록일시(regDT) : " + friendInfo.regDT + CRLF + CRLF;

                }

                MessageBox.Show(tmp, "플러스친구 목록 확인");
            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "플러스친구 목록 확인");
            }
        }

        /*
         * 발신번호 관리 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_SENDER_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "SENDER");
                MessageBox.Show(url, "발신번호 관리 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "발신번호 관리 팝업 URL");
            }
        }

        /*
         * 발신번호 목록을 반환합니다.
         */
        private void btnGetSenderNumberList_Click(object sender, EventArgs e)
        {
            try
            {
                List<SenderNumber> SenderNumberList = kakaoService.GetSenderNumberList(txtCorpNum.Text, txtUserId.Text);

                String tmp = null;

                foreach (SenderNumber numInfo in SenderNumberList)
                {
                    tmp += "발신번호(number) : " + numInfo.number + CRLF;
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
         * 알림톡 템플릿관리 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_TEMPLATE_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "TEMPLATE");
                MessageBox.Show(url, "알림톡 템플릿 관리 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "알림톡 템플릿 관리 팝업 URL");
            }
        }


        /*
         * (주)카카오로 심사 승인된 알림톡 템플릿 목록을 반환합니다.
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
                    tmp += "플러스친구 아이디(plusFriendID) : " + templateInfo.plusFriendID + CRLF;
                    
                    if (templateInfo.btns != null)
                    {
                        foreach (KakaoButton buttonInfo in templateInfo.btns)
                        {
                        tmp += "[알림톡 버튼 정보]"+ CRLF;
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
         * 카카오톡 전송내역 팝업 URL을 반환합니다.
         * - 반환된 URL은 보안정책으로 인해 30초의 유효시간을 갖습니다.
         */
        private void btnGetURL_BOX_Click(object sender, EventArgs e)
        {
            try
            {
                string url = kakaoService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX");                
                MessageBox.Show(url, "카카오톡 전송내역 팝업 URL");

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "카카오톡 전송내역 팝업 URL");
            }
        }

        /*
         * 카카오톡 전송내역 목록을 조회합니다.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 시작일자, 날짜형식(yyyMMdd)
            String SDate = "20180305";

            // 종료일자, 날짜형식(yyyyMMdd)
            String EDate = "20180305";

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

            try
            {
                KakaoSearchResult searchResult = kakaoService.Search(txtCorpNum.Text, SDate, EDate, State,
                                                                  Item, ReserveYN, SenderYN, Order, Page, PerPage);

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
         * 카카오톡 예약전송건을 취소합니다.
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
         * 카카오톡 전송내역을 조회합니다.
         */
        private void btnGetMessages_Click(object sender, EventArgs e)
        {
            try
            {
                KakaoSentResult info = kakaoService.GetMessages(txtCorpNum.Text, txtReceiptNum.Text);

                String tmp = "";
                tmp += "contentType (카카오톡 유형) : " + info.contentType + CRLF;
                tmp += "templateCode (템플릿 코드) : " + info.templateCode + CRLF;
                tmp += "plusFriendID (플러스친구 아이디) : " + info.plusFriendID + CRLF;
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

                if (info.btns != null)
                {
                    foreach (KakaoButton buttonInfo in info.btns)
                    {
                        tmp += "[버튼 정보]" + CRLF;
                        tmp += "버튼명(n) : " + buttonInfo.n + CRLF;
                        tmp += "버튼유형(t) : " + buttonInfo.t + CRLF;
                        tmp += "버튼링크1(u1) : " + buttonInfo.u1 + CRLF;
                        tmp += "버튼링크1(u2) : " + buttonInfo.u2 + CRLF;
                    }
                }
                MessageBox.Show(tmp, "전송내역 확인");

                dataGridView1.DataSource = info.msgs;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역 확인");
            }
        }

        private void btnGetMessagesRN_Click(object sender, EventArgs e)
        {
            try
            {
                KakaoSentResult info = kakaoService.GetMessagesRN(txtCorpNum.Text, txtRequestNum.Text);

                String tmp = "";
                tmp += "contentType (카카오톡 유형) : " + info.contentType + CRLF;
                tmp += "templateCode (템플릿 코드) : " + info.templateCode + CRLF;
                tmp += "plusFriendID (플러스친구 아이디) : " + info.plusFriendID + CRLF;
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

                if (info.btns != null)
                {
                    foreach (KakaoButton buttonInfo in info.btns)
                    {
                        tmp += "[버튼 정보]" + CRLF;
                        tmp += "버튼명(n) : " + buttonInfo.n + CRLF;
                        tmp += "버튼유형(t) : " + buttonInfo.t + CRLF;
                        tmp += "버튼링크1(u1) : " + buttonInfo.u1 + CRLF;
                        tmp += "버튼링크1(u2) : " + buttonInfo.u2 + CRLF;
                    }
                }
                MessageBox.Show(tmp, "전송내역 확인");

                dataGridView1.DataSource = info.msgs;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show("응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "전송내역 확인");
            }
        }

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

    }
}