namespace IFOA.Exceptions;

public class IfoaManagedException : System.Exception
{
    public string ErrorCode { get; set; }

    public IfoaManagedException(string message)
        : base(message)
    {
    }
}