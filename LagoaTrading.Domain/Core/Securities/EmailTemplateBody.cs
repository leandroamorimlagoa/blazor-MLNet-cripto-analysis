namespace LagoaTrading.Domain.Core.Securities
{
    public class EmailTemplateBody
    {
        public static string LoadTemplate(string templateName)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, templateName);
            var body = File.ReadAllText(filePath);
            return body;
        }
    }
}
