using System.Collections.Generic;

namespace API.Resources
{
    public class ErrorResource
    {
        public ErrorResource(List<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        public ErrorResource(string message)
        {
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message)) Messages.Add(message);
        }

        public bool Success => false;
        public List<string> Messages { get; }
    }
}