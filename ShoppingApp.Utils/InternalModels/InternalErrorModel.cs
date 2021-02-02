using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Utils.InternalModels
{
    public class InternalErrorModel
    {
        public int Code { get; set; }
        public ErrorType Type { get; set; }
        public string Message { get; set; }
    }
}
