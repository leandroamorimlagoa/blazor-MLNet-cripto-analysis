namespace LagoaTrading.Shared.Enumerators
{
    public enum OrderState : short
    {
        Active = 1,
        Canceled = 2,
        Filled = 3,
        PartiallyCancelled = 4,
        PartiallyFilled = 5,
    }
}
