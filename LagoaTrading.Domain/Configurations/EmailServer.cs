namespace LagoaTrading.Domain.Configurations
{
    public class EmailServer
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpFromAddress { get; set; }
        public string SmtpFromName { get; set; }
        public string SmtpReplyToAddress { get; set; }
        public string SmtpReplyToName { get; set; }
        public bool SmtpUseSsl { get; set; }
        public bool SmtpUseDefaultCredentials { get; set; }
    }
}
