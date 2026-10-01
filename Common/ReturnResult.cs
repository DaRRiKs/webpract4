namespace webpract4.Common;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }
}
