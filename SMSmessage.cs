using System;

namespace Message
{
    public delegate void EventDelegate(string message);

    [Serializable]
    class SMSmessage
    {
        public delegate void EventDelegate(string message);
        public event EventDelegate Notify;

        protected string senderNumber;
        protected string recepientNumber;
        protected string text;
        protected string SenderNumber { get { return this.senderNumber; } set { this.senderNumber = value; } }
        protected string RecepientNumber { get { return this.recepientNumber; } set { this.recepientNumber = value; } }
        protected string Text { get { return this.text; } set { this.text = value; } }

        public SMSmessage()
        {
            this.senderNumber = "Unknown";
            this.recepientNumber = "Unknown";
            this.text = "Unknown";

        }
        public SMSmessage(string senderNumber, string recepientNumber, string text)
        {
            this.senderNumber = senderNumber;
            this.recepientNumber = recepientNumber;
            this.text = text;

        }
        // get-methods
        public string GET_senderNumber()
        {
            return this.senderNumber;
        }
        public string GET_recepientNumber()
        {
            return this.recepientNumber;
        }
        public string GET_text()
        {
            return this.text;
        }

        // set-methods
        public void SET_senderNumber(string senderNumber)
        {
            this.senderNumber = senderNumber;
            Notify?.Invoke("\n[EVENT] Sender name has been changed!\n");
        }
        public void SET_recepientNumber(string recepientNumber)
        {
            this.recepientNumber = recepientNumber;
            Notify?.Invoke("\n[EVENT] Recepient number has been changed!\n");
        }
        public void SET_text(string text)
        {
            this.text = text;
            Notify?.Invoke("\n[EVENT] Text of message has been changed!\n");
        }

        // own-methods
        public int CLC_payment()
        {
            return this.text.Length / 40;
        }

        // operator override
        public static bool operator >(SMSmessage obj1, SMSmessage obj2)
        {
            if (obj1.text.Length > obj2.text.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <(SMSmessage obj1, SMSmessage obj2)
        {
            if (obj1.text.Length < obj2.text.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static double operator *(SMSmessage obj1, double value)
        {
            return obj1.CLC_payment() * value;
        }

        // ToString override
        public override string ToString()
        {
            return $"[SMS-MESSAGE] Sender number : {this.senderNumber}\n[SMS-MESSAGE] Recepient number : {this.recepientNumber}" +
                $"\n[SMS-MESSAGE] Text of message : {this.text}";
        }

    }
}
