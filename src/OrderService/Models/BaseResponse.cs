namespace OrderService.Models;

public class BaseResponse
{
    public object Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}