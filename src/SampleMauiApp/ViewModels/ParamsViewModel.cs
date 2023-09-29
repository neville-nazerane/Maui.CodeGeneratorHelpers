using CodeGenerator.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMauiApp.ViewModels
{

    [ForQueryParameter(nameof(Name), "name")]
    public partial class ParamsViewModel : ViewModelBase
    {

        [ObservableProperty]
        string name;


    }
}
