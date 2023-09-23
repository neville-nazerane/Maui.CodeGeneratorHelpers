using CodeGeneratorHelpers.Maui.Models;
using Maui.CodeGeneratorHelpers;





await CodeGenerationBuilder.WithNewInstance()
                           .WithMobileProjectName("SampleMauiApp")
                           .WithExecutionLocations("SampleGeneratorConsoleApp")

                           .AddPageToViewModelEvent(PageEventType.OnNavigatedTo, "OnNavigatedToAsync", true)
                           .AddPageToViewModelEvent(PageEventType.OnNavigatedFrom, "OnNavigatedFromAsync", true)

                           .GenerateAsync();
