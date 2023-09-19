using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.CodeGeneratorHelpers
{
    public class GenerationBuilder
    {

        string viewModelPath = null;
        string pagesPath = null;

        string viewModelSuffix = "ViewModel";
        string pageSuffix = "Page";

        string mobileAppLocation = null;
        string mobileProjectName = null;


        public GenerationBuilder WithMobileProjectName(string name)
        {
            mobileProjectName = name;
            mobileAppLocation ??= name;
            return this;
        }


        public GenerationBuilder WithViewModelPath(string path)
        {
            viewModelPath = path;
            return this;
        }

        public GenerationBuilder WithPagesPath(string path)
        {
            pagesPath = path;
            return this;
        }

        public GenerationBuilder WithViewModelSuffix(string suffix)
        {
            viewModelSuffix = suffix;
            return this;
        }

        public GenerationBuilder WithPageSuffix(string suffix)
        {
            pageSuffix = suffix;
            return this;
        }

        public GenerationBuilder WithMobileAppLocation(string location)
        {
            mobileAppLocation = location;
            return this;
        }

        public Task GenerateAsync()
        {
            if (mobileProjectName is null)
                throw new ArgumentNullException(nameof(mobileProjectName), "Specify mobile project using WithMobileProjectName()");

            string generatedFileName = null;


            return Task.CompletedTask;
        }

    }
}
