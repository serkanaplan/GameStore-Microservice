namespace BasketService.Model;

public class ResponseModel<T>
{
    public bool isSuccess { get; set; } = false;
    public string Message { get; set; }
    public T Data { get; set; }
}