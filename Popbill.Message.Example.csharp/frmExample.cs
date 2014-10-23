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
        private string SecretKey = "AtWERjckl6htO35eW7NsxF7jNhD5NrxyNPIWvLrbDFw=";

        private MessageService messageService;

        private const string CRLF = "\r\n";

        public frmExample()
        {
            InitializeComponent();

            //초기화
            messageService = new MessageService(LinkID, SecretKey);
            //테스트를 완료한후 아래 변수를 false로 변경하거나, 아래줄을 삭제하여 실제 서비스 연결.
            messageService.IsTest = true;
        }

        private void getPopbillURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetPopbillURL(txtCorpNum.Text, txtUserId.Text, cboPopbillTOGO.Text);

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnJoinMember_Click(object sender, EventArgs e)
        {
            JoinForm joinInfo = new JoinForm();

            joinInfo.LinkID = LinkID;
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
                Response response = messageService.JoinMember(joinInfo);

                MessageBox.Show(response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {

            try
            {
                double remainPoint = messageService.GetBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetPartnerBalance_Click(object sender, EventArgs e)
        {
            try
            {
                double remainPoint = messageService.GetPartnerBalance(txtCorpNum.Text);

                MessageBox.Show(remainPoint.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnCheckIsMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CheckIsMember(txtCorpNum.Text, LinkID);

                MessageBox.Show(response.code.ToString() + " | " + response.message);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnUnitCost_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text,MessageType.SMS);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }
       
        private void btnUnitCost_LMS_Click(object sender, EventArgs e)
        {
            try
            {
                float unitCost = messageService.GetUnitCost(txtCorpNum.Text, MessageType.LMS);

                MessageBox.Show(unitCost.ToString());

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

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
            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text, "07075106766", "11122223333", "수신자명칭", "단문 문자 메시지 내용. 90Byte", getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.sendNum = "07075106766";
                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
                msg.content = "단문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text,messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendSMS_Same_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
            
                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendSMS(txtCorpNum.Text,"07075106766","동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendLMS_one_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNum = messageService.SendLMS(
                                        txtCorpNum.Text, 
                                        "07075106766", 
                                        "11122223333", 
                                        "수신자명칭",
                                        "장문문자 메시지 제목",
                                        "장문 문자 메시지 내용. 2000Byte", 
                                        getReserveDT(), 
                                        txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendLMS_hund_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.sendNum = "07075106766";
                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
                msg.subject = "장문 문자메시지 제목";
                msg.content = "장문 문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendLMS_same_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendLMS(txtCorpNum.Text, "07075106766","동보 메시지 제목", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendXMS_one_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNum = messageService.SendXMS(
                                        txtCorpNum.Text,
                                        "07075106766",
                                        "11122223333",
                                        "수신자명칭",
                                        "장문문자 메시지 제목",
                                        "문자 메시지 내용. 메시지의 길이에 따라 90Byte를 기준으로 SMS/LMS를 선택전송",
                                        getReserveDT(),
                                        txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendXMS_hund_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.sendNum = "07075106766";
                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;
                msg.subject = "문자메시지 제목";
                msg.content = "문자메시지 내용, 각 메시지마다 개별설정 가능." + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnSendXMS_same_Click(object sender, EventArgs e)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();

                msg.receiveNum = "11122223333";
                msg.receiveName = "수신자명칭_" + i;

                messages.Add(msg);
            }
            try
            {
                string receiptNum = messageService.SendXMS(txtCorpNum.Text, "07075106766", "동보 메시지 제목", "동보 단문문자 메시지 내용", messages, getReserveDT(), txtUserId.Text);

                MessageBox.Show("접수번호 : " + receiptNum);
                txtReceiptNum.Text = receiptNum;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);

            }
        }

        private void btnGetURL_Click(object sender, EventArgs e)
        {
            try
            {
                string url = messageService.GetURL(txtCorpNum.Text, txtUserId.Text, "BOX");

                MessageBox.Show(url);

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnCancelReserve_Click(object sender, EventArgs e)
        {
            try
            {
                Response response = messageService.CancelReserve(txtCorpNum.Text,txtReceiptNum.Text,txtUserId.Text);

                MessageBox.Show(response.message);


            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

        private void btnGetMessageResult_Click(object sender, EventArgs e)
        {
            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResult(txtCorpNum.Text, txtReceiptNum.Text);

                dataGridView1.DataSource = ResultList;

            }
            catch (PopbillException ex)
            {
                MessageBox.Show(ex.code.ToString() + " | " + ex.Message);
            }
        }

    }
}
