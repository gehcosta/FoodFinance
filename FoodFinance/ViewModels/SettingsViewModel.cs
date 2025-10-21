using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFinance.Models;
using FoodFinance.Services;
using System.Collections.ObjectModel;

namespace FoodFinance.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
     [ObservableProperty]
 private string selectedTheme = "System";

        [ObservableProperty]
   private string gasPrice = "6.50";

     [ObservableProperty]
   private string kmPerLiter = "30";

      [ObservableProperty]
     private string maintenancePerKm = "0.20";

        public ObservableCollection<string> Themes { get; } = new() { "Light", "Dark", "System" };

        public SettingsViewModel()
    {
      LoadSettings();
        }

  private async void LoadSettings()
{
      var settings = await LocalStorageService.LoadSettingsAsync<Settings>();
     selectedTheme = settings.Theme;
       gasPrice = settings.GasPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
     kmPerLiter = settings.KmPerLiter.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
     maintenancePerKm = settings.MaintenancePerKm.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

     ApplyTheme(settings.Theme);
        }

   [RelayCommand]
        public async Task SaveSettings()
        {
 if (!decimal.TryParse(gasPrice, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var gasPriceValue))
  {
       await Application.Current!.MainPage!.DisplayAlert("Erro", "Preço da gasolina inválido.", "OK");
   return;
     }

            if (!float.TryParse(kmPerLiter, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var kmPerLiterValue))
 {
 await Application.Current!.MainPage!.DisplayAlert("Erro", "Km por litro inválido.", "OK");
       return;
}

            if (!decimal.TryParse(maintenancePerKm, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var maintenanceValue))
            {
    await Application.Current!.MainPage!.DisplayAlert("Erro", "Manutenção por km inválida.", "OK");
          return;
    }

var settings = new Settings
        {
        Theme = selectedTheme,
    GasPrice = gasPriceValue,
    KmPerLiter = kmPerLiterValue,
          MaintenancePerKm = maintenanceValue
 };

  await LocalStorageService.SaveSettingsAsync(settings);
            ApplyTheme(selectedTheme);

    await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Configurações salvas com sucesso!", "OK");
     }

     private void ApplyTheme(string theme)
    {
       if (Application.Current == null) return;

     AppTheme appTheme = theme switch
       {
                "Light" => AppTheme.Light,
        "Dark" => AppTheme.Dark,
  _ => AppTheme.Unspecified
        };

    Application.Current.UserAppTheme = appTheme;
   }
    }
}
