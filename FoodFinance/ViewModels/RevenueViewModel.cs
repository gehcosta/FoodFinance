using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFinance.Models;
using FoodFinance.Services;
using System.Collections.ObjectModel;

namespace FoodFinance.ViewModels
{
    public partial class RevenueViewModel : ObservableObject
    {
    [ObservableProperty]
  private ObservableCollection<DayEntry> entries = new();

        [ObservableProperty]
 private decimal totalEarned;

   [ObservableProperty]
  private decimal totalGasCost;

   [ObservableProperty]
private decimal totalMaintenanceCost;

        [ObservableProperty]
    private decimal netRevenue;

        [ObservableProperty]
        private float totalKm;

        [ObservableProperty]
     private string totalWorkTime = "00:00:00";

 [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        [ObservableProperty]
        private string viewMode = "Diário";

public ObservableCollection<string> ViewModes { get; } = new() { "Diário", "Mensal" };

        public RevenueViewModel()
  {
 _ = LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
       try
  {
        var allEntries = await LocalStorageService.LoadDataAsync<ObservableCollection<DayEntry>>() ?? new ObservableCollection<DayEntry>();
   var settings = await LocalStorageService.LoadSettingsAsync<Settings>();

        IEnumerable<DayEntry> filteredEntries;

    if (ViewMode == "Diário")
     {
   filteredEntries = allEntries.Where(e => e.Date.Date == SelectedDate.Date);
          }
 else // Mensal
       {
      filteredEntries = allEntries.Where(e => e.Date.Year == SelectedDate.Year && e.Date.Month == SelectedDate.Month);
          }

     Entries = new ObservableCollection<DayEntry>(filteredEntries);

      TotalKm = Entries.Sum(e => e.Km);
    TotalEarned = Entries.Sum(e => e.Earned);

    // Cálculo de custos corrigido
// Custo da gasolina = (Total de Km / Km por Litro) * Preço por Litro
    // Exemplo: Se andei 100km, consumo 10km/L e gasolina custa R$6,50/L
     // Litros gastos = 100 / 10 = 10 litros
  // Custo = 10 * 6,50 = R$ 65,00
if (settings.KmPerLiter > 0)
    {
 var litrosGastos = TotalKm / settings.KmPerLiter;
      TotalGasCost = (decimal)litrosGastos * settings.GasPrice;
    }
   else
   {
          TotalGasCost = 0;
 }

    // Custo de manutenção = Total de Km * Custo por Km
  TotalMaintenanceCost = (decimal)TotalKm * settings.MaintenancePerKm;
         
   NetRevenue = TotalEarned - TotalGasCost - TotalMaintenanceCost;

              var totalTime = TimeSpan.Zero;
     foreach (var entry in Entries)
       {
        totalTime += entry.WorkTime;
}
     TotalWorkTime = totalTime.ToString(@"hh\:mm\:ss");
        }
        catch (Exception ex)
{
        System.Diagnostics.Debug.WriteLine($"Erro ao carregar dados: {ex.Message}");
    }
     }

 [RelayCommand]
   async Task DeleteEntry(DayEntry entry)
   {
   try
      {
  bool confirm = await Application.Current!.MainPage!.DisplayAlert(
      "Confirmar Exclusão",
   $"Deseja realmente excluir o registro do dia {entry.Date:dd/MM/yyyy}?",

     "Sim", "Não");

        if (!confirm) return;

   // Carregar todos os registros
    var allEntries = await LocalStorageService.LoadDataAsync<ObservableCollection<DayEntry>>() 
             ?? new ObservableCollection<DayEntry>();

       // Encontrar e remover o registro
   var entryToRemove = allEntries.FirstOrDefault(e => 
     e.Date == entry.Date && 
   e.Km == entry.Km && 
           e.Earned == entry.Earned &&
        e.StartTime == entry.StartTime);

   if (entryToRemove != null)
  {
    allEntries.Remove(entryToRemove);
     await LocalStorageService.SaveDataAsync(allEntries);
    
      await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Registro excluído com sucesso!", "OK");
       
      // Recarregar dados
    await LoadDataAsync();
    }
   }
        catch (Exception ex)
     {
     await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao excluir registro: {ex.Message}", "OK");
        }
 }

 [RelayCommand]
        async Task EditEntry(DayEntry entry)
 {
            try
     {
    // Criar página de diálogo para editar
     string dateStr = await Application.Current!.MainPage!.DisplayPromptAsync(
    "Editar Data",
      "Data (dd/MM/yyyy):",
          initialValue: entry.Date.ToString("dd/MM/yyyy"),
         keyboard: Keyboard.Text);

      if (string.IsNullOrEmpty(dateStr)) return;

   string kmStr = await Application.Current!.MainPage!.DisplayPromptAsync(
      "Editar Km",
 "Km percorridos:",
     initialValue: entry.Km.ToString("F2"),
  keyboard: Keyboard.Numeric);

      if (string.IsNullOrEmpty(kmStr)) return;

    string earnedStr = await Application.Current!.MainPage!.DisplayPromptAsync(
     "Editar Ganho",
     "Ganho (R$):",
    initialValue: entry.Earned.ToString("F2"),
  keyboard: Keyboard.Numeric);

   if (string.IsNullOrEmpty(earnedStr)) return;

      string hoursStr = await Application.Current!.MainPage!.DisplayPromptAsync(
       "Editar Horas Trabalhadas",
  "Horas trabalhadas (HH:MM:SS):",
         initialValue: entry.WorkTime.ToString(@"hh\:mm\:ss"),
   keyboard: Keyboard.Text);

  if (string.IsNullOrEmpty(hoursStr)) return;

   // Validar e parsear
      if (!DateTime.TryParse(dateStr, out DateTime newDate))
   {
     await Application.Current!.MainPage!.DisplayAlert("Erro", "Data inválida!", "OK");
       return;
      }

    if (!float.TryParse(kmStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out float newKm) || newKm <= 0)
            {
      await Application.Current!.MainPage!.DisplayAlert("Erro", "Km inválido!", "OK");
      return;
    }

            if (!decimal.TryParse(earnedStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal newEarned) || newEarned <= 0)
    {
           await Application.Current!.MainPage!.DisplayAlert("Erro", "Ganho inválido!", "OK");
        return;
  }

     if (!TimeSpan.TryParse(hoursStr, out TimeSpan newWorkTime))
   {
    await Application.Current!.MainPage!.DisplayAlert("Erro", "Horas inválidas! Use o formato HH:MM:SS", "OK");
        return;
        }

  // Carregar todos os registros
   var allEntries = await LocalStorageService.LoadDataAsync<ObservableCollection<DayEntry>>() 
   ?? new ObservableCollection<DayEntry>();

    // Encontrar e atualizar o registro
     var entryToEdit = allEntries.FirstOrDefault(e => 
      e.Date == entry.Date && 
               e.Km == entry.Km && 
   e.Earned == entry.Earned &&
           e.StartTime == entry.StartTime);

       if (entryToEdit != null)
      {
      entryToEdit.Date = newDate;
       entryToEdit.Km = newKm;
 entryToEdit.Earned = newEarned;
            entryToEdit.WorkTime = newWorkTime;
      
     await LocalStorageService.SaveDataAsync(allEntries);
    
           await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Registro atualizado com sucesso!", "OK");
    
       // Recarregar dados
        await LoadDataAsync();
      }
   }
      catch (Exception ex)
            {
       await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao editar registro: {ex.Message}", "OK");
   }
        }

        partial void OnSelectedDateChanged(DateTime value)
        {
     _ = LoadDataAsync();
     }

        partial void OnViewModeChanged(string value)
  {
  _ = LoadDataAsync();
      }
    }
}
