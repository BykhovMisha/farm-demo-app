using Microsoft.AspNetCore.Mvc;

namespace FarmDemoApp.API.Models.MiddlewareModels
{
    public class FarmValidationProblemDetails : ProblemDetails
    {
        public List<string>? Messages { get; }

        public FarmValidationProblemDetails() { }

        public FarmValidationProblemDetails(string message) 
            : this(string.IsNullOrWhiteSpace(message) ? null : new List<string> { message }) { }

        public FarmValidationProblemDetails(IEnumerable<string>? messages)
        {
            Messages = messages?.ToList();
        }
    }
}
