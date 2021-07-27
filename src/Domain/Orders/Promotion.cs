using System.Collections.Generic;
using System.ComponentModel;

namespace Domain.Orders
{
    public class Promotion
    {
        public int Id { get; set; }
        public int UnitCostAmount { get; set; }
        public int PriceTrigger { get; set; }

        [DefaultValue(0)]
        public int NumberOfTimesValid { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
