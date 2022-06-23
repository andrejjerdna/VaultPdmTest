namespace VaultPdmTest.Contracts
{
    public interface ICryptoServiceProvider
    {
        string GetHash(string value);
        
        bool Verify(string value, string hash);
    }
}