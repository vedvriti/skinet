using System;

namespace API.Error;

public class APIErrorResponse(int statauscode,string message,string? details)
{
    //Specify properties in the class that we are going to return when an exception occurs
     public int StatusCode{get;set;} = statauscode;
     public string Message {get;set;} = message;
     public string? Details {get;set;} = details;//Exception Details Stack trace - we do want to return it when we are in developement but we dont want to return when we are in production
}
