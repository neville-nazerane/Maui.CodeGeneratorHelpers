using CodeGeneratorHelpers.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorHelpers.Maui.Internal
{
    internal class PageEventData
    {

        internal PageEventType Type { get; set; }

        internal bool IsAwaitable { get; set; }

        internal string FunctionName { get; set; }

        internal PageEventData(PageEventType type, string functionName, bool isAwaitable)
        {
            Type = type;
            IsAwaitable = isAwaitable;
            FunctionName = functionName;
        }


    }
}
