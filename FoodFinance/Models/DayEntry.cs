using System;

namespace FoodFinance.Models
{
    public class DayEntry
    {
        public DateTime Date { get; set; }
        public float Km { get; set; }
 public decimal Earned { get; set; }
        public TimeSpan WorkTime { get; set; }
        public TimeSpan StartTime { get; set; }
      public TimeSpan EndTime { get; set; }
    }
}
