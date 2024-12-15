using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
        public class ExceptionList : Exception
        {
            public List<string> Errors
            { get; }

            public ExceptionList(List<string> errors)
            { Errors = errors; }

            public override string Message
            { get => string.Join("\n", Errors); }
        }
}
