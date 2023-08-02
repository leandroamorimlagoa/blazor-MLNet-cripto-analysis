namespace LagoaTrading.Domain.Core.Securities
{
    public static class ConstantNamesServer
    {
        public static class Messages
        {
            public const string ActivationEmailSent = "Foi enviado um email ({email}) para ativação da sua conta";
        }
        public static class InterpolationNames
        {
            public const string email = "{email}";
            public const string hash = "{hash}";
            public const string activation_url = "{activation-url}";
            public const string forgot_password_url = "{forgotpassword-url}";
        }
        public static class Database
        {
            public const int NameSize = 256;
            public const int HashSize = 512;
        }

        public static class Currency
        {
            public const string BrlSymbol = "brl";
        }
    }
}
