using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMauiApp.ViewModels
{
    public partial class FirstViewModel : ViewModelBase
    {

        [ObservableProperty]
        string name;

        [RelayCommand]
        Task ToParamsAsync() => Shell.Current.GoToAsync($"//params?name={Name}");

    }
}
