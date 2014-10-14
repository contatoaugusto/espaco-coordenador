using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EC.Common
{
    [Serializable]
    public class BusinessException : System.Exception
    {
        private string _message = "";
        private string[] _args = null;

        public int Number { get; set; }

        public override string Message
        {
            get { return _message; }
        }

        public string[] Args
        {
            get { return _args; }
        }

        public BusinessException(int number)
            : base()
        {
            this.Number = number;

            _message = BusinessRules.GetRule(number);
        }

        public BusinessException(int number, string message)
            : base(message)
        {
            this.Number = number;
            _message = message;
        }

        public BusinessException(int number, string message, params string[] args)
            : base(message)
        {
            this.Number = number;
            _message = message;
            _args = args;

            _message = BusinessRules.GetRule(number);

            if (args != null)
                _message = string.Format(_message, args);
        }

        public BusinessException(int number, string message, BusinessException innerException)
            : base(message, innerException)
        {
            this.Number = number;
            _message = message;
        }

        public BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.Number = info.GetInt32("Number");
                _message = info.GetString("Message");
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("Number", this.Number);
                info.AddValue("Message", this.Message);
            }
        }
    }
}
