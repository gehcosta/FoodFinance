using System;
using System.Collections.Generic;

namespace FoodFinance.Models
{
  public class RevenueSummary
    {
        public DateTime Date { get; set; }
        public decimal TotalEarned { get; set; }
        public decimal TotalGasCost { get; set; }
        public decimal TotalMaintenanceCost { get; set; }
  public decimal NetRevenue { get; set; }
        public float TotalKm { get; set; }
public TimeSpan TotalWorkTime { get; set; }
        public List<DayEntry> Entries { get; set; } = new();
    }
}
