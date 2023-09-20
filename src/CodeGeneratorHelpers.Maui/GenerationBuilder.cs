using Maui.CodeGeneratorHelpers.Internal;
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
        string generatedFolderName = "Generated";

        string viewModelSuffix = "ViewModel";
        string pageSuffix = "Page";

        string mobileAppLocation = null;
        string mobileProjectName = null;

        IEnumerable<string> executionLocations = Array.Empty<string>();


        /// <summary>
        /// Possible project paths generator could be executed from
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public GenerationBuilder WithExecutionLocations(params string[] locations)
        {
            executionLocations = locations;
            return this;
        }

        public GenerationBuilder WithGeneratedFolderName(string folderName)
        {
            generatedFolderName = folderName;
            return this;
        }


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
            var locations = new HashSet<string>(executionLocations)
            {
                mobileAppLocation
            };
            string rootPath = Directory.GetCurrentDirectory().ToFullRootPath(locations);
            string fullMobilePath = rootPath.Combine(mobileAppLocation);
            string fullPagePath = fullMobilePath.Combine(pagesPath);
            string fullViewModelPath = fullMobilePath.Combine(viewModelPath);
            string generationPath = fullViewModelPath.Combine(generatedFolderName);




            return Task.CompletedTask;
        }

    }
}
