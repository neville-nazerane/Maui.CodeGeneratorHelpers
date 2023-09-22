using Maui.CodeGeneratorHelpers;





await GenerationBuilder.WithNewInstance()
                       .WithMobileProjectName("SampleMauiApp")
                       .WithExecutionLocations("SampleGeneratorConsoleApp")
                       .GenerateAsync();
