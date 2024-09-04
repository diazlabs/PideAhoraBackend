
namespace Domain.Common
{
    public class Response<T>
    {
        public bool Ok { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string>? GeneralErrors { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
