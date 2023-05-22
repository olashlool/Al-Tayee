namespace Admin.Models.ViewModels
{
    public class ReturnResponse
    {
        public bool? IsSuccess { get; set; }
        public string Code { get; set; }

        public object Data { get; set; }
        public int NewInt { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
    }
    public enum Status
    {
        Success,
        Error,

    }
}
