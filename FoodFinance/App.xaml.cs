using Microsoft.Extensions.DependencyInjection;
using FoodFinance.Services;
using FoodFinance.Models;

namespace FoodFinance
{
    public partial class App : Application
    {
        public App()
        {
          InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
 base.OnStart();
  
    // Verificar se é o primeiro acesso
            await CheckFirstAccess();
        }

   private async Task CheckFirstAccess()
        {
try
            {
  var settings = await LocalStorageService.LoadSettingsAsync<Settings>();
     
       // Se as configurações essenciais não estiverem preenchidas, mostrar aviso
        if (settings.GasPrice == 0 || settings.KmPerLiter == 0)
        {
     // Aguardar um pouco para garantir que a navegação está pronta
           await Task.Delay(500);
          
                    if (MainPage != null)
         {
            bool goToSettings = await MainPage.DisplayAlert(
           "Bem-vindo ao FoodFinance!", 
          "Para começar a usar o aplicativo, é necessário configurar os parâmetros básicos como preço da gasolina e consumo do veículo.\n\nDeseja ir para as configurações agora?",
          "Sim", 
     "Depois");
            
        if (goToSettings)
  {
  await Shell.Current.GoToAsync("//Settings");
            }
 }
          }
            }
       catch (Exception ex)
       {
            System.Diagnostics.Debug.WriteLine($"Erro ao verificar primeiro acesso: {ex.Message}");
            }
        }
    }
}