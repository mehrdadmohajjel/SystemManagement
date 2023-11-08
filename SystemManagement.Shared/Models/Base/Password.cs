using System.Text.RegularExpressions;
using SystemManagement.Shared.Utilities;

namespace SystemManagement.Shared.Models.Base
{
    public class Password
    {
        private string _hashValue;

        public Reason Reason { set; get; }

        public string HashValue
        {
            set => _hashValue = value;
            get => _hashValue ?? InitiatePasswordHash();
        }

        public string PlainTextValue { set; get; }

        public bool PlainTextValueContentIsValid { set; get; }

        private string InitiatePasswordHash()
        {
            return Converter.ToSHA256Hash(PlainTextValue);
        }

        public bool DoesPlainTextContentValid()
        {
            Regex len = new Regex("^.{6,20}$");
            Regex space = new Regex("^\\S*$");
            Regex num = new Regex("\\d");
            Regex alpha = new Regex("\\D");


            if (!len.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else if (!space.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else if (!num.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else if (!alpha.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else
            {
                PlainTextValueContentIsValid = true;
            }

            return true;
        }
    }
}