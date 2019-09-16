using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class Trade
    {
        public int Id { get; set; }
        public int NoOfShares { get; set; }
        public string Action { get; set; }
        public double Price { get; set; }
        public string Symbol { get; set; }
        public int PortfolioId { get; set; }
    }
}
