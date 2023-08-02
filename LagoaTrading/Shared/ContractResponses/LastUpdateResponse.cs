namespace LagoaTrading.Shared.ContractResponses
{
    public class LastUpdateResponse
    {
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.MinValue;

    }
}
