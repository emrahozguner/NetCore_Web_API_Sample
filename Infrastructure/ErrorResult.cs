using Microsoft.AspNetCore.Mvc;

namespace API.Infrastructure
{
    public class ErrorResult : ObjectResult
    {
        public ErrorResult(ErrorModel resultModel, int statusCode)
            : base(new ErrorResultModel(resultModel))
        {
            StatusCode = statusCode;
        }
    }

    public class ErrorResultModel
    {
        public ErrorResultModel(ErrorModel resultModel)
        {
            Error = resultModel;
        }

        public ErrorModel Error { get; set; }
    }

    public class ErrorModel
    {
        public string Target { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }
    }
}