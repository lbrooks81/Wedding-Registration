namespace WedReg
{
    public static class Constants
    {
        public static String API_URL { get; private set; } = String.Empty;
        public static void SetAPI_URL(String url)
        {
            if(String.IsNullOrEmpty(API_URL) == false)
            {
                throw new InvalidOperationException("API_URL is already set.");
            }

            API_URL = url;
        }
    }
}
