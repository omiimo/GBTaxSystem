using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxSystem.Application.Exceptions
{
    public class ValidationException : Exception
    {
        IDictionary<string, string[]> _errors;
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            _errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            _errors = errors;
        }

        public IDictionary<string, string[]> Errors { get => _errors; }
    }

}
