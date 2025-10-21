using FoodFinance.Helpers;
using FoodFinance.Models;

namespace FoodFinance.Tests
{
    public class RevenueCalculatorTests
    {
 public static void RunTests()
    {
            Console.WriteLine("=== Executando Testes do FoodFinance ===\n");

      // Teste 1: Cálculo conforme exemplo do projeto
       Test1_ExampleFromProject();

   // Teste 2: Teste com valores zerados
Test2_ZeroValues();

  // Teste 3: Teste com valores altos
    Test3_HighValues();

      // Teste 4: Teste de persistência (simulado)
 Test4_DataPersistence();

  Console.WriteLine("\n=== Todos os testes concluídos ===");
      }

  private static void Test1_ExampleFromProject()
{
        Console.WriteLine("Teste 1: Exemplo do Projeto");
   Console.WriteLine("Entrada: Ganho=R$ 51.42, Km=27.5, Gasolina=R$ 6.50/L, Km/L=30, Manutenção=R$ 0.20/km");
      
     var net = RevenueCalculator.CalculateNetRevenue(
    earned: 51.42M,
    km: 27.5F,
     gasPrice: 6.50M,
   kmPerLiter: 30F,
      maintenancePerKm: 0.20M
       );

     // Cálculo esperado:
   // Custo gasolina = (27.5 / 30) * 6.5 = 5.958333...
   // Custo manutenção = 27.5 * 0.20 = 5.50
// Líquido = 51.42 - 5.958333 - 5.50 = 39.961666...

     var expected = 39.96M; // Aproximado
 var tolerance = 0.01M;

   if (Math.Abs(net - expected) < tolerance)
            {
                Console.WriteLine($"? PASSOU: Líquido calculado = R$ {net:F2} (esperado ~R$ {expected:F2})");
     }
         else
      {
    Console.WriteLine($"? FALHOU: Líquido calculado = R$ {net:F2}, esperado ~R$ {expected:F2}");
 }
  Console.WriteLine();
        }

    private static void Test2_ZeroValues()
        {
    Console.WriteLine("Teste 2: Valores Zerados");
      Console.WriteLine("Entrada: Ganho=R$ 0, Km=0");

  var net = RevenueCalculator.CalculateNetRevenue(
  earned: 0M,
       km: 0F,
     gasPrice: 6.50M,
   kmPerLiter: 30F,
    maintenancePerKm: 0.20M
      );

if (net == 0M)
   {
                Console.WriteLine($"? PASSOU: Líquido = R$ {net:F2}");
  }
  else
  {
    Console.WriteLine($"? FALHOU: Líquido = R$ {net:F2}, esperado R$ 0.00");
            }
Console.WriteLine();
 }

   private static void Test3_HighValues()
        {
    Console.WriteLine("Teste 3: Valores Altos");
       Console.WriteLine("Entrada: Ganho=R$ 500.00, Km=150");

     var net = RevenueCalculator.CalculateNetRevenue(
  earned: 500.00M,
     km: 150F,
      gasPrice: 6.50M,
     kmPerLiter: 30F,
    maintenancePerKm: 0.20M
   );

     // Custo gasolina = (150 / 30) * 6.5 = 32.50
   // Custo manutenção = 150 * 0.20 = 30.00
  // Líquido = 500 - 32.50 - 30.00 = 437.50

var expected = 437.50M;

       if (net == expected)
{
       Console.WriteLine($"? PASSOU: Líquido = R$ {net:F2}");
  }
   else
       {
    Console.WriteLine($"? FALHOU: Líquido = R$ {net:F2}, esperado R$ {expected:F2}");
    }
     Console.WriteLine();
  }

        private static void Test4_DataPersistence()
     {
   Console.WriteLine("Teste 4: Estrutura de Dados");
      Console.WriteLine("Verificando se as classes de modelo estão corretas");

   try
      {
     var entry = new DayEntry
    {
          Date = DateTime.Now,
         Km = 27.5F,
   Earned = 51.42M,
    WorkTime = TimeSpan.FromHours(8),
    StartTime = TimeSpan.FromHours(9),
      EndTime = TimeSpan.FromHours(17)
  };

     var settings = new Settings
       {
     Theme = "Light",
     GasPrice = 6.50M,
    KmPerLiter = 30F,
            MaintenancePerKm = 0.20M
      };

    var summary = new RevenueSummary
     {
      Date = DateTime.Now,
      TotalEarned = 100M,
TotalGasCost = 20M,
  TotalMaintenanceCost = 10M,
        NetRevenue = 70M,
       TotalKm = 50F,
        TotalWorkTime = TimeSpan.FromHours(16)
            };

    Console.WriteLine($"? PASSOU: DayEntry criado - Ganho: R$ {entry.Earned:F2}, Km: {entry.Km}");
     Console.WriteLine($"? PASSOU: Settings criado - Gasolina: R$ {settings.GasPrice:F2}/L");
 Console.WriteLine($"? PASSOU: RevenueSummary criado - Líquido: R$ {summary.NetRevenue:F2}");
  }
catch (Exception ex)
   {
     Console.WriteLine($"? FALHOU: Erro ao criar modelos - {ex.Message}");
   }
  Console.WriteLine();
   }
    }
}
