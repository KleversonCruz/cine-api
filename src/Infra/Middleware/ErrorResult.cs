namespace Infra.Middleware
{
    public class ErrorResult
    {
        public List<string>? Messages { get; set; } = new();
        public string? Exception { get; set; }
        public int StatusCode { get; set; }
    }
}
