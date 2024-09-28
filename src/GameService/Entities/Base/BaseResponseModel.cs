namespace GameService.Entities.Base;

public class BaseResponseModel
{
    public object Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}