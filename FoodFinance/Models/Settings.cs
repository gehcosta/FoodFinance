namespace FoodFinance.Models
{
    public class Settings
    {
        public string Theme { get; set; } = "System";
        public decimal GasPrice { get; set; } = 6.50M;
        public float KmPerLiter { get; set; } = 30;
        public decimal MaintenancePerKm { get; set; } = 0.20M;
    }
}
