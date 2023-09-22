using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMauiApp.ViewModels
{
    public class ViewModelBase
    {

        public virtual Task OnNavigatedToAsync() => Task.CompletedTask;
        public virtual Task OnAppearingAsync() => Task.CompletedTask;
        public virtual Task<bool> OnBackButtonPressedAsync() => Task.FromResult(false);
        public virtual Task OnBindingContextChangedAsync() => Task.CompletedTask;
        public virtual Task OnChildMeasureInvalidatedAsync(object sender, EventArgs e) => Task.CompletedTask;
        public virtual Task OnDisappearingAsync() => Task.CompletedTask;
        public virtual Task OnNavigatedFromAsync(NavigatedFromEventArgs args) => Task.CompletedTask;
        public virtual Task OnNavigatedToAsync(NavigatedToEventArgs args) => Task.CompletedTask;
        public virtual Task OnNavigatingFromAsync(NavigatingFromEventArgs args) => Task.CompletedTask;
        public virtual Task OnParentSetAsync() => Task.CompletedTask;
        public virtual Task OnSizeAllocatedAsync(double width, double height) => Task.CompletedTask;

    }
}
