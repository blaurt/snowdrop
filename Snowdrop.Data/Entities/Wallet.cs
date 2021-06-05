using Snowdrop.Data.Entities.Core;
using Snowdrop.Data.Enums;

namespace Snowdrop.Data.Entities
{
    public class Wallet: BaseEntity
    {
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        
        public virtual Project Project { get; set; }
    }
}