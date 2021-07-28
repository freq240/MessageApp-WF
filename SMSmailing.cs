namespace Message
{
    class SMSmailing : SMSmessage
    {
        public delegate void EventDelegate(string message);
        public event EventDelegate Notify;

        private string recepientNumberTwo;
        private string recepientNumberThree;
        private string RecepientNumberTwo { get { return this.recepientNumberTwo; } set { this.recepientNumberTwo = value; } }
        private string RecepientNumberThree { get { return this.recepientNumberThree; } set { this.recepientNumberThree = value; } }

        // Constructors
        public SMSmailing(string senderNumber, string recepientNumber, string recepientNumberTwo, string text) :
            base(senderNumber, recepientNumber, text)
        {
            this.recepientNumberTwo = recepientNumberTwo;
            this.recepientNumberThree = "Unknown";
        }
        public SMSmailing(string senderNumber, string recepientNumber, string recepientNumberTwo, string recepientNumberThree, string text) :
            base(senderNumber, recepientNumber, text)
        {
            this.recepientNumberTwo = recepientNumberTwo;
            this.recepientNumberThree = recepientNumberThree;
        }

        // get-methods
        public string GET_recepientNumberTwo()
        {
            return this.recepientNumberTwo;
        }
        public string GET_recepientNumberThree()
        {
            return this.recepientNumberThree;
        }

        // own-methods
        public int CLC_payment_mailing()
        {
            if (this.RecepientNumberTwo == "Unknown" && this.RecepientNumberThree == "Unknown")
            {
                return (this.text.Length / 40);
            }
            else
            {
                if (this.RecepientNumberThree == "Unknown")
                {
                    return (this.text.Length / 40) * 2;
                }
                else
                {
                    return (this.text.Length / 40) * 3;
                }
            }

        }

        // set-methods

        public void SET_recepientNumberTwo(string recepientNumberTwo)
        {
            this.recepientNumberTwo = recepientNumberTwo;
            Notify?.Invoke("\n[EVENT] Recepient 2 number has been changed!\n");
        }

        public void SET_recepientNumberThree(string recepientNumberThree)
        {
            this.recepientNumberThree = recepientNumberThree;
            Notify?.Invoke("\n[EVENT] Recepient 3 number has been changed!\n");
        }

        // ToString override
        public override string ToString()
        {
            return $"[SMS-MAILING] Sender number : {this.senderNumber}\n[SMS-MAILING] Recepient number 1: {this.recepientNumber}" +
                $"\n[SMS-MAILING] Recepient number 2 : { this.recepientNumberTwo}" + $"\n[SMS-MAILING] Recepient number 3 : { this.recepientNumberThree}" +
                $"\n[SMS-MAILING] Text of message : {this.text}";
        }
    }
}
