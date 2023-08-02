namespace LagoaTrading.Shared.Enumerators
{
    public enum State : short
    {
        Registered = 0,
        Active = 1,
        Cancelled = 2,
        Filled = 3,
        PartiallyFilled = 4,
        PartiallyCanceled = 5
    }
}
