using System;

namespace EC.Common
{
    public class ParameterException : Exception
    {
        public ParameterException(string parameterName, object parameterValue, TypeParameterException type)
            : base(FormatMessage(parameterName, parameterValue, type))
        {
        }

        public ParameterException(string parameterName, object parameterValue)
            : base(FormatMessage(parameterName, parameterValue, TypeParameterException.Invalid))
        {
        }

        private static string FormatMessage(string parameterName, object parameterValue, TypeParameterException type)
        {
            switch (type)
            {
                case TypeParameterException.IsNullOrEmpty:
                    return string.Concat("O parâmetro \"{0}\" não pode ser ", parameterValue == null ? "nulo" : "vazio", ".");
                case TypeParameterException.Invalid:
                    return string.Format("O parâmetro \"{0}\" com valor \"{1}\" é inválido.", parameterName, parameterValue);
                default:
                    throw new Exception("Invalid type of parameter exception.");
            }
        }
    }
}
