namespace Zanis.Ostad.Core.Dtos
{
    public class Response
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public static Response Success()
        {
            return new Response
            {
                Status = ResponseStatus.Success
            };
        }

        public static Response Failed(string message = "")
        {
            return new Response
            {
                Status = ResponseStatus.Fail,
                Message = message
            };
        }

        public static Response UnKnown(string message = "")
        {
            return new Response
            {
                Status = ResponseStatus.UnKnown,
                Message = message
            };
        }
    }


    public enum ResponseStatus
    {
        UnKnown,
        Success,
        Fail
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }

        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                Data = data,
                Status = ResponseStatus.Success
            };
        }

        public static Response<T> Failed(string message = "")
        {
            return new  Response<T>
            {
                Status = ResponseStatus.Fail,
                Message = message
            };
        }

        public static  Response<T> UnKnown(string message = "")
        {
            return new  Response<T>
            {
                Status = ResponseStatus.UnKnown,
                Message = message
            };
        }
    }
}