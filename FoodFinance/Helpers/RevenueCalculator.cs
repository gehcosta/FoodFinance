using FoodFinance.Models;

namespace FoodFinance.Helpers
{
    public static class RevenueCalculator
    {
        /// <summary>
        /// Calcula o faturamento líquido considerando custos de combustível e manutenção
        /// </summary>
        /// <param name="earned">Valor ganho</param>
      /// <param name="km">Quilometragem percorrida</param>
        /// <param name="gasPrice">Preço da gasolina por litro</param>
  /// <param name="kmPerLiter">Km por litro do veículo</param>
        /// <param name="maintenancePerKm">Custo de manutenção por km</param>
        /// <returns>Valor líquido após descontar custos</returns>
        public static decimal CalculateNetRevenue(
         decimal earned,
            float km,
         decimal gasPrice,
            float kmPerLiter,
    decimal maintenancePerKm)
        {
     // Calcula custo de combustível: (km / km_por_litro) * preco_gasolina
            var gasCost = (decimal)(km / kmPerLiter) * gasPrice;
          
      // Calcula custo de manutenção: km * custo_por_km
          var maintenanceCost = (decimal)km * maintenancePerKm;
  
      // Retorna o valor líquido
            return earned - gasCost - maintenanceCost;
        }

    /// <summary>
        /// Calcula todos os custos e retorna um resumo detalhado
   /// </summary>
   public static (decimal gasCost, decimal maintenanceCost, decimal netRevenue) CalculateDetailedRevenue(
            decimal earned,
   float km,
            Settings settings)
   {
            var gasCost = (decimal)(km / settings.KmPerLiter) * settings.GasPrice;
    var maintenanceCost = (decimal)km * settings.MaintenancePerKm;
var netRevenue = earned - gasCost - maintenanceCost;

        return (gasCost, maintenanceCost, netRevenue);
        }
    }
}
