namespace VaultPdmTest.Models
{
    public class LogInResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserData UserData { get; set; }
        
        public AuthorizedState AuthorizedState { get; set; }
    }
}