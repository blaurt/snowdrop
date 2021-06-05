using Snowdrop.Data.Entities.Core;
using Snowdrop.Data.Enums;

namespace Snowdrop.Data.Entities
{
    public class Balance: BaseEntity
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        
        public virtual User User { get; set; }
    }
}