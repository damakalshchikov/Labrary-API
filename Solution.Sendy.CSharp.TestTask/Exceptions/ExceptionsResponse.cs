using Microsoft.AspNetCore.Mvc;

namespace Solution.Sendy.CSharp.TestTask.Exceptions;

public class ExceptionsResponse : ProblemDetails
{
    public ExceptionsResponse(string detail, string type, int statusCode) 
    {
        Detail = detail;
        Type = type;
        Status = statusCode;
    }
}