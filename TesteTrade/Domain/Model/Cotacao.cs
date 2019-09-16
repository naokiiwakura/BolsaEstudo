using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class Cotacao
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public double Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
