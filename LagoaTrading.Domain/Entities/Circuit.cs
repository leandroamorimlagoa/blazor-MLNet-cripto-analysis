using LagoaTrading.Domain.Core.Basics;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Entities
{
    public class Circuit : BaseEntity
    {
        public long UserId { get; set; }
        public CircuitType CircuitType { get; set; }
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        public DateTime? EndDateTime { get; set; }

        public decimal StartValue { get; set; }
        public decimal EndValue { get; set; }
        public decimal DifferenceValue { get; set; }



        public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
        public virtual List<Position> PositionBuy
        {
            get
            {
                return Positions.Where(x=>x.Side == Side.Buy).ToList();
            }
        }
        public virtual ICollection<Position> PositionSell
        {
            get
            {
                return Positions.Where(x => x.Side == Side.Sell).ToList();
            }
        }
    }
}
