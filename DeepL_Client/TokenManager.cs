namespace DeepL_Client
{
    public class TokenManager
    {
        public TokenManager()
        {
            DeepL_Token = "";
        }

        public string DeepL_Token { get; private set; }
    }
}
