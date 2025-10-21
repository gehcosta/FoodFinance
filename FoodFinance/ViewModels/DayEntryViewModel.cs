using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFinance.Models;
using FoodFinance.Services;
using System.Collections.ObjectModel;

namespace FoodFinance.ViewModels
{
 public partial class DayEntryViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime date = DateTime.Today;

      [ObservableProperty]
      private string km = string.Empty;

    [ObservableProperty]
        private string earned = string.Empty;

        [ObservableProperty]
        private bool timerRunning;

        [ObservableProperty]
     private string elapsedTimeText = "00:00:00";

   private TimeSpan startTime;
  private TimeSpan elapsedTime;
      private System.Timers.Timer? updateTimer;

    public DayEntryViewModel()
 {
      updateTimer = new System.Timers.Timer(1000);
        updateTimer.Elapsed += UpdateTimer_Elapsed;
    }

  private void UpdateTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
     {
   if (TimerRunning)
        {
      var currentElapsed = elapsedTime + (DateTime.Now.TimeOfDay - startTime);
      ElapsedTimeText = currentElapsed.ToString(@"hh\:mm\:ss");
     }
        }

        [RelayCommand]
    public void ToggleTimer()
        {
 TimerRunning = !TimerRunning;

        if (TimerRunning)
        {
 startTime = DateTime.Now.TimeOfDay;
  updateTimer?.Start();
 }
     else
      {
      elapsedTime += DateTime.Now.TimeOfDay - startTime;
  updateTimer?.Stop();
 ElapsedTimeText = elapsedTime.ToString(@"hh\:mm\:ss");
 }
     }

   [RelayCommand]
        public async Task FinalizeDay()
    {
     // Verificar se as configurações foram preenchidas
          var settings = await LocalStorageService.LoadSettingsAsync<Settings>();
   if (settings.GasPrice == 0 || settings.KmPerLiter == 0)
    {
        await Application.Current!.MainPage!.DisplayAlertAsync("Atenção", 
    "Por favor, preencha as configurações antes de registrar um dia de trabalho.", "OK");
 
   // Navegar para a página de configurações
         await Shell.Current.GoToAsync("//Settings");
         return;
   }

     if (string.IsNullOrWhiteSpace(Km) || string.IsNullOrWhiteSpace(Earned))
         {
  await Application.Current!.MainPage!.DisplayAlertAsync("Erro", "Por favor, preencha todos os campos.", "OK");
     return;
  }

      if (!float.TryParse(Km, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var kmValue) || kmValue <= 0)
  {
  await Application.Current!.MainPage!.DisplayAlertAsync("Erro", "Km inválido.", "OK");
   return;
 }

      if (!decimal.TryParse(Earned, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var earnedValue) || earnedValue <= 0)
 {
      await Application.Current!.MainPage!.DisplayAlertAsync("Erro", "Ganho inválido.", "OK");
   return;
  }

    var finalElapsedTime = elapsedTime + (TimerRunning ? DateTime.Now.TimeOfDay - startTime : TimeSpan.Zero);

    var entry = new DayEntry
            {
         Date = Date,
     Km = kmValue,
 Earned = earnedValue,
   WorkTime = finalElapsedTime,
     StartTime = startTime,
      EndTime = DateTime.Now.TimeOfDay
    };

      var allEntries = await LocalStorageService.LoadDataAsync<ObservableCollection<DayEntry>>() ?? new ObservableCollection<DayEntry>();
   allEntries.Add(entry);
          await LocalStorageService.SaveDataAsync(allEntries);

      await Application.Current!.MainPage!.DisplayAlertAsync("Sucesso", 
    $"Dia finalizado!\nKm: {kmValue:F2}\nGanho: R$ {earnedValue:F2}\nTempo: {finalElapsedTime:hh\\:mm\\:ss}", "OK");

       // Reset
  Km = string.Empty;
      Earned = string.Empty;
            Date = DateTime.Today;
         elapsedTime = TimeSpan.Zero;
          ElapsedTimeText = "00:00:00";
            TimerRunning = false;
  updateTimer?.Stop();
        }
    }
}
