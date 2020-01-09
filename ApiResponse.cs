namespace EmailService
{
    public class ApiResponse
    {
        public bool Status  { get; set; }
        public object Response { get; set; }
        public string Message { get; set; }
    }
}