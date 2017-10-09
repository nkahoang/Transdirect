using System;
using Refit;

namespace Transdirect.Exceptions
{
    public class TransdirectException : Exception
    {
        public TransdirectException(string message, Exception innerException) : base(message, innerException) { }
        internal static TransdirectException FromApiException(ApiException exception)
        {
            var message = exception.Message;

            if (!exception.HasContent)
            {
                return new TransdirectException(message, exception) { };
            }

            if (!exception.ContentHeaders.ContentType.MediaType.Contains("application/json"))
            {
                return new TransdirectException(message, exception)
                {
                    RawContent = exception.Content
                };
            }

            var c = exception.GetContentAs<TransdirectError>();

            return new TransdirectException(message, exception)
            {
                RawContent = exception.Content,
                Error = c,
            };
        }

        public string RawContent { get; set; }
        public TransdirectError Error { get; set; }
    }
}