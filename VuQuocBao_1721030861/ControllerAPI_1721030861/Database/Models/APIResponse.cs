namespace ControllerAPI_1721030861.Database.Models
{
    public class APIResponse<T>
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; }
        public T? Data { get; set; }

        public APIResponse(T _Data)
        {
            Message = "Ok";
            Data = _Data;
        }
        public APIResponse(int _Code, string _Message)
        {
            Code = _Code;
            Message = _Message;
        }
        public APIResponse(int _Code, string _Message, T _Data)
        {
            Code = _Code;
            Message = _Message;
            Data = _Data;
        }
        public APIResponse(string _Message, T _Data)
        {
            Message = _Message;
            Data = _Data;
        }
        public APIResponse(int _Code, T _Data)
        {
            Code = _Code;
            Message = "";
            Data = _Data;
        }
    }
}
