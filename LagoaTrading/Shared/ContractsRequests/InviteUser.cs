namespace LagoaTrading.Shared.ContractsRequests
{
    public class InviteUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime SignedIn { get; set; }
    }
}
