namespace LagoaTrading.Client.Core
{
    public static class ConstantNames
    {
        public static class ApiRoutes
        {
            public const string NewUser = "api/newuser";
            public const string ActivateUser = "api/activateuser/{hash}";
            public const string ForgotPassword = "api/forgotpassword/{hash}";
            public const string ForgotPasswordRegisterNew = "api/forgotpasswordregisternew";
            public const string AuthenticateUser = "api/authenticateuser";

            public const string UserAccount = "api/useraccount/{hash}";
            public const string UserPositions = "api/userpositions";
            public const string UserParameters = "api/userparameters/{hash}";
            public const string Circuit = "api/circuit";
            public const string StartSingleCircuit = "api/circuit/{hash}/Single";
            public const string Analysis = "api/analysis/{hash}";
            public const string Simulation = "api/simulation/{hash}/{marketId}";
            public const string CandlestickLastUpdate = "api/Candlesticks/LastUpdate";
            public const string CandlestickUpdate = "api/Candlesticks/Update";
        }
        public static class Interpolation
        {
            public const string Hash = "{hash}";
            public const string MarketId = "{marketId}";
        }
        public static class Routes
        {
            public const string Dashboard = "dashboard";
            public const string InviteFriend = "invite-friend";
            public const string Parameters = "parameters";
            public const string SingleCircuit = "single-circuit";
            public const string ContinuousCircuit = "continuous-circuit";

            public const string ForgotPassword = "forgot-password";
            public const string RegisterUser = "register-user";
            public const string Logout = "logout";
            public const string AuthenticateUser = "/";
        }
        public static class LocalStoreNames
        {
            public const string AuthenticationToken = "Auth";
        }
        public static class DefaultMasks
        {
            public const string Date = "dd/MM/yyyy";
            public const string DateTime = "dd/MM/yyyy HH:mm";
            public const string CryptoyCurrency = "#0.00000000";
            public const string DecimalTime = "#0.00";
        }
    }
}
